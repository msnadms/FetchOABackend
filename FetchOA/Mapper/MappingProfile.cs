﻿using AutoMapper;
using FetchOA.Dtos;
using FetchOA.Models;

namespace FetchOA.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ReceiptDto, Receipt>()
                .ForMember(dest => dest.Id, opts => opts.Ignore())
                .ForMember(dest => dest.PurchaseDate, opts => opts.MapFrom(src => DateOnly.Parse(src.PurchaseDate!)))
                .ForMember(dest => dest.PurchaseTime, opts => opts.MapFrom(src => TimeOnly.Parse(src.PurchaseTime!)))
                .ForMember(dest => dest.Total, opts => opts.MapFrom(src => float.Parse(src.Total!)));

            CreateMap<ItemDto, Item>()
                .ForMember(dest => dest.Price, opts => opts.MapFrom(src => float.Parse(src.Price!)));
        }


    }
}
