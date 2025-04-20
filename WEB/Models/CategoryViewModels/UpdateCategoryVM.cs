using System.ComponentModel.DataAnnotations;

namespace WEB.Models.CategoryViewModels
{
    public class UpdateCategoryVM
    {
        public int Id { get; set; }

        [Display(Name = "Kategori Adı")]
        public string Name { get; set; }
    }
}
