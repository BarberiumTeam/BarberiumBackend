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
                ExceptionDate = scheduleExceptions.ExceptionDate,
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
                ExceptionDate = scheduleException.ExceptionDate,
                ExceptionStartTime = scheduleException.ExceptionStartTime,
                ExceptionEndTime = scheduleException.ExceptionEndTime,
                ExceptionType = scheduleException.ExceptionType
            };
        }

        public bool CreateScheduleException(CreateScheduleExceptionRequest request)
        {
            if (request.ExceptionEndTime <= request.ExceptionStartTime) return false;
            var entity = new ScheduleException
            {
                BarberId = request.BarberId,
                ExceptionDate = request.ExceptionDate,
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
            existingException.ExceptionDate = request.ExceptionDate;
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
