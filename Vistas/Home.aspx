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
                            <asp:LinkButton ID="btnReserv" runat="server" CssClass="nav-link text-white" OnClick="btnReserv_Click">Crear Reserva</asp:LinkButton>
                        </li>
                        <li class="nav-item">
                            <asp:LinkButton ID="btnRooms" runat="server" CssClass="nav-link text-white" OnClick="btnRooms_Click">Estado de habitaciones</asp:LinkButton>
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
                <div class="card-header bg-dark text-white">
                    <asp:Label ID="lblSeccionTitulo" runat="server" Text="Sección Actual" class="fw-bold "></asp:Label>
                </div>
                <div class="card-body">

                    <!-- Panel para Reservas -->
                    <asp:Panel ID="panelReservas" runat="server" Visible="false">
                    </asp:Panel>

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
                                <h2 class="mb-4">Registrar Usuario</h2>

                                <div class="mb-3  p-0 d-flex align-items-center gap-2">
                                    <label for="txtName" class="form-label me-2 mb-0" style="min-width: 90px;">Nombre: </label>
                                    <asp:TextBox ID="txtName" runat="server" CssClass="form-control flex-grow-1" Style="max-width: 250px;"/>
                                    &nbsp;<asp:Label ID="lblMensajeNombre" runat="server"></asp:Label>
                                </div>

                                <div class="mb-3  p-0 d-flex align-items-center gap-2">
                                    <label for="txtPassword" class="form-label me-2 mb-0" style="min-width: 90px;">Contraseña: </label>
                                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control flex-grow-1"  Style="max-width: 250px;" />
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
                        </asp:Panel>

                        <!-- Panel para Panel Servicios -->
                        <asp:Panel ID="panelServicios" runat="server" Visible="false">
                        </asp:Panel>
                    </asp:Panel>



                    <!-- Panel para Historial de Reservas -->
                    <asp:Panel ID="panelHistorialReservas" runat="server" Visible="false">
                        <p>
                            <asp:Label ID="Label1" runat="server" Text="Número de Habitación:" />
                            <asp:TextBox ID="txtNumber" runat="server" />
                        </p>
                        <p>
                            <asp:Label ID="Label2" runat="server" Text="Fecha Desde:" />
                            <asp:TextBox ID="txtDateFrom" runat="server" />
                        </p>
                        <p>
                            <asp:Label ID="Label3" runat="server" Text="Fecha Hasta:" />
                            <asp:TextBox ID="txtDateTo" runat="server" />
                        </p>
                        <p>
                            <asp:Button ID="btnFilter" runat="server" Text="Filtrar" CssClass="btn btn-primary w-10" OnClick="btnFilter_Click" />
                            <asp:Button ID="btnSacarFiltro" runat="server" Text="Sacar Filtro" CssClass="btn btn-secondary w-10" OnClick="btnSacarFiltro_Click" />
                        </p>
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

                </div>
            </div>
        </div>


    </form>

    <!-- Bootstrap JS -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>
