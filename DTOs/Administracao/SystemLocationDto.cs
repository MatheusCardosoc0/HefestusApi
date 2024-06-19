using HefestusApi.DTOs.Pessoal;

namespace HefestusApi.DTOs.Administracao
{
    public record struct SystemLocationDto
    (
        string Id,
        string Description,
        string Name
    );

    public record struct SystemLocationSimpleSearchDataDto(
        string Id,
        string Name
    );

    public record struct SystemLocationRequestDataDto(
        string Name,
        string? Description,
        string? Password
    );
}
