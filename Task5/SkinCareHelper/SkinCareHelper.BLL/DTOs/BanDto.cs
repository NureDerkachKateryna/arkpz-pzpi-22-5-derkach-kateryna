using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkinCareHelper.BLL.DTOs
{
    public class BanDto
    {
        public int BanId { get; set; }

        public string UserId { get; set; } = null!;

        public UserDto User { get; set; } = null!;

        public int ProductId { get; set; }

        public ProductDto Product { get; set; } = null!;
    }
}
