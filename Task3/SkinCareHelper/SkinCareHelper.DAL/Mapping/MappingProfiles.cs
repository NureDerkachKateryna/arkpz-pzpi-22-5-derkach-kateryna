using AutoMapper;
using SkinCareHelper.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkinCareHelper.DAL.Mapping
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles() 
        {
            CreateMap<Product, Product>();
            CreateMap<User, User>();
            CreateMap<Ban, Ban>();
            CreateMap<RoutineProduct, RoutineProduct>();
            CreateMap<SkincareRoutine, SkincareRoutine>();
        }
    }
}
