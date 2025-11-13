using Application.Abstraction;
using Contracts.DollarRate.Response;
using System.Net.Http.Json;
using System.Threading;

namespace Infrastructure.ExternalServices
{
    public class DolarClient : IDollarClient
    {
        // No se inyecta el BarberiumDbContext, ya que en esta ocasion 
        // No estamos trayendo cosas de nuestro servidor, si no, de uno externo.
        private readonly HttpClient _httpClient;

        public DolarClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<DolarRateDto?> GetBlueRateAsync(CancellationToken cancellationToken = default)
        {
            var url = ""; // Ruta vacía, ya que la BaseUrl es el endpoint final.

            return await _httpClient.GetFromJsonAsync<DolarRateDto>(url, cancellationToken);
        }

    }
}