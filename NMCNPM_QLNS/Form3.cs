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
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            radioButton1.Checked = false;
            radioButton2.Checked = false;   
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;   
            comboBox3.SelectedIndex = -1;   
            comboBox4.SelectedIndex = -1;
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
    }
}
