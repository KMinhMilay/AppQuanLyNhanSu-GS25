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
    public partial class AddSanPham : Form
    {
        public AddSanPham()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public void clearInput()
        {
            IDtxb.Clear();
            Nametxb.Clear();
            Moneytxb.Clear();
            CKtxb.Clear();
            NCCtxb.Clear();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(IDtxb.Text) || string.IsNullOrWhiteSpace(Nametxb.Text) || string.IsNullOrWhiteSpace(Nametxb.Text) || string.IsNullOrWhiteSpace(CKtxb.Text)|| string.IsNullOrWhiteSpace(CKtxb.Text))
            {
                MessageBox.Show("Bạn chưa điền đầy đủ các thông tin cần thiết");
            }
            else
            {
                if(ProductDAO.Instance.addNewProduct(IDtxb.Text.Trim(),Nametxb.Text.Trim(),Moneytxb.Text.Trim(),CKtxb.Text.Trim(),NCCtxb.Text.Trim())==true)
                {
                    clearInput();
                }
            }
        }
    }
}
