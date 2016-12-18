using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransfer
{
    public class KhachHang  
    {
        #region Fields
        private string _maKhachHang;
        private string _hoTen;
        private string _gioiTinh;
        private string _diaChi;
        private string _soDienThoai;
        private string _email;
        #endregion

        #region Properties
        public string MaKhachHang
        {
            get { return _maKhachHang; }
            set { _maKhachHang = value; }
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

       
        public string DiaChi
        {
            get { return _diaChi; }
            set { _diaChi = value; }
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
        public KhachHang(string maKhachHang, string hoTen, string gioiTinh, string diaChi, string soDienThoai, string email)
        {
            MaKhachHang = maKhachHang;
            HoTen = hoTen;
            GioiTinh = gioiTinh;
            DiaChi = diaChi;
            SoDienThoai = soDienThoai;
            Email = email;
        }
        #endregion
    }
}
