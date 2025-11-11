using Application.Abstraction;
using Contracts.DollarRate.Response;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

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
            // El cliente ya trae solo el Dólar Blue en una lista de un elemento.
            IReadOnlyList<DolarRateDto> allRates = await _dolarClient.GetRatesAsync(cancellationToken);

            // Devolvemos el primer (y único) elemento.
            return allRates.FirstOrDefault();
        }
    }
}