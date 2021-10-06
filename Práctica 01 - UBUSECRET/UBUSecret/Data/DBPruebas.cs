using System;
using System.Collections.Generic;
using Interfaces;
using UBUSecret;

namespace Data
{
    public class DBPruebas: ICapaDatos
    {
        private SortedList<int, Usuario> tblUsuarios = new SortedList<int, Usuario>();
        private SortedList<string, Secreto> tblSecretos = new SortedList<string, Secreto>();

        public Usuario LeerUsuario(int id) {
            return tblUsuarios[id];
        }
        public Usuario LeerUsuario(string email);

        public Usuario BorrarUsuario(string email);

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

        public Secreto BorrarSecreto(string idSecreto) {

            tblSecretos[idSecreto] = null;
        }
        public bool InsertarSecreto(Secreto secreto);

        public List<Secreto> LeerSecretosRecibidos(Usuario usuario);
        public List<Secreto> LeerSecretosEnviados(Usuario usuario);
    }
}
