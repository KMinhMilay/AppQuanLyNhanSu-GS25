using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NMCNPM_QLNS
{
    public partial class test : Form
    {
        public test()
        {
            InitializeComponent();
        }
        string idEmployeeDate = "";
        string cpy;
        string idYear = DateTime.Now.Year.ToString();
        string idMonth = DateTime.Now.Month.ToString();
        string idDay = DateTime.Now.Day.ToString();
        int count = 1;
        

        public void applyID()
        {
            cpy = idYear.Substring(2);
            if(idMonth.Length < 2)
            {
                idMonth = "0" + idMonth;
            }
            if(idDay.Length < 2) 
            {
                idDay = "0" + idDay;
            }
        }
        public string getIDNewEmployee()
        {

            applyID();
            string idCountInDay = "000";
            int idCount = Int16.Parse(idCountInDay)+count;
            if(idCount < 10)
            {
                idEmployeeDate = cpy + idMonth + idDay + "00"+ idCount;
            }
            else if (idCount < 100)
            {
                idEmployeeDate = cpy + idMonth + idDay + "0" + idCount;
            }
            else if (idCount < 1000) {
                idEmployeeDate = cpy + idMonth + idDay + idCount;
            }
            else
            {
                MessageBox.Show("LỖI");
            } 
            if(count == 999)
            {
                //Hide nut them nhan vien
                count = 0;
                return idCountInDay;
            }
            else
            {
                count++;
                return idEmployeeDate;
            }

        }
        private void button1_Click(object sender, EventArgs e)
        {             
            string result = getIDNewEmployee();
            textBox1.Text = result;
            label1.Text = (Int32.Parse(result) -1).ToString();

        }
    }
}
