using System;
using UBUSecret;
using Util;

namespace Web
{
    public partial class WebForm4 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Usuario usuario = (Usuario) Session["usuario"];
            if (usuario == null)
            {
                Response.Redirect("default.aspx?err=" + Crypt.CodificarB64("Usuario No Autenticado"));
            }
            if (usuario.Rol != Rol.Administrador)
            {
                Response.Redirect("secretos.aspx?err=" + Crypt.CodificarB64("No tienes permisos para realizar esta acción."));
            }


        }
    }
}