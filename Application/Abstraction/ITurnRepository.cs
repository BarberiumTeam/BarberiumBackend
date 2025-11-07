using Domain.Entity;

namespace Application.Abstraction
{
    public interface ITurnRepository
    {
        Turn? GetTurnById(int id);
        List<Turn> GetAllTurns();
        bool CreateTurn (Turn turn);
        bool UpdateTurn (Turn turn);
        bool UpdateTurnState (int id, State newState);
        bool UpdateTurnServiceType(int id, ServiceType newServiceType);
        bool DeleteTurn (int id);
    }
}
