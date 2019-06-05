using System;
using System.Windows.Forms;
using WinFormStudents.Model;
using WinFormStudents.Services;

namespace SzkolaPostgreSQLWinForms
{
    public partial class FormAdd : Form
    {
        private FormStudents form;
        private readonly SemesterService semesterService;
        private readonly StudentService studentsService;
        public FormAdd()
        {
            InitializeComponent();
            this.semesterService = new SemesterService();
            this.studentsService = new StudentService();
            this.LoadToComboBox();
        }

        private async void LoadToComboBox()
        {
            try
            {
                this.comboBox1.DataSource = await this.semesterService.GetSemesters();
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
            try
            {
                if (this.textBox1.Text.Length < 3 || this.textBox2.Text.Length < 3)
                {
                    MessageBox.Show("Imię lub nazwisko mają za mało znaków");
                }
                else
                {
                    var classObj = (Semester)this.comboBox1.SelectedItem;
                    var professor = new Student(0, this.textBox3.Text, this.textBox1.Text, this.textBox2.Text,
                       classObj);
                    await this.studentsService.AddStudent(professor);
                    MessageBox.Show("Zapisano studenta");
                    this.form.RefreshWindow();
                    this.Dispose();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
