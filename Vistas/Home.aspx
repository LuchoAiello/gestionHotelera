<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="Vistas.Home" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Inicio - Gestión Hotelera</title>
    <!-- Bootstrap 5 -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.1/font/bootstrap-icons.css" />
    <style>
        .link-activo {
            border-bottom: 2px solid white;
        }

        .link-principal-activo {
            border-bottom: 2px solid white;
        }
    </style>
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
                            <asp:LinkButton ID="btnReservas" runat="server" CssClass="nav-link text-white" OnClick="btnReservas_Click">Reservas</asp:LinkButton>
                        </li>
                        <li class="nav-item">
                            <asp:LinkButton ID="btnRegisterHuesped" runat="server" CssClass="nav-link text-white" OnClick="btnRegisterHuesped_Click">Huesped</asp:LinkButton>
                        </li>
                        <li class="nav-item">
                            <asp:LinkButton ID="btnRooms" runat="server" CssClass="nav-link text-white" OnClick="btnHabitaciones_Click">Habitaciones</asp:LinkButton>
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

                    <!-- Panel para ocultar/mostrar los botones de admin-->
                    <asp:Panel ID="panelAdministrativo" runat="server" CssClass="d-flex gap-3">
                        <asp:LinkButton ID="btnUsuario" runat="server" CssClass="nav-link text-white px-2" OnClick="btnUsuario_Click">Usuario</asp:LinkButton>
                        <asp:LinkButton ID="btnMetodoPago" runat="server" CssClass="nav-link text-white px-2" OnClick="btnMetodoPago_Click">Método de Pago</asp:LinkButton>
                        <asp:LinkButton ID="btnServicios" runat="server" CssClass="nav-link text-white px-2" OnClick="btnServicios_Click">Servicios</asp:LinkButton>
                    </asp:Panel>

                    <!-- Panel para ocultar/mostrar los botones de reservas-->
                    <asp:Panel ID="panelAdministarReservas" runat="server" CssClass="d-flex gap-3">
                        <asp:LinkButton ID="btnReserva" runat="server" CssClass="nav-link text-white px-2" OnClick="btnReserva_Click">Reservas</asp:LinkButton>
                        <asp:LinkButton ID="btnCrearReserva" runat="server" CssClass="nav-link text-white px-2" OnClick="btnCrearReserva_Click1">Crear Reserva</asp:LinkButton>
                        <asp:LinkButton ID="btnHistorialReserva" runat="server" CssClass="nav-link text-white px-2" OnClick="btnHistorialReservas_Click">Historial de Reservas</asp:LinkButton>
                    </asp:Panel>
                </div>

                <div class="card-body">
                    <!-- Panel para Panel Usuario -->
                    <asp:Panel ID="panelUsuario" runat="server" Visible="false">
                        <asp:Button ID="btnNuevoUsuario" runat="server" Text="Nuevo Usuario" CssClass="btn btn-primary" OnClick="btnMostrarFormularioUsuarios_Click" />
                        <div class="mt-3" style="overflow-x: auto; width: 100%;">
                            <asp:GridView ID="grvUsuario" runat="server" AutoGenerateColumns="False" DataKeyNames="Id_usuario" CssClass="table table-striped table-bordered w-100"
                                AllowPaging="true"
                                PageSize="5"
                                OnRowCancelingEdit="grvUsuario_RowCancelingEdit"
                                OnRowEditing="grvUsuario_RowEditing"
                                OnRowUpdating="grvUsuario_RowUpdating"
                                OnRowDataBound="grvUsuario_RowDataBound"
                                OnPageIndexChanging="grvUsuario_PageIndexChanging"
                                OnRowCommand="grvUsuario_RowCommand">
                                <Columns>
                                    <asp:TemplateField HeaderText="Usuario">
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
                                    <asp:TemplateField HeaderText="Acciones">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnEditar" runat="server" CommandName="Edit" CssClass="btn btn-sm btn-outline-primary me-1"><i class="bi bi-pencil"></i></asp:LinkButton>
                                            <asp:Button ID="btnDarDeBajaUsuario" runat="server" Text="Dar de baja"
                                                CommandName="DarDeBajaUsuario"
                                                CommandArgument='<%# Eval("Id_usuario") %>'
                                                CssClass="btn btn-outline-danger btn-sm" />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:LinkButton ID="btnActualizar" runat="server" CommandName="Update" CssClass="btn btn-sm btn-outline-success  me-1"><i class="bi bi-check"></i></asp:LinkButton>
                                            <asp:LinkButton ID="btnCancelar" runat="server" CommandName="Cancel" CssClass="btn btn-sm  btn-outline-danger "><i class="bi bi-x"></i></asp:LinkButton>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>

                        </div>
                    </asp:Panel>
                    <!-- Panel para Registrar Usuario Dinamico -->
                    <asp:Panel ID="panelRegistrarUsuario" runat="server" Visible="false" CssClass="mt-4 p-3 border rounded">
                        <h6 class="fw-bold mb-3">Agregar Usuario</h6>
                        <div class="row mb-3">
                            <div class="col-md-4">
                                <label for="txtName" class="form-label">Usuario</label>
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
                    <!-- Panel para Panel Metodo de Pago -->
                    <asp:Panel ID="panelMetodoPago" runat="server" Visible="false">
                        <asp:Button ID="btnNuevoMetodo" runat="server" Text="Nuevo Metodo" CssClass="btn btn-primary" OnClick="btnNuevoMetodo_Click" />

                        <div class="mt-3" style="overflow-x: auto; width: 100%;">

                            <asp:GridView ID="grvMetodoPago"
                                runat="server"
                                AutoGenerateColumns="False"
                                DataKeyNames="Id_metodoPago"
                                CssClass="table table-striped table-bordered w-100"
                                AllowPaging="true"
                                PageSize="5"
                                OnRowCancelingEdit="grvMetodoPago_RowCancelingEdit"
                                OnRowEditing="grvMetodoPago_RowEditing"
                                OnRowUpdating="grvMetodoPago_RowUpdating"
                                OnPageIndexChanging="grvMetodoPago_PageIndexChanging"
                                OnRowDataBound="grvMetodoPago_RowDataBound"
                                OnRowCommand="grvMetodoPago_RowCommand">
                                <Columns>
                                    <asp:TemplateField HeaderText="Metodo de Pago">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtEINombrePago" runat="server" Text='<%# Bind("Nombre") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="txtNombrePago" runat="server" Text='<%# Bind("Nombre") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Acciones">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnEditar" runat="server" CommandName="Edit" CssClass="btn btn-sm btn-outline-primary me-1"><i class="bi bi-pencil"></i></asp:LinkButton>
                                            <asp:Button ID="btnDarDeBajaMetodoPago" runat="server" Text="Dar de baja"
                                                CommandName="DarDeBajaMetodoPago"
                                                CommandArgument='<%# Eval("Id_metodoPago") %>'
                                                CssClass="btn btn-outline-danger btn-sm" />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:LinkButton ID="btnActualizar" runat="server" CommandName="Update" CssClass="btn btn-sm btn-outline-success  me-1"><i class="bi bi-check"></i></asp:LinkButton>
                                            <asp:LinkButton ID="btnCancelar" runat="server" CommandName="Cancel" CssClass="btn btn-sm  btn-outline-danger "><i class="bi bi-x"></i></asp:LinkButton>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>

                            <asp:Panel ID="panelRegistrarMetodo" runat="server" Visible="false" CssClass="mt-4 p-3 border rounded">
                                <h6 class="fw-bold mb-3">Agregar Método de Pago</h6>

                                <div class="row mb-3">
                                    <div class="col-md-4">
                                        <label for="txtNameMetodoPago" class="form-label">Metodo de Pago</label>
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

                            <asp:GridView ID="grvServicio"
                                runat="server"
                                AutoGenerateColumns="False"
                                DataKeyNames="Id_servicioAdicional"
                                CssClass="table table-striped table-bordered w-100"
                                AllowPaging="true"
                                PageSize="5"
                                OnPageIndexChanging="grvServicio_PageIndexChanging"
                                OnRowCancelingEdit="grvServicio_RowCancelingEdit"
                                OnRowEditing="grvServicio_RowEditing"
                                OnRowUpdating="grvServicio_RowUpdating"
                                OnRowDataBound="grvServicio_RowDataBound"
                                OnRowCommand="grvServicio_RowCommand">
                                <Columns>
                                    <asp:TemplateField HeaderText="Servicio">
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
                                    <asp:TemplateField HeaderText="Acciones">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnEditar" runat="server" CommandName="Edit" CssClass="btn btn-sm btn-outline-primary me-1"><i class="bi bi-pencil"></i></asp:LinkButton>
                                            <asp:Button ID="btnDarDeBajaServicio" runat="server" Text="Dar de baja"
                                                CommandName="DarDeBajaServicio"
                                                CommandArgument='<%# Eval("Id_servicioAdicional") %>'
                                                CssClass="btn btn-outline-danger btn-sm" />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:LinkButton ID="btnActualizar" runat="server" CommandName="Update" CssClass="btn btn-sm btn-outline-success  me-1"><i class="bi bi-check"></i></asp:LinkButton>
                                            <asp:LinkButton ID="btnCancelar" runat="server" CommandName="Cancel" CssClass="btn btn-sm  btn-outline-danger "><i class="bi bi-x"></i></asp:LinkButton>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>

                            <asp:Panel ID="panelRegistrarServicio" runat="server" Visible="false" CssClass="mt-4 p-3 border rounded">
                                <h6 class="fw-bold mb-3">Agregar Servicio Adicional</h6>

                                <div class="row mb-3">
                                    <div class="col-md-4">
                                        <label for="txtNameServicio" class="form-label">Servicio</label>
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

                    <!-- Panel para Huespedes -->
                    <asp:Panel ID="panelHuespedes" runat="server" Visible="false">
                        <asp:Button ID="btnNuevoHuesped" runat="server" Text="Nuevo Huesped" CssClass="btn btn-primary" OnClick="btnNuevoHuesped_Click" />
                        <div class="mt-3" style="overflow-x: auto; width: 100%;">
                            <asp:GridView ID="grvHuespedes"
                                runat="server"
                                AutoGenerateColumns="False"
                                DataKeyNames="Id_huesped"
                                CssClass="table table-striped table-bordered w-100"
                                AllowPaging="true"
                                PageSize="5"
                                OnPageIndexChanging="grvHuespedes_PageIndexChanging"
                                OnRowCancelingEdit="grvHuespedes_RowCancelingEdit"
                                OnRowEditing="grvHuespedes_RowEditing"
                                OnRowUpdating="grvHuespedes_RowUpdating"
                                OnRowDataBound="grvHuespedes_RowDataBound"
                                OnRowCommand="grvHuespedes_RowCommand">
                                <Columns>
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
                                            <asp:DropDownList ID="txtEITipoDocumento" runat="server" CssClass="form-select"></asp:DropDownList>
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
                                            <asp:TextBox ID="txtEIFechaNacimiento" runat="server" TextMode="Date"
                                                Text='<%# Bind("FechaNacimiento", "{0:yyyy-MM-dd}") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblFechaNacimiento" runat="server" Text='<%# Bind("FechaNacimiento", "{0:dd/MM/yyyy}") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Acciones">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnEditar" runat="server" CommandName="Edit" CssClass="btn btn-sm btn-outline-primary me-1"><i class="bi bi-pencil"></i></asp:LinkButton>
                                            <asp:Button ID="btnDarDeBajaHuesped" runat="server" Text="Dar de baja"
                                                CommandName="DarDeBajaHuesped"
                                                CommandArgument='<%# Eval("Id_huesped") %>'
                                                CssClass="btn btn-outline-danger btn-sm" />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <div style="max-width: 75px; width: 75px; white-space: nowrap; overflow: hidden;">
                                                <asp:LinkButton ID="btnActualizar" runat="server" CommandName="Update" CssClass="btn btn-sm btn-outline-success  me-1"><i class="bi bi-check"></i></asp:LinkButton>
                                                <asp:LinkButton ID="btnCancelar" runat="server" CommandName="Cancel" CssClass="btn btn-sm  btn-outline-danger "><i class="bi bi-x"></i></asp:LinkButton>
                                            </div>
                                        </EditItemTemplate>
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
                                            <asp:ListItem>DNI</asp:ListItem>
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

                    <%--Panel para Habitaciones--%>
                    <asp:Panel ID="panelHabitaciones" runat="server" Visible="false">
                        <!-- Botón para iniciar registro -->
                        <div class="col text-start">
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
                                            <asp:LinkButton ID="btnEditar" runat="server" CommandName="Edit" CssClass="btn btn-sm btn-outline-primary me-1"><i class="bi bi-pencil"></i></asp:LinkButton>
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

                    <!-- Panel para Reservas -->
                    <asp:Panel ID="panelReservas" runat="server" Visible="false">
                        <div class="mt-3" style="overflow-x: auto; width: 100%;">
                            <asp:GridView ID="grvReservas" runat="server" AutoGenerateColumns="False" AllowPaging="True" PageSize="5"
                                OnPageIndexChanging="grvReservas_PageIndexChanging"
                                OnRowCommand="grvReservas_RowCommand"
                                OnRowDataBound="grvReservas_RowDataBound"
                                CssClass="table table-striped table-bordered" DataKeyNames="Id_reserva">
                                <Columns>
                                    <asp:BoundField HeaderText="Nombre Completo" DataField="NombreCompleto" />
                                    <asp:BoundField HeaderText="Documento" DataField="Documento" />
                                    <asp:BoundField HeaderText="Email" DataField="Email" />
                                    <asp:BoundField HeaderText="Teléfono" DataField="Telefono" />
                                    <asp:BoundField HeaderText="Fecha Reserva" DataField="FechaReserva" DataFormatString="{0:dd/MM/yyyy}" />
                                    <asp:BoundField HeaderText="Fecha Llegada" DataField="FechaLlegada" DataFormatString="{0:dd/MM/yyyy}" />
                                    <asp:BoundField HeaderText="Fecha Salida" DataField="FechaSalida" DataFormatString="{0:dd/MM/yyyy}" />
                                    <asp:BoundField HeaderText="Fecha de Pago" DataField="FechaPago" DataFormatString="{0:dd/MM/yyyy}" />
                                    <asp:BoundField HeaderText="Huéspedes" DataField="CantidadHuespedes" />
                                    <asp:BoundField HeaderText="Monto Total" DataField="MontoTotal" DataFormatString="{0:C}" />
                                    <asp:TemplateField HeaderText="Detalles">
                                        <ItemTemplate>
                                            <asp:Button ID="btnVerDetalle" runat="server"
                                                Text='<%# GetButtonTextReserva((int)Eval("Id_reserva")) %>'
                                                CommandName="MostrarDetalle"
                                                CommandArgument='<%# Eval("Id_reserva") %>'
                                                CssClass="btn btn-outline-success btn-sm" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:Button ID="btnDarDeBajaReserva" runat="server" Text="Dar de baja"
                                                CommandName="DarDeBajaReserva"
                                                CommandArgument='<%# Eval("Id_reserva") %>'
                                                CssClass="btn btn-outline-danger btn-sm" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </asp:Panel>

                    <asp:Panel ID="panelCheckInOut" runat="server" Visible="false">
                        <div class="container" style="margin: 10px">
                            <asp:HiddenField ID="hfIdDetalleReserva" runat="server" />

                            <div class="row mb-3 align-items-end">
                                <div class="col-md-3">
                                    <asp:Label ID="lblCheckin" runat="server" Text="Check-In" CssClass="form-label d-block" />
                                    <!-- Botón para hacer Check-In -->
                                    <asp:Button ID="btnHacerCheckIn" runat="server" Text="Hacer Check-In" CssClass="btn btn-outline-primary" OnClick="btnHacerCheckIn_Click" />
                                    <!-- Mostrar fecha si ya se hizo Check-In -->
                                    <asp:Label ID="lblFechaCheckIn" runat="server" CssClass="form-label" Visible="false" />
                                </div>

                                <div class="col-md-3">
                                    <asp:Label ID="lblCheckOut" runat="server" Text="Check-Out" CssClass="form-label" />
                                    <asp:TextBox ID="txtCheckOut" runat="server" CssClass="form-control" TextMode="Date" />
                                </div>
                            </div>

                            <div class="d-flex gap-2">
                                <asp:Button ID="btnRegistrarCheckInOut" runat="server" Text="Registrar Check-Out" CssClass="btn btn-success" OnClick="btnRegistrarCheckInOut_Click" />
                                <asp:Button ID="btnCancelarCheckInOut" runat="server" Text="Cancelar" CssClass="btn btn-secondary" OnClick="btnCancelarCheckInOut_Click" />
                            </div>
                        </div>
                    </asp:Panel>
                    <!-- Panel para Crear Reservas -->
                    <!-- ETAPA 1 -->
                    <asp:Panel ID="panelCrearReservaEtapa1" runat="server" Visible="false">
                        <div class="container" style="margin: 10px">
                            <div class="row mb-3 align-items-end">
                                <div class="col-md-4">
                                    <asp:Label ID="lblHuespedBuscarPorDocumento" runat="server" Text="Buscar por Documento" CssClass="form-label" />
                                    <asp:TextBox ID="txtHuespedBuscarPorDocumento" runat="server" CssClass="form-control" />
                                </div>
                                <div class="col-md-4 d-flex align-items-end">
                                    <asp:Button ID="btnFiltarHuespedPorDocumento" runat="server" Text="Filtrar" CssClass="btn btn-primary me-2" OnClick="btnFiltarHuespedPorDocumento_Click" />
                                    <asp:Button ID="btnLimpiarFiltroHuespedPorDocumento" runat="server" Text="Limpiar Filtro" CssClass="btn btn-secondary" OnClick="btnLimpiarFiltroHuespedPorDocumento_Click" />
                                </div>
                            </div>

                            <asp:GridView
                                ID="grvCrearReservaEtapa1"
                                runat="server"
                                AutoGenerateColumns="False"
                                DataKeyNames="Id_huesped"
                                CssClass="table table-striped table-bordered w-100"
                                AllowPaging="true" PageSize="5"
                                OnPageIndexChanging="GridViewCrearReservaEtapa1_PageIndexChanging"
                                OnSelectedIndexChanged="grvCrearReservaEtapa1_SelectedIndexChanged"
                                OnRowDataBound="grvCrearReservaEtapa1_RowDataBound"
                                AutoPostBack="True">
                                <Columns>
                                    <asp:TemplateField HeaderText="Nombre">
                                        <ItemTemplate>
                                            <asp:Label ID="txtNombreHuesped" runat="server" Text='<%# Bind("Nombre") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Apellido">
                                        <ItemTemplate>
                                            <asp:Label ID="txtApellido" runat="server" Text='<%# Bind("Apellido") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Documento">
                                        <ItemTemplate>
                                            <asp:Label ID="txtDocumento" runat="server" Text='<%# Bind("Documento") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Tipo Documento">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTipoDocumento" runat="server" Text='<%# Bind("TipoDocumento") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Email">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEmail" runat="server" Text='<%# Bind("Email") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Teléfono">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTelefono" runat="server" Text='<%# Bind("Telefono") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Fecha Nacimiento">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFechaNacimiento" runat="server" Text='<%# Bind("FechaNacimiento", "{0:dd/MM/yyyy}") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:CommandField ShowSelectButton="True" SelectText="Seleccionar" HeaderText="Acciones" ButtonType="Link" ControlStyle-CssClass="btn btn-sm btn-outline-success" />
                                </Columns>
                            </asp:GridView>
                            <div class="row mb-3 align-items-end">
                                <div class="col-md-4">
                                </div>

                                <div class="col-md-4">
                                </div>

                                <div class="col-md-4 d-flex justify-content-end">
                                    <asp:Button ID="btnSiguienteEtapa1" runat="server" Text="Siguiente" CssClass="btn" OnClick="btnSiguienteEtapa1_Click" />
                                </div>
                            </div>
                        </div>
                    </asp:Panel>

                    <!-- ETAPA 2 -->
                    <asp:Panel ID="panelCrearReservaEtapa2" runat="server" Visible="false">
                        <div class="container" style="margin: 10px">
                            <div class="row mb-3 align-items-end">
                                <div class="col-md-3">
                                    <asp:Label ID="lblCantidadDeHuespedes" runat="server" Text="Cantidad de Huespedes" CssClass="form-label" />
                                    <asp:TextBox ID="txtCantidadDeHuespedes" runat="server" CssClass="form-control" />
                                </div>
                                <div class="col-md-3">
                                    <asp:Label ID="lblFechaDesde" runat="server" Text="Fecha Desde:" CssClass="form-label" />
                                    <asp:TextBox ID="txtFechaDesde" runat="server" CssClass="form-control" TextMode="Date" />
                                </div>
                                <div class="col-md-3">
                                    <asp:Label ID="lblFechaHastsa" runat="server" Text="Fecha Hasta:" CssClass="form-label" />
                                    <asp:TextBox ID="txtFechaHasta" runat="server" CssClass="form-control" TextMode="Date" />
                                </div>
                                <div class="col-md-3">
                                    <div class="d-flex gap-2">
                                        <asp:Button ID="btnFiltrarHabitacionPorFechas" runat="server" Text="Filtrar" CssClass="btn btn-primary w-100" OnClick="btnFiltrarHabitacionPorFechas_Click" />
                                        <asp:Button ID="btnLimpiarFiltroHabitacionPorFechas" runat="server" Text="Limpiar Filtro" CssClass="btn btn-secondary w-100" OnClick="btnLimpiarFiltroHabitacionPorFechas_Click" />
                                    </div>
                                </div>
                                <asp:Label ID="lblError" runat="server" Text="" CssClass="form-label" />
                            </div>

                            <asp:GridView
                                ID="grvCrearReservaEtapa2"
                                runat="server"
                                AutoGenerateColumns="False"
                                DataKeyNames="Id_habitacion"
                                CssClass="table table-striped table-bordered w-100"
                                AllowPaging="true" PageSize="5"
                                OnPageIndexChanging="GridViewCrearReservaEtapa2_PageIndexChanging"
                                OnRowDataBound="grvCrearReservaEtapa2_RowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderText="Número">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNumeroHabitacion" runat="server" Text='<%# Bind("NumeroHabitacion") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Tipo">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTipo" runat="server" Text='<%# Bind("Tipo") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Capacidad">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCapacidad" runat="server" Text='<%# Bind("Capacidad") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Precio Por Noche">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPrecio" runat="server" Text='<%# Bind("Precio", "{0:C2}") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Descripción">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDescripcion" runat="server" Text='<%# Bind("Descripcion") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Seleccionar">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkSeleccionarHabitacion" runat="server" AutoPostBack="True" OnCheckedChanged="chkSeleccionarHabitacion_CheckedChanged" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <div class="row mb-3 align-items-end">
                                <div class="col-md-4">
                                    <asp:Button ID="btnVolverEtapa2" runat="server" Text="Volver" CssClass="btn btn-secondary" OnClick="btnVolverEtapa2_Click" />
                                </div>

                                <div class="col-md-4">
                                    <asp:Label ID="lblText" runat="server" Text="" CssClass="form-label" />
                                </div>

                                <div class="col-md-4 d-flex justify-content-end">
                                    <asp:Button ID="btnSiguienteEtapa2" runat="server" Text="Siguiente" CssClass="btn" OnClick="btnSiguienteEtapa2_Click" />
                                </div>
                            </div>
                        </div>
                    </asp:Panel>

                    <!-- ETAPA 3 -->
                    <asp:Panel ID="panelCrearReservaEtapa3" runat="server" Visible="false">
                        <div class="container" style="margin: 10px">
                            <asp:GridView
                                ID="grvCrearReservaEtapa3"
                                runat="server"
                                AutoGenerateColumns="False"
                                DataKeyNames="Id_servicioAdicional"
                                CssClass="table table-striped table-bordered w-100"
                                AllowPaging="true" PageSize="5"
                                OnPageIndexChanging="GridViewCrearReservaEtapa3_PageIndexChanging"
                                OnRowDataBound="grvCrearReservaEtapa3_RowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderText="Servicio">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNombreServicio" runat="server" Text='<%# Bind("NombreServicio") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Precio">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPrecioServicio" runat="server" Text='<%# Bind("Precio", "{0:C2}") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Seleccionar">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkSeleccionarServicio" runat="server" AutoPostBack="True" OnCheckedChanged="chkSeleccionarServicio_CheckedChanged" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>

                            <div class="row mb-3 align-items-end">
                                <div class="col-md-4">
                                    <asp:Button ID="btnVolverEtapa3" runat="server" Text="Volver" CssClass="btn btn-secondary" OnClick="btnVolverEtapa3_Click" />
                                </div>

                                <div class="col-md-4">
                                    <asp:Label ID="Label13" runat="server" Text="" CssClass="form-label" />
                                </div>

                                <div class="col-md-4 d-flex justify-content-end">
                                    <asp:Button ID="btnSiguienteEtapa3" runat="server" Text="Siguiente" CssClass="btn btn-primary" OnClick="btnSiguienteEtapa3_Click" />
                                </div>
                            </div>
                        </div>
                    </asp:Panel>

                    <!-- ETAPA 4 -->
                    <asp:Panel ID="panelCrearReservaEtapa4" runat="server" Visible="false">
                        <div class="container" style="margin: 10px">
                            <asp:GridView
                                ID="grvCrearReservaEtapa4"
                                runat="server"
                                AutoGenerateColumns="False"
                                DataKeyNames="Id_metodoPago"
                                CssClass="table table-striped table-bordered w-100"
                                AllowPaging="true" PageSize="5"
                                OnPageIndexChanging="GridViewCrearReservaEtapa4_PageIndexChanging"
                                OnSelectedIndexChanged="grvCrearReservaEtapa4_SelectedIndexChanged"
                                OnRowDataBound="grvCrearReservaEtapa4_RowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderText="Nombre">
                                        <ItemTemplate>
                                            <asp:Label ID="txtNombreMetodoPago" runat="server" Text='<%# Bind("Nombre") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:CommandField ShowSelectButton="True" SelectText="Seleccionar" HeaderText="Acciones" ButtonType="Link" ControlStyle-CssClass="btn btn-sm btn-outline-success" />
                                </Columns>
                            </asp:GridView>

                            <div class="row mb-3">
                                <div class="col-md-6">
                                    <asp:Label ID="lblNumeroTarjeta" runat="server" Text="Número de Tarjeta:" CssClass="form-label" />
                                    <asp:TextBox ID="txtNumeroTarjeta" runat="server" CssClass="form-control" MaxLength="16" />
                                </div>
                                <div class="col-md-3">
                                    <asp:Label ID="lblVencimiento" runat="server" Text="Vencimiento (MM/AA):" CssClass="form-label" />
                                    <asp:TextBox ID="txtVencimiento" runat="server" CssClass="form-control" MaxLength="5" Placeholder="MM/AA" />
                                </div>
                            </div>

                            <div class="row mb-3">
                                <div class="row mb-3">
                                    <asp:Label ID="lblErrorMetodoPago" runat="server" Text="" CssClass="form-label" />
                                </div>
                                <div class="col-md-3">
                                    <asp:Label ID="lblMontoTotal" runat="server" Text="" CssClass="form-label" />
                                </div>
                                <div class="row mb-3">
                                    <asp:Label ID="lblErrorMetodoPago1" runat="server" Text="" CssClass="form-label" />
                                </div>
                            </div>

                            <div class="row mb-3">
                                <asp:Label ID="lblData" runat="server" Text="" CssClass="form-label" />
                            </div>

                            <div class="row mb-3 align-items-end">
                                <div class="col-md-4">
                                    <asp:Button ID="btnVolverEtapa4" runat="server" Text="Volver" CssClass="btn btn-secondary" OnClick="btnVolverEtapa4_Click" />
                                </div>

                                <div class="col-md-4">
                                    <asp:Label ID="Label9" runat="server" Text="" CssClass="form-label" />
                                </div>

                                <div class="col-md-4 d-flex justify-content-end">
                                    <asp:Button ID="btnRegistrarReserva" runat="server" Text="Crear Reserva" CssClass="btn" OnClick="btnRegistrarReserva_Click" />
                                </div>
                            </div>
                        </div>
                    </asp:Panel>

                    <!-- Panel para Historial de Reservas -->
                    <asp:Panel ID="panelHistorialReservas" runat="server" Visible="false">
                        <div class="mt-3" style="overflow-x: auto; width: 100%;">
                            <asp:GridView ID="grvHistorialReservas" runat="server" AutoGenerateColumns="False" AllowPaging="True" PageSize="5"
                                OnPageIndexChanging="grvHistorialReservas_PageIndexChanging"
                                OnRowCommand="grvHistorialReservas_RowCommand"
                                OnRowDataBound="grvHistorialReservas_RowDataBound"
                                CssClass="table table-striped table-bordered" DataKeyNames="Id_reserva">
                                <Columns>
                                    <asp:BoundField HeaderText="Nombre Completo" DataField="NombreCompleto" />
                                    <asp:BoundField HeaderText="Documento" DataField="Documento" />
                                    <asp:BoundField HeaderText="Email" DataField="Email" />
                                    <asp:BoundField HeaderText="Teléfono" DataField="Telefono" />
                                    <asp:BoundField HeaderText="Fecha Reserva" DataField="FechaReserva" DataFormatString="{0:dd/MM/yyyy}" />
                                    <asp:BoundField HeaderText="Fecha Llegada" DataField="FechaLlegada" DataFormatString="{0:dd/MM/yyyy}" />
                                    <asp:BoundField HeaderText="Fecha Salida" DataField="FechaSalida" DataFormatString="{0:dd/MM/yyyy}" />
                                    <asp:BoundField HeaderText="Fecha de Pago" DataField="FechaPago" DataFormatString="{0:dd/MM/yyyy}" />
                                    <asp:BoundField HeaderText="Huéspedes" DataField="CantidadHuespedes" />
                                    <asp:BoundField HeaderText="Monto Total" DataField="MontoTotal" DataFormatString="{0:C}" />
                                    <asp:TemplateField HeaderText="Detalles">
                                        <ItemTemplate>
                                            <asp:Button ID="btnVerDetalle" runat="server"
                                                Text='<%# GetButtonTextReservaHistorial(Convert.ToInt32(Eval("Id_reserva"))) %>'
                                                CommandName="MostrarDetalle"
                                                CommandArgument='<%# Eval("Id_reserva") %>'
                                                CssClass="btn btn-outline-success btn-sm" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
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
