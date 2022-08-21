using BussinessLogicLayer.Dtos.ProductDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogicLayer.Validations.FluentValidations.ProductValidations
{
    public class CreateProductRequestValidator : AbstractValidator<CreateProductRequestDto>
    {
        public CreateProductRequestValidator()
        {
            RuleFor(x => x.Sku).NotEmpty().Length(7);

            RuleFor(x => x.Name).NotEmpty().MinimumLength(3).MaximumLength(50);

            RuleFor(x => x.Price).GreaterThanOrEqualTo(0);

            RuleFor(x => x.CategoryId).NotEmpty();

            RuleFor(x => x.Stoke).NotEmpty().GreaterThanOrEqualTo(0);

            RuleFor(x => x.Discount_Percentage).NotEmpty().LessThan(100).GreaterThanOrEqualTo(0);

        }
    }
}