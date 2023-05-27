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

            cl6.Value2 = "Số giờ công";

            cl6.ColumnWidth = 15.0;

            Microsoft.Office.Interop.Excel.Range cl7 = oSheet.get_Range("G3", "G3");

            cl7.Value2 = "Số giờ tăng ca";

            cl7.ColumnWidth = 15.0;

            Microsoft.Office.Interop.Excel.Range cl8 = oSheet.get_Range("H3", "H3");

            cl8.Value2 = "Tổng lương";

            cl8.ColumnWidth = 15.0;

            Microsoft.Office.Interop.Excel.Range cl9 = oSheet.get_Range("I3", "I3");

            cl9.Value2 = "Tình trạng";

            cl9.ColumnWidth = 15.0;



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

                        arr[row, col] = dataRow[col];


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
            if (Searchtxb.Text == "")
            {
                MessageBox.Show("Bạn chưa điền thông tin cần tìm", "Cảnh báo");
            }
            else
            {
                SalaryListView.Items.Clear();
                SalaryDAO.Instance.loadSpecificSalaryList(SalaryListView, type, Searchtxb.Text);
            }

        }
        private void button4_Click(object sender, EventArgs e)
        {
            if (Int64.Parse(Luongtxb.Text) < 0 || Int64.Parse(TangCatxb.Text) < 0)
            {
                MessageBox.Show("Vui lòng nhập đúng định số giờ công hay số giờ tăng ca", "Cảnh báo");
            }

            else if (Int64.TryParse(Luongtxb.Text, out _) == false || Int64.TryParse(TangCatxb.Text, out _) == false)
            {
                MessageBox.Show("Số giờ công hay số giờ tăng ca không được chứa chữ cái hay kí tự", "Cảnh báo");
            }

            else
            {
                DialogResult result = MessageBox.Show("Bạn có muốn lưu thay đổi về lương của nhân viên này không", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    SalaryDAO.Instance.changeSalary(Luongtxb.Text.Trim(), TangCatxb.Text.Trim(), IDtxb.Text.Trim());

                    refreshSalaryList();

                    clearInput();
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Khi xuất file thì sẽ cập nhật lại bảng lương nhân viên về 0", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                DataTable dataTable = SalaryDAO.Instance.exportSalaryList();
                DateTime curr = DateTime.Now;
                string sheetNamme = "LƯƠNG THÁNG" + curr.ToString("MM");
                string title = "Danh sách lương nhân viên tháng " + curr.ToString("MM");
                ExportFile(dataTable, sheetNamme, title);
                SalaryDAO.Instance.resetSalary();
                refreshSalaryList();
                clearInput();
            }

        }

        private void Luongtxb_TextChanged(object sender, EventArgs e)
        {
            string prev = Luongtxb.Text;
            if(string.IsNullOrEmpty(Luongtxb.Text.Trim())) { Luongtxb.Text = "0"; }
            else if (Int64.TryParse(Luongtxb.Text,out _)==false)
            {
                MessageBox.Show("Số giờ công hay số giờ tăng ca không được chứa chữ cái hay kí tự", "Cảnh báo");
                Luongtxb.Text = "0";
            }
            
        }

        private void TangCatxb_TextChanged(object sender, EventArgs e)
        {
            string prev =  TangCatxb.Text;
            if (string.IsNullOrEmpty(TangCatxb.Text.Trim())) { TangCatxb.Text = "0"; }
            else if (Int64.TryParse(TangCatxb.Text,out _)==false)
            {
                MessageBox.Show("Số giờ công hay số giờ tăng ca không được chứa chữ cái hay kí tự", "Cảnh báo");
                TangCatxb.Text="0";
            }
        }
    }
}
