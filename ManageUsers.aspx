<%@ Page Language="C#" AutoEventWireup="true"
    CodeBehind="ManageUsers.aspx.cs"
    Inherits="Password_Manager.ManageUsers" %>

<!DOCTYPE html>

<html>
<head runat="server">

    <title>Manage Users - Password Manager</title>

    <link href="Style.css"
        rel="stylesheet"
        type="text/css" />

    <style>

        .page-header {
            display: flex;
            justify-content: space-between;
            align-items: center;
            margin-bottom: 25px;
        }

        .user-table {
            width: 100%;
            border-collapse: collapse;
            margin-top: 20px;
        }

        .user-table th {
            background: #2563eb;
            color: white;
            padding: 12px;
            text-align: left;
        }

        .user-table td {
            padding: 12px;
            border-bottom: 1px solid #ddd;
            vertical-align: middle;
        }

        .user-table tr:hover {
            background: #f8fafc;
        }

        .action-button {
            padding: 7px 12px;
            border: none;
            border-radius: 5px;
            cursor: pointer;
            margin-right: 5px;
        }

        .role-dropdown {
            padding: 6px;
            border: 1px solid #ccc;
            border-radius: 5px;
        }

        .message {
            display: block;
            margin-top: 20px;
        }

        @media (max-width: 800px) {

            .user-table {
                display: block;
                overflow-x: auto;
                white-space: nowrap;
            }

        }

    </style>

</head>

<body>

<form id="form1" runat="server">

    <div class="container">

        <div class="card">

            <div class="page-header">

                <h1>Manage Users</h1>

                <asp:Button
                    ID="btnBack"
                    runat="server"
                    Text="Back to Admin Dashboard"
                    CssClass="btn btn-secondary"
                    OnClick="btnBack_Click" />

            </div>


            <p>
                Manage registered user accounts and roles.
            </p>


            <asp:GridView
                ID="gvUsers"
                runat="server"
                AutoGenerateColumns="False"
                DataKeyNames="UserId"
                CssClass="user-table"
                GridLines="None"
                OnRowCommand="gvUsers_RowCommand">

                <Columns>

                    <asp:BoundField
                        DataField="UserId"
                        HeaderText="User ID"
                        ReadOnly="True" />

                    <asp:BoundField
                        DataField="Name"
                        HeaderText="Name"
                        ReadOnly="True" />

                    <asp:BoundField
                        DataField="Email"
                        HeaderText="Email"
                        ReadOnly="True" />

                    <asp:BoundField
                        DataField="Role"
                        HeaderText="Current Role"
                        ReadOnly="True" />

                    <asp:TemplateField
                        HeaderText="Change Role">

                        <ItemTemplate>

                            <asp:DropDownList
                                ID="ddlRole"
                                runat="server"
                                CssClass="role-dropdown">

                                <asp:ListItem
                                    Text="User"
                                    Value="User">
                                </asp:ListItem>

                                <asp:ListItem
                                    Text="Admin"
                                    Value="Admin">
                                </asp:ListItem>

                            </asp:DropDownList>

                        </ItemTemplate>

                    </asp:TemplateField>


                    <asp:TemplateField
                        HeaderText="Actions">

                        <ItemTemplate>

                            <asp:Button
                                ID="btnChangeRole"
                                runat="server"
                                Text="Change Role"
                                CommandName="ChangeRole"
                                CommandArgument='<%# Eval("UserId") %>'
                                CssClass="action-button btn-primary" />

                            <asp:Button
                                ID="btnDeleteUser"
                                runat="server"
                                Text="Delete"
                                CommandName="DeleteUser"
                                CommandArgument='<%# Eval("UserId") %>'
                                CssClass="action-button btn-danger"
                                OnClientClick="return confirm('Are you sure you want to delete this user account?');" />

                        </ItemTemplate>

                    </asp:TemplateField>

                </Columns>

            </asp:GridView>


            <asp:Label
                ID="lblMessage"
                runat="server"
                CssClass="message">
            </asp:Label>

        </div>

    </div>

</form>

</body>
</html>