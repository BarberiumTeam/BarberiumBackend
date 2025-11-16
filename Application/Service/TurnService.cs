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

            // 2.5 validacion de disponibilidad del Barbero (Schedule y Exception)
            if (!IsBarberAvailable(
                request.BarberId, request.TurnDate, request.TurnStartTime, request.TurnEndTime))
            {
                return false;
            }

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

            // 1.5 validacion para la disponibilidad del Barbero para las nuevas horas
            if (!IsBarberAvailable(
                turnToUpdate.BarberId, request.TurnDate, request.TurnStartTime, request.TurnEndTime))
            {
                return false; // El nuevo horario o fecha no es válido por Schedule/Excepción.
            }

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

        private bool IsBarberAvailable(int barberId, DateOnly date, TimeOnly startTime, TimeOnly endTime)
        {
            // 1. Verificar EXCEPCIONES DE CALENDARIO (Vacaciones, Enfermedad, etc.)
            var exceptions = _turnRepository.GetScheduleExceptionsByDate(barberId, date);

            foreach (var exception in exceptions)
            {
                // Una excepción de fecha completa (como vacaciones) anula la disponibilidad
                // Si la excepción no tiene un rango de tiempo específico (00:00 a 00:00, o si se maneja como día completo)
                // se asume que el barbero NO está disponible todo el día.

                // Aquí se requiere una decisión de diseño: si TimeOnly es 00:00-00:00, ¿es el día completo?
                // Vamos a asumir que si se encuentra CUALQUIER excepción para esa fecha, el barbero NO está disponible.
                // Si quieres manejar excepciones por hora (ej: "No atiende de 14:00 a 15:00"), la lógica es más compleja,
                // pero para días completos de excepción (Vacation, Holiday) esto es suficiente.
                return false; // El barbero tiene una excepción de día completo (o parcial)
            }

            // 2. Verificar HORARIO SEMANAL REGULAR
            // Se debe mapear la fecha (DateOnly) al día de la semana (WeekDay)
            // Nota: El tipo WeekDay en C# es System.DayOfWeek. Tu enum WeekDay no incluye Monday.
            // Aquí usamos el DayOfWeek de .NET y lo convertimos a tu enum WeekDay para la búsqueda.
            var dayOfWeek = date.DayOfWeek;

            // Convertir el DayOfWeek de .NET a tu WeekDay enum
            // Si tu enum no incluye Monday, por ejemplo, puedes verificar si la conversión es posible.
            if (!Enum.IsDefined(typeof(WeekDay), dayOfWeek.ToString()))
            {
                // Si es Lunes (Monday) y no está en tu enum, el barbero NO está disponible.
                return false;
            }

            WeekDay turnWeekDay = (WeekDay)Enum.Parse(typeof(WeekDay), dayOfWeek.ToString());

            var schedules = _turnRepository.GetBarberSchedules(barberId)
                                           .Where(s => s.WeekDay == turnWeekDay)
                                           .ToList();

            if (!schedules.Any())
            {
                return false; // No hay horario definido para ese día (ej: Lunes/Domingo o no definido)
            }

            // Verificar si el horario del turno solicitado cae dentro de AL MENOS UN horario de trabajo.
            bool isWithinSchedule = schedules.Any(schedule =>
                // El inicio del turno debe ser mayor o igual al inicio del schedule
                startTime >= schedule.StartTime &&
                // El fin del turno debe ser menor o igual al fin del schedule
                endTime <= schedule.EndTime
            );

            return isWithinSchedule;
        }
    }
}
