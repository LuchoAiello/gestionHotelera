<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="Vistas.Home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="lblNameLogin" runat="server"></asp:Label>
        </div>
        <asp:Button ID="btnRegisterUser" runat="server" Text="Registrar Usuario" />
        <asp:Button ID="btnReserv" runat="server" Text="Crear Reserva" />
        <asp:Button ID="btnRooms" runat="server" Text="Estado de habitaciones" />
        <asp:Button ID="btnHistorialReservas" runat="server" OnClick="btnHistorialReservas_Click" Text="Historial de Reservas" />
        <asp:Button ID="btnLogout" runat="server" OnClick="btnLogout_Click" Text="Cerrar Sesion" />
    </form>
</body>
</html>
