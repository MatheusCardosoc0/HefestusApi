namespace HefestusApi.DTOs.Pessoal
{
    public record struct CityDto(
        int Id,
        string Name,
        int IBGENumber,
        string State
    );

    public record struct CityRequestDataDto(
        string Name,
        int IBGENumber,
        string State
    );

    public record struct CitySimpleSearchDataDto(
        int Id,
        string Name
    );
}
