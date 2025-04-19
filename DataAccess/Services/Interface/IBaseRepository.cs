using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Core.Entities.Abstract;
using DataAccess.Utils;
using Microsoft.EntityFrameworkCore.Query;

namespace DataAccess.Services.Interface
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task<ServiceMessages> AddAsync(T entity);
        Task<ServiceMessages> UpdateAsync(T entity);
        Task<ServiceMessages> DeleteAsync(T entity);

        Task<T?> GetByIdAsync(int id);
        Task<List<T>> GetByDefaultAsync(Expression<Func<T, bool>> expression);
        Task<bool> AnyAsync(Expression<Func<T, bool>> expression);
        Task<List<TResult>> GetFilteredListAsync<TResult>
        (
            Expression<Func<T, TResult>> select,
            Expression<Func<T, bool>>? where = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? join = null
        );
    }
}