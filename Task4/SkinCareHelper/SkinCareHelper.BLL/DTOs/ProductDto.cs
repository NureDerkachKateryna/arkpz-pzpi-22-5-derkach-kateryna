using Microsoft.AspNetCore.Http;
using SkinCareHelper.DAL.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkinCareHelper.BLL.DTOs
{
    public class ProductDto
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; } = null!;

        public IFormFile Photo { get; set; } = null!;

        public PhotoDto ProductPhoto { get; set; } = null!;

        public string PhotoId { get; set; } = null!;

        public string ProductDescription { get; set; } = null!;

        public ProductTypeDto ProductType { get; set; } 

        public SkinTypeDto SkinType { get; set; } 

        public SkinIssueDto SkinIssue { get; set; }

        public string Brand { get; set; } = null!;

        public List<RoutineProductDto> RoutineProducts { get; set; } = new List<RoutineProductDto>();

        public List<BanDto> Bans { get; set; } = new List<BanDto>();
    }
}
