using Core.Entities.Abstract;

namespace WEB.Models.CategoryViewModels
{
    public class GetCategoryVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedDate { get; set; }
        public string Status { get; set; }
    }
}
