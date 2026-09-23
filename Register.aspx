<%@ Page Language="C#" AutoEventWireup="true"
    CodeBehind="Register.aspx.cs"
    Inherits="Password_Manager.Register" %>

<!DOCTYPE html>

<html>
<head runat="server">

    <title>Register - Password Manager</title>

    <link href="Style.css"
        rel="stylesheet"
        type="text/css" />

    <style>

        .auth-container {
            width: 90%;
            max-width: 450px;
            margin: 60px auto;
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

                <p>Create your account</p>

            </div>


            <div class="form-group">

                <label>Name</label>

                <asp:TextBox
                    ID="txtName"
                    runat="server"
                    CssClass="form-control">
                </asp:TextBox>

                <asp:RequiredFieldValidator
                    ID="rfvName"
                    runat="server"
                    ControlToValidate="txtName"
                    ErrorMessage="Name is required."
                    CssClass="validation-message">
                </asp:RequiredFieldValidator>

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

                <asp:RegularExpressionValidator
                    ID="revPassword"
                    runat="server"
                    ControlToValidate="txtPassword"
                    ValidationExpression="^.{8,}$"
                    ErrorMessage="Password must contain at least 8 characters."
                    CssClass="validation-message">
                </asp:RegularExpressionValidator>

            </div>


            <div class="form-group">

                <label>Confirm Password</label>

                <asp:TextBox
                    ID="txtConfirmPassword"
                    runat="server"
                    TextMode="Password"
                    CssClass="form-control">
                </asp:TextBox>

                <asp:RequiredFieldValidator
                    ID="rfvConfirmPassword"
                    runat="server"
                    ControlToValidate="txtConfirmPassword"
                    ErrorMessage="Please confirm your password."
                    CssClass="validation-message">
                </asp:RequiredFieldValidator>

                <asp:CompareValidator
                    ID="cvPassword"
                    runat="server"
                    ControlToValidate="txtConfirmPassword"
                    ControlToCompare="txtPassword"
                    Operator="Equal"
                    Type="String"
                    ErrorMessage="Passwords do not match."
                    CssClass="validation-message">
                </asp:CompareValidator>

            </div>


            <asp:Button
                ID="btnRegister"
                runat="server"
                Text="Create Account"
                CssClass="btn btn-success auth-button"
                OnClick="btnRegister_Click" />


            <asp:Label
                ID="lblMessage"
                runat="server"
                CssClass="error-message">
            </asp:Label>


            <div class="auth-link">

                <span>Already have an account?</span>

                <br />

                <asp:HyperLink
                    ID="lnkLogin"
                    runat="server"
                    NavigateUrl="Login.aspx">
                    Login to your account
                </asp:HyperLink>

            </div>

        </div>

    </div>

</form>

</body>
</html>