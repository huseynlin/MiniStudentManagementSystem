using System;

namespace MiniStudentManagementSystem
{
    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session.Abandon();
            if (Request.Cookies["username"] != null)
            {
                System.Web.HttpCookie cookie = Request.Cookies["username"];
                cookie.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(cookie);
            }
            Response.Redirect("Login.aspx");
        }
    }
}
