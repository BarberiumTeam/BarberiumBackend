using Contracts.DollarRate.Response;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Service
{
    // Define el contrato para el servicio de información
    public interface IInfoService
    {
        // Método para obtener el precio de venta del Dólar Blue
        Task<DolarRateDto?> GetBlueDolarPriceAsync(CancellationToken cancellationToken = default);
    }
}