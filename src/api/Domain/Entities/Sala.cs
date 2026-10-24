using stage1.Domain.Exceptions;

namespace stage1.Domain.Entities;


public class Sala
{
    private Sala(int numero)
    {
        Numero = numero;
    }

    public int Id { get; private set; }
    public int Numero { get; private set; }
    public bool IsActive { get; private set; } = true;
    public ICollection<Reserva>? Reservas {get; set;}

    public static Sala CriarSala( int numero )
    {
        ValidNumero( numero );
        return new Sala(numero);
    }
    
    
    
    // eu tenho um metodo pra validar se o numero é valido
    private static void ValidNumero( int numero )
    {
        if( numero <= 0)
        {
            throw new DomainException("Deve informar um numero positivo maior que zero" );
        }
    }


    
}