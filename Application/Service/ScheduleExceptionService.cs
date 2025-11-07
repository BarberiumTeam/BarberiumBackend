using Application.Abstraction;
using Contracts.ScheduleException.Request;
using Contracts.ScheduleException.Response;
using Domain.Entity;

namespace Application.Service
{
    public class ScheduleExceptionService : IScheduleExceptionService
    {
        private readonly IScheduleExceptionRepository _scheduleExceptionRepository;

        public ScheduleExceptionService(IScheduleExceptionRepository scheduleExceptionRepository)
        {
            _scheduleExceptionRepository = scheduleExceptionRepository;
        }

        public List<ScheduleExceptionResponse> GetAllScheduleExceptions() 
        {
            var scheduleExceptions = _scheduleExceptionRepository.GetAllScheduleExceptions();
            return scheduleExceptions.Select(scheduleExceptions => new ScheduleExceptionResponse
            {
                Id = scheduleExceptions.Id,
                BarberId = scheduleExceptions.BarberId,
                Barber = scheduleExceptions.Barber.Name ?? string.Empty,
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
                Barber = scheduleException.Barber.Name ?? string.Empty,
                ExceptionStartDate = scheduleException.ExceptionStartDate,
                ExceptionEndDate = scheduleException.ExceptionEndDate,
                ExceptionStartTime = scheduleException.ExceptionStartTime,
                ExceptionEndTime = scheduleException.ExceptionEndTime,
                ExceptionType = scheduleException.ExceptionType
            };
        }

        public bool CreateScheduleException(CreateScheduleExceptionRequest request)
        {
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
            if (existingException == null)
            {
                return false;
            }
            if (request.ExceptionEndTime <= request.ExceptionStartTime) return false;
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
