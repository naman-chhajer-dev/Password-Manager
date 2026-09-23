using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Password_Manager
{
    public static class EncryptionHelper
    {
        // IMPORTANT:
        // Keep this key secret in a real application.
        private static readonly string EncryptionKey =
            "PasswordManager2026Key1234567890";

        public static string Encrypt(string plainText)
        {
            if (string.IsNullOrEmpty(plainText))
                return "";

            byte[] clearBytes =
                Encoding.UTF8.GetBytes(plainText);

            using (Aes aes = Aes.Create())
            {
                aes.Key =
                    Encoding.UTF8.GetBytes(
                        EncryptionKey.Substring(0, 32));

                aes.IV = new byte[16];

                using (MemoryStream memoryStream =
                       new MemoryStream())
                {
                    using (CryptoStream cryptoStream =
                           new CryptoStream(
                               memoryStream,
                               aes.CreateEncryptor(),
                               CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(
                            clearBytes,
                            0,
                            clearBytes.Length);

                        cryptoStream.FlushFinalBlock();
                    }

                    return Convert.ToBase64String(
                        memoryStream.ToArray());
                }
            }
        }


        public static string Decrypt(string encryptedText)
        {
            if (string.IsNullOrEmpty(encryptedText))
                return "";

            byte[] cipherBytes =
                Convert.FromBase64String(
                    encryptedText);

            using (Aes aes = Aes.Create())
            {
                aes.Key =
                    Encoding.UTF8.GetBytes(
                        EncryptionKey.Substring(0, 32));

                aes.IV = new byte[16];

                using (MemoryStream memoryStream =
                       new MemoryStream(cipherBytes))
                {
                    using (CryptoStream cryptoStream =
                           new CryptoStream(
                               memoryStream,
                               aes.CreateDecryptor(),
                               CryptoStreamMode.Read))
                    {
                        using (StreamReader reader =
                               new StreamReader(
                                   cryptoStream))
                        {
                            return reader.ReadToEnd();
                        }
                    }
                }
            }
        }
    }
}