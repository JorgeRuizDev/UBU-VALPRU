using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UBUSecret;
namespace Web
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            Usuario usuario = (Usuario)Session["usuario"];

            Nav.Visible = false;

            if (usuario != null)
            {
                Nav.Visible = true;
                LblUsuario.Text = "Hola " + usuario.Nombre + " " + usuario.Apellidos + "! (Cerrar Sesión)";

                string path = HttpContext.Current.Request.Url.AbsolutePath;
                

                if (!Page.IsPostBack && (path == "/" || path == "/default.aspx"))
                {
                    Response.Redirect("secretos.aspx", true);
                }
            }

        }
    }
}