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
                            <asp:LinkButton ID="btnRegisterUser" runat="server" CssClass="nav-link text-white" OnClick="btnRegisterUser_Click">Registrar Usuario</asp:LinkButton>
                        </li>
                        <li class="nav-item">
                            <asp:LinkButton ID="btnRegisterHuesped" runat="server" CssClass="nav-link text-white" OnClick="btnRegisterHuesped_Click">Registrar Huesped</asp:LinkButton>
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
        <div class="card-header bg-primary text-white">
            <asp:Label ID="lblSeccionTitulo" runat="server" Text="Sección Actual"></asp:Label>
        </div>
        <div class="card-body">

            <!-- Panel para Reservas -->
            <asp:Panel ID="panelReservas" runat="server" Visible="false">
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
