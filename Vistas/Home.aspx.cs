﻿using Entidades;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

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

                OcultarTodosLosPaneles();

                if (rol == "Administrador")
                {
                    btnRooms.Visible = true;
                    OcultarTodosLosPaneles();
                    ResaltarBotonPrincipal(btnPanelAdministrativo);
                    ResaltarBotonSeleccionado(btnUsuario);
                    panelAdministrativo.Visible = true;
                    panelUsuario.Visible = true;
                    lblSeccionTitulo.Text = "Panel Administrativo";

                    getDataUser();

                }
                else if (rol == "Recepcionista")
                {
                    btnPanelAdministrativo.Visible = false;
                    btnRooms.Visible = false;
                    OcultarTodosLosPaneles();
                    ResaltarBotonPrincipal(btnReservas);
                    ResaltarBotonSeleccionado(btnReserva);
                    panelReservas.Visible = true;
                    panelAdministarReservas.Visible = true;
                    lblSeccionTitulo.Text = "Panel de Reservas";

                    getDataReservas();
                }

                btnRegisterHuesped.Visible = true;
                btnReservas.Visible = true;
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
            panelDetalleReserva.Visible = false;
            panelHistorialReservaDetalle.Visible = false;
            panelCrearReservaEtapa1.Visible = false;
            panelCrearReservaEtapa2.Visible = false;
            panelCrearReservaEtapa3.Visible = false;
            panelCrearReservaEtapa4.Visible = false;
            //panelCheckInOut.Visible = false;
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

            Huespedes huesped = Huespedes.ModificarHuesped(id, nombre, apellido, documento, tipoDocumento, email, telefono, fechaNacimiento);
            negocioHuesped.ModificarHuesped(huesped);

            grvHuespedes.EditIndex = -1;
            getDataHuesped();
        }

        protected void grvHuespedes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DarDeBajaHuesped")
            {
                int idHuesped = Convert.ToInt32(e.CommandArgument);

                Huespedes eliminarHuesped = Huespedes.EliminarHuesped(idHuesped);
                negocioHuesped.EliminarHuesped(eliminarHuesped);

                getDataHuesped();
            }
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
                // Validar que tenga al menos 18 años
                int edad = DateTime.Now.Year - fechaNacimiento.Year;
                if (DateTime.Now < fechaNacimiento.AddYears(edad))
                    edad--;

                if (edad < 18)
                {
                    lblFechaNacimientoHuesped.Text = "El huésped debe tener al menos 18 años.";
                    lblFechaNacimientoHuesped.CssClass = "text-danger";
                    return;
                }
                else
                {
                    lblFechaNacimientoHuesped.Text = "";
                }
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
        protected void grvHistorialReservas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "MostrarHistorialDetalle")
            {
                int idReserva = Convert.ToInt32(e.CommandArgument);

                MostrarHistorialDetalleReservaFijo(idReserva);
            }
        }
        protected void grvHistorialDetalleReserva_RowCommand(object sender, GridViewCommandEventArgs e)
        {
        
        }

        protected void grvHistorialReservas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvHistorialReservas.PageIndex = e.NewPageIndex;
            getDataHistorialReservas();
        }

        private void getDataHistorialReservas()
        {
            DataTable historial = negocioReserva.GetHistorialDeReservas();
            grvHistorialReservas.DataSource = historial;
            grvHistorialReservas.DataBind();
        }

        protected void txtBuscarHistorialReserva_TextChanged(object sender, EventArgs e)
        {
            string filtro = txtBuscarHistorialReservas.Text.Trim().ToLower();
            NegocioHabitaciones habitacionService = new NegocioHabitaciones();
            DataTable resultado = (string.IsNullOrEmpty(filtro)) ? habitacionService.GetAll() : habitacionService.GetByFilter(filtro);
            gvHabitaciones.DataSource = resultado;
            gvHabitaciones.DataBind();

            DataTable historialReservaFiltrado = (string.IsNullOrEmpty(filtro)) ? negocioReserva.GetHistorialDeReservas() : negocioReserva.ObtenerHistorialReservaFiltrado(filtro);
            grvHistorialReservas.DataSource = historialReservaFiltrado;
            grvHistorialReservas.DataBind();

        }
        protected void btnHistorialReservas_Click(object sender, EventArgs e)
        {
            OcultarTodosLosPanelesReserva();
            ResaltarBotonSeleccionado(btnHistorialReserva);
            getDataHistorialReservas();
            panelHistorialReservas.Visible = true;
        }
        protected void grvHistorialDetalleReservaFijo_RowDataBound(object sender, EventArgs e)
        {

        }

        protected void btnVolverAHistorialReserva_Click(object sender, EventArgs e)
        {
            OcultarTodosLosPanelesReserva();
            ResaltarBotonSeleccionado(btnHistorialReserva);
            panelAdministarReservas.Visible = true;
            panelHistorialReservas.Visible = true;

            getDataHistorialReservas();
        }

        private void MostrarHistorialDetalleReservaFijo(int idReserva)
        {
            OcultarTodosLosPaneles();
            panelHistorialReservas.Visible = false;
            panelHistorialReservaDetalle.Visible = true;

            DataTable dtHistorialReserva = negocioReserva.ObtenerHistorialReservaPorId(idReserva);
            gvHistorialReservaById.DataSource = dtHistorialReserva;
            gvHistorialReservaById.DataBind();

            DataTable detalles = negocioReserva.ObtenerDetallesPorReserva(idReserva);
            grvHistorialReservaDetalle.DataSource = detalles;
            grvHistorialReservaDetalle.DataBind();
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

            int id = Convert.ToInt32(gvHabitaciones.DataKeys[e.RowIndex].Value);

            int numero = int.Parse(((TextBox)row.FindControl("txtNumeroHab")).Text);
            string tipo = ((TextBox)row.FindControl("txtTipoHab")).Text;
            int capacidad = int.Parse(((TextBox)row.FindControl("txtCapacidad")).Text);
            string estado = ((DropDownList)row.FindControl("ddlEIEstado")).SelectedValue;
            decimal precio = decimal.Parse(((TextBox)row.FindControl("txtPrecioHab")).Text);
            string descripcion = ((TextBox)row.FindControl("txtDescripcionHab")).Text;

            Habitaciones habitacion = Habitaciones.ModificarHabitacion(id, numero, tipo, capacidad, estado, precio, descripcion);
            negocioHabitaciones.ModificarHabitacion(habitacion);

            gvHabitaciones.EditIndex = -1;
            CargarHabitaciones();
        }

        protected void btnRegistrarHabitacion_Click(object sender, EventArgs e)
        {
            string numeroTexto = txtNumeroHab.Text.Trim();
            string tipo = txtTipoHab.Text.Trim();
            string capacidadTexto = txtCapacidad.Text.Trim();
            string estado = ddlEstadoHab.SelectedValue;
            string precioTexto = txtPrecioHab.Text.Trim();
            string descripcion = txtDescripcionHab.Text.Trim();

            if (string.IsNullOrEmpty(numeroTexto) || string.IsNullOrEmpty(tipo) ||
                string.IsNullOrEmpty(capacidadTexto) || string.IsNullOrEmpty(precioTexto))
            {
                lblMensajeRegistro.Visible = true;
                lblMensajeRegistro.Text = "Por favor, completá todos los campos obligatorios.";
                return;
            }

            if (!int.TryParse(numeroTexto, out int numero))
            {
                lblMensajeRegistro.Visible = true;
                lblMensajeRegistro.Text = "Número de habitación inválido.";
                return;
            }

            if (!int.TryParse(capacidadTexto, out int capacidad))
            {
                lblMensajeRegistro.Visible = true;
                lblMensajeRegistro.Text = "Capacidad inválida.";
                return;
            }

            if (!decimal.TryParse(precioTexto, out decimal precio))
            {
                lblMensajeRegistro.Visible = true;
                lblMensajeRegistro.Text = "Precio inválido.";
                return;
            }

            Habitaciones habitacion = Habitaciones.CrearHabitacion(numero, tipo, capacidad, estado, precio, descripcion);
            negocioHabitaciones.CrearHabitacion(habitacion);

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

        protected void grvHabitaciones_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && gvHabitaciones.EditIndex == e.Row.RowIndex)
            {
                DropDownList ddlEstado = (DropDownList)e.Row.FindControl("ddlEIEstado");
                ddlEstado.Items.Clear();
                ddlEstado.Items.Add("Disponible");
                ddlEstado.Items.Add("Ocupada");
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

            Usuario userMod = Usuario.ModificarUsuario(id, nombre, contrasenia, rol);
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

        protected void grvUsuario_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DarDeBajaUsuario")
            {
                int idUsuario = Convert.ToInt32(e.CommandArgument);

                Usuario eliminarUsuario = Usuario.EliminarUsuario(idUsuario);
                negocioUsuario.EliminarUsuario(eliminarUsuario);

                getDataUser();
            }
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

        protected void grvMetodoPago_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DarDeBajaMetodoPago")
            {
                int idMetodoPago = Convert.ToInt32(e.CommandArgument);

                MetodoPago eliminarMetodoPago = MetodoPago.EliminarMetodoPago(idMetodoPago);
                negocioMetodoPago.EliminarMetodoPago(eliminarMetodoPago);

                getDataMetodoPago();
            }
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

            MetodoPago metodoPago = MetodoPago.ModificarMetodoPago(id, nombre);
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

            Servicios servicio = Servicios.ModificarServicio(id, nombre, precio);
            negocioServicio.ModificarServicio(servicio);

            grvServicio.EditIndex = -1;
            getDataServicio();
        }

        protected void grvServicio_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DarDeBajaServicio")
            {
                int idServicio = Convert.ToInt32(e.CommandArgument);

                Servicios eliminarServicio = Servicios.EliminarServicio(idServicio);
                negocioServicio.EliminarServicio(eliminarServicio);

                getDataServicio();
            }
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
 
        protected void btnReserva_Click(object sender, EventArgs e)
        {
            OcultarTodosLosPanelesReserva();
            ResaltarBotonSeleccionado(btnReserva);
            panelReservas.Visible = true;
            panelDetalleReserva.Visible = false;

            getDataReservas();
        }
        protected void btnVolverAReservas_Click(object sender, EventArgs e)
        {
            OcultarTodosLosPanelesReserva();
            ResaltarBotonSeleccionado(btnReserva);
            panelAdministarReservas.Visible = true;
            panelReservas.Visible = true;

            getDataReservas();
        }

        protected void grvDetalleReservaFijo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView rowView = (DataRowView)e.Row.DataItem;

                Button btnCheckIn = (Button)e.Row.FindControl("btnCheckIn");
                Button btnCheckOut = (Button)e.Row.FindControl("btnCheckOut");
                Label lblFinalizada = (Label)e.Row.FindControl("lblFinalizada");

                DateTime? checkIn = rowView["CheckIn"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(rowView["CheckIn"]);
                DateTime? checkOut = rowView["CheckOut"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(rowView["CheckOut"]);

                if (checkIn == null)
                {
                    btnCheckIn.Visible = true;
                    btnCheckOut.Visible = false;
                    lblFinalizada.Visible = false;
                }
                else if (checkOut == null)
                {
                    btnCheckIn.Visible = false;
                    btnCheckOut.Visible = true;
                    lblFinalizada.Visible = false;
                }
                else
                {
                    btnCheckIn.Visible = false;
                    btnCheckOut.Visible = false;
                    lblFinalizada.Visible = true;
                }
            }
        }

        private void MostrarDetalleReservaFijo(int idReserva)
        {
            OcultarTodosLosPaneles();
            panelReservas.Visible = false;
            panelDetalleReserva.Visible = true;

            DataTable dtReserva = negocioReserva.ObtenerReservaPorId(idReserva);
            grvReservaById.DataSource = dtReserva;
            grvReservaById.DataBind();

            DataTable detalles = negocioReserva.ObtenerDetallesPorReserva(idReserva);
            grvDetalleReservaFijo.DataSource = detalles;
            grvDetalleReservaFijo.DataBind();

        }

        protected void grvDetalleReservaFijo_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "HacerCheckIn" || e.CommandName == "HacerCheckOut")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                int idDetalle = index;

                if (e.CommandName == "HacerCheckIn")
                    negocioReserva.RegistrarCheckIn(idDetalle);
                else
                    negocioReserva.RegistrarCheckOut(idDetalle);

                int idReserva = negocioReserva.ObtenerIdReservaDesdeDetalle(idDetalle);
                ViewState["IdReservaDetalle"] = idReserva;
                MostrarDetalleReservaFijo(idReserva);
            }
        }

        protected void grvReservas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "MostrarDetalle")
            {
                int idReserva = Convert.ToInt32(e.CommandArgument);
                ViewState["IdReservaDetalle"] = idReserva;

                MostrarDetalleReservaFijo(idReserva);
            }
            else if (e.CommandName == "DarDeBajaReserva")
            {
                int idReserva = Convert.ToInt32(e.CommandArgument);

                negocioReserva.EliminarReserva(idReserva);

                getDataReservas();
            }
        }

        protected void grvDetalleReserva_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int idDetalleReserva = Convert.ToInt32(e.CommandArgument);
            NegocioReserva negocioReserva = new NegocioReserva();

            if (e.CommandName == "HacerCheckIn")
            {
                negocioReserva.RegistrarCheckIn(idDetalleReserva);
            }
            else if (e.CommandName == "HacerCheckOut")
            {
                negocioReserva.RegistrarCheckOut(idDetalleReserva);
            }

            // Obtener la fila actual para actualizar los controles si querés
            GridViewRow row = ((Control)e.CommandSource).NamingContainer as GridViewRow;
            if (row != null)
            {
                Label lblFinalizada = row.FindControl("lblFinalizada") as Label;
                Button btnCheckIn = row.FindControl("btnCheckIn") as Button;
                Button btnCheckOut = row.FindControl("btnCheckOut") as Button;

                if (lblFinalizada != null)
                    lblFinalizada.Visible = true;

                if (btnCheckIn != null)
                    btnCheckIn.Visible = false;

                if (btnCheckOut != null)
                    btnCheckOut.Visible = false;
            }

  
            int idReserva = negocioReserva.ObtenerIdReservaDesdeDetalle(idDetalleReserva);

            getDataReservas();
            MostrarDetalleReservaFijo(idReserva);
        }

        protected void btnCancelarCheckInOut_Click(object sender, EventArgs e)
        {
            OcultarTodosLosPanelesReserva();
            ResaltarBotonSeleccionado(btnReserva);
            panelReservas.Visible = true;

            getDataReservas();
        }

        protected void grvReservas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvReservas.PageIndex = e.NewPageIndex;

            getDataReservas();
        }

        private void getDataReservas()
        {
            DataTable reservas = negocioReserva.GetReservasActualesYFuturas();
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
            string fechaLlegadaText = txtFechaDesde.Text.Trim();
            string fechaSalidaText = txtFechaHasta.Text.Trim();

            if (string.IsNullOrWhiteSpace(cantidadText) ||
                string.IsNullOrWhiteSpace(fechaLlegadaText) ||
                string.IsNullOrWhiteSpace(fechaSalidaText))
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

            if (!DateTime.TryParse(fechaLlegadaText, out DateTime fechaLlegada) ||
                !DateTime.TryParse(fechaSalidaText, out DateTime fechaSalida))
            {
                lblError.Text = "Las fechas ingresadas no son válidas.";
                lblError.ForeColor = System.Drawing.Color.Red;
                return;
            }

            if (fechaLlegada.Date < DateTime.Today)
            {
                lblError.Text = "La fecha de llegada no puede ser anterior a hoy.";
                lblError.ForeColor = System.Drawing.Color.Red;
                return;
            }

            if (fechaSalida.Date <= fechaLlegada.Date)
            {
                lblError.Text = "La fecha de salida debe ser posterior a la fecha de llegada.";
                lblError.ForeColor = System.Drawing.Color.Red;
                return;
            }

            lblError.Text = "";

            DataTable Reservas = negocioHabitaciones.FiltarHabitacionesPorFecha(fechaLlegadaText, fechaSalidaText);
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
            reserva.FechaLlegada = DateTime.Parse(txtFechaDesde.Text.Trim());
            reserva.FechaSalida = DateTime.Parse(txtFechaHasta.Text.Trim());

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

            int dias = (reserva.FechaSalida - reserva.FechaLlegada).Days;
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

            Session["PrecioFinal"] = totalHabitaciones + totalServicios;
        }


        #endregion

        #region Crear Reservas Etapa 3
        // PANEL CREAR RESERVAS ETAPA 3

        private void getDataServiciosReserva()
        {
            DataTable Servicio = negocioServicio.GetServicios();
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

                decimal precioFinal = Convert.ToDecimal(Session["PrecioFinal"]);
                lblMontoTotal.Text = "Monto Total: " + precioFinal.ToString("C2");
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
            DataTable MetodoPagos = negocioMetodoPago.GetMetodoPagos();
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
            lblErrorMetodoPago.Text = ""; // Limpiar errores previos
            string nroTarjeta = txtNumeroTarjeta.Text.Trim();
            string vencimiento = txtVencimiento.Text.Trim();

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
                reserva.VtoTarjeta = vencimiento;
                Session["reservaEnProceso"] = reserva;

                NegocioReserva negocio = new NegocioReserva();
                bool exito = negocio.GuardarReserva(reserva);

                if (exito)
                {
                    reserva = new ReservaEnProceso();
                    Session["reservaEnProceso"] = reserva;

                    LimpiarControlesReserva();

                    OcultarTodosLosPanelesReserva();
                    getDataReservas();
                    ResaltarBotonSeleccionado(btnReserva);
                    panelReservas.Visible = true;
                }
                else
                {
                    lblErrorMetodoPago.Text = "Error al guardar la reserva. Intentá nuevamente.";
                    lblErrorMetodoPago.ForeColor = System.Drawing.Color.Red;
                }
            }
        }

        private void LimpiarControlesReserva()
        {
            // Limpiar TextBox
            txtCantidadDeHuespedes.Text = "";
            txtFechaDesde.Text = "";
            txtFechaHasta.Text = "";
            txtNumeroTarjeta.Text = "";
            txtVencimiento.Text = "";

            // Resetear reserva
            Session["reservaEnProceso"] = new ReservaEnProceso();

            // Limpiar ViewState usados para pintar
            ViewState["IdHuespedSeleccionado"] = null;
            ViewState["IdMetodoPagoSeleccionado"] = null;

            // Rebind de GridViews para que se actualicen sin clases
            grvCrearReservaEtapa1.DataSource = new List<object>();
            grvCrearReservaEtapa1.DataBind();

            ActualizarEstadoBotonSiguienteEtapa1();

            grvCrearReservaEtapa2.DataSource = new List<object>();
            grvCrearReservaEtapa2.DataBind();

            ActualizarEstadoBotonSiguienteEtapa2();

            grvCrearReservaEtapa3.DataSource = new List<object>();
            grvCrearReservaEtapa3.DataBind();

            grvCrearReservaEtapa4.DataSource = new List<object>();
            grvCrearReservaEtapa4.DataBind();

            ActualizarEstadoBotonSiguienteEtapa4();

            lblErrorMetodoPago.Text = "";
        }


        protected void btnVolverEtapa4_Click(object sender, EventArgs e)
        {
            OcultarTodosLosPanelesReserva();
            panelCrearReservaEtapa3.Visible = true;
        }
        #endregion

    }










}