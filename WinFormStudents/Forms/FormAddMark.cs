using System;
using System.Windows.Forms;
using WinFormStudents.Model;
using WinFormStudents.Services;

namespace SzkolaPostgreSQLWinForms
{
    public partial class FormAddMark : Form
    {
        private FormMarks form;
        private readonly MarkService markService;
        private readonly LessonService lessonService;
        private readonly StudentService studentService;
        private readonly ProfessorService professorsService;
        public FormAddMark()
        {
            InitializeComponent();
            this.markService = new MarkService();
            this.studentService = new StudentService();
            this.lessonService = new LessonService();
            this.professorsService = new ProfessorService();
          
            this.LoadToComboBox();
           
        }

        private async void LoadToComboBox()
        {
            try
            {
                this.comboBox1.DataSource = await this.lessonService.Get();
               

                this.comboBox2.DataSource = await this.studentService.GetStudents();
               

                this.comboBox3.DataSource = await this.professorsService.GetProfessors();
              
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void SetForm(FormMarks form)
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
                var classObj = (Lesson)this.comboBox1.SelectedItem;
                var classObj2 = (Student)this.comboBox2.SelectedItem;
                var classObj3 = (Professor)this.comboBox3.SelectedItem;
                var mark = new Mark(0, (int)this.numericUpDown1.Value, classObj2, classObj3, classObj);
                await this.markService.Add(mark);
                MessageBox.Show("Zapisano ocene");
                this.form.RefreshWindow();
                this.Dispose();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
        }
    }
}
