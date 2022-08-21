using BussinessLogicLayer.Dtos.ApplicationUserDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogicLayer.Validations.FluentValidations.ApplicationUser
{
    public class UpdateApplicationUserRequestValidator : AbstractValidator<UpdateApplicationUserRequestDto>
    {
        public UpdateApplicationUserRequestValidator()
        {

            RuleFor(x => x.FirstName).MinimumLength(3).MaximumLength(100);

            RuleFor(x => x.LastName).MinimumLength(3).MaximumLength(100);

            RuleFor(x => x.UserName).MinimumLength(3).MaximumLength(100);
        }
    }
}
