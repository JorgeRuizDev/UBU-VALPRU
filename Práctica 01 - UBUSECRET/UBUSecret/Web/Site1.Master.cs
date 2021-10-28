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
    public partial class Site1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            Usuario usuario = (Usuario)Session["usuario"];

            Nav.Visible = false;
            ErrBox.Visible = false;
            Admin.Visible = false;
            if (Request["err"] != null)
            {
                ErrBox.Visible = true;
                ErrMsg.Text = Crypt.DecodificarB64(Request["err"]);
            }


            if (usuario != null)
            {
                Nav.Visible = true;
                LblUsuario.Text = "Hola " + usuario.Nombre + " " + usuario.Apellidos + "! (Cerrar Sesión)";

                string path = HttpContext.Current.Request.Url.AbsolutePath;
                
                if (usuario.Rol == Rol.Administrador)
                {
                    Admin.Visible = true;
                }


                if (!Page.IsPostBack && (path == "/" || path == "/default.aspx"))
                {
                    Response.Redirect("secretos.aspx", true);
                }
            }

        }

        protected void LinkSesión_Click(object sender, EventArgs e)
        {
            Session["usuario"] = null;
            Response.Redirect("default.aspx", true);
        }
    }
}