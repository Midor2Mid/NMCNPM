//Written by @sontq
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransfer
{
    public class LoaiSanPham
    {
        #region Fields
        private string _maLoai;
        private string _tenLoai;
        #endregion

        #region Properties
        public string MaLoai
        {
            get { return _maLoai; }
            set { _maLoai = value; }
        }

        public string TenLoai
        {
            get { return _tenLoai; }
            set { _tenLoai = value; }
        }
        #endregion

        #region Methods
        public LoaiSanPham(string maLoai, string tenLoai)
        {
            MaLoai = maLoai;
            TenLoai = tenLoai;
        }
        #endregion
    }
}
