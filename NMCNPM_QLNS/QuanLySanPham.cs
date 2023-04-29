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

namespace NMCNPM_QLNS
{
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
            LoadProductList(productListView);
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
            panel1.Controls.Add(childForm);
            panel1.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }
        void LoadProductList(ListView listView)
        {
            ProductDAO.Instance.loadProductList(listView);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Form8());
        }



        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            label5.Text=radioButton1.Text.ToString();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            label5.Text=radioButton2.Text.ToString();
        }
    }
}
