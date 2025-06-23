using Entidades;
using Negocio;
using Negocio.Negocio;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Vistas
{
    public partial class Home : System.Web.UI.Page
    {
        NegocioHistorialReservas negocioHistorialReservas = new NegocioHistorialReservas();
        NegocioUsuario negocioUsuario = new NegocioUsuario();
        NegocioMetodoPagos negocioMetodoPago = new NegocioMetodoPagos();
        NegocioServicios negocioServicio = new NegocioServicios();
        NegocioHuespedes negocioHuesped = new NegocioHuespedes();

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
                btnHistorialReservas.Visible = true;
                btnRooms.Visible = true;

                OcultarTodosLosPaneles();
                lblSeccionTitulo.Text = "Lista de Reservas del Hotel";
            }

            if (Session["NameLogin"] != null)
            {
                lblNameLogin.Text = "Usuario: " + Session["NameLogin"] + ", " + Session["RolLogin"];
            }
            else
            {
                //Response.Redirect("Login.aspx");
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
            panelHistorialReservas.Visible = false;
            panelHabitaciones.Visible = false;
            panelHuespedes.Visible = false;
            panelRegistrarHuesped.Visible = false;
            OcultarTodosLosPanelesAdmin();
            // Agregá más paneles si sumás nuevas secciones
        }

        private void OcultarTodosLosPanelesAdmin()
        {
            panelUsuario.Visible = false;
            panelMetodoPago.Visible = false;
            panelServicios.Visible = false;
            panelRegistrarUsuario.Visible = false;
            panelRegistrarMetodo.Visible = false;
            panelRegistrarServicio.Visible = false;
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
            panelUsuario.Visible = true;
            lblSeccionTitulo.Text = "Panel Administrativo";

            getDataUser();
        }

        #region Panel Huesped
        protected void btnRegisterHuesped_Click(object sender, EventArgs e)
        {
            OcultarTodosLosPaneles();
            lblSeccionTitulo.Text = "Huesped";
            panelHuespedes.Visible = true;

            getDataHuesped();
        }

        protected void btnNuevoHuesped_Click(object sender, EventArgs e)
        {
            panelRegistrarHuesped.Visible = true;

            getDataHuesped();
        }

        private void getDataHuesped()
        {
            DataTable Huesped = negocioHuesped.GetHuespedes();
            grvHuespedes.DataSource = Huesped;
            grvHuespedes.DataBind();
        }

        protected void grvHuespedes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvHuespedes.PageIndex = e.NewPageIndex;
            getDataHuesped();
        }

        protected void grvHuespedes_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grvHuespedes.EditIndex = e.NewEditIndex;
            getDataHuesped();
        }

        protected void grvHuespedes_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grvHuespedes.EditIndex = -1;
            getDataHuesped();
        }

        protected void grvHuespedes_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int id = Convert.ToInt32(grvHuespedes.DataKeys[e.RowIndex].Value);
            string nombre = ((TextBox)grvHuespedes.Rows[e.RowIndex].FindControl("txtEINombreHuesped")).Text;
            string apellido = ((TextBox)grvHuespedes.Rows[e.RowIndex].FindControl("txtEIApellido")).Text;
            string documento = ((TextBox)grvHuespedes.Rows[e.RowIndex].FindControl("txtEIDocumento")).Text;
            string tipoDocumento = ((TextBox)grvHuespedes.Rows[e.RowIndex].FindControl("txtEITipoDocumento")).Text;
            string email = ((TextBox)grvHuespedes.Rows[e.RowIndex].FindControl("txtEIEmail")).Text;
            string telefono = ((TextBox)grvHuespedes.Rows[e.RowIndex].FindControl("txtEITelefono")).Text;
            DateTime fechaNacimiento = Convert.ToDateTime(((TextBox)grvHuespedes.Rows[e.RowIndex].FindControl("txtEIFechaNacimiento")).Text);
            bool estadoBool = ((CheckBox)grvHuespedes.Rows[e.RowIndex].FindControl("chkEIEstadoServicio")).Checked;
            int estado = estadoBool ? 1 : 0;

            Huespedes huesped = Huespedes.ModificarHuesped(id, nombre, apellido, documento, tipoDocumento, email, telefono, fechaNacimiento, estado);
            negocioHuesped.ModificarHuesped(huesped);

            grvHuespedes.EditIndex = -1;
            getDataHuesped();
        }

        protected void btnRegistrarHuesped_Click(object sender, EventArgs e)
        {
            string nombre = txtNombreHuesped.Text.Trim();
            string apellido = txtApellidoHuesped.Text.Trim();
            string documento = txtDocumentoHuesped.Text.Trim();
            string tipoDocumento = ddlTipoDocumentoHuesped.SelectedValue.Trim();
            string email = txtEmailHuesped.Text.Trim();
            string telefono = txtTelefonoHuesped.Text.Trim();
            string fechaNacimientoTexto = txtFechaNacimientoHuesped.Text.Trim();
            DateTime fechaNacimiento;

            // Validación: Nombre
            if (string.IsNullOrEmpty(nombre))
            {
                lblNombreHuesped.Text = "El campo Nombre es obligatorio.";
                lblNombreHuesped.CssClass = "text-danger";
                return;
            }
            else
            {
                lblNombreHuesped.Text = "";
            }

            // Validación: Apellido
            if (string.IsNullOrEmpty(apellido))
            {
                lblApellidoHuesped.Text = "El campo Apellido es obligatorio.";
                lblApellidoHuesped.CssClass = "text-danger";
                return;
            }
            else
            {
                lblApellidoHuesped.Text = "";
            }

            // Validación: Documento
            if (string.IsNullOrEmpty(documento))
            {
                lblDocumentoHuesped.Text = "El campo Documento es obligatorio.";
                lblDocumentoHuesped.CssClass = "text-danger";
                return;
            }
            else
            {
                lblDocumentoHuesped.Text = "";
            }

            // Validación: Tipo de Documento
            if (string.IsNullOrEmpty(tipoDocumento))
            {
                lblTipoDocumentoHuesped.Text = "Debe seleccionar un tipo de documento.";
                lblTipoDocumentoHuesped.CssClass = "text-danger";
                return;
            }
            else
            {
                lblTipoDocumentoHuesped.Text = "";
            }

            // Validación: Email
            if (string.IsNullOrEmpty(email))
            {
                lblEmailHuesped.Text = "El campo Email es obligatorio.";
                lblEmailHuesped.CssClass = "text-danger";
                return;
            }
            else if (!System.Text.RegularExpressions.Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                lblEmailHuesped.Text = "El formato del email no es válido.";
                lblEmailHuesped.CssClass = "text-danger";
                return;
            }
            else
            {
                lblEmailHuesped.Text = "";
            }

            // Validación: Teléfono
            if (string.IsNullOrEmpty(telefono))
            {
                lblTelefonoHuesped.Text = "El campo Teléfono es obligatorio.";
                lblTelefonoHuesped.CssClass = "text-danger";
                return;
            }
            else
            {
                lblTelefonoHuesped.Text = "";
            }

            // Validación: Fecha de Nacimiento
            if (string.IsNullOrEmpty(fechaNacimientoTexto))
            {
                lblFechaNacimientoHuesped.Text = "Debe ingresar la fecha de nacimiento.";
                lblFechaNacimientoHuesped.CssClass = "text-danger";
                return;
            }
            else if (!DateTime.TryParse(fechaNacimientoTexto, out fechaNacimiento))
            {
                lblFechaNacimientoHuesped.Text = "La fecha ingresada no es válida.";
                lblFechaNacimientoHuesped.CssClass = "text-danger";
                return;
            }
            else
            {
                lblFechaNacimientoHuesped.Text = "";
            }

            Huespedes huesped = Huespedes.CrearHuesped(nombre, apellido, documento, tipoDocumento, email, telefono, fechaNacimiento);
            negocioHuesped.CrearHuesped(huesped);

            getDataHuesped();

            txtNombreHuesped.Text = "";
            txtApellidoHuesped.Text = "";
            txtDocumentoHuesped.Text = "";
            ddlTipoDocumentoHuesped.SelectedValue = "Dni";
            txtEmailHuesped.Text = "";
            txtTelefonoHuesped.Text = "";
            txtFechaNacimientoHuesped.Text = "";
        }

        protected void btnLimpiarHuesped_Click(object sender, EventArgs e)
        {
            panelRegistrarHuesped.Visible = false;

            txtNombreHuesped.Text = "";
            txtApellidoHuesped.Text = "";
            txtDocumentoHuesped.Text = "";
            ddlTipoDocumentoHuesped.SelectedValue = "Dni";
            txtEmailHuesped.Text = "";
            txtTelefonoHuesped.Text = "";
            txtFechaNacimientoHuesped.Text = "";
        }
    
        #endregion

        #region Panel Reservas
        protected void grvHistorialReservas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Editar")
            {
                //int idReserva = Convert.ToInt32(e.CommandArgument);
            }
            else if (e.CommandName == "Eliminar")
            {
                //int idReserva = Convert.ToInt32(e.CommandArgument);
                //EliminarReserva(idReserva); // Por ejemplo
                //CargarReservas(); // Recargás la grilla
            }
        }
        protected void btnCrearReserva_Click(object sender, EventArgs e)
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

        protected void btnHistorialReservas_Click(object sender, EventArgs e)
        {
            OcultarTodosLosPaneles();
            panelHistorialReservas.Visible = true;
            lblSeccionTitulo.Text = "Historial de Reservas";

            DataTable HistorialReservas = negocioHistorialReservas.GetHistorialReserva();
            grvHistorialReservas.DataSource = HistorialReservas;
            grvHistorialReservas.DataBind();
        }
        #endregion

        #region Panel de Habitaciones
        private void CargarHabitaciones()
        {
            var habitacionesService = new HabitacionesService();
            DataTable dt = habitacionesService.GetAll();
            gvHabitaciones.DataSource = dt;
            gvHabitaciones.DataBind();
        }

        protected void grvHabitaciones_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = gvHabitaciones.Rows[e.RowIndex];

            var habitacion = new Habitacion
            {
                Id_habitacion = Convert.ToInt32(gvHabitaciones.DataKeys[e.RowIndex].Value.ToString()),
                NumeroHabitacion = int.Parse(((TextBox)row.FindControl("txtNumeroHab")).Text),
                Tipo = ((TextBox)row.FindControl("txtTipoHab")).Text,
                Capacidad = int.Parse(((TextBox)row.FindControl("txtCapacidad")).Text),
                Estado = ((DropDownList)row.FindControl("ddlEIEstado")).SelectedValue,
                Precio = decimal.Parse(((TextBox)row.FindControl("txtPrecioHab")).Text),
                Descripcion = ((TextBox)row.FindControl("txtDescripcionHab")).Text
            };
            var negocio = new HabitacionesService();
            negocio.Update(habitacion);

            gvHabitaciones.EditIndex = -1;
            CargarHabitaciones();
        }

        protected void btnRegistrarHabitacion_Click(object sender, EventArgs e)
        {
            string numero = txtNumeroHab.Text.Trim();
            string tipo = txtTipoHab.Text.Trim();
            string capacidad = txtCapacidad.Text.Trim();
            string precio = txtPrecioHab.Text.Trim();
            string descripcion = txtDescripcionHab.Text.Trim();
            string estado = ddlEstadoHab.SelectedValue;

            if (string.IsNullOrEmpty(numero) || string.IsNullOrEmpty(tipo) || string.IsNullOrEmpty(capacidad) || string.IsNullOrEmpty(precio))
            {
                lblMensajeRegistro.Visible = true;
                lblMensajeRegistro.Text = "Por favor, completá todos los campos obligatorios.";
                return;
            }

            Habitacion nueva = new Habitacion
            {
                NumeroHabitacion = int.Parse(numero),
                Tipo = tipo,
                Capacidad = int.Parse(capacidad),
                Precio = decimal.Parse(precio),
                Descripcion = descripcion,
                Estado = estado
            };

            HabitacionesService service = new HabitacionesService();
            service.Insert(nueva);

            LimpiarFormularioHabitacion();
            panelFormularioRegistro.Visible = false;
            panelListadoHabitaciones.Visible = true;
            CargarHabitaciones();
        }

        protected void gvHabitaciones_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvHabitaciones.PageIndex = e.NewPageIndex;
            CargarHabitaciones();
        }

        private void LimpiarFormularioHabitacion()
        {
            txtNumeroHab.Text = "";
            txtTipoHab.Text = "";
            txtCapacidad.Text = "";
            txtPrecioHab.Text = "";
            txtDescripcionHab.Text = "";
            ddlEstadoHab.SelectedValue = "Activa";
        }

        protected void btnCancelarRegistroHabitacion_Click(object sender, EventArgs e)
        {
            panelFormularioRegistro.Visible = false;
            panelListadoHabitaciones.Visible = true;
        }

        protected void btnMostrarFormularioHabitaciones_Click(object sender, EventArgs e)
        {
            LimpiarFormularioHabitacion();
            panelFormularioRegistro.Visible = true;
            panelListadoHabitaciones.Visible = false;
        }

        protected void btnLimpiarFormularioHabitacion_Click(object sender, EventArgs e)
        {
            LimpiarFormularioHabitacion();
        }

        protected void grvHabitaciones_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            //HabitacionesService service = new HabitacionesService();

            //if (e.CommandName == "Desactivar")
            //{
            //    int index = Convert.ToInt32(e.CommandArgument);
            //    GridViewRow row = gvHabitaciones.Rows[index];
            //    int id = Convert.ToInt32(gvHabitaciones.DataKeys[index].Value);
            //    bool resultado = service.Delete(id);

            //    if (resultado)
            //    {  
            //        lblMensaje.Text = $"Habitación con ID {id} desactivada.";
            //    }
            //    else
            //    {
            //        lblMensaje.Text = "No se pudo desactivar la habitación.";
            //    }
            //    CargarHabitaciones();
            //}
        }

        protected void grvHabitaciones_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && gvHabitaciones.EditIndex == e.Row.RowIndex)
            {
                DropDownList ddlEstado = (DropDownList)e.Row.FindControl("ddlEIEstado");
                ddlEstado.Items.Clear();
                ddlEstado.Items.Add("Activa");
                ddlEstado.Items.Add("Inactiva");
                ddlEstado.Items.Add("Mantenimiento");

                string estadoActual = DataBinder.Eval(e.Row.DataItem, "Estado").ToString();
                ddlEstado.SelectedValue = estadoActual;
            }
        }

        protected void grvHabitaciones_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvHabitaciones.EditIndex = e.NewEditIndex;
            CargarHabitaciones();
        }

        protected void grvHabitaciones_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvHabitaciones.EditIndex = -1;
            CargarHabitaciones();
        }

        protected void txtBuscarHabitacion_TextChanged(object sender, EventArgs e)
        {
            string filtro = txtBuscarHabitacion.Text.Trim().ToLower();
            HabitacionesService habitacionService = new HabitacionesService();
            DataTable resultado = (string.IsNullOrEmpty(filtro)) ? habitacionService.GetAll() : habitacionService.GetByFilter(filtro);
            gvHabitaciones.DataSource = resultado;
            gvHabitaciones.DataBind();
        }

        protected void btnHabitaciones_Click(object sender, EventArgs e)
        {
            OcultarTodosLosPaneles();
            lblSeccionTitulo.Text = "Estado de Habitaciones";
            panelHabitaciones.Visible = true;
            CargarHabitaciones();
        }
        #endregion

        #region Panel Usuario
        protected void btnUsuario_Click(object sender, EventArgs e)
        {
            OcultarTodosLosPanelesAdmin();
            panelUsuario.Visible = true;

            getDataUser();

        }

        protected void btnNuevoUsuario_Click(object sender, EventArgs e)
        {
            OcultarTodosLosPanelesAdmin();
            panelUsuario.Visible = true;
            panelRegistrarUsuario.Visible = true;
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

            Usuario userMod = Usuario.ModificarUsuario(id, nombre, contrasenia, rol, estado);
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

            Usuario userCreate = Usuario.CrearUsuario(nombre, contrasenia, rol);
            negocioUsuario.CrearUsuario(userCreate);

            getDataUser();

            txtName.Text = "";
            txtPassword.Text = "";
        }

        protected void grvUsuario_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvUsuario.PageIndex = e.NewPageIndex;
            getDataUser();
        }

        protected void btnCancelarUser_Click(object sender, EventArgs e)
        {
            txtName.Text = "";
            txtPassword.Text = "";
            lblMensajePassword.Text = "";
            lblMensajeNombre.Text = "";
            OcultarTodosLosPanelesAdmin();
            panelUsuario.Visible = true;
            panelRegistrarUsuario.Visible = false;
        }

        private void getDataUser()
        {
            DataTable Usuarios = negocioUsuario.GetUser();
            grvUsuario.DataSource = Usuarios;
            grvUsuario.DataBind();
        }
        #endregion

        #region Panel Metodo de Pago
        protected void btnMetodoPago_Click(object sender, EventArgs e)
        {
            OcultarTodosLosPanelesAdmin();
            panelMetodoPago.Visible = true;

            getDataMetodoPago();
        }

        protected void btnNuevoMetodo_Click(object sender, EventArgs e)
        {
            OcultarTodosLosPanelesAdmin();
            panelMetodoPago.Visible = true;
            panelRegistrarMetodo.Visible = true;
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

            MetodoPago metodoPago = MetodoPago.ModificarMetodoPago(id, nombre, estado);
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

            MetodoPago crearMetodoPago = MetodoPago.CrearMetodoPago(nombre);
            negocioMetodoPago.CrearMetodoPago(crearMetodoPago);

            getDataMetodoPago();


            txtNameMetodoPago.Text = "";
            lblNameMetodoPago.Text = "";
        }

        protected void grvMetodoPago_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvMetodoPago.PageIndex = e.NewPageIndex;
            getDataMetodoPago();
        }

        protected void btnCancelarMetodoPago_Click(object sender, EventArgs e)
        {
            OcultarTodosLosPanelesAdmin();
            panelMetodoPago.Visible = true;
            panelRegistrarMetodo.Visible = false;
            txtNameMetodoPago.Text = "";
            lblNameMetodoPago.Text = "";
        }

        private void getDataMetodoPago()
        {
            DataTable MetodoPagos = negocioMetodoPago.GetMetodoPagos();
            grvMetodoPago.DataSource = MetodoPagos;
            grvMetodoPago.DataBind();
        }
        #endregion

        #region Panel Servicios
        protected void btnServicios_Click(object sender, EventArgs e)
        {
            OcultarTodosLosPanelesAdmin();
            panelServicios.Visible = true;

            getDataServicio();
        }

        protected void btnNuevoServicio_Click(object sender, EventArgs e)
        {
            OcultarTodosLosPanelesAdmin();
            panelServicios.Visible = true;
            panelRegistrarServicio.Visible = true;
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

            Servicios servicio = Servicios.ModificarServicio(id, nombre, precio, estado);
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

            Servicios servicio = Servicios.CrearServicios(nombre, precio);
            negocioServicio.CrearServicio(servicio);

            getDataServicio();

            txtNameServicio.Text = "";
            txtPrecio.Text = "";
            lblNameServicio.Text = "";
            lblNamePrecio.Text = "";
        }

        protected void btnCancelarServicio_Click(object sender, EventArgs e)
        {
            txtNameServicio.Text = "";
            txtPrecio.Text = "";
            lblNameServicio.Text = "";
            lblNamePrecio.Text = "";
            OcultarTodosLosPanelesAdmin();
            panelServicios.Visible = true;
        }

        private void getDataServicio()
        {
            DataTable Servicio = negocioServicio.GetServicios();
            grvServicio.DataSource = Servicio;
            grvServicio.DataBind();
        }
        #endregion


    }
}