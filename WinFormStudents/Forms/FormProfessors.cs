using System;
using System.Windows.Forms;
using WinFormStudents.Model;
using WinFormStudents.Services;

namespace SzkolaPostgreSQLWinForms
{
    public partial class FormProfessors : Form
    {
        private readonly ProfessorService professorService;
        public FormProfessors()
        {
            InitializeComponent();
            this.professorService = new ProfessorService();
            this.Text = "Zarządzanie wykładowcami";
            this.LoadDataToDataGrid();
        }

        public void RefreshWindow()
        {
            this.LoadDataToDataGrid();
        }

        private async void LoadDataToDataGrid()
        {
            try
            {

                dataGridView1.DataSource = await professorService.GetProfessors();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                var professor = (Professor)dataGridView1.SelectedRows[0].DataBoundItem;
                FormEditProfessor form = new FormEditProfessor(professor);
                form.SetForm(this);
                form.ShowDialog();
            }
            else
            {
                MessageBox.Show("Zaznacz rekord");
            }
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                if (MessageBox.Show("Czy na pewno chcesz usunąć rekord ?", "Ostrzeżenie!", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    try
                    {
                        var item = (Professor)dataGridView1.SelectedRows[0].DataBoundItem;
                        await this.professorService.Remove(item);
                        this.LoadDataToDataGrid();
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
            }
            else
            {
                MessageBox.Show("Zaznacz rekord");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FormAddProfessor form = new FormAddProfessor();
            form.SetForm(this);
            form.ShowDialog();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }
    }
}
