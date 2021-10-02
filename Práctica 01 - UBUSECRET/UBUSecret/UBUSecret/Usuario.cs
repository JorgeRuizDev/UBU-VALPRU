﻿using System;
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

        public bool CambiarPassword(string password)
        {
            if (password == null || password.Length == 0)
            {
                return false;
            }

            this.previousPasswordHash = this.passwordHash;
            this.passwordHash = Util.Crypt.Encriptar(password, this.salt.ToString());
            return true;
        }

        
    }
}
