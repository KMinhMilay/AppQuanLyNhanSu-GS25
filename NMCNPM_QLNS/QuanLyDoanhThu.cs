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
        public void updateNewest()
        {
            SalesDAO.Instance.updateNewestSale(DateTime.Now.ToString("MM-dd-yyyy"));
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
            Microsoft.Office.Interop.Excel.Range head = oSheet.get_Range("A1", "H1");
            head.Interior.ColorIndex = 33;
            head.MergeCells = true;

            head.Value2 = title;

            head.Font.Bold = true;

            head.Font.Name = "Times New Roman";

            head.Font.Size = "20";

            head.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            // Tạo tiêu đề cột 

            Microsoft.Office.Interop.Excel.Range cl1 = oSheet.get_Range("A3", "A3");

            cl1.Value2 = "Ngày tháng năm";

            cl1.ColumnWidth = 20.0;

            Microsoft.Office.Interop.Excel.Range cl2 = oSheet.get_Range("B3", "B3");

            cl2.Value2 = "Doanh thu tiền mặt";

            cl2.ColumnWidth = 20.0;

            Microsoft.Office.Interop.Excel.Range cl3 = oSheet.get_Range("C3", "C3");

            cl3.Value2 = "Doanh thu tiền điện tử";
            cl3.ColumnWidth = 20.0;

            Microsoft.Office.Interop.Excel.Range cl4 = oSheet.get_Range("D3", "D3");

            cl4.Value2 = "Doanh thu tiền bank";

            cl4.ColumnWidth = 20.0;

            Microsoft.Office.Interop.Excel.Range cl5 = oSheet.get_Range("E3", "E3");

            cl5.Value2 = "Tổng doanh thu";

            cl5.ColumnWidth = 15.0;

            Microsoft.Office.Interop.Excel.Range cl6 = oSheet.get_Range("F3", "F3");

            cl6.Value2 = "Doanh thu đã nhận";

            cl6.ColumnWidth = 20.0;

            Microsoft.Office.Interop.Excel.Range cl7 = oSheet.get_Range("G3", "G3");

            cl7.Value2 = "Doanh thu còn thiếu";

            cl7.ColumnWidth = 20.0;

            Microsoft.Office.Interop.Excel.Range cl8 = oSheet.get_Range("H3", "H3");

            cl8.Value2 = "Trạng thái";

            cl8.ColumnWidth = 15.0;




            Microsoft.Office.Interop.Excel.Range rowHead = oSheet.get_Range("A3", "H3");

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
                    if (col == 0)
                    {
                        string sub = dataRow[col].ToString();
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

            int rowEnd = rowStart + dataTable.Rows.Count - 2;

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
            updateNewest();
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
            DialogResult result = MessageBox.Show("Bạn có muốn lưu thay đổi về doanh thu này không", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

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
        private void button6_Click(object sender, EventArgs e)
        {
            DataTable dataTable = SalesDAO.Instance.exportSaleList();
            DateTime curr = DateTime.Now;
            string sheetNamme = "SẢN PHẨM";
            string title = "Danh sách sản phẩm ngày " + curr.ToString("dd-MM-yyyy");
            ExportFile(dataTable, sheetNamme, title);
        }
    }
}
