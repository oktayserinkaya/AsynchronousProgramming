using AutoMapper;
using Core.Entities.Abstract;
using DataAccess.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WEB.Models.ProductViewModels;

namespace WEB.Controllers
{
    public class ProductsController(IProductRepository productRepo, ICategoryRepository categoryRepo, IMapper mapper, IWebHostEnvironment webHostEnvironment) : Controller
    {
        private readonly IProductRepository _productRepo = productRepo;
        private readonly ICategoryRepository _categoryRepo = categoryRepo;
        private readonly IMapper _mapper = mapper;
        private readonly IWebHostEnvironment _webHostEnvironment = webHostEnvironment;

        public async Task<IActionResult> Index()
        {
            try
            {
                var model = await _productRepo.GetFilteredListAsync
                (
                    select: x => _mapper.Map<GetProductVM>(x),
                    where: x => x.Status != Status.Passive,
                    orderBy: x => x.OrderByDescending(z => z.CreatedDate),
                    join: x => x.Include(z => z.Category)
                );

                return View(model);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Index", "Home");
            }
        }

        public async Task<IActionResult> GetInfoProduct(int id)
        {
            try
            {
                var productExists = await _productRepo.AnyAsync(x => x.Id == id && x.Status != Status.Passive);
                if (!productExists)
                {
                    TempData["Error"] = "Böyle bir ürün bulunmamaktadır!...";
                    return RedirectToAction("Index");
                }

                var product = await _productRepo.GetByIdAsync(id);
            }
            catch (Exception)
            {

                throw;
            }

            /* *-*-*-*-*-*-*-*-*-
public async Task<IActionResult> GetInfoEmployee(int id)
{
    try
    {
        var employeeExists = await _employeeRepository.AnyAsync(x => x.Id == id && x.Status != Status.Passive);
        if (!employeeExists)
        {
            TempData["Error"] = "Böyle bir çalışan bulunmamaktadır.";
            return RedirectToAction("Index");
        }

        var employee = await _employeeRepository.GetEmployeeByIdAsync(id);
        if (employee == null)
        {
            TempData["Error"] = "Çalışan bilgisi bulunamadı.";
            return RedirectToAction("Index");
        }

        var model = _mapper.Map<GetEmployeeInfoVM>(employee);
        return View(model);
    }
    catch (Exception ex)
    {
        TempData["Error"] = "İşlem sırasında bir hata oluştu.";
        return RedirectToAction("Index");
    }
}
             */
        }
    }
}
