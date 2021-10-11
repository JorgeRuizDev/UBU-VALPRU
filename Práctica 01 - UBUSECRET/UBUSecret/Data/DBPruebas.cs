using System;
using System.Collections.Generic;
using Interfaces;
using UBUSecret;

namespace Data
{
    public class DBPruebas: ICapaDatos
    {
        private SortedList<int, Usuario> tblUsuarios = new SortedList<int, Usuario>();
        private SortedList<int, Secreto> tblSecretos = new SortedList<int, Secreto>();

        private static DBPruebas instancia = new DBPruebas();

        private DBPruebas()
        {
            
        }

        public void Reset()
        {
            this.tblUsuarios = new SortedList<int, Usuario>();
            this.tblSecretos = new SortedList<int, Secreto>();
        }

        public static DBPruebas ObtenerInstacia()
        {
            return instancia;
        }

        public Usuario LeerUsuario(int id) {
            return tblUsuarios[id];
        }

        public Usuario LeerUsuario(string email)
        {
            if (email == null)
            {
                return null;
            }

            foreach(var usuario in tblUsuarios.Values)
            {
                if (usuario.Email.Equals(email))
                {
                    return usuario;
                }
            }
            return null;
        }

        public Usuario BorrarUsuario(string email)
        {
            Usuario usuario = LeerUsuario(email);

            if (usuario == null)
            {
                return null;
            }

            return tblUsuarios.Remove(usuario.Id) ? usuario : null;
        }

        public bool InsertarUsuario(Usuario usuario)
        {
            if (usuario != null)
            {
                tblUsuarios.Add(usuario.Id, usuario);
                return true;
            }

            return false;
        }

        public List<Usuario> LeerUsuariosDeshabilitados()
        {
            var usuariosDeshabilitados = new List<Usuario>();
            
            foreach(var usuario in tblUsuarios.Values)
            {
                if (usuario.Rol == Rol.Deshabilitado)
                {
                    usuariosDeshabilitados.Add(usuario);
                }
            }
            return usuariosDeshabilitados;
        }
        

        public Secreto BorrarSecreto(int idSecreto) {

            Console.WriteLine(tblSecretos.Count);

            if (tblSecretos.ContainsKey(idSecreto))
            {
                Secreto secreto = tblSecretos[idSecreto];
                tblSecretos.Remove(idSecreto);

                Console.WriteLine(tblSecretos.Count);

                return secreto;
            }
            
            return null;
        }
        public bool InsertarSecreto(Secreto secreto)
        {
            if (secreto == null)
            {
                return false;
            }

            tblSecretos.Add(secreto.IdSecreto, secreto);

            return true;
        }

        public List<Secreto> LeerSecretosRecibidos(Usuario usuario)
        {
            List<Secreto> secretos = new List<Secreto>();

            foreach (var secreto in tblSecretos.Values)
            {

                if (secreto.Receptores.Contains(usuario))
                {
                    secretos.Add(secreto);
                }
            }

            return secretos;
        }

        public List<Secreto> LeerSecretosEnviados(Usuario usuario)
        {
            List<Secreto> secretos = new List<Secreto>();

            foreach (var secreto in tblSecretos.Values)
            {
                if (secreto.Remitente.Equals(usuario))
                {
                    secretos.Add(secreto);
                }
            }

            return secretos;
        }
    }
}
