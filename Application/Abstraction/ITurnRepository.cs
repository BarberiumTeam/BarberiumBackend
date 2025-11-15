using Domain.Entity;

namespace Application.Abstraction
{
    public interface ITurnRepository
    {
        Turn? GetTurnById(int id);
        List<Turn> GetAllTurns();
        bool CreateTurn(Turn turn);
        bool UpdateTurn(Turn turn);
        bool UpdateTurnState(int id, State newState);
        bool UpdateTurnServiceType(int id, ServiceType newServiceType);
        bool DeleteTurn(int id);

        // para las validaciones 
        bool ExistsClient(int clientId);
        bool ExistsBarber(int barberId);

        // para la validacion de turnos solapados pero en la CREACION
        bool IsTimeSlotBooked(int barberId, DateOnly date, TimeOnly startTime, TimeOnly endTime);

        // para la validacion de turnos solapados pero en la ACTUALIZACION, con un parametro mas.
        bool IsTimeSlotBooked2(int barberId, DateOnly date, TimeOnly startTime, TimeOnly endTime, int turnIdToExclude);

    }
}
