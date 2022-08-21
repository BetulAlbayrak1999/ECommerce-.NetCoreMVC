using BussinessLogicLayer.Dtos.CategoryDtos;
using BussinessLogicLayer.Dtos.OrderDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogicLayer.Validations.FluentValidations.OrderValidations
{
    public class UpdateOrderRequestValidator : AbstractValidator<UpdateOrderRequestDto>
    {
        public UpdateOrderRequestValidator()
        {

            RuleFor(x => x.ApplicationUserId).NotEmpty();


            RuleFor(x => x.ProductId).NotEmpty();


            RuleFor(x => x.Quantity).GreaterThanOrEqualTo(0);


        }
    }
}
