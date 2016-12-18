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

namespace QLNS
{
    public partial class frmLapPhieuThuTien : DevExpress.XtraEditors.XtraForm
    {       
        public frmLapPhieuThuTien(DataTransfer.NguoiDung currUser = null)
        {
            InitializeComponent();
            KhoiTaoCacBUSCanThiet();
            currentUser = currUser;
        }

        private bool IsInsert = false;
        private bool checkGrid_Click = false;
        private DataTransfer.NguoiDung currentUser;

        public void NapGiaoDienPhanQuyen(DataTransfer.NguoiDung nd)
        {
            //quyền Khách
            if (nd == null)
            {
                btnThem.Enabled = false;
                btnSua.Enabled = false;
                btnXoa.Enabled = false;
            }
        }

        public void KhoiTaoCacBUSCanThiet()
        {
            if (BUS.LoaiSanPham == null)
                BUS.LoaiSanPham = new BusinessLogic.LoaiSanPham();
        }


        void LockControl() // Khoa cac control
        {
            txtMaPhieu.ReadOnly = true;
            txtNhanVien.ReadOnly = true;
            txtKhachHang.ReadOnly = true;
            speSoTien.ReadOnly = true;
            edtNgayLap.ReadOnly = true;

            btnThem.Enabled = true;
            btnLuu.Enabled = false;
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
            btnHuy.Enabled = false;
        }

        void UnLockControl() // Mo khoa control
        {
            txtNhanVien.ReadOnly = true;
            txtMaPhieu.ReadOnly = true;
            txtKhachHang.ReadOnly = false;
            speSoTien.ReadOnly = false;
            edtNgayLap.ReadOnly = false;

            btnThem.Enabled = false;
            btnLuu.Enabled = true;
            btnHuy.Enabled = true;
            btnXoa.Enabled = false;
            btnSua.Enabled = false;
        }

        void DeleteText() // Xoa dong text
        {
            txtMaPhieu.Text = string.Empty;
            txtNhanVien.Text = string.Empty;
            txtKhachHang.Text = string.Empty;
            speSoTien.Text = string.Empty;
            edtNgayLap.Text = string.Empty;
        }


        void ShowScreen() // refresh lai man hinh
        {

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            UnLockControl();
            DeleteText();
            IsInsert = true;
            txtMaPhieu.Text = BUS.LoaiSanPham.AutoGenerateID();
            txtNhanVien.Text = currentUser.HoTen;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                if (checkGrid_Click)
                {
                    UnLockControl();
                    txtNhanVien.ReadOnly = false;
                    btnLuu.Enabled = true;
                    IsInsert = false;
                }
                else
                    throw new IndexOutOfRangeException();
            }
            catch (IndexOutOfRangeException)
            {
                XtraMessageBox.Show("Chọn phiếu sản phẩm muốn sửa !");
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
            }
            catch
            {
                XtraMessageBox.Show("Chọn loại sản phẩm muốn xóa !");
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {          
        }

        private void msds_Click(object sender, EventArgs e)
        {
         
        }

        private void frmLapPhieuThuTien_Load(object sender, EventArgs e)
        {
            LockControl();
            NapGiaoDienPhanQuyen(currentUser);
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            LockControl();
            DeleteText();
        }
    }
}