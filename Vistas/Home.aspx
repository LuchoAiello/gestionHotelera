<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="Vistas.Home" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Inicio - Gestión Hotelera</title>
    <!-- Bootstrap 5 -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.1/font/bootstrap-icons.css">
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
                            <asp:LinkButton ID="btnRooms" runat="server" CssClass="nav-link text-white" OnClick="btnHabitaciones_Click">Habitaciones</asp:LinkButton>
                        </li>
                        <li class="nav-item">
                            <asp:LinkButton ID="btnHistorialReservas" runat="server" CssClass="nav-link text-white" OnClick="btnHistorialReservas_Click">Reservas</asp:LinkButton>
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
                <div class="card-header bg-dark text-white">
                    <asp:Label ID="lblSeccionTitulo" runat="server" Text="Sección Actual" class="fw-bold "></asp:Label>
                </div>
                <div class="card-body">

                    <!-- Panel para Panel Administrativo -->
                    <asp:Panel ID="panelAdministrativo" runat="server" Visible="false">
                        <div class="d-flex gap-2 mb-1">
                            <asp:Button ID="btnUsuario" runat="server" Text="Usuario" CssClass="btn btn-primary" OnClick="btnUsuario_Click" />
                            <asp:Button ID="btnMetodoPago" runat="server" Text="Metodo de Pago" CssClass="btn btn-primary" OnClick="btnMetodoPago_Click" />
                            <asp:Button ID="btnServicios" runat="server" Text="Servicios" CssClass="btn btn-primary" OnClick="btnServicios_Click" />
                        </div>

                        <!-- Panel para Panel Usuario -->
                        <asp:Panel ID="panelUsuario" runat="server" Visible="false">
                            <div class="login-container text-start">
                                <h6 class="mb-2 fw-bold">Registrar Usuario</h6>

                                <div class="mb-3  p-0 d-flex align-items-center gap-2">
                                    <label for="txtName" class="form-label me-2 mb-0" style="min-width: 90px;">Nombre: </label>
                                    <asp:TextBox ID="txtName" runat="server" CssClass="form-control flex-grow-1" Style="max-width: 250px;" />
                                    &nbsp;<asp:Label ID="lblMensajeNombre" runat="server"></asp:Label>
                                </div>

                                <div class="mb-3  p-0 d-flex align-items-center gap-2">
                                    <label for="txtPassword" class="form-label me-2 mb-0" style="min-width: 90px;">Contraseña: </label>
                                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control flex-grow-1" Style="max-width: 250px;" />
                                    &nbsp;<asp:Label ID="lblMensajePassword" runat="server"></asp:Label>
                                </div>

                                <div class="mb-3 col-md-4 col-sm-6 p-0 d-flex align-items-center gap-2">
                                    <label for="DropDownList1" class="form-label me-2 mb-0" style="min-width: 90px;">Rol: </label>
                                    <asp:DropDownList ID="ddlRolUsuario" runat="server" CssClass="form-select w-auto flex-grow-1" Style="max-width: 250px;">
                                        <asp:ListItem Text="Administrador" Value="Administrador" />
                                        <asp:ListItem Text="Recepcionista" Value="Recepcionista" />
                                    </asp:DropDownList>
                                </div>

                                <asp:Button ID="btnRegisterUser" runat="server" Text="Registrar" CssClass="btn btn-primary" OnClick="btnRegisterUser_Click" />
                                <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" CssClass="btn btn-secondary w-10" OnClick="btnLimpiar_Click" />
                            </div>

                            <div class="mt-3" style="overflow-x: auto; width: 100%;">

                                <asp:GridView ID="grvUsuario" runat="server" AutoGenerateColumns="False" AutoGenerateEditButton="True" DataKeyNames="Id_usuario" CssClass="table table-striped table-bordered w-100"
                                    AllowPaging="true" PageSize="10" OnRowCancelingEdit="grvUsuario_RowCancelingEdit" OnRowEditing="grvUsuario_RowEditing" OnRowUpdating="grvUsuario_RowUpdating" OnRowDataBound="grvUsuario_RowDataBound">
                                    <Columns>
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

                            </div>
                        </asp:Panel>

                        <!-- Panel para Panel Metodo de Pago -->
                        <asp:Panel ID="panelMetodoPago" runat="server" Visible="false">
                            <div class="login-container text-start">
                                <h6 class="mb-2 fw-bold">Agregar Metodo de Pago</h6>

                                <div class="mb-3  p-0 d-flex align-items-center gap-2">
                                    <label for="txtNameMetodoPago" class="form-label me-2 mb-0" style="min-width: 90px;">Nombre: </label>
                                    <asp:TextBox ID="txtNameMetodoPago" runat="server" CssClass="form-control flex-grow-1" Style="max-width: 250px;" />
                                    &nbsp;<asp:Label ID="lblNameMetodoPago" runat="server"></asp:Label>
                                </div>

                                <asp:Button ID="btnAgregarPago" runat="server" Text="Registrar" CssClass="btn btn-primary" OnClick="btnAgregarPago_Click" />
                                <asp:Button ID="btnLimpiarMetodoPago" runat="server" Text="Limpiar" CssClass="btn btn-secondary w-10" OnClick="btnLimpiarMetodoPago_Click" />
                            </div>

                            <div class="mt-3" style="overflow-x: auto; width: 100%;">

                                <asp:GridView ID="grvMetodoPago" runat="server" AutoGenerateColumns="False" AutoGenerateEditButton="True" DataKeyNames="Id_metodoPago" CssClass="table table-striped table-bordered w-100"
                                    AllowPaging="true" PageSize="10" OnRowCancelingEdit="grvMetodoPago_RowCancelingEdit" OnRowEditing="grvMetodoPago_RowEditing" OnRowUpdating="grvMetodoPago_RowUpdating">
                                    <Columns>
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

                            </div>
                        </asp:Panel>

                        <!-- Panel para Panel Servicios -->
                        <asp:Panel ID="panelServicios" runat="server" Visible="false">
                            <div class="login-container text-start">
                                <h6 class="mb-2 fw-bold">Agregar Servicio adicional</h6>

                                <div class="mb-3  p-0 d-flex align-items-center gap-2">
                                    <label for="txtNameServicio" class="form-label me-2 mb-0" style="min-width: 90px;">Nombre: </label>
                                    <asp:TextBox ID="txtNameServicio" runat="server" CssClass="form-control flex-grow-1" Style="max-width: 250px;" />
                                    &nbsp;<asp:Label ID="lblNameServicio" runat="server"></asp:Label>
                                </div>

                                <div class="mb-3  p-0 d-flex align-items-center gap-2">
                                    <label for="txtPrecio" class="form-label me-2 mb-0" style="min-width: 90px;">Precio: </label>
                                    <asp:TextBox ID="txtPrecio" runat="server" CssClass="form-control flex-grow-1" Style="max-width: 250px;" />
                                    &nbsp;<asp:Label ID="lblNamePrecio" runat="server"></asp:Label>
                                </div>

                                <asp:Button ID="btnAgregarServicio" runat="server" Text="Registrar" CssClass="btn btn-primary" OnClick="btnAgregarServicio_Click" />
                                <asp:Button ID="btnLimpiarServicio" runat="server" Text="Limpiar" CssClass="btn btn-secondary w-10" OnClick="btnLimpiarServicio_Click" />
                            </div>

                            <div class="mt-3" style="overflow-x: auto; width: 100%;">

                                <asp:GridView ID="grvServicio" runat="server" AutoGenerateColumns="False" AutoGenerateEditButton="True" DataKeyNames="Id_servicioAdicional" CssClass="table table-striped table-bordered w-100"
                                    AllowPaging="true" PageSize="10" OnPageIndexChanging="grvServicio_PageIndexChanging" OnRowCancelingEdit="grvServicio_RowCancelingEdit" OnRowEditing="grvServicio_RowEditing" OnRowUpdating="grvServicio_RowUpdating">
                                    <Columns>
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

                            </div>
                        </asp:Panel>
                    </asp:Panel>

                    <!-- Panel para Reservas -->
                    <asp:Panel ID="panelHistorialReservas" runat="server" Visible="false">
                        <div class="container" style="margin: 10px">
                            <div class="row mb-3">
                                <div class="col-md-4">
                                    <asp:Label ID="Label1" runat="server" Text="Número de Habitación:" CssClass="form-label" />
                                    <asp:TextBox ID="txtNumber" runat="server" CssClass="form-control" />
                                </div>
                                <div class="col-md-4">
                                    <asp:Label ID="Label2" runat="server" Text="Fecha Desde:" CssClass="form-label" />
                                    <asp:TextBox ID="txtDateFrom" runat="server" CssClass="form-control" TextMode="Date" />
                                </div>
                                <div class="col-md-4">
                                    <asp:Label ID="Label3" runat="server" Text="Fecha Hasta:" CssClass="form-label" />
                                    <asp:TextBox ID="txtDateTo" runat="server" CssClass="form-control" TextMode="Date" />
                                </div>
                            </div>

                            <div class="row">
                                <div class="col text-end">
                                    <asp:Button ID="Button1" runat="server" Text="Crear" CssClass="btn btn-primary me-1" OnClick="btnCrearReserva_Click" />
                                    <asp:Button ID="btnFilter" runat="server" Text="Filtrar" CssClass="btn btn-primary me-2" OnClick="btnFilter_Click" />
                                    <asp:Button ID="btnSacarFiltro" runat="server" Text="Sacar Filtro" CssClass="btn btn-secondary" OnClick="btnSacarFiltro_Click" />
                                </div>
                            </div>
                        </div>

                        <div style="overflow-x: auto; width: 100%;">
                            <asp:GridView ID="grvHistorialReservas" runat="server" AutoGenerateColumns="False"
                                CssClass="table table-striped table-bordered w-100"
                                AllowPaging="true" PageSize="10"
                                OnPageIndexChanging="grvHistorialReservas_PageIndexChanging"
                                OnRowCommand="grvHistorialReservas_RowCommand">
                                <Columns>
                                    <asp:BoundField DataField="NumeroHabitacion" HeaderText="Habitación N°" />
                                    <asp:BoundField DataField="NombreCompleto" HeaderText="Nombre del Huésped" />
                                    <asp:BoundField DataField="Documento" HeaderText="Documento" />
                                    <asp:BoundField DataField="FechaReserva" HeaderText="Reservado el" DataFormatString="{0:dd/MM/yyyy}" />
                                    <asp:BoundField DataField="FechaLlegada" HeaderText="Llegada" DataFormatString="{0:dd/MM/yyyy}" />
                                    <asp:BoundField DataField="FechaSalida" HeaderText="Salida" DataFormatString="{0:dd/MM/yyyy}" />
                                    <asp:BoundField DataField="ServiciosAdicionales" HeaderText="Servicios Extras" />
                                    <asp:BoundField DataField="PrecioFinal" HeaderText="Total" DataFormatString="{0:C}" />

 
                                    <asp:TemplateField HeaderText="Acciones">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnEditar" runat="server" CssClass="btn btn-sm btn-outline-primary me-1"
                                                CommandName="Editar" CommandArgument='<%# Eval("Id_reserva") %>' ToolTip="Editar">
                                                <i class="bi bi-pencil"></i>
                                            </asp:LinkButton>
                                            <asp:LinkButton ID="btnEliminar" runat="server" CssClass="btn btn-sm btn-outline-danger"
                                                CommandName="Eliminar" CommandArgument='<%# Eval("Id_reserva") %>' ToolTip="Eliminar"
                                                OnClientClick="return confirm('¿Estás seguro que querés eliminar esta reserva?');">
                                                <i class="bi bi-trash"></i>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </asp:Panel>
                    <%--Panel para Habitaciones--%>
                    <asp:Panel ID="panelHabitaciones" runat="server" Visible="false">
                         <!-- Botón para iniciar registro -->
                        <div class="col text-end">
                         <asp:Button ID="btnMostrarFormulario" runat="server" Text="Registrar habitación" CssClass="btn btn-primary mb-3" OnClick="btnMostrarFormularioHabitaciones_Click" />
                        </div>
                        <!-- Panel de formulario de registro -->
                        <asp:Panel ID="panelFormularioRegistro" runat="server" Visible="false">
                            <h6 class="mb-2 fw-bold">Registrar Habitación</h6>

                            <div class="mb-3  p-0 d-flex align-items-center gap-2">
                                <label for="txtNumeroHab" class="form-label me-2 mb-0" style="min-width: 90px;">Numero Habitacion: </label>
                                <asp:TextBox ID="txtNumeroHab" runat="server" CssClass="form-control flex-grow-1" Style="max-width: 250px;" />
                                &nbsp;<asp:Label ID="Label4" runat="server"></asp:Label>
                            </div>
                            <div class="mb-3  p-0 d-flex align-items-center gap-2">
                                <label for="txtTipoHab" class="form-label me-2 mb-0" style="min-width: 90px;">Tipo: </label>
                                <asp:TextBox ID="txtTipoHab" runat="server" CssClass="form-control flex-grow-1" Style="max-width: 250px;" />
                                &nbsp;<asp:Label ID="Label5" runat="server"></asp:Label>
                            </div>
                            <div class="mb-3  p-0 d-flex align-items-center gap-2">
                                <label for="txtCapacidad" class="form-label me-2 mb-0" style="min-width: 90px;">Capacidad: </label>
                                <asp:TextBox ID="txtCapacidad" runat="server" CssClass="form-control flex-grow-1" Style="max-width: 250px;" />
                                &nbsp;<asp:Label ID="Label6" runat="server"></asp:Label>
                            </div>
                            <div class="mb-3  p-0 d-flex align-items-center gap-2">
                                <label for="txtPrecioHab" class="form-label me-2 mb-0" style="min-width: 90px;">Precio: </label>
                                <asp:TextBox ID="txtPrecioHab" runat="server" CssClass="form-control flex-grow-1" Style="max-width: 250px;" />
                                &nbsp;<asp:Label ID="Label7" runat="server"></asp:Label>
                            </div>
                            <div class="mb-3  p-0 d-flex align-items-center gap-2">
                                <label for="txtDescripcionHab" class="form-label me-2 mb-0" style="min-width: 90px;">Descripcion: </label>
                                <asp:TextBox ID="txtDescripcionHab" runat="server" CssClass="form-control flex-grow-1" Style="max-width: 250px;" />
                                &nbsp;<asp:Label ID="Label8" runat="server"></asp:Label>
                            </div>
                                <div class="mb-3 col-md-4 col-sm-6 p-0 d-flex align-items-center gap-2">
                                    <label for="DropDownList1" class="form-label me-2 mb-0" style="min-width: 90px;">Estado: </label>
                                    <asp:DropDownList ID="ddlEstadoHab" runat="server" CssClass="form-select w-auto flex-grow-1" Style="max-width: 250px;">
                                        <asp:ListItem Text="Activa" Value="Activa" />
                                        <asp:ListItem Text="Inactiva" Value="Inactiva" />
                                        <asp:ListItem Text="Mantenimiento" Value="Mantenimiento" />
                                    </asp:DropDownList>
                                </div>
                            <asp:Label ID="lblMensajeRegistro" runat="server" CssClass="alert alert-danger d-block" Visible="false" />
                            <asp:Button ID="btnGuardarHabitacion" runat="server" Text="Guardar" CssClass="btn btn-success me-2" OnClick="btnRegistrarHabitacion_Click" />
                            <asp:Button ID="btnCancelarRegistro" runat="server" Text="Cancelar" CssClass="btn btn-secondary" OnClick="btnCancelarRegistroHabitacion_Click" />
                        </asp:Panel>
                        
                        <!-- Panel del listado -->
                        <asp:Panel ID="panelListadoHabitaciones" runat="server" Visible="true">
                        
                            <div class="d-flex justify-content-between align-items-center mb-3">
                                <div class="input-group w-50">
                                    <asp:TextBox ID="txtBuscarHabitacion" runat="server" CssClass="form-control"
                                        placeholder="Buscar habitación..."
                                        AutoPostBack="true" OnTextChanged="txtBuscarHabitacion_TextChanged" />
                                </div>
                            </div>
                            <asp:GridView ID="gvHabitaciones" runat="server" AutoGenerateColumns="False"
                                DataKeyNames="Id_habitacion"
                                CssClass="table table-striped table-bordered"
                                AllowPaging="true"
                                PageSize="10"
                                OnRowCancelingEdit="grvHabitaciones_RowCancelingEdit"
                                OnRowEditing="grvHabitaciones_RowEditing"
                                OnRowUpdating="grvHabitaciones_RowUpdating"
                                OnRowDataBound="grvHabitaciones_RowDataBound"
                                OnRowCommand="grvHabitaciones_RowCommand"
                                OnPageIndexChanging="gvHabitaciones_PageIndexChanging">

                                <Columns>
                                    <asp:TemplateField HeaderText="Numero Habitacion">
                                            <EditItemTemplate>
                                            <asp:TextBox ID="txtNumeroHab" runat="server" Style="max-width: 200px;" Text='<%# Bind("NumeroHabitacion") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                            <asp:Label ID="txtNumeroHab" runat="server" Text='<%# Bind("NumeroHabitacion") %>'></asp:Label>
                                            </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Tipo">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtTipoHab" runat="server" Style="max-width: 200px;" Text='<%# Bind("Tipo") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="txtTipoHab" runat="server" Text='<%# Bind("Tipo") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Capacidad">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtCapacidad" runat="server" Style="max-width: 50px;" Text='<%# Bind("Capacidad") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="txtCapacidad" runat="server" Style="max-width: 50px;" Text='<%# Bind("Capacidad") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Precio">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtPrecioHab" runat="server" Style="max-width: 50px;" Text='<%# Bind("Precio") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="txtPrecioHab" runat="server" Style="max-width: 50px;" Text='<%# Bind("Precio") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Descripción">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtDescripcionHab" runat="server" Style="max-width: 200px;" Text='<%# Bind("Descripcion") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="txtDescripcionHab" runat="server" Text='<%# Bind("Descripcion") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Estado">
                                        <EditItemTemplate>
                                            <asp:DropDownList ID="ddlEIEstado" runat="server" Style="max-width: 100px;"></asp:DropDownList>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblEstadoHab" runat="server" Text='<%# Bind("Estado") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Aciones">
                                        <ItemTemplate>
                                            <!-- Se muestra cuando la fila NO está en edición -->
                                            <asp:LinkButton ID="btnEditar" runat="server" CssClass="btn btn-sm btn-outline-primary me-1"
                                                CommandName="Edit" CommandArgument='<%# Eval("Id_habitacion") %>' ToolTip="Editar">
                                                <i class="bi bi-pencil"></i>
                                            </asp:LinkButton>
                                            <%--<asp:LinkButton ID="btnEliminar" runat="server"  CssClass="btn btn-sm btn-outline-danger"
                                                CommandName="Desactivar" CommandArgument='<%# Eval("Id_habitacion") %>' ToolTip="Eliminar"
                                                OnClientClick="return confirm('¿Estás seguro que querés eliminar esta reserva?');">
                                                <i class="bi bi-trash"></i>
                                            </asp:LinkButton>--%>
                                        </ItemTemplate>
                                         <EditItemTemplate>
                                        <!-- Se muestra cuando la fila ESTÁ en edición -->
                                            <asp:LinkButton ID="btnAceptar" runat="server" CssClass="btn btn-sm btn-success me-1"
                                                CommandName="Update" ToolTip="Aceptar">
                                                <i class="bi bi-check"></i>
                                            </asp:LinkButton>
                                            <asp:LinkButton ID="btnCancelar" runat="server" CssClass="btn btn-sm btn-secondary"
                                                CommandName="Cancel" ToolTip="Cancelar">
                                                <i class="bi bi-x"></i>
                                            </asp:LinkButton>
                                        </EditItemTemplate>
                                     </asp:TemplateField>
                                        
                                </Columns>
                            </asp:GridView>
                        </asp:Panel>
                        <asp:Label ID="lblMensaje" runat="server" CssClass="text-danger" />
                        
                    </asp:Panel>
    
            </div>
        </div>
    </div>


    </form>

    <!-- Bootstrap JS -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>
