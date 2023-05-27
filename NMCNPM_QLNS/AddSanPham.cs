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
        public bool existSanPham()
        {
            if (ProductDAO.Instance.existProduct(IDtxb.Text))
            {
                return true;
            }
            return false;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if(Int64.TryParse(IDtxb.Text,out _)==false || Int64.Parse(Moneytxb.Text) <0 || Int64.Parse(CKtxb.Text) < 0 || Int64.Parse(CKtxb.Text)>Int64.Parse(Moneytxb.Text) || IDtxb.ForeColor == Color.Red)
            {
                MessageBox.Show("Vui lòng nhập đúng định dạng");
                clearInput();

            }
            else
            {
                if (string.IsNullOrWhiteSpace(IDtxb.Text) || string.IsNullOrWhiteSpace(Nametxb.Text) || string.IsNullOrWhiteSpace(Nametxb.Text) || string.IsNullOrWhiteSpace(CKtxb.Text) || string.IsNullOrWhiteSpace(CKtxb.Text))
                {
                    MessageBox.Show("Bạn chưa điền đầy đủ các thông tin cần thiết");
                }
                else
                {

                    if (ProductDAO.Instance.addNewProduct(IDtxb.Text.Trim(), Nametxb.Text.Trim(), Moneytxb.Text.Trim(), CKtxb.Text.Trim(), NCCtxb.Text.Trim()) == true)
                    {
                        clearInput();
                    }
                }
            }
        }

        private void IDtxb_TextChanged(object sender, EventArgs e)
        {
            if (existSanPham()|| int.TryParse(IDtxb.Text, out _) == false)
            {
                IDtxb.Text.Trim();
                IDtxb.ForeColor = Color.Red;
            }
            else
            {
                IDtxb.Text.Trim();
                IDtxb.ForeColor = Color.Green;
            }

        }
    }
}
