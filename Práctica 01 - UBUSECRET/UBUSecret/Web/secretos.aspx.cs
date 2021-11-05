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
    public partial class secretos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Usuario usuario = (Usuario)Session["usuario"];
            ICapaDatos db = Data.DBPruebas.ObtenerInstacia();
            if (usuario == null)
            {
                
                Response.Redirect("default.aspx?err="+ Crypt.CodificarB64("Usuario No Autenticado"));
            }else if (usuario.Rol == Rol.Deshabilitado)
            {
                Session["usuario"] = null;
                Response.Redirect("default.aspx?err=" + Crypt.CodificarB64("Un administrador debe darte acceso manualmente"));     
            }
            else if (!usuario.EsValido())
            {
                Session["usuario"] = null;
                Response.Redirect("default.aspx?err=" + Crypt.CodificarB64("No tienes permisos para realizar esta acción. ¿Has cambiado tu contraseña al menos una vez?"));
                
            }

            var secretos = db.LeerSecretosRecibidos(usuario);

            foreach (var secreto in secretos)
            {
                Secretos.InnerHtml += String.Format(@"
                <a href=""secreto.aspx?id={3}"">
                    <section class=""alert alert-secondary flex flex-row overflow-hidden"" role=""alert"">
                        <div style=""width: 60%"" class=""overflow-hidden truncate "">
                            {0}
                        </div>
                        <div style=""width: 20%"" class=""overflow-hidden truncate text-center "" >
                            {1}
                        </div>
                        <div style=""width: 20%"" class=""overflow-hidden truncate text-center"">
                            {2}
                        </div>
                    </section>
                </ a >", secreto.Titulo, secreto.Alias, secreto.DiaHora.ToString(), secreto.IdSecreto);


            }
        }

        protected void CrearSecreto_Click(object sender, EventArgs e)
        {
            Server.Transfer("crearSecreto.aspx");
        }
    }
}