using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using QLNS.Properties;

namespace QLNS
{
    public partial class frmCaiDat : DevExpress.XtraEditors.XtraForm
    {
        public frmCaiDat()
        {
            InitializeComponent();
            if (BUS.NguoiDung == null)
                BUS.NguoiDung = new BusinessLogic.NguoiDung();
            if (BUS.DacQuyen == null)
                BUS.DacQuyen = new BusinessLogic.DacQuyen();

            DataRow r = BUS.NguoiDung.Table.Rows.Find(Settings.Default.User_CurrentUser);
            nd = new DataTransfer.NguoiDung(r["MaNguoiDung"].ToString(), r["TenDangNhap"].ToString(),
                r["MatKhau"].ToString(), r["HoTen"].ToString(), r["GioiTinh"].ToString(), r["MaDacQuyen"].ToString(),
                (DateTime)r["NgaySinh"], r["SoDienThoai"].ToString(), r["Email"].ToString());
        }

        private DataTransfer.NguoiDung nd;

        /// <summary>
        /// Nạp từ người dùng lên giao diện
        /// </summary>
        public void NapGiaoDien()
        {
            txtTenDangNhap.Text = nd.TenDangNhap;
            txtHoTen.Text = nd.HoTen;
            dateNgaySinh.EditValue = nd.NgaySinh;
            cboGioiTinh.Text = nd.GioiTinh;
            cboDacQuyen.EditValue = nd.MaDacQuyen;
            txtSoDienThoai.Text = nd.SoDienThoai;
            txtEmail.Text = nd.Email;
        }

        /// <summary>
        /// Nạp từ giao diện lên biến người dùng
        /// </summary>
        /// <returns></returns>
        public DataTransfer.NguoiDung NapNguoiDung()
        {
            if (string.IsNullOrEmpty(txtTenDangNhap.Text) ||
                string.IsNullOrEmpty(txtHoTen.Text) || string.IsNullOrEmpty(txtSoDienThoai.Text))
                throw new ArgumentException("Vui lòng nhập đủ thông tin");
            else if ((!string.IsNullOrEmpty(txtMatKhauCu.Text) && txtMatKhauCu.Text != nd.MatKhau) || txtMatKhauMoi.Text != txtMatKhauMoiAgain.Text)
                throw new ArgumentException("Mật khẩu không khớp hoặc mật khẩu cũ không chính xác, vui lòng nhập lại.");


            if (txtMatKhauCu.Text == nd.MatKhau && txtMatKhauMoi.Text == txtMatKhauMoiAgain.Text && !string.IsNullOrEmpty(txtMatKhauMoi.Text))
                nd.MatKhau = txtMatKhauMoi.Text;

            nd.HoTen = txtHoTen.Text;
            nd.NgaySinh = (DateTime)dateNgaySinh.EditValue;
            nd.GioiTinh = cboGioiTinh.Text;
            nd.SoDienThoai = txtSoDienThoai.Text;
            nd.Email = txtEmail.Text;

            return nd;
        }

        private void frmCaiDatNguoiDung_Load(object sender, EventArgs e)
        {
            //nạp giới tính
            cboGioiTinh.Properties.Items.AddRange(new string[] { "Nam", "Nữ" });

            //nạp đặc quyền
            cboDacQuyen.Properties.Columns.Add(new LookUpColumnInfo("TenDacQuyen"));
            cboDacQuyen.Properties.DataSource = BUS.DacQuyen.Table;
            cboDacQuyen.Properties.DisplayMember = "TenDacQuyen";
            cboDacQuyen.Properties.ValueMember = "MaDacQuyen";

            //nap người dùng
            NapGiaoDien();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                nd = NapNguoiDung();

                if (BUS.NguoiDung.Sua(nd))
                {
                    XtraMessageBox.Show("Cập nhật thông tin người dùng " + nd.HoTen + " thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                }
                else
                    XtraMessageBox.Show("Cập nhật thông tin người dùng " + nd.HoTen + " không thành công", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (ArgumentException ex)
            {
                XtraMessageBox.Show(ex.Message, "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}