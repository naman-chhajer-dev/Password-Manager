<%@ Page Language="C#" AutoEventWireup="true"
    CodeBehind="AdminDashboard.aspx.cs"
    Inherits="Password_Manager.AdminDashboard" %>

<!DOCTYPE html>

<html>
<head runat="server">

    <title>Admin Dashboard - Password Manager</title>

    <link href="Style.css"
        rel="stylesheet"
        type="text/css" />

    <style>

        .admin-header {
            display: flex;
            justify-content: space-between;
            align-items: center;
            margin-bottom: 30px;
        }

        .admin-badge {
            display: inline-block;
            background: #dc2626;
            color: white;
            padding: 6px 12px;
            border-radius: 20px;
            font-size: 13px;
            font-weight: bold;
        }

        .admin-actions {
            display: flex;
            gap: 15px;
            flex-wrap: wrap;
            margin-top: 30px;
        }

    </style>

</head>

<body>

<form id="form1" runat="server">

    <div class="container">

        <div class="card">

            <div class="admin-header">

                <div>

                    <h1>Admin Dashboard</h1>

                    <span class="admin-badge">
                        ADMIN
                    </span>

                </div>

            </div>


            <h2>
                Welcome,
                <asp:Label
                    ID="lblAdminName"
                    runat="server">
                </asp:Label>
            </h2>


            <p>
                Manage the Password Manager system
                and user accounts.
            </p>


            <div class="admin-actions">

                <asp:Button
                    ID="btnManageUsers"
                    runat="server"
                    Text="Manage Users"
                    CssClass="btn btn-primary"
                    OnClick="btnManageUsers_Click" />

                <asp:Button
                    ID="btnLogout"
                    runat="server"
                    Text="Logout"
                    CssClass="btn btn-danger"
                    OnClick="btnLogout_Click" />

            </div>

        </div>

    </div>

</form>

</body>
</html>