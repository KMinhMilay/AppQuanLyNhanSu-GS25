using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NMCNPM_QLNS
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            button1.FlatStyle = FlatStyle.Flat;
            button1.FlatAppearance.BorderSize = 0;
            button2.FlatStyle = FlatStyle.Flat;
            button2.FlatAppearance.BorderSize = 0;
            button3.FlatStyle = FlatStyle.Flat;
            button3.FlatAppearance.BorderSize = 0;
            button4.FlatStyle = FlatStyle.Flat;
            button4.FlatAppearance.BorderSize = 0;
            button5.FlatStyle = FlatStyle.Flat;
            button5.FlatAppearance.BorderSize = 0;
            OpenChildForm(new Form3());
        }
        private Form currentFormChild;
        private void OpenChildForm(Form childForm)
        {
            if (currentFormChild != null)
            {
                currentFormChild.Close();
            }
            currentFormChild = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panel2.Controls.Add(childForm);
            panel2.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Form3());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Form4());
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Form5());
        }

        private void button5_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Form6());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult option = MessageBox.Show("Bạn có chắc muốn thoát chương trình và đăng xuất không ?", "Đăng xuất", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (option == DialogResult.Yes)
            {
                currentFormChild.Close();
                this.Close();
            }
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            label2.Text = Form1.displayName;

        }
    }
}
