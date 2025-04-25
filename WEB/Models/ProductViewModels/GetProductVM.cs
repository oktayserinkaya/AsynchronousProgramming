namespace WEB.Models.ProductViewModels
{
    public class GetProductVM
    {
        internal object Category;

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedDate { get; set; }
        public string Status { get; set; }
    }
}
