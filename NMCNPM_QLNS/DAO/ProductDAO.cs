using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NMCNPM_QLNS.DAO
{
    public class ProductDAO
    {
        private static ProductDAO instance;

        public static ProductDAO Instance
        {
            get { if (instance == null) instance = new ProductDAO(); return instance; }
            private set { instance = value; }
        }
        private ProductDAO() { }
        public void loadProductList(ListView listView)
        {
            string query = "USP_LoadProduct";
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
        public void loadSpecifiProductList(ListView listView,int type,string value) 
        {
            string query;
            DataTable data = new DataTable();
            if(type==1)
            {
                query = "select * from SANPHAM where sanphamID like '%' + @sanphamID + '%'";
                data = DataProvider.Instance.ExecuteQuery(query, new object[] { value });
            }
            else if(type==2)
            {
                query = "select * from SANPHAM where NCC like '%' + @NCC + '%'";
                data = DataProvider.Instance.ExecuteQuery(query, new object[] { value });
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
        public bool addNewProduct(string sanphamID,string sanphamName, string gia,string chietkhau, string NCC)
        {
            string checkExist = "select * from SANPHAM where sanphamID = @sanphamID";
            DataTable tmp = DataProvider.Instance.ExecuteQuery(checkExist, new object[] { sanphamID, });
            if(tmp.Rows.Count > 0)
            {
                MessageBox.Show("Không thể thêm sản phẩm này cho do đã tồn tài sản phẩm với Mã sản phẩm là " + sanphamID);
                return false;
            }
            else
            {
                string query = "insert into SANPHAM values ( CAST( @sanphamID as bigint) , @sanphamName , CAST( @gia as bigint) , CAST( @chietkhau as bigint) , @NCC)";
                int data = DataProvider.Instance.ExecuteNonQuery(query,new object[] { sanphamID, sanphamName, gia, chietkhau, NCC });
                if(data > 0)
                {
                    MessageBox.Show("Thêm thông tin sản phẩm thành công. Vui lòng ấn refresh để cập nhật Danh sách");
                    return true;
                }
                else
                {
                    MessageBox.Show("Đã xảy ra lỗi khi thêm thông tin sản phẩm mới");
                    return false;
                }
            }
        }
        public void changeProductInfo( string gia, string chietkhau, string sanphamID)
        {
            string query = "update SANPHAM set gia=CAST( @gia as bigint) , chietkhau=CAST( @chietkhau as bigint) where sanphamID = CAST( @sanphamID as bigint)";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { gia, chietkhau, sanphamID });
            if(data > 0)
            {
                MessageBox.Show("Cập nhật thông tin sản phẩm thành công. Vui lòng ấn refresh để cập nhật Danh sách", "Thành công thêm nhân viên");
            }
            else
            {
                MessageBox.Show("Đã xảy ra lỗi khi cập nhật thông tin sản phẩm", "WARNING");
            }
        }
        public void deleteProduct(string  sanphamID)
        {
            string query = "detele from SANPHAM where sanphamID = @sanphamID";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { sanphamID });
            if(data > 0)
            {
                MessageBox.Show("Xóa thông tin sản phẩm thành công");
            }
            else
            {
                MessageBox.Show("Xóa thông tin sản phẩm thất bại");
            }
        }
    }
}
