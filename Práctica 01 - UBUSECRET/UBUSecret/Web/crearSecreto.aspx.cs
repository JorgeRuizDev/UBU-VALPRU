using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UBUSecret;
using Util;
using Interfaces;
using Data;

namespace Web
{
    public partial class crearSecreto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Usuario usuario = (Usuario)Session["usuario"];
            if (usuario == null)
            {
                Server.Transfer("default.aspx?err=" + Crypt.CodificarB64("Usuario no Autenticado"));
            }
            else if (!usuario.EsValido())
            {
                Server.Transfer("default.aspx?err=" + Crypt.CodificarB64("No tienes permisos"));
            }




        }

        protected void BtnSend_Click(object sender, EventArgs e)
        {
            Usuario usuario = (Usuario)Session["usuario"];
            ICapaDatos bd = Data.DBPruebas.ObtenerInstacia();


           


            if (Validar()) {

                LinkedList<Usuario> receptores = new LinkedList<Usuario>();

                char[] separador = new char[] { ';' };
                string[] resultado = BoxMails.Text.Split(separador, StringSplitOptions.None);

                

                foreach (String element in resultado)
                {
                    receptores.AddLast(bd.LeerUsuario(element));
                }

                if (TextTitle.Text.Length == 0) {
                    TextTitle.Text = "Sin Titulo";
                }

                if (TexAlias.Text.Length == 0)
                {
                    TexAlias.Text =usuario.Nombre;
                }

                Secreto secreto = new Secreto(usuario, receptores, BoxSecreto.Text, TextTitle.Text, TexAlias.Text);

                bd.InsertarSecreto(secreto);

                Server.Transfer("secretos.aspx", true);

            }

            
        }

        private bool Validar() {
            bool valsec = ValidarSecreto();
            bool valrec = ValidarReceptores();
            return valrec && valsec;
        }


        private bool ValidarSecreto()
        {
            if (BoxSecreto.Text.Length >= 250) {
                ErrSecreto.Visible = true;
                ErrSecreto.Text = "No se puede superar 250 caracteres";
                return false;
            }

            if (BoxSecreto.Text.Length == 0) {
                ErrSecreto.Visible = true;
                ErrSecreto.Text = "Debe haber caracteres en el mensaje";
                return false;
            }
            
            ErrSecreto.Visible = false;
            return true;
        }

        private bool ValidarReceptores()
        {
            char[] separador = new char[] { ';' };
            string[] resultado = BoxMails.Text.Split(separador, StringSplitOptions.None);

            ICapaDatos bd = Data.DBPruebas.ObtenerInstacia();

            //PrintMails.Text = resultado.ToString();

            foreach (String element in resultado)
            {
                if (bd.LeerUsuario(element) == null) {
                    ErrMails.Visible = true;
                    
                    return false;
                }
                    
            }
            
            ErrSecreto.Visible = false;
            return true;

        }
    }

}