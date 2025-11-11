using Contracts.DollarRate.Response;
using System.Threading;

namespace Application.Abstraction
{
    public interface IDollarClient
    {
        Task<IReadOnlyList<DolarRateDto>> GetRatesAsync(CancellationToken cancellationToken = default);
    }
}