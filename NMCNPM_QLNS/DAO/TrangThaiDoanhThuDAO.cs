using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NMCNPM_QLNS.DAO
{
    public class TrangThaiDoanhThuDAO
    {
        private static TrangThaiDoanhThuDAO instance;
        public static TrangThaiDoanhThuDAO Instance
        {
            get { if (instance == null) instance = new TrangThaiDoanhThuDAO(); return instance; }
            set { instance = value; }
        }
        private  TrangThaiDoanhThuDAO() { }
        
        public DataTable loadTrangThaiDoanhThu()
        {
            string query = "USP_LoadTTDT";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            return data;
        }
    }
}
