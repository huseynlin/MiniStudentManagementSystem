<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="MiniStudentManagementSystem.Register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Qeydiyyat - Mini Tələbə İdarəetmə Sistemi</title>
    <meta charset="utf-8" />
    <style>
        body {
            font-family: Arial, sans-serif;
            background-color: #f4f4f4;
            margin: 0;
            padding: 20px;
        }
        .container {
            max-width: 400px;
            margin: 50px auto;
            background-color: white;
            padding: 30px;
            border-radius: 5px;
            box-shadow: 0 0 10px rgba(0,0,0,0.1);
        }
        h1 {
            text-align: center;
            color: #333;
        }
        .form-group {
            margin-bottom: 15px;
        }
        label {
            display: block;
            margin-bottom: 5px;
            color: #555;
            font-weight: bold;
        }
        input[type="text"], input[type="password"], input[type="email"] {
            width: 100%;
            padding: 10px;
            border: 1px solid #ddd;
            border-radius: 3px;
            box-sizing: border-box;
            font-size: 14px;
        }
        input[type="button"] {
            width: 100%;
            padding: 10px;
            background-color: #4CAF50;
            color: white;
            border: none;
            border-radius: 3px;
            cursor: pointer;
            font-size: 16px;
            font-weight: bold;
        }
        input[type="button"]:hover {
            background-color: #45a049;
        }
        .message {
            margin-top: 15px;
            padding: 10px;
            border-radius: 3px;
            text-align: center;
        }
        .success {
            background-color: #d4edda;
            color: #155724;
            border: 1px solid #c3e6cb;
        }
        .error {
            background-color: #f8d7da;
            color: #721c24;
            border: 1px solid #f5c6cb;
        }
        .login-link {
            text-align: center;
            margin-top: 15px;
        }
        .login-link a {
            color: #007bff;
            text-decoration: none;
        }
        .login-link a:hover {
            text-decoration: underline;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h1>Hesab Yaratın</h1>

            <div class="form-group">
                <label for="UsernameTextBox">İstifadəçi adı:</label>
                <asp:TextBox ID="UsernameTextBox" runat="server"></asp:TextBox>
            </div>

            <div class="form-group">
                <label for="EmailTextBox">Elektron poçt:</label>
                <asp:TextBox ID="EmailTextBox" runat="server" TextMode="Email"></asp:TextBox>
            </div>

            <div class="form-group">
                <label for="PasswordTextBox">Şifrə:</label>
                <asp:TextBox ID="PasswordTextBox" runat="server" TextMode="Password"></asp:TextBox>
            </div>

            <div>
                <asp:Button ID="RegisterButton" runat="server" Text="Qeydiyyatdan keç" OnClick="RegisterButton_Click" />
            </div>

            <asp:Label ID="MessageLabel" runat="server" Visible="false"></asp:Label>

            <div class="login-link">
                <p>Artıq hesabınız var? <a href="Login.aspx">Buradan daxil olun</a></p>
            </div>
        </div>
    </form>
</body>
</html>
