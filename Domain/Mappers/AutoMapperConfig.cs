using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Domain.Entity;
using Domain.ViewModels;

namespace Domain.Mappers
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Order, OrderViewModel>().ReverseMap();
        }
    }
}
