﻿using AutoMapper;
using HefestusApi.DTOs.Administracao;
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
        }
    }
}
