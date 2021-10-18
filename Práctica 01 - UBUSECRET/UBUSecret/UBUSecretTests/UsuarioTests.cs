using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace UBUSecret.Tests
{
    [TestClass()]
    public class UsuarioTests
    {

        private Usuario usuario;

        [TestInitialize()]
        public void Inicializacion()
        {
            this.usuario = new Usuario("Pedro", "Piqueras", "pedro@ubu.es", "631 882 831", "1234");
        }


        [TestMethod()]
        // Instancia sin excepciones
        public void UsuarioTest()
        {
            new Usuario("Pedro", "Piqueras", "pedro@ubu.es", "631 882 831", "1234");
        }

            
        

        [TestMethod()]
        public void EsValidoTest()
        {
            Assert.IsFalse(usuario.EsValido());


            usuario.Rol = Rol.Usuario;
            Assert.IsFalse(usuario.EsValido());

            usuario.Rol = Rol.Usuario;
            Assert.IsTrue(usuario.CambiarPassword("1234", "1234", "contrasenia123"));

            Assert.IsTrue(usuario.EsValido());

        }


        [TestMethod()]
        public void ValidarEmail()
        {
            Usuario u1 = new Usuario("Pedro", "Piqueras", "pedro@ubu.es", "631 882 831", "1234");

            try
            {
                Usuario u2 = new Usuario("Pedro", "Piqueras", "pedro@ubu", "631 882 831", "1234");
            }catch(Exception e)
            {
                Assert.IsTrue(e is FormatException);
            }

            try
            {
                Usuario u2 = new Usuario("Pedro", "Piqueras", null, "631 882 831", "1234");
            }
            catch (Exception e)
            {
                Assert.IsTrue(e is ArgumentNullException);
            }

        }

        [TestMethod()]
        public void ValidarTelefonoTest()
        {
            Usuario u1 = new Usuario("Pedro", "Piqueras", "pedro@ubu", "631 631 631", "1234");
            try
            {
                Usuario u2 = new Usuario("Pedro", "Piqueras", "pedro@ubu", "631", "1234");
            }
            catch (Exception e)
            {
                Assert.IsTrue(e is FormatException);
            }
        }

        [TestMethod()]
        public void ComprobarPasswordTest()
        {

            Assert.IsTrue(usuario.ComprobarPassword("1234"));
            Assert.IsFalse(usuario.ComprobarPassword("1235"));

            Assert.IsFalse(usuario.ComprobarPassword(null));
        }

        [TestMethod()]
        public void CambiarPasswordTest()
        {
           
            Assert.IsFalse(usuario.CambiarPassword("", "", "hola"));

            Assert.IsTrue(usuario.ComprobarPassword("1234"));

            Assert.IsFalse(usuario.CambiarPassword("1234", "1234", null));
            Assert.IsTrue(usuario.ComprobarPassword("1234"));

            Assert.IsFalse(usuario.CambiarPassword(null, null, null));
            Assert.IsTrue(usuario.ComprobarPassword("1234"));

            Assert.IsTrue(usuario.CambiarPassword("1234", "1234", "hola"));
            Assert.IsTrue(usuario.ComprobarPassword("hola"));

        }

        [TestMethod()]
        public void CambiarRolTest()
        {
            // Comprueba el rol por defecto:
            Assert.AreEqual(usuario.Rol, Rol.Deshabilitado);

            // Prueba diferentes roles:
            usuario.Rol = Rol.Usuario;

            Assert.AreEqual(usuario.Rol, Rol.Usuario);

            usuario.Rol = Rol.Administrador;

            Assert.AreEqual(usuario.Rol, Rol.Administrador);

            usuario.Rol = Rol.Deshabilitado;
            Assert.AreEqual(usuario.Rol, Rol.Deshabilitado);


        }


        [TestMethod()]
        public void UsuarioNombreApellidos()
        {
            try
            {
                Usuario u1 = new Usuario("", "Piqueras", "pedro@ubu.es", "631 882 831", "1234");
            }
            catch (Exception e)
            {
                Assert.IsTrue(e is ArgumentException);
            }

            try
            {
                Usuario u1 = new Usuario("Paco", "", "pedro@ubu.es", "631 882 831", "1234");
            }
            catch (Exception e)
            {
                Assert.IsTrue(e is ArgumentException);
            }

            try
            {
                usuario.Telefono = null;
            }
            catch (Exception e)
            {
                Assert.IsTrue(e is ArgumentException);
            }

        }
    }
}