using HefestusApi.DTOs.Pessoal;

namespace HefestusApi.DTOs.Administracao
{
    public record struct SystemLocationDto
    (
        int Id,
        string Description,
        int PersonId,
        string LocationName,
        PersonDto Person 
    );

    public record struct SystemLocationSimpleSearchDataDto(
        int Id,
        string LocationName
    );

    public record struct SystemLocationRequestDataDto(
        string PersonName,
        string Description,
        int PersonId
    );
}
