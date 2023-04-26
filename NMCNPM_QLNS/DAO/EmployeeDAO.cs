using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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

    }

}
