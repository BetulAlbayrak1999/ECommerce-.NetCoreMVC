using BussinessLogicLayer.Configrations.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogicLayer.Services.Abstract
{
    public interface IGenericService<T>
        where T : class, new()
    {
         public Task<CommandResponse> DeleteAsync(Guid Id);
    }
}
