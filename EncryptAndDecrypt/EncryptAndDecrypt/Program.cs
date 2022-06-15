using System;
using System.Security.Cryptography;
using System.Text;

namespace EncryptAndDecrypt
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var program = new Program();
            var inputText = string.Empty;
            Console.WriteLine("Input your text here: ");
            inputText = Console.ReadLine();

            var encryptText = program.Encrypt(inputText);
            Console.WriteLine($"Your encrypt text is: {encryptText}");

            var decryptText = program.Decrypt(encryptText);
            Console.WriteLine($"Your decrypt text is: {decryptText}");
        }

        public string Encrypt(string text)
        {
            byte[] src = Encoding.UTF8.GetBytes(text);
            byte[] key = Encoding.ASCII.GetBytes("0123456789abcdef0123456789abcdef");
            RijndaelManaged aes = new RijndaelManaged();
            aes.Mode = CipherMode.ECB;
            aes.Padding = PaddingMode.PKCS7;
            aes.KeySize = 256;

            using (ICryptoTransform encrypt = aes.CreateEncryptor(key, null))
            {
                byte[] dest = encrypt.TransformFinalBlock(src, 0, src.Length);
                encrypt.Dispose();
                return Convert.ToBase64String(dest);
            }
        }

        public string Decrypt(string text)
        {
            byte[] src = Convert.FromBase64String(text);
            RijndaelManaged aes = new RijndaelManaged();
            byte[] key = Encoding.ASCII.GetBytes("0123456789abcdef0123456789abcdef");
            aes.KeySize = 256;
            aes.Padding = PaddingMode.PKCS7;
            aes.Mode = CipherMode.ECB;
            using (ICryptoTransform decrypt = aes.CreateDecryptor(key, null))
            {
                byte[] dest = decrypt.TransformFinalBlock(src, 0, src.Length);
                decrypt.Dispose();
                return Encoding.UTF8.GetString(dest);
            }
        }
    }
}
