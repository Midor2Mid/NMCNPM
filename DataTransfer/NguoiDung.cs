using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransfer
{
    public class NguoiDung
    {
        #region Fields
        private string _maNhanVien;
        private string _tenDangNhap;
        private string _matKhau;
        private string _hoTen;
        private string _gioiTinh;
        private string _maDacQuyen;
        private DateTime _ngaySinh;
        private string _soDienThoai;
        private string _email;
        #endregion

        #region Properties
        public string MaNhanVien
        {
            get { return _maNhanVien; }
            set { _maNhanVien = value; }
        }

        public string TenDangNhap
        {
            get { return _tenDangNhap; }
            set { _tenDangNhap = value; }
        }


        public string MatKhau
        {
            get { return _matKhau; }
            set { _matKhau = value; }
        }


        public string HoTen
        {
            get { return _hoTen; }
            set { _hoTen = value; }
        }


        public string GioiTinh
        {
            get { return _gioiTinh; }
            set { _gioiTinh = value; }
        }

        public string MaDacQuyen
        {
            get { return _maDacQuyen; }
            set { _maDacQuyen = value; }
        }

        public DateTime NgaySinh
        {
            get { return _ngaySinh; }
            set { _ngaySinh = value; }
        }

        public string SoDienThoai
        {
            get { return _soDienThoai; }
            set { _soDienThoai = value; }
        }

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }
        #endregion

        #region Methods
        public NguoiDung(string maNguoiDung, string tenDangNhap, string matKhau, string hoTen, string gioiTinh
            , string maDacQuyen, DateTime ngaySinh, string soDienThoai, string email)
        {
            MaNhanVien = maNguoiDung;
            TenDangNhap = tenDangNhap;
            MatKhau = matKhau;
            HoTen = hoTen;
            GioiTinh = gioiTinh;
            MaDacQuyen = maDacQuyen;
            NgaySinh = ngaySinh;
            SoDienThoai = soDienThoai;
            Email = email;
        }
        #endregion
    }
}
