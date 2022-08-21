using BussinessLogicLayer.Configrations.Responses;
using BussinessLogicLayer.Dtos.ApplicationUserDtos;
using BussinessLogicLayer.Dtos.OrderDtos;
using BussinessLogicLayer.Dtos.ProductDtos;
using DataAccessLayer.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogicLayer.Services.Abstract
{
    public interface IOrderService : IGenericService<Order>
    {
        public Task<IEnumerable<GetAllOrderRequestDto>> GetAllActivatedAsync();

        public Task<IEnumerable<GetAllOrderRequestDto>> GetAllUnActivatedAsync();

        public Task<IEnumerable<GetAllOrderRequestDto>> GetAllAsync();

        public Task<CommandResponse> UpdateAsync(UpdateOrderRequestDto item);

        public Task<CommandResponse> ActivateAsync(Guid Id);

        public Task<CommandResponse> UnActivateAsync(Guid Id);

        public Task<CommandResponse> CreateAsync(CreateOrderRequestDto item);

        public Task<GetOrderRequestDto> GetByIdAsync(Guid Id);

        public Task<IEnumerable<GetAllApplicationUserRequestDto>> GetAllActivatedApplicationUser();
        public Task<IEnumerable<GetAllProductRequestDto>> GetAllActivatedProduct();

    }
}
