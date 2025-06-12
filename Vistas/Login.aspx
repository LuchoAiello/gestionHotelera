<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Vistas.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">


h2 {
    margin-bottom: 20px;
}

    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Login</h2>
        </div>
        <asp:TextBox ID="txtNameLogin" runat="server"></asp:TextBox>
        <p>
            <asp:TextBox ID="txtPasswordLogin" runat="server" TextMode="Password"></asp:TextBox>
        </p>
        <asp:Button ID="btnLogin" runat="server" Height="30px" OnClick="btnLogin_Click" Text="Ingresar" Width="100px" />
        <p>
            <asp:Label ID="txtError" runat="server"></asp:Label>
        </p>
    </form>
</body>
</html>
