using System;
using System.Windows.Forms;
using WinFormStudents.Model;
using WinFormStudents.Services;

namespace SzkolaPostgreSQLWinForms
{
    public partial class FormEditMark : Form
    {
        private FormMarks form;
        private readonly MarkService markService;
        private readonly LessonService lessonService;
        private readonly StudentService studentService;
        private readonly ProfessorService professorsService;
        private readonly int markId;
        public FormEditMark(Mark markd)
        {
            InitializeComponent();
            this.markService = new MarkService();
            this.studentService = new StudentService();
            this.lessonService = new LessonService();
            this.professorsService = new ProfessorService();
            this.numericUpDown1.Value = markd.MarkNumber;
            this.LoadToComboBox(markd);
            this.markId = markd.MarkId;
        }

        private async void LoadToComboBox(Mark mark)
        {
            try
            {
                this.comboBox1.DataSource = await this.lessonService.Get();
                this.comboBox1.SelectedIndex = mark.Lesson.LessonId - 1;

                this.comboBox2.DataSource = await this.studentService.GetStudents();
                this.comboBox2.SelectedItem = mark.Student;

                this.comboBox3.DataSource = await this.professorsService.GetProfessors();
                this.comboBox3.SelectedItem = mark.Professor;
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

            var classObj = (Lesson)this.comboBox1.SelectedItem;
            var classObj2 = (Student)this.comboBox2.SelectedItem;
            var classObj3 = (Professor)this.comboBox3.SelectedItem;
            var mark = new Mark(this.markId, (int)this.numericUpDown1.Value, classObj2, classObj3, classObj);
                await this.markService.Edit(mark);
                MessageBox.Show("Zapisano ocene");
                this.form.RefreshWindow();
                this.Dispose();
            
        }
    }
}
