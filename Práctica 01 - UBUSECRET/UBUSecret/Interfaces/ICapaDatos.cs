using System;
using System.Collections.Generic;
using System.Text;
using UBUSecret;
namespace Interfaces
{
    public interface ICapaDatos
    {
        public Usuario LeerUsuario(Usuario usuario);
        public Usuario LeerUsuario(string email);

        public Usuario BorrarUsuario(string email);

        public bool InsertarUsuario(Usuario usuario);

        public List<Usuario> LeerUsuariosDeshabilitados();

        public Secreto BorrarSecreto(string idSecreto);
        public bool InsertarSecreto(Secreto secreto);

        public List<Secreto> LeerSecretosRecibidos(Usuario usuario);
        public List<Secreto> LeerSecretosEnviados(Usuario usuario);
            
    }
}
