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
        

        [TestInitialize]
        public void Reset()
        {
            datos.Reset();
            pepe = CrearUsuario("Pepe", "Pepe", "pepe@ubusecret.es");
            paco = CrearUsuario("Paco", "Paco", "paco@ubusecret.es");
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
        public void BorrarSecretoTest()
        {
            LinkedList<Usuario> receptores = new LinkedList<Usuario>();
            receptores.AddLast(paco);
            Secreto secreto = new Secreto(pepe,receptores,"prueba","hola","pepito");
            Assert.IsTrue(datos.InsertarSecreto(secreto));
            Assert.AreEqual(secreto,datos.BorrarSecreto(secreto.IdSecreto));
        }
        








        /*
        [TestMethod]
        public void InsertarSecretoTest()
        {


        }

        [TestMethod]
        public void LeerSecretoTest()
        {


        }

        [TestMethod]
        public void LeerSecretosRecibidosTest()
        {


        }

        [TestMethod]
        public void LeerSecretosEnviadosTest()
        {


        }

        */
    }
}
