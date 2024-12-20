using FluentValidation;
using SkinCareHelper.BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkinCareHelper.BLL.Validators
{
    public class UserValidator : AbstractValidator<UserDto>
    {
        public UserValidator() 
        {
            RuleFor(u => u.Email).NotEmpty();
            RuleFor(u => u.DisplayName).NotEmpty();
            RuleFor(u => u.SkinIssue).NotNull();
            RuleFor(u => u.SkinType).NotNull();
        }
    }
}
