using Microsoft.VisualStudio.TestTools.UnitTesting;
using Data;
using Interfaces;
using UBUSecret;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Tests
{



    [TestClass()]
    public class DBPruebasTests
    {

        private ICapaDatos bd = DBPruebas.ObtenerInstacia();

        [TestInitialize]
        public void Setup()
        {
            bd.Reset();
        }

        [TestMethod()]
        public void ObtenerInstaciaTest()
        {
            Assert.IsNotNull(bd);
        }

        [TestMethod()]
        public void ResetTest()
        {
            InsertarUsuarioTest();
            bd.Reset();
            Assert.IsNull(bd.LeerUsuario("pablo@ubu.es"));

        }

        private Usuario InsertarPablo()
        {
            var pablo = new Usuario("Pablo", "Pablito", "pablo@ubu.es", "123456789", "HolaCaracola123.");
            bd.InsertarUsuario(pablo);
            return pablo;
        }

        [TestMethod()]
        public void InsertarUsuarioTest()
        {
            Assert.IsNull(bd.LeerUsuario(""));
            Assert.IsNull(bd.LeerUsuario(null));
            Assert.IsNull(bd.LeerUsuario("pablo@ubu.es"));
            Usuario pablo = InsertarPablo();
            Assert.AreEqual(pablo, bd.LeerUsuario("pablo@ubu.es"));
        }


        [TestMethod()]
        public void LeerUsuarioEmailTest()
        {
            InsertarUsuarioTest();
            Assert.AreEqual("pablo@ubu.es", bd.LeerUsuario("pablo@ubu.es").Email);
        }

        [TestMethod()]
        public void LeerUsuarioIDTest()
        {
            var pedro = new Usuario("Pedro", "Picapiedra", "pedro@ubu.es", "123456789", "HolaCaracola1234.");
            bd.InsertarUsuario(pedro);
            Assert.AreEqual(pedro, bd.LeerUsuario(pedro.Id));
        }

        [TestMethod()]
        public void BorrarUsuarioTest()
        {

            string email = "pablo@ubu.es";
            Assert.IsNull(bd.BorrarUsuario(email));
            Assert.IsNull(bd.BorrarUsuario(null));
            var pablo = InsertarPablo();
            Assert.AreEqual(pablo, bd.LeerUsuario(email));
            Assert.AreEqual(pablo, bd.BorrarUsuario(email));
            Assert.IsNull(bd.LeerUsuario(email));

        }


        [TestMethod()]
        public void LeerUsuariosDeshabilitadosTest()
        {
            Assert.AreEqual(0, bd.LeerUsuariosDeshabilitados().Count);

            Usuario pablo = InsertarPablo();

            Assert.AreEqual(1, bd.LeerUsuariosDeshabilitados().Count);
            Assert.AreEqual(pablo, bd.LeerUsuariosDeshabilitados()[0]);
            pablo.Rol = Rol.Usuario;

            Assert.AreEqual(0, bd.LeerUsuariosDeshabilitados().Count);

        }

        [TestMethod()]
        public void BorrarSecretoTest()
        {
            Assert.IsNull(bd.BorrarSecreto(999));
            (var secreto, var remitente, var receptores) = InsertarSecreto();



            Assert.AreEqual(1, bd.LeerSecretosEnviados(remitente).Count);
            Assert.AreEqual(1, bd.LeerSecretosRecibidos(receptores.First.Value).Count);


            Assert.AreEqual(secreto, bd.BorrarSecreto(secreto.IdSecreto));

            Assert.AreEqual(0, bd.LeerSecretosEnviados(remitente).Count);
            Assert.AreEqual(0, bd.LeerSecretosEnviados(receptores.First.Value).Count);

        }

        private (Secreto, Usuario, LinkedList<Usuario>) InsertarSecreto()
        {
            Usuario remitente = new Usuario("Paco", "Paquito", "paco@ubu.es", "123456789", "HolaCaracola1234.");
            Usuario receptor = new Usuario("Manolo", "Manolón", "manolo@ubu.es", "123456789", "HolaCaracola1234.");

            var receptores = new LinkedList<Usuario>();
            receptores.AddLast(receptor);

            Secreto secreto = new Secreto(remitente, receptores, "Este es un secreto de prueba", "Prueba", "Manolón");
            bd.InsertarSecreto(secreto);
            return (secreto, remitente, receptores);

        }

        [TestMethod()]
        public void InsertarSecretoTest()
        {

            Usuario remitente = new Usuario("Paco", "Paquito", "paco@ubu.es", "123456789", "HolaCaracola1234.");
            Usuario receptor = new Usuario("Manolo", "Manolón", "manolo@ubu.es", "123456789", "HolaCaracola1234.");

            Assert.AreEqual(bd.LeerSecretosEnviados(remitente).Count, 0);
            Assert.AreEqual(bd.LeerSecretosRecibidos(receptor).Count, 0);

            var receptores = new LinkedList<Usuario>();
            receptores.AddLast(receptor);

            Secreto secreto = new Secreto(remitente, receptores, "Este es un secreto de prueba", "Prueba", "Manolón");

            Assert.IsTrue(bd.InsertarSecreto(secreto));

            Assert.AreEqual(bd.LeerSecretosEnviados(remitente).Count, 1);
            Assert.AreEqual(bd.LeerSecretosRecibidos(receptor).Count, 1);
        }

        [TestMethod()]
        public void LeerSecretosRecibidosTest()
        {
            Usuario remitente = new Usuario("Paco", "Paquito", "paco@ubu.es", "123456789", "HolaCaracola1234.");
            Usuario receptor = new Usuario("Manolo", "Manolón", "manolo@ubu.es", "123456789", "HolaCaracola1234.");

            Assert.AreEqual(bd.LeerSecretosEnviados(remitente).Count, 0);
            Assert.AreEqual(bd.LeerSecretosRecibidos(receptor).Count, 0);

            var receptores = new LinkedList<Usuario>();
            receptores.AddLast(receptor);

            Secreto secreto = new Secreto(remitente, receptores, "Este es un secreto de prueba", "Prueba", "Manolón");

            Assert.IsTrue(bd.InsertarSecreto(secreto));

            Assert.AreEqual(bd.LeerSecretosEnviados(remitente).Count, 1);
            Assert.AreEqual(bd.LeerSecretosRecibidos(receptor).Count, 1);

            Assert.AreEqual(secreto, bd.LeerSecretosRecibidos(receptor)[0]);
        }

        [TestMethod()]
        public void LeerSecretosEnviadosTest()
        {
            Usuario remitente = new Usuario("Paco", "Paquito", "paco@ubu.es", "123456789", "HolaCaracola1234.");
            Usuario receptor = new Usuario("Manolo", "Manolón", "manolo@ubu.es", "123456789", "HolaCaracola1234.");

            Assert.AreEqual(bd.LeerSecretosEnviados(remitente).Count, 0);
            Assert.AreEqual(bd.LeerSecretosRecibidos(receptor).Count, 0);

            var receptores = new LinkedList<Usuario>();
            receptores.AddLast(receptor);

            Secreto secreto = new Secreto(remitente, receptores, "Este es un secreto de prueba", "Prueba", "Manolón");

            Assert.IsTrue(bd.InsertarSecreto(secreto));

            Assert.AreEqual(bd.LeerSecretosEnviados(remitente).Count, 1);
            Assert.AreEqual(bd.LeerSecretosRecibidos(receptor).Count, 1);

            Assert.AreEqual(secreto, bd.LeerSecretosEnviados(remitente)[0]);
        }

        [TestMethod()]
        public void LeerUsuariosTest()
        {
            InsertarPablo();
            Assert.AreEqual(1, bd.LeerUsuarios().Count);
        }

        [TestMethod()]
        public void ResetLogsTest()
        {
            //Añadimos un log
            AñadirLogTest();

            //Reseteamos los Log
            bd.ResetLogs();

            //Comprobamos que esta vacia
            Assert.Equals(bd.LeerLogs().Count, 0);
        }

        [TestMethod()]
        public void LeerLogsTest()
        {
            //Añadimos un log
            AñadirLogTest();

            //Leemos los log
            List<Log> lista = bd.LeerLogs();

            //Comprobamos que la lista sea la correspondiente a los log
            Assert.Equals(lista.Count, 1);

        }

        [TestMethod()]
        public void AñadirLogTest()
        {
            //Comprobamos que esta vacia
            Assert.Equals(bd.LeerLogs().Count, 0);

            //Añadimos el nuevo log
            Logger.Log("Prueba", Level.ERROR);

            //Comprobamos que contiene el log
            Assert.Equals(bd.LeerLogs().Count, 1);

        }
    }
}