using BussinessLogicLayer.Configrations.Responses;
using BussinessLogicLayer.Dtos.ApplicationUserDtos;
using DataAccessLayer.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogicLayer.Services.Abstract
{
    public interface IApplicationUserService
    {
        public Task<IEnumerable<GetAllApplicationUserRequestDto>> GetAllActivatedAsync();

        public Task<IEnumerable<GetAllApplicationUserRequestDto>> GetAllUnActivatedAsync();

        public Task<IEnumerable<GetAllApplicationUserRequestDto>> GetAllAsync();

        //public Task<CommandResponse> UpdateAsync(UpdateApplicationUserRequestDto item);

        public Task<CommandResponse> ActivateAsync(Guid Id);

        public Task<CommandResponse> UnActivateAsync(Guid Id);

        public Task<GetApplicationUserRequestDto> GetByIdAsync(Guid Id);

    }
}
