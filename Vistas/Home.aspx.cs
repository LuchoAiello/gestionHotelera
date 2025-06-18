using Negocio;
using System;
using System.Data;

namespace Vistas
{
    public partial class Home : System.Web.UI.Page
    {
        NegocioHistorialReservas negocioHistorialReservas = new NegocioHistorialReservas();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                OcultarTodosLosPaneles();
                panelReservas.Visible = true; // Mostrar sección inicial si querés
                lblSeccionTitulo.Text = "Lista de Reservas del Hotel";
            }

            if (Session["NameLogin"] != null)
            {
                lblNameLogin.Text = "Usuario: " + Session["NameLogin"] + ", " + Session["RolLogin"];
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }

        private void OcultarTodosLosPaneles()
        {
            panelReservas.Visible = false;
            panelHistorialReservas.Visible = false;
            // Agregá más paneles si sumás nuevas secciones
        }


        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Remove("NameLogin");
            Response.Redirect("Login.aspx");

        }

        protected void btnRegisterUser_Click(object sender, EventArgs e)
        {
            OcultarTodosLosPaneles();
            lblSeccionTitulo.Text = "Registrar Usuario";
            // Aquí podés cambiar el DataSource si es necesario o mostrar otro contenido
            //gvReservas.DataSource = null;
            //gvReservas.DataBind();
        }

        protected void btnHistorialReservas_Click(object sender, EventArgs e)
        {
            OcultarTodosLosPaneles();
            panelHistorialReservas.Visible = true;
            lblSeccionTitulo.Text = "Historial de Reservas";

            DataTable HistorialReservas = negocioHistorialReservas.GetHistorialReserva();
            grvHistorialReservas.DataSource = HistorialReservas;
            grvHistorialReservas.DataBind();
        }
        protected void btnRooms_Click(object sender, EventArgs e)
        {
            OcultarTodosLosPaneles();
            lblSeccionTitulo.Text = "Estado de Habitaciones";
            //gvReservas.DataSource = ObtenerEstadoHabitaciones(); // método que trae habitaciones
            //gvReservas.DataBind();
        }

        protected void btnReserv_Click(object sender, EventArgs e)
        {
            OcultarTodosLosPaneles();
            lblSeccionTitulo.Text = "Crear Reserva";
            //gvReservas.DataSource = ObtenerDatosReserva(); // Método que devuelve una lista o DataTable
            //gvReservas.DataBind();
        }

        protected void btnFilter_Click(object sender, EventArgs e)
        {
            string numeroHabitacion = txtNumber.Text;
            string fechaDesde = txtDateFrom.Text;
            string fechaHasta = txtDateTo.Text;

            DataTable HistorialReservas = negocioHistorialReservas.GetFilterHistorialReserva(
                numeroHabitacion, fechaDesde, fechaHasta);

            grvHistorialReservas.DataSource = HistorialReservas;
            grvHistorialReservas.DataBind();
        }

        protected void btnSacarFiltro_Click(object sender, EventArgs e)
        {
            DataTable HistorialReservas = negocioHistorialReservas.GetHistorialReserva();
            grvHistorialReservas.DataSource = HistorialReservas;
            grvHistorialReservas.DataBind();
            txtNumber.Text = "";
            txtDateFrom.Text = "";
            txtDateTo.Text = "";
        }
    }
}