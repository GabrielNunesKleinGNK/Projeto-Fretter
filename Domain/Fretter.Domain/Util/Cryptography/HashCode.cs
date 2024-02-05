namespace System
{
    public static class HashCode
    {
        public static string Encrypt(string text) => string.Format("{0:X}", text.GetHashCode());
    }
}
