using BussinessLogicLayer.Configrations.Responses;
using BussinessLogicLayer.Dtos.CategoryDtos;
using DataAccessLayer.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogicLayer.Services.Abstract
{
    public interface ICategoryService : IGenericService<Category>
    {
        public Task<IEnumerable<GetAllCategoryRequestDto>> GetAllActivatedAsync();

        public Task<IEnumerable<GetAllCategoryRequestDto>> GetAllUnActivatedAsync();

        public Task<IEnumerable<GetAllCategoryRequestDto>> GetAllAsync();

        public Task<CommandResponse> UpdateAsync(UpdateCategoryRequestDto item);

        public Task<CommandResponse> ActivateAsync(Guid Id);

        public Task<CommandResponse> UnActivateAsync(Guid Id);

        public Task<CommandResponse> CreateAsync(CreateCategoryRequestDto item);
        public Task<GetCategoryRequestDto> GetByIdAsync(Guid Id);

    }
}
