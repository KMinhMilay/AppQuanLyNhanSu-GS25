using NMCNPM_QLNS.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace NMCNPM_QLNS
{
    public partial class MenuButton : Form
    {
        public MenuButton()
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
            OpenChildForm(new QuanLyNhanSu());

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
            OpenChildForm(new QuanLyNhanSu());


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
            displayNamelabel.Text = Login.displayname;

        }


    }
}
