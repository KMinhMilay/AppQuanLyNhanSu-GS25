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
        int type = 1;
        public Form6()
        {
            InitializeComponent();
            loadProductList(productListView);
            refreshProductList();
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
        void loadProductList(ListView listView)
        {
            ProductDAO.Instance.loadProductList(listView);
        }
        void refreshProductList()
        {
            productListView.Items.Clear();
            ProductDAO.Instance.loadProductList(productListView);
        }
        void clearInput()
        {
            Sreachtxb.Clear();
            IDtxb.Clear();
            Nametxb.Clear();
            Moneytxb.Clear();
            CKtxb.Clear();
            NCCtxb.Clear();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            OpenChildForm(new AddSanPham());
        }



        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            label5.Text=radioButton1.Text.ToString();
            type = 1;
        }
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            label5.Text=radioButton2.Text.ToString();
            type = 2;
        }
        private void productListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (productListView.SelectedItems.Count > 0)
            {
                IDtxb.Text = productListView.FocusedItem.SubItems[0].Text;
                Nametxb.Text = productListView.FocusedItem.SubItems[1].Text;
                Moneytxb.Text = productListView.FocusedItem.SubItems[2].Text;
                CKtxb.Text = productListView.FocusedItem.SubItems[3].Text;
                NCCtxb.Text = productListView.FocusedItem.SubItems[4].Text;
            }
        }
        private void productListView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            ItemComparer sorter = productListView.ListViewItemSorter as ItemComparer;

            if (sorter == null)
            {
                sorter = new ItemComparer(e.Column);
                sorter.Order = SortOrder.Ascending;
                productListView.ListViewItemSorter = sorter;
            }
            // if clicked column is already the column that is being sorted
            if (e.Column == sorter.Column)
            {
                // Reverse the current sort direction
                if (sorter.Order == SortOrder.Ascending)
                    sorter.Order = SortOrder.Descending;
                else
                    sorter.Order = SortOrder.Ascending;
            }
            else
            {
                // Set the column number that is to be sorted; default to ascending.
                sorter.Column = e.Column;
                sorter.Order = SortOrder.Ascending;
            }
            productListView.Sort();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            productListView.Items.Clear();
            ProductDAO.Instance.loadSpecifiProductList(productListView,type,Sreachtxb.Text);
        }
        private void button5_Click(object sender, EventArgs e)
        {
            refreshProductList();
            clearInput();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có muốn lưu thay đổi về giá và chiết khấu sản phẩm của nhân viên này không", "Cảnh báo", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                ProductDAO.Instance.changeProductInfo(Moneytxb.Text, CKtxb.Text,IDtxb.Text);

                refreshProductList();

                clearInput();
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (productListView.SelectedItems.Count <= 0)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm bạn muốn xóa");
            }
            else
            {
                DialogResult deleteUserWarning = MessageBox.Show("Bạn có muốn xóa sản phẩm này không?", "Cảnh báo", MessageBoxButtons.YesNo);
                if (deleteUserWarning == DialogResult.Yes)
                {
                    if (!string.IsNullOrWhiteSpace(IDtxb.Text))
                    {
                        ProductDAO.Instance.deleteProduct(IDtxb.Text);
                        refreshProductList();
                    }

                }
            }
        }
    }
}
