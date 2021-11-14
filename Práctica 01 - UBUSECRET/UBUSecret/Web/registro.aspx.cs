using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Util;
using Interfaces;
using Data;
using UBUSecret;

namespace Web
{
    public partial class registro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuario"] != null)
            {
                Server.Transfer("default.aspx");
            }

            ErrMail.Visible = false;
            ErrPassw.Visible = false;
            ErrSurName.Visible = false;
            ErrName.Visible = false;
            ErrPassw.Visible = false;
            ErrPassw2.Visible = false;
            ErrPhone.Visible = false;
        }


        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            Server.Transfer("default.aspx", true);
        }

        protected void BtnSend_Click(object sender, EventArgs e)
        {
            ICapaDatos bd = Data.DBPruebas.ObtenerInstacia();

            if (Valida() && bd.LeerUsuario(BoxMail.Text)==null) {
                Usuario nuevoUsuario = new Usuario(TextBoxName.Text,TextBoxSurName.Text,BoxMail.Text,TextPhone.Text,TextPhone.Text);
                bd.InsertarUsuario(nuevoUsuario);
                Session["usuario"] = nuevoUsuario;
                Server.Transfer("default.aspx", true);

            }


        }

        private bool Valida() {
            bool email = validarEmail();
            bool name = validarNombreApellido();
            bool cont = validarContraseña();
            bool tel = validarTelefono();

            return email && cont && name && tel;
        
        }

        private bool validarEmail() {
            if (!Validar.Email(BoxMail.Text))
            {
                ErrMail.Visible = true;
                return false;
            }
            return true;
        }

        private bool validarContraseña()
        {

            if (BoxPassw.Text.Length > 0 || BoxPassw2.Text.Length > 0)
            {
                bool e = true;
                bool i = true;
                if (!Validar.Password(BoxPassw.Text))
                {
                    ErrPassw.Text = "La contraseña no es lo suficientemente fuerte";
                    ErrPassw.Visible = true;
                    e = false;
                }


                if (!BoxPassw.Text.Equals(BoxPassw2.Text))
                {
                    ErrPassw2.Text = "Las contraseñas no coinciden";
                    ErrPassw2.Visible = true;
                    i = false;
                }

                return e && i ;
            }
            return false;



        }
       
        private bool validarTelefono() {

            try
            {
                Validar.Telefono(TextPhone.Text);
               

                
            } catch { ErrPhone.Visible = true;  return false; }

            return true;

        }

        private bool validarNombreApellido() {
            if (TextBoxSurName.Text.Length == 0) {
                ErrSurName.Visible = true;
                return false;
            }

            if (TextBoxName.Text.Length == 0) {
                ErrName.Visible = true;
                return false;
            }

            return true;


        }
    }
}