using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Script.Serialization;
using MiniStudentManagementSystem.Models;

namespace MiniStudentManagementSystem
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void RegisterButton_Click(object sender, EventArgs e)
        {
            try
            {
                string username = UsernameTextBox.Text.Trim();
                string email = EmailTextBox.Text.Trim();
                string password = PasswordTextBox.Text.Trim();

                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                {
                    ShowErrorMessage("Lütfən bütün sahələri doldurun");
                    return;
                }

                string appDataPath = Server.MapPath("~/App_Data");
                if (!Directory.Exists(appDataPath))
                {
                    try
                    {
                        Directory.CreateDirectory(appDataPath);
                    }
                    catch (Exception ex)
                    {
                        ShowErrorMessage("Qovluq yaratma xətası: " + ex.Message);
                        return;
                    }
                }

                string filePath = Path.Combine(appDataPath, "users.json");

                List<User> usersList = ReadUsersFromFile(filePath);

                User existingUser = usersList.FirstOrDefault(u => u.Username == username);

                if (existingUser != null)
                {
                    ShowErrorMessage("Bu istifadəçi adı artıq mövcuddur. Başqa bir ad seçin.");
                    return;
                }

                User newUser = new User
                {
                    Username = username,
                    Email = email,
                    Password = password
                };

                usersList.Add(newUser);

                SaveUsersToFile(filePath, usersList);

                UsernameTextBox.Text = "";
                EmailTextBox.Text = "";
                PasswordTextBox.Text = "";

                ShowSuccessMessage("Qeydiyyat uğurlu oldu! İndi <a href='Login.aspx'>daxil ola bilərsiniz</a>.");
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
                    try
                    {
                        File.WriteAllText(filePath, "[]");
                    }
                    catch
                    {
                        return new List<User>();
                    }
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

        private void SaveUsersToFile(string filePath, List<User> usersList)
        {
            try
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                string json = serializer.Serialize(usersList);

                string directory = Path.GetDirectoryName(filePath);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                if (File.Exists(filePath))
                {
                    try
                    {
                        FileAttributes attrs = File.GetAttributes(filePath);
                        if ((attrs & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
                        {
                            attrs = attrs & ~FileAttributes.ReadOnly;
                            File.SetAttributes(filePath, attrs);
                        }
                    }
                    catch
                    {
                    }
                }

                byte[] bytes = System.Text.Encoding.UTF8.GetBytes(json);
                using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    fs.Write(bytes, 0, bytes.Length);
                    fs.Flush();
                }
            }
            catch (UnauthorizedAccessException uaEx)
            {
                throw new Exception("İstifadəçiləri saxlamaqda xəta: Fayla girişə icazə yoxdur. Server icazələrini yoxlayın. (" + uaEx.Message + ")");
            }
            catch (Exception ex)
            {
                throw new Exception("İstifadəçiləri saxlamaqda xəta: " + ex.Message);
            }
        }

        private void ShowSuccessMessage(string message)
        {
            MessageLabel.Text = message;
            MessageLabel.CssClass = "message success";
            MessageLabel.Visible = true;
        }

        private void ShowErrorMessage(string message)
        {
            MessageLabel.Text = message;
            MessageLabel.CssClass = "message error";
            MessageLabel.Visible = true;
        }
    }
}
