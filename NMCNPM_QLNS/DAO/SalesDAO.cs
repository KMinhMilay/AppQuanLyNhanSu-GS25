using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NMCNPM_QLNS.DAO
{
    public class SalesDAO
    {
        private static SalesDAO instance;

        public static SalesDAO Instance
        {
            get { if (instance == null) instance = new SalesDAO(); return instance; }
            private set { instance = value; }
        }
        private SalesDAO() { }
        public void loadSaleList(ListView saleListView)
        {
            string query = "SELECT * FROM dbo.DOANHTHU";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow row in data.Rows)
            {
                string tmp = row[0].ToString();
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
                ListViewItem item = new ListViewItem(tmp);
                for (int i = 1; i < data.Columns.Count; i++)
                {

                        item.SubItems.Add(row[i].ToString());

                }
                saleListView.Items.Add(item);
            }
        }
        public DataTable exportSaleList()
        {
            string query = "SELECT * FROM DOANHTHU";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            //DataTable dt = new DataTable();
            //foreach (DataRow row in data.Rows)
            //{
            //    string tmp = row[0].ToString();
            //    if (tmp.Length == 20)
            //    {
            //        tmp = tmp.Substring(0, 8);
            //    }
            //    else if (tmp.Length == 21)
            //    {
            //        tmp = tmp.Substring(0, 9);
            //    }
            //    else if (tmp.Length == 22)
            //    {
            //        tmp = tmp.Substring(0, 10);
            //    }
            //    dt.Rows.Add(tmp);
            //    for (int i = 1; i < data.Columns.Count; i++)
            //    {
            //        dt.Rows.Add(row[i].ToString());
            //    }
            //}
            return data;
        }
        public void loadSpecificSaleList(ListView saleListView,string sreachValue)
        {
            string query = "select * from DOANHTHU where ngaythangSold = CAST( @ngaythangSold as DATE) ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { sreachValue });
            foreach (DataRow row in data.Rows)
            {
                string tmp = row[0].ToString();
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
                ListViewItem item = new ListViewItem(tmp);
                for (int i = 1; i < data.Columns.Count; i++)
                {

                    item.SubItems.Add(row[i].ToString());


                }
                saleListView.Items.Add(item);
            }
        }
        public void changeSaleInfo( string cashReceived , string cashDebt , string TTDT , string ngaythangSold)
        {
            string query = "update DOANHTHU set cashReceived = CAST( @cashReceived AS BIGINT) , cashDebt = CAST( @cashDebt AS BIGINT) , TTDT = @TTDT where ngaythangSold = CAST( @ngaythangSold as DATE) ";
            int data = DataProvider.Instance.ExecuteNonQuery(query , new object[] {cashReceived,cashDebt,TTDT,ngaythangSold});
            if (data>0)
            {
                MessageBox.Show("Cập nhật thông tin doanh thu thành công. Vui lòng ấn refresh để cập nhật Danh sách", "Thành công thêm nhân viên");
            }
            else
            {
                MessageBox.Show("Đã xảy ra lỗi khi cập nhật thông tin doanh thu", "WARNING");
            }
        }
    }
}
