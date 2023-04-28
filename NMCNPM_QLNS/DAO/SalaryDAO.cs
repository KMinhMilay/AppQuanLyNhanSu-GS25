using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NMCNPM_QLNS.DAO
{
    public class SalaryDAO
    {
        private static SalaryDAO instance;

        public static SalaryDAO Instance
        {
            get { if (instance == null) instance = new SalaryDAO(); return instance; }
            private set { instance = value; }
        }
        private SalaryDAO() { }
        
        public void loadSalaryList(ListView listView)
        {
            string query = "USP_LoadSalary";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow row in data.Rows)
            {
                ListViewItem item = new ListViewItem(row[0].ToString());
                for (int i = 1; i < data.Columns.Count; i++)
                {
                        item.SubItems.Add(row[i].ToString());
                }
                listView.Items.Add(item);
            }
        }
        public void loadSpecificSalaryList(ListView listView, int type, string sreachValue)
        {
            string query;
            DataTable data = new DataTable();

            if (type == 0)
            {
                query = "SELECT b.nvID, a.ChucVu, a.nvHo , a.nvTen, a.nvGioiTinh ,b.soGioLam,b.tangGioLam,b.Luong,a.TTNV FROM NHANVIEN as a inner join SALARY as b ON a.nvID = b.nvID where a.nvID like '%' + @nvID + '%'";
                data = DataProvider.Instance.ExecuteQuery(query, new object[] { sreachValue });
            }
            else if (type == 1)
            {
                query = "SELECT b.nvID, a.ChucVu, a.nvHo , a.nvTen, a.nvGioiTinh ,b.soGioLam,b.tangGioLam,b.Luong,a.TTNV FROM NHANVIEN as a inner join SALARY as b ON a.nvID = b.nvID where a.ChucVu like '%' + @ChucVu + '%'";
                data = DataProvider.Instance.ExecuteQuery(query, new object[] { sreachValue });
            }
            else if (type == 2)
            {
                query = "SELECT b.nvID, a.ChucVu, a.nvHo , a.nvTen, a.nvGioiTinh ,b.soGioLam,b.tangGioLam,b.Luong,a.TTNV FROM NHANVIEN as a inner join SALARY as b ON a.nvID = b.nvID where a.TTNV like '%' + @TTNV + '%'";
                data = DataProvider.Instance.ExecuteQuery(query, new object[] { sreachValue });
            }
            foreach (DataRow row in data.Rows)
            {
                ListViewItem item = new ListViewItem(row[0].ToString());
                for (int i = 1; i < data.Columns.Count; i++)
                {
                        item.SubItems.Add(row[i].ToString());
                }
                listView.Items.Add(item);
            }

        }
        public void changeSalary(string soGioLam,string tangGioLam,string nvID)
        {
            string query = "update SALARY set soGioLam = CAST( @soGioLam as INT) , tangGioLam = CAST( @tangGioLam as INT) where nvID = @nvID";
            string querry = "update SALARY set Luong = soGioLam + tangGioLam * 4 where nvID = @nvID";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] {soGioLam,tangGioLam,nvID });
            int sum = DataProvider.Instance.ExecuteNonQuery(querry, new object[] {nvID});
            if (data > 0)
            {
                MessageBox.Show("Cập nhật lương thành công. Vui lòng ấn refresh để cập nhật danh sách", "Thành công cập nhật lương nhân viên");
            }
            else
            {
                MessageBox.Show("Đã xảy ra lỗi khi cập nhật lương","WARNING");
            }
        }
    }
}
