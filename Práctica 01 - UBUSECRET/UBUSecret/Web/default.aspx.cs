using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UBUSecret;
using Interfaces;
using Data;
using Util;
namespace Web
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ErrMail.Visible = false;
            ErrPassw.Visible = false;

            ValidarCampos();
           
            
        }

        private void ComprobarSesion()
        {
            ICapaDatos bd = Data.DBPruebas.ObtenerInstacia();

            Usuario usuario = (Usuario)Session["usuario"];

            if (usuario != null)
            {
                // Redirige a otra página
                
            }
            Stats.Text = bd.ToString() + usuario?.ToString();
        }


        private bool ValidarCampos()
        {
            bool esValido = true;

            if (BoxMail.Text.Length > 0)
            {
                if (!Validar.Email(BoxMail.Text)){
                    ErrMail.Text = "Formato Email Incorrecto";
                    ErrMail.Visible = true;
                    esValido = false;
                }
            }
            else
            {
                esValido = false;
            }

            if (BoxPassw.Text.Length == 0)
            {
                esValido = false;
            }


            return esValido;
        }

        protected void Send_Click(object sender, EventArgs e)
        {
            
            if (ValidarCampos())
            {
                ICapaDatos bd = Data.DBPruebas.ObtenerInstacia();
                Usuario u = bd.LeerUsuario(BoxMail.Text);

                if (u == null)
                {
                    ErrMail.Text = "El usuario no existe";
                    ErrMail.Visible = true;
                    return;
                }

                if (!u.ComprobarPassword(BoxPassw.Text))
                {
                    ErrPassw.Text = "La contraseña es incorrecta";
                    ErrPassw.Visible = true;
                    return;
                }
                
                
                Session["usuario"] = u;
                ComprobarSesion();
                
            }
        }
    }


}