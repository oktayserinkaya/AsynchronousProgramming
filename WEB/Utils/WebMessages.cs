namespace WEB.Utils
{
    public static class WebMessages
    {
        private static string _successMessage = "İşlem Başarılı";
        private static string _errorMessage = "İşlem Başarısız";
        private static string _validationErrorMessage = "Lütfen Aşağıdaki Kurallara Uyunuz";
        private static string _sameNameValidationErrorMessage = "Bu isim kullanılmaktadır";
        private static string _notFoundErrorMessage = "Sayfa bulunamadı";

        public static string SuccessMessage { get => _successMessage; }
        public static string ErrorMessage { get => _errorMessage; }
        public static string ValidationErrorMessage { get => _validationErrorMessage; }
        public static string SameNameValidationErrorMessage { get => _sameNameValidationErrorMessage; }
        public static string NotFoundErrorMessage { get => _notFoundErrorMessage; }
    }
}
