//Written by @phungnlg

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransfer
{
    public class HoaDonBanHang
    {
        #region Fields
        private string _maHoaDon;
        private string _maKhachHang;
        private string _maNhanVien;
        private DateTime _ngayLap;
        private double _thanhTien;
        private bool _daThu;
        private string _ghiChu;
        #endregion

        #region Properties
        public string MaHoaDon
        {
            get { return _maHoaDon; }
            set { _maHoaDon = value; }
        }
        public string MaKhachHang
        {
            get { return _maKhachHang; }
            set { _maKhachHang = value; }
        }
        public string MaNhanVien
        {
            get { return _maNhanVien; }
            set { _maNhanVien = value; }
        }
        public DateTime NgayLap
        {
            get { return _ngayLap; }
            set { _ngayLap = value; }
        }
        
        public double ThanhTien
        {
            get { return _thanhTien; }
            set { _thanhTien = value; }
        }
        public bool DaThu
        {
            get { return _daThu; }
            set { _daThu = value; }
        }
        public string GhiChu
        {
            get { return _ghiChu; }
            set { _ghiChu = value; }
        }
        #endregion

        #region Methods
        public HoaDonBanHang(string maHoaDon, string maKhachHang, string maNhanVien,
            DateTime ngayLap, double thanhTien, bool daThu, string ghiChu)
        {
            MaHoaDon = maHoaDon;
            MaKhachHang = maKhachHang;
            MaNhanVien = maNhanVien;
            NgayLap = ngayLap;
            ThanhTien = thanhTien;
            DaThu = daThu;
            GhiChu = ghiChu;
        }
        #endregion
    }
}
