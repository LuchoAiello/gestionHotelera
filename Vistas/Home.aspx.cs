using Entidades;
using Negocio;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace Vistas
{
    public partial class Home : System.Web.UI.Page
    {
        NegocioHistorialReservas negocioHistorialReservas = new NegocioHistorialReservas();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string rol = Session["RolLogin"]?.ToString();

                if (rol == "Administrador")
                {
                    btnRegisterUser.Visible = true;
                }
                else if (rol == "Recepcionista")
                {
                    btnRegisterUser.Visible = false;
                }

                btnRegisterHuesped.Visible = true;
                btnReserv.Visible = true;
                btnRooms.Visible = true;
                btnHistorialReservas.Visible = true;

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

        protected void grvHistorialReservas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvHistorialReservas.PageIndex = e.NewPageIndex;

            // Reobtener datos para mostrar en la nueva página
            string numeroHabitacion = txtNumber.Text;
            string fechaDesde = txtDateFrom.Text;
            string fechaHasta = txtDateTo.Text;

            DataTable HistorialReservas = negocioHistorialReservas.GetFilterHistorialReserva(
                numeroHabitacion, fechaDesde, fechaHasta);

            grvHistorialReservas.DataSource = HistorialReservas;
            grvHistorialReservas.DataBind();
        }

        private void OcultarTodosLosPaneles()
        {
            panelReservas.Visible = false;
            panelHistorialReservas.Visible = false;
            panelHabitaciones.Visible = false;
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

        protected void btnRegisterHuesped_Click(object sender, EventArgs e)
        {
            OcultarTodosLosPaneles();
            lblSeccionTitulo.Text = "Registrar Huesped";
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
            panelHabitaciones.Visible = true;
            CargarHabitaciones();
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
        protected void btnNuevaHabitacion_Click(object sender, EventArgs e)
        {
            LimpiarFormularioHabitacion();
            panelFormularioHabitacion.Visible = true;
        }
        protected void btnCancelarHabitacion_Click(object sender, EventArgs e)
        {
            panelFormularioHabitacion.Visible = false;
            LimpiarFormularioHabitacion();
        }

        protected void btnGuardarHabitacion_Click(object sender, EventArgs e)
        {
            Habitacion h = new Habitacion
            {
                Id_habitacion = string.IsNullOrEmpty(hfIdHabitacion.Value) ? 0 : Convert.ToInt32(hfIdHabitacion.Value),
                NumeroHabitacion = int.Parse(txtNumero.Text),
                Tipo = txtTipo.Text,
                Capacidad = int.Parse(txtCapacidad.Text),
                Estado = ddlEstado.SelectedValue,
                Precio = decimal.Parse(txtPrecio.Text),
                Descripcion = txtDescripcion.Text
            };

            HabitacionesService repo = new HabitacionesService();

            if (h.Id_habitacion == 0)
                repo.Insert(h);
            else
                repo.Update(h);

            panelFormularioHabitacion.Visible = false;
            LimpiarFormularioHabitacion();
            CargarHabitaciones();
        }

        protected void gvHabitaciones_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            int id = Convert.ToInt32(gvHabitaciones.DataKeys[index].Value);
            GridViewRow row = gvHabitaciones.Rows[index];
            var repo = new HabitacionesService();
            if (e.CommandName == "Editar")
            {
                hfIdHabitacion.Value = id.ToString();
                txtNumero.Text = row.Cells[0].Text;
                txtTipo.Text = row.Cells[1].Text;
                txtCapacidad.Text = row.Cells[2].Text;
                string valor = row.Cells[3].Text.Trim();
                ddlEstado.SelectedValue = ddlEstado.Items.FindByValue(valor) != null ? valor : "Activo";
                txtPrecio.Text = row.Cells[4].Text;
                txtDescripcion.Text = row.Cells[5].Text;
            }
            else if (e.CommandName == "Desactivar")
            {
                Habitacion h = new Habitacion
                {
                    Id_habitacion = id,
                    NumeroHabitacion = int.Parse(row.Cells[0].Text),
                    Tipo = row.Cells[1].Text,
                    Capacidad = int.Parse(row.Cells[2].Text),
                    Estado = "Inactiva",
                    Precio = decimal.Parse(row.Cells[4].Text),
                    Descripcion = row.Cells[5].Text
                };

                repo.Update(h);
                CargarHabitaciones();
            }
        }
        private void LimpiarFormularioHabitacion()
        {
            hfIdHabitacion.Value = "";
            txtNumero.Text = "";
            txtTipo.Text = "";
            txtCapacidad.Text = "";
            txtPrecio.Text = "";
            txtDescripcion.Text = "";
            ddlEstado.SelectedValue = "Activa";
        }

        private void CargarHabitaciones()
        {
            var habitacionesService = new HabitacionesService();
            DataTable dt = habitacionesService.GetAll();
            gvHabitaciones.DataSource = dt;
            gvHabitaciones.DataBind();
        }
    }
}