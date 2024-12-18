using Microsoft.AspNetCore.Http;
using SkinCareHelper.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkinCareHelper.DAL.Abstractions
{
    public interface IPhotoAccessor
    {
        Task<Photo> AddPhoto(IFormFile file);

        Task<bool> DeletePhoto(string publicId);
    }
}
