using Application.Abstraction;
using Contracts.Turn.Request;
using Contracts.Turn.Response;
using Domain.Entity;

namespace Application.Service
{
    public class TurnService : ITurnService
    {
        private readonly ITurnRepository _turnRepository;

        public TurnService(ITurnRepository turnRepository)
        {
            _turnRepository = turnRepository;
        }

        public List<TurnResponse> GetAllTurns()
        {
            var turns = _turnRepository.GetAllTurns();

            return turns.Select(turn => new TurnResponse
            {
                Id = turn.Id,
                Client = turn.Client?.Name ?? string.Empty,
                Barber = turn.Barber?.Name ?? string.Empty,
                DateTimeTurn = turn.DateTimeTurn,
                Service = turn.Service,
                Start = turn.Start,
                End = turn.End,
                State = turn.State
            }).ToList();
        }

        public TurnResponse? GetTurnById(int id)
        {
            var turn = _turnRepository.GetTurnById(id);
            if (turn == null) return null;

            return new TurnResponse
            {
                Id = turn.Id,
                Client = turn.Client?.Name ?? string.Empty,
                Barber = turn.Barber?.Name ?? string.Empty,
                DateTimeTurn = turn.DateTimeTurn,
                Service = turn.Service,
                Start = turn.Start,
                End = turn.End,
                State = turn.State
            };
        }

        public bool CreateTurn(CreateTurnRequest request)
        {
            if (request.End <= request.Start) return false;

            var entity = new Turn
            {
                ClientId = request.ClientId,
                BarberId = request.BarberId,
                DateTimeTurn = request.DateTimeTurn,
                Service = request.Service,
                Start = request.Start,
                End = request.End,
                State = State.Scheduled
            };
            return _turnRepository.CreateTurn(entity);
        }

        public bool UpdateTurn(int id, UpdateTurnRequest request)
        {
            var turnToUpdate = _turnRepository.GetTurnById(id);

            if (turnToUpdate == null || request.End <= request.Start) return false;


            turnToUpdate.DateTimeTurn = request.DateTimeTurn;
            turnToUpdate.Service = request.Service;
            turnToUpdate.Start = request.Start;
            turnToUpdate.End = request.End;

            return _turnRepository.UpdateTurn(turnToUpdate);
        }


        public bool UpdateTurnServiceType(int id, ServiceType newServiceType)
        {
            
            return _turnRepository.UpdateTurnServiceType(id, newServiceType);
        }

        
        public bool UpdateTurnState(int id, State newState)
        {
            return _turnRepository.UpdateTurnState(id, newState);
        }

        public bool DeleteTurn(int id) => _turnRepository.DeleteTurn(id);
    }
}
