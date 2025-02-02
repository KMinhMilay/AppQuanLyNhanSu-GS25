﻿using NMCNPM_QLNS.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace NMCNPM_QLNS
{
    public partial class AddNhanSu : Form
    {
        public AddNhanSu()
        {
            InitializeComponent();
            addCBX_ChucVu_QueQuan_HopDong_Note();


        }
        string cpy;
        string idYear = DateTime.Now.Year.ToString();
        string idMonth = DateTime.Now.Month.ToString();
        string idDay = DateTime.Now.Day.ToString();
        public string applyID() //lay id: yymmdd
        {
            cpy = idYear.Substring(2);
            if (idMonth.Length < 2)
            {
                idMonth = "0" + idMonth;
            }
            if (idDay.Length < 2)
            {
                idDay = "0" + idDay;
            }
             return cpy + idMonth + idDay;
        }
        public bool existNhanVien()
        {
            if(EmployeeDAO.Instance.existEmployee(IDtbx.Text))
            {
                return true;
            }
            return false;
        }
        public string selectLastestID()
        {
            return EmployeeDAO.Instance.selectLastEmployssList();
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
            IDtbx.Text = applyID();
            ChucVucbx.SelectedIndex = -1;
            Namrdb.Checked = false;
            Nurdb.Checked = false;
            GioiTinhtxb.Clear();
            Hotxb.Clear();
            Tentxb.Clear();
            QueQuancbx.SelectedIndex = -1;
            dateTimePicker1.Value = DateTime.Now;
            Notetxb.SelectedIndex = 0;
            HopDongtxb.SelectedIndex = -1;
        }
        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();

        }
        //string nvID,string ChucVu,string nvHo,string nvTen,string nvGioiTinh, string nvQueQuan, string nvNgaySinh, string TTNV, string HopDong
        private void button3_Click(object sender, EventArgs e) // nut them
        {
            bool checkHo = true;
            bool checkTen = true;
            string[] stringArray = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };
            foreach (string x in stringArray)
            {
                if (Hotxb.Text.Contains(x))
                {
                    checkHo = false;
                }
                if (Tentxb.Text.Contains(x))
                {
                    checkTen = false;
                }
            }
            if (checkHo == false)
            {
                MessageBox.Show("Họ nhân viên không được chứa số", "Cảnh báo");
            }
            else if (checkTen == false)
            {
                MessageBox.Show("Tên nhân viên không được chứa số", "Cảnh báo");
            }
            else if (string.IsNullOrWhiteSpace(IDtbx.Text)||IDtbx.Text==applyID()||IDtbx.TextLength==6||IDtbx.ForeColor == Color.Red || string.IsNullOrWhiteSpace(ChucVucbx.Text) || string.IsNullOrWhiteSpace(Hotxb.Text) || string.IsNullOrWhiteSpace(Tentxb.Text) || string.IsNullOrWhiteSpace(GioiTinhtxb.Text) || string.IsNullOrWhiteSpace(QueQuancbx.Text) || string.IsNullOrWhiteSpace(dateTimePicker1.Text) || string.IsNullOrWhiteSpace(Notetxb.Text) || string.IsNullOrWhiteSpace(HopDongtxb.Text))
            {
                if (IDtbx.ForeColor == Color.Red)
                {
                    MessageBox.Show("Vui lòng nhập đúng định dạng ID","Cảnh báo");

                }
                else
                MessageBox.Show("Bạn chưa điền đầy đủ các thông tin cần thiết", "Cảnh báo");
            }
            else
            {
                if (DateTime.Now.Year - dateTimePicker1.Value.Year >= 18)
                {
                    string date = dateTimePicker1.Value.ToString("MM-dd-yyyy");
                    if (EmployeeDAO.Instance.addNewEmployee(IDtbx.Text.Trim(), ChucVucbx.Text, Hotxb.Text.Trim(), Tentxb.Text.Trim(), GioiTinhtxb.Text, date, QueQuancbx.Text, Notetxb.Text, HopDongtxb.Text) == true)
                    {
                        clearInput();
                        label11.Text = "";
                        label11.Text = "Bạn vừa thêm 1 nhân viên với mã: " + selectLastestID() + " vào " + DateTime.Now.ToString();
                    }
                }
                else
                {
                    MessageBox.Show("Nhân viên này bạn nhập tuổi dưới 18. Mời nhập lại", "Cảnh báo");
                    dateTimePicker1.Value = DateTime.Now;
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


        private void IDtbx_TextChanged(object sender, EventArgs e)
        {
            if (existNhanVien()||IDtbx.TextLength>9||IDtbx.TextLength<6||int.TryParse(IDtbx.Text,out _)==false)
            {
                IDtbx.Text.Trim();
                IDtbx.ForeColor = Color.Red;
            }
            else
            {
                IDtbx.Text.Trim();
                IDtbx.ForeColor = Color.Green;
            }
        }

        private void ChucVucbx_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void AddNhanSu_Load(object sender, EventArgs e)
        {
            IDtbx.Text = applyID();
            label11.Text = "";
            label11.Text = "Bạn vừa thêm 1 nhân viên mới gần đây nhất với mã: " + selectLastestID();
            Notetxb.SelectedIndex = 0;
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }
    }
}
