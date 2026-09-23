<%@ Page Language="C#" AutoEventWireup="true"
    CodeBehind="Login.aspx.cs"
    Inherits="Password_Manager.Login" %>

<!DOCTYPE html>

<html>
<head runat="server">

    <title>Login - Password Manager</title>

    <link href="Style.css"
        rel="stylesheet"
        type="text/css" />

    <style>

        .auth-container {
            width: 90%;
            max-width: 450px;
            margin: 80px auto;
        }

        .auth-title {
            text-align: center;
            margin-bottom: 30px;
        }

        .auth-button {
            width: 100%;
            margin-top: 10px;
        }

        .auth-link {
            display: block;
            text-align: center;
            margin-top: 20px;
        }

        .validation-message {
            display: block;
            color: #dc2626;
            font-size: 13px;
            margin-top: 5px;
        }

    </style>

</head>

<body>

<form id="form1" runat="server">

    <div class="auth-container">

        <div class="card">

            <div class="auth-title">

                <h1>Password Manager</h1>

                <p>Login to your account</p>

            </div>


            <div class="form-group">

                <label>Email</label>

                <asp:TextBox
                    ID="txtEmail"
                    runat="server"
                    TextMode="Email"
                    CssClass="form-control">
                </asp:TextBox>

                <asp:RequiredFieldValidator
                    ID="rfvEmail"
                    runat="server"
                    ControlToValidate="txtEmail"
                    ErrorMessage="Email is required."
                    CssClass="validation-message">
                </asp:RequiredFieldValidator>

                <asp:RegularExpressionValidator
                    ID="revEmail"
                    runat="server"
                    ControlToValidate="txtEmail"
                    ValidationExpression="^[^@\s]+@[^@\s]+\.[^@\s]+$"
                    ErrorMessage="Enter a valid email address."
                    CssClass="validation-message">
                </asp:RegularExpressionValidator>

            </div>


            <div class="form-group">

                <label>Password</label>

                <asp:TextBox
                    ID="txtPassword"
                    runat="server"
                    TextMode="Password"
                    CssClass="form-control">
                </asp:TextBox>

                <asp:RequiredFieldValidator
                    ID="rfvPassword"
                    runat="server"
                    ControlToValidate="txtPassword"
                    ErrorMessage="Password is required."
                    CssClass="validation-message">
                </asp:RequiredFieldValidator>

            </div>


            <asp:Button
                ID="btnLogin"
                runat="server"
                Text="Login"
                CssClass="btn btn-primary auth-button"
                OnClick="btnLogin_Click" />


            <asp:Label
                ID="lblMessage"
                runat="server"
                CssClass="error-message">
            </asp:Label>


            <div class="auth-link">

                <span>Don't have an account?</span>

                <br />

                <asp:HyperLink
                    ID="lnkRegister"
                    runat="server"
                    NavigateUrl="Register.aspx">
                    Create an account
                </asp:HyperLink>

            </div>

        </div>

    </div>

</form>

</body>
</html>