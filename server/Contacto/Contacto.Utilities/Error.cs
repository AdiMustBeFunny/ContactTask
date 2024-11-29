namespace Contacto.Utilities
{
    public record Error(string ErrorCode, string ErrorMessage)
    {
        public static Error Empty = new(string.Empty, string.Empty);

        public static Error NullObject = new(nameof(NullObject), "Object was null");
    }
}