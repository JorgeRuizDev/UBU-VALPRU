using System;
using Util;



namespace UBUSecret
{

    public enum Rol {Administrador, Usuario, Deshabilitado }


    

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

        public bool CambiarPassword(string prev1, string prev2, string password)
        {
            if (prev1 == null || !prev1.Equals(prev2))
            {
                return false;
            }

            if (password == null || password.Length == 0)
            {
                return false;
            }

            if (previousPasswordHash != Util.Crypt.Encriptar(prev1, salt.ToString()))
            {
                return false;
            }

            this.previousPasswordHash = this.passwordHash;
            this.passwordHash = Util.Crypt.Encriptar(password, this.salt.ToString());
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

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(id);
            hash.Add(nombre);
            hash.Add(apellidos);
            hash.Add(email);
            hash.Add(rol);
            hash.Add(telefono);
            hash.Add(passwordHash);
            hash.Add(previousPasswordHash);
            hash.Add(salt);
            return hash.ToHashCode();
        }

        public override string ToString()
        {
            return String.Format("Id: {0}, Nombre: {1}, app: {2}, email: {3}, rol:{4}, tfno: {5}", this.id, this.nombre, this.apellidos, this.email, this.rol, this.telefono);
        }
    }
}
