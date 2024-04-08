using AutoMapper;
using HefestusApi.DTOs.Pessoal;
using HefestusApi.DTOs.Financeiro;
using HefestusApi.DTOs.Produtos;
using HefestusApi.DTOs.Vendas;
using HefestusApi.Models.Pessoal;
using HefestusApi.Models.Financeiro;
using HefestusApi.Models.Produtos;
using HefestusApi.Models.Vendas;

namespace HefestusApi.DTOs.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<Person, UserPersonViewDto>();

            CreateMap<Order, OrderDto>()
                .ForMember(dest => dest.ClientName, opt => opt.MapFrom(src => src.Client.Name))
                .ForMember(dest => dest.ResponsibleName, opt => opt.MapFrom(src => src.Responsible.Name))
                .ForMember(dest => dest.PaymentConditionName, opt => opt.MapFrom(src => src.PaymentCondition.Name))
                .ForMember(dest => dest.PaymentOptionName, opt => opt.MapFrom(src => src.PaymentOption.Name));
            CreateMap<OrderProduct, OrderProductDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.Batch, opt => opt.MapFrom(src => src.Product.Batch));
            CreateMap<OrderInstallment, OrderInstallmentDto>()
                .ForMember(dest => dest.ClientName, opt => opt.MapFrom(src => src.Order.Client.Name))
                .ForMember(dest => dest.DateOfCompletion, opt => opt.MapFrom(src => src.Order.DateOfCompletion));
            CreateMap<PaymentCondition, PaymentConditionDto>();
            CreateMap<PaymentOptions, PaymentOptionDto>();

            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.GroupName, opt => opt.MapFrom(src => src.Group.Name))
                .ForMember(dest => dest.FamilyName, opt => opt.MapFrom(src => src.Family.Name))
                .ForMember(dest => dest.SubGroupName, opt => opt.MapFrom(src => src.Subgroup.Name));
            CreateMap<Product, ProductRequestDataDto>()
                .ForMember(dest => dest.Group, opt => opt.MapFrom(src => new ProductGroupSimpleSearchDataDto { Id = src.Group.Id, Name = src.Group.Name }))
                .ForMember(dest => dest.SubGroup, opt => opt.MapFrom(src => new ProductSubGroupSimpleSearchDataDto { Id = src.Subgroup.Id, Name = src.Subgroup.Name }))
                .ForMember(dest => dest.Family, opt => opt.MapFrom(src => new ProductFamilySimpleSearchDataDto { Id = src.Family.Id, Name = src.Family.Name }));
            CreateMap<ProductSubGroup, ProductSubGroupDto>();
            CreateMap<ProductSubGroup, ProductSubGroupSimpleSearchDataDto>();
            CreateMap<ProductGroup, ProductGroupDto>();
            CreateMap<ProductGroup, ProductGroupSimpleSearchDataDto>();
            CreateMap<ProductFamily, ProductFamilyDto>();
            CreateMap<ProductFamily, ProductFamilySimpleSearchDataDto>();

            CreateMap<Person, PersonDto>()
                 .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.City.State))
                 .ForMember(dest => dest.MainPersonGroup, opt => opt.MapFrom(src => src.PersonGroup[0].Name))
                 .ForMember(dest => dest.CityName, opt => opt.MapFrom(src => src.City.Name));
            CreateMap<Person, PersonRequestDataDto>()
                .ForMember(dest => dest.PersonGroup, opt => opt.MapFrom(src => src.PersonGroup.Select(pg => new PersonGroupSimpleSearchDataDto { Id = pg.Id, Name = pg.Name })))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => new CitySimpleSearchDataDto { Id = src.City.Id, Name = src.City.Name }));
            CreateMap<PersonGroup, PersonGroupSimpleSearchDataDto>();

            CreateMap<City, CitySimpleSearchDataDto>();

            CreateMap<PersonGroup, PersonGroupDto>();
            CreateMap<City, CityDto>();
        }
    }
}
