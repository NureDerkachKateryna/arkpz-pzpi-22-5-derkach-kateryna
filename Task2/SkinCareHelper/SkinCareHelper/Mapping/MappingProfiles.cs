using AutoMapper;
using SkinCareHelper.BLL.DTOs;
using SkinCareHelper.DAL.Entities;
using SkinCareHelper.ViewModels.Products;

namespace SkinCareHelper.Mapping
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<AddProductViewModel, ProductDto>();
            CreateMap<ProductDto, AddProductViewModel>();
            CreateMap<ProductDto, UpdateProductViewModel>();
            CreateMap<UpdateProductViewModel, ProductDto>();
            CreateMap<ProductDto, ProductViewModel>();

            CreateMap<PhotoDto, PhotoViewModel>();
            CreateMap<PhotoViewModel, PhotoDto>();
            
            CreateMap<UserDto, UpdateUserViewModel>();
            CreateMap<UserDto, UserViewModel>();

            CreateMap<AddBanViewModel, BanDto>();
            CreateMap<BanDto, AddBanViewModel>();
            CreateMap<BanDto, BanViewModel>();

            CreateMap<AddSkincareRoutineViewModel, SkincareRoutineDto>();
            CreateMap<SkincareRoutineDto, AddSkincareRoutineViewModel>();
            CreateMap<SkincareRoutineDto, UpdateSkincareRoutineViewModel>();
            CreateMap<SkincareRoutineDto, SkincareRoutineViewModel>();

            CreateMap<RegisterViewModel, User>();
            CreateMap<UserDto, AuthResponseViewModel>();
        }
    }
}
