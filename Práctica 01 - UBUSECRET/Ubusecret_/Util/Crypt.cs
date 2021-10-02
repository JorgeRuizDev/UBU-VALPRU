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
    }
}
