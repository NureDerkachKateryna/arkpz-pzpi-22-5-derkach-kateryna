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
            RuleFor(u => u.DisplayName).NotEmpty().MinimumLength(100);
            RuleFor(u => u.SkinIssue).NotEmpty();
            RuleFor(u => u.SkinType).NotEmpty();
        }
    }
}
