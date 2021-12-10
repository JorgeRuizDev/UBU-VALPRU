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
                try
                {
                    tblUsuarios.Add(usuario.Id, usuario);

                }
                catch (ArgumentException)
                {
                    return false;
                }catch(Exception e)
                {
                    throw e;
                }
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

            var usuarios = new List<Usuario>();

            foreach (var usuario in tblUsuarios.Values)
            {
                if (usuario.EsInactivo())
                {
                    usuarios.Add(usuario);
                }
            }
            return usuarios;
        }
        /*

         
         */


        public List<Usuario> LeerUsuariosActivos()
        {
            var usuarios = new List<Usuario>();

            foreach (var usuario in tblUsuarios.Values)
            {
                if (usuario.EsValido())
                {
                    usuarios.Add(usuario);
                }
            }
            return usuarios;
        }

        
        public Secreto BorrarSecreto(int idSecreto)
        {
            foreach (var secreto in tblSecretos.Values)
            {
                if (secreto.IdSecreto == idSecreto)
                {
                    tblSecretos.Remove(idSecreto);
                    return secreto;
                }
            }
            return null;
        }

        public bool InsertarSecreto(Secreto secreto)
        {
            if (secreto != null)
            {
                try
                {
                    tblSecretos.Add(secreto.IdSecreto, secreto);

                }
                catch (ArgumentException)
                {
                    return false;
                }
                catch (Exception e)
                {
                    throw e;
                }
                return true;
            }

            return false;
        }

        /*

         
         */


        public Secreto LeerSecreto(int idSecreto)
        {
            foreach (var secreto in tblSecretos.Values)
            {
                if (secreto.IdSecreto.Equals(idSecreto))
                {
                    return secreto;
                }
            }
            return null;
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
                if (secreto.Remitente==usuario)
                {
                    secretos.Add(secreto);
                }
            }
            return secretos;
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
