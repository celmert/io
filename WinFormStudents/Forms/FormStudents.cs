using System;
using System.Windows.Forms;
using WinFormStudents.Model;
using WinFormStudents.Services;

namespace SzkolaPostgreSQLWinForms
{
    public partial class FormStudents : Form
    {
        private readonly StudentService studentsService;
        public FormStudents()
        {
            InitializeComponent();
            this.studentsService = new StudentService();
            this.Text = "Zarządzanie studentami";
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
               
                dataGridView1.DataSource = await this.studentsService.GetStudents();   
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
                var professor = (Student)dataGridView1.SelectedRows[0].DataBoundItem;
                FormEdit form = new FormEdit(professor);
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
                        var professor = (Student)dataGridView1.SelectedRows[0].DataBoundItem;
                        await this.studentsService.RemoveStudent(professor);
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
            FormAdd form = new FormAdd();
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
