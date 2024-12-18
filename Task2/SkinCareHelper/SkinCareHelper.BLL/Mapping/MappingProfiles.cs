using AutoMapper;
using SkinCareHelper.BLL.DTOs;
using SkinCareHelper.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkinCareHelper.BLL.Mapping
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductDto>();
            CreateMap<ProductDto, Product>();
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
            CreateMap<Ban, BanDto>();
            CreateMap<BanDto, Ban>();
            CreateMap<RoutineProduct, RoutineProductDto>();
            CreateMap<RoutineProductDto, RoutineProduct>();
            CreateMap<SkincareRoutine, SkincareRoutineDto>();
            CreateMap<SkincareRoutineDto, SkincareRoutine>();
            CreateMap<Photo, PhotoDto>();
            CreateMap<PhotoDto, Photo>();
        }
    }
}
