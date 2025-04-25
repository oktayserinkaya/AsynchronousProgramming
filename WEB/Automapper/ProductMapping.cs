using AutoMapper;
using Core.Entities.Concrete;
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
        }
    }
}
