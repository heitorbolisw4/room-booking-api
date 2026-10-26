using Microsoft.EntityFrameworkCore;
using stage1.Data;
using stage1.Domain.Entities;
using stage1.Domain.Exceptions;
using static stage1.Dto.ReservaContracts;

namespace stage1.Endpoints;


public static  class ReservaEndpoints
{
    public static IEndpointRouteBuilder MapReservasEndpoints( this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/reservas");
        group.MapPost("", Create);
        group.MapGet("", List);
        group.MapPost("/cancelar/{id:int}", Cancel);


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
    
    private static async Task<IResult> List(AppDbContext db)
    {
        var reservas = await db.Reservas.Select( r => ToResponse(r)).ToListAsync();
        
        return Results.Ok(reservas);
    }

    private static async Task<IResult> Cancel(int id, AppDbContext db)
    {
        // eu verifico de acordo com Id
        var reserva = await db.Reservas.FirstOrDefaultAsync( r => r.Id == id );
        if ( reserva is null )
        {
            return Results.NotFound("Nenhuma reserva localizada");
        }

        try
        {
            reserva.Cancelar(DateTimeOffset.UtcNow);
            
        }catch( DomainException ex)
        {
            return Results.Conflict(ex.Message);
        }

        await db.SaveChangesAsync();
        return Results.NoContent();

    }

    private static  ReservaResponse ToResponse( Reserva reserva)
    {
        return new ReservaResponse(reserva.SalaId, reserva.Inicio, reserva.Fim);
    }







}