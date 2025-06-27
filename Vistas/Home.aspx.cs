using Entidades;
using Negocio;
using Negocio.Negocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using static Entidades.Reserva;

namespace Vistas
{
    public partial class Home : System.Web.UI.Page
    {
        NegocioUsuario negocioUsuario = new NegocioUsuario();
        NegocioMetodoPagos negocioMetodoPago = new NegocioMetodoPagos();
        NegocioServicios negocioServicio = new NegocioServicios();
        NegocioHuespedes negocioHuesped = new NegocioHuespedes();
        NegocioReserva negocioReserva = new NegocioReserva();
        NegocioHabitaciones negocioHabitaciones = new NegocioHabitaciones();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string rol = Session["RolLogin"]?.ToString();

                if (rol == "Administrador")
                {
                    btnPanelAdministrativo.Visible = true;
                    btnRooms.Visible = true;

                }
                else if (rol == "Recepcionista")
                {
                    btnPanelAdministrativo.Visible = false;
                    btnRooms.Visible = false;

                }

                btnRegisterHuesped.Visible = true;
                btnReservas.Visible = true;

                OcultarTodosLosPaneles();
            }

            if (Session["NameLogin"] != null)
            {
                lblNameLogin.Text = "Usuario: " + Session["NameLogin"] + ", " + Session["RolLogin"];
            }
            else
            {
                // Response.Redirect("Login.aspx");
            }
        }

        private void OcultarTodosLosPaneles()
        {
            panelAdministrativo.Visible = false;
            panelAdministarReservas.Visible = false;
            panelHistorialReservas.Visible = false;
            panelHabitaciones.Visible = false;
            panelHuespedes.Visible = false;
            panelRegistrarHuesped.Visible = false;
            panelUsuario.Visible = false;
            panelRegistrarUsuario.Visible = false;
            OcultarTodosLosPanelesAdmin();
            OcultarTodosLosPanelesReserva();
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

        private void OcultarTodosLosPanelesReserva()
        {
            panelHistorialReservas.Visible = false;
            panelReservas.Visible = false;
            panelCrearReservaEtapa1.Visible = false;
            panelCrearReservaEtapa2.Visible = false;
            panelCrearReservaEtapa3.Visible = false;
            panelCrearReservaEtapa4.Visible = false;
        }

        private void ResaltarBotonPrincipal(LinkButton botonSeleccionado)
        {
            btnPanelAdministrativo.CssClass = "nav-link text-white";
            btnRegisterHuesped.CssClass = "nav-link text-white";
            btnRooms.CssClass = "nav-link text-white";
            btnReservas.CssClass = "nav-link text-white";

            botonSeleccionado.CssClass += " link-principal-activo";
        }

        private void ResaltarBotonSeleccionado(LinkButton botonSeleccionado)
        {
            btnReserva.CssClass = "nav-link text-white px-2";
            btnCrearReserva.CssClass = "nav-link text-white px-2";
            btnHistorialReserva.CssClass = "nav-link text-white px-2";

            btnUsuario.CssClass = "nav-link text-white px-2";
            btnMetodoPago.CssClass = "nav-link text-white px-2";
            btnServicios.CssClass = "nav-link text-white px-2";

            botonSeleccionado.CssClass += " link-activo";
        }
        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Remove("NameLogin");
            Response.Redirect("Login.aspx");
        }

        protected void btnPanelAdministrativo_Click(object sender, EventArgs e)
        {
            OcultarTodosLosPaneles();
            ResaltarBotonPrincipal(btnPanelAdministrativo);
            ResaltarBotonSeleccionado(btnUsuario);
            panelAdministrativo.Visible = true;
            panelUsuario.Visible = true;
            lblSeccionTitulo.Text = "Panel Administrativo";

            getDataUser();
        }

        protected void btnReservas_Click(object sender, EventArgs e)
        {
            OcultarTodosLosPaneles();
            ResaltarBotonPrincipal(btnReservas);
            ResaltarBotonSeleccionado(btnReserva);
            panelAdministarReservas.Visible = true;
            panelReservas.Visible = true;
            lblSeccionTitulo.Text = "Panel de Reservas";

            getDataReservas();
        }

        #region Panel Huesped 

        //PANEL HUESPED
        protected void btnRegisterHuesped_Click(object sender, EventArgs e)
        {
            OcultarTodosLosPaneles();
            ResaltarBotonPrincipal(btnRegisterHuesped);
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

        protected void grvHuespedes_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && grvHuespedes.EditIndex == e.Row.RowIndex)
            {
                DropDownList ddlTipoDoc = (DropDownList)e.Row.FindControl("txtEITipoDocumento");
                if (ddlTipoDoc != null)
                {
                    ddlTipoDoc.Items.Clear();
                    ddlTipoDoc.Items.Add("DNI");
                    ddlTipoDoc.Items.Add("Libreta Cívica");
                    ddlTipoDoc.Items.Add("Libreta de Enrolamiento");
                    ddlTipoDoc.Items.Add("Pasaporte");

                    string tipoDocActual = DataBinder.Eval(e.Row.DataItem, "TipoDocumento").ToString();
                    ddlTipoDoc.SelectedValue = tipoDocActual;
                }

                DropDownList ddlEstado = (DropDownList)e.Row.FindControl("ddlEIEstadoHuesped");
                if (ddlEstado != null)
                {
                    ddlEstado.Items.Clear();
                    ddlEstado.Items.Add(new ListItem("Activo", "1"));
                    ddlEstado.Items.Add(new ListItem("Inactivo", "0"));

                    bool estadoBool = Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "Estado"));
                    string estadoStr = estadoBool ? "1" : "0";

                    if (ddlEstado.Items.FindByValue(estadoStr) != null)
                        ddlEstado.SelectedValue = estadoStr;
                }
            }

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
            string tipoDocumento = ((DropDownList)grvHuespedes.Rows[e.RowIndex].FindControl("txtEITipoDocumento")).SelectedValue;
            string email = ((TextBox)grvHuespedes.Rows[e.RowIndex].FindControl("txtEIEmail")).Text;
            string telefono = ((TextBox)grvHuespedes.Rows[e.RowIndex].FindControl("txtEITelefono")).Text;
            DateTime fechaNacimiento = Convert.ToDateTime(((TextBox)grvHuespedes.Rows[e.RowIndex].FindControl("txtEIFechaNacimiento")).Text);
            DropDownList ddlEstado = (DropDownList)grvHuespedes.Rows[e.RowIndex].FindControl("ddlEIEstadoHuesped");
            int estado = Convert.ToInt32(ddlEstado.SelectedValue);

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
            ddlTipoDocumentoHuesped.SelectedValue = "DNI";
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
            ddlTipoDocumentoHuesped.SelectedValue = "DNI";
            txtEmailHuesped.Text = "";
            txtTelefonoHuesped.Text = "";
            txtFechaNacimientoHuesped.Text = "";
        }

        #endregion

        #region Panel Historial de Reservas
        // PANEL HISTORIAL DE RESERVAS 
        protected void grvHistorialReservas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "MostrarDetalle")
            {
                int idReserva = Convert.ToInt32(e.CommandArgument);
                if (ViewState["IdHistorialDetalle"] != null && (int)ViewState["IdHistorialDetalle"] == idReserva)
                {
                    ViewState["IdHistorialDetalle"] = null;
                }
                else
                {
                    ViewState["IdHistorialDetalle"] = idReserva;
                }

                getDataHistorialReservas();
            }
        }

        protected string GetButtonTextReservaHistorial(int idReserva)
        {
            return (ViewState["IdHistorialDetalle"] != null && (int)ViewState["IdHistorialDetalle"] == idReserva) ? "Ocultar Detalle" : "Ver Detalle";
        }

        protected void grvHistorialReservas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int idReserva = (int)grvHistorialReservas.DataKeys[e.Row.RowIndex].Value;

                if (ViewState["IdHistorialDetalle"] != null && (int)ViewState["IdHistorialDetalle"] == idReserva)
                {
                    GridViewRow detalleRow = new GridViewRow(-1, -1, DataControlRowType.DataRow, DataControlRowState.Insert);
                    TableCell celda = new TableCell();
                    celda.ColumnSpan = grvHistorialReservas.Columns.Count;
                    celda.Controls.Add(CrearGrillaDetalles(idReserva));
                    detalleRow.Cells.Add(celda);

                    Table tabla = (Table)grvHistorialReservas.Controls[0];
                    int rowIndex = tabla.Rows.GetRowIndex(e.Row);
                    tabla.Rows.AddAt(rowIndex + 1, detalleRow);
                }
            }
        }

        protected void grvHistorialReservas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvHistorialReservas.PageIndex = e.NewPageIndex;
            getDataHistorialReservas();
        }

        private void getDataHistorialReservas()
        {
            DataTable historial = negocioReserva.GetReservas();
            grvHistorialReservas.DataSource = historial;
            grvHistorialReservas.DataBind();
        }


        protected void btnHistorialReservas_Click(object sender, EventArgs e)
        {
            OcultarTodosLosPanelesReserva();
            ResaltarBotonSeleccionado(btnHistorialReserva);
            getDataHistorialReservas();
            panelHistorialReservas.Visible = true;
        }

      
        #endregion

        #region Panel de Habitaciones
        private void CargarHabitaciones()
        {
            var habitacionesService = new NegocioHabitaciones();
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
            var negocio = new NegocioHabitaciones();
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
            //me falta agregar validaciones para los tipos de datos
            Habitacion nueva = new Habitacion
            {
                NumeroHabitacion = int.Parse(numero),
                Tipo = tipo,
                Capacidad = int.Parse(capacidad),
                Precio = decimal.Parse(precio),
                Descripcion = descripcion,
                Estado = estado
            };

            NegocioHabitaciones service = new NegocioHabitaciones();
            service.Crear(nueva);

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
            NegocioHabitaciones habitacionService = new NegocioHabitaciones();
            DataTable resultado = (string.IsNullOrEmpty(filtro)) ? habitacionService.GetAll() : habitacionService.GetByFilter(filtro);
            gvHabitaciones.DataSource = resultado;
            gvHabitaciones.DataBind();
        }

        protected void btnHabitaciones_Click(object sender, EventArgs e)
        {
            OcultarTodosLosPaneles();
            ResaltarBotonPrincipal(btnRooms);
            lblSeccionTitulo.Text = "Habitaciones";
            panelHabitaciones.Visible = true;
            CargarHabitaciones();
        }
        #endregion

        #region Panel Usuario
        // PANEL USUARIO
        protected void btnUsuario_Click(object sender, EventArgs e)
        {
            OcultarTodosLosPanelesAdmin();
            ResaltarBotonSeleccionado(btnUsuario);
            panelUsuario.Visible = true;

            getDataUser();

        }
        protected void btnMostrarFormularioUsuarios_Click(object sender, EventArgs e)
        {
            limpiarFormularioUsuarios();
            panelUsuario.Visible = true;
            panelRegistrarUsuario.Visible = true;
        }

        protected void limpiarFormularioUsuarios()
        {
            txtName.Text = "";
            txtPassword.Text = "";
            lblMensajePassword.Text = "";
            ddlEstadoHab.SelectedIndex = 1;
        }

        protected void grvUsuario_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && grvUsuario.EditIndex == e.Row.RowIndex)
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

                DropDownList ddlEstado = (DropDownList)e.Row.FindControl("ddlEIEstado");
                if (ddlEstado != null)
                {
                    ddlEstado.Items.Clear();
                    ddlEstado.Items.Add(new ListItem("Activo", "1"));
                    ddlEstado.Items.Add(new ListItem("Inactivo", "0"));

                    bool estadoBool = Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "Estado"));
                    string estadoStr = estadoBool ? "1" : "0";

                    if (ddlEstado.Items.FindByValue(estadoStr) != null)
                        ddlEstado.SelectedValue = estadoStr;
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
            DropDownList ddlEstado = (DropDownList)grvUsuario.Rows[e.RowIndex].FindControl("ddlEIEstado");
            int estado = Convert.ToInt32(ddlEstado.SelectedValue);

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
            panelUsuario.Visible = true;
            panelRegistrarUsuario.Visible = false;
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
        //PANEL METODO DE PAGO
        protected void btnMetodoPago_Click(object sender, EventArgs e)
        {
            OcultarTodosLosPanelesAdmin();
            ResaltarBotonSeleccionado(btnMetodoPago);
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

        protected void grvMetodoPago_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && grvMetodoPago.EditIndex == e.Row.RowIndex)
            {

                DropDownList ddlEstado = (DropDownList)e.Row.FindControl("ddlEIEstadoPago");
                if (ddlEstado != null)
                {
                    ddlEstado.Items.Clear();
                    ddlEstado.Items.Add(new ListItem("Activo", "1"));
                    ddlEstado.Items.Add(new ListItem("Inactivo", "0"));

                    bool estadoBool = Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "Estado"));
                    string estadoStr = estadoBool ? "1" : "0";

                    if (ddlEstado.Items.FindByValue(estadoStr) != null)
                        ddlEstado.SelectedValue = estadoStr;
                }
            }
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
            DropDownList ddlEstado = (DropDownList)grvMetodoPago.Rows[e.RowIndex].FindControl("ddlEIEstadoPago");
            int estado = Convert.ToInt32(ddlEstado.SelectedValue);

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
        //PANEL SERVICIOS
        protected void btnServicios_Click(object sender, EventArgs e)
        {
            OcultarTodosLosPanelesAdmin();
            ResaltarBotonSeleccionado(btnServicios);
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

        protected void grvServicio_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && grvServicio.EditIndex == e.Row.RowIndex)
            {

                DropDownList ddlEstado = (DropDownList)e.Row.FindControl("ddlEIEstadoServicio");
                if (ddlEstado != null)
                {
                    ddlEstado.Items.Clear();
                    ddlEstado.Items.Add(new ListItem("Activo", "1"));
                    ddlEstado.Items.Add(new ListItem("Inactivo", "0"));

                    bool estadoBool = Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "Estado"));
                    string estadoStr = estadoBool ? "1" : "0";

                    if (ddlEstado.Items.FindByValue(estadoStr) != null)
                        ddlEstado.SelectedValue = estadoStr;
                }
            }
        }

        protected void grvServicio_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int id = Convert.ToInt32(grvServicio.DataKeys[e.RowIndex].Value);
            string nombre = ((TextBox)grvServicio.Rows[e.RowIndex].FindControl("txtEINombreServicio")).Text;
            string precioTexto = ((TextBox)grvServicio.Rows[e.RowIndex].FindControl("txtEIPrecio")).Text.Trim();
            decimal precio = Convert.ToDecimal(precioTexto);
            DropDownList ddlEstado = (DropDownList)grvServicio.Rows[e.RowIndex].FindControl("ddlEIEstadoServicio");
            int estado = Convert.ToInt32(ddlEstado.SelectedValue);

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

        #region Reservas
        // PANEL RESERVAS
        protected void btnReserva_Click(object sender, EventArgs e)
        {
            OcultarTodosLosPanelesReserva();
            ResaltarBotonSeleccionado(btnReserva);
            panelReservas.Visible = true;

            getDataReservas();
        }

        protected void grvReservas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "MostrarDetalle")
            {
                int idReserva = Convert.ToInt32(e.CommandArgument);
                if (ViewState["IdReservaDetalle"] != null && (int)ViewState["IdReservaDetalle"] == idReserva)
                {
                    ViewState["IdReservaDetalle"] = null;
                }
                else
                {
                    ViewState["IdReservaDetalle"] = idReserva;
                }

                getDataReservas();
            }
            else if (e.CommandName == "DarDeBaja")
            {
                int idReserva = Convert.ToInt32(e.CommandArgument);

                negocioReserva.EliminarReserva(idReserva);

                getDataReservas();
            }
        }

        private GridView CrearGrillaDetalles(int idReserva)
        {
            var negocioDetalle = new NegocioReserva();
            DataTable dtDetalles = negocioDetalle.ObtenerDetallesPorReserva(idReserva);

            GridView gvDetalles = new GridView();
            gvDetalles.CssClass = "table table-sm";
            gvDetalles.AutoGenerateColumns = false;

            gvDetalles.Columns.Add(new BoundField { HeaderText = "N° Habitación", DataField = "NumeroHabitacion" });
            gvDetalles.Columns.Add(new BoundField { HeaderText = "Tipo", DataField = "Tipo" });
            gvDetalles.Columns.Add(new BoundField { HeaderText = "Check-In", DataField = "CheckIn", DataFormatString = "{0:dd/MM/yyyy}" });
            gvDetalles.Columns.Add(new BoundField { HeaderText = "Check-Out", DataField = "CheckOut", DataFormatString = "{0:dd/MM/yyyy}" });
            gvDetalles.Columns.Add(new BoundField { HeaderText = "Precio", DataField = "PrecioDetalle", DataFormatString = "{0:C}" });

            gvDetalles.DataSource = dtDetalles;
            gvDetalles.DataBind();

            return gvDetalles;
        }

        protected string GetButtonTextReserva(int idReserva)
        {
            return (ViewState["IdReservaDetalle"] != null && (int)ViewState["IdReservaDetalle"] == idReserva) ? "Ocultar Detalle" : "Ver Detalle";
        }

        protected void grvReservas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int idReserva = (int)grvReservas.DataKeys[e.Row.RowIndex].Value;

                if (ViewState["IdReservaDetalle"] != null && (int)ViewState["IdReservaDetalle"] == idReserva)
                {
                    GridViewRow detalleRow = new GridViewRow(-1, -1, DataControlRowType.DataRow, DataControlRowState.Insert);
                    TableCell celda = new TableCell();
                    celda.ColumnSpan = grvReservas.Columns.Count;
                    celda.Controls.Add(CrearGrillaDetalles(idReserva));
                    detalleRow.Cells.Add(celda);

                    Table tabla = (Table)grvReservas.Controls[0];
                    int rowIndex = tabla.Rows.GetRowIndex(e.Row);
                    tabla.Rows.AddAt(rowIndex + 1, detalleRow);
                }
            }
        }

        protected void grvReservas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvReservas.PageIndex = e.NewPageIndex;

            getDataReservas();
        }


        private void getDataReservas()
        {
            DataTable reservas = negocioReserva.GetReservas();
            grvReservas.DataSource = reservas;
            grvReservas.DataBind();
        }

        #endregion

        #region Crear Reservas Etapa 1
        // PANEL CREAR RESERVAS ETAPA 1
        private void getDataHuespedReserva()
        {
            DataTable Huesped = negocioHuesped.GetHuespedes();
            grvCrearReservaEtapa1.DataSource = Huesped;
            grvCrearReservaEtapa1.DataBind();
        }

        private void ActualizarEstadoBotonSiguienteEtapa1()
        {
            bool haySeleccion = ViewState["IdHuespedSeleccionado"] != null;
            btnSiguienteEtapa1.Enabled = haySeleccion;

            if (haySeleccion)
            {
                btnSiguienteEtapa1.CssClass = "btn btn-primary";
            }
            else
            {
                btnSiguienteEtapa1.CssClass = "btn btn-secondary disabled";
            }
        }
        protected void btnCrearReserva_Click1(object sender, EventArgs e)
        {
            OcultarTodosLosPanelesReserva();
            ResaltarBotonSeleccionado(btnCrearReserva);
            panelCrearReservaEtapa1.Visible = true;

            getDataHuespedReserva();
        }

        protected void GridViewCrearReservaEtapa1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvCrearReservaEtapa1.PageIndex = e.NewPageIndex;
            getDataHuespedReserva();
        }

        protected void btnLimpiarFiltroHuespedPorDocumento_Click(object sender, EventArgs e)
        {
            getDataHuespedReserva();
            txtHuespedBuscarPorDocumento.Text = "";
        }

        protected void btnFiltarHuespedPorDocumento_Click(object sender, EventArgs e)
        {
            string Documento = txtHuespedBuscarPorDocumento.Text;
            DataTable Huesped = negocioHuesped.FiltrarHuespedPorDocumento(Documento);
            grvCrearReservaEtapa1.DataSource = Huesped;
            grvCrearReservaEtapa1.DataBind();
        }

        protected void grvCrearReservaEtapa1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idHuesped = Convert.ToInt32(grvCrearReservaEtapa1.SelectedDataKey.Value);
            ViewState["IdHuespedSeleccionado"] = idHuesped;

            string Documento = txtHuespedBuscarPorDocumento.Text;
            DataTable Huesped = negocioHuesped.FiltrarHuespedPorDocumento(Documento);
            grvCrearReservaEtapa1.DataSource = Huesped;
            grvCrearReservaEtapa1.DataBind();

            ReservaEnProceso reserva = Session["reservaEnProceso"] as ReservaEnProceso;
            if (reserva == null)
            {
                reserva = new ReservaEnProceso();
            }

            reserva.IdHuesped = idHuesped;
            Session["reservaEnProceso"] = reserva;

            ActualizarEstadoBotonSiguienteEtapa1();
        }

        protected void grvCrearReservaEtapa1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int idHuespedFila = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Id_huesped"));

                if (ViewState["IdHuespedSeleccionado"] != null &&
                    (int)ViewState["IdHuespedSeleccionado"] == idHuespedFila)
                {
                    e.Row.CssClass = "table-success";
                }
            }
        }

        protected void btnSiguienteEtapa1_Click(object sender, EventArgs e)
        {
            if (ViewState["IdHuespedSeleccionado"] == null)
            {

                return;
            }

            OcultarTodosLosPanelesReserva();
            panelCrearReservaEtapa2.Visible = true;
        }

        #endregion

        #region Crear Reservas Etapa 2
        // PANEL CREAR RESERVAS ETAPA 2

        protected void btnLimpiarFiltroHabitacionPorFechas_Click(object sender, EventArgs e)
        {
            txtCantidadDeHuespedes.Text = "";
            txtFechaDesde.Text = "";
            txtFechaHasta.Text = "";
            lblError.Text = "";

            ActualizarEstadoBotonSiguienteEtapa2();
        }
        protected void btnFiltrarHabitacionPorFechas_Click(object sender, EventArgs e)
        {
            string cantidadText = txtCantidadDeHuespedes.Text.Trim();
            string fechaLlegada = txtFechaDesde.Text.Trim();
            string fechaSalida = txtFechaHasta.Text.Trim();

            if (string.IsNullOrWhiteSpace(cantidadText) ||
                string.IsNullOrWhiteSpace(fechaLlegada) ||
                string.IsNullOrWhiteSpace(fechaSalida))
            {
                lblError.Text = "Por favor, completá todos los campos.";
                lblError.ForeColor = System.Drawing.Color.Red;
                return;
            }

            if (!int.TryParse(cantidadText, out int cantidadHuespedes))
            {
                lblError.Text = "La cantidad de huéspedes debe ser un número válido.";
                lblError.ForeColor = System.Drawing.Color.Red;
                return;
            }

            lblError.Text = "";

            DataTable Reservas = negocioHabitaciones.FiltarHabitacionesPorFecha(fechaLlegada, fechaSalida);
            ViewState["HabitacionesFiltradas"] = Reservas;

            grvCrearReservaEtapa2.DataSource = Reservas;
            grvCrearReservaEtapa2.DataBind();

            Dictionary<int, decimal> preciosPorHabitacion = new Dictionary<int, decimal>();

            foreach (DataRow row in Reservas.Rows)
            {
                int idHabitacion = Convert.ToInt32(row["Id_habitacion"]);
                decimal precio = Convert.ToDecimal(row["Precio"]);
                preciosPorHabitacion[idHabitacion] = precio;
            }

            Session["PreciosHabitaciones"] = preciosPorHabitacion;

            ActualizarEstadoBotonSiguienteEtapa2();
        }


        private void ActualizarEstadoBotonSiguienteEtapa2()
        {
            bool camposValidos = !string.IsNullOrWhiteSpace(txtCantidadDeHuespedes.Text)
                              && !string.IsNullOrWhiteSpace(txtFechaDesde.Text)
                              && !string.IsNullOrWhiteSpace(txtFechaHasta.Text);

            bool haySeleccion = false;
            int capacidadTotal = 0;

            foreach (GridViewRow row in grvCrearReservaEtapa2.Rows)
            {
                CheckBox chk = (CheckBox)row.FindControl("chkSeleccionarHabitacion");
                if (chk != null && chk.Checked)
                {
                    haySeleccion = true;

                    Label lblCapacidad = (Label)row.FindControl("lblCapacidad");
                    if (lblCapacidad != null && int.TryParse(lblCapacidad.Text, out int capacidad))
                    {
                        capacidadTotal += capacidad;
                    }
                }
            }

            bool capacidadSuficiente = false;
            if (int.TryParse(txtCantidadDeHuespedes.Text, out int cantidadHuespedes))
            {
                capacidadSuficiente = capacidadTotal >= cantidadHuespedes;
            }

            bool habilitar = camposValidos && haySeleccion && capacidadSuficiente;

            btnSiguienteEtapa2.Enabled = habilitar;
            btnSiguienteEtapa2.CssClass = habilitar ? "btn btn-primary" : "btn";
        }

        protected void GridViewCrearReservaEtapa2_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvCrearReservaEtapa2.PageIndex = e.NewPageIndex;

            if (ViewState["HabitacionesFiltradas"] != null)
            {
                DataTable reservas = (DataTable)ViewState["HabitacionesFiltradas"];
                grvCrearReservaEtapa2.DataSource = reservas;
                grvCrearReservaEtapa2.DataBind();
            }
        }

        protected void grvCrearReservaEtapa2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int idHabitacion = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Id_habitacion"));
                CheckBox chk = (CheckBox)e.Row.FindControl("chkSeleccionarHabitacion");

                ReservaEnProceso reserva = Session["reservaEnProceso"] as ReservaEnProceso;

                if (reserva != null && reserva.IdHabitaciones.Contains(idHabitacion))
                {
                    e.Row.CssClass = "table-success";
                    chk.Checked = true;
                }
                else
                {
                    e.Row.CssClass = "";
                    chk.Checked = false;
                }
            }
        }


        protected void chkSeleccionarHabitacion_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            GridViewRow row = (GridViewRow)chk.NamingContainer;
            int idHabitacion = Convert.ToInt32(grvCrearReservaEtapa2.DataKeys[row.RowIndex].Value);

            ReservaEnProceso reserva = Session["reservaEnProceso"] as ReservaEnProceso;

            if (reserva == null)
                reserva = new ReservaEnProceso();

            if (chk.Checked)
            {
                if (!reserva.IdHabitaciones.Contains(idHabitacion))
                    reserva.IdHabitaciones.Add(idHabitacion);
            }
            else
            {
                if (reserva.IdHabitaciones.Contains(idHabitacion))
                    reserva.IdHabitaciones.Remove(idHabitacion);
            }

            Session["reservaEnProceso"] = reserva;

            if (ViewState["HabitacionesFiltradas"] != null)
            {
                grvCrearReservaEtapa2.DataSource = (DataTable)ViewState["HabitacionesFiltradas"];
                grvCrearReservaEtapa2.DataBind();
            }

            ActualizarEstadoBotonSiguienteEtapa2();
        }

        protected void btnSiguienteEtapa2_Click(object sender, EventArgs e)
        {
            ActualizarEstadoBotonSiguienteEtapa2();

            if (!btnSiguienteEtapa2.Enabled)
            {
                lblError.Text = "Por favor, completá todos los campos";
                lblError.ForeColor = System.Drawing.Color.Red;
                return;
            }

            ReservaEnProceso reserva = Session["reservaEnProceso"] as ReservaEnProceso ?? new ReservaEnProceso();

            reserva.CantidadHuespedes = int.Parse(txtCantidadDeHuespedes.Text.Trim());
            reserva.CheckIn = DateTime.Parse(txtFechaDesde.Text.Trim());
            reserva.CheckOut = DateTime.Parse(txtFechaHasta.Text.Trim());
            reserva.FechaReserva = DateTime.Now;

            Session["reservaEnProceso"] = reserva;

            OcultarTodosLosPanelesReserva();
            getDataServiciosReserva();
            panelCrearReservaEtapa3.Visible = true;
        }

        protected void btnVolverEtapa2_Click(object sender, EventArgs e)
        {
            OcultarTodosLosPanelesReserva();
            panelCrearReservaEtapa1.Visible = true;

        }

        private void CalcularMontoTotalReserva(ReservaEnProceso reserva)
        {
            var preciosHabitaciones = Session["PreciosHabitaciones"] as Dictionary<int, decimal>;
            var preciosServicios = Session["PreciosServiciosAdicionales"] as Dictionary<int, decimal>;

            if (preciosHabitaciones == null) return;

            int dias = (reserva.CheckOut - reserva.CheckIn).Days;
            if (dias <= 0) dias = 1;

            int cantidadHabitaciones = reserva.IdHabitaciones.Count;

            decimal totalHabitaciones = 0;
            foreach (int idHabitacion in reserva.IdHabitaciones)
            {
                if (preciosHabitaciones.TryGetValue(idHabitacion, out decimal precioHabitacion))
                    totalHabitaciones += precioHabitacion;
            }
            totalHabitaciones *= dias;

            decimal totalServicios = 0;
            if (preciosServicios != null)
            {
                foreach (int idServicio in reserva.ServiciosAdicionales)
                {
                    if (preciosServicios.TryGetValue(idServicio, out decimal precioServicio))
                        totalServicios += precioServicio;
                }
            }

            totalServicios *= cantidadHabitaciones;

            reserva.PrecioFinal = totalHabitaciones + totalServicios;
        }


        #endregion

        #region Crear Reservas Etapa 3
        // PANEL CREAR RESERVAS ETAPA 3

        private void getDataServiciosReserva()
        {
            DataTable Servicio = negocioServicio.GetServiciosActivos();
            grvCrearReservaEtapa3.DataSource = Servicio;
            grvCrearReservaEtapa3.DataBind();

            Dictionary<int, decimal> preciosServicios = new Dictionary<int, decimal>();
            foreach (DataRow row in Servicio.Rows)
            {
                int idServicio = Convert.ToInt32(row["Id_servicioAdicional"]);
                decimal precio = Convert.ToDecimal(row["Precio"]);
                preciosServicios[idServicio] = precio;
            }
            Session["PreciosServiciosAdicionales"] = preciosServicios;
        }


        protected void GridViewCrearReservaEtapa3_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvCrearReservaEtapa3.PageIndex = e.NewPageIndex;
            getDataServiciosReserva();
        }

        protected void grvCrearReservaEtapa3_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int idServicioAdicional = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Id_servicioAdicional"));
                CheckBox chk = (CheckBox)e.Row.FindControl("chkSeleccionarServicio");

                ReservaEnProceso reserva = Session["reservaEnProceso"] as ReservaEnProceso;

                if (reserva != null && reserva.ServiciosAdicionales.Contains(idServicioAdicional))
                {
                    e.Row.CssClass = "table-success";
                    chk.Checked = true;
                }
                else
                {
                    e.Row.CssClass = "";
                    chk.Checked = false;
                }
            }
        }

        protected void chkSeleccionarServicio_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            GridViewRow row = (GridViewRow)chk.NamingContainer;
            int idServicioAdicional = Convert.ToInt32(grvCrearReservaEtapa3.DataKeys[row.RowIndex].Value);

            ReservaEnProceso reserva = Session["reservaEnProceso"] as ReservaEnProceso;
            if (reserva == null)
                reserva = new ReservaEnProceso();

            if (chk.Checked)
            {
                if (!reserva.ServiciosAdicionales.Contains(idServicioAdicional))
                    reserva.ServiciosAdicionales.Add(idServicioAdicional);
            }
            else
            {
                if (reserva.ServiciosAdicionales.Contains(idServicioAdicional))
                    reserva.ServiciosAdicionales.Remove(idServicioAdicional);
            }

            Session["reservaEnProceso"] = reserva;

            getDataServiciosReserva();
        }

        protected void btnSiguienteEtapa3_Click(object sender, EventArgs e)
        {
            ReservaEnProceso reserva = Session["reservaEnProceso"] as ReservaEnProceso;

            if (reserva != null)
            {
                CalcularMontoTotalReserva(reserva);
                Session["reservaEnProceso"] = reserva;

                lblMontoTotal.Text = "Monto Total: " + reserva.PrecioFinal.ToString("C2");
            }

            OcultarTodosLosPanelesReserva();
            getDataMetodoPagoReserva();
            panelCrearReservaEtapa4.Visible = true;
        }

        protected void btnVolverEtapa3_Click(object sender, EventArgs e)
        {
            OcultarTodosLosPanelesReserva();
            panelCrearReservaEtapa2.Visible = true;
        }
        #endregion

        #region Crear Reservas Etapa 4
        // PANEL CREAR RESERVAS ETAPA 4

        private void getDataMetodoPagoReserva()
        {
            DataTable MetodoPagos = negocioMetodoPago.GetMetodoPagosReserva();
            grvCrearReservaEtapa4.DataSource = MetodoPagos;
            grvCrearReservaEtapa4.DataBind();
        }

        private void ActualizarEstadoBotonSiguienteEtapa4()
        {
            bool haySeleccion = ViewState["IdMetodoPagoSeleccionado"] != null;
            btnRegistrarReserva.Enabled = haySeleccion;

            if (haySeleccion)
            {
                btnRegistrarReserva.CssClass = "btn btn-primary";
            }
            else
            {
                btnRegistrarReserva.CssClass = "btn btn-secondary disabled";
            }
        }

        protected void GridViewCrearReservaEtapa4_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvCrearReservaEtapa4.PageIndex = e.NewPageIndex;
            getDataMetodoPagoReserva();
        }

        protected void grvCrearReservaEtapa4_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idMetodoPago = Convert.ToInt32(grvCrearReservaEtapa4.SelectedDataKey.Value);
            ViewState["IdMetodoPagoSeleccionado"] = idMetodoPago;

            getDataMetodoPagoReserva();

            ReservaEnProceso reserva = Session["reservaEnProceso"] as ReservaEnProceso;
            if (reserva == null)
            {
                reserva = new ReservaEnProceso();
            }

            reserva.IdMetodoPago = idMetodoPago;
            Session["reservaEnProceso"] = reserva;

            ActualizarEstadoBotonSiguienteEtapa4();
        }

        protected void grvCrearReservaEtapa4_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int idMetodoPago = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Id_metodoPago"));

                if (ViewState["IdMetodoPagoSeleccionado"] != null &&
                    (int)ViewState["IdMetodoPagoSeleccionado"] == idMetodoPago)
                {
                    e.Row.CssClass = "table-success";
                }
            }
        }
        protected void btnRegistrarReserva_Click(object sender, EventArgs e)
        {
            string nroTarjeta = txtNumeroTarjeta.Text.Trim();
            string vencimiento = txtVencimiento.Text.Trim();

            lblErrorMetodoPago.Text = ""; // Limpiar error

            if (string.IsNullOrEmpty(nroTarjeta) || !long.TryParse(nroTarjeta, out _))
            {
                lblErrorMetodoPago.Text = "Por favor ingresá un número de tarjeta válido.";
                lblErrorMetodoPago.ForeColor = System.Drawing.Color.Red;
                return;
            }

            if (string.IsNullOrEmpty(vencimiento) || vencimiento.Length != 5 || !vencimiento.Contains('/'))
            {
                lblErrorMetodoPago.Text = "Por favor ingresá el vencimiento en formato MM/AA.";
                lblErrorMetodoPago.ForeColor = System.Drawing.Color.Red;
                return;
            }

            ReservaEnProceso reserva = Session["reservaEnProceso"] as ReservaEnProceso;
            if (reserva != null)
            {
                reserva.NroTarjeta = nroTarjeta;
                reserva.VenTarjeta = vencimiento;
                Session["reservaEnProceso"] = reserva;

                NegocioReserva negocio = new NegocioReserva();
                bool exito = negocio.GuardarReserva(reserva);

                if (exito)
                {
                    OcultarTodosLosPanelesReserva();
                    getDataReservas();
                    panelReservas.Visible = true;
                }
                else
                {
                    lblErrorMetodoPago.Text = "Error al guardar la reserva. Intentá nuevamente.";
                    lblErrorMetodoPago.ForeColor = System.Drawing.Color.Red;
                }
            }
        }


        protected void btnVolverEtapa4_Click(object sender, EventArgs e)
        {
            OcultarTodosLosPanelesReserva();
            panelCrearReservaEtapa3.Visible = true;
        }
        #endregion

    }










}