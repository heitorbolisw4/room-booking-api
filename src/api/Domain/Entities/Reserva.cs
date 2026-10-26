using stage1.Domain.Enum;
using stage1.Domain.Exceptions;

namespace stage1.Domain.Entities;


public class Reserva
{
    private Reserva(int salaId, DateTimeOffset inicio, DateTimeOffset fim)
    {
        SalaId = salaId;
        Inicio = inicio;
        Fim = fim;
    }

    public int Id { get; private set; }
    public int SalaId { get; private set; }
    public DateTimeOffset Inicio { get; private set; }
    public DateTimeOffset Fim { get; private set; }
    public ReservaStatus Status {get; set;} = ReservaStatus.Pendente;
    public Sala? Sala {get; set;}
    public DateTimeOffset ExpiraEm {get; private set;} = DateTimeOffset.UtcNow.AddMinutes(2);

    public static Reserva CriarReserva(int salaId, DateTimeOffset inicio, DateTimeOffset fim)
    {
        ValidSalaId(salaId);
        ValidInicioFim(inicio, fim);
        return new Reserva(salaId, inicio, fim);
    }


    private static void ValidSalaId(int salaId)
    {
        if( salaId <= 0)
        {
            throw new DomainException("O Id da Sala deve ser um INT POSITIVO maior que ZERO!");
        }
    }
    private static void ValidInicioFim(DateTimeOffset inicio, DateTimeOffset fim)
    {
        var now = DateTimeOffset.Now;
        if( inicio <= now || inicio >= fim)
        {
            throw new DomainException("A data INICIO deve ser no FUTURO e MENOR que a data FIM");
        }
        if( fim <= now)
        {
            throw new DomainException("A data FIM deve ser no FUTURO");
        }
    }

    public void Cancelar( DateTimeOffset agora)
    {
        if( Inicio <= agora.AddHours(1))
        {
            throw new DomainException("O Cancelamento deve ser feito com 1 hora de antecedencia");
        }
        Status = ReservaStatus.Cancelada;
    }
    public bool SobrepoeCom(Reserva outra)
        => SalaId == outra.SalaId && outra.Inicio < Fim && outra.Fim > Inicio;
    
    
}