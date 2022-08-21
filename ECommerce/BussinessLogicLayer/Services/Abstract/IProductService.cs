using BussinessLogicLayer.Configrations.Responses;
using BussinessLogicLayer.Dtos.CategoryDtos;
using BussinessLogicLayer.Dtos.ProductDtos;
using DataAccessLayer.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogicLayer.Services.Abstract
{
    public interface IProductService : IGenericService<Product>
    {
        public Task<IEnumerable<GetAllProductRequestDto>> GetAllActivatedAsync();

        public Task<IEnumerable<GetAllProductRequestDto>> GetAllUnActivatedAsync();

        public Task<IEnumerable<GetAllProductRequestDto>> GetAllAsync();

        public Task<CommandResponse> UpdateAsync(UpdateProductRequestDto item);

        public Task<CommandResponse> ActivateAsync(Guid Id);

        public Task<CommandResponse> UnActivateAsync(Guid Id);

        public Task<CommandResponse> CreateAsync(CreateProductRequestDto item);
        public Task<GetProductRequestDto> GetByIdAsync(Guid Id);

        public Task<IEnumerable<GetAllCategoryRequestDto>> GetAllActivatedCategory();
    }
}
