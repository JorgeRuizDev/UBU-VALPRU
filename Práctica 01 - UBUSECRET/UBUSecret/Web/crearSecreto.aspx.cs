using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UBUSecret;
using Util;

namespace Web
{
    public partial class crearSecreto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Usuario usuario = (Usuario) Session["usuario"];
            if (usuario == null)
            {
                Server.Transfer("default.aspx?err="+Crypt.CodificarB64("Usuario no Autenticado"));
            }
            else if (!usuario.EsValido())
            {
                Server.Transfer("default.aspx?err=" + Crypt.CodificarB64("No tienes permisos"));
            }


        }
    }
}