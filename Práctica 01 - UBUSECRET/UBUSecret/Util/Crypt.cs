using System;
using System.Security.Cryptography;
namespace Util
{
    public class Crypt
    {
        public static string Encriptar(string password, string salt)
        {
            string code = password + salt;
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(code);
            SHA256 mySHA256 = SHA256.Create();
            bytes = mySHA256.ComputeHash(bytes);
            return (System.Text.Encoding.ASCII.GetString(bytes));
        }

        public static string CodificarB64(string t)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(t);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public static string DecodificarB64(string t)
        {
            try
            {
                var base64EncodedBytes = System.Convert.FromBase64String(t);
                return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
            }
            catch
            {
                return "Código Desconocido";
            }

        }
           
    }
}  
