using BussinessLogicLayer.ViewModels.AuthViewModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogicLayer.Validations.FluentValidations.AuthValidations
{
    public class TokenRequestVMValidator : AbstractValidator<TokenRequestVM>
    {
        public TokenRequestVMValidator()
        {
            RuleFor(x => x.Email).EmailAddress().NotEmpty();

            RuleFor(x => x.Password).NotEmpty().MinimumLength(8).MaximumLength(16);
        }
    }
}
