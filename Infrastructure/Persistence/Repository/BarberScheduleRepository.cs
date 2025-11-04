using Application.Abstraction;
using Domain.Entity;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Persistence.Repository
{
    public class BarberScheduleRepository : IBarberScheduleRepository
    {
        private readonly BarberiumDbContext _context;

        public BarberScheduleRepository(BarberiumDbContext context)
        {
            _context = context;
        }

        public List<BarberSchedule> GetAllBarberSchedules()
        {
            return _context.BarbersSchedules.Include(s => s.Barber).ToList();   
        }

        public BarberSchedule? GetBarberScheduleById(int Id)
        {
            return _context.BarbersSchedules.Include(s => s.Barber).FirstOrDefault(i => i.Id == Id);
        }

        public bool CreateBarberSchedule(BarberSchedule barberSchedule)
        {
            _context.BarbersSchedules.Add(barberSchedule);
            return _context.SaveChanges() > 0;

        }
        public bool UpdateBarberSchedule(BarberSchedule barberSchedule)
        {
            _context.BarbersSchedules.Update(barberSchedule);
            return _context.SaveChanges() > 0;

        }

        public bool DeleteBarberSchedule(int id)
        {
            var deleteBarberSchedule = _context.BarbersSchedules.FirstOrDefault(i => i.Id == id);

            if (deleteBarberSchedule ==  null)
            {
                return false;
            }
            
            _context.BarbersSchedules.Remove(deleteBarberSchedule);

            return _context.SaveChanges() > 0;

        }

    }
}
