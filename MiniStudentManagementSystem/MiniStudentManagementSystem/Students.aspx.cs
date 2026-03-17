using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls;
using MiniStudentManagementSystem.Models;

namespace MiniStudentManagementSystem
{
    public partial class Students : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["username"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            if (!IsPostBack)
            {
                UsernameLabel.Text = Session["username"].ToString();

                LoadStudents();
            }
        }

        private void LoadStudents()
        {
            try
            {
                string appDataPath = Server.MapPath("~/App_Data");
                if (!Directory.Exists(appDataPath)) Directory.CreateDirectory(appDataPath);
                string filePath = Path.Combine(appDataPath, "students.json");
                List<Student> studentsList = ReadStudentsFromFile(filePath);
                StudentsGridView.DataSource = studentsList;
                StudentsGridView.DataBind();
            }
            catch (Exception ex)
            {
                ShowErrorMessage("Tələbələri yükləmə xətası: " + ex.Message);
            }
        }

        protected void AddButton_Click(object sender, EventArgs e)
        {
            try
            {
                string name = NameTextBox.Text.Trim();
                string surname = SurnameTextBox.Text.Trim();
                string studentNumber = StudentNumberTextBox.Text.Trim();
                if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(surname) || string.IsNullOrEmpty(studentNumber))
                {
                    ShowErrorMessage("Lütfən bütün sahələri doldurun");
                    return;
                }
                string appDataPath = Server.MapPath("~/App_Data");
                if (!Directory.Exists(appDataPath)) Directory.CreateDirectory(appDataPath);
                string filePath = Path.Combine(appDataPath, "students.json");
                List<Student> studentsList = ReadStudentsFromFile(filePath);
                Student existingStudent = studentsList.FirstOrDefault(s => s.StudentNumber == studentNumber);
                if (existingStudent != null)
                {
                    ShowErrorMessage("Bu Tələbə Nömrəsi artıq mövcuddur");
                    return;
                }
                Student newStudent = new Student { Name = name, Surname = surname, StudentNumber = studentNumber };
                studentsList.Add(newStudent);
                SaveStudentsToFile(filePath, studentsList);
                NameTextBox.Text = "";
                SurnameTextBox.Text = "";
                StudentNumberTextBox.Text = "";
                ShowSuccessMessage("Tələbə uğurla əlavə olundu");
                LoadStudents();
            }
            catch (Exception ex)
            {
                ShowErrorMessage("Tələbə əlavə etmə xətası: " + ex.Message);
            }
        }

        protected void StudentsGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            StudentsGridView.EditIndex = e.NewEditIndex;
            LoadStudents();
        }

        protected void StudentsGridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                int rowIndex = e.RowIndex;
                string name = e.NewValues["Name"].ToString();
                string surname = e.NewValues["Surname"].ToString();
                string studentNumber = e.NewValues["StudentNumber"].ToString();
                if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(surname) || string.IsNullOrEmpty(studentNumber))
                {
                    ShowErrorMessage("Lütfən bütün sahələri doldurun");
                    return;
                }
                string appDataPath = Server.MapPath("~/App_Data");
                string filePath = Path.Combine(appDataPath, "students.json");
                List<Student> studentsList = ReadStudentsFromFile(filePath);
                string originalStudentNumber = studentsList[rowIndex].StudentNumber;
                Student duplicateStudent = studentsList.FirstOrDefault(s => s.StudentNumber == studentNumber && s.StudentNumber != originalStudentNumber);
                if (duplicateStudent != null)
                {
                    ShowErrorMessage("Bu Tələbə Nömrəsi artıq mövcuddur");
                    StudentsGridView.EditIndex = -1;
                    LoadStudents();
                    return;
                }
                studentsList[rowIndex].Name = name;
                studentsList[rowIndex].Surname = surname;
                studentsList[rowIndex].StudentNumber = studentNumber;
                SaveStudentsToFile(filePath, studentsList);
                StudentsGridView.EditIndex = -1;
                ShowSuccessMessage("Tələbə uğurla yenilədi");
                LoadStudents();
            }
            catch (Exception ex)
            {
                ShowErrorMessage("Tələbə yeniləmə xətası: " + ex.Message);
            }
        }

        protected void StudentsGridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            StudentsGridView.EditIndex = -1;
            LoadStudents();
        }

        protected void StudentsGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                string appDataPath = Server.MapPath("~/App_Data");
                string filePath = Path.Combine(appDataPath, "students.json");
                List<Student> studentsList = ReadStudentsFromFile(filePath);
                studentsList.RemoveAt(e.RowIndex);
                SaveStudentsToFile(filePath, studentsList);
                ShowSuccessMessage("Tələbə uğurla silindi");
                LoadStudents();
            }
            catch (Exception ex)
            {
                ShowErrorMessage("Tələbə silmə xətası: " + ex.Message);
            }
        }

        private List<Student> ReadStudentsFromFile(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    List<Student> sampleStudents = new List<Student>
                    {
                        new Student { Name = "Ahmet", Surname = "Yilmaz", StudentNumber = "2023001" },
                        new Student { Name = "Fatima", Surname = "Demir", StudentNumber = "2023002" },
                        new Student { Name = "Emre", Surname = "Kaya", StudentNumber = "2023003" },
                        new Student { Name = "Ayse", Surname = "Ozturk", StudentNumber = "2023004" },
                        new Student { Name = "Mehmet", Surname = "Arslan", StudentNumber = "2023005" }
                    };
                    SaveStudentsToFile(filePath, sampleStudents);
                    return sampleStudents;
                }
                string json = File.ReadAllText(filePath);
                if (string.IsNullOrWhiteSpace(json) || json.Trim() == "[]") return new List<Student>();
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                List<Student> students = serializer.Deserialize<List<Student>>(json);
                return students ?? new List<Student>();
            }
            catch
            {
                return new List<Student>();
            }
        }

        private void SaveStudentsToFile(string filePath, List<Student> studentsList)
        {
            try
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                string json = serializer.Serialize(studentsList);
                string directory = Path.GetDirectoryName(filePath);
                if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);
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
                throw new Exception("Tələbələri saxlamaqda xəta: Fayla girişə icazə yoxdur. Server icazələrini yoxlayın. (" + uaEx.Message + ")");
            }
            catch (Exception ex)
            {
                throw new Exception("Tələbələri saxlamaqda xəta: " + ex.Message);
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

        protected void StudentsGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var linkButtons = e.Row.Cells[0].Controls.OfType<LinkButton>();
                foreach (var lb in linkButtons)
                {
                    if (lb.CommandName == "Delete")
                    {
                        lb.OnClientClick = "return confirm('Silmək istədiyinizdən əminsinizmi?');";
                    }
                }
            }
        }
    }
}
