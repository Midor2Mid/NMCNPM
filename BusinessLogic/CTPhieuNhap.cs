using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class CTPhieuNhap
    {
        private DataAccess.CTPhieuNhap access = new DataAccess.CTPhieuNhap();

        public DataTable Table
        {
            get { return access.Table; }
            set { access.Table = value; }
        }
        public DataTable SelectAll()
        {
            return access.SelectAll();
        }
        public bool Them(DataTransfer.CTPhieuNhap ctpn)
        {
            return access.Them(ctpn);
        }
        public bool Xoa(string maPhieu)
        {
            return access.Xoa(maPhieu);
        }
        public bool Sua(DataTransfer.CTPhieuNhap ctpn)
        {
            return access.Sua(ctpn);
        }
       
    }
}
