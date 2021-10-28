using System;
using System.Collections.Generic;
using Util;



namespace UBUSecret
{

    public enum Rol {Administrador = 1, Usuario = 2, Deshabilitado =3 }

    public class Usuario
    {
        private static int IDSEQ = 0;
        int id;

        private string nombre;
        private string apellidos;
        private string email;
        private Rol rol;
        private string telefono;

        private string passwordHash;
        private string previousPasswordHash;

        private string salt;

        public int Id { get => id;}
        public string Nombre { get => nombre; set { Validar.String(value); nombre = value; } }
        public string Apellidos { get => apellidos; set { Validar.String(value); apellidos = value; } }
        public string Email { get => email; set { Validar.String(value); email = value; } }
        public Rol Rol { get => rol; set => rol = value; }
        public string Telefono { get => telefono; set { Validar.String(value); telefono = value; } }

        public Usuario(string nombre, string apellidos, string email, string telefono, string password) {
            this.id = IDSEQ++;

            this.Nombre  =  nombre;
            this.Apellidos  =  apellidos;




            Validar.Email(email);
            
            this.Email  =  email;
            this.Telefono = telefono;
            this.Rol   =   Rol.Deshabilitado;

            this.salt = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ");

            this.previousPasswordHash = null;

            

            Validar.String(password);
            this.passwordHash = Crypt.Encriptar(password, this.salt);
            
        }

        public bool EsValido()
        {
       
            return previousPasswordHash != null && Rol != Rol.Deshabilitado;
        }


        public bool ComprobarPassword(string password){

            if (password == null)
            {
                return false;
            }

            return this.passwordHash.Equals(Crypt.Encriptar(password, this.salt));
        }

        public bool CambiarPassword(string vieja, string nueva1, string nueva2)
        {   

            if(vieja == null)
            {
                return false;
            }
            if (nueva1 == null || nueva1.Length == 0)
            {
                return false;
            }


            if (!nueva1.Equals(nueva2))
            {
                return false;
            }


            if (!passwordHash.Equals(Crypt.Encriptar(vieja,
                salt)))
            {
                
                return false;
            }

            previousPasswordHash = this.passwordHash;
            passwordHash = Util.Crypt.Encriptar(nueva1, this.salt.ToString());
            return true;
        }

        public override bool Equals(object obj)
        {
            return obj is Usuario usuario &&
                   id == usuario.id &&
                   nombre == usuario.nombre &&
                   apellidos == usuario.apellidos &&
                   email == usuario.email &&
                   rol == usuario.rol &&
                   telefono == usuario.telefono &&
                   passwordHash == usuario.passwordHash &&
                   previousPasswordHash == usuario.previousPasswordHash &&
                   salt == usuario.salt;
        }



        public override string ToString()
        {
            return String.Format("Id: {0}, Nombre: {1}, app: {2}, email: {3}, rol:{4}, tfno: {5}", this.id, this.nombre, this.apellidos, this.email, this.rol, this.telefono);
        }

        public override int GetHashCode()
        {
            int hashCode = -354207900;
            hashCode = hashCode * -1521134295 + id.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(nombre);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(apellidos);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(email);
            hashCode = hashCode * -1521134295 + rol.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(telefono);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(passwordHash);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(previousPasswordHash);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(salt);
            return hashCode;
        }
    }
}
