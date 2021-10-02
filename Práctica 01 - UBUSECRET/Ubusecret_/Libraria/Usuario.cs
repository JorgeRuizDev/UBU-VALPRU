using System;
using Util;




namespace Libraria
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
        public string Nombre { get => nombre; set => nombre = value; }
        public string Apellidos { get => apellidos; set => apellidos = value; }
        public string Email { get => email; set => email = value; }
        public Rol Rol { get => rol; set => rol = value; }
        public string Telefono { get => telefono; set => telefono = value; }

        public Usuario(string nombre, string apellidos, string email, string telefono, string password) {
            this.id = crearId();
            this.Nombre  =  nombre;
            this.Apellidos  =  apellidos;
            this.Email  =  email;
            this.Telefono = telefono;
            this.Rol   =   Rol.Pendiente;

            this.salt = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ");

            this.previousPasswordHash = null;
            this.passwordHash = Crypt.Encriptar(password, this.salt);
            
        }

        public bool esValido()
        {
            return previousPasswordHash != null && Rol != Rol.Pendiente;
        }

        private int crearId()
        {
            int oldId = IDSEQ;
            IDSEQ++;
            return oldId;
        }

        public bool comprobarPassword(string password){
            return this.previousPasswordHash.Equals(Crypt.Encriptar(password, this.salt);
        }

        public bool cambiarPassword(string password)
        {
            if (password.Length == 0)
            {
                return false;
            }

            this.previousPasswordHash = this.passwordHash;
            this.passwordHash = Util.Crypt.Encriptar(password, this.Id.ToString());
            return true;
        }

        public bool cambiarRol(Rol rol)
        {
            if (rol == this.Rol)
            {
                return false;
            }

            this.Rol = rol;
            return true;
        }

        
    }
}
