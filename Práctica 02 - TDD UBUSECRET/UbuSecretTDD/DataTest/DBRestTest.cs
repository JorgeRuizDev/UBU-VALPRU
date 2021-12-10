using Microsoft.VisualStudio.TestTools.UnitTesting;
using Data;
using System.Collections.Generic;
using Interfaces;
using UBUSecret;

namespace DataTest
{
    [TestClass]
    public class DBRestTest
    {
        ICapaDatos datos = DBRest.ObtenerInstancia();
        Usuario pepe = null;
        Usuario paco = null;
        Secreto secreto = null;
        Secreto secretoDos = null;


        [TestInitialize]
        public void Reset()
        {
            datos.Reset();
            pepe = CrearUsuario("Pepe", "Pepe", "pepe@ubusecret.es");
            paco = CrearUsuario("Paco", "Paco", "paco@ubusecret.es");
            LinkedList<Usuario> receptores = new LinkedList<Usuario>();
            receptores.AddLast(paco);
            secreto = new Secreto(pepe, receptores, "prueba", "hola", "pepito");
            secretoDos = new Secreto(pepe, receptores, "dos", "dos", "pep");
        }

        [TestMethod]
        public void SingletonTest()
        {
            Assert.IsNotNull(datos);
        }

        public Usuario CrearUsuario(string nombre, string apellidos, string email)
        {
            return new Usuario(nombre, apellidos, email, "123456789", "Usuario1234");
        }
        
        [TestMethod]
        public void RestTest()
        {
            LeerUsuariosTest();
            datos.Reset();
            Assert.AreEqual(0, datos.LeerUsuarios().Count);
            
            
            int cont = 0;
            foreach (var usuario in datos.LeerUsuarios()) 
            {
                cont += datos.LeerSecretosEnviados(usuario).Capacity;
            }
            Assert.AreEqual(0, cont);

        }

        [TestMethod]
        public void InsertarUsuarioTest()
        {        
            Assert.IsNull(datos.LeerUsuario("paco@ubusecret.es"));
            Assert.IsNotNull(paco);
            Assert.IsTrue(datos.InsertarUsuario(paco));
            Assert.AreEqual(paco, datos.LeerUsuario("paco@ubusecret.es"));
            Assert.IsFalse(datos.InsertarUsuario(paco));

        }

        [TestMethod]
        public void LeerUsuarioTest()
        {
            Assert.IsNull(datos.LeerUsuario("pepe@ubusecret.es"));
            Assert.IsNotNull(pepe);
            Assert.IsTrue(datos.InsertarUsuario(pepe));
            Assert.AreEqual(pepe, datos.LeerUsuario("pepe@ubusecret.es"));
        }

        [TestMethod]
        public void LeerUsuariosTest()
        {
            Assert.AreEqual(0, datos.LeerUsuarios().Count);
            Assert.IsNotNull(pepe);
            Assert.IsTrue(datos.InsertarUsuario(pepe));
            Assert.IsTrue(datos.InsertarUsuario(paco));
            var usuarios = datos.LeerUsuarios();
            Assert.AreEqual(2, usuarios.Count);
            Assert.IsTrue(usuarios.Contains(pepe));
            Assert.IsTrue(usuarios.Contains(paco));
        }

        [TestMethod]
        public void ListarUsuariosActivosTest()
        {
            pepe.CambiarPassword("Usuario1234", "Usuario1234_", "Usuario1234_");
            paco.CambiarPassword("Usuario1234", "Usuario1234_", "Usuario1234_");
            Assert.AreEqual(0, datos.LeerUsuariosActivos().Count);

            Assert.IsTrue(datos.InsertarUsuario(pepe));
            Assert.AreEqual(1, datos.LeerUsuarios().Count);
            Assert.AreEqual(0, datos.LeerUsuariosActivos().Count);

            // Aprovechamos que no tenemos método UpdateUser()
            pepe.Rol = Rol.Administrador;

            
            Assert.AreEqual(1, datos.LeerUsuariosActivos().Count);

            paco.Rol = Rol.Usuario;

            Assert.IsTrue(datos.InsertarUsuario(paco));

            var usuarios = datos.LeerUsuariosActivos();

            Assert.AreEqual(2, usuarios.Count);

            Assert.IsTrue(usuarios.Contains(pepe));
            Assert.IsTrue(usuarios.Contains(paco));
        }

        [TestMethod]
        public void ListarUsuariosInactivosTest()
        {

            Assert.AreEqual(0, datos.LeerUsuariosInactivos().Count);

            Assert.IsTrue(datos.InsertarUsuario(pepe));
            Assert.IsTrue(datos.InsertarUsuario(paco));

            Assert.AreEqual(2, datos.LeerUsuariosInactivos().Count);

            paco.CambiarPassword("Usuario1234", "Usuario1234_", "Usuario1234_");
            pepe.Rol = Rol.Administrador;

            Assert.AreEqual(2, datos.LeerUsuariosInactivos().Count);

            pepe.CambiarPassword("Usuario1234", "Usuario1234_", "Usuario1234_");
            paco.Rol = Rol.Administrador;

            var usuarios = datos.LeerUsuariosInactivos();

            Assert.AreEqual(0, usuarios.Count);

            // Bloqueamos a ambos usuarios
            pepe.Rol = Rol.Bloqueado;
            paco.Rol = Rol.Bloqueado;

            Assert.AreEqual(2, datos.LeerUsuariosInactivos().Count);

        }

        [TestMethod]
        public void InsertarSecretoTest()
        {
            Assert.IsNull(datos.LeerSecreto(secreto.IdSecreto));
            Assert.IsTrue(datos.InsertarSecreto(secreto));
            Assert.AreEqual(datos.LeerSecreto(secreto.IdSecreto), secreto);
            Assert.IsFalse(datos.InsertarSecreto(secreto));
        }


        [TestMethod]
        public void LeerSecretoTest()
        {
            Assert.IsNull(datos.LeerSecreto(secretoDos.IdSecreto));
            Assert.IsNotNull(secreto);
            Assert.IsTrue(datos.InsertarSecreto(secretoDos));
            Assert.AreEqual(secretoDos, datos.LeerSecreto(secretoDos.IdSecreto));

        }


        [TestMethod]
        public void BorrarSecretoTest()
        {
            LeerSecretoTest();
            Assert.IsNotNull(datos.LeerSecreto(secretoDos.IdSecreto));
            Assert.AreEqual(secretoDos, datos.BorrarSecreto(secretoDos.IdSecreto));
            Assert.IsNull(datos.LeerSecreto(secretoDos.IdSecreto));
            Assert.IsNull(datos.BorrarSecreto(secretoDos.IdSecreto));
        }


        [TestMethod]
        public void LeerSecretosRecibidosTest()
        {
            LeerSecretoTest();
            Assert.AreEqual(1, datos.LeerSecretosRecibidos(paco).Count);
            Assert.IsTrue(datos.LeerSecretosRecibidos(paco).Contains(secretoDos));
            datos.BorrarSecreto(secretoDos.IdSecreto);
            Assert.AreEqual(0, datos.LeerSecretosRecibidos(paco).Count);
            Assert.AreEqual(0, datos.LeerSecretosRecibidos(pepe).Count);
        }


        [TestMethod]
        public void LeerSecretosEnviadosTest()
        {
            LeerSecretoTest();
            InsertarSecretoTest();
            Assert.AreEqual(2, datos.LeerSecretosEnviados(pepe).Count);
            Assert.IsTrue(datos.LeerSecretosEnviados(pepe).Contains(secretoDos));
            Assert.IsTrue(datos.LeerSecretosEnviados(pepe).Contains(secreto));
            datos.BorrarSecreto(secretoDos.IdSecreto);
            datos.BorrarSecreto(secreto.IdSecreto);
            Assert.AreEqual(0, datos.LeerSecretosEnviados(paco).Count);
            Assert.AreEqual(0, datos.LeerSecretosEnviados(pepe).Count);

        }


    }
}
