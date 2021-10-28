using System;
using UBUSecret;
using Util;
using Data;
using Interfaces;
using System.Web.UI.WebControls;

namespace Web
{
    public partial class WebForm4 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Usuario usuario = (Usuario) Session["usuario"];

            // Comrpueba los permisos y avisa
            if (usuario == null)
            {
                Response.Redirect("default.aspx?err=" + Crypt.CodificarB64("Usuario No Autenticado"));
            }
            else if (usuario.Rol != Rol.Administrador)
            {
                Response.Redirect("secretos.aspx?err=" + Crypt.CodificarB64("No tienes permisos para realizar esta acción."));
            }

            ICapaDatos bd = Data.DBPruebas.ObtenerInstacia();

            var usuarios = bd.LeerUsuarios();

            foreach (var u in usuarios)
            {
                // Crea la fila de cada uno de los usuarios con el dropdown para modificar el rol
                AñadeUsuario(u, usuario);
            }
        }

        private void OnChange(object sender, EventArgs e)
        {
            try
            {
                var drop = (DropDownList) sender;
                
                ICapaDatos bd = Data.DBPruebas.ObtenerInstacia();

                var usuario = bd.LeerUsuario(drop.ID);

                if (usuario != null)
                {
                    foreach (Rol r in Enum.GetValues(typeof(Rol)))
                    {

                        if (drop.SelectedValue.Equals(r.ToString()))
                        {
                            usuario.Rol = r;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                Test.Text += "Error " + ex.Message;
            }
        }

        private void AñadeUsuario(Usuario u, Usuario actual)
        {
            // Crea una nueva fila
            Panel div = new Panel();
            div.ID = "" + u.Id;
            div.CssClass = "alert alert-secondary flex flex-row overflow-hidden";
            Deshabilitados.Controls.Add(div);

            // Crea el primer texto, nombre y apellidos
            var nombre = new Label();
            nombre.CssClass = "overflow-hidden truncate w-1/3";
            nombre.Text = u.Nombre + " " + u.Apellidos;
            div.Controls.Add(nombre);

            // Crea la segunda columna: Email
            var email = new Label();
            email.CssClass = "overflow-hidden truncate w-1/3";
            email.Text = u.Email;
            div.Controls.Add(email);
            
            // Crea el Dropdown
            var drop = new DropDownList();
            drop.ID = "" + u.Email;
            
            // No crees el dropdown para el usuario actual (gestor)
            if (u.Equals(actual))
            {
                return;
            }

            
            foreach (Rol r in Enum.GetValues(typeof(Rol))) {

                var item = new ListItem();
                item.Text = r.ToString();
                item.Value = r.ToString();

                drop.Items.Insert(Math.Max(drop.Items.Count -1, 0), item);
            }
            drop.SelectedValue = u.Rol.ToString();
            drop.AutoPostBack = true;
            drop.SelectedIndexChanged += new EventHandler(OnChange);

            div.Controls.Add(drop);

        }

    }
}