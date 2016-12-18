using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class CTHoaDonBanHang
    {
        private DataAccess.CTHoaDonBanHang access = new DataAccess.CTHoaDonBanHang();

        public DataTable Table
        {
            get { return access.Table; }
            set { access.Table = value; }
        }
        public DataTable SelectAll()
        {
            return access.SelectAll();
        }
        public bool Them(DataTransfer.CTHoaDonBanHang cthd)
        {
            return access.Them(cthd);
        }
        public bool Xoa(string maHoaDon, string maSanPham)
        {
            return access.Xoa(maHoaDon, maSanPham);
        }
        public bool Sua(DataTransfer.CTHoaDonBanHang cthd)
        {
            return access.Sua(cthd);
        }
    }

}
