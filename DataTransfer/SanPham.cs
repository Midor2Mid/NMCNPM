using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransfer
{
    public class SanPham
    {
        #region Fields
        private string _maSanPham;
        private string _tenSanPham;
        private double _giaNhap;
        private double _giaBan;
        private string _maLoai;
        private int _tonKho;
        #endregion

        #region Properties
        public string MaSanPham
        {
            get { return _maSanPham; }
            set { _maSanPham = value; }
        }
        public string TenSanPham
        {
            get { return _tenSanPham; }
            set { _tenSanPham = value; }
        }
        public double GiaNhap
        {
            get { return _giaNhap; }
            set { _giaNhap = value; }
        }
        public double GiaBan
        {
            get { return _giaBan; }
            set { _giaBan = value; }
        }
        public string MaLoai
        {
            get { return _maLoai; }
            set { _maLoai = value; }
        }
        public int TonKho
        {
            get { return _tonKho; }
            set { _tonKho = value; }
        }
        #endregion

        #region Methods
        public SanPham(string maSanPham, string tenSanPham, double giaNhap, double giaBan,
            string maLoai, int tonKho)
        {
            MaSanPham = maSanPham;
            TenSanPham = tenSanPham;
            GiaNhap = giaNhap;
            GiaBan = giaBan;
            MaLoai = maLoai;
            TonKho = tonKho;
        }
        #endregion
    }
}
