using Application.Abstraction;
using Contracts.ScheduleException.Request;
using Contracts.ScheduleException.Response;
using Domain.Entity;

namespace Application.Service
{
    public class ScheduleExceptionService : IScheduleExceptionService
    {
        private readonly IScheduleExceptionRepository _scheduleExceptionRepository;
        private readonly IBarberRepository _barberRepository;

        public ScheduleExceptionService(IScheduleExceptionRepository scheduleExceptionRepository, IBarberRepository barberRepository)
        {
            _scheduleExceptionRepository = scheduleExceptionRepository;
            _barberRepository = barberRepository;
        }

        public List<ScheduleExceptionResponse> GetAllScheduleExceptions() 
        {
            var scheduleExceptions = _scheduleExceptionRepository.GetAllScheduleExceptions();
            return scheduleExceptions.Select(scheduleExceptions => new ScheduleExceptionResponse
            {
                Id = scheduleExceptions.Id,
                BarberId = scheduleExceptions.BarberId,
                Barber = scheduleExceptions.Barber?.Name ?? string.Empty,
                ExceptionStartDate = scheduleExceptions.ExceptionStartDate,
                ExceptionEndDate = scheduleExceptions.ExceptionEndDate,
                ExceptionStartTime = scheduleExceptions.ExceptionStartTime,
                ExceptionEndTime = scheduleExceptions.ExceptionEndTime,
                ExceptionType = scheduleExceptions.ExceptionType
            }).ToList();
        }

        public ScheduleExceptionResponse? GetScheduleExceptionById(int id)
        {
            var scheduleException = _scheduleExceptionRepository.GetScheduleExceptionById(id);
            if (scheduleException == null)
            {
                return null;
            }
            return new ScheduleExceptionResponse
            {
                Id = scheduleException.Id,
                BarberId = scheduleException.BarberId,
                Barber = scheduleException.Barber?.Name ?? string.Empty,
                ExceptionStartDate = scheduleException.ExceptionStartDate,
                ExceptionEndDate = scheduleException.ExceptionEndDate,
                ExceptionStartTime = scheduleException.ExceptionStartTime,
                ExceptionEndTime = scheduleException.ExceptionEndTime,
                ExceptionType = scheduleException.ExceptionType
            };
        }

        public bool CreateScheduleException(CreateScheduleExceptionRequest request)
        {
            var today = DateOnly.FromDateTime(DateTime.Now);

            // 1. Validar existencia del barbero
            if (_barberRepository.GetBarberById(request.BarberId) == null) return false;

            // 2. Validar que la fecha de inicio no sea en el pasado
            if (request.ExceptionStartDate < today) return false;

            // 3. Validar rango de fechas (Fin >= Inicio)
            if (request.ExceptionEndDate < request.ExceptionStartDate) return false;

            // 4. Validar rango de horas (Solo si es el mismo día)
            if (request.ExceptionStartDate == request.ExceptionEndDate)
            {
                if (request.ExceptionEndTime <= request.ExceptionStartTime) return false;
            }

            var entity = new ScheduleException
            {
                BarberId = request.BarberId,
                ExceptionStartDate = request.ExceptionStartDate,
                ExceptionEndDate = request.ExceptionEndDate,
                ExceptionStartTime = request.ExceptionStartTime,
                ExceptionEndTime = request.ExceptionEndTime,
                ExceptionType = request.ExceptionType
            };
            return _scheduleExceptionRepository.CreateScheduleException(entity);
        }

        public bool UpdateScheduleException(int id, UpdateScheduleExceptionRequest request)
        {
            var existingException = _scheduleExceptionRepository.GetScheduleExceptionById(id);
            if (existingException == null) return false;

            var today = DateOnly.FromDateTime(DateTime.Now);

            // Validaciones similares al Create
            if (_barberRepository.GetBarberById(request.BarberId) == null) return false;

            // Al editar, quizás permitas fechas pasadas si ya estaban registradas, 
            // pero lo ideal es que la nueva fecha de inicio sea hoy o posterior.
            if (request.ExceptionStartDate < today) return false;
            if (request.ExceptionEndDate < request.ExceptionStartDate) return false;

            if (request.ExceptionStartDate == request.ExceptionEndDate &&
                request.ExceptionEndTime <= request.ExceptionStartTime) return false;

            existingException.BarberId = request.BarberId;
            existingException.ExceptionStartDate = request.ExceptionStartDate;
            existingException.ExceptionEndDate = request.ExceptionEndDate;
            existingException.ExceptionStartTime = request.ExceptionStartTime;
            existingException.ExceptionEndTime = request.ExceptionEndTime;
            existingException.ExceptionType = request.ExceptionType;

            return _scheduleExceptionRepository.UpdateScheduleException(existingException);
        }

        public bool DeleteScheduleException(int id)
        {
            var existingException = _scheduleExceptionRepository.GetScheduleExceptionById(id);
            if (existingException == null)
            {
                return false;
            }
            return _scheduleExceptionRepository.DeleteScheduleException(id);
        }
    }
}
