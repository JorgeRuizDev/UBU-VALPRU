using System;
using System.Collections.Generic;
using System.Text;
using UBUSecret;
namespace Interfaces
{
    public interface ICapaDatos
    {
        public Usuario LeerUsuario(int id);
        public Usuario LeerUsuario(string email);

        public Usuario BorrarUsuario(string email);

        public bool InsertarUsuario(Usuario usuario);

        public List<Usuario> LeerUsuariosDeshabilitados();

        public Secreto BorrarSecreto(int idSecreto);
        public bool InsertarSecreto(Secreto secreto);

        public List<Secreto> LeerSecretosRecibidos(Usuario usuario);
        public List<Secreto> LeerSecretosEnviados(Usuario usuario);

        public void Reset();
    }
}
