using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using QLNS.Properties;
using DevExpress.XtraEditors.Controls;

namespace QLNS
{
    public partial class frmNguoiDung : DevExpress.XtraEditors.XtraForm
    {
        public frmNguoiDung()
        {
            InitializeComponent();
            if (BUS.NguoiDung == null)
                BUS.NguoiDung = new BusinessLogic.NguoiDung();
            if (BUS.DacQuyen == null)
                BUS.DacQuyen = new BusinessLogic.DacQuyen();
        }

        private DataTransfer.NguoiDung nd;
        private bool isInsert = true;

        /// <summary>
        /// Nạp danh sách người dùng
        /// </summary>
        public void NapListNguoiDung()
        {
            //nạp danh sách người dùng
            ImageList icon = new ImageList();
            icon.ImageSize = new Size(48, 48);
            icon.Images.Add(Resources.icon_user);

            lsNguoiDung.Items.Clear();

            lsNguoiDung.ImageList = icon;

            foreach (DataRow item in BUS.NguoiDung.Table.Rows)
            {
                lsNguoiDung.Items.Add(new ImageListBoxItem(item["MaNguoiDung"].ToString(), item["HoTen"].ToString(), 0));
            }
        }

        /// <summary>
        /// Nạp biến lên giao diện
        /// </summary>
        /// <param name="nd"></param>
        public void NapGiaoDien(DataTransfer.NguoiDung nd = null)
        {
            if (nd == null)
            {
                this.nd = new DataTransfer.NguoiDung(BUS.NguoiDung.AutoGenerateID(), string.Empty, string.Empty,
                    string.Empty, "Nam", "DQ01", DateTime.Today, string.Empty, string.Empty);
                NapGiaoDien(this.nd);
            }
            else
            {
                txtTenDangNhap.Text = nd.TenDangNhap;
                txtMatKhau.Text = nd.MatKhau;
                txtHoTen.Text = nd.HoTen;
                dateNgaySinh.EditValue = nd.NgaySinh;
                cboGioiTinh.Text = nd.GioiTinh;
                cboDacQuyen.EditValue = nd.MaDacQuyen;
                txtSoDienThoai.Text = nd.SoDienThoai;
                txtEmail.Text = nd.Email;
            }
        }

        /// <summary>
        /// Nạp giao diện xuống biến
        /// </summary>
        /// <returns></returns>
        public DataTransfer.NguoiDung NapNguoiDung()
        {
            if (string.IsNullOrEmpty(txtTenDangNhap.Text) || string.IsNullOrEmpty(txtMatKhau.Text) ||
                string.IsNullOrEmpty(txtHoTen.Text) || string.IsNullOrEmpty(txtSoDienThoai.Text))
                throw new ArgumentException("Vui lòng nhập đủ thông tin");

            nd.TenDangNhap = txtTenDangNhap.Text;
            nd.MatKhau = txtMatKhau.Text;
            nd.HoTen = txtHoTen.Text;
            nd.NgaySinh = (DateTime)dateNgaySinh.EditValue;
            nd.GioiTinh = cboGioiTinh.Text;
            nd.MaDacQuyen = cboDacQuyen.EditValue.ToString();
            nd.SoDienThoai = txtSoDienThoai.Text;
            nd.Email = txtEmail.Text;

            return nd;
        }

        /// <summary>
        /// Khóa các control lại
        /// </summary>
        public void KhoaControl()
        {
            txtTenDangNhap.Enabled = false;
            txtMatKhau.Enabled = false;
            txtHoTen.Enabled = false;
            dateNgaySinh.Enabled = false;
            cboGioiTinh.Enabled = false;
            cboDacQuyen.Enabled = false;
            txtSoDienThoai.Enabled = false;
            txtEmail.Enabled = false;
            btnLuu.Enabled = false;
            btnHuy.Enabled = false;
        }

        /// <summary>
        /// Mở khóa các control
        /// </summary>
        public void MoKhoaControl()
        {
            txtTenDangNhap.Enabled = true;
            txtMatKhau.Enabled = true;
            txtHoTen.Enabled = true;
            dateNgaySinh.Enabled = true;
            cboGioiTinh.Enabled = true;
            cboDacQuyen.Enabled = true;
            txtSoDienThoai.Enabled = true;
            txtEmail.Enabled = true;
            btnLuu.Enabled = true;
            btnHuy.Enabled = true;
        }

        private void frmQuanLyNguoiDung_Load(object sender, EventArgs e)
        {
            NapListNguoiDung();

            //nạp giới tính
            cboGioiTinh.Properties.Items.AddRange(new string[] { "Nam", "Nữ" });

            //nạp đặc quyền
            cboDacQuyen.Properties.Columns.Add(new LookUpColumnInfo("TenDacQuyen"));
            cboDacQuyen.Properties.DataSource = BUS.DacQuyen.Table;
            cboDacQuyen.Properties.DisplayMember = "TenDacQuyen";
            cboDacQuyen.Properties.ValueMember = "MaDacQuyen";

            //khóa control
            KhoaControl();
            lblTongCong.Text = string.Format("Tổng cộng: {0} người dùng", lsNguoiDung.Items.Count);
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            MoKhoaControl();
            isInsert = true;

            NapGiaoDien();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            MoKhoaControl();
            txtTenDangNhap.Enabled = false;
            isInsert = false;
        }

        private void lsNguoiDung_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lsNguoiDung.SelectedValue != null)
            {
                DataRow r = BUS.NguoiDung.Table.Rows.Find(lsNguoiDung.SelectedValue);
                nd = new DataTransfer.NguoiDung(r["MaNguoiDung"].ToString(), r["TenDangNhap"].ToString(),
                    r["MatKhau"].ToString(), r["HoTen"].ToString(), r["GioiTinh"].ToString(), r["MaDacQuyen"].ToString(),
                    (DateTime)r["NgaySinh"], r["SoDienThoai"].ToString(), r["Email"].ToString());

                NapGiaoDien(nd);
                KhoaControl();
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            lsNguoiDung_SelectedIndexChanged(sender, e);
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                nd = NapNguoiDung();
                if (isInsert)
                {
                    if (BUS.NguoiDung.Them(nd))
                    {
                        XtraMessageBox.Show("Thêm người dùng " + nd.HoTen + " thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        NapListNguoiDung();
                        lsNguoiDung.SelectedValue = nd.MaNhanVien;
                    }
                    else
                        XtraMessageBox.Show("Thêm người dùng " + nd.HoTen + " không thành công", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (BUS.NguoiDung.Sua(nd))
                        XtraMessageBox.Show("Cập nhật thông tin người dùng " + nd.HoTen + " thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        XtraMessageBox.Show("Cập nhật thông tin người dùng " + nd.HoTen + " không thành công", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                lblTongCong.Text = string.Format("Tổng cộng: {0} người dùng", lsNguoiDung.Items.Count);
            }
            catch (ArgumentException ex)
            {
                XtraMessageBox.Show(ex.Message, "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void lsNguoiDung_DoubleClick(object sender, EventArgs e)
        {
            btnSua_Click(sender, e);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (nd.MaNhanVien != Settings.Default.User_CurrentUser)
            {
                DialogResult dlgrs = XtraMessageBox.Show("Bạn có muốn xóa người dùng " + nd.HoTen + " không?" + Environment.NewLine +
                "Điều này sẽ xóa tất cả các Hóa đơn đặt hàng, Hóa đơn bán hàng, Phiếu thu, Phiếu chi, Phiếu nhập, Phiếu xuất và Báo giá của người dùng này?",
                "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                try
                {
                    if (dlgrs == DialogResult.Yes && BUS.NguoiDung.Xoa(nd.MaNhanVien))
                    {
                        XtraMessageBox.Show("Xóa người dùng thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        lsNguoiDung.Items.Remove(lsNguoiDung.SelectedItem);
                    }
                }
                catch
                {
                    XtraMessageBox.Show("Xóa người dùng không thành công", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
                XtraMessageBox.Show("Bạn không thể xóa chính mình", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);


        }
    }

}