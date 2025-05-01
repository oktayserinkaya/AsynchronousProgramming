using AutoMapper;
using Core.Entities.Concrete;
using WEB.ExtensionMethods;
using WEB.Models.ProductViewModels;

namespace WEB.Automapper
{
    public class ProductMapping : Profile
    {
        public ProductMapping()
        {
            CreateMap<GetProductVM, Product>().ReverseMap()
                .ForMember(dest => dest.UpdatedDate, opt => opt.MapFrom(src => src.UpdatedDate == null ? " - " : src.UpdatedDate.Value.ToString("yyyy-MM-dd HH:mm:ss")))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.Name));

            CreateMap<GetProductInfoVM, Product>().ReverseMap()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.UnitPrice.ToTL()))
                .ForMember(dest => dest.ImagePath, opt => opt.MapFrom(src => string.IsNullOrEmpty(src.ImagePath) ? "default.jpg" : src.ImagePath));

            CreateMap<Product, CreateProductVM>().ReverseMap();

            CreateMap<Product, UpdateProductVM>().ReverseMap()
                .ForMember(dest => dest.ImagePath, opt => opt.MapFrom(src => string.IsNullOrEmpty(src.ImagePath) ? "default.jpg" : src.ImagePath));

            CreateMap<Product, UpdateProductVM>().ReverseMap();
        }
    }
}
