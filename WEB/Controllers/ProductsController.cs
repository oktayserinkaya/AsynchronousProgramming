using AutoMapper;
using Core.Entities.Abstract;
using Core.Entities.Concrete;
using DataAccess.Services.Interface;
using DataAccess.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
                var checkId = await _productRepo.AnyAsync(x => x.Id == id && x.Status != Status.Passive);
                if (!checkId)
                {
                    TempData["Error"] = "Böyle bir ürün bulunmamaktadır!...";
                    return RedirectToAction("Index");
                }

                var product = await _productRepo.GetByIdAsync(id);
                var model = _mapper.Map<GetProductInfoVM>(product);
                return View(model);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public async Task<IActionResult> CreateProduct()
        {
            var categories = await _categoryRepo.GetByDefaultAsync(x => x.Status != Status.Passive);
            ViewBag.Categories = new SelectList(categories, "Id", "Name");

            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateProduct(CreateProductVM model)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Lütfen Aşağıdaki Kurallara Uyunuz";
                return View(model);
            }

            var product = _mapper.Map<Product>(model);

            if (model.Image != null)
            {
                product.ImagePath = await ImageSaveAsync(model.Image);
            }

            var result = await _productRepo.AddAsync(product);
            if (result == ServiceMessages.Error)
            {
                TempData["Error"] = "Ürün Eklenememiştir";
                return View(model);
            }

            TempData["Success"] = "Ürün Başarılı Bir Şekilde Eklenmiştir";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateProduct(int id)
        {
            var checkId = await _productRepo.AnyAsync(x => x.Id == id && x.Status != Status.Passive);
            if (!checkId)
            {
                TempData["Error"] = "Böyle Bir Ürün Bulunmamaktadır.";
                return RedirectToAction("Index");
            }

            var product = await _productRepo.GetProductByIdAsync(id);

            ViewBag.Categories = new SelectList
            (
                await _categoryRepo.GetByDefaultAsync(x => x.Status != Status.Passive), "Id", "Name", _categoryRepo.GetByIdAsync(product.CategoryId)
            );

            var model = _mapper.Map<UpdateProductVM>(product);

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateProduct(UpdateProductVM model)
        {
            var categories = await _categoryRepo.GetByDefaultAsync(x => x.Status != Status.Passive);

            if (!ModelState.IsValid)
            {
                var selectedCat = await _categoryRepo.GetByIdAsync(model.CategoryId.Value);
                ViewBag.Categories = new SelectList(categories, "Id", "Name", selectedCat?.Id);
                TempData["Error"] = "Lütfen aşağıdaki kurallara uyunuz!";
                return View(model);
            }

            var checkId = await _productRepo.AnyAsync(x => x.Id == model.Id && x.Status != Status.Passive);
            if (!checkId)
            {
                var selectedCat = await _categoryRepo.GetByIdAsync(model.CategoryId.Value);
                ViewBag.Categories = new SelectList(categories, "Id", "Name", selectedCat?.Id);
                TempData["Error"] = "Böyle bir ürün bulunmamaktadır!";
                return RedirectToAction("Index");
            }

            var product = await _productRepo.GetByIdAsync(model.Id);
            string imageURL = product.ImagePath;

            _mapper.Map(model, product);

            if (model.Image != null)
            {
                imageURL = await ImageSaveAsync(model.Image);
            }

            product.ImagePath = imageURL;

            var result = await _productRepo.UpdateAsync(product);
            if (result == ServiceMessages.Error)
            {
                var selectedCat = await _categoryRepo.GetByIdAsync(model.CategoryId.Value);
                ViewBag.Categories = new SelectList(categories, "Id", "Name", selectedCat?.Id);
                TempData["Error"] = "Ürün güncellenemedi!";
                return View(model);
            }

            TempData["Success"] = "Ürün bilgileri başarılı bir şekilde güncellendi!";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteProduct(int id)
        {
            var checkId = await _productRepo.AnyAsync(x => x.Id == id && x.Status != Status.Passive);

            if (!checkId)
            {
                TempData["Error"] = "Böyle Bir Ürün Bulunmamaktadır.";
                return RedirectToAction("Index");
            }

            var product = await _productRepo.GetByIdAsync(id);
            var result = await _productRepo.DeleteAsync(product);

            if (result == ServiceMessages.Error)
            {
                TempData["Error"] = "Ürün Silinemedi";
                return RedirectToAction("Index");
            }

            TempData["Success"] = "Ürün Başarılı Bir Şekilde Silindi";
            return RedirectToAction("Index");
        }

        private async Task<string> ImageSaveAsync(IFormFile file)
        {
            string imageName = $"{Guid.NewGuid()}_{file.FileName}";
            string imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", imageName);

            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return imageName;
        }
    }
}
