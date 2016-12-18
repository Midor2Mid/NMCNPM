using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransfer
{
   public class PhieuNhap
    {
        #region Fields
        private string _maPhieu;
        private string _maNhanVien;
        private DateTime _ngayLap;
        private double _tongTien;
        private string _ghiChu;
       
        #endregion

        #region Properties
        public string MaPhieu
        {
            get { return _maPhieu; }
            set { _maPhieu = value; }
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

       
        public double TongTien
        {
            get { return _tongTien; }
            set { _tongTien = value; }
        }

        
        public string GhiChu
        {
            get { return _ghiChu; }
            set { _ghiChu = value; }
        }

        
        
        #endregion

        #region Methods
        public PhieuNhap(string maPhieu, string maNhanVien, DateTime ngayLap, double tongTien, string ghiChu)
        {
            MaPhieu = maPhieu;
            MaNhanVien = MaNhanVien;
            NgayLap = ngayLap;
            TongTien = tongTien;
            GhiChu = ghiChu;
            
        }
        #endregion
    }
}
