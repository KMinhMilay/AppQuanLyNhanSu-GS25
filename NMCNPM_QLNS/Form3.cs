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
    public partial class Form3 : Form
    {

        public Form3()
        {
            InitializeComponent();
            loadEmployeeListView(employeeListView);
            addCBX_ChucVu_QueQuan_HopDong_Note();
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
        private void refreshEmployeeList()
        {
            employeeListView.Items.Clear();
            EmployeeDAO.Instance.loadEmployeeList(employeeListView);
        }
        private void clearInput()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();

            rdNam.Checked = false;
            rdNu.Checked = false;   
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;   
            comboBox3.SelectedIndex = -1;   
            comboBox4.SelectedIndex = -1;
        }
        private void addCBX_ChucVu_QueQuan_HopDong_Note()
        {
            DataTable chucvuList = ChucVu_QueQuan_HopDong_Note_DAO.Instance.loadChucVuList();
            foreach (DataRow item in chucvuList.Rows)
            {
                comboBox2.Items.Add(item[0].ToString());
            }
            chucvuList.Clear();
            DataTable quequanList = ChucVu_QueQuan_HopDong_Note_DAO.Instance.loadQueQuanList();
            foreach (DataRow item in quequanList.Rows)
            {
                comboBox3.Items.Add(item[0].ToString());
            }
            quequanList.Clear();
            DataTable hopdongList = ChucVu_QueQuan_HopDong_Note_DAO.Instance.loadHopDongList();
            foreach (DataRow item in hopdongList.Rows)
            {
                comboBox4.Items.Add(item[0].ToString());
            }
            hopdongList.Clear();
            DataTable noteList = ChucVu_QueQuan_HopDong_Note_DAO.Instance.loadNoteList();
            foreach (DataRow item in noteList.Rows)
            {
                comboBox1.Items.Add(item[0].ToString());
            }
            noteList.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Form7());
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
        }
        private void button5_Click(object sender, EventArgs e)
        {
            clearInput();
            refreshEmployeeList();
        }

        private void employeeListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(employeeListView.SelectedItems.Count > 0)
            {
                textBox1.Text = employeeListView.FocusedItem.SubItems[0].Text.ToString();
                comboBox2.SelectedIndex = comboBox2.FindStringExact(employeeListView.FocusedItem.SubItems[1].Text);
                textBox3.Text = employeeListView.FocusedItem.SubItems[2].Text.ToString();
                textBox4.Text = employeeListView.FocusedItem.SubItems[3].Text.ToString();
                textBox2.Text = employeeListView.FocusedItem.SubItems[4].Text.ToString();

                if (rdNu.Checked == true)
                {
                    rdNu.Checked = false;
                }
                if( rdNam.Checked == true)
                {
                    rdNam.Checked = false;
                }
                string date = employeeListView.FocusedItem.SubItems[5].Text.ToString();
                dateTimePicker1.Value = DateTime.Parse(date);

                comboBox3.SelectedIndex = comboBox3.FindStringExact(employeeListView.FocusedItem.SubItems[6].Text);
                comboBox4.SelectedIndex = comboBox4.FindStringExact(employeeListView.FocusedItem.SubItems[8].Text);
                comboBox1.SelectedIndex = comboBox1.FindStringExact(employeeListView.FocusedItem.SubItems[7].Text);
            }
        }

        private void rdNu_Click(object sender, EventArgs e)
        {
            textBox2.Text = "Nữ";
        }
        private void rdNam_Click(object sender, EventArgs e)
        {
            textBox2.Text = "Nam";
        }
    }
}
