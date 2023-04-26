using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NMCNPM_QLNS.DAO
{
    public class ChucVu_QueQuan_HopDong_Note_DAO
    {
        private static ChucVu_QueQuan_HopDong_Note_DAO instance;
        public static ChucVu_QueQuan_HopDong_Note_DAO Instance
        {
            get { if (instance == null) instance = new ChucVu_QueQuan_HopDong_Note_DAO(); return instance; }
            set { instance = value; }
        }
        private ChucVu_QueQuan_HopDong_Note_DAO() { }

        public DataTable loadChucVuList()
        {
            string query = "USP_Load_ChucVu_List";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            return data;
        }
        public DataTable loadQueQuanList()
        {
            string query = "USP_Load_QueQuan_List";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            return data;
        }
        public DataTable loadHopDongList()
        {
            string query = "USP_Load_HopDong_List";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            return data;
        }
        public DataTable loadNoteList()
        {
            string query = "USP_Load_Note_List";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            return data;
        }
    }
}
