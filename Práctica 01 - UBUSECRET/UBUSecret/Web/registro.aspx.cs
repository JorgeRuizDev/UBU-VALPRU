using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web
{
    public partial class registro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuario"] != null)
            {
                Server.Transfer("");
            }

            ErrMail.Visible = false;
            ErrPassw.Visible = false;
        }


        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            Server.Transfer("default.aspx", true);
        }

        protected void BtnSend_Click(object sender, EventArgs e)
        {
            // Valida y Registra;

        }
    }
}