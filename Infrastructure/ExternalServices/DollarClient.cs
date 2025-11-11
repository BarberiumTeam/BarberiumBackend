using Application.Abstraction;
using Contracts.DollarRate.Response;
using System.Net.Http.Json;
using System.Threading;

namespace Infrastructure.ExternalServices
{
    public class DolarClient : IDollarClient
    {
        private readonly HttpClient _httpClient;

        public DolarClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IReadOnlyList<DolarRateDto>> GetRatesAsync(CancellationToken cancellationToken = default)
        {
            // Usamos ruta relativa vacía ("") porque la BaseUrl en appsettings.json es el endpoint final.
            var url = "";

            // La API devuelve UN solo objeto DolarRateDto.
            var rate = await _httpClient.GetFromJsonAsync<DolarRateDto>(url, cancellationToken);

            // Devolvemos el resultado como una lista de un solo elemento (para mantener el contrato IDollarClient).
            return rate != null ? new List<DolarRateDto> { rate } : new List<DolarRateDto>();
        }
    }
}