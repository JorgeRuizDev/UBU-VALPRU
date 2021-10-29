using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Util;
using Interfaces;
using Data;
namespace Web
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ErrMail.Visible = false;
            ErrOld.Visible = false;
            ErrPassw.Visible = false;
            ValidarCampos();

        }

        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            Server.Transfer("default.aspx", true);
        }

        protected void BtnSend_Click(object sender, EventArgs e)
        {
            if (ValidarCampos())
            {
                ICapaDatos bd = Data.DBPruebas.ObtenerInstacia();

                var usuario = bd.LeerUsuario(BoxMail.Text);

                if (usuario == null)
                {
                    ErrPassw.Text = "El usuario no existe";
                    ErrPassw.Visible = true;
                    return;
                }

                if(!usuario.CambiarPassword(BoxOld.Text, BoxPassw.Text, BoxPassw2.Text))
                {
                    ErrPassw.Text = "La contraseña antigua es incorrecta";
                    ErrPassw.Visible = true;
                    return;
                }


                Session["usuario"] = usuario;
                Server.Transfer("default.aspx", true);
            }
        }

        private bool ValidarCampos()
        {

            bool esValido = true;

            if (BoxMail.Text.Length > 0)
            {
                if (!Validar.Email(BoxMail.Text))
                {
                    ErrMail.Text = "Formato de Email Incorrecto";
                    ErrMail.Visible = true;
                    esValido = false;
                }
            }

            if (BoxPassw.Text.Length > 0 || BoxPassw2.Text.Length > 0)
            {
                if (!Validar.Password(BoxPassw.Text))
                {
                    ErrPassw.Text = "La contraseña no es lo suficientemente fuerte";
                    ErrPassw.Visible = true;
                    esValido = false;
                }

                if (!BoxPassw.Text.Equals(BoxPassw2.Text))
                {
                    ErrPassw.Text = "Las contraseñas no coinciden";
                    ErrPassw.Visible = true;
                    esValido = false;
                }

                if (BoxPassw.Text.Equals(BoxOld.Text))
                {
                    ErrPassw.Text = "Las contraseñas Son Igulaes";
                    ErrPassw.Visible = true;
                    esValido = false;
                }
            }
            return esValido;
        }
    }
}