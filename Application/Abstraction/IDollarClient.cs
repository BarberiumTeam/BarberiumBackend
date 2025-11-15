using Contracts.DollarRate.Response;
using System.Threading;

namespace Application.Abstraction
{
    public interface IDollarClient
    {
      Task<DolarRateDto?> GetBlueRateAsync(CancellationToken cancellationToken = default);
    }

    // Esto es un cliente para obtener la cotización del dólar blue 
    // Se le dice cliente porque probablemente hará una llamada HTTP a una API externa
    // La interfaz define un método asíncrono que devuelve un objeto DolarRateDto
    // que contiene la información de la cotización.
    // Se utiliza CancellationToken para permitir la cancelación de la operación si es necesario.



    // La implementación concreta de esta interfaz se encargará de realizar la llamada a la API externa
    // para obtener la cotización del dólar blue.
    // El uso de un cliente dedicado para esta funcionalidad permite separar las preocupaciones
    // y facilita el mantenimiento y las pruebas del código.
    // Además, al definir una interfaz, se puede cambiar la implementación
    // sin afectar al resto de la aplicación.
    // Esto es especialmente útil si se desea cambiar la fuente de datos
}