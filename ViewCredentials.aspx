<%@ Page Language="C#" AutoEventWireup="true"
    CodeBehind="ViewCredentials.aspx.cs"
    Inherits="Password_Manager.ViewCredentials" %>

<!DOCTYPE html>

<html>
<head runat="server">

    <title>My Credentials - Password Manager</title>

    <link href="Style.css"
        rel="stylesheet"
        type="text/css" />
    <script type="text/javascript">

    function confirmDelete() {
        return confirm(
            "Are you sure you want to delete this credential?"
        );
    }

    </script>

    <style>

        .page-header {
            display: flex;
            justify-content: space-between;
            align-items: center;
            margin-bottom: 25px;
        }

        .search-area {
            display: flex;
            gap: 8px;
            margin-bottom: 25px;
        }

        .search-box {
            flex: 1;
        }

        .credential-table {
            width: 100%;
            border-collapse: collapse;
            margin-top: 10px;
        }

        .credential-table th {
            background: #2563eb;
            color: white;
            padding: 12px;
            text-align: left;
        }

        .credential-table td {
            padding: 12px;
            border-bottom: 1px solid #ddd;
            vertical-align: middle;
        }

        .credential-table tr:hover {
            background: #f8fafc;
        }

        .password-button {
            margin-left: 8px;
            padding: 6px 10px;
            border: none;
            border-radius: 5px;
            cursor: pointer;
        }

        .edit-delete {
            white-space: nowrap;
        }

        .empty-message {
            display: block;
            margin-top: 20px;
            color: #666;
        }

        @media (max-width: 900px) {

            .credential-table {
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

                <h1>My Saved Credentials</h1>

                <asp:Button
                    ID="btnBack"
                    runat="server"
                    Text="Back to Dashboard"
                    CssClass="btn btn-secondary"
                    OnClick="btnBack_Click" />

            </div>


            <div class="search-area">

                <asp:TextBox
                    ID="txtSearch"
                    runat="server"
                    CssClass="form-control search-box"
                    placeholder="Search website, URL or username">
                </asp:TextBox>

                <asp:Button
                    ID="btnSearch"
                    runat="server"
                    Text="Search"
                    CssClass="btn btn-primary"
                    OnClick="btnSearch_Click" />

                <asp:Button
                    ID="btnClearSearch"
                    runat="server"
                    Text="Clear"
                    CssClass="btn btn-secondary"
                    OnClick="btnClearSearch_Click" />

            </div>


            <asp:GridView
    ID="gvCredentials"
    runat="server"
    AutoGenerateColumns="False"
    DataKeyNames="CredentialId"
    CssClass="credential-table"
    GridLines="None"
    OnRowEditing="gvCredentials_RowEditing"
    OnRowCancelingEdit="gvCredentials_RowCancelingEdit"
    OnRowUpdating="gvCredentials_RowUpdating"
    OnRowDeleting="gvCredentials_RowDeleting"
    OnRowDataBound="gvCredentials_RowDataBound">

                <Columns>

                    <asp:BoundField
                        DataField="CredentialId"
                        HeaderText="ID"
                        ReadOnly="True" />

                    <asp:BoundField
                        DataField="WebsiteName"
                        HeaderText="Website" />

                    <asp:BoundField
                        DataField="WebsiteURL"
                        HeaderText="URL" />

                    <asp:BoundField
                        DataField="Username"
                        HeaderText="Username" />

                    <asp:TemplateField HeaderText="Password">

                        <ItemTemplate>

                            <asp:Label
                                ID="lblPassword"
                                runat="server"
                                Text="••••••••">
                            </asp:Label>

                            <asp:Button
                                ID="btnShowPassword"
                                runat="server"
                                Text="Show"
                                CssClass="password-button"
                                CommandName="ShowPassword"
                                CommandArgument='<%# Eval("CredentialId") %>'
                                OnCommand="btnShowPassword_Command" />

                        </ItemTemplate>

                        <EditItemTemplate>

                            <asp:TextBox
                                ID="txtEditPassword"
                                runat="server"
                                Text='<%# Eval("PasswordValue") %>'
                                CssClass="form-control">
                            </asp:TextBox>

                        </EditItemTemplate>

                    </asp:TemplateField>

                    <asp:BoundField
                        DataField="Notes"
                        HeaderText="Notes" />

                   <asp:CommandField
    ShowEditButton="True"
    ShowDeleteButton="True"
    DeleteText="Delete"
    HeaderText="Actions" />

                </Columns>

            </asp:GridView>


            <asp:Label
                ID="lblMessage"
                runat="server"
                CssClass="empty-message">
            </asp:Label>

        </div>

    </div>

</form>

</body>
</html>