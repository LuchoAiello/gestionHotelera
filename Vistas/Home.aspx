<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="Vistas.Home" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Inicio - Gestión Hotelera</title>
    <!-- Bootstrap 5 -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">

        <!-- Navbar -->
        <nav class="navbar navbar-expand-lg navbar-dark bg-dark">
            <div class="container-fluid">

                <!-- Nombre de la app -->
                <a class="navbar-brand fw-bold me-5" href="#">Gestión Hotelera</a>

                <!-- Botones de navegación -->
                <div class="collapse navbar-collapse">
                    <ul class="navbar-nav me-auto mb-2 mb-lg-0">
                        <li class="nav-item">
                            <asp:LinkButton ID="btnPanelAdministrativo" runat="server" CssClass="nav-link text-white" OnClick="btnPanelAdministrativo_Click">Panel Administrativo</asp:LinkButton>
                        </li>
                        <li class="nav-item">
                            <asp:LinkButton ID="btnRegisterHuesped" runat="server" CssClass="nav-link text-white" OnClick="btnRegisterHuesped_Click">Huesped</asp:LinkButton>
                        </li>
                        <li class="nav-item">
                            <asp:LinkButton ID="btnReserv" runat="server" CssClass="nav-link text-white" OnClick="btnReserv_Click">Reservas</asp:LinkButton>
                        </li>
                        <li class="nav-item">
                            <asp:LinkButton ID="btnRooms" runat="server" CssClass="nav-link text-white" OnClick="btnRooms_Click">Habitaciones</asp:LinkButton>
                        </li>
                        <li class="nav-item">
                            <asp:LinkButton ID="btnHistorialReservas" runat="server" CssClass="nav-link text-white" OnClick="btnHistorialReservas_Click">Historial de Reservas</asp:LinkButton>
                        </li>
                    </ul>

                    <!-- Sector de usuario -->
                    <div class="d-flex align-items-center text-white">
                        <asp:Label ID="lblNameLogin" runat="server" CssClass="me-3 fw-semibold" Text="Administrador"></asp:Label>
                        <asp:Button ID="btnLogout" runat="server" CssClass="btn btn-outline-light btn-sm" Text="Cerrar Sesión" OnClick="btnLogout_Click" />
                    </div>
                </div>
            </div>
        </nav>

        <!-- Contenido principal con sección dinámica -->
        <div class="container mt-5">
            <div class="card shadow">
                <div class="card-header bg-dark text-white d-flex align-items-center gap-3">
                    <!-- Título -->
                    <asp:Label ID="lblSeccionTitulo" runat="server" Text="Panel Administrativo" CssClass="fw-bold mb-0" />

                    <!-- Panel para ocultar/mostrar los botones -->
                    <asp:Panel ID="panelAdministrativo" runat="server" CssClass="d-flex gap-3">
                        <asp:LinkButton ID="btnUsuario" runat="server" CssClass="nav-link text-white px-2" OnClick="btnUsuario_Click">Usuario</asp:LinkButton>
                        <asp:LinkButton ID="btnMetodoPago" runat="server" CssClass="nav-link text-white px-2" OnClick="btnMetodoPago_Click">Método de Pago</asp:LinkButton>
                        <asp:LinkButton ID="btnServicios" runat="server" CssClass="nav-link text-white px-2" OnClick="btnServicios_Click">Servicios</asp:LinkButton>
                    </asp:Panel>
                </div>

                <div class="card-body">

                    <!-- Panel para Reservas -->
                    <asp:Panel ID="panelReservas" runat="server" Visible="false">
                    </asp:Panel>

                    <!-- Panel para Panel Usuario -->
                    <asp:Panel ID="panelUsuario" runat="server" Visible="false">
                        <asp:Button ID="btnNuevoUsuario" runat="server" Text="Nuevo Usuario" CssClass="btn btn-primary" OnClick="btnNuevoUsuario_Click" />
                        <div class="mt-3" style="overflow-x: auto; width: 100%;">
                            <asp:GridView ID="grvUsuario" runat="server" AutoGenerateColumns="False" DataKeyNames="Id_usuario" CssClass="table table-striped table-bordered w-100"
                                AllowPaging="true" PageSize="5" OnRowCancelingEdit="grvUsuario_RowCancelingEdit" OnRowEditing="grvUsuario_RowEditing" OnRowUpdating="grvUsuario_RowUpdating" OnRowDataBound="grvUsuario_RowDataBound" OnPageIndexChanging="grvUsuario_PageIndexChanging">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnEditar" runat="server" CommandName="Edit" CssClass="btn btn-primary btn-sm">Editar</asp:LinkButton>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:LinkButton ID="btnActualizar" runat="server" CommandName="Update" CssClass="btn btn-success btn-sm">Guardar</asp:LinkButton>
                                            <asp:LinkButton ID="btnCancelar" runat="server" CommandName="Cancel" CssClass="btn btn-danger btn-sm">Cancelar</asp:LinkButton>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Nombre">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtEINombre" runat="server" Text='<%# Bind("Nombre") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="txtNombre" runat="server" Text='<%# Bind("Nombre") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Contraseña">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtEIContrasenia" runat="server" Text='<%# Bind("Contrasenia") %>' Width="100px"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="txtContrasenia" runat="server" Text='<%# Bind("Contrasenia") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Rol">
                                        <EditItemTemplate>
                                            <asp:DropDownList ID="ddpEIRol" runat="server">
                                            </asp:DropDownList>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="txtRol" runat="server" Text='<%# Bind("Rol") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Estado">
                                        <EditItemTemplate>
                                            <asp:CheckBox ID="chkEIEstado" runat="server" Checked='<%# Convert.ToBoolean(Eval("Estado")) %>' />
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="txtEstado" runat="server" Text='<%# Convert.ToBoolean(Eval("Estado")) ? "Activo" : "Inactivo" %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>

                            <!-- Panel para Registrar Usuario Dinamico -->
                            <asp:Panel ID="panelRegistrarUsuario" runat="server" Visible="false" CssClass="mt-4 p-3 border rounded">
                                <div class="row mb-3">
                                    <div class="col-md-4">
                                        <label for="txtName" class="form-label">Nombre</label>
                                        <asp:TextBox ID="txtName" runat="server" CssClass="form-control" />
                                        <asp:Label ID="lblMensajeNombre" runat="server" CssClass="text-danger small" />
                                    </div>
                                    <div class="col-md-4">
                                        <label for="txtPassword" class="form-label">Contraseña</label>
                                        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control" />
                                        <asp:Label ID="lblMensajePassword" runat="server" CssClass="text-danger small" />
                                    </div>
                                    <div class="col-md-4">
                                        <label for="ddlRolUsuario" class="form-label">Rol</label>
                                        <asp:DropDownList ID="ddlRolUsuario" runat="server" CssClass="form-select">
                                            <asp:ListItem Text="Administrador" Value="Administrador" />
                                            <asp:ListItem Text="Recepcionista" Value="Recepcionista" />
                                        </asp:DropDownList>
                                    </div>
                                </div>

                                <div class="d-flex gap-2">
                                    <asp:Button ID="btnRegisterUser" runat="server" Text="Registrar" CssClass="btn btn-success" OnClick="btnRegisterUser_Click" />
                                    <asp:Button ID="btnCancelarUser" runat="server" Text="Cancelar" CssClass="btn btn-secondary" OnClick="btnCancelarUser_Click" />
                                </div>
                            </asp:Panel>


                        </div>
                    </asp:Panel>

                    <!-- Panel para Panel Metodo de Pago -->
                    <asp:Panel ID="panelMetodoPago" runat="server" Visible="false">
                        <asp:Button ID="btnNuevoMetodo" runat="server" Text="Nuevo Metodo" CssClass="btn btn-primary" OnClick="btnNuevoMetodo_Click" />

                        <div class="mt-3" style="overflow-x: auto; width: 100%;">

                            <asp:GridView ID="grvMetodoPago" runat="server" AutoGenerateColumns="False" DataKeyNames="Id_metodoPago" CssClass="table table-striped table-bordered w-100"
                                AllowPaging="true" PageSize="5" OnRowCancelingEdit="grvMetodoPago_RowCancelingEdit" OnRowEditing="grvMetodoPago_RowEditing" OnRowUpdating="grvMetodoPago_RowUpdating" OnPageIndexChanging="grvMetodoPago_PageIndexChanging">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnEditar" runat="server" CommandName="Edit" CssClass="btn btn-primary btn-sm">Editar</asp:LinkButton>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:LinkButton ID="btnActualizar" runat="server" CommandName="Update" CssClass="btn btn-success btn-sm">Guardar</asp:LinkButton>
                                            <asp:LinkButton ID="btnCancelar" runat="server" CommandName="Cancel" CssClass="btn btn-danger btn-sm">Cancelar</asp:LinkButton>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Nombre">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtEINombrePago" runat="server" Text='<%# Bind("Nombre") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="txtNombrePago" runat="server" Text='<%# Bind("Nombre") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Estado">
                                        <EditItemTemplate>
                                            <asp:CheckBox ID="chkEIEstadoPago" runat="server" Checked='<%# Convert.ToBoolean(Eval("Estado")) %>' />
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="txtEstadoPago" runat="server" Text='<%# Convert.ToBoolean(Eval("Estado")) ? "Activo" : "Inactivo" %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>

                            <asp:Panel ID="panelRegistrarMetodo" runat="server" Visible="false" CssClass="mt-4 p-3 border rounded">
                                <h6 class="fw-bold mb-3">Agregar Método de Pago</h6>

                                <div class="row mb-3">
                                    <div class="col-md-4">
                                        <label for="txtNameMetodoPago" class="form-label">Nombre</label>
                                        <asp:TextBox ID="txtNameMetodoPago" runat="server" CssClass="form-control" />
                                        <asp:Label ID="lblNameMetodoPago" runat="server" CssClass="text-danger small" />
                                    </div>
                                </div>

                                <div class="d-flex gap-2">
                                    <asp:Button ID="btnAgregarPago" runat="server" Text="Registrar" CssClass="btn btn-success" OnClick="btnAgregarPago_Click" />
                                    <asp:Button ID="btnCancelarMetodoPago" runat="server" Text="Cancelar" CssClass="btn btn-secondary" OnClick="btnCancelarMetodoPago_Click" />
                                </div>
                            </asp:Panel>
                        </div>
                    </asp:Panel>

                    <!-- Panel para Panel Servicios -->
                    <asp:Panel ID="panelServicios" runat="server" Visible="false">
                        <asp:Button ID="btnNuevoServicio" runat="server" Text="Nuevo Servicio" CssClass="btn btn-primary" OnClick="btnNuevoServicio_Click" />

                        <div class="mt-3" style="overflow-x: auto; width: 100%;">

                            <asp:GridView ID="grvServicio" runat="server" AutoGenerateColumns="False" DataKeyNames="Id_serviciosAdicionales" CssClass="table table-striped table-bordered w-100"
                                AllowPaging="true" PageSize="5" OnPageIndexChanging="grvServicio_PageIndexChanging" OnRowCancelingEdit="grvServicio_RowCancelingEdit" OnRowEditing="grvServicio_RowEditing" OnRowUpdating="grvServicio_RowUpdating">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnEditar" runat="server" CommandName="Edit" CssClass="btn btn-primary btn-sm">Editar</asp:LinkButton>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:LinkButton ID="btnActualizar" runat="server" CommandName="Update" CssClass="btn btn-success btn-sm">Guardar</asp:LinkButton>
                                            <asp:LinkButton ID="btnCancelar" runat="server" CommandName="Cancel" CssClass="btn btn-danger btn-sm">Cancelar</asp:LinkButton>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Nombre">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtEINombreServicio" runat="server" Text='<%# Bind("NombreServicio") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="txtNombreServicio" runat="server" Text='<%# Bind("NombreServicio") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Precio">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtEIPrecio" runat="server" Text='<%# Bind("Precio") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="txtPrecio" runat="server" Text='<%# Bind("Precio") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Estado">
                                        <EditItemTemplate>
                                            <asp:CheckBox ID="chkEIEstadoServicio" runat="server" Checked='<%# Convert.ToBoolean(Eval("Estado")) %>' />
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="txtEstadoServicio" runat="server" Text='<%# Convert.ToBoolean(Eval("Estado")) ? "Activo" : "Inactivo" %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>

                            <asp:Panel ID="panelRegistrarServicio" runat="server" Visible="false" CssClass="mt-4 p-3 border rounded">
                                <h6 class="fw-bold mb-3">Agregar Servicio Adicional</h6>

                                <div class="row mb-3">
                                    <div class="col-md-4">
                                        <label for="txtNameServicio" class="form-label">Nombre</label>
                                        <asp:TextBox ID="txtNameServicio" runat="server" CssClass="form-control" />
                                        <asp:Label ID="lblNameServicio" runat="server" CssClass="text-danger small" />
                                    </div>
                                    <div class="col-md-4">
                                        <label for="txtPrecio" class="form-label">Precio</label>
                                        <asp:TextBox ID="txtPrecio" runat="server" CssClass="form-control" />
                                        <asp:Label ID="lblNamePrecio" runat="server" CssClass="text-danger small" />
                                    </div>
                                </div>

                                <div class="d-flex gap-2">
                                    <asp:Button ID="btnAgregarServicio" runat="server" Text="Registrar" CssClass="btn btn-success" OnClick="btnAgregarServicio_Click" />
                                    <asp:Button ID="btnCancelarServicio" runat="server" Text="Cancelar" CssClass="btn btn-secondary" OnClick="btnCancelarServicio_Click" />
                                </div>
                            </asp:Panel>

                        </div>
                    </asp:Panel>

                    <!-- Panel para Historial de Reservas -->
                    <asp:Panel ID="panelHistorialReservas" runat="server" Visible="false">
                        <div class="row mb-3">
                            <div class="col-md-3">
                                <label for="txtNumber">Número de Habitación</label>
                                <asp:TextBox ID="txtNumber" runat="server" CssClass="form-control" />
                            </div>
                            <div class="col-md-3">
                                <label for="txtDateFrom">Fecha Desde</label>
                                <asp:TextBox ID="txtDateFrom" runat="server" TextMode="Date" CssClass="form-control" />
                            </div>
                            <div class="col-md-3">
                                <label for="txtDateTo">Fecha Hasta</label>
                                <asp:TextBox ID="txtDateTo" runat="server" TextMode="Date" CssClass="form-control" />
                            </div>
                            <div class="col-md-3 d-flex align-items-end">
                                <asp:Button ID="btnFilter" runat="server" Text="Filtrar" CssClass="btn btn-primary me-2" OnClick="btnFilter_Click" />
                                <asp:Button ID="btnSacarFiltro" runat="server" Text="Limpiar" CssClass="btn btn-secondary" OnClick="btnSacarFiltro_Click" />
                            </div>
                        </div>


                        <div style="overflow-x: auto; width: 100%;">
                            <asp:GridView ID="grvHistorialReservas" runat="server" AutoGenerateColumns="False"
                                CssClass="table table-striped table-bordered w-100"
                                AllowPaging="true" PageSize="10" OnPageIndexChanging="grvHistorialReservas_PageIndexChanging">
                                <Columns>
                                    <asp:BoundField DataField="NumeroHabitacion" HeaderText="Habitación N°" />
                                    <asp:BoundField DataField="NombreCompleto" HeaderText="Nombre del Huésped" />
                                    <asp:BoundField DataField="Documento" HeaderText="Documento" />
                                    <asp:BoundField DataField="FechaReserva" HeaderText="Reservado el" DataFormatString="{0:dd/MM/yyyy}" />
                                    <asp:BoundField DataField="FechaLlegada" HeaderText="Llegada" DataFormatString="{0:dd/MM/yyyy}" />
                                    <asp:BoundField DataField="FechaSalida" HeaderText="Salida" DataFormatString="{0:dd/MM/yyyy}" />
                                    <asp:BoundField DataField="ServiciosAdicionales" HeaderText="Servicios Extras" />
                                    <asp:BoundField DataField="PrecioFinal" HeaderText="Total" DataFormatString="{0:C}" />
                                </Columns>
                            </asp:GridView>
                        </div>
                    </asp:Panel>

                    <asp:Panel ID="panelHabitaciones" runat="server" Visible="false">

                        <asp:Button ID="btnNuevaHabitacion" runat="server" Text="Nueva Habitación" CssClass="btn btn-primary mb-3"
                            OnClick="btnNuevaHabitacion_Click" />

                        <!-- GridView listado de habitaciones -->
                        <asp:GridView ID="gvHabitaciones" runat="server" AutoGenerateColumns="False"
                            CssClass="table table-striped table-bordered"
                            OnRowCommand="gvHabitaciones_RowCommand"
                            DataKeyNames="Id_habitaciones">
                            <Columns>
                                <asp:BoundField DataField="NumeroHabitacion" HeaderText="Número" />
                                <asp:BoundField DataField="Tipo" HeaderText="Tipo" />
                                <asp:BoundField DataField="Capacidad" HeaderText="Capacidad" />
                                <asp:BoundField DataField="Estado" HeaderText="Estado" />
                                <asp:BoundField DataField="Precio" HeaderText="Precio" />
                                <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
                                <asp:ButtonField ButtonType="Button" CommandName="Editar" Text="Editar" />
                                <asp:ButtonField ButtonType="Button" CommandName="Desactivar" Text="Baja" />
                            </Columns>
                        </asp:GridView>

                        <!-- Formulario ABM Habitaciones (oculto inicialmente) -->
                        <asp:Panel ID="panelFormularioHabitacion" runat="server" Visible="false" CssClass="mt-4 p-3 border rounded">

                            <asp:HiddenField ID="hfIdHabitacion" runat="server" />

                            <div class="row mb-3">
                                <div class="col-md-2">
                                    <label>Número</label>
                                    <asp:TextBox ID="txtNumero" runat="server" CssClass="form-control" />
                                </div>
                                <div class="col-md-3">
                                    <label>Tipo</label>
                                    <asp:TextBox ID="txtTipo" runat="server" CssClass="form-control" />
                                </div>
                                <div class="col-md-2">
                                    <label>Capacidad</label>
                                    <asp:TextBox ID="txtCapacidad" runat="server" CssClass="form-control" />
                                </div>
                                <div class="col-md-2">
                                    <label>Precio</label>
                                    <asp:TextBox ID="txtPrecioHabitacion" runat="server" CssClass="form-control" />
                                </div>
                                <div class="col-md-3">
                                    <label>Estado</label>
                                    <asp:DropDownList ID="ddlEstado" runat="server" CssClass="form-select">
                                        <asp:ListItem Text="Activa" Value="Activa" />
                                        <asp:ListItem Text="Inactiva" Value="Inactiva" />
                                        <asp:ListItem Text="Mantenimiento" Value="Mantenimiento" />
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="mb-3">
                                <label>Descripción</label>
                                <asp:TextBox ID="txtDescripcion" runat="server" TextMode="MultiLine" Rows="3" CssClass="form-control" />
                            </div>

                            <asp:Button ID="btnGuardarHabitacion" runat="server" Text="Guardar" CssClass="btn btn-success"
                                OnClick="btnGuardarHabitacion_Click" />
                            <asp:Button ID="btnCancelarHabitacion" runat="server" Text="Cancelar" CssClass="btn btn-secondary ms-2"
                                OnClick="btnCancelarHabitacion_Click" />
                        </asp:Panel>

                    </asp:Panel>

                    <!-- Panel para Huespedes -->
                    <asp:Panel ID="panelHuespedes" runat="server" Visible="false">
                        <asp:Button ID="btnNuevoHuesped" runat="server" Text="Nuevo Huesped" CssClass="btn btn-primary" OnClick="btnNuevoHuesped_Click" />

                        <div class="mt-3" style="overflow-x: auto; width: 100%;">

                            <asp:GridView ID="grvHuespedes" runat="server" AutoGenerateColumns="False" DataKeyNames="Id_huesped" CssClass="table table-striped table-bordered w-100"
                                AllowPaging="true" PageSize="5" OnPageIndexChanging="grvHuespedes_PageIndexChanging" OnRowCancelingEdit="grvHuespedes_RowCancelingEdit" OnRowEditing="grvHuespedes_RowEditing" OnRowUpdating="grvHuespedes_RowUpdating">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnEditar" runat="server" CommandName="Edit" CssClass="btn btn-primary btn-sm">Editar</asp:LinkButton>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:LinkButton ID="btnActualizar" runat="server" CommandName="Update" CssClass="btn btn-success btn-sm">Guardar</asp:LinkButton>
                                            <asp:LinkButton ID="btnCancelar" runat="server" CommandName="Cancel" CssClass="btn btn-danger btn-sm">Cancelar</asp:LinkButton>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Nombre">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtEINombreHuesped" runat="server" Text='<%# Bind("Nombre") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="txtNombreHuesped" runat="server" Text='<%# Bind("Nombre") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Apellido">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtEIApellido" runat="server" Text='<%# Bind("Apellido") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="txtApellido" runat="server" Text='<%# Bind("Apellido") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Documento">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtEIDocumento" runat="server" Text='<%# Bind("Documento") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="txtDocumento" runat="server" Text='<%# Bind("Documento") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Tipo Documento">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtEITipoDocumento" runat="server" Text='<%# Bind("TipoDocumento") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblTipoDocumento" runat="server" Text='<%# Bind("TipoDocumento") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Email">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtEIEmail" runat="server" Text='<%# Bind("Email") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblEmail" runat="server" Text='<%# Bind("Email") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Teléfono">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtEITelefono" runat="server" Text='<%# Bind("Telefono") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblTelefono" runat="server" Text='<%# Bind("Telefono") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Fecha Nacimiento">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtEIFechaNacimiento" runat="server" Text='<%# Bind("FechaNacimiento", "{0:yyyy-MM-dd}") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblFechaNacimiento" runat="server" Text='<%# Bind("FechaNacimiento", "{0:dd/MM/yyyy}") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Estado">
                                        <EditItemTemplate>
                                            <asp:CheckBox ID="chkEIEstadoServicio" runat="server" Checked='<%# Convert.ToBoolean(Eval("Estado")) %>' />
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="txtEstadoServicio" runat="server" Text='<%# Convert.ToBoolean(Eval("Estado")) ? "Activo" : "Inactivo" %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>

                            <!-- Panel para Registrar Huespedes -->
                            <asp:Panel ID="panelRegistrarHuesped" runat="server" Visible="false" CssClass="mt-4 p-3 border rounded">
                                <h6 class="fw-bold mb-3">Agregar Huésped</h6>

                                <div class="row mb-3">
                                    <div class="col-md-4">
                                        <label for="txtNombreHuesped" class="form-label">Nombre</label>
                                        <asp:TextBox ID="txtNombreHuesped" runat="server" CssClass="form-control" />
                                        <asp:Label ID="lblNombreHuesped" runat="server" CssClass="text-danger small" />
                                    </div>
                                    <div class="col-md-4">
                                        <label for="txtApellidoHuesped" class="form-label">Apellido</label>
                                        <asp:TextBox ID="txtApellidoHuesped" runat="server" CssClass="form-control" />
                                        <asp:Label ID="lblApellidoHuesped" runat="server" CssClass="text-danger small" />
                                    </div>
                                    <div class="col-md-4">
                                        <label for="txtDocumentoHuesped" class="form-label">Documento</label>
                                        <asp:TextBox ID="txtDocumentoHuesped" runat="server" CssClass="form-control" />
                                        <asp:Label ID="lblDocumentoHuesped" runat="server" CssClass="text-danger small" />
                                    </div>
                                </div>

                                <div class="row mb-3">
                                    <div class="col-md-4">
                                        <label for="ddlTipoDocumentoHuesped" class="form-label">Tipo de Documento</label>
                                        <asp:DropDownList ID="ddlTipoDocumentoHuesped" runat="server" CssClass="form-select">
                                            <asp:ListItem>Dni</asp:ListItem>
                                            <asp:ListItem>Libreta Cívica</asp:ListItem>
                                            <asp:ListItem>Libreta de Enrolamiento</asp:ListItem>
                                            <asp:ListItem>Pasaporte</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:Label ID="lblTipoDocumentoHuesped" runat="server" CssClass="text-danger small" />
                                    </div>
                                    <div class="col-md-4">
                                        <label for="txtEmailHuesped" class="form-label">Email</label>
                                        <asp:TextBox ID="txtEmailHuesped" runat="server" CssClass="form-control" />
                                        <asp:Label ID="lblEmailHuesped" runat="server" CssClass="text-danger small" />
                                    </div>
                                    <div class="col-md-4">
                                        <label for="txtTelefonoHuesped" class="form-label">Teléfono</label>
                                        <asp:TextBox ID="txtTelefonoHuesped" runat="server" CssClass="form-control" />
                                        <asp:Label ID="lblTelefonoHuesped" runat="server" CssClass="text-danger small" />
                                    </div>
                                </div>

                                <div class="row mb-3">
                                    <div class="col-md-4">
                                        <label for="txtFechaNacimientoHuesped" class="form-label">Fecha de Nacimiento</label>
                                        <asp:TextBox ID="txtFechaNacimientoHuesped" runat="server" TextMode="Date" CssClass="form-control" />
                                        <asp:Label ID="lblFechaNacimientoHuesped" runat="server" CssClass="text-danger small" />
                                    </div>
                                </div>

                                <div class="d-flex gap-2">
                                    <asp:Button ID="btnRegistrarHuesped" runat="server" Text="Registrar" CssClass="btn btn-success" OnClick="btnRegistrarHuesped_Click" />
                                    <asp:Button ID="btnLimpiarHuesped" runat="server" Text="Cancelar" CssClass="btn btn-secondary" OnClick="btnLimpiarHuesped_Click" />
                                </div>
                            </asp:Panel>

                        </div>
                    </asp:Panel>
                </div>
            </div>
        </div>
    </form>

    <!-- Bootstrap JS -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>
