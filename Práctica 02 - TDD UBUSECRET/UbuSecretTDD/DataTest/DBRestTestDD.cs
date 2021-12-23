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
        static Secreto secretoUno = null;
        static Secreto secretoDos = null;


        [TestInitialize]
        public void Reset()
        {
            datos.Reset();
            pepe = CrearUsuario("Pepe", "Pepe", "pepe@ubusecret.es");
            paco = CrearUsuario("Paco", "Paco", "paco@ubusecret.es");
            LinkedList<Usuario> receptores = new LinkedList<Usuario>();
            receptores.AddLast(paco);
            secretoUno = CrearSecretoUno(pepe,paco);
            secretoDos = CrearSecretoDos(pepe,paco);
            Assert.IsNotNull(secretoUno);
            Assert.IsNotNull(secretoDos);


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

        static public Secreto CrearSecretoUno(Usuario pepe, Usuario paco) 
        {
            //Usuario pepe = CrearUsuario("Pepe", "Pepe", "pepe@ubusecret.es");
            //Usuario paco = CrearUsuario("Paco", "Paco", "paco@ubusecret.es");
            LinkedList<Usuario> receptores = new LinkedList<Usuario>();
            receptores.AddLast(paco);
            return new Secreto(pepe, receptores, "prueba", "hola", "pepito");
        }

        static public Secreto CrearSecretoDos(Usuario pepe, Usuario paco)
        {

            LinkedList<Usuario> receptores = new LinkedList<Usuario>();
            receptores.AddLast(paco);
            return new Secreto(pepe, receptores, "dos", "dos", "pep");
        }



        [TestMethod]
        public void RestTest()
        {
            datos.InsertarUsuario(pepe);
            datos.InsertarUsuario(paco);
            datos.Reset();
            Assert.AreEqual(0, datos.LeerUsuarios().Count);
            LinkedList<Usuario> receptores = new LinkedList<Usuario>();
            receptores.AddLast(paco);
            secretoUno = new Secreto(pepe, receptores, "prueba", "hola", "pepito");
            secretoDos = new Secreto(pepe, receptores, "dos", "dos", "pep");

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






        public static IEnumerable<object[]> GetInsertarSecretoData() 
        {
            yield return new object[] { secretoUno, false };
            yield return new object[] { secretoDos, false };
        }

        [DataTestMethod]
        [DynamicData(nameof(GetInsertarSecretoData), DynamicDataSourceType.Method)]
        public void InsertarSecretoTest(Secreto secreto, bool falla)
        {
            try
            {
                Assert.IsNull(datos.LeerSecreto(secreto.IdSecreto));
                Assert.IsTrue(datos.InsertarSecreto(secreto));
                Assert.AreEqual(datos.LeerSecreto(secreto.IdSecreto), secreto);
                Assert.IsFalse(datos.InsertarSecreto(secreto));
            }
            catch (AssertFailedException e)
            {
                Assert.IsTrue(falla);
            }
        }


        public static IEnumerable<object[]> GetLeerSecretoData()
        {
            yield return new object[] { secretoUno, false };
            yield return new object[] { secretoDos, false };
        }

        [DataTestMethod]
        [DynamicData(nameof(GetLeerSecretoData), DynamicDataSourceType.Method)]
        public void LeerSecretoTest(Secreto secreto, bool falla)
        {
            try
            {
                Console.WriteLine(secreto.ToString());
                Assert.IsNull(datos.LeerSecreto(secreto.IdSecreto));
                Assert.IsNotNull(secreto);
                Assert.IsTrue(datos.InsertarSecreto(secreto));
                Assert.AreEqual(secreto, datos.LeerSecreto(secreto.IdSecreto));
            }
            catch (AssertFailedException e)
            {
                Assert.IsTrue(falla);
            }
        }

        public static IEnumerable<object[]> GetBorrarSecretoData()
        {
            Usuario pepe = CrearUsuario("Pepe", "Pepe", "pepe@ubusecret.es");
            Usuario paco = CrearUsuario("Paco", "Paco", "paco@ubusecret.es");

            yield return new object[] { CrearSecretoUno(pepe, paco), false };
            yield return new object[] { CrearSecretoDos(pepe, paco), false };
            yield return new object[] { null, true };
        }

        [DataTestMethod]
        [DynamicData(nameof(GetBorrarSecretoData), DynamicDataSourceType.Method)]
        public void BorrarSecretoTest(Secreto secreto, bool falla)
        {
            try
            {
                datos.InsertarSecreto(secreto);
                Assert.IsNotNull(datos.LeerSecreto(secreto.IdSecreto));
                Assert.AreEqual(secreto, datos.BorrarSecreto(secreto.IdSecreto));
                Assert.IsNull(datos.LeerSecreto(secreto.IdSecreto));
                Assert.IsNull(datos.BorrarSecreto(secreto.IdSecreto));
            }
            catch (Exception e)
            {
                Assert.IsTrue(falla);
            }
        }








        public static IEnumerable<object[]> GetLeerSecretosRecibidosTestData()
        {
            Usuario pepe = CrearUsuario("Pepe", "Pepe", "pepe@ubusecret.es");
            Usuario paco = CrearUsuario("Paco", "Paco", "paco@ubusecret.es");
            yield return new object[] { CrearSecretoUno(pepe, paco),paco,pepe, false };
            yield return new object[] { CrearSecretoDos(pepe, paco),paco,pepe, false };
            yield return new object[] { null, paco, pepe, true };
        }

        [DataTestMethod]
        [DynamicData(nameof(GetLeerSecretosRecibidosTestData), DynamicDataSourceType.Method)]
        public void LeerSecretosRecibidosTest(Secreto secreto, Usuario receptor, Usuario owner ,bool falla)
        {
            try
            {
                datos.InsertarSecreto(secreto);
                Assert.AreEqual(1, datos.LeerSecretosRecibidos(receptor).Count);
                Assert.IsTrue(datos.LeerSecretosRecibidos(receptor).Contains(secreto));
                datos.BorrarSecreto(secreto.IdSecreto);
                Assert.AreEqual(0, datos.LeerSecretosRecibidos(receptor).Count);
                Assert.AreEqual(0, datos.LeerSecretosRecibidos(owner).Count);

            }
            catch (Exception e)
            {
                Assert.IsTrue(falla);
            }
        }




        public static IEnumerable<object[]> GetLeerSecretosEnviadosTestData()
        {
            Usuario pepe = CrearUsuario("Pepe", "Pepe", "pepe@ubusecret.es");
            Usuario paco = CrearUsuario("Paco", "Paco", "paco@ubusecret.es");
            yield return new object[] { CrearSecretoUno(pepe, paco), paco, pepe, false };
            yield return new object[] { CrearSecretoDos(pepe, paco), paco, pepe, false };
            yield return new object[] { null, paco, pepe, true };
        }

        [DataTestMethod]
        [DynamicData(nameof(GetLeerSecretosEnviadosTestData), DynamicDataSourceType.Method)]
        public void LeerSecretosEnviadosTest(Secreto secreto, Usuario receptor, Usuario owner, bool falla)
        {
            try
            {
                datos.InsertarSecreto(secreto);
                Assert.AreEqual(1, datos.LeerSecretosEnviados(owner).Count);
                Assert.IsTrue(datos.LeerSecretosEnviados(owner).Contains(secreto));
                datos.BorrarSecreto(secreto.IdSecreto);
                Assert.AreEqual(0, datos.LeerSecretosEnviados(owner).Count);
                Assert.AreEqual(0, datos.LeerSecretosEnviados(receptor).Count);
            }
            catch (Exception e)
            {
                Assert.IsTrue(falla);
            }
        }




        public static IEnumerable<object[]> GetBorrarUsuarioTestData()
        {
            Usuario pepe = CrearUsuario("Pepe", "Pepe", "pepe@ubusecret.es");
            Usuario paco = CrearUsuario("Paco", "Paco", "paco@ubusecret.es");
            yield return new object[] { pepe, "pepe@ubusecret.es", false };
            yield return new object[] { paco, "paco@ubusecret.es", false };
            yield return new object[] { null, "paco@ubusecret.es", true };
            yield return new object[] { null, "pepe@ubusecret.es", true };

        }

        [DataTestMethod]
        [DynamicData(nameof(GetBorrarUsuarioTestData), DynamicDataSourceType.Method)]
        public void BorrarUsuarioTest(Usuario usuario, String email, bool falla)
        {
            try
            {
                datos.InsertarUsuario(usuario);
                Assert.AreEqual(usuario, datos.BorrarUsuario(email));
                Assert.IsNull(datos.LeerUsuario(email));
                Assert.IsNull(datos.BorrarUsuario(email));
            }
            catch (Exception e)
            {
                Assert.IsTrue(falla);
            }
        }



        public static IEnumerable<object[]> GetLeerUsuariosDeshabilitadosTestData()
        {
            Usuario pepe = CrearUsuario("Pepe", "Pepe", "pepe@ubusecret.es");
            Usuario paco = CrearUsuario("Paco", "Paco", "paco@ubusecret.es");
            yield return new object[] { pepe, false };
            yield return new object[] { paco, false };
            yield return new object[] { null, true };

        }

        [DataTestMethod]
        [DynamicData(nameof(GetLeerUsuariosDeshabilitadosTestData), DynamicDataSourceType.Method)]
        public void LeerUsuariosDeshabilitadosTest(Usuario usuario, bool falla)
        {
            try
            {
                datos.InsertarUsuario(usuario);

                usuario.Rol = Rol.Deshabilitado;

                Assert.IsTrue(usuario.EsDeshabilitado());

                Assert.IsTrue(datos.LeerUsuariosDeshabilitados().Contains(usuario));

                usuario.Rol = Rol.Bloqueado;

                Assert.IsTrue(usuario.EsInactivo());

                Assert.IsFalse(datos.LeerUsuariosDeshabilitados().Contains(usuario));
            }
            catch (Exception e)
            {
                Assert.IsTrue(falla);
            }
        }


    }
}
