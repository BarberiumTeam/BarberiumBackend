namespace Infrastructure.ExternalServices
{
    public class PollySettings
    {
        // Número de veces que reintentará la petición
        public int RetryCount { get; set; }

        // Base para el tiempo de espera exponencial entre reintentos
        public int RetryAttemptInSeconds { get; set; }

        // Número de fallos consecutivos antes de "abrir" el cortacircuitos
        public int HandledEventsAllowedBeforeBreaking { get; set; }

        // Tiempo que el cortacircuitos permanecerá abierto antes de reintentar
        public int DurationOfBreakInSeconds { get; set; }
    }
}
