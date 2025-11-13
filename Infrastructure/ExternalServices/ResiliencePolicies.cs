using Polly;
using Polly.Extensions.Http;
using System.Net;

namespace Infrastructure.ExternalServices
{
    public static class ResiliencePolicies
    {
        // 1. Política de WAIT AND RETRY
        public static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy(PollySettings config)
        {
            return HttpPolicyExtensions
                // Maneja errores transitorios (Timeout, 5xx)
                .HandleTransientHttpError()
                // Añadimos el manejo de 400 Bad Request si la API de terceros lo usa para fallos de negocio
                .OrResult(msg => msg.StatusCode == HttpStatusCode.BadRequest)
                .WaitAndRetryAsync(
                    config.RetryCount,
                    // Espera exponencial: 2^n * base de segundos (ej: 2s, 4s, 8s)
                    retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt) * config.RetryAttemptInSeconds)
                );
        }

        // 2. Política de CIRCUIT BREAKER
        public static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy(PollySettings config)
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(msg => msg.StatusCode == HttpStatusCode.BadRequest)
                .CircuitBreakerAsync(
                    config.HandledEventsAllowedBeforeBreaking,
                    TimeSpan.FromSeconds(config.DurationOfBreakInSeconds)
                );
        }
    }
}