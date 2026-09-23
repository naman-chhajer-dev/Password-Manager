<%@ Page Language="C#" AutoEventWireup="true"
    CodeBehind="AddCredential.aspx.cs"
    Inherits="Password_Manager.AddCredential" %>

<!DOCTYPE html>

<html>
<head runat="server">

    <title>Add Credential - Password Manager</title>

    <link href="Style.css"
        rel="stylesheet"
        type="text/css" />

</head>

<body>

<form id="form1" runat="server">

    <div class="container">

        <div class="card">

            <h1>Add Credential</h1>

            <!-- Website Name -->

            <div class="form-group">

                <label>Website Name</label>

                <asp:TextBox
                    ID="txtWebsiteName"
                    runat="server"
                    CssClass="form-control">
                </asp:TextBox>

            </div>


            <!-- Website URL -->

            <div class="form-group">

                <label>Website URL</label>

                <asp:TextBox
                    ID="txtWebsiteURL"
                    runat="server"
                    CssClass="form-control">
                </asp:TextBox>

            </div>


            <!-- Username -->

            <div class="form-group">

                <label>Username / Email</label>

                <asp:TextBox
                    ID="txtUsername"
                    runat="server"
                    CssClass="form-control">
                </asp:TextBox>

            </div>


            <!-- Password -->

            <div class="form-group">

                <label>Password</label>

                <div class="password-row">

                    <asp:TextBox
                        ID="txtPassword"
                        runat="server"
                        TextMode="Password"
                        CssClass="form-control">
                    </asp:TextBox>

                    <asp:Button
                        ID="btnGeneratePassword"
                        runat="server"
                        Text="Generate"
                        CssClass="btn btn-primary"
                        OnClick="btnGeneratePassword_Click" />

                    <asp:Button
                        ID="btnShowPassword"
                        runat="server"
                        Text="Show"
                        CssClass="btn btn-secondary"
                        OnClick="btnShowPassword_Click" />

                </div>

                <asp:Label
                    ID="lblGeneratedPassword"
                    runat="server"
                    CssClass="success-message">
                </asp:Label>

            </div>


            <!-- Notes -->

            <div class="form-group">

                <label>Notes</label>

                <asp:TextBox
                    ID="txtNotes"
                    runat="server"
                    TextMode="MultiLine"
                    Rows="5"
                    CssClass="form-control">
                </asp:TextBox>

            </div>


            <!-- Save -->

            <div class="form-group">

                <asp:Button
                    ID="btnSave"
                    runat="server"
                    Text="Save Credential"
                    CssClass="btn btn-success"
                    OnClick="btnSave_Click" />

                <asp:Button
                    ID="btnBack"
                    runat="server"
                    Text="Back to Dashboard"
                    CssClass="btn btn-secondary"
                    OnClick="btnBack_Click" />

            </div>


            <!-- Message -->

            <asp:Label
                ID="lblMessage"
                runat="server">
            </asp:Label>

        </div>

    </div>

</form>

</body>
</html>