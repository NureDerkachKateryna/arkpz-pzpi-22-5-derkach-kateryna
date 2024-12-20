using FluentValidation;
using SkinCareHelper.BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkinCareHelper.BLL.Validators
{
    public class ProductValidator : AbstractValidator<ProductDto>
    {
        public ProductValidator() 
        { 
            RuleFor(p => p.ProductName).NotEmpty();
            RuleFor(p => p.SkinType).NotNull();
            RuleFor(p => p.ProductDescription).NotEmpty();
            RuleFor(p => p.SkinIssue).NotNull();
        }
    }
}
