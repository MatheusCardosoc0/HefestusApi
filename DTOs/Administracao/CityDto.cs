namespace HefestusApi.DTOs.Administracao
{
    public record struct CityDto(
        string Name,
        int IBGENumber,
        string State
        );
}
