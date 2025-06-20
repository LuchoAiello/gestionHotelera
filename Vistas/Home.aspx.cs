using Entidades;
using Negocio;
using System;
using System.Configuration;
using System.Data;
using System.Web.UI;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace Vistas
{
    public partial class Home : System.Web.UI.Page
    {
        NegocioHistorialReservas negocioHistorialReservas = new NegocioHistorialReservas();
        NegocioUsuario negocioUsuario = new NegocioUsuario();
        NegocioMetodoPagos negocioMetodoPago = new NegocioMetodoPagos();
        NegocioServicios negocioServicio = new NegocioServicios();
       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string rol = Session["RolLogin"]?.ToString();

                if (rol == "Administrador")
                {
                    btnPanelAdministrativo.Visible = true;
                }
                else if (rol == "Recepcionista")
                {
                    btnPanelAdministrativo.Visible = false;
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
            panelAdministrativo.Visible = false;
            panelReservas.Visible = false;
            panelHistorialReservas.Visible = false;
            panelHabitaciones.Visible = false;
            // Agregá más paneles si sumás nuevas secciones
        }

        private void OcultarTodosLosPanelesAdmin()
        {
            panelUsuario.Visible = false;
            panelMetodoPago.Visible = false;
            panelServicios.Visible = false;
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Remove("NameLogin");
            Response.Redirect("Login.aspx");

        }

        protected void btnPanelAdministrativo_Click(object sender, EventArgs e)
        {
            OcultarTodosLosPaneles();
            panelAdministrativo.Visible = true;
            lblSeccionTitulo.Text = "Panel Administrativo";
            // Aquí podés cambiar el DataSource si es necesario o mostrar otro contenido
            //gvReservas.DataSource = null;
            //gvReservas.DataBind();
        }

        protected void btnRegisterHuesped_Click(object sender, EventArgs e)
        {
            OcultarTodosLosPaneles();
            lblSeccionTitulo.Text = "Huesped";
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

        // ---------- PANEL USUARIO ----------
        protected void btnUsuario_Click(object sender, EventArgs e)
        {
            OcultarTodosLosPanelesAdmin();
            panelUsuario.Visible = true;

            getDataUser();

        }

        protected void grvUsuario_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow &&
                (e.Row.RowState & DataControlRowState.Edit) > 0)
            {
                DropDownList ddlRol = (DropDownList)e.Row.FindControl("ddpEIRol");
                if (ddlRol != null)
                {         
                    ddlRol.Items.Add(new ListItem("Administrador", "Administrador"));
                    ddlRol.Items.Add(new ListItem("Recepcionista", "Recepcionista"));

                    string rolActual = DataBinder.Eval(e.Row.DataItem, "Rol").ToString();

                    ListItem item = ddlRol.Items.FindByValue(rolActual);
                    if (item != null)
                    {
                        ddlRol.ClearSelection();
                        item.Selected = true;
                    }
                }
            }
        }

        protected void grvUsuario_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grvUsuario.EditIndex = e.NewEditIndex;
            getDataUser();
        }

        protected void grvUsuario_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grvUsuario.EditIndex = -1;
            getDataUser();
        }

        protected void grvUsuario_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int id = Convert.ToInt32(grvUsuario.DataKeys[e.RowIndex].Value);
            string nombre = ((TextBox)grvUsuario.Rows[e.RowIndex].FindControl("txtEINombre")).Text;
            string contrasenia = ((TextBox)grvUsuario.Rows[e.RowIndex].FindControl("txtEIContrasenia")).Text;
            string rol = ((DropDownList)grvUsuario.Rows[e.RowIndex].FindControl("ddpEIRol")).SelectedValue;
            bool estadoBool = ((CheckBox)grvUsuario.Rows[e.RowIndex].FindControl("chkEIEstado")).Checked;
            int estado = estadoBool ? 1 : 0;

            Entidades.Usuario userMod = Entidades.Usuario.ModificarUsuario(id, nombre, contrasenia, rol, estado);
            negocioUsuario.ModificarUsuario(userMod);

            grvUsuario.EditIndex = -1;
            getDataUser();
        }

        protected void btnRegisterUser_Click(object sender, EventArgs e)
        {
            string nombre = txtName.Text.Trim();
            string contrasenia = txtPassword.Text.Trim();
            string rol = ddlRolUsuario.SelectedValue.Trim();

            if (string.IsNullOrEmpty(nombre))
            {
                // Mostrar mensaje de error
                lblMensajeNombre.Text = "El campo Nombre es obligatorio.";
                lblMensajePassword.Text = "";
                lblMensajeNombre.CssClass = "text-danger";
                return;
            }

            if (string.IsNullOrEmpty(contrasenia))
            {
                // Mostrar mensaje de error
                lblMensajePassword.Text = "El campo Contraseña es obligatorio.";
                lblMensajeNombre.Text = "";
                lblMensajePassword.CssClass = "text-danger";
                return;
            }

            Entidades.Usuario userCreate = Entidades.Usuario.CrearUsuario(nombre, contrasenia, rol);
            negocioUsuario.CrearUsuario(userCreate);

            getDataUser();


            txtName.Text = "";
            txtPassword.Text = "";
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtName.Text = "";
            txtPassword.Text = "";
            lblMensajePassword.Text = "";
            lblMensajeNombre.Text = "";
        }

        private void getDataUser()
        {
            DataTable Usuarios = negocioUsuario.GetUser();
            grvUsuario.DataSource = Usuarios;
            grvUsuario.DataBind();
        }

        // ---------- PANEL METODO DE PAGO ----------

        protected void btnMetodoPago_Click(object sender, EventArgs e)
        {
            OcultarTodosLosPanelesAdmin();
            panelMetodoPago.Visible = true;

            getDataMetodoPago();
        }
     
        protected void grvMetodoPago_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grvMetodoPago.EditIndex = e.NewEditIndex;
            getDataMetodoPago();
        }

        protected void grvMetodoPago_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grvMetodoPago.EditIndex = -1;
            getDataMetodoPago();
        }

        protected void grvMetodoPago_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int id = Convert.ToInt32(grvMetodoPago.DataKeys[e.RowIndex].Value);
            string nombre = ((TextBox)grvMetodoPago.Rows[e.RowIndex].FindControl("txtEINombrePago")).Text;
            bool estadoBool = ((CheckBox)grvMetodoPago.Rows[e.RowIndex].FindControl("chkEIEstadoPago")).Checked;
            int estado = estadoBool ? 1 : 0;

            Entidades.MetodoPago metodoPago = Entidades.MetodoPago.ModificarMetodoPago(id,nombre, estado);
            negocioMetodoPago.ModificarMetodoPago(metodoPago);

            grvMetodoPago.EditIndex = -1;
            getDataMetodoPago();
        }

        protected void btnAgregarPago_Click(object sender, EventArgs e)
        {
            string nombre = txtNameMetodoPago.Text.Trim();

            if (string.IsNullOrEmpty(nombre))
            {
                // Mostrar mensaje de error
                lblNameMetodoPago.Text = "El campo Nombre es obligatorio.";
                lblNameMetodoPago.CssClass = "text-danger";
                return;
            }

            Entidades.MetodoPago crearMetodoPago = Entidades.MetodoPago.CrearMetodoPago(nombre);
            negocioMetodoPago.CrearMetodoPago(crearMetodoPago);

            getDataMetodoPago();


            txtNameMetodoPago.Text = "";
            lblNameMetodoPago.Text = "";
        }

        protected void btnLimpiarMetodoPago_Click(object sender, EventArgs e)
        {
            txtNameMetodoPago.Text = "";
            lblNameMetodoPago.Text = "";
        }

        private void getDataMetodoPago()
        {
            DataTable MetodoPagos = negocioMetodoPago.GetMetodoPagos();
            grvMetodoPago.DataSource = MetodoPagos;
            grvMetodoPago.DataBind();
        }

      

        // ---------- PANEL SERVICIOS ----------
        protected void btnServicios_Click(object sender, EventArgs e)
        {
            OcultarTodosLosPanelesAdmin();
            panelServicios.Visible = true;

            getDataServicio();
        }

  
        protected void grvServicio_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grvServicio.EditIndex = e.NewEditIndex;
            getDataServicio();
        }

        protected void grvServicio_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grvServicio.EditIndex = -1;
            getDataServicio();
        }

        protected void grvServicio_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int id = Convert.ToInt32(grvServicio.DataKeys[e.RowIndex].Value);
            string nombre = ((TextBox)grvServicio.Rows[e.RowIndex].FindControl("txtEINombreServicio")).Text;
            string precioTexto = ((TextBox)grvServicio.Rows[e.RowIndex].FindControl("txtEIPrecio")).Text.Trim();
            decimal precio = Convert.ToDecimal(precioTexto);
            bool estadoBool = ((CheckBox)grvServicio.Rows[e.RowIndex].FindControl("chkEIEstadoServicio")).Checked;
            int estado = estadoBool ? 1 : 0;

            Entidades.Servicios servicio = Entidades.Servicios.ModificarServicio(id, nombre, precio, estado);
            negocioServicio.ModificarServicio(servicio);

            grvServicio.EditIndex = -1;
            getDataServicio();
        }

        protected void grvServicio_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvServicio.PageIndex = e.NewPageIndex;
            getDataServicio();
        }

        protected void btnAgregarServicio_Click(object sender, EventArgs e)
        {
            string nombre = txtNameServicio.Text.Trim();
            string precioTexto = txtPrecio.Text.Trim();

            if (string.IsNullOrEmpty(nombre))
            {
                lblNameServicio.Text = "El campo Nombre es obligatorio.";
                lblNamePrecio.Text = "";
                lblNameServicio.CssClass = "text-danger";
                return;
            }

            if (string.IsNullOrEmpty(precioTexto))
            {
                lblNamePrecio.Text = "El campo Precio es obligatorio.";
                lblNameServicio.Text = "";
                lblNamePrecio.CssClass = "text-danger";
                return;
            }

            decimal precio;
            if (!decimal.TryParse(precioTexto, out precio))
            {
                lblNamePrecio.Text = "El precio debe ser un número válido.";
                lblNameServicio.Text = "";
                lblNamePrecio.CssClass = "text-danger";
                return;
            }

            Entidades.Servicios servicio = Entidades.Servicios.CrearServicios(nombre, precio);
            negocioServicio.CrearServicio(servicio);

            getDataServicio();

            txtNameServicio.Text = "";
            txtPrecio.Text = "";
            lblNameServicio.Text = "";
            lblNamePrecio.Text = "";
        }

        protected void btnLimpiarServicio_Click(object sender, EventArgs e)
        {
            txtNameServicio.Text = "";
            txtPrecio.Text = "";
            lblNameServicio.Text = "";
            lblNamePrecio.Text = "";
        }
        private void getDataServicio()
        {
            DataTable Servicio = negocioServicio.GetServicios();
            grvServicio.DataSource = Servicio;
            grvServicio.DataBind();
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