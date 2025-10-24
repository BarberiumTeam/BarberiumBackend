using Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class BarberiumDbContext : DbContext
{
    public DbSet<Client> Clients { get; set; }
    public BarberiumDbContext(DbContextOptions<BarberiumDbContext> options) : base(options)   
    {
        
    }
}
