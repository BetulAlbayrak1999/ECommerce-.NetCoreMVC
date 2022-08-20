using BussinessLogicLayer.Dtos.CategoryDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogicLayer.Validations.FluentValidations.CategoryValidations
{
    public class UpdateCategoryRequestValidator : AbstractValidator<UpdateCategoryRequestDto>
    {
        public UpdateCategoryRequestValidator()
        {

            RuleFor(x => x.Name).NotEmpty().MinimumLength(3).MaximumLength(50);

            RuleFor(x => x.Description).NotEmpty().MinimumLength(3).MaximumLength(50);

        }
    }
}
