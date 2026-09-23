using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Security.Cryptography;

namespace Password_Manager
{
    public partial class Login : System.Web.UI.Page
    {
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(email) ||
                string.IsNullOrEmpty(password))
            {
                lblMessage.Text =
                    "Please enter email and password.";

                return;
            }

            string connectionString =
                ConfigurationManager
                .ConnectionStrings["PasswordManagerConnection"]
                .ConnectionString;

            int userId = 0;
            string name = "";
            string passwordHash = "";
            string role = "";

            using (SqlConnection connection =
                   new SqlConnection(connectionString))
            {
                connection.Open();

                string query = @"
                    SELECT
                        UserId,
                        Name,
                        PasswordHash,
                        Role
                    FROM Users
                    WHERE Email = @Email";

                using (SqlCommand command =
                       new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue(
                        "@Email",
                        email);

                    using (SqlDataReader reader =
                           command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            userId =
                                Convert.ToInt32(
                                    reader["UserId"]);

                            name =
                                reader["Name"].ToString();

                            passwordHash =
                                reader["PasswordHash"]
                                .ToString();

                            role =
                                reader["Role"].ToString();
                        }
                    }
                }
            }

            if (userId == 0)
            {
                lblMessage.Text =
                    "Invalid email or password.";

                return;
            }

            if (!VerifyPassword(password, passwordHash))
            {
                lblMessage.Text =
                    "Invalid email or password.";

                return;
            }

            // Store user information in Session
            Session["UserId"] = userId;
            Session["UserName"] = name;
            Session["Email"] = email;
            Session["Role"] = role;

            // Redirect according to role
            if (role.Equals(
                    "Admin",
                    StringComparison.OrdinalIgnoreCase))
            {
                Response.Redirect("AdminDashboard.aspx");
            }
            else
            {
                Response.Redirect("Dashboard.aspx");
            }
        }


        private bool VerifyPassword(
            string password,
            string storedHash)
        {
            try
            {
                string[] parts =
                    storedHash.Split(':');

                if (parts.Length != 2)
                    return false;

                byte[] salt =
                    Convert.FromBase64String(parts[0]);

                byte[] storedPasswordHash =
                    Convert.FromBase64String(parts[1]);

                using (var pbkdf2 =
                       new Rfc2898DeriveBytes(
                           password,
                           salt,
                           100000))
                {
                    byte[] calculatedHash =
                        pbkdf2.GetBytes(32);

                    return AreEqual(
                        calculatedHash,
                        storedPasswordHash);
                }
            }
            catch
            {
                return false;
            }
        }


        private bool AreEqual(
            byte[] first,
            byte[] second)
        {
            if (first == null ||
                second == null ||
                first.Length != second.Length)
            {
                return false;
            }

            int result = 0;

            for (int i = 0;
                 i < first.Length;
                 i++)
            {
                result |= first[i] ^ second[i];
            }

            return result == 0;
        }
    }
}