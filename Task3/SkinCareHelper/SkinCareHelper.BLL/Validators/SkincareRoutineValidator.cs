using FluentValidation;
using SkinCareHelper.BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkinCareHelper.BLL.Validators
{
    public class SkincareRoutineValidator : AbstractValidator<SkincareRoutineDto>
    {
        public SkincareRoutineValidator() 
        { 
            RuleFor(s => s.CreationDate).NotEmpty();
            RuleFor(s => s.DayOfWeek).NotEmpty();
            RuleFor(s => s.TimeOfDay).NotEmpty();
        }
    }
}
