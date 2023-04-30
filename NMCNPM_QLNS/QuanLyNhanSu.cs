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
    public partial class QuanLyNhanSu : Form
    {
        int type = 1;
        public QuanLyNhanSu()
        {
            InitializeComponent();
            loadEmployeeListView(employeeListView);
            addCBX_ChucVu_QueQuan_HopDong_Note();
            refreshEmployeeList();
    
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
        void loadEmployeeListView(ListView listView)
        {
            EmployeeDAO.Instance.loadEmployeeList(listView);
        }
        void refreshEmployeeList()
        {
            employeeListView.Items.Clear();
            EmployeeDAO.Instance.loadEmployeeList(employeeListView);
        }
        void clearInput()
        {
            IDtxt.Clear();
            GioiTinhtxb.Clear();
            Hotxb.Clear();
            Tentxb.Clear();
            textBox5.Clear();

            rdNam.Checked = false;
            rdNu.Checked = false;   
            Notecbx.SelectedIndex = -1;
            ChucVucbx.SelectedIndex = -1;   
            QueQuancbx.SelectedIndex = -1;   
            HopDongtxb.SelectedIndex = -1;
        }
        void addCBX_ChucVu_QueQuan_HopDong_Note()
        {
            DataTable chucvuList = ChucVu_QueQuan_HopDong_Note_DAO.Instance.loadChucVuList();
            foreach (DataRow item in chucvuList.Rows)
            {
                ChucVucbx.Items.Add(item[0].ToString());
            }
            chucvuList.Clear();
            DataTable quequanList = ChucVu_QueQuan_HopDong_Note_DAO.Instance.loadQueQuanList();
            foreach (DataRow item in quequanList.Rows)
            {
                QueQuancbx.Items.Add(item[0].ToString());
            }
            quequanList.Clear();
            DataTable hopdongList = ChucVu_QueQuan_HopDong_Note_DAO.Instance.loadHopDongList();
            foreach (DataRow item in hopdongList.Rows)
            {
                HopDongtxb.Items.Add(item[0].ToString());
            }
            hopdongList.Clear();
            DataTable noteList = ChucVu_QueQuan_HopDong_Note_DAO.Instance.loadNoteList();
            foreach (DataRow item in noteList.Rows)
            {
                Notecbx.Items.Add(item[0].ToString());
            }
            noteList.Clear();
        }






        private void button3_Click(object sender, EventArgs e) //Form Them Nhan Vien
        {
            OpenChildForm(new AddNhanSu());
        }
        private void employeeListView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            ItemComparer sorter = employeeListView.ListViewItemSorter as ItemComparer;

            if (sorter == null)
            {
                sorter = new ItemComparer(e.Column);
                sorter.Order = SortOrder.Ascending;
                employeeListView.ListViewItemSorter = sorter;
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
            employeeListView.Sort();
        }//hienthi
        private void button5_Click(object sender, EventArgs e)
        {
            clearInput();
            refreshEmployeeList();
        } // refresh
        private void employeeListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(employeeListView.SelectedItems.Count > 0)
            {
                IDtxt.Text = employeeListView.FocusedItem.SubItems[0].Text.ToString();
                ChucVucbx.SelectedIndex = ChucVucbx.FindStringExact(employeeListView.FocusedItem.SubItems[1].Text);
                Hotxb.Text = employeeListView.FocusedItem.SubItems[2].Text.ToString();
                Tentxb.Text = employeeListView.FocusedItem.SubItems[3].Text.ToString();
                GioiTinhtxb.Text = employeeListView.FocusedItem.SubItems[4].Text.ToString();

                if (rdNu.Checked == true)
                {
                    rdNu.Checked = false;
                }
                if( rdNam.Checked == true)
                {
                    rdNam.Checked = false;
                }
                string date = employeeListView.FocusedItem.SubItems[5].Text.ToString();
                dateTimePickerNgaySinh.Value = DateTime.Parse(date);

                QueQuancbx.SelectedIndex = QueQuancbx.FindStringExact(employeeListView.FocusedItem.SubItems[6].Text);
                HopDongtxb.SelectedIndex = HopDongtxb.FindStringExact(employeeListView.FocusedItem.SubItems[8].Text);
                Notecbx.SelectedIndex = Notecbx.FindStringExact(employeeListView.FocusedItem.SubItems[7].Text);
            }
        }
        private void rdNu_Click(object sender, EventArgs e)
        {
            GioiTinhtxb.Text = "Nữ";
        }
        private void rdNam_Click(object sender, EventArgs e)
        {
            GioiTinhtxb.Text = "Nam";
        }
        private void button2_Click(object sender, EventArgs e) //Xoa Nhan Vien
        {
            if(employeeListView.SelectedItems.Count <= 0)
            {
                MessageBox.Show("Vui lòng chọn nhân viên bạn muốn xóa");
            }
            else
            {
                DialogResult deleteUserWarning = MessageBox.Show("Bạn có muốn xóa người dùng này không?", "Cảnh báo", MessageBoxButtons.YesNo);
                if(deleteUserWarning == DialogResult.Yes )
                {
                    if (!string.IsNullOrWhiteSpace(IDtxt.Text))
                    {
                        EmployeeDAO.Instance.deleteEmployee(IDtxt.Text);
                        refreshEmployeeList();
                    }
                }
            }
        }
        private void button1_Click(object sender, EventArgs e) // tim kiem
        {
            employeeListView.Items.Clear();
            EmployeeDAO.Instance.loadSpecificEmployeeList(employeeListView, type, textBox5.Text);
        }
        //string nvID,string ChucVu,string nvHo,string nvTen,string nvGioiTinh, string nvQueQuan, string nvNgaySinh, string TTNV, string HopDong
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

            label13.Text = radioButton2.Text;
            type = 1;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            label13.Text = radioButton1.Text;
            type = 2;

        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            label13.Text = radioButton3.Text;
            type=3;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có muốn lưu thay đổi về thông tin của nhân viên này không", "Cảnh báo", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                EmployeeDAO.Instance.changeEmployeeInfo(ChucVucbx.Text,Hotxb.Text,Tentxb.Text,GioiTinhtxb.Text,dateTimePickerNgaySinh.Value.ToString("MM-dd-yyyy"),QueQuancbx.Text,Notecbx.Text,HopDongtxb.Text,IDtxt.Text);
                refreshEmployeeList();
                clearInput();
            }
        }
    }
}
