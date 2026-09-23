using System;
using System.Data.SqlClient;
using System.Web.UI;

namespace Password_Manager
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnStart_Click(object sender, EventArgs e)
        {
            string connectionString =
                System.Configuration.ConfigurationManager
                .ConnectionStrings["PasswordManagerConnection"]
                .ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                Response.Write("Database Connected Successfully!");
            }
        }
    }
}