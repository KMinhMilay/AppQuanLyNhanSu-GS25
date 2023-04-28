using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NMCNPM_QLNS.DAO
{
    public class EmployeeDAO
    {
        private static EmployeeDAO instance;

        public static EmployeeDAO Instance
        {
            get { if (instance == null) instance = new EmployeeDAO(); return instance; }
            private set { instance = value; }
        }
        private EmployeeDAO() { }

        public void loadEmployeeList(ListView employeeListView)
        {
            string query = "SELECT * FROM dbo.NHANVIEN";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow row in data.Rows)
            {
                ListViewItem item = new ListViewItem(row[0].ToString());
                for (int i = 1; i < data.Columns.Count; i++)
                {
                    if (i == 5)
                    {
                        string tmp = row[i].ToString();
                        if (tmp.Length == 20)
                        {
                            tmp = tmp.Substring(0, 8);
                        }
                        else if (tmp.Length == 21)
                        {
                            tmp = tmp.Substring(0, 9);
                        }
                        else if (tmp.Length == 22)
                        {
                            tmp = tmp.Substring(0, 10);
                        }
                        item.SubItems.Add(tmp);
                    }
                    else
                    {
                        item.SubItems.Add(row[i].ToString());
                    }

                }
                employeeListView.Items.Add(item);
            }

        }
        public void deleteEmployee(string employeeID)
        {
            string query = "DELETE FROM NHANVIEN WHERE nvID = @nvID";
            int data = DataProvider.Instance.ExecuteNonQuery(query , new object[] {employeeID});
            if(data>0) {
                MessageBox.Show("Xóa thông tin nhân viên thành công");
            }
        }
        public bool addNewEmployee(string nvID,string ChucVu,string nvHo,string nvTen,string nvGioiTinh,string nvNgaySinh, string nvQueQuan , string TTNV , string HopDong)
        {
            string checkExist = "select * from NHANVIEN where nvID = @nvID";
            DataTable tmp = DataProvider.Instance.ExecuteQuery(checkExist, new object[] { nvID });
            if(tmp.Rows.Count > 0)
            {
                MessageBox.Show("Không thể thêm thông tin nhân viên do đã tồn tại một nhân viên với ID là " + nvID);
                return false;
            }
            else
            {
                string query = "insert into NHANVIEN values ( CAST( @nvID as BIGINT) , @ChucVu , @nvHo , @nvTen , @nvGioiTinh , CAST( @nvNgaySinh as DATE) , @nvQueQuan , @TTNV , @HopDong )";
                int data = DataProvider.Instance.ExecuteNonQuery (query , new object[] { nvID, ChucVu, nvHo, nvTen, nvGioiTinh, nvNgaySinh, nvQueQuan, TTNV, HopDong });
                if(data>0)
                {
                    MessageBox.Show("Thêm thông tin nhân viên thành công. Vui lòng ấn refresh để cập nhật Danh sách");

                    return true;
                }
                else
                {
                    MessageBox.Show("Đã xảy ra lỗi khi thêm thông tin nhân viên");

                    return false;
                }
            }
        }
        public bool existEmployee(string employeeID)
        {
            string query = "select * from NHANVIEN where nvID = @employeeID";
            DataTable data = DataProvider.Instance.ExecuteQuery(query , new object[] {employeeID});
            if(data.Rows.Count>0)
            {
                return true;
            }else { return false; }
        }
        public void changeEmployeeInfo(string ChucVu, string nvHo, string nvTen, string nvGioiTinh, string nvNgaySinh,string nvQueQuan , string TTNV, string HopDong, string nvID)
        {

            string query = "update NHANVIEN set ChucVu = @ChucVu , nvHo = @nvHo , nvTen = @nvTen , nvGioiTinh = @nvGioiTinh , nvNgaySinh = CAST( @nvNgaySinh AS DATE) , nvQueQuan = @nvQueQuan , TTNV = @TTNV , HopDong = @HopDong where nvID = CAST( @nvID AS bigint)  ";
            int data = DataProvider.Instance.ExecuteNonQuery(query , new object[] { ChucVu, nvHo, nvTen, nvGioiTinh,nvNgaySinh , nvQueQuan, TTNV, HopDong , nvID});
            if(data>0)
            {
                MessageBox.Show("Cập nhật thông tin nhân viên thành công. Vui lòng ấn refresh để cập nhật Danh sách","Thành công thêm nhân viên");
            }
            else
            {
                MessageBox.Show("Đã xảy ra lỗi khi cập nhật thông tin nhân viên","WARNING");
            }
        }
        public void loadSpecificEmployeeList(ListView listView,int type,string sreachValue)
        {
            string query;
            DataTable data = new DataTable();

            if(type == 1)
            {
                query = "select * from NHANVIEN where nvID like '%' + @nvID + '%'";
                data = DataProvider.Instance.ExecuteQuery(query, new object[] { sreachValue });
            }
            else if(type == 2)
            {
                query = "select * from NHANVIEN where ChucVu like '%' + @ChucVu + '%'";
                data = DataProvider.Instance.ExecuteQuery(query, new object[] { sreachValue });
            }
            else if (type == 3)
            {
                query = "select * from NHANVIEN where TTNV like '%' + @TTNV + '%'";
                data = DataProvider.Instance.ExecuteQuery(query, new object[] { sreachValue });
            }
            foreach (DataRow row in data.Rows)
            {
                ListViewItem item = new ListViewItem(row[0].ToString());
                for (int i = 1; i < data.Columns.Count; i++)
                {
                    if (i == 5)
                    {
                        string tmp = row[i].ToString();
                        if (tmp.Length == 20)
                        {
                            tmp = tmp.Substring(0, 8);
                        }
                        else if (tmp.Length == 21)
                        {
                            tmp = tmp.Substring(0, 9);
                        }
                        else if (tmp.Length == 22)
                        {
                            tmp = tmp.Substring(0, 10);
                        }
                        item.SubItems.Add(tmp);
                    }
                    else
                    {
                        item.SubItems.Add(row[i].ToString());
                    }

                }
                listView.Items.Add(item);
            }

        }
    }

}
