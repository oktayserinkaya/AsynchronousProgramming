using FluentValidation;
using System.Text.RegularExpressions;
using WEB.Models.CategoryViewModels;

namespace WEB.FluentValidation.CategoryValidators
{
    public partial class UpdateCategoryValidator : AbstractValidator<UpdateCategoryVM>
    {

        public UpdateCategoryValidator()
        {
            Regex regex = MyRegex();

            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Bu Alan Boş Geçilemez!")
                .MaximumLength(100)
                .WithMessage("100 karakter sınıırını aştınız")
                .MinimumLength(3)
                .WithMessage("En az 3 karakter girmelisiniz")
                .Matches(regex)
                .WithMessage("Sadece harf, boşluk ve \"-\" kullanabilirsiniz");
        }

        [GeneratedRegex("^[a-zA-Z ıİğĞüÜşŞçÇöÖ-]+$")]
        private static partial Regex MyRegex();
    }
}
