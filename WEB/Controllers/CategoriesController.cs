using System.Threading.Tasks;
using Core.Entities.Abstract;
using DataAccess.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace WEB.Controllers
{
    public class CategoriesController(ICategoryRepository categoryRepo) : Controller
    {
        private readonly ICategoryRepository _categoryRepo = categoryRepo;

        public async Task<IActionResult> Index()
        {
            var categories = await _categoryRepo.GetByDefaultAsync(x => x.Status != Status.Passive);
            return View();
        }
    }
}
