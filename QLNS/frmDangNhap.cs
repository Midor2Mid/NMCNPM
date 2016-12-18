using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using QLNS.Properties;

namespace QLNS
{
    public partial class frmDangNhap : DevExpress.XtraEditors.XtraForm
    {
        public frmDangNhap()
        {
            InitializeComponent();
            if (BUS.NguoiDung == null)
                BUS.NguoiDung = new BusinessLogic.NguoiDung();
        }
        public DataTransfer.NguoiDung NguoiDung { get; set; }

        public void LoadSettings()
        {
            chkSave.Checked = Settings.Default.Login_IsSaveData;
            if (chkSave.Checked)
            {
                txtTenDangNhap.Text = Settings.Default.Login_UserName;
                txtMatKhau.Text = Settings.Default.Login_Password;
            }
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            if (BUS.NguoiDung.DangNhap(txtTenDangNhap.Text, txtMatKhau.Text))
            {
                DataRow r = BUS.NguoiDung.Table.Select(string.Format("TenDangNhap = '{0}'", txtTenDangNhap.Text))[0];
                NguoiDung = new DataTransfer.NguoiDung(r["MaNguoiDung"].ToString(), r["TenDangNhap"].ToString(),
                   r["MatKhau"].ToString(), r["HoTen"].ToString(), r["GioiTinh"].ToString(), r["MaDacQuyen"].ToString(),
                   Convert.ToDateTime(r["NgaySinh"]), r["SoDienThoai"].ToString(), r["Email"].ToString());

                if(chkSave.Checked)
                {
                    Settings.Default.Login_UserName = txtTenDangNhap.Text;
                    Settings.Default.Login_Password = txtMatKhau.Text;
                    Settings.Default.Login_IsSaveData = true;
                    Settings.Default.Save();
                }
                else
                {
                    Settings.Default.Login_UserName = string.Empty;
                    Settings.Default.Login_Password = string.Empty;
                    Settings.Default.Login_IsSaveData = false;
                    Settings.Default.Save();
                }
                Close();
            }
            else
            {
                lblThongBao.Text = "Tên đăng nhập hoặc mật khẩu không đúng!";
            }
        }


        private void frmDangNhap_Load(object sender, EventArgs e)
        {
            LoadSettings();
            lblThongBao.Text = string.Empty;
        }

        private void btnDangNhapKhach_Click(object sender, EventArgs e)
        {
            NguoiDung = null;
            Close();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void chkSave_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.Login_IsSaveData = chkSave.Checked;
            Settings.Default.Save();
        }
    }
}