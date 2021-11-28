using Microsoft.VisualStudio.TestTools.UnitTesting;
using Data;
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
            Assert.AreEqual(1, datos.LeerUsuarios().Count);
            Assert.AreEqual(pepe, datos.LeerUsuarios()[0]);
        }
    }
}
