using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkinCareHelper.BLL.DTOs
{
    public class RoutineProductDto
    {
        public int RoutineProductId { get; set; }

        public int SkincareRoutineId { get; set; }

        public SkincareRoutineDto SkincareRoutine { get; set; } = null!;

        public int ProductId { get; set; }

        public ProductDto Product { get; set; } = null!;
    }
}
