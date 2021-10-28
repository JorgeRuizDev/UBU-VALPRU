using System;
using System.Collections.Generic;
using System.Text;
using UBUSecret;
namespace Interfaces
{
    public interface ICapaDatos
    {
        Usuario LeerUsuario(int id);
        Usuario LeerUsuario(string email);

        Usuario BorrarUsuario(string email);

        bool InsertarUsuario(Usuario usuario);

        List<Usuario> LeerUsuariosDeshabilitados();

        List<Usuario> LeerUsuarios();

        Secreto BorrarSecreto(int idSecreto);
        bool InsertarSecreto(Secreto secreto);

        List<Secreto> LeerSecretosRecibidos(Usuario usuario);
        List<Secreto> LeerSecretosEnviados(Usuario usuario);

        void Reset();
    }
}
