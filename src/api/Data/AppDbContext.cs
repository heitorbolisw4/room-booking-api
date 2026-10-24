using Microsoft.EntityFrameworkCore;
using stage1.Domain.Entities;

namespace stage1.Data;


public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{

    public DbSet<Reserva> Reservas => Set<Reserva>();
    public DbSet<Sala> Salas => Set<Sala>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}