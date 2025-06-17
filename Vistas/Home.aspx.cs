using System;

namespace Vistas
{
    public partial class Home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["NameLogin"] != null)
            {
                lblNameLogin.Text = "Usuario: " + Session["NameLogin"].ToString() + ", " + Session["RolLogin"].ToString();
            } else
            {
                Response.Redirect("Login.aspx");
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Remove("NameLogin");
            Response.Redirect("Login.aspx");

        }

        protected void btnRegisterUser_Click(object sender, EventArgs e)
        {
            lblSeccionTitulo.Text = "Registrar Usuario";
            // Aquí podés cambiar el DataSource si es necesario o mostrar otro contenido
            //gvReservas.DataSource = null;
            //gvReservas.DataBind();
        }

        protected void btnHistorialReservas_Click(object sender, EventArgs e)
        {
            lblSeccionTitulo.Text = "Historial de Reservas";
            //gvReservas.DataSource = ObtenerHistorialReservas(); 
            //gvReservas.DataBind();
        }
        protected void btnRooms_Click(object sender, EventArgs e)
        {
            lblSeccionTitulo.Text = "Estado de Habitaciones";
            //gvReservas.DataSource = ObtenerEstadoHabitaciones(); // método que trae habitaciones
            //gvReservas.DataBind();
        }

        protected void btnReserv_Click(object sender, EventArgs e)
        {
            lblSeccionTitulo.Text = "Crear Reserva";
            //gvReservas.DataSource = ObtenerDatosReserva(); // Método que devuelve una lista o DataTable
            //gvReservas.DataBind();
        }
    }
}