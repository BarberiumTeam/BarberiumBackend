using Application.Abstraction;
using Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repository
{
    public class TurnRepository : ITurnRepository
    {
        private readonly BarberiumDbContext _context;

        public TurnRepository(BarberiumDbContext context)
        {
            _context = context;
        }

        private IQueryable<Turn> GetTurnsWithRelations() =>
            _context.Turns
                .Include(t => t.Barber)
                .Include(t => t.Client);

        public List<Turn> GetAllTurns()
        {
            return GetTurnsWithRelations().ToList();
        }

        public Turn? GetTurnById(int id)
        {
            return GetTurnsWithRelations().FirstOrDefault(i => i.Id == id);
        }

        public bool CreateTurn(Turn turn)
        {
            _context.Turns.Add(turn);
            return _context.SaveChanges() > 0;
        }

        public bool UpdateTurn(Turn turn)
        {
            _context.Turns.Update(turn);
            return _context.SaveChanges() > 0;
        }
        public bool UpdateTurnServiceType(int id, ServiceType newServiceType)
        {
            var turn = _context.Turns.FirstOrDefault(i => i.Id == id);
            if (turn == null) return false;
            turn.Service = newServiceType;
            return _context.SaveChanges() > 0;
        }

        public bool UpdateTurnState(int id, State newState)
        {
            var turn = _context.Turns.FirstOrDefault(i => i.Id == id);
            if (turn == null) return false;
            turn.State = newState;
            return _context.SaveChanges() > 0;
        }

        public bool DeleteTurn(int id)
        {
            var turnToDelete = _context.Turns.FirstOrDefault(i => i.Id == id);
            if (turnToDelete == null) return false;
            _context.Turns.Remove(turnToDelete);
            return _context.SaveChanges() > 0;
        }

    }

}
