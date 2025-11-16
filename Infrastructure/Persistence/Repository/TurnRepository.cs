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

        // --- NUEVAS IMPLEMENTACIONES DE VERIFICACIÓN SÍNCRONA ---

        public bool ExistsClient(int clientId)
        {
            // Usamos Any() para verificar la existencia sin traer la entidad completa.
            return _context.Clients.Any(c => c.Id == clientId);
        }

        public bool ExistsBarber(int barberId)
        {
            // Usamos Any() para verificar la existencia sin traer la entidad completa.
            return _context.Barbers.Any(b => b.Id == barberId);
        }

        // Implementación para la validación en la CREACIÓN

        public bool IsTimeSlotBooked(int barberId, DateOnly date, TimeOnly startTime, TimeOnly endTime)
        {
            // Lógica para la superposición de turnos.

            // 1. Filtrar por Barbero y Fecha
            // 2. Filtrar por aquellos turnos que están:
            //    a) Empezando antes de que termine el nuevo Y
            //    b) Terminando después de que empiece el nuevo

            return _context.Turns.Any(t =>
                t.BarberId == barberId &&
                t.TurnDate == date &&

                // Criterio de superposición 
                t.TurnStartTime < endTime &&
                t.TurnEndTime > startTime
            );
        }

        // Implementación para la validación en la ACTUALIZACIÓN
        public bool IsTimeSlotBooked2(int barberId, DateOnly date, TimeOnly startTime, TimeOnly endTime, int turnIdToExclude)
        {
            // Utilizamos el mismo criterio de superposición, pero añadimos la exclusión.
            return _context.Turns.Any(t =>
                t.BarberId == barberId &&
                t.TurnDate == date &&

                // Criterio de superposición: [A empieza antes de B termina] Y [A termina después de B empieza]
                t.TurnStartTime < endTime &&
                t.TurnEndTime > startTime &&

                // CRITERIO DE EXCLUSIÓN: Ignoramos el turno con el ID que estamos actualizando.
                t.Id != turnIdToExclude
            );
        }

        // --- NUEVAS IMPLEMENTACIONES DE HORARIOS Y EXCEPCIONES ---

        public List<BarberSchedule> GetBarberSchedules(int barberId)
        {
            // Devuelve todos los horarios semanales definidos para ese barbero.
            // Usamos ToList() para ejecutar la consulta de forma síncrona.
            return _context.BarbersSchedules
                .Where(s => s.BarberId == barberId)
                .ToList();
        }

        public List<ScheduleException> GetScheduleExceptionsByDate(int barberId, DateOnly date)
        {
            // Devuelve las excepciones donde la fecha del turno cae dentro del rango de la excepción.
            // Usamos ToList() para ejecutar la consulta de forma síncrona.
            return _context.ScheduleExceptions.Where(e =>
                e.BarberId == barberId &&
                date >= e.ExceptionStartDate &&
                date <= e.ExceptionEndDate
            ).ToList();
        }

    }

}
