using System;
using System.Windows.Forms;
using WinFormStudents.Model;
using WinFormStudents.Services;

namespace SzkolaPostgreSQLWinForms
{
    public partial class FormEditProfessor: Form
    {
        private FormProfessors form;
        private readonly ScienceTitleService titleScienceService;
        private readonly ProfessorService professorsService;
        private readonly int profId;
        public FormEditProfessor(Professor professor)
        {
            InitializeComponent();
            this.titleScienceService = new ScienceTitleService();
            this.professorsService = new ProfessorService();
            this.textBox1.Text = professor.FirstName;
            this.textBox2.Text = professor.LastName;
          
            this.LoadToComboBox(professor);
            this.profId = professor.ProfessorId;
        }

        private async void LoadToComboBox(Professor selectedClass)
        {
            try
            {
                this.comboBox1.DataSource = await this.titleScienceService.Get();
                this.comboBox1.SelectedIndex = selectedClass.TitleScience.TitleScienceId - 1;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void SetForm(FormProfessors form)
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
                var classObj = (TitleScience)this.comboBox1.SelectedItem;
                var professor = new Professor(profId, this.textBox1.Text, this.textBox2.Text,
                   classObj);
                await this.professorsService.Edit(professor);
                MessageBox.Show("Zapisano wykładowce");
                this.form.RefreshWindow();
                this.Dispose();
            }
        }
    }
}
