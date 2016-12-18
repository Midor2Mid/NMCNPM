using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLNS
{
    public static class BUS
    {
        #region Fields
        private static BusinessLogic.HoaDonBanHang _hoadon = null;
        private static BusinessLogic.KhachHang _khachhang = null;
        private static BusinessLogic.CTHoaDonBanHang _cthd = null;
        private static BusinessLogic.SanPham _sp = null;
        private static BusinessLogic.NguoiDung _nd = null;
        private static BusinessLogic.LoaiSanPham _ls = null;
        private static BusinessLogic.PhieuNhap _pn = null;
        private static BusinessLogic.CTPhieuNhap _ctpn = null;
        private static BusinessLogic.DacQuyen _dq=null;
        #endregion

        #region Properties
        public static BusinessLogic.HoaDonBanHang HoaDonBanHang
        {
            get { return _hoadon; }
            set { _hoadon = value; }
        }
        public static BusinessLogic.KhachHang KhachHang
        {
            get { return _khachhang; }
            set { _khachhang = value; }
        }
        public static BusinessLogic.CTHoaDonBanHang CTHoaDonBanHang
        {
            get { return _cthd; }
            set { _cthd = value; }
        }
        public static BusinessLogic.SanPham SanPham
        {
            get { return _sp; }
            set { _sp = value; }
        }
        public static BusinessLogic.NguoiDung NguoiDung
        {
            get { return _nd; }
            set { _nd = value; }
        }
        public static BusinessLogic.LoaiSanPham LoaiSanPham
        {
            get { return _ls; }
            set { _ls = value; }
        }
        public static BusinessLogic.PhieuNhap PhieuNhap
        {
            get { return _pn; }
            set { _pn = value; }
        }
        public static BusinessLogic.CTPhieuNhap CTPhieuNhap
        {
            get { return _ctpn; }
            set { _ctpn = value; }
        }
        public static BusinessLogic.DacQuyen DacQuyen
        {
            get { return _dq; }
            set { _dq = value; }
        }
        #endregion
    }
}
