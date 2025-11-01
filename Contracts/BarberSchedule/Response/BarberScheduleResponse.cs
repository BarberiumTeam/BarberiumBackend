using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.BarberSchedule.Response
{
    public class BarberScheduleResponse
    {
        public int Id { get; set; }
        public int BarberId { get; set; }
        public string Barber { get; set; } = string.Empty;
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public WeekDay WeekDay { get; set; }
    }
}
