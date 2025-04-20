using System.ComponentModel.DataAnnotations;

namespace WEB.Models.CategoryViewModels
{
    public class CreateCategoryVM
    {
        [Display(Name = "Kategori Adı")]
        public string? Name { get; set; }
    }
}
