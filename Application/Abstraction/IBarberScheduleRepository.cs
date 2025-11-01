using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstraction
{
    public interface IBarberScheduleRepository
    {
        BarberSchedule? GetBarberScheduleById(int barberId);
        List<BarberSchedule> GetAllBarberSchedules();
        bool CreateBarberSchedule(BarberSchedule barberSchedule);
        bool UpdateBarberSchedule(BarberSchedule barberSchedule);
        bool DeleteBarberSchedule(int id);
    }
}
