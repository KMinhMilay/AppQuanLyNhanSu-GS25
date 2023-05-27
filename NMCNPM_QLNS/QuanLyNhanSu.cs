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
            Sreachtxb.Clear();
            dateTimePickerNgaySinh.Value = DateTime.Now;
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
        public void ExportFile(DataTable dataTable, string sheetName, string title)
        {
            //Tạo các đối tượng Excel

            Microsoft.Office.Interop.Excel.Application oExcel = new Microsoft.Office.Interop.Excel.Application();

            Microsoft.Office.Interop.Excel.Workbooks oBooks;

            Microsoft.Office.Interop.Excel.Sheets oSheets;

            Microsoft.Office.Interop.Excel.Workbook oBook;

            Microsoft.Office.Interop.Excel.Worksheet oSheet;

            //Tạo mới một Excel WorkBook 

            oExcel.Visible = true;

            oExcel.DisplayAlerts = false;

            oExcel.Application.SheetsInNewWorkbook = 1;

            oBooks = oExcel.Workbooks;

            oBook = (Microsoft.Office.Interop.Excel.Workbook)(oExcel.Workbooks.Add(Type.Missing));

            oSheets = oBook.Worksheets;

            oSheet = (Microsoft.Office.Interop.Excel.Worksheet)oSheets.get_Item(1);

            oSheet.Name = sheetName;

            // Tạo phần Tiêu đề
            Microsoft.Office.Interop.Excel.Range head = oSheet.get_Range("A1", "I1");
            head.Interior.ColorIndex = 33;
            head.MergeCells = true;

            head.Value2 = title;

            head.Font.Bold = true;

            head.Font.Name = "Times New Roman";

            head.Font.Size = "20";

            head.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            // Tạo tiêu đề cột 

            Microsoft.Office.Interop.Excel.Range cl1 = oSheet.get_Range("A3", "A3");

            cl1.Value2 = "ID nhân viên";

            cl1.ColumnWidth = 15.0;

            Microsoft.Office.Interop.Excel.Range cl2 = oSheet.get_Range("B3", "B3");

            cl2.Value2 = "Chức vụ";

            cl2.ColumnWidth = 12.0;

            Microsoft.Office.Interop.Excel.Range cl3 = oSheet.get_Range("C3", "C3");

            cl3.Value2 = "Họ và tên đệm";
            cl3.ColumnWidth = 20.0;

            Microsoft.Office.Interop.Excel.Range cl4 = oSheet.get_Range("D3", "D3");

            cl4.Value2 = "Tên";

            cl4.ColumnWidth = 15.0;

            Microsoft.Office.Interop.Excel.Range cl5 = oSheet.get_Range("E3", "E3");

            cl5.Value2 = "Giới tính";

            cl5.ColumnWidth = 12.0;

            Microsoft.Office.Interop.Excel.Range cl6 = oSheet.get_Range("F3", "F3");

            cl6.Value2 = "Ngày sinh";

            cl6.ColumnWidth = 15.0;

            Microsoft.Office.Interop.Excel.Range cl7 = oSheet.get_Range("G3", "G3");

            cl7.Value2 = "Quê quán";

            cl7.ColumnWidth = 15.0;

            Microsoft.Office.Interop.Excel.Range cl8 = oSheet.get_Range("H3", "H3");

            cl8.Value2 = "Tình trạng";

            cl8.ColumnWidth = 15.0;

            Microsoft.Office.Interop.Excel.Range cl9 = oSheet.get_Range("I3", "I3");

            cl9.Value2 = "Hợp đồng";

            cl9.ColumnWidth = 12.0;



            Microsoft.Office.Interop.Excel.Range rowHead = oSheet.get_Range("A3", "I3");

            rowHead.Font.Bold = true;

            // Kẻ viền

            rowHead.Borders.LineStyle = Microsoft.Office.Interop.Excel.Constants.xlSolid;

            // Thiết lập màu nền

            rowHead.Interior.ColorIndex = 33;

            rowHead.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            // Tạo mảng theo datatable

            object[,] arr = new object[dataTable.Rows.Count, dataTable.Columns.Count];

            //Chuyển dữ liệu từ DataTable vào mảng đối tượng

            for (int row = 0; row < dataTable.Rows.Count; row++)
            {
                DataRow dataRow = dataTable.Rows[row];

                for (int col = 0; col < dataTable.Columns.Count; col++)
                {
                    if (col == 5)
                    {
                        string sub= dataRow[col].ToString();
                        if (sub.Length == 20)
                        {
                            sub = sub.Substring(0, 8);
                        }
                        else if (sub.Length == 21)
                        {
                            sub = sub.Substring(0, 9);
                        }
                        else if (sub.Length == 22)
                        {
                            sub = sub.Substring(0, 10);
                        }
                        arr[row, col] = sub;
                    }
                    else
                    {
                        arr[row, col] = dataRow[col];
                    }

                }
            }
            //Thiết lập vùng điền dữ liệu

            int rowStart = 4;

            int columnStart = 1;

            int rowEnd = rowStart + dataTable.Rows.Count - 1;

            int columnEnd = dataTable.Columns.Count;

            // Ô bắt đầu điền dữ liệu

            Microsoft.Office.Interop.Excel.Range c1 = (Microsoft.Office.Interop.Excel.Range)oSheet.Cells[rowStart, columnStart];

            // Ô kết thúc điền dữ liệu

            Microsoft.Office.Interop.Excel.Range c2 = (Microsoft.Office.Interop.Excel.Range)oSheet.Cells[rowEnd, columnEnd];

            // Lấy về vùng điền dữ liệu

            Microsoft.Office.Interop.Excel.Range range = oSheet.get_Range(c1, c2);

            //Điền dữ liệu vào vùng đã thiết lập

            range.Value2 = arr;

            // Kẻ viền

            range.Borders.LineStyle = Microsoft.Office.Interop.Excel.Constants.xlSolid;

            // Căn giữa cột mã nhân viên

            //Microsoft.Office.Interop.Excel.Range c3 = (Microsoft.Office.Interop.Excel.Range)oSheet.Cells[rowEnd, columnStart];

            //Microsoft.Office.Interop.Excel.Range c4 = oSheet.get_Range(c1, c3);

            //Căn giữa cả bảng 
            oSheet.get_Range(c1, c2).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            oSheet.get_Range(c1, c2).VerticalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            oSheet.get_Range(c1, c2).WrapText = false;
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
                DialogResult deleteUserWarning = MessageBox.Show("Bạn có muốn xóa người dùng này không?", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if(deleteUserWarning == DialogResult.Yes )
                {
                    if (!string.IsNullOrWhiteSpace(IDtxt.Text))
                    {
                        EmployeeDAO.Instance.deleteEmployee(IDtxt.Text);
                        refreshEmployeeList();
                        clearInput();
                    }
                }
            }
        }
        private void button1_Click(object sender, EventArgs e) // tim kiem
        {
            if (Sreachtxb.Text == "")
            {
                MessageBox.Show("Bạn chưa điền thông tin cần tìm", "Cảnh báo");
            }
            else
            {
                employeeListView.Items.Clear();
                EmployeeDAO.Instance.loadSpecificEmployeeList(employeeListView, type, Sreachtxb.Text);
            }

        }
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
            bool checkHo = true;
            bool checkTen = true;
            string[] stringArray = { "1","2","3","4","5","6","7","8","9","0" };
            foreach (string x in stringArray)
            {
                if (Hotxb.Text.Contains(x))
                {
                    checkHo = false;
                }
                if (Tentxb.Text.Contains(x)) 
                { 
                    checkTen = false; 
                }
            }
            if (DateTime.Today.Year-dateTimePickerNgaySinh.Value.Year < 18||String.IsNullOrEmpty( Hotxb.Text)|| String.IsNullOrEmpty(Tentxb.Text))
            {
                if(DateTime.Today.Year - dateTimePickerNgaySinh.Value.Year < 18)
                {
                    MessageBox.Show("Nhân viên này bạn nhập tuổi dưới 18. Mời nhập lại", "Cảnh báo");
                    dateTimePickerNgaySinh.Value = DateTime.Now;
                }
                else
                {
                    MessageBox.Show("Họ hoặc tên của bạn không được phép trống hoặc chưa số", "Cảnh báo");
                }
            }
            else if (checkHo == false)
            {
                MessageBox.Show("Họ nhân viên không được chứa số. Mời nhập lại", "Cảnh báo");
                

            }
            else if (checkTen == false)
            {
                MessageBox.Show("Tên nhân viên không được chứa số. Mời nhập lại", "Cảnh báo");

                
            }
            else
            {
                DialogResult result = MessageBox.Show("Bạn có muốn lưu thay đổi về thông tin của nhân viên này không", "Cảnh báo", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    EmployeeDAO.Instance.changeEmployeeInfo(ChucVucbx.Text, Hotxb.Text.Trim(), Tentxb.Text.Trim(), GioiTinhtxb.Text.Trim(), dateTimePickerNgaySinh.Value.ToString("MM-dd-yyyy"), QueQuancbx.Text, Notecbx.Text, HopDongtxb.Text, IDtxt.Text);
                    refreshEmployeeList();
                    clearInput();
                }
            }
            
        }
        private void button6_Click(object sender, EventArgs e)
        {
            DataTable dataTable = EmployeeDAO.Instance.exportEmployeeList();
            DateTime curr=DateTime.Now;
            string sheetNamme = "NHÂN VIÊN";
            string title = "Danh sách nhân viên ngày " + curr.ToString("dd-MM-yyyy");
            ExportFile(dataTable, sheetNamme, title);
        }
    }
}
