using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Core.Entities.Abstract;
using DataAccess.Context;
using DataAccess.Services.Interface;
using DataAccess.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace DataAccess.Services.Concrete
{
    public class BaseRepository<T>(AppDbContext context) : IBaseRepository<T> where T : BaseEntity
    {
        private readonly AppDbContext _context = context;
        private readonly DbSet<T> _table = context.Set<T>();

        public async Task<ServiceMessages> AddAsync(T entity)
        {
            await _table.AddAsync(entity);

            return await GetResultAsync();
        }

        public async Task<ServiceMessages> UpdateAsync(T entity)
        {
            entity.UpdatedDate = DateTime.Now;
            entity.Status = Status.Modified;
            _table.Update(entity);

            return await GetResultAsync();
        }

        public async Task<ServiceMessages> DeleteAsync(T entity)
        {
            entity.DeletedDate = DateTime.Now;
            entity.Status = Status.Passive;
            _table.Update(entity);

            return await GetResultAsync();
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression) => await _table.AnyAsync(expression);

        public async Task<List<T>> GetByDefaultAsync(Expression<Func<T, bool>> expression) => await _table.Where(expression).ToListAsync();

        public async Task<T?> GetByIdAsync(int id) => await _table.FirstOrDefaultAsync(x => x.Status != Status.Passive && x.Id == id);

        public async Task<List<TResult>> GetFilteredListAsync<TResult>(Expression<Func<T, TResult>> select, Expression<Func<T, bool>>? where = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>>? join = null)
        {
            IQueryable<T> query = _table;

            if (join != null)
                query = join(query);
            if (where != null)
                query = query.Where(where);
            if (orderBy != null)
                return await orderBy(query).Select(select).ToListAsync();

            return await query.Select(select).ToListAsync();

        }

        private async Task<bool> SaveAsync() => await _context.SaveChangesAsync() > 0;

        private async Task<ServiceMessages> GetResultAsync() => await SaveAsync() ? ServiceMessages.Success : ServiceMessages.Error;
    }
}
