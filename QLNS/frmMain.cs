using QLNS.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QLNS
{
    public partial class frmMain : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public frmMain()
        {
            frmDangNhap dangNhap = new frmDangNhap();
            dangNhap.ShowDialog();

            currentUser = dangNhap.NguoiDung;

            InitializeComponent();
            Settings.Default.User_CurrentUser = currentUser != null ? currentUser.MaNhanVien : null;
        }
        private DataTransfer.NguoiDung currentUser;
        private void timer_Tick(object sender, EventArgs e)
        {
            lblTimeNow.Caption = DateTime.Now.ToString("HH:mm:ss tt");
            lblDate.Caption = DateTime.Now.ToString("dd/MM/yyyy");
        }
        public Form IsActive(Type type)
        {
            foreach (var f in this.MdiChildren)
            {
                if (f.GetType() == type)
                    return f;
            }
            return null;
        }
        private void btnLapHoaDonMoi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form lapHD = IsActive(typeof(frmLapHoaDonBanHang));

            if (lapHD != null)
                lapHD.Activate();
            else
            {
                lapHD = new frmLapHoaDonBanHang();
                lapHD.MdiParent = this;
                lapHD.Show();
            }
        }

        private void btnXemCacHoaDon_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form lapHD = IsActive(typeof(frmHoaDonBanHang));

            if (lapHD != null)
                lapHD.Activate();
            else
            {
                lapHD = new frmHoaDonBanHang();
                lapHD.MdiParent = this;
                lapHD.Show();
            }
        }
        
        private void frmMain_Load(object sender, EventArgs e)
        {
            lblTenNhaSach.Caption = "NPSH Bookstore";
            timer.Start();
        }

        private void btnSaoLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form saoLuu = IsActive(typeof(frmSaoLuu));

            if (saoLuu != null)
                saoLuu.Activate();
            else
            {
                saoLuu = new frmSaoLuu();
                saoLuu.MdiParent = this;
                saoLuu.Show();
            }
        }

        private void btnPhucHoi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form phucHoi = IsActive(typeof(frmPhucHoi));

            if (phucHoi != null)
                phucHoi.Activate();
            else
            {
                phucHoi = new frmPhucHoi();
                phucHoi.MdiParent = this;
                phucHoi.Show();
            }
        }

        private void btnNguoiDung_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form NguoiDung = IsActive(typeof(frmNguoiDung));

            if (NguoiDung != null)
                NguoiDung.Activate();
            else
            {
                NguoiDung = new frmNguoiDung();
                NguoiDung.MdiParent = this;
                NguoiDung.Show();
            }
        }

        private void btnCaiDat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form CaiDat = IsActive(typeof(frmCaiDat));

            if (CaiDat != null)
                CaiDat.Activate();
            else
            {
                CaiDat = new frmCaiDat();
                CaiDat.MdiParent = this;
                CaiDat.Show();
            }
        }

        private void btnDangXuat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Hide();
            frmDangNhap dangNhap = new frmDangNhap();
            dangNhap.ShowDialog();

            currentUser = dangNhap.NguoiDung;
            Settings.Default.User_CurrentUser = currentUser != null ? currentUser.MaNhanVien : null;
            this.Show();
        }

        private void btnDanhSachKH_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form KhachHang = IsActive(typeof(frmKhachHang));

            if (KhachHang != null)
                KhachHang.Activate();
            else
            {
                KhachHang = new frmKhachHang();
                KhachHang.MdiParent = this;
                KhachHang.Show();
            }
        }

        private void btnSach_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form Sach = IsActive(typeof(frmSach));

            if (Sach != null)
                Sach.Activate();
            else
            {
                Sach = new frmSach();
                Sach.MdiParent = this;
                Sach.Show();
            }
        }

        private void btnLapPhieuNhapKho_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form lapPhieuNhapKho = IsActive(typeof(frmLapPhieuNhapKho));

            if (lapPhieuNhapKho != null)
                lapPhieuNhapKho.Activate();
            else
            {
                lapPhieuNhapKho = new frmLapPhieuNhapKho();
                lapPhieuNhapKho.MdiParent = this;
                lapPhieuNhapKho.Show();
            }
        }

        private void btnXemTinhTrang_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form xemTinhTrang = IsActive(typeof(frmXemTinhTrang));

            if (xemTinhTrang != null)
                xemTinhTrang.Activate();
            else
            {
                xemTinhTrang = new frmXemTinhTrang();
                xemTinhTrang.MdiParent = this;
                xemTinhTrang.Show();
            }
        }

        private void barButtonItem11_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmThemKH frm = new frmThemKH();
            frm.ShowDialog();
        }

        private void barButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmThemSach frm = new frmThemSach();
            frm.ShowDialog();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form LapPhieuThuTien = IsActive(typeof(frmLapPhieuThuTien));

            if (LapPhieuThuTien != null)
                LapPhieuThuTien.Activate();
            else
            {
                LapPhieuThuTien = new frmLapPhieuThuTien(currentUser);
                LapPhieuThuTien.MdiParent = this;
                LapPhieuThuTien.Show();
            }
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmThayDoiQuyDinh ThayDoiQuyDinh = new frmThayDoiQuyDinh();
            ThayDoiQuyDinh.ShowDialog();
        }
    }
}
