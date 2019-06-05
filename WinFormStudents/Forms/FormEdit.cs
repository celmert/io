using System;
using System.Windows.Forms;
using WinFormStudents.Model;
using WinFormStudents.Services;

namespace SzkolaPostgreSQLWinForms
{
    public partial class FormEdit: Form
    {
        private FormStudents form;
        private readonly SemesterService semesterService;
        private readonly StudentService studentService;
        private readonly int studentId;
        public FormEdit(Student Student)
        {
            InitializeComponent();
            this.semesterService = new SemesterService();
            this.studentService = new StudentService();
            this.textBox1.Text = Student.FirstName;
            this.textBox2.Text = Student.LastName;
            this.textBox3.Text = Student.IndexNumber;
            this.LoadToComboBox(Student);
            this.studentId = Student.StudentId;
        }

        private async void LoadToComboBox(Student selectedClass)
        {
            try
            {
                this.comboBox1.DataSource = await this.semesterService.GetSemesters();
                this.comboBox1.SelectedIndex = selectedClass.Semester.SemesterId - 1;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void SetForm(FormStudents form)
        {
            this.form = form;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.form = null;
            this.Dispose();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (this.textBox1.Text.Length < 3 || this.textBox2.Text.Length < 3)
            {
                MessageBox.Show("Imię lub nazwisko mają za mało znaków");
            }
            else
            {
                var classObj = (Semester)this.comboBox1.SelectedItem;
                var Student = new Student(studentId, this.textBox3.Text, this.textBox1.Text, this.textBox2.Text,
                   classObj);
                await this.studentService.EditStudent(Student);
                MessageBox.Show("Zapisano studenta");
                this.form.RefreshWindow();
                this.Dispose();
            }
        }
    }
}
