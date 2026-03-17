using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Script.Serialization;
using MiniStudentManagementSystem.Models;

namespace MiniStudentManagementSystem
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void LoginButton_Click(object sender, EventArgs e)
        {
            try
            {
                string username = UsernameTextBox.Text.Trim();
                string password = PasswordTextBox.Text.Trim();
                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                {
                    ShowErrorMessage("Lütfən istifadəçi adı və şifrəni daxil edin");
                    return;
                }
                string appDataPath = Server.MapPath("~/App_Data");
                string filePath = Path.Combine(appDataPath, "users.json");
                List<User> usersList = ReadUsersFromFile(filePath);
                User user = usersList.FirstOrDefault(u => u.Username == username && u.Password == password);
                if (user == null)
                {
                    ShowErrorMessage("Yanlış istifadəçi adı və ya şifrə");
                    return;
                }
                Session["username"] = username;
                System.Web.HttpCookie cookie = new System.Web.HttpCookie("username");
                cookie.Value = username;
                cookie.Expires = DateTime.Now.AddMinutes(30);
                Response.Cookies.Add(cookie);
                Response.Redirect("Students.aspx");
            }
            catch (Exception ex)
            {
                ShowErrorMessage("Xəta baş verdi: " + ex.Message);
            }
        }

        private List<User> ReadUsersFromFile(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    return new List<User>();
                }
                string json = File.ReadAllText(filePath);
                if (string.IsNullOrWhiteSpace(json) || json.Trim() == "[]")
                {
                    return new List<User>();
                }
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                List<User> users = serializer.Deserialize<List<User>>(json);
                return users ?? new List<User>();
            }
            catch
            {
                return new List<User>();
            }
        }

        private void ShowErrorMessage(string message)
        {
            MessageLabel.Text = message;
            MessageLabel.CssClass = "message error";
            MessageLabel.Visible = true;
        }
    }
}
