using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using UBUSecret;
using Newtonsoft.Json;
using Data;

namespace DataTest
{
    [TestClass()]
    public class RestTests
    {
        Rest api = new Rest();

        [TestInitialize]
        public void Reset()
        {
            api.Populate();
        }


        [TestMethod()]
        public void GetUserTest()
        {
            Assert.AreEqual("404", api.GetUsuario("juan@ubusecret.es"));

            string res = api.GetUsuario("paco@ubusecret.es");
            Usuario paco = JsonConvert.DeserializeObject<Usuario>(res);

            Assert.AreEqual(paco.Email, "paco@ubusecret.es");
            Assert.AreEqual(paco.Nombre, "Paco");
            Assert.AreEqual(paco.Apellidos, "Paco");
            Assert.AreEqual(paco.Rol, Rol.Deshabilitado);
            Assert.AreEqual(paco.Telefono, "123456789");
        }

        [TestMethod()]
        public Secreto GetSecretoTest()
        {
            Assert.AreEqual("404", api.GetSecreto(-1));

            string res = PutSecretoTest();
            
            Console.WriteLine(res);
            Secreto s = JsonConvert.DeserializeObject<Secreto>(res);
            string res2 = api.GetSecreto(s.IdSecreto);
            Assert.AreEqual(res, res2);
            Assert.AreEqual("pepe@ubusecret.es", s.RemitenteMail);
            Assert.AreEqual("Del Paquito", s.Alias);
            Assert.AreEqual("Un Título", s.Titulo);
            Assert.AreEqual("Este es el mensaje", s.Mensaje);
            Assert.IsTrue(s.Emails.Contains("paco@ubusecret.es"));
            Assert.IsTrue(s.Emails.Contains("pepe@ubusecret.es"));
            return s;
            // No comprobamos la fecha por que la desconocemos. 
        }

        [TestMethod()]
        public void PutUsuarioTest()
        {   

            string body = @"{
                'Nombre': 'Juan',
                'Apellidos': 'Juan',
                'Email': 'juan@ubusecret.es',
                'Telefono': '123456789',
                'Password': 'Abc1234',
            }";

            Assert.AreEqual("400", api.PutUsuario(""));

            string resJson = api.PutUsuario(body);
            Usuario res = JsonConvert.DeserializeObject<Usuario>(resJson);

            Assert.AreEqual("juan@ubusecret.es", res.Email);
            Assert.AreEqual("Juan", res.Nombre);

            // Comprobamos que esté almacenado
            Assert.AreNotEqual("404", api.GetUsuario("juan@ubusecret.es"));
        }

        [TestMethod()]
        public void GetDisabledTest()
        {
            string resJson = api.GetDesactivados();

            List<Usuario> res = JsonConvert.DeserializeObject<List<Usuario>>(resJson);

            foreach(var u in res)
            {
                Assert.IsTrue(u.Rol == Rol.Deshabilitado || u.Rol == Rol.Bloqueado);
            }
        }

        [TestMethod()]
        public void GetActiveTest()
        {
            string resJson = api.GetActivados();

            List<Usuario> res = JsonConvert.DeserializeObject<List<Usuario>>(resJson);
            
            foreach (var u in res)
            {
                Assert.IsTrue(u.Rol == Rol.Administrador || u.Rol == Rol.Usuario);
            }
        }


        [TestMethod()]
        public void GetUnauthorisedTest()
        {
            string resJson = api.GetDesautorizados();

            List<Usuario> res = JsonConvert.DeserializeObject<List<Usuario>>(resJson);

            foreach (var u in res)
            {
                Assert.IsTrue(u.Rol == Rol.Deshabilitado);
            }
        }

        [TestMethod()]
        public string PutSecretoTest()
        {
            string body = @"{
                'Receptores': ['pepe@ubusecret.es', 'paco@ubusecret.es'],
                'Mensaje': 'Este es el mensaje',
                'Titulo': 'Un Título',
                'Alias': 'Del Paquito'
            }";

            Assert.AreEqual("400", api.PutSecreto("", body));
            Assert.AreEqual("400", api.PutSecreto("paco@ubusecret.es", ""));
             
            string resJson = api.PutSecreto("pepe@ubusecret.es", body);
            Secreto res  = JsonConvert.DeserializeObject<Secreto>(resJson);

            Assert.AreEqual("Un Título", res.Titulo);
            Assert.AreEqual("Del Paquito", res.Alias);
            Assert.AreEqual(2, res.Emails.Count);
            Assert.AreNotEqual("404", api.GetSecreto(res.IdSecreto));

            return api.GetSecreto(res.IdSecreto);
        }


        [TestMethod()]
        public void DeleteSecretoTest()
        {
            Secreto insertado = GetSecretoTest();

            
            Assert.AreEqual("404", api.DeleteSecreto(-1));
            
            string res = api.DeleteSecreto(insertado.IdSecreto);


            Console.WriteLine(res);
            Secreto s = JsonConvert.DeserializeObject<Secreto>(res);
            string res2 = api.GetSecreto(s.IdSecreto);
            Assert.AreEqual(insertado.RemitenteMail, s.RemitenteMail);
            Assert.AreEqual(insertado.Alias, s.Alias);
            Assert.AreEqual(insertado.Titulo, s.Titulo);
            Assert.AreEqual(insertado.Mensaje, s.Mensaje);
        }

        [TestMethod()]
        public void DeleteUserTest()
        {
            Assert.AreEqual("404", api.DeleteUsuario("juan@ubusecret.es"));

            string res = api.DeleteUsuario("paco@ubusecret.es");
            Usuario paco = JsonConvert.DeserializeObject<Usuario>(res);

            Assert.AreEqual(paco.Email, "paco@ubusecret.es");
            Assert.AreEqual(paco.Nombre, "Paco");
            Assert.AreEqual(paco.Apellidos, "Paco");
            Assert.AreEqual(paco.Rol, Rol.Deshabilitado);
            Assert.AreEqual(paco.Telefono, "123456789");
        }



        [TestMethod()]
        public void GetRecivedTest()
        {
            string resJson = api.GetRecibidos("oiwfjpoi@eii.es");
            List<Secreto> res = JsonConvert.DeserializeObject<List<Secreto>>(resJson);
            Assert.AreEqual(0,res.Count);

            resJson = api.GetRecibidos("pepe@ubusecret.es");
            res = JsonConvert.DeserializeObject<List<Secreto>>(resJson);
            Assert.AreEqual(0,res.Count);

            resJson = api.GetRecibidos("paco@ubusecret.es");
            res = JsonConvert.DeserializeObject<List<Secreto>>(resJson);
            Assert.AreEqual(2, res.Count);

        }

        [TestMethod()]
        public void GetSentTest()
        {
            string resJson = api.GetEnviados("oiwfjpoi@eii.es");
            List<Secreto> res = JsonConvert.DeserializeObject<List<Secreto>>(resJson);
            Assert.AreEqual(0, res.Count);

            resJson = api.GetEnviados("pepe@ubusecret.es");
            res = JsonConvert.DeserializeObject<List<Secreto>>(resJson);
            Assert.AreEqual(2, res.Count);

            resJson = api.GetEnviados("paco@ubusecret.es");
            res = JsonConvert.DeserializeObject<List<Secreto>>(resJson);
            Assert.AreEqual(0, res.Count);

        }



    }
}