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
    public partial class QuanLyLuong : Form
    {
        int type = 0;
        public QuanLyLuong()
        {
            InitializeComponent();
            LoadSalaryList(SalaryListView);

        }
        public void LoadSalaryList(ListView listView)
        {
            SalaryDAO.Instance.loadSalaryList(listView);
        }
        public void clearInput()
        {
            IDtxb.Clear();
            Hotxb.Clear();
            Tentxb.Clear();
            ChucVutxb.Clear();
            GioiTinhtxb.Clear();
            Luongtxb.Clear();
            TangCatxb.Clear();
            TongLuongtxb.Clear();
            Notetxb.Clear();
            Searchtxb.Clear();
        }
        public void refreshSalaryList()
        {
            SalaryListView.Items.Clear();
            SalaryDAO.Instance.loadSalaryList(SalaryListView);
        }


        private void SalaryListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SalaryListView.SelectedItems.Count > 0)
            {
                IDtxb.Text = SalaryListView.FocusedItem.SubItems[0].Text;
                ChucVutxb.Text = SalaryListView.FocusedItem.SubItems[1].Text;
                Hotxb.Text = SalaryListView.FocusedItem.SubItems[2].Text;
                Tentxb.Text = SalaryListView.FocusedItem.SubItems[3].Text;
                GioiTinhtxb.Text = SalaryListView.FocusedItem.SubItems[4].Text;
                Luongtxb.Text = SalaryListView.FocusedItem.SubItems[5].Text;
                TangCatxb.Text = SalaryListView.FocusedItem.SubItems[6].Text;
                TongLuongtxb.Text = SalaryListView.FocusedItem.SubItems[7].Text;
                Notetxb.Text = SalaryListView.FocusedItem.SubItems[8].Text;

            }
        }
        private void SalaryListView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            ItemComparer sorter = SalaryListView.ListViewItemSorter as ItemComparer;

            if (sorter == null)
            {
                sorter = new ItemComparer(e.Column);
                sorter.Order = SortOrder.Ascending;
                SalaryListView.ListViewItemSorter = sorter;
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
            SalaryListView.Sort();
        }
        private void IDrdb_CheckedChanged(object sender, EventArgs e)
        {
            label12.Text=IDrdb.Text;
            type = 0;
        }
        private void ChucVurdb_CheckedChanged(object sender, EventArgs e)
        {
            label12.Text= ChucVurdb.Text;
            type = 1;
        }
        private void Noterdb_CheckedChanged(object sender, EventArgs e)
        {
            label12.Text= Noterdb.Text;
            type = 2;
        }
        private void button5_Click(object sender, EventArgs e)
        {
            refreshSalaryList();
            clearInput();
        }
        private void button1_Click(object sender, EventArgs e) //timkiem
        {
            SalaryListView.Items.Clear();
            SalaryDAO.Instance.loadSpecificSalaryList(SalaryListView,type,Searchtxb.Text);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có muốn lưu thay đổi về lương của nhân viên này không", "Cảnh báo", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                SalaryDAO.Instance.changeSalary(Luongtxb.Text.Trim(),TangCatxb.Text.Trim(),IDtxb.Text.Trim());

                refreshSalaryList();

                clearInput();
            }
        }
    }
}
