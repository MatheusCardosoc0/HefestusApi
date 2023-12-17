namespace HefestusApi.DTOs.Administracao
{
public record struct PersonDto(
        string Name,
        string Email,
        string Phone,
        int Age,
        string CPF,
        string Address,
        string BirthDate,
        string IBGE,
        string Razao,
        string InscricaoEstadual,
        string CEP,
        string UrlImage,
        bool? IsBlocked,
        string MaritalStatus,
        string Habilities,
        string Description,
        List<PersonGroupDto> PersonGroup,
        int CityId,
        CityDto City
        );
};