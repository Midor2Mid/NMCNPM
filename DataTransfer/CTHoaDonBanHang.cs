//Written by @phungnlg
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransfer
{
    public class CTHoaDonBanHang
    {
        #region Fields
        private string _maHoaDon;
        private string _maSanPham;
        private int _soLuong;
        #endregion

        #region Properties
        
        public string MaHoaDon
        {
            get { return _maHoaDon; }
            set { _maHoaDon = value; }
        }

        
        public string MaSanPham
        {
            get { return _maSanPham; }
            set { _maSanPham = value; }
        }

        
        public int SoLuong
        {
            get { return _soLuong; }
            set { _soLuong = value; }
        }
        #endregion

        #region Methods
        public CTHoaDonBanHang(string maHoaDon, string maSanPham, int soLuong)
        {
            MaHoaDon = maHoaDon;
            MaSanPham = maSanPham;
            SoLuong = soLuong;
        }
        #endregion
    }
}
