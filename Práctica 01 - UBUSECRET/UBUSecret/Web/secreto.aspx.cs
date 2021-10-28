using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UBUSecret;
using Util;
using Interfaces;
namespace Web
{
    public partial class WebForm3 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            Usuario usuario = (Usuario)Session["usuario"];

            if (usuario == null)
            {
                RedirectHome("Usuario no autenticado");
            }

            int secretId = -1;
            SecretId.Text = Request["id"];
            if (Request["id"] != null)
            {
                try
                {
                    secretId = Int32.Parse( Request["id"]);
                    
                }
                catch (Exception ex)
                {
                    RedirectHome("ID Secreto Incorrecto");
                }
            }
            else
            {
                RedirectHome("No hay Id de secreto");
            }
            
            

            ICapaDatos bd = Data.DBPruebas.ObtenerInstacia();

            Secreto secreto = null;

            if (secreto == null)
            {
                return;
            }

            // comprobar que el usuario pueda ver dicho secreto


            BreadcrumSecret.Text = secreto?.Titulo;
           
        }

        private void RedirectHome(string err)
        {
            Response.Redirect("default.aspx?err="+ Crypt.CodificarB64(err));
        }
    }
}