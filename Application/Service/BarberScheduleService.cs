using Application.Abstraction;
using Contracts.BarberSchedule.Request;
using Contracts.BarberSchedule.Response;
using Domain.Entity;

namespace Application.Service
{
    public class BarberScheduleService : IBarberScheduleService
    {
         private readonly IBarberScheduleRepository _barberScheduleRepository;
         
         public BarberScheduleService(IBarberScheduleRepository barberScheduleRepository)
        {
            _barberScheduleRepository = barberScheduleRepository;
        }

        public List<BarberScheduleResponse> GetAllBarberSchedules()
        {
            var BarberSchedule = _barberScheduleRepository.GetAllBarberSchedules();

            return BarberSchedule.Select(BarberSchedule => new BarberScheduleResponse
            {

                Id = BarberSchedule.Id,
                BarberId = BarberSchedule.BarberId,
                Barber = BarberSchedule.Barber.Name ?? string.Empty,
                StartTime = BarberSchedule.StartTime,
                EndTime = BarberSchedule.EndTime,
                WeekDay = BarberSchedule.WeekDay,

            }).ToList();
        }

        public BarberScheduleResponse? GetBarberScheduleById(int Id)
        {
            var schedule = _barberScheduleRepository.GetBarberScheduleById(Id);

            if (schedule == null)
            {
                return null;
            }

            return new BarberScheduleResponse
            {
                Id = schedule.Id,
                BarberId = schedule.BarberId,
                Barber = schedule.Barber.Name ?? string.Empty,
                StartTime = schedule.StartTime,
                EndTime = schedule.EndTime,
                WeekDay = schedule.WeekDay
            };

        }

        public bool CreateBarberSchedule(CreateBarberScheduleRequest request)
        {
            if (request.EndTime <= request.StartTime) return false;

            var entity = new BarberSchedule
            {
                BarberId = request.BarberId,
                StartTime = request.StartTime,
                EndTime = request.EndTime,
                WeekDay = request.WeekDay

                //Aca se hashea la contraseña.
            };
            return _barberScheduleRepository.CreateBarberSchedule(entity);

        }

        public bool UpdateBarberSchedule(int id, UpdateBarberScheduleRequest request)
        {
            var BarberScheduleToUpdate = _barberScheduleRepository.GetBarberScheduleById(id);

            if (BarberScheduleToUpdate == null)
            {
                return false;
            }

            if (request.EndTime <= request.StartTime) return false;

            BarberScheduleToUpdate.StartTime = request.StartTime;
            BarberScheduleToUpdate.EndTime = request.EndTime;
            BarberScheduleToUpdate.WeekDay = request.WeekDay;
            BarberScheduleToUpdate.BarberId = request.BarberId;

            return _barberScheduleRepository.UpdateBarberSchedule(BarberScheduleToUpdate);
        }

        public bool DeleteBarberSchedule(int id)
        {
            return _barberScheduleRepository.DeleteBarberSchedule(id);
        }


    }
}
