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
            Microsoft.Office.Interop.Excel.Range head = oSheet.get_Range("A1", "E1");
            head.Interior.ColorIndex = 33;
            head.MergeCells = true;

            head.Value2 = title;

            head.Font.Bold = true;

            head.Font.Name = "Times New Roman";

            head.Font.Size = "20";

            head.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            // Tạo tiêu đề cột 

            Microsoft.Office.Interop.Excel.Range cl1 = oSheet.get_Range("A3", "A3");

            cl1.Value2 = "Mã sản phẩm";

            cl1.ColumnWidth = 20.0;

            Microsoft.Office.Interop.Excel.Range cl2 = oSheet.get_Range("B3", "B3");

            cl2.Value2 = "Tên sản phẩm";

            cl2.ColumnWidth = 40.0;

            Microsoft.Office.Interop.Excel.Range cl3 = oSheet.get_Range("C3", "C3");

            cl3.Value2 = "Giá tiền";
            cl3.ColumnWidth = 15.0;

            Microsoft.Office.Interop.Excel.Range cl4 = oSheet.get_Range("D3", "D3");

            cl4.Value2 = "Chiết khẩu";

            cl4.ColumnWidth = 15.0;

            Microsoft.Office.Interop.Excel.Range cl5 = oSheet.get_Range("E3", "E3");

            cl5.Value2 = "Nhà cung cấp";

            cl5.ColumnWidth = 30.0;

            Microsoft.Office.Interop.Excel.Range rowHead = oSheet.get_Range("A3", "E3");

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
            DialogResult result = MessageBox.Show("Bạn có muốn lưu thay đổi về giá và chiết khấu sản phẩm của nhân viên này không", "Cảnh báo", MessageBoxButtons.YesNo , MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            
            {
                if(Int64.Parse(Moneytxb.Text) <0 || Int64.Parse(CKtxb.Text)<0)
                {
                    MessageBox.Show("Vui lòng nhập đúng định dạng giá tiền hay chiết khấu");
                }
                else
                {
                    ProductDAO.Instance.changeProductInfo(Moneytxb.Text.Trim(), CKtxb.Text.Trim(), IDtxb.Text.Trim());

                    refreshProductList();

                    clearInput();
                }
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
        private void button6_Click(object sender, EventArgs e)
        {
            DataTable dataTable = ProductDAO.Instance.exportProductList();
            DateTime curr = DateTime.Now;
            string sheetNamme = "SẢN PHẨM";
            string title = "Danh sách sản phẩm ngày " + curr.ToString("dd-MM-yyyy");
            ExportFile(dataTable, sheetNamme, title);
        }

        private void Moneytxb_TextChanged(object sender, EventArgs e)
        {

        }

        private void CKtxb_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
