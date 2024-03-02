using AutoMapper;
using HefestusApi.DTOs.Administracao;
using HefestusApi.DTOs.Financeiro;
using HefestusApi.DTOs.Produtos;
using HefestusApi.DTOs.Vendas;
using HefestusApi.Models.Administracao;
using HefestusApi.Models.Financeiro;
using HefestusApi.Models.Produtos;
using HefestusApi.Models.Vendas;

namespace HefestusApi.Data
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserViewDto>();
            CreateMap<Person, UserPersonViewDto>();

            CreateMap<Order, OrderDto>()
                .ForMember(dest => dest.ClientName, opt => opt.MapFrom(src => src.Client.Name))
                .ForMember(dest => dest.ResponsibleName, opt => opt.MapFrom(src => src.Responsible.Name))
                .ForMember(dest => dest.PaymentConditionName, opt => opt.MapFrom(src => src.PaymentCondition.Name))
                .ForMember(dest => dest.PaymentOptionName, opt => opt.MapFrom(src => src.PaymentOption.Name));
            CreateMap<OrderProduct, OrderProductDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.Batch, opt => opt.MapFrom(src => src.Product.Batch));
            CreateMap<PaymentCondition, PaymentConditionDto>();
            CreateMap<PaymentOptions, PaymentOptionsDto>();

            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.GroupName, opt => opt.MapFrom(src => src.Group.Name))
                .ForMember(dest => dest.FamilyName, opt => opt.MapFrom(src => src.Family.Name))
                .ForMember(dest => dest.SubGroupName, opt => opt.MapFrom(src => src.Subgroup.Name));
            CreateMap<ProductSubGroup, ProductSubGroupDto>();
            CreateMap<ProductGroup, ProductGroupDto>();
            CreateMap<ProductFamily, ProductFamilyDto>();

            CreateMap<Person, PersonDto>()
                .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.City.State))
                .ForMember(dest => dest.MainPersonGroup, opt => opt.MapFrom(src => src.PersonGroup[0].Name))
                .ForMember(dest => dest.CityName, opt => opt.MapFrom(src => src.City.Name));
            CreateMap<PersonGroup, PersonGroupDto>();
            CreateMap<City, CityDto>();
        }
    }
}
