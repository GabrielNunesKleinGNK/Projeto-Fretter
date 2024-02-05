using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Fretter.Domain.Extensions
{
    public class CryptoHelper
    {
        public static string SetHashValue(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return null;
            }
            byte[] bytes = Encoding.UTF8.GetBytes("926fb83cccc77e74" + text + "c0e0177157025690");
            byte[] bytes2 = Encoding.UTF8.GetBytes("c2a128532c3d1b6420cfceeada54aec5");
            using (var s = SHA256.Create())
                return Convert.ToBase64String(Encrypt(bytes, s.ComputeHash(bytes2)));
        }

        public static string GetHashValue(string hash)
        {
            string ret = string.Empty;
            if (string.IsNullOrEmpty(hash))
                return null;

            try
            {
                byte[] bytes = Convert.FromBase64String(hash);
                byte[] bytes2 = Encoding.UTF8.GetBytes("c2a128532c3d1b6420cfceeada54aec5");
                using (var s = SHA256.Create())
                    ret = Encoding.UTF8.GetString(Decrypt(bytes, s.ComputeHash(bytes2))).Replace("926fb83cccc77e74", "").Replace("c0e0177157025690", "");
            }
            catch (Exception)
            {
            }

            return ret;
        }

        private static byte[] Encrypt(byte[] bytesToBeEncrypted, byte[] passwordBytes)
        {
            //byte[] array = null;
            byte[] salt = new byte[8] { 9, 9, 1, 5, 3, 4, 6, 9 };
            using (var memoryStream = new MemoryStream())
            {
                using (var rijndaelManaged = new RijndaelManaged())
                {
                    using (var rfc2898DeriveBytes = new Rfc2898DeriveBytes(passwordBytes, salt, 1000))
                    {
                        rijndaelManaged.KeySize = 256;
                        rijndaelManaged.BlockSize = 128;
                        rijndaelManaged.Key = rfc2898DeriveBytes.GetBytes(rijndaelManaged.KeySize / 8);
                        rijndaelManaged.IV = rfc2898DeriveBytes.GetBytes(rijndaelManaged.BlockSize / 8);
                        rijndaelManaged.Mode = CipherMode.CBC;
                        using (var cryptoStream = new CryptoStream(memoryStream, rijndaelManaged.CreateEncryptor(), CryptoStreamMode.Write))
                        {
                            cryptoStream.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
                            cryptoStream.Close();
                        }
                    }
                    return memoryStream.ToArray();
                }
            }
        }

        private static byte[] Decrypt(byte[] bytesToBeDecrypted, byte[] passwordBytes)
        {
            byte[] salt = new byte[8] { 9, 9, 1, 5, 3, 4, 6, 9 };
            using (var memoryStream = new MemoryStream())
            {
                using (var rijndaelManaged = new RijndaelManaged())
                {
                    rijndaelManaged.KeySize = 256;
                    rijndaelManaged.BlockSize = 128;
                    using (var rfc2898DeriveBytes = new Rfc2898DeriveBytes(passwordBytes, salt, 1000))
                    {
                        rijndaelManaged.Key = rfc2898DeriveBytes.GetBytes(rijndaelManaged.KeySize / 8);
                        rijndaelManaged.IV = rfc2898DeriveBytes.GetBytes(rijndaelManaged.BlockSize / 8);
                        rijndaelManaged.Mode = CipherMode.CBC;
                        using (var cryptoStream = new CryptoStream(memoryStream, rijndaelManaged.CreateDecryptor(), CryptoStreamMode.Write))
                        {
                            cryptoStream.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
                            cryptoStream.Close();
                        }
                    }
                    return memoryStream.ToArray();
                }
            }
        }
    }
}
