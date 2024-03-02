namespace HefestusApi.DTOs.Administracao
{
    public record struct CityDto(
        int Id,
        string Name,
        int IBGENumber,
        string State
    );

    public record struct CitySearchTermDto(
        int Id,
        string Name
    );

    public record struct CityPostOrPutDto(
        string Name,
        int IBGENumber,
        string State
    );
}
