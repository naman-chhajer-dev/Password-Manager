using System;

namespace Password_Manager
{
    public partial class Dashboard : System.Web.UI.Page
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
                lblWelcome.Text =
                    "Welcome, " + Session["UserName"].ToString();
            }
        }


        protected void btnLogout_Click(
            object sender,
            EventArgs e)
        {
            Session.Clear();
            Session.Abandon();

            Response.Redirect("Login.aspx");
        }


        protected void btnAddCredential_Click(
            object sender,
            EventArgs e)
        {
            Response.Redirect(
                "AddCredential.aspx");
        }


        protected void btnViewCredentials_Click(
            object sender,
            EventArgs e)
        {
            Response.Redirect(
                "ViewCredentials.aspx");
        }
    }
}