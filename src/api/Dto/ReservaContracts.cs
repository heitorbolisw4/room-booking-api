using System.ComponentModel.DataAnnotations;

namespace stage1.Dto;


public class ReservaContracts
{
    public record CriarReservaRequest(
    [ property: Range( 1, int.MaxValue, ErrorMessage = "Deve informar uma Sala Valida" ) ] int SalaId,
    [ property: Required ] DateTimeOffset Inicio,
    [ property: Required ] DateTimeOffset Fim );

    public record ReservaResponse(int SalaId, DateTimeOffset Inicio, DateTimeOffset Fim);
}