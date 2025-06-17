<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Vistas.WebForm1" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Login</title>

    
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        body {
            background-color: #f8f9fa;
        }

        .login-container {
            max-width: 400px;
            margin: 80px auto;
            padding: 30px;
            background-color: white;
            border-radius: 10px;
            box-shadow: 0 4px 10px rgba(0, 0, 0, 0.1);
        }

        .form-label {
            margin-top: 15px;
        }

        .btn {
            margin-top: 20px;
        }

        .error-label {
            color: red;
            margin-top: 10px;
            display: block;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="login-container">
            <h2 class="text-center">Login</h2>

            <div class="mb-3">
                <label for="txtNameLogin" class="form-label">Usuario</label>
                <asp:TextBox ID="txtNameLogin" runat="server" CssClass="form-control" />
            </div>

            <div class="mb-3">
                <label for="txtPasswordLogin" class="form-label">Contraseña</label>
                <asp:TextBox ID="txtPasswordLogin" runat="server" TextMode="Password" CssClass="form-control" />
            </div>

            <asp:Button ID="btnLogin" runat="server" Text="Ingresar" CssClass="btn btn-primary w-100" OnClick="btnLogin_Click" />

            <asp:Label ID="txtError" runat="server" CssClass="error-label text-center"></asp:Label>
        </div>
    </form>
  
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>
