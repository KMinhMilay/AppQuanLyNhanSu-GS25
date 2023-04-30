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
    public partial class QuanLyDoanhThu : Form
    {
        public QuanLyDoanhThu()
        {
            InitializeComponent();
            LoadSaleList(salesListView);
            addTTDTcbx();
            refreshSaleList();
        }
        public void LoadSaleList(ListView listView)
        {
            SalesDAO.Instance.loadSaleList(listView);
        }
        public void addTTDTcbx()
        {
            DataTable ttdtList = TrangThaiDoanhThuDAO.Instance.loadTrangThaiDoanhThu();
            foreach (DataRow item in ttdtList.Rows) 
            {
                TTDTcbx.Items.Add(item[0].ToString());
            }
            ttdtList.Clear();
        }
        public void clearInput()
        {
            dateTimePickerSreach.Value = DateTime.Now;
            Datetxb.Clear();
            TienMattxb.Clear();
            TienDienTutxb.Clear();
            TienBanktxb.Clear();
            Totaltxb.Text="0";
            DaNhantxb.Text="0";
            ConThieutxb.Text = "0";
            TTDTcbx.SelectedIndex = -1;

        }
        public void refreshSaleList()
        {
            salesListView.Items.Clear();
            SalesDAO.Instance.loadSaleList(salesListView);
        }
        private void button5_Click(object sender, EventArgs e)
        {
            refreshSaleList();
            clearInput();
        }
        private void salesListView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            ItemComparer sorter = salesListView.ListViewItemSorter as ItemComparer;

            if (sorter == null)
            {
                sorter = new ItemComparer(e.Column);
                sorter.Order = SortOrder.Ascending;
                salesListView.ListViewItemSorter = sorter;
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
            salesListView.Sort();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            salesListView.Items.Clear ();
            string sreach = dateTimePickerSreach.Value.ToString("MM-dd-yyyy");
            SalesDAO.Instance.loadSpecificSaleList(salesListView,sreach);
        }
        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có muốn lưu thay đổi về doanh thu này không", "Cảnh báo", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                SalesDAO.Instance.changeSaleInfo(DaNhantxb.Text.Trim(), ConThieutxb.Text.Trim(), TTDTcbx.Text, Datetxb.Text);
                refreshSaleList();
                clearInput();
            }
        }
        private void salesListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            Datetxb.Text = salesListView.FocusedItem.SubItems[0].Text.ToString();
            TienMattxb.Text = salesListView.FocusedItem.SubItems[1].Text.ToString();
            TienDienTutxb.Text = salesListView.FocusedItem.SubItems[2].Text.ToString();
            TienBanktxb.Text = salesListView.FocusedItem.SubItems[3].Text.ToString();
            Totaltxb.Text = salesListView.FocusedItem.SubItems[4].Text.ToString();
            DaNhantxb.Text = salesListView.FocusedItem.SubItems[5].Text.ToString();
            ConThieutxb.Text= salesListView.FocusedItem.SubItems[6].Text.ToString();
            TTDTcbx.SelectedIndex = TTDTcbx.FindStringExact(salesListView.FocusedItem.SubItems[7].Text);
            
        }
        private void DaNhantxb_TextChanged(object sender, EventArgs e)
        {
            if (DaNhantxb.Text == "")
            {
                DaNhantxb.Text = "0";
            }
            if (Int64.Parse(DaNhantxb.Text) < 0 && Int64.Parse(DaNhantxb.Text)> Int32.Parse(Totaltxb.Text))
            {
                MessageBox.Show("Bạn nhập sai hoặc quá số tiền nhận được", "WARNING");
            }
            else
            {
                ConThieutxb.Text = (Int64.Parse(Totaltxb.Text) - Int64.Parse(DaNhantxb.Text)).ToString();
                if (Int64.Parse(ConThieutxb.Text) > 0)
                {
                    TTDTcbx.SelectedIndex = TTDTcbx.FindStringExact("Chưa hoàn thành");
                }
                else
                {
                    TTDTcbx.SelectedIndex = TTDTcbx.FindStringExact("Hoàn thành");
                }
            }

        }


    }
}
