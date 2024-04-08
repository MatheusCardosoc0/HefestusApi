using HefestusApi.DTOs.Produtos;
using HefestusApi.Models.Pessoal;

namespace HefestusApi.DTOs.Pessoal
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

    public record struct PersonRequestDataDto(
        int Id,
        string Name,
        string Email,
        string Phone,
        int Age,
        string CPF,
        string Address,
        DateTime? BirthDate,
        string? IBGE,
        string? Razao,
        string? InscricaoEstadual,
        string CEP,
        string? UrlImage,
        bool? IsBlocked,
        string? MaritalStatus,
        string? Habilities,
        string? Description,
        List<PersonGroupSimpleSearchDataDto> PersonGroup,
        CitySimpleSearchDataDto City,
        string? Gender,
        bool? ICMSContributor,
        string PersonType
        );

    public record struct PersonSimpleSearchDataDto(
        int Id,
        string Name
        );
};

