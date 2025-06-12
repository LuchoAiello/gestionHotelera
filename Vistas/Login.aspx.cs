using Entidades;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Vistas
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        NegocioLogin negocioLogin = new NegocioLogin();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            loggedUser login = negocioLogin.Login(txtNameLogin.Text, txtPasswordLogin.Text);

            if (login != null)
            {
                txtError.Text = "";
                Session["NameLogin"] = login.Nombre; 
                Session["RolLogin"] = login.Rol;
                Response.Redirect("Home.aspx");
            }
            else
            {
                txtError.Text = "Las credenciales son Incorrectas";
            }
        }
    }
}