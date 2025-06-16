<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HistorialReservas.aspx.cs" Inherits="Vistas.WebForm2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">


h2 {
    color: #333;
}

    </style>
</head>
<body>
    <form id="form1" runat="server">
        <h2>Historial de Reservas&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnBack" runat="server" OnClick="btnBack_Click" Text="Volver Atras" />
        </h2>
        <p>
            <asp:Label ID="Label1" runat="server" Text="Numero de Habitacion:"></asp:Label>
            <asp:TextBox ID="txtNumber" runat="server"></asp:TextBox>
        </p>
        <p>
            <asp:Label ID="Label2" runat="server" Text="Fecha Desde:"></asp:Label>
&nbsp;<asp:TextBox ID="txtDateFrom" runat="server"></asp:TextBox>
        </p>
        <p>
            <asp:Label ID="Label3" runat="server" Text="Fecha Hasta:"></asp:Label>
            <asp:TextBox ID="txtDateTo" runat="server"></asp:TextBox>
        </p>
        <p>
            <asp:Button ID="btnFilter" runat="server" OnClick="btnFilter_Click" Text="Filtrar" />
            <asp:Button ID="btnSacarFiltro" runat="server" OnClick="btnSacarFiltro_Click" Text="Sacar Filtro" />
        </p>
        <asp:GridView ID="grvHistorialReservas" runat="server" 
              AutoGenerateColumns="False"
              CssClass="table table-striped table-bordered">
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
    </form>
</body>
</html>
