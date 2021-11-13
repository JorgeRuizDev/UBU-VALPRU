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
        private SortedList<DateTime, Log> logs = new SortedList<DateTime, Log>();

        private static DBPruebas instancia = new DBPruebas();

        private DBPruebas()
        {
            Setup();
        }

        private void Setup()
        {
            Usuario paco = new Usuario("Paco", "González", "paco@ubusecret.es", "123456789", "Paco1122");
            Usuario pepe = new Usuario("Pepe", "Pepe", "pepe@ubusecret.es", "123456789", "Pepe11");
            Usuario juan = new Usuario("Juan", "Carlos", "juan@ubusecret.es", "123456788", "Juan11");
            Usuario gestor = new Usuario("Gestor", "Ubusecret", "gestor@ubusecret.es", "123456789", "Gestor11");

            var receptores = new LinkedList<Usuario>();
            receptores.AddLast(paco);
            receptores.AddLast(pepe);
            receptores.AddLast(juan);

            var secreto = new Secreto(gestor, receptores, "Hola este es el mensaje", "Un secreto más", "No soy el gestor");

            // Cambio de contraseñas
            gestor.CambiarPassword("Gestor11", "Gestor1122", "Gestor1122");
            juan.CambiarPassword("Juan11", "Juan1122", "Juan1122");

            // Cambiar Roles
            gestor.Rol = Rol.Administrador;
            juan.Rol = Rol.Usuario;

            InsertarUsuario(gestor);
            InsertarUsuario(paco);
            InsertarUsuario(pepe);
            InsertarUsuario(juan);
            InsertarSecreto(secreto);
        }

        /**
         Vacía la instancia actual de la base de datos
         */
        public void Reset()
        {
            tblUsuarios = new SortedList<int, Usuario>();
            tblSecretos = new SortedList<int, Secreto>();
        }

        /**
         Resetea la base de datos con los datos por defecto. 
         */
        public ICapaDatos SoftReset()
        {
            Reset();
            Setup();
            return ObtenerInstacia();
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

        public List<Usuario> LeerUsuarios()
        {
            var usuarios = new List<Usuario>();

            foreach (var usuario in tblUsuarios.Values)
            {
                    usuarios.Add(usuario);
            }
            return usuarios;
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

        public void AñadirLog(Log Log) {
            logs.Add(Log.Timestamp,Log);
        }

        public List<Log> LeerLogs()
        {
            List<Log> list = new List<Log>();

            foreach (Log log in logs.Values)
            {
                list.Add(log);
            }

            return list;
        }

        public void ResetLogs()
        {
            logs = new SortedList<DateTime, Log>();
        }



        public override string ToString()
        {
            return "Hay " + tblUsuarios.Count + " usuarios registrados y " + tblSecretos.Count + " secretos registrados.";
        }
    }
}
