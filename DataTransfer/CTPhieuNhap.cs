using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransfer
{
    public class CTPhieuNhap
    {
         #region Fields
        private string _maPhieu;
        private string _maSanPham;
        private double _soLuong;
        
        #endregion

        #region Properties
        public string MaPhieu
        {
            get { return _maPhieu; }
            set { _maPhieu = value; }
        }

        public string MaSanPham
        {
            get { return _maSanPham; }
            set { _maSanPham = value; }
        }

       
        public double SoLuong
        {
            get { return _soLuong; }
            set { _soLuong = value; }
        }

       
       
        #endregion

        #region Methods
        public CTPhieuNhap(string maPhieu, string maSanPham, double soLuong)
        {
            MaPhieu = maPhieu;
            MaSanPham = maSanPham;
            SoLuong = soLuong;
           
        }
        #endregion
    }
}
