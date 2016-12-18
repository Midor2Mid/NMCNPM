using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class HoaDonBanHang
    {
        private DataAccess.HoaDonBanHang access = new DataAccess.HoaDonBanHang();

        public DataTable Table
        {
            get { return access.Table; }
            set { access.Table = value; }
        }
        public DataTable SelectAll()
        {
            return access.SelectAll();
        }
        public bool Them(DataTransfer.HoaDonBanHang hd)
        {
            return access.Them(hd);
        }
        public bool Xoa(string maHoaDon)
        {
            return access.Xoa(maHoaDon);
        }
        public bool Sua(DataTransfer.HoaDonBanHang hd)
        {
            return access.Sua(hd);
        }
        public string AutoGenerateID()
        {
            //mã định danh
            string id = "HD";

            //mã ngẫu nhiên đã tồn tại chưa
            bool isExist;

            do
            {
                string result = id + access.AutoGenerateNumber();
                isExist = Table.Rows.Find(result) != null;
                if (!isExist)
                    return result;
            }
            while (isExist);
            return null;
        }
    }
}
