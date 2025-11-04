using Contracts.Turn.Request;
using Contracts.Turn.Response;
using Domain.Entity;

namespace Application.Service
{
    public interface ITurnService
    {
        TurnResponse? GetTurnById(int id);
        List<TurnResponse> GetAllTurns();
        bool CreateTurn(CreateTurnRequest request);
        bool UpdateTurn(int id, UpdateTurnRequest request);
        bool UpdateTurnState(int id, State newState);
        bool UpdateTurnServiceType(int id, ServiceType newServiceType);
        bool DeleteTurn(int id);
    }
}