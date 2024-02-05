using System.Text;
using Fretter.Util;

namespace System
{
    public class Base64
    {
        public static string Encrypt(string text)
        {
            Throw.IfIsNullOrEmpty(text);

            byte[] toEncodeAsBytes = ASCIIEncoding.UTF8.GetBytes(text);
            string returnValue = Convert.ToBase64String(toEncodeAsBytes);
            return returnValue;
        }

        public static string Decrypt(string text)
        {
            Throw.IfIsNullOrEmpty(text);

            byte[] encodedDataAsBytes = Convert.FromBase64String(text);
            string returnValue = ASCIIEncoding.UTF8.GetString(encodedDataAsBytes);
            return returnValue;
        }
    }
}
