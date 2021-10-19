using System;
using System.Text.RegularExpressions;
namespace Util
{
    public class Validar
    {
        /**
         * Valida un email.
         */
        public static bool Email(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return true;
            }catch(FormatException e)
            {
                return false;
            }
            catch (Exception e)
            {
                return false;
            }

        }

         /**
         * Valida un email.
         * 
         * <exception cref="PhoneFormatException"/>
         */
        public static bool Telefono(string telefono)
        {

            if (telefono == null || telefono.Length < 9)
            {
                throw new PhoneFormatException();
            }

            return true;
        }


        /**
        * Valida que una cadena no sea nula o tenga una longitud de 0. 
        * 
        * <exception cref="ArgumentException"/>
        */
        public static bool String(string s)
        {
            if (s == null || s.Length == 0)
            {
                throw new ArgumentException();
            }

            return true;
        }

        /**
         * Valida que una contraseña
         * - Tenga al menos 6 carácteres
         * - Tenga al menos una mayúscula
         * - Tenga al menos una minúscula
         * - Tenga al menos un número.
         */
        public static bool Password(string p)
        {
            if (p == null || p.Length < 6)
            {
                return false;
            }

            Regex r = new Regex(@"^.*(?=.*[a-z])(?=.*\d)(?=.*[A-Z])");
            return r.IsMatch(p);
        }
    }
}
