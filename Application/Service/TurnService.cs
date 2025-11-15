using Application.Abstraction;
using Contracts.Turn.Request;
using Contracts.Turn.Response;
using Domain.Entity;

namespace Application.Service
{
    // hereda de la interfaz ITurnService (es el contrato de la Lógica de Negocio)
    // e implementa todos los metodos en su contrato(que el controlador espera usar, y devuelve DTOs)
    // utiliza el repositorio ITurnRepository para interactuar con la base de datos
    public class TurnService : ITurnService
    {
        // declara una variable privada de solo lectura para que viva en la clase TurnService
        private readonly ITurnRepository _turnRepository;
        // constructor que recibe una instancia de ITurnRepository a traves de inyeccion de dependencias
        // asigna la instancia recibida a la variable privada _turnRepository
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
                TurnDate = turn.TurnDate,
                Service = turn.Service,
                TurnStartTime = turn.TurnStartTime,
                TurnEndTime = turn.TurnEndTime,
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
                TurnDate = turn.TurnDate,
                Service = turn.Service,
                TurnStartTime = turn.TurnStartTime,
                TurnEndTime = turn.TurnEndTime,
                State = turn.State
            };
        }

        public bool CreateTurn(CreateTurnRequest request)
        {
            // 1. validacion de horarios para que la hora de fin sea mayor a la de inicio
            if (request.TurnEndTime <= request.TurnStartTime) return false;

            // 2. validacion de existencia de cliente y barbero
            if (!_turnRepository.ExistsClient(request.ClientId))
                return false;

            if (!_turnRepository.ExistsBarber(request.BarberId))
                return false;

            // 3. validar si el barbero ya tiene un turno reservado en el mismo horario
            if (_turnRepository.IsTimeSlotBooked(
                request.BarberId, request.TurnDate, request.TurnStartTime, request.TurnEndTime))
                return false;

            var entity = new Turn
            {
                ClientId = request.ClientId,
                BarberId = request.BarberId,
                TurnDate = request.TurnDate,
                Service = request.Service,
                TurnStartTime = request.TurnStartTime,
                TurnEndTime = request.TurnEndTime,
                State = State.Scheduled
            };
            return _turnRepository.CreateTurn(entity);
        }

        public bool UpdateTurn(int id, UpdateTurnRequest request)
        {
            var turnToUpdate = _turnRepository.GetTurnById(id);

            // 1. Validación básica de existencia y horario
            if (turnToUpdate == null || request.TurnEndTime <= request.TurnStartTime) return false;

            // 2. VALIDACIÓN DE SUPERPOSICIÓN (usando la versión de ACTUALIZACIÓN)
            if (_turnRepository.IsTimeSlotBooked2(
                turnToUpdate.BarberId,
                request.TurnDate,
                request.TurnStartTime,
                request.TurnEndTime,
                id //  ID que debe ser EXCLUIDO de la búsqueda de conflicto
            ))
            {
                return false; // Conflicto con otro turno encontrado.
            }

            turnToUpdate.TurnDate = request.TurnDate;
            turnToUpdate.Service = request.Service;
            turnToUpdate.TurnStartTime = request.TurnStartTime;
            turnToUpdate.TurnEndTime = request.TurnEndTime;

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
