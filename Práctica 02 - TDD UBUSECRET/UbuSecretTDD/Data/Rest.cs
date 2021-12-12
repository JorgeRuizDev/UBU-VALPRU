using System;
using System.Collections.Generic;
using System.Text;
using Interfaces;
using Newtonsoft.Json;
using UBUSecret;
namespace Data
{
   
    public class Rest
    {

        ICapaDatos datos = DBRest.ObtenerInstancia();

        public void Populate()
        {
            datos.Reset();
            Usuario pepe = CrearUsuario("Pepe", "Pepe", "pepe@ubusecret.es");
            Usuario paco = CrearUsuario("Paco", "Paco", "paco@ubusecret.es");
            LinkedList<Usuario> receptores = new LinkedList<Usuario>();
            receptores.AddLast(paco);
            Secreto secreto = new Secreto(pepe, receptores, "prueba", "hola", "pepito");
            Secreto secretoDos = new Secreto(pepe, receptores, "dos", "dos", "pep");

            datos.InsertarSecreto(secreto);
            datos.InsertarSecreto(secretoDos);
            datos.InsertarUsuario(pepe);
            datos.InsertarUsuario(paco);
        }
        private Usuario CrearUsuario(string nombre, string apellidos, string email)
        {
            return new Usuario(nombre, apellidos, email, "123456789", "Usuario1234");
        }

        public string GetUsuario(string email)
        {
            Usuario u = datos.LeerUsuario(email);
            if (u == null)
            {
                return "404";
            }
            return JsonConvert.SerializeObject(u, Formatting.Indented);
        }

        public string GetSecreto(int id)
        {
            Secreto s = datos.LeerSecreto(id);
            if (s == null)
            {
                return "404";
            }
            return JsonConvert.SerializeObject(s, Formatting.Indented);
        }
        
        public string PutUsuario(string u)
        {
            try
            {
                var def = new { Nombre = "", Apellidos = "", Email = "", Telefono = "", Password = "" };
                var res = JsonConvert.DeserializeAnonymousType(u, def);

                Usuario usr = new Usuario(res.Nombre,
                    res.Apellidos,
                    res.Email,
                    res.Telefono,
                    res.Password);
                if (!datos.InsertarUsuario(usr))
                {
                    return "400";
                }
                return JsonConvert.SerializeObject(usr);
            }catch(Exception e)
            {
                return "400";
            }

        }

        public string GetDesactivados()
        {
            return JsonConvert.SerializeObject(datos.LeerUsuariosInactivos());
        }

        public string PutSecreto(string remitente, string body)
        {
            if (datos.LeerUsuario(remitente) == null)
            {
                return "400";
            }

            var def = new {Receptores = new string[] { },  Mensaje = "", Titulo= "", Alias = "",  };

            try
            {
                var res = JsonConvert.DeserializeAnonymousType(body, def);
             

                LinkedList<Usuario> receptores = new LinkedList<Usuario>();
                
                foreach (string receptor in res.Receptores)
                {
                    Usuario r = datos.LeerUsuario(receptor);
                    if (r == null)
                    {
                        return "400";
                    }
                    
                    receptores.AddLast(r);
                }
                

                Usuario rem = datos.LeerUsuario(remitente);

                Secreto s = new Secreto(rem, receptores, res.Mensaje, res.Titulo, res.Alias);

                if (!datos.InsertarSecreto(s))
                {
                    return "400";
                }

                return JsonConvert.SerializeObject(s);
            }
            catch(Exception e)
            {
                return "400";
            }
        }
        
    }
}
