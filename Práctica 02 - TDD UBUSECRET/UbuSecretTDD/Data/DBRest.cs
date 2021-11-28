using System;
using System.Collections.Generic;
using Interfaces;
using UBUSecret;

namespace Data
{
    public class DBRest: ICapaDatos
    {

        private SortedList<int, Usuario> tblUsuarios = new SortedList<int, Usuario>();
        private SortedList<int, Secreto> tblSecretos = new SortedList<int, Secreto>();

        private static DBRest instancia = new DBRest();
        private DBRest()
        {
            
        }

        public static ICapaDatos ObtenerInstancia()
        {
            return instancia;
        }


        public void Reset()
        {
            tblUsuarios = new SortedList<int, Usuario>();
            tblSecretos = new SortedList<int, Secreto>();
        }

        public Usuario LeerUsuario(string email)
        {
            if (email == null)
            {
                return null;
            }

            foreach (var usuario in tblUsuarios.Values)
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public List<Usuario> LeerUsuariosInactivos()
        {
            throw new NotImplementedException();
        }

        public List<Usuario> LeerUsuariosActivos()
        {
            throw new NotImplementedException();
        }

        public Secreto BorrarSecreto(int idSecreto)
        {
            throw new NotImplementedException();
        }

        public bool InsertarSecreto(Secreto secreto)
        {
            throw new NotImplementedException();
        }

        public Secreto LeerSecreto(int idSecreto)
        {
            throw new NotImplementedException();
        }

        public List<Secreto> LeerSecretosRecibidos(Usuario usuario)
        {
            throw new NotImplementedException();
        }

        public List<Secreto> LeerSecretosEnviados(Usuario usuario)
        {
            throw new NotImplementedException();
        }

        public List<Usuario> LeerUsuarios()
        {
            var usuarios = new List<Usuario>();

            foreach (var usuario in tblUsuarios.Values)
            {
                usuarios.Add(usuario);
            }
            return usuarios;
        }
    }
}
