using System;
using System.Windows.Forms;
using WinFormStudents.Model;
using WinFormStudents.Services;

namespace SzkolaPostgreSQLWinForms
{
    public partial class FormAddProfessor : Form
    {
        private FormProfessors form;
        private readonly ScienceTitleService scienceTitleService;
        private readonly ProfessorService professorService;
        public FormAddProfessor()
        {
            InitializeComponent();
            this.scienceTitleService = new ScienceTitleService();
            this.professorService = new ProfessorService();
            this.LoadToComboBox();
        }

        private async void LoadToComboBox()
        {
            try
            {
                this.comboBox1.DataSource = await this.scienceTitleService.Get();
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
            try
            {
                if (this.textBox1.Text.Length < 3 || this.textBox2.Text.Length < 3)
                {
                    MessageBox.Show("Imię lub nazwisko mają za mało znaków");
                }
                else
                {
                    var classObj = (TitleScience)this.comboBox1.SelectedItem;
                    var professor = new Professor(0, this.textBox1.Text, this.textBox2.Text,
                       classObj);
                    await this.professorService.Add(professor);
                    MessageBox.Show("Zapisano wykładowce");
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
