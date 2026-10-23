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
    public void CriarDuasReservas_MesmaSalaEHorario_LancaDomainException()
    {
        var inicio1 = DateTimeOffset.Now.AddHours(3);
        var inicio2 = DateTimeOffset.Now.AddHours(3);
        var fim1 = inicio1.AddHours(2);
        var fim2 = inicio2.AddHours(2);
        Assert.Throws<DomainException>(() => Reserva.CriarReserva(1, inicio1, fim1));
        Assert.Throws<DomainException>(() => Reserva.CriarReserva(2, inicio2, fim2));
    }
}
