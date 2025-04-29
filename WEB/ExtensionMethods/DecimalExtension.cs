using System.Runtime.CompilerServices;

namespace WEB.ExtensionMethods
{
    public static class DecimalExtension
    {
        public static string ToTL(this decimal unitPrice) => unitPrice.ToString("N2") + "TL";
    }
}
