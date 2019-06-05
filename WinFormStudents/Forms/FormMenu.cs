using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SzkolaPostgreSQLWinForms
{
    public partial class FormMenu : Form
    {
        public FormMenu()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
        }


        private void ShowForm(Form form)
        {
            form.WindowState = FormWindowState.Maximized;
            form.MdiParent = this;
            form.Show();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            this.ShowForm(new FormProfessors());
        }


        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            this.ShowForm(new FormMarks());
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.ShowForm(new FormStudents());
        }
    }
}

