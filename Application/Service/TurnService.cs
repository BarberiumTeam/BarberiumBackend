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
            var today = DateOnly.FromDateTime(DateTime.Now);
            var nowTime = TimeOnly.FromDateTime(DateTime.Now);

            // 0. Validación de fechas pasadas
            if (request.TurnDate < today) return false;

            // Si es hoy, validar que la hora de inicio no haya pasado ya
            if (request.TurnDate == today && request.TurnStartTime <= nowTime) return false;

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
            if (turnToUpdate == null) return false;

            var today = DateOnly.FromDateTime(DateTime.Now);
            var nowTime = TimeOnly.FromDateTime(DateTime.Now);

            // 0. Validación de fechas pasadas para la nueva fecha sugerida
            if (request.TurnDate < today) return false;

            // Si el nuevo horario es para hoy, validar que no haya pasado
            if (request.TurnDate == today && request.TurnStartTime <= nowTime) return false;

            // 1. Validación básica de horario (fin > inicio)
            if (request.TurnEndTime <= request.TurnStartTime) return false;

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
                id // ID que debe ser EXCLUIDO de la búsqueda de conflicto
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
            // 1. Verificar EXCEPCIONES DE CALENDARIO (Vacaciones, Permisos, Horas de comida)
            var exceptions = _turnRepository.GetScheduleExceptionsByDate(barberId, date);

            foreach (var exception in exceptions)
            {
                // CASO A: La excepción cubre todo el día (ej. estamos en el medio de sus vacaciones)
                if (date > exception.ExceptionStartDate && date < exception.ExceptionEndDate)
                {
                    return false;
                }

                // CASO B: El turno es el MISMO día que empieza una excepción larga
                if (date == exception.ExceptionStartDate && date < exception.ExceptionEndDate)
                {
                    // Si el turno termina DESPUÉS de que empieza la excepción, choca.
                    if (endTime > exception.ExceptionStartTime) return false;
                }

                // CASO C: El turno es el MISMO día que termina una excepción larga
                if (date > exception.ExceptionStartDate && date == exception.ExceptionEndDate)
                {
                    // Si el turno empieza ANTES de que termine la excepción, choca.
                    if (startTime < exception.ExceptionEndTime) return false;
                }

                // CASO D (El que te fallaba): La excepción empieza y termina el MISMO día (ej: Break de 10 a 14)
                if (date == exception.ExceptionStartDate && date == exception.ExceptionEndDate)
                {
                    // Fórmula para saber si dos rangos de horas se chocan:
                    // (Inicio1 < Fin2) Y (Fin1 > Inicio2)
                    if (startTime < exception.ExceptionEndTime && endTime > exception.ExceptionStartTime)
                    {
                        return false; // Se solapan, no está disponible
                    }
                }
            }

            // 2. Verificar HORARIO SEMANAL REGULAR
            var dayOfWeek = date.DayOfWeek;

            if (!Enum.IsDefined(typeof(WeekDay), dayOfWeek.ToString()))
            {
                return false;
            }

            WeekDay turnWeekDay = (WeekDay)Enum.Parse(typeof(WeekDay), dayOfWeek.ToString());

            var schedules = _turnRepository.GetBarberSchedules(barberId)
                                           .Where(s => s.WeekDay == turnWeekDay)
                                           .ToList();

            if (!schedules.Any())
            {
                return false; // No atiende ese día de la semana
            }

            // Verificar si el turno cae DENTRO del horario de trabajo regular
            bool isWithinSchedule = schedules.Any(schedule =>
                startTime >= schedule.StartTime &&
                endTime <= schedule.EndTime
            );

            return isWithinSchedule;
        }
    }
}
