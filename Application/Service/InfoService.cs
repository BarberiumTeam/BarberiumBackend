using Application.Abstraction;
using Contracts.DollarRate.Response;

namespace Application.Service
{
    public class InfoService : IInfoService
    {
        private readonly IDollarClient _dolarClient;

        public InfoService(IDollarClient dolarClient)
        {
            _dolarClient = dolarClient;
        }

        public async Task<DolarRateDto?> GetBlueDolarPriceAsync(CancellationToken cancellationToken = default)
        {
            return await _dolarClient.GetBlueRateAsync(cancellationToken);
        }
    }
}