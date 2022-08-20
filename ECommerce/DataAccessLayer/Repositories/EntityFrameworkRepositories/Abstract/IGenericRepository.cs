using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.EntityFrameworkRepositories.Abstract
{
    public interface IGenericRepository<T> where T : class
    {
        Task<bool> CreateAsync(T item);

        Task<bool> UpdateAsync(T item);

        Task<bool> DeleteAsync(Guid Id);

        Task<T> GetByAsync(Expression<Func<T, bool>> predicate = null);

        Task<T> GetByIdAsync(Guid Id);

        Task<IEnumerable<T>> GetAllByAsync(Expression<Func<T, bool>> predicate = null);
    }
}
