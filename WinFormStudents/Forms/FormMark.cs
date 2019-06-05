using System;
using System.Windows.Forms;
using WinFormStudents.Model;
using WinFormStudents.Services;

namespace SzkolaPostgreSQLWinForms
{
    public partial class FormMarks : Form
    {
        private readonly MarkService marksService;
        public FormMarks()
        {
            InitializeComponent();
            this.marksService = new MarkService();
            this.Text = "Zarządzanie ocenami";
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

                dataGridView1.DataSource = await this.marksService.GetMarks();
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
                var item = (Mark)dataGridView1.SelectedRows[0].DataBoundItem;
                FormEditMark form = new FormEditMark(item);
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
                        var item = (Mark)dataGridView1.SelectedRows[0].DataBoundItem;
                        await this.marksService.Remove(item);
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
            FormAddMark form = new FormAddMark();
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
