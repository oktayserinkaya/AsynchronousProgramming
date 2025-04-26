using System.ComponentModel.DataAnnotations;
using Core.Entities.Abstract;

namespace WEB.Models.ProductViewModels
{
    public class CreateProductVM : BaseEntity
    {
        [Display(Name = "Ürün Adı")]
        public string Name { get; set; }

        [Display(Name = "Açıklama")]
        public string Description { get; set; }

        [Display(Name = "Birim Fiyat")]
        public decimal? UnitPrice { get; set; }

        [Display(Name = "Fotoğraf")]
        public IFormFile? Image { get; set; }
        public string? ImagePath { get; set; }

        [Display(Name = "Kategori")]
        public int? CategoryId { get; set; }
    }
}
