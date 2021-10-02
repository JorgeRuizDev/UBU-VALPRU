using System;

namespace Util
{
    public class Validar
    {
        /**
         * Valida un email.
         * 
         * <exception cref="FormatException"/>
         * <exception cref="ArgumentNullException"/>
         */
        public static bool Email(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }catch(FormatException e)
            {
                throw e;
            }
            catch (ArgumentNullException e)
            {
                throw e;
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
    }
}
