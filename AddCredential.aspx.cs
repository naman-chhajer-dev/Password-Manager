using System;
using System.Data.SqlClient;
using System.Configuration;
using System.Security.Cryptography;

namespace Password_Manager
{
    public partial class AddCredential : System.Web.UI.Page
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
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string websiteName = txtWebsiteName.Text.Trim();
            string websiteURL = txtWebsiteURL.Text.Trim();
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;
            string encryptedPassword =
    EncryptionHelper.Encrypt(password);
            string notes = txtNotes.Text.Trim();

            // Check required fields
            if (websiteName == "" ||
                username == "" ||
                password == "")
            {
                lblMessage.Text =
                    "Please fill all required fields.";

                return;
            }

            int userId = Convert.ToInt32(Session["UserId"]);

            string connectionString =
                ConfigurationManager
                .ConnectionStrings["PasswordManagerConnection"]
                .ConnectionString;

            using (SqlConnection connection =
                   new SqlConnection(connectionString))
            {
                connection.Open();

                string query = @"
                    INSERT INTO Credentials
                    (
                        UserId,
                        WebsiteName,
                        WebsiteURL,
                        Username,
                        PasswordValue,
                        Notes
                    )
                    VALUES
                    (
                        @UserId,
                        @WebsiteName,
                        @WebsiteURL,
                        @Username,
                        @PasswordValue,
                        @Notes
                    )";

                using (SqlCommand command =
                       new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue(
                        "@UserId", userId);

                    command.Parameters.AddWithValue(
                        "@WebsiteName", websiteName);

                    command.Parameters.AddWithValue(
                        "@WebsiteURL", websiteURL);

                    command.Parameters.AddWithValue(
                        "@Username", username);

                    command.Parameters.AddWithValue(
    "@PasswordValue", encryptedPassword);

                    command.Parameters.AddWithValue(
                        "@Notes", notes);

                    command.ExecuteNonQuery();
                }
            }

            lblMessage.Text =
                "Credential saved successfully!";

            // Clear fields
            txtWebsiteName.Text = "";
            txtWebsiteURL.Text = "";
            txtUsername.Text = "";
            txtPassword.Text = "";
            txtNotes.Text = "";
        }
        protected void btnGeneratePassword_Click(
    object sender,
    EventArgs e)
        {
            txtPassword.Text = GeneratePassword(16);

            lblGeneratedPassword.Text =
                "Strong password generated.";
        }

        private string GeneratePassword(int length)
        {
            const string uppercase =
                "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            const string lowercase =
                "abcdefghijklmnopqrstuvwxyz";

            const string numbers =
                "0123456789";

            const string special =
                "!@#$%^&*";

            string allCharacters =
                uppercase +
                lowercase +
                numbers +
                special;

            char[] password =
                new char[length];

            using (RandomNumberGenerator random =
                   RandomNumberGenerator.Create())
            {
                byte[] randomBytes =
                    new byte[length];

                random.GetBytes(randomBytes);

                for (int i = 0; i < length; i++)
                {
                    password[i] =
                        allCharacters[
                            randomBytes[i] %
                            allCharacters.Length];
                }
            }

            return new string(password);
        }

        protected void btnShowPassword_Click(
    object sender,
    EventArgs e)
        {
            if (txtPassword.TextMode == System.Web.UI.WebControls.TextBoxMode.Password)
            {
                txtPassword.TextMode =
                    System.Web.UI.WebControls.TextBoxMode.SingleLine;

                btnShowPassword.Text = "Hide";
            }
            else
            {
                txtPassword.TextMode =
                    System.Web.UI.WebControls.TextBoxMode.Password;

                btnShowPassword.Text = "Show";
            }
        }

        protected void btnBack_Click(
    object sender,
    EventArgs e)
        {
            Response.Redirect("Dashboard.aspx");
        }
    }
}