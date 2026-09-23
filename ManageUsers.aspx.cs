using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Password_Manager
{
    public partial class ManageUsers : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

            if (Session["Role"] == null ||
                !Session["Role"]
                    .ToString()
                    .Equals(
                        "Admin",
                        StringComparison.OrdinalIgnoreCase))
            {
                Response.Redirect("Dashboard.aspx");
                return;
            }

            if (!IsPostBack)
            {
                LoadUsers();
            }
        }


        private void LoadUsers()
        {
            string connectionString =
                ConfigurationManager
                .ConnectionStrings[
                    "PasswordManagerConnection"]
                .ConnectionString;

            DataTable dataTable =
                new DataTable();

            using (SqlConnection connection =
                   new SqlConnection(connectionString))
            {
                connection.Open();

                string query = @"
                    SELECT
                        UserId,
                        Name,
                        Email,
                        Role
                    FROM Users
                    ORDER BY UserId";

                using (SqlCommand command =
                       new SqlCommand(
                           query,
                           connection))
                {
                    using (SqlDataAdapter adapter =
                           new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }

            gvUsers.DataSource = dataTable;
            gvUsers.DataBind();

            // Set current role in each dropdown
            for (int i = 0;
                 i < dataTable.Rows.Count;
                 i++)
            {
                DropDownList ddlRole =
                    (DropDownList)gvUsers.Rows[i]
                    .FindControl("ddlRole");

                if (ddlRole != null)
                {
                    ddlRole.SelectedValue =
                        dataTable.Rows[i]["Role"]
                        .ToString();
                }
            }

            if (dataTable.Rows.Count == 0)
            {
                lblMessage.Text =
                    "No users found.";
            }
            else
            {
                lblMessage.Text =
                    "Total users: "
                    + dataTable.Rows.Count;
            }
        }


        protected void gvUsers_RowCommand(
            object sender,
            GridViewCommandEventArgs e)
        {
            if (e.CommandName != "ChangeRole" &&
                e.CommandName != "DeleteUser")
            {
                return;
            }

            int userId;

            if (!int.TryParse(
                    e.CommandArgument.ToString(),
                    out userId))
            {
                lblMessage.Text =
                    "Invalid user ID.";

                return;
            }

            int currentAdminId =
                Convert.ToInt32(
                    Session["UserId"]);

            // Prevent admin from deleting their own account
            if (userId == currentAdminId)
            {
                if (e.CommandName == "DeleteUser")
                {
                    lblMessage.Text =
                        "You cannot delete your own admin account.";
                    return;
                }

                if (e.CommandName == "ChangeRole")
                {
                    lblMessage.Text =
                        "You cannot change your own admin role.";
                    return;
                }
            }

            if (e.CommandName == "ChangeRole")
            {
                ChangeUserRole(userId, e);
            }

            if (e.CommandName == "DeleteUser")
            {
                DeleteUser(userId);
            }
        }


        private void ChangeUserRole(
            int userId,
            GridViewCommandEventArgs e)
        {
            GridViewRow row =
                (GridViewRow)
                ((Control)e.CommandSource)
                .NamingContainer;

            DropDownList ddlRole =
                (DropDownList)
                row.FindControl("ddlRole");

            if (ddlRole == null)
            {
                lblMessage.Text =
                    "Unable to read selected role.";

                return;
            }

            string newRole =
                ddlRole.SelectedValue;

            if (newRole == "User")
            {
                string connectionString =
                    ConfigurationManager
                    .ConnectionStrings[
                        "PasswordManagerConnection"]
                    .ConnectionString;

                using (SqlConnection connection =
                       new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = @"
            SELECT Role
            FROM Users
            WHERE UserId = @UserId";

                    using (SqlCommand command =
                           new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue(
                            "@UserId",
                            userId);

                        object result = command.ExecuteScalar();

                        if (result != null &&
                            result.ToString()
                                .Equals("Admin",
                                    StringComparison.OrdinalIgnoreCase))
                        {
                            if (GetAdminCount() <= 1)
                            {
                                lblMessage.Text =
                                    "You cannot remove the last Admin account.";

                                return;
                            }
                        }
                    }
                }
            }

            if (newRole != "User" &&
                newRole != "Admin")
            {
                lblMessage.Text =
                    "Invalid role.";

                return;
            }

            string connectionString =
                ConfigurationManager
                .ConnectionStrings[
                    "PasswordManagerConnection"]
                .ConnectionString;

            using (SqlConnection connection =
                   new SqlConnection(connectionString))
            {
                connection.Open();

                string query = @"
                    UPDATE Users
                    SET Role = @Role
                    WHERE UserId = @UserId";

                using (SqlCommand command =
                       new SqlCommand(
                           query,
                           connection))
                {
                    command.Parameters.AddWithValue(
                        "@Role",
                        newRole);

                    command.Parameters.AddWithValue(
                        "@UserId",
                        userId);

                    command.ExecuteNonQuery();
                }
            }

            lblMessage.Text =
                "User role updated successfully.";

            LoadUsers();
        }

        private int GetAdminCount()
        {
            string connectionString =
                ConfigurationManager
                .ConnectionStrings[
                    "PasswordManagerConnection"]
                .ConnectionString;

            using (SqlConnection connection =
                   new SqlConnection(connectionString))
            {
                connection.Open();

                string query = @"
            SELECT COUNT(*)
            FROM Users
            WHERE Role = 'Admin'";

                using (SqlCommand command =
                       new SqlCommand(query, connection))
                {
                    return Convert.ToInt32(
                        command.ExecuteScalar());
                }
            }
        }

        private void DeleteUser(int userId)
        {
            string connectionString =
                ConfigurationManager
                .ConnectionStrings[
                    "PasswordManagerConnection"]
                .ConnectionString;

            using (SqlConnection connection =
                   new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlTransaction transaction =
                       connection.BeginTransaction())
                {
                    try
                    {
                        string deleteCredentials = @"
                    DELETE FROM Credentials
                    WHERE UserId = @UserId";

                        using (SqlCommand command =
                               new SqlCommand(
                                   deleteCredentials,
                                   connection,
                                   transaction))
                        {
                            command.Parameters.AddWithValue(
                                "@UserId",
                                userId);

                            command.ExecuteNonQuery();
                        }

                        string deleteUser = @"
                    DELETE FROM Users
                    WHERE UserId = @UserId";

                        using (SqlCommand command =
                               new SqlCommand(
                                   deleteUser,
                                   connection,
                                   transaction))
                        {
                            command.Parameters.AddWithValue(
                                "@UserId",
                                userId);

                            command.ExecuteNonQuery();
                        }

                        transaction.Commit();

                        lblMessage.Text =
                            "User account deleted successfully.";
                    }
                    catch
                    {
                        transaction.Rollback();

                        lblMessage.Text =
                            "Unable to delete the user account.";
                    }
                }
            }

            LoadUsers();
        }


        protected void btnBack_Click(
            object sender,
            EventArgs e)
        {
            Response.Redirect(
                "AdminDashboard.aspx");
        }
    }
}