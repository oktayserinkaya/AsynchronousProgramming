using System.Text.RegularExpressions;
using FluentValidation;
using WEB.Models.ProductViewModels;

namespace WEB.FluentValidation.ProductValidators
{
    public class CreateProductValidator : AbstractValidator<CreateProductVM>
    {
        public CreateProductValidator()
        {
            Regex regex = new Regex("^[a-zA-Z ıİğĞüÜşŞçÇöÖ-]+$");

            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Bu Alan Zorunludur")
                .MaximumLength(250)
                .WithMessage("250 Karakter Sınırını Geçtiniz")
                .MinimumLength(3)
                .WithMessage("En Az 3 Karakter Girmelisiniz")
                .Matches(regex)
                .WithMessage("Sadece harf, boşluk ve \"-\" kullanabilirsiniz");

            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("Bu Alan Zorunludur")
                .MaximumLength(250)
                .WithMessage("250 Karakter Sınırını Geçtiniz")
                .MinimumLength(3)
                .WithMessage("En Az 3 Karakter Girmelisiniz")
                .Matches(regex)
                .WithMessage("Sadece harf, boşluk ve \"-\" kullanabilirsiniz");

            RuleFor(x => x.UnitPrice)
                .NotEmpty()
                .WithMessage("Bu Alan Zorunludur")
                .GreaterThan(25)
                .WithMessage("En Az 25 Girmelisiniz")
                .LessThanOrEqualTo(100)
                .WithMessage("En Fazla 200 Girmelisiniz");

            RuleFor(x => x.CategoryId)
                .NotEmpty()
                .WithMessage("Bu Alan Zorunludur")
                .GreaterThan(0)
                .WithMessage("Lütfen Bir Kategori Seçiniz");
        }
    }
}
