using Microsoft.VisualStudio.TestTools.UnitTesting;
using Data;
using System.Collections.Generic;
using Interfaces;
using UBUSecret;
using System;

namespace DataTest
{
    [TestClass]
    public class DBRestTestDD
    {
        ICapaDatos datos = DBRest.ObtenerInstancia();
        static Usuario pepe = null;
        static Usuario paco = null;
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

        static public Usuario CrearUsuario(string nombre, string apellidos, string email)
        {
            return new Usuario(nombre, apellidos, email, "123456789", "Usuario1234");
        }

        [TestMethod]
        public void RestTest()
        {
            datos.InsertarUsuario(pepe);
            datos.InsertarUsuario(paco);
            datos.Reset();
            Assert.AreEqual(0, datos.LeerUsuarios().Count);

            int cont = 0;
            foreach (var usuario in datos.LeerUsuarios())
            {
                cont += datos.LeerSecretosEnviados(usuario).Capacity;
            }
            Assert.AreEqual(0, cont);
        }


        public static IEnumerable<object[]> GetInsertarUsuarioData()
        {
            yield return new object[] { pepe, "paco@ubusecret.es", true };
            yield return new object[] { paco, "pepe@ubusecret.es", true };
            yield return new object[] { paco, "paco@ubusecret.es", false };
            yield return new object[] { pepe, "pepe@ubusecret.es", false };

        }

        [DataTestMethod]
        [DynamicData(nameof(GetInsertarUsuarioData), DynamicDataSourceType.Method)]
        public void InsertarUsuarioTest(Usuario u, string email, bool falla)
        {
            try
            {
                Assert.IsNull(datos.LeerUsuario(email));
                Assert.IsNotNull(u);
                Assert.IsTrue(datos.InsertarUsuario(u));
                Assert.AreEqual(u, datos.LeerUsuario(email));
                Assert.IsFalse(datos.InsertarUsuario(u));
                
            }
            catch (AssertFailedException e)
            {
                Assert.AreEqual(true, falla);
                return;
            
            }catch(Exception e)
            {
                throw e;
            }
            Assert.AreEqual(false, falla);
        }



        public static IEnumerable<object[]> GetLeerUsuarioData()
        {
            yield return new object[] { pepe, "paco@ubusecret.es", true };
            yield return new object[] { paco, "pepe@ubusecret.es", true };
            yield return new object[] { null, "pepe@ubusecret.es", true };
            yield return new object[] { null, null, true };
            yield return new object[] { pepe, null, true };
            yield return new object[] { paco, "paco@ubusecret.es", false };
            yield return new object[] { pepe, "pepe@ubusecret.es", false };

        }
        [DataTestMethod]
        [DynamicData(nameof(GetLeerUsuarioData), DynamicDataSourceType.Method)]
        public void LeerUsuarioTest(Usuario u, string email, bool falla)
        {
            try
            {
                Assert.IsNull(datos.LeerUsuario(email));
                Assert.IsNotNull(u);
                Assert.IsTrue(datos.InsertarUsuario(u));
                Assert.AreEqual(u, datos.LeerUsuario(email));
                Assert.IsFalse(falla);
            }
            catch (AssertFailedException e)
            {
                Assert.IsTrue(falla);
            }
        }

        public static IEnumerable<object[]> GetLeerUsuariosData()
        {
            var juan = CrearUsuario("Juan", "Juan", "pepe@ubusecret.es");

            yield return new object[] { new Usuario[] { } , false};
            // Insertar dos nulos falla
            yield return new object[] { new Usuario[] { null, null}, true };
            // Insertar dos veces al mismo usuario falla
            yield return new object[] { new Usuario[] { juan, juan }, true };
            // Insertar a dos usuarios distintos no falla
            yield return new object[] { 
                new Usuario[] { 
                    CrearUsuario("Pepe", "Pepe", "pepe@ubusecret.es"),
                    CrearUsuario("Paco", "Paco", "paco@ubusecret.es") },
                false 
            };
        }
        [DataTestMethod]
        [DynamicData(nameof(GetLeerUsuariosData), DynamicDataSourceType.Method)]
        public void LeerUsuariosTest(Usuario[] usuarios, bool falla)
        {
            
            try
            {
                Assert.AreEqual(0, datos.LeerUsuarios().Count);
                
                // Insertamos los usuarios
                foreach(var u in usuarios)
                {
                    Assert.IsTrue(datos.InsertarUsuario(u));
                }

                // Leemos de vuelta
                var insertados = datos.LeerUsuarios();
                Assert.AreEqual(usuarios.Length, insertados.Count);

                // Comprobamos que los usuarios se hayan insertado correctamente
                foreach (var u in usuarios)
                {
                    Assert.IsTrue(insertados.Contains(u));
                }
            }
            catch(AssertFailedException e)
            {
                Assert.AreEqual(true, falla);
                return;
            }
            catch(Exception e)
            {
                throw e;
            }
            Assert.AreEqual(false, falla);
        }

        public static IEnumerable<object[]> GetListarActivosData()
        {
            // Insertar dos usuarios con cambios correctos no falla
            yield return new object[] {
                new Usuario[] { CrearUsuario("Pepe", "Pepe", "pepe@ubusecret.es"), CrearUsuario("Paco", "Paco", "paco@ubusecret.es") },
                new string[] { "Usuario1234", "Usuario1234" },
                new string[] { "Usuario1234_", "Usuario1234_" },
                new Rol[] { Rol.Usuario, Rol.Usuario },
                2,
                false
            };

            yield return new object[] {
                new Usuario[] { CrearUsuario("Pepe", "Pepe", "pepe@ubusecret.es"), CrearUsuario("Paco", "Paco", "paco@ubusecret.es") },
                new string[] { "Usuario1234", "Usuario1234" },
                new string[] { "Usuario1234_", "Usuario1234_" },
                new Rol[] { Rol.Usuario, Rol.Administrador },
                2,
                false
            };

            // Bloquear un usuario no lo marca como activo
            yield return new object[] {
                new Usuario[] { CrearUsuario("Pepe", "Pepe", "pepe@ubusecret.es"), CrearUsuario("Paco", "Paco", "paco@ubusecret.es") },
                new string[] { "Usuario1234", "Usuario1234" },
                new string[] { "Usuario1234_", "Usuario1234_" },
                new Rol[] { Rol.Usuario, Rol.Bloqueado },
                1,
                false
            };
            yield return new object[] {
                new Usuario[] { CrearUsuario("Pepe", "Pepe", "pepe@ubusecret.es"), CrearUsuario("Paco", "Paco", "paco@ubusecret.es") },
                new string[] { "Usuario1234", "Usuario1234" },
                new string[] { "Usuario1234_", "Usuario1234_" },
                new Rol[] { Rol.Bloqueado, Rol.Bloqueado },
                0,
                false
            };

            // La contraseña inicial no es correcta
            yield return new object[] {
                new Usuario[] { CrearUsuario("Pepe", "Pepe", "pepe@ubusecret.es"), CrearUsuario("Paco", "Paco", "paco@ubusecret.es") },
                new string[] { "Usuario1234__", "Usuario1234__" },
                new string[] { "Usuario1234_", "Usuario1234_" },
                new Rol[] { Rol.Bloqueado, Rol.Bloqueado },
                0,
                true
            };
            // Misma contraseña antigua y nueva
            yield return new object[] {
                new Usuario[] { CrearUsuario("Pepe", "Pepe", "pepe@ubusecret.es"), CrearUsuario("Paco", "Paco", "paco@ubusecret.es") },
                new string[] { "Usuario1234", "Usuario1234" },
                new string[] { "Usuario1234", "Usuario1234" },
                new Rol[] { Rol.Usuario, Rol.Usuario },
                0,
                true
            };

        }
        [DataTestMethod]
        [DynamicData(nameof(GetListarActivosData), DynamicDataSourceType.Method)]
        public void ListarUsuariosActivosTest(Usuario[] usuarios, string[] oldPassw, string[] newPassw, Rol[] newRoles, int okCount, bool falla)
        {
            try
            {
                // Cambiamos la contraseña y roles de todos los usuarios
                for (int i = 0; i < usuarios.Length; i++)
                {
                    Assert.IsTrue(usuarios[i].CambiarPassword(oldPassw[i], newPassw[i], newPassw[i]));
                    usuarios[i].Rol = newRoles[i];
                }

                // Comprobamos que la BBDD está vacía
                Assert.AreEqual(0, datos.LeerUsuarios().Count);
                Assert.AreEqual(0, datos.LeerUsuariosActivos().Count);

                // Insertamos en la BBDD
                foreach (var u in usuarios)
                {
                    Assert.IsTrue(datos.InsertarUsuario(u));
                }

                // Comprobamos con los parámetros de entrada
                Assert.AreEqual(usuarios.Length, datos.LeerUsuarios().Count);
                Assert.AreEqual(okCount, datos.LeerUsuariosActivos().Count);
            }
            catch(AssertFailedException e)
            {
                Assert.IsTrue(falla);
                return;
            }
            Assert.IsFalse(falla);
        }


        public static IEnumerable<object[]> GetListarInactivosData()
        {
            // Activar a ambos usuarios el método devuelve una lista vacía.
            yield return new object[] {
                new Usuario[] { CrearUsuario("Pepe", "Pepe", "pepe@ubusecret.es"), CrearUsuario("Paco", "Paco", "paco@ubusecret.es") },
                new string[] { "Usuario1234", "Usuario1234" },
                new string[] { "Usuario1234_", "Usuario1234_" },
                new Rol[] { Rol.Usuario, Rol.Usuario },
                0,
                false
            };

            yield return new object[] {
                new Usuario[] { CrearUsuario("Pepe", "Pepe", "pepe@ubusecret.es"), CrearUsuario("Paco", "Paco", "paco@ubusecret.es") },
                new string[] { "Usuario1234", "Usuario1234" },
                new string[] { "Usuario1234_", "Usuario1234_" },
                new Rol[] { Rol.Usuario, Rol.Administrador },
                0,
                false
            };

            // Cambiar el rol pero no la contraseña no activa a los usuarios
            yield return new object[] {
                new Usuario[] { CrearUsuario("Pepe", "Pepe", "pepe@ubusecret.es"), CrearUsuario("Paco", "Paco", "paco@ubusecret.es") },
                new string[] {  },
                new string[] {  },
                new Rol[] { Rol.Usuario, Rol.Usuario },
                2,
                false
            };

            // Cambiar la contraseña pero no el rol no activa a los usuarios
            yield return new object[] {
                new Usuario[] { CrearUsuario("Pepe", "Pepe", "pepe@ubusecret.es"), CrearUsuario("Paco", "Paco", "paco@ubusecret.es") },
                new string[] { "Usuario1234", "Usuario1234" },
                new string[] { "Usuario1234_", "Usuario1234_" },
                new Rol[] {  },
                2,
                false
            };

            // Cambiar los roles a deshabilitado y cambiar la contraseña no activa a los usuarios 
            yield return new object[] {
                new Usuario[] { CrearUsuario("Pepe", "Pepe", "pepe@ubusecret.es"), CrearUsuario("Paco", "Paco", "paco@ubusecret.es") },
                new string[] { "Usuario1234", "Usuario1234" },
                new string[] { "Usuario1234_", "Usuario1234_" },
                new Rol[] { Rol.Deshabilitado, Rol.Deshabilitado },
                2,
                false
            };

            // Cambiar los roles a bloqueado no activa a los usuarios
            yield return new object[] {
                new Usuario[] { CrearUsuario("Pepe", "Pepe", "pepe@ubusecret.es"), CrearUsuario("Paco", "Paco", "paco@ubusecret.es") },
                new string[] { "Usuario1234", "Usuario1234" },
                new string[] { "Usuario1234_", "Usuario1234_" },
                new Rol[] { Rol.Bloqueado, Rol.Bloqueado },
                2,
                false
            };

            // Introducir dos veces al mismo usuarios no ejecuta correctamente los tests
            var juan = CrearUsuario("Juan", "Juan", "juan@ubusecret.es");
            yield return new object[] {
                new Usuario[] { juan, juan },
                new string[] { "Usuario1234", "Usuario1234" },
                new string[] { "Usuario1234_", "Usuario1234_" },
                new Rol[] { Rol.Bloqueado, Rol.Bloqueado },
                2,
                true
            };

        }
        [DataTestMethod]
        [DynamicData(nameof(GetListarInactivosData), DynamicDataSourceType.Method)]
        public void ListarUsuariosInactivosTest(Usuario[] usuarios, string[] oldPassw, string[] newPassw, Rol[] newRoles, int okCount, bool falla)
        {
            try
            {

                // Comprobamos que la BBDD está vacía
                Assert.AreEqual(0, datos.LeerUsuarios().Count);
                Assert.AreEqual(0, datos.LeerUsuariosInactivos().Count);

                // Insertamos en la BBDD
                foreach (var u in usuarios)
                {
                    Assert.IsTrue(datos.InsertarUsuario(u));
                }
                // Comprobamos que los usuarios son inactivos por defecto:
                Assert.AreEqual(usuarios.Length, datos.LeerUsuarios().Count);
                Assert.AreEqual(usuarios.Length, datos.LeerUsuariosInactivos().Count);

                // Cambiamos los roles de todos los usuarios
                for (int i = 0; i < newRoles.Length; i++)
                { 
                    usuarios[i].Rol = newRoles[i];
                }
                // Cambiamos la contraseña 
                for (int i = 0; i < oldPassw.Length; i++)
                {
                    Assert.IsTrue(usuarios[i].CambiarPassword(oldPassw[i], newPassw[i], newPassw[i]));
                }

                // Comprobamos con los parámetros de entrada
                Assert.AreEqual(usuarios.Length, datos.LeerUsuarios().Count);
                Assert.AreEqual(okCount, datos.LeerUsuariosInactivos().Count);
            }
            catch (AssertFailedException e)
            {
                Console.WriteLine(e.Message);
                Assert.IsTrue(falla);
                return;
            }
            Assert.IsFalse(falla);

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
