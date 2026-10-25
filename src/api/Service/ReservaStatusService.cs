using Microsoft.EntityFrameworkCore;
using stage1.Data;
using stage1.Domain.Entities;
using stage1.Domain.Enum;

namespace stage1.Service;


public class ReservaStatusService(IServiceScopeFactory scopeFactory) : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory = scopeFactory;




    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var timer = new PeriodicTimer(TimeSpan.FromSeconds(30));

        while( await timer.WaitForNextTickAsync(stoppingToken))
        {
            using var scope = _scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var agora = DateTimeOffset.UtcNow;
            
            var pendente = (int)ReservaStatus.Pendente;
            var cancelada = (int)ReservaStatus.Cancelada;

            await context.Database.ExecuteSqlInterpolatedAsync(
                $"UPDATE Reservas SET Status = {cancelada} WHERE Status = {pendente} AND ExpiraEm <= '{agora:O}'", stoppingToken
            );
        }
        
        
        
        throw new NotImplementedException();
    }
}