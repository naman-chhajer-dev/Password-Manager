using System;
using System.Data.SqlClient;
using System.Configuration;
using System.Security.Cryptography;

namespace Password_Manager
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            string name = txtName.Text.Trim();
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text;
            string confirmPassword = txtConfirmPassword.Text;

            // Check empty fields
            if (name == "" || email == "" || password == "")
            {
                lblMessage.Text = "Please fill all fields.";
                return;
            }

            // Check password confirmation
            if (password != confirmPassword)
            {
                lblMessage.Text = "Passwords do not match.";
                return;
            }

            string connectionString =
                ConfigurationManager
                .ConnectionStrings["PasswordManagerConnection"]
                .ConnectionString;

            using (SqlConnection connection =
                   new SqlConnection(connectionString))
            {
                connection.Open();

                // Check if email already exists
                string checkQuery =
                    "SELECT COUNT(*) FROM Users WHERE Email = @Email";

                using (SqlCommand checkCommand =
                       new SqlCommand(checkQuery, connection))
                {
                    checkCommand.Parameters.AddWithValue("@Email", email);

                    int count = (int)checkCommand.ExecuteScalar();

                    if (count > 0)
                    {
                        lblMessage.Text = "Email already registered.";
                        return;
                    }
                }

                // Hash password
                string passwordHash = HashPassword(password);

                // Insert user
                string insertQuery =
                    @"INSERT INTO Users
                      (Name, Email, PasswordHash)
                      VALUES
                      (@Name, @Email, @PasswordHash)";

                using (SqlCommand command =
                       new SqlCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@PasswordHash", passwordHash);

                    command.ExecuteNonQuery();
                }
            }

            lblMessage.Text = "Registration successful!";
        }


        private string HashPassword(string password)
        {
            byte[] salt = new byte[16];

            using (var random = RandomNumberGenerator.Create())
            {
                random.GetBytes(salt);
            }

            using (var pbkdf2 =
                   new Rfc2898DeriveBytes(password, salt, 100000))
            {
                byte[] hash = pbkdf2.GetBytes(32);

                return Convert.ToBase64String(salt)
                    + ":"
                    + Convert.ToBase64String(hash);
            }
        }
    }
}