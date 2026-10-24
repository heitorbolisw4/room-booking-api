using stage1.Domain.Entities;
using stage1.Domain.Exceptions;




namespace stage1.Tests.Domain;


public class ReservaTests
{
    [Fact]
    public void CriarReserva_ComSalaIdZero_LancaDomainException()
    {
        var inicio = DateTimeOffset.Now.AddDays(1);
        var fim = inicio.AddHours(2);
        Assert.Throws<DomainException>(() => Reserva.CriarReserva(0, inicio, fim));
    }

    [Fact]
    public void CriarReserva_ComSalaIdNegativo_LancaDomainException()
    {
        var inicio = DateTimeOffset.Now.AddDays(1);
        var fim = inicio.AddHours(2);
        Assert.Throws<DomainException>(() => Reserva.CriarReserva(-1, inicio, fim));
    }

    [Fact]
    public void CriarReserva_FimMenorQueInicio_LancaDomainException()
    {
        var inicio = DateTimeOffset.Now.AddDays(1);
        var fim = inicio.AddDays(-3);
        Assert.Throws<DomainException>(() => Reserva.CriarReserva(1, inicio, fim));
    }
    [Fact]
    public void CriarReserva_DadosValidos_RetornaReserva()
    {
        var inicio = DateTimeOffset.Now.AddDays(1);
        var fim = inicio.AddHours(2);

        var reserva = Reserva.CriarReserva(1, inicio, fim);

        Assert.Equal(1, reserva.SalaId);
        Assert.Equal(inicio, reserva.Inicio);
        Assert.Equal(fim, reserva.Fim);
    
    }
    [Fact]
    public void CriarReserva_InicioNoPassado_LancaDomainException()
    {
        var inicio = DateTimeOffset.Now.AddHours(-1);
        var fim = inicio.AddHours(2);
        Assert.Throws<DomainException>(() => Reserva.CriarReserva(1, inicio, fim));

    }

    [Fact]
    public void SobrepoeCom_HorarioParcialMesmaSala_RetornaTrue()
    {
        var base14h = DateTimeOffset.UtcNow.Date.AddDays(1).AddHours(14);
        var a = Reserva.CriarReserva(1, base14h ,base14h.AddHours(2));
        var b = Reserva.CriarReserva(1, base14h.AddHours(1) ,base14h.AddHours(3));

        Assert.True(a.SobrepoeCom(b));
    }

    [Fact]
    public void SobrepoeCom_HorariosEncostados_RetornaFalse()
    {
        var base14h = DateTimeOffset.UtcNow.Date.AddDays(1).AddHours(14);
        var a = Reserva.CriarReserva(1, base14h.AddHours(1) ,base14h.AddHours(3));//inicio 15h, fim 17h
        var b = Reserva.CriarReserva(1, base14h.AddHours(3) ,base14h.AddHours(4));//inicio 17h, fim 18h

        Assert.False(a.SobrepoeCom(b));
    }
    [Fact]
    public void SobrepoeCom_SalasDiferentes_RetornaFalse()
    {
        var base14h = DateTimeOffset.UtcNow.Date.AddDays(1).AddHours(14);
        var a = Reserva.CriarReserva(1, base14h.AddHours(1) ,base14h.AddHours(3));//inicio 15h, fim 17h
        var b = Reserva.CriarReserva(2, base14h.AddHours(2) ,base14h.AddHours(4));//inicio 16h, fim 18h

        Assert.False(a.SobrepoeCom(b));
    }
}
