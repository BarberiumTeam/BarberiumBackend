using Contracts.DollarRate.Response;
using System.Threading;

namespace Application.Abstraction
{
    public interface IDollarClient
    {
      Task<DolarRateDto?> GetBlueRateAsync(CancellationToken cancellationToken = default);
    }
}