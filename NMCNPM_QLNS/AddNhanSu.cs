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
    public partial class AddNhanSu : Form
    {
        public AddNhanSu()
        {
            InitializeComponent();
            addCBX_ChucVu_QueQuan_HopDong_Note();
        }
        private void addCBX_ChucVu_QueQuan_HopDong_Note()
        {
            DataTable chucvuList = ChucVu_QueQuan_HopDong_Note_DAO.Instance.loadChucVuList();
            foreach (DataRow item in chucvuList.Rows)
            {
                ChucVucbx.Items.Add(item[0].ToString());
            }
            chucvuList.Clear();
            DataTable quequanList = ChucVu_QueQuan_HopDong_Note_DAO.Instance.loadQueQuanList();
            foreach (DataRow item in quequanList.Rows)
            {
                QueQuancbx.Items.Add(item[0].ToString());
            }
            quequanList.Clear();
            DataTable hopdongList = ChucVu_QueQuan_HopDong_Note_DAO.Instance.loadHopDongList();
            foreach (DataRow item in hopdongList.Rows)
            {
                HopDongtxb.Items.Add(item[0].ToString());
            }
            hopdongList.Clear();
            DataTable noteList = ChucVu_QueQuan_HopDong_Note_DAO.Instance.loadNoteList();
            foreach (DataRow item in noteList.Rows)
            {
                Notetxb.Items.Add(item[0].ToString());
            }
            noteList.Clear();
        }
        private void clearInput()
        {
            IDtbx.Clear();
            ChucVucbx.SelectedIndex = -1;
            Namrdb.Checked = false;
            Nurdb.Checked = false;
            GioiTinhtxb.Clear();
            Hotxb.Clear();
            Tentxb.Clear();
            QueQuancbx.SelectedIndex = -1;
            dateTimePicker1.Value = DateTime.Now;
            Notetxb.SelectedIndex = -1;
            HopDongtxb.SelectedIndex = -1;
        }

        
        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();

        }
        //string nvID,string ChucVu,string nvHo,string nvTen,string nvGioiTinh, string nvQueQuan, string nvNgaySinh, string TTNV, string HopDong
        private void button3_Click(object sender, EventArgs e) // nut them
        {
            if(string.IsNullOrWhiteSpace(IDtbx.Text)||string.IsNullOrWhiteSpace(ChucVucbx.Text) || string.IsNullOrWhiteSpace(Hotxb.Text) || string.IsNullOrWhiteSpace(Tentxb.Text) || string.IsNullOrWhiteSpace(GioiTinhtxb.Text) || string.IsNullOrWhiteSpace(QueQuancbx.Text) || string.IsNullOrWhiteSpace(dateTimePicker1.Text) || string.IsNullOrWhiteSpace(Notetxb.Text) || string.IsNullOrWhiteSpace(HopDongtxb.Text))
            {
                MessageBox.Show("Bạn chưa điền đầy đủ các thông tin cần thiết");
            }
            else
            {
                string date = dateTimePicker1.Value.ToString("MM-dd-yyyy");
                if(EmployeeDAO.Instance.addNewEmployee(IDtbx.Text, ChucVucbx.Text, Hotxb.Text, Tentxb.Text,GioiTinhtxb.Text, date,  QueQuancbx.Text,Notetxb.Text,HopDongtxb.Text)==true) 
                {
                    clearInput();
                }
            }
        }

        private void Namrdb_CheckedChanged(object sender, EventArgs e)
        {
            GioiTinhtxb.Text = "Nam";
        }

        private void Nurdb_CheckedChanged(object sender, EventArgs e)
        {
            GioiTinhtxb.Text = "Nữ";
        }
    }
}
