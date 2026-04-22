using Application.Abstraction;
using Contracts.BarberSchedule.Request;
using Contracts.BarberSchedule.Response;
using Domain.Entity;

namespace Application.Service
{
    public class BarberScheduleService : IBarberScheduleService
    {
        private readonly IBarberScheduleRepository _barberScheduleRepository;
        private readonly IBarberRepository _barberRepository;
        public BarberScheduleService(
            IBarberScheduleRepository barberScheduleRepository,
            IBarberRepository barberRepository)
        {
            _barberScheduleRepository = barberScheduleRepository;
            _barberRepository = barberRepository;
        }

        public List<BarberScheduleResponse> GetAllBarberSchedules()
        {
            var BarberSchedule = _barberScheduleRepository.GetAllBarberSchedules();

            return BarberSchedule.Select(BarberSchedule => new BarberScheduleResponse
            {
                Id = BarberSchedule.Id,
                BarberId = BarberSchedule.BarberId,
                Barber = BarberSchedule.Barber?.Name ?? string.Empty,
                StartTime = BarberSchedule.StartTime,
                EndTime = BarberSchedule.EndTime,
                WeekDay = BarberSchedule.WeekDay,
            }).ToList();
        }

        public BarberScheduleResponse? GetBarberScheduleById(int Id)
        {
            var schedule = _barberScheduleRepository.GetBarberScheduleById(Id);

            if (schedule == null) return null;

            return new BarberScheduleResponse
            {
                Id = schedule.Id,
                BarberId = schedule.BarberId,
                Barber = schedule.Barber?.Name ?? string.Empty,
                StartTime = schedule.StartTime,
                EndTime = schedule.EndTime,
                WeekDay = schedule.WeekDay
            };
        }

        public bool CreateBarberSchedule(CreateBarberScheduleRequest request)
        {
            // VALIDACIÓN 1: Que el tiempo sea lógico
            if (request.EndTime <= request.StartTime) return false;

            // VALIDACIÓN 2: ¿Existe el barbero?
            var barber = _barberRepository.GetBarberById(request.BarberId);
            if (barber == null) return false; // Si no existe, no creamos nada

            var entity = new BarberSchedule
            {
                BarberId = request.BarberId,
                StartTime = request.StartTime,
                EndTime = request.EndTime,
                WeekDay = request.WeekDay
            };
            return _barberScheduleRepository.CreateBarberSchedule(entity);
        }

        public bool UpdateBarberSchedule(int id, UpdateBarberScheduleRequest request)
        {
            // Buscamos si el cronograma existe
            var BarberScheduleToUpdate = _barberScheduleRepository.GetBarberScheduleById(id);
            if (BarberScheduleToUpdate == null) return false;

            // VALIDACIÓN 1: Que el tiempo sea lógico
            if (request.EndTime <= request.StartTime) return false;

            // VALIDACIÓN 2: ¿Existe el barbero que nos pasan en el request?
            var barber = _barberRepository.GetBarberById(request.BarberId);
            if (barber == null) return false;

            // Si todo está ok, actualizamos
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