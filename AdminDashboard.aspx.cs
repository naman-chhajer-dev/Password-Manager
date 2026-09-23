using System;

namespace Password_Manager
{
    public partial class AdminDashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Check whether the user is logged in
            if (Session["UserId"] == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

            // Check whether the logged-in user is Admin
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
                lblAdminName.Text =
                    Session["UserName"].ToString();
            }
        }


        protected void btnManageUsers_Click(
            object sender,
            EventArgs e)
        {
            Response.Redirect("ManageUsers.aspx");
        }


        protected void btnLogout_Click(
            object sender,
            EventArgs e)
        {
            Session.Clear();
            Session.Abandon();

            Response.Redirect("Login.aspx");
        }
    }
}