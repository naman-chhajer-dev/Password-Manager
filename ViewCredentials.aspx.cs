using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Password_Manager
{
    public partial class ViewCredentials : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

            string role = Session["Role"]?.ToString();

            if (string.Equals(role, "Admin",
                StringComparison.OrdinalIgnoreCase))
            {
                Response.Redirect("AdminDashboard.aspx");
                return;
            }

            if (!string.Equals(role, "User",
                StringComparison.OrdinalIgnoreCase))
            {
                Session.Clear();
                Session.Abandon();

                Response.Redirect("Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                LoadCredentials();
            }
        }


        // ==========================================
        // LOAD ALL CREDENTIALS
        // ==========================================

        private void LoadCredentials()
        {
            LoadCredentials("");
        }


        // ==========================================
        // LOAD CREDENTIALS WITH SEARCH
        // ==========================================

        private void LoadCredentials(string searchText)
        {
            int userId =
                Convert.ToInt32(Session["UserId"]);

            string connectionString =
                ConfigurationManager
                .ConnectionStrings["PasswordManagerConnection"]
                .ConnectionString;

            using (SqlConnection connection =
                   new SqlConnection(connectionString))
            {
                connection.Open();

                string query = @"
                    SELECT
                        CredentialId,
                        WebsiteName,
                        WebsiteURL,
                        Username,
                        PasswordValue,
                        Notes
                    FROM Credentials
                    WHERE UserId = @UserId
                    AND
                    (
                        WebsiteName LIKE @Search
                        OR WebsiteURL LIKE @Search
                        OR Username LIKE @Search
                    )
                    ORDER BY CredentialId DESC";

                using (SqlCommand command =
                       new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue(
                        "@UserId",
                        userId);

                    command.Parameters.AddWithValue(
                        "@Search",
                        "%" + searchText + "%");

                    using (SqlDataAdapter adapter =
                           new SqlDataAdapter(command))
                    {
                        DataTable table =
                            new DataTable();

                        adapter.Fill(table);

                        // Decrypt password before displaying
                        foreach (DataRow row in table.Rows)
                        {
                            string encryptedPassword =
                                row["PasswordValue"]
                                .ToString();

                            try
                            {
                                row["PasswordValue"] =
                                    EncryptionHelper.Decrypt(
                                        encryptedPassword);
                            }
                            catch
                            {
                                // Existing old plaintext
                                // passwords will not decrypt.
                                row["PasswordValue"] =
                                    "[Old/Unencrypted Password]";
                            }
                        }

                        gvCredentials.DataSource =
                            table;

                        gvCredentials.DataBind();
                    }
                }
            }
        }


        // ==========================================
        // SEARCH
        // ==========================================

        protected void btnSearch_Click(
            object sender,
            EventArgs e)
        {
            string searchText =
                txtSearch.Text.Trim();

            LoadCredentials(searchText);
        }


        // ==========================================
        // CLEAR SEARCH
        // ==========================================

        protected void btnClearSearch_Click(
            object sender,
            EventArgs e)
        {
            txtSearch.Text = "";

            LoadCredentials();
        }


        // ==========================================
        // EDIT
        // ==========================================

        protected void gvCredentials_RowEditing(
            object sender,
            GridViewEditEventArgs e)
        {
            gvCredentials.EditIndex =
                e.NewEditIndex;

            LoadCredentials();
        }


        // ==========================================
        // CANCEL EDIT
        // ==========================================

        protected void gvCredentials_RowCancelingEdit(
            object sender,
            GridViewCancelEditEventArgs e)
        {
            gvCredentials.EditIndex = -1;

            LoadCredentials();
        }


        // ==========================================
        // UPDATE
        // ==========================================

        protected void gvCredentials_RowUpdating(
            object sender,
            GridViewUpdateEventArgs e)
        {
            int credentialId =
                Convert.ToInt32(
                    gvCredentials.Rows[e.RowIndex]
                    .Cells[0].Text);

            GridViewRow row =
                gvCredentials.Rows[e.RowIndex];

            string websiteName =
                ((TextBox)row.Cells[1]
                .Controls[0]).Text;

            string websiteURL =
                ((TextBox)row.Cells[2]
                .Controls[0]).Text;

            string username =
                ((TextBox)row.Cells[3]
                .Controls[0]).Text;

            string password =
                ((TextBox)row.Cells[4]
                .Controls[0]).Text;

            string notes =
                ((TextBox)row.Cells[5]
                .Controls[0]).Text;

            // Encrypt the password before saving
            string encryptedPassword =
                EncryptionHelper.Encrypt(password);

            int userId =
                Convert.ToInt32(
                    Session["UserId"]);

            string connectionString =
                ConfigurationManager
                .ConnectionStrings["PasswordManagerConnection"]
                .ConnectionString;

            using (SqlConnection connection =
                   new SqlConnection(connectionString))
            {
                connection.Open();

                string query = @"
                    UPDATE Credentials
                    SET
                        WebsiteName = @WebsiteName,
                        WebsiteURL = @WebsiteURL,
                        Username = @Username,
                        PasswordValue = @PasswordValue,
                        Notes = @Notes
                    WHERE CredentialId = @CredentialId
                    AND UserId = @UserId";

                using (SqlCommand command =
                       new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue(
                        "@WebsiteName",
                        websiteName);

                    command.Parameters.AddWithValue(
                        "@WebsiteURL",
                        websiteURL);

                    command.Parameters.AddWithValue(
                        "@Username",
                        username);

                    command.Parameters.AddWithValue(
                        "@PasswordValue",
                        encryptedPassword);

                    command.Parameters.AddWithValue(
                        "@Notes",
                        notes);

                    command.Parameters.AddWithValue(
                        "@CredentialId",
                        credentialId);

                    command.Parameters.AddWithValue(
                        "@UserId",
                        userId);

                    command.ExecuteNonQuery();
                }
            }

            gvCredentials.EditIndex = -1;

            LoadCredentials();
        }


        // ==========================================
        // DELETE
        // ==========================================

        protected void gvCredentials_RowDeleting(
            object sender,
            GridViewDeleteEventArgs e)
        {
            int credentialId =
                Convert.ToInt32(
                    gvCredentials.Rows[e.RowIndex]
                    .Cells[0].Text);

            int userId =
                Convert.ToInt32(
                    Session["UserId"]);

            string connectionString =
                ConfigurationManager
                .ConnectionStrings["PasswordManagerConnection"]
                .ConnectionString;

            using (SqlConnection connection =
                   new SqlConnection(connectionString))
            {
                connection.Open();

                string query = @"
                    DELETE FROM Credentials
                    WHERE CredentialId = @CredentialId
                    AND UserId = @UserId";

                using (SqlCommand command =
                       new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue(
                        "@CredentialId",
                        credentialId);

                    command.Parameters.AddWithValue(
                        "@UserId",
                        userId);

                    command.ExecuteNonQuery();
                }
            }

            LoadCredentials();
        }


        // ==========================================
        // BACK TO DASHBOARD
        // ==========================================

        protected void btnBack_Click(
            object sender,
            EventArgs e)
        {
            Response.Redirect("Dashboard.aspx");
        }

        protected void btnShowPassword_Command(
    object sender,
    CommandEventArgs e)
        {
            int credentialId =
                Convert.ToInt32(e.CommandArgument);

            GridViewRow row =
                (GridViewRow)((Control)sender).NamingContainer;

            Label lblPassword =
                (Label)row.FindControl("lblPassword");

            Button btnShowPassword =
                (Button)row.FindControl("btnShowPassword");

            if (btnShowPassword.Text == "Show")
            {
                int userId =
                    Convert.ToInt32(Session["UserId"]);

                string connectionString =
                    ConfigurationManager
                    .ConnectionStrings["PasswordManagerConnection"]
                    .ConnectionString;

                string encryptedPassword = "";

                using (SqlConnection connection =
                       new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = @"
                SELECT PasswordValue
                FROM Credentials
                WHERE CredentialId = @CredentialId
                AND UserId = @UserId";

                    using (SqlCommand command =
                           new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue(
                            "@CredentialId",
                            credentialId);

                        command.Parameters.AddWithValue(
                            "@UserId",
                            userId);

                        object result =
                            command.ExecuteScalar();

                        if (result != null)
                        {
                            encryptedPassword =
                                result.ToString();
                        }
                    }
                }

                try
                {
                    string password =
                        EncryptionHelper.Decrypt(
                            encryptedPassword);

                    lblPassword.Text = password;
                    btnShowPassword.Text = "Hide";
                }
                catch
                {
                    lblPassword.Text =
                        "[Old/Unencrypted Password]";

                    btnShowPassword.Text = "Show";
                }
            }
            else
            {
                lblPassword.Text = "••••••••";
                btnShowPassword.Text = "Show";
            }
        }
        protected void gvCredentials_RowDataBound(
    object sender,
    GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                foreach (Control control in e.Row.Controls)
                {
                    if (control is DataControlFieldCell)
                    {
                        foreach (Control childControl
                                 in control.Controls)
                        {
                            if (childControl is LinkButton)
                            {
                                LinkButton button =
                                    (LinkButton)childControl;

                                if (button.CommandName == "Delete")
                                {
                                    button.OnClientClick =
                                        "return confirmDelete();";
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}