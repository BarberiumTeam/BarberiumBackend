using Application.Abstraction;
using Domain.Entity;

namespace Infrastructure.Persistence.Repository
{
    public class ScheduleExceptionRepository : IScheduleExceptionRepository
    {
        private readonly BarberiumDbContext _context;

        public ScheduleExceptionRepository(BarberiumDbContext context)
        {
            _context = context;
        }

        public List<ScheduleException> GetAllScheduleExceptions()
        {
            return _context.ScheduleExceptions.ToList();
        }
        public ScheduleException? GetScheduleExceptionById(int id)
        {
            return _context.ScheduleExceptions.FirstOrDefault(se => se.Id == id);
        }
        public bool CreateScheduleException(ScheduleException scheduleException)
        {
            _context.ScheduleExceptions.Add(scheduleException);
            return _context.SaveChanges() > 0;
        }
        public bool UpdateScheduleException(ScheduleException scheduleException)
        {
            _context.ScheduleExceptions.Update(scheduleException);
            return _context.SaveChanges() > 0;
        }
        public bool DeleteScheduleException(int id)
        {
            var scheduleExceptionToDelete = _context.ScheduleExceptions.FirstOrDefault(se => se.Id == id);
            if (scheduleExceptionToDelete == null)
            {
                return false;
            }
            _context.ScheduleExceptions.Remove(scheduleExceptionToDelete);
            return _context.SaveChanges() > 0;
        }
    }
}
