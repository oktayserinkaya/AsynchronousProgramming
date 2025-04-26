using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities.Abstract;
using Core.Entities.Concrete;
using DataAccess.Context;
using DataAccess.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Services.Concrete
{
    public class ProductRepository(AppDbContext context) : BaseRepository<Product>(context), IProductRepository
    {
        private readonly AppDbContext _context = context;

        public async Task<Product?> GetProductByIdAsync(int id)
            => await _context.Products.Include(z => z.Category).FirstOrDefaultAsync(x => x.Id == id && x.Status != Status.Passive);
    }
}
