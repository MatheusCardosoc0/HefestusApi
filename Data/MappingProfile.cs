using AutoMapper;
using HefestusApi.DTOs.Administracao;
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
            CreateMap<Order, OrderViewDto>();
            CreateMap<OrderProduct, OrderProductViewDto>()
                .ForMember(dest => dest.Product, opt => opt.MapFrom(src => src.Product));
            CreateMap<Product, OrderProductDataViewDto>();
            CreateMap<PaymentCondition, OrderPaymentConditionViewDto>();
            CreateMap<PaymentOptions, OrderPaymentOptionViewDto>();
            CreateMap<Person, OrderPersonViewDto>();

            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.GroupName, opt => opt.MapFrom(src => src.Group.Name))
                .ForMember(dest => dest.FamilyName, opt => opt.MapFrom(src => src.Family.Name))
                .ForMember(dest => dest.SubGroupName, opt => opt.MapFrom(src => src.Subgroup.Name));
            CreateMap<ProductSubGroup, ProductSubGroupDto>();
            CreateMap<ProductGroup, ProductGroupDto>();
            CreateMap<ProductFamily, ProductFamilyDto>();

            CreateMap<Person, PersonDto>()
                .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.City.State))
                .ForMember(dest => dest.MainPersonGroup, opt => opt.MapFrom(src => src.PersonGroup[0].Name));
            CreateMap<PersonGroup, PersonGroupDto>();
            CreateMap<City, CityDto>();
        }
    }
}
