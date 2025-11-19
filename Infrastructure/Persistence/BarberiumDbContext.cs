using Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class BarberiumDbContext : DbContext
{
    public DbSet<Client> Clients { get; set; }
    public DbSet<Barber> Barbers { get; set; }
    public DbSet<BarberSchedule> BarbersSchedules { get; set; }
    public DbSet<ScheduleException> ScheduleExceptions { get; set; }
    public DbSet<Turn> Turns { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public BarberiumDbContext(DbContextOptions<BarberiumDbContext> options) : base(options)   
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Esta configuración es parte de la Infraestructura (mapeo SQL Server)
        modelBuilder.Entity<Payment>()
            .Property(p => p.Amount)
            .HasPrecision(18, 4); // Especifica decimal(18, 4) para SQL Server
    }

}
