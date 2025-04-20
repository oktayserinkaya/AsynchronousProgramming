using System.Threading.Tasks;
using AutoMapper;
using Core.Entities.Abstract;
using Core.Entities.Concrete;
using DataAccess.Services.Interface;
using DataAccess.Utils;
using Microsoft.AspNetCore.Mvc;
using WEB.Models.CategoryViewModels;
using WEB.Utils;

namespace WEB.Controllers
{
    public class CategoriesController(ICategoryRepository categoryRepo, IMapper mapper) : Controller
    {
        private readonly ICategoryRepository _categoryRepo = categoryRepo;
        private readonly IMapper _mapper = mapper;

        public async Task<IActionResult> Index()
        {
            var model = await _categoryRepo.GetFilteredListAsync
            (
                select: x => _mapper.Map<GetCategoryVM>(x),
                where: x => x.Status != Status.Passive,
                orderBy: x => x.OrderByDescending(z => z.CreatedDate)
            );

            return View(model);
        }

        public IActionResult CreateCategory() => View();

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCategory(CreateCategoryVM model)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = WebMessages.ValidationErrorMessage;
                return View(model);
            }

            if (await _categoryRepo.AnyAsync(x => x.Name == model.Name && x.Status != Status.Passive))
            {
                TempData["Error"] = WebMessages.SameNameValidationErrorMessage;
                return View(model);
            }

            var category = _mapper.Map<Category>(model);
            var result = await _categoryRepo.AddAsync(category);

            if (result == ServiceMessages.Error)
            {
                TempData["Error"] = WebMessages.ErrorMessage;
                return View(model);
            }

            TempData["Success"] = WebMessages.SuccessMessage;
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> UpdateCategory(int id)
        {
            if (id <= 0 || !await _categoryRepo.AnyAsync(x => x.Id == id && x.Status != Status.Passive))
            {
                TempData["Error"] = WebMessages.NotFoundErrorMessage;
                return RedirectToAction("Index");
            }

            var category = await _categoryRepo.GetByIdAsync(id);
            if (category == null)
            {
                TempData["Error"] = WebMessages.NotFoundErrorMessage;
                return RedirectToAction("Index");
            }
            var model = _mapper.Map<UpdateCategoryVM>(category);
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryVM model)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = WebMessages.ValidationErrorMessage;
                return View(model);
            }

            if (await _categoryRepo.AnyAsync(x => x.Name == model.Name && x.Id != model.Id && x.Status != Status.Passive))
            {
                TempData["Error"] = WebMessages.SameNameValidationErrorMessage;
                return View(model);
            }

            var category = await _categoryRepo.GetByIdAsync(model.Id);

            if (category == null)
            {
                TempData["Error"] = WebMessages.NotFoundErrorMessage;
                return RedirectToAction("Index");
            }

            _ = _mapper.Map(model, category);

            var result = await _categoryRepo.UpdateAsync(category);

            if (result == ServiceMessages.Error)
            {
                TempData["Error"] = WebMessages.ErrorMessage;
                return View(model);
            }

            TempData["Success"] = WebMessages.SuccessMessage;
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteCategory(int id)
        {
            if (id <= 0 || !await _categoryRepo.AnyAsync(x => x.Id == id && x.Status != Status.Passive))
            {
                TempData["Error"] = WebMessages.NotFoundErrorMessage;
                return RedirectToAction("Index");
            }

            var category = await _categoryRepo.GetByIdAsync(id);

            if (category == null)
            {

                TempData["Error"] = WebMessages.NotFoundErrorMessage;
                return RedirectToAction("Index");
            }

            var result = await _categoryRepo.DeleteAsync(category);

            if (result == ServiceMessages.Error)
            {
                TempData["Error"] = WebMessages.ErrorMessage;
                return RedirectToAction("Index");
            }

            TempData["Success"] = WebMessages.SuccessMessage;

            return RedirectToAction("Index");
        }
    }
}
