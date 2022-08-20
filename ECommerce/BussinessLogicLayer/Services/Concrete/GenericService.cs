using AutoMapper;
using BussinessLogicLayer.Configrations.Responses;
using BussinessLogicLayer.Services.Abstract;
using DataAccessLayer.Repositories.EntityFrameworkRepositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogicLayer.Services.Concrete
{
    public abstract class GenericService<T>
        : IGenericService<T>
        where T : class, new()
    {
        #region Field and Constructor
        private readonly IMapper _autoMapper;
        private readonly IGenericRepository<T> _genericRepository;

        public GenericService(IMapper autoMapper, IGenericRepository<T> genericRepository)
        {
            _autoMapper = autoMapper;
            _genericRepository = genericRepository;
        }
        #endregion


        #region DeleteAsync
        public async Task<CommandResponse> DeleteAsync(Guid Id)
        {
            try
            {
                bool IsDeleted = await _genericRepository.DeleteAsync(Id);
                if (IsDeleted)
                    return new CommandResponse { Status = true, Message = "This operation has not done successfully" };

                return new CommandResponse { Status = false, Message = "This operation has not done successfully" };
            }
            catch (Exception ex)
            {
                return new CommandResponse { Status = false, Message = ex.Message };
            }
        }

        #endregion

    }
}
