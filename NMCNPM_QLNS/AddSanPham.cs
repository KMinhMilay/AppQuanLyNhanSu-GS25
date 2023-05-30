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
            if (string.IsNullOrWhiteSpace(IDtxb.Text) || string.IsNullOrWhiteSpace(Nametxb.Text) || string.IsNullOrWhiteSpace(Moneytxb.Text) || string.IsNullOrWhiteSpace(CKtxb.Text) || string.IsNullOrWhiteSpace(NCCtxb.Text))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin sản phẩm cần thêm", "Cảnh báo");

            }
            else
            {


                if (Int64.TryParse(IDtxb.Text, out _) == false || IDtxb.ForeColor == Color.Red)
                {
                    MessageBox.Show("Vui lòng nhập lại ID do trùng ID hoặc định dạng ID ko đúng", "Cảnh báo");
                    clearInput();
                }
                else if (Int64.Parse(CKtxb.Text) > Int64.Parse(Moneytxb.Text))
                {
                    MessageBox.Show("Bạn đã nhập giá trị chiết khấu lớn hơn đơn giá ", "Cảnh báo");
                    clearInput();
                }
                else if (Int64.Parse(Moneytxb.Text) < 1000 || Int64.Parse(Moneytxb.Text) > 10000000)
                {
                    MessageBox.Show("Vui lòng nhập đơn giá sản phẩm không dưới 1 nghìn đồng và trên 10 triệu đồng", "Cảnh báo");
                    clearInput();
                }
                else if (Int64.Parse(CKtxb.Text) < 0 || Int64.Parse(CKtxb.Text) > 10000000)
                {
                    MessageBox.Show("Vui lòng nhập chiết khẩu sản phẩm không dưới 0 đồng và trên 10 triệu đồng", "Cảnh báo");
                    clearInput();
                }
                else if (string.IsNullOrWhiteSpace(IDtxb.Text) || string.IsNullOrWhiteSpace(Nametxb.Text) || string.IsNullOrWhiteSpace(Nametxb.Text) || string.IsNullOrWhiteSpace(CKtxb.Text) || string.IsNullOrWhiteSpace(CKtxb.Text))
                {
                    MessageBox.Show("Bạn chưa điền đầy đủ các thông tin cần thiết", "Cảnh báo");
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
            if (Int64.TryParse(IDtxb.Text, out _) == false)
            {
                if (IDtxb.Text == "")
                {
                    IDtxb.Text = "10000";
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập đúng định dạng mã sản phẩm chỉ chứa số", "Cảnh báo");
                    clearInput();
                }

            }
            else
            {
                if (existSanPham())
                {

                    IDtxb.ForeColor = Color.Red;
                }
                else
                {

                    IDtxb.ForeColor = Color.Green;
                }
            }

        }

        private void CKtxb_TextChanged(object sender, EventArgs e)
        {
            if (Int64.TryParse(CKtxb.Text, out _) == false)
            {
                if (CKtxb.Text == "")
                {
                    CKtxb.Text = "0";
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập đúng định dạng chiết khấu", "Cảnh báo");
                    CKtxb.Text = "0";
                }

            }
        }

        private void NCCtxb_TextChanged(object sender, EventArgs e)
        {

        }

        private void Moneytxb_TextChanged(object sender, EventArgs e)
        {
            if (Int64.TryParse(Moneytxb.Text, out _) == false)
            {
                if (Moneytxb.Text == "")
                {
                    Moneytxb.Text = "1000";
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập đúng định dạng giá tiền", "Cảnh báo");
                    Moneytxb.Text = "1000";
                }

            }
        }
    }
}
