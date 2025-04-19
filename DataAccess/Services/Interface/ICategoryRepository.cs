using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities.Concrete;

namespace DataAccess.Services.Interface
{
    public interface ICategoryRepository : IBaseRepository<Category>
    {
    }
}
