using HefestusApi.DTOs.Produtos;

namespace HefestusApi.DTOs.Administracao
{
public record struct PersonDto(
        int Id,
        string Name,
        string Email,
        string Phone,
        int Age,
        string CPF,
        string Address,
        string? BirthDate,
        string? IBGE,
        string? Razao,
        string? InscricaoEstadual,
        string CEP,
        string? UrlImage,
        bool? IsBlocked,
        string? MaritalStatus,
        string? Habilities,
        string? Description,
        int CityId,
        string CityName,
        string? State,
        string? MainPersonGroup,
        string? Gender,
        bool? ICMSContributor ,
        string PersonType
        );

    public record struct PersonPostOrPutDto(
        int Id,
        string Name,
        string Email,
        string Phone,
        int Age,
        string CPF,
        string Address,
        string? BirthDate,
        string? IBGE,
        string? Razao,
        string? InscricaoEstadual,
        string CEP,
        string? UrlImage,
        bool? IsBlocked,
        string? MaritalStatus,
        string? Habilities,
        string? Description,
        List<PersonGroupSearchTermDto> PersonGroup,
        CitySearchTermDto City,
        string? Gender,
        bool? ICMSContributor,
        string PersonType
        );

    public record struct PersonSearchTermDto(
        int Id,
        string Name
        );
};

