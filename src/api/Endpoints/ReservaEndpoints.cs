using Microsoft.EntityFrameworkCore;
using stage1.Data;
using stage1.Domain.Entities;
using static stage1.Dto.ReservaContracts;

namespace stage1.Endpoints;


public static  class ReservaEndpoints
{
    public static IEndpointRouteBuilder MapReservasEndpoints( this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/reservas");
        group.MapPost("", Create);


        return app;
    }
    



    private static async Task<IResult> Create(AppDbContext db, CriarReservaRequest request)
    {
        
        
        bool reservaExiste = await db.Reservas.AnyAsync(x => x.SalaId == request.SalaId && request.Inicio < x.Fim && request.Fim > x.Inicio);
        if (reservaExiste)
        {
            return Results.Conflict("Há um horario reservado nesta sala");
        }

        
        var reserva = Reserva.CriarReserva(request.SalaId, request.Inicio, request.Fim);

        db.Add(reserva);
        await db.SaveChangesAsync();
        return Results.Created($"Sua reserva { reserva.Id } foi realizada com sucesso ", reserva.Id);

    }







}