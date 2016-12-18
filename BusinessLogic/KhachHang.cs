using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class KhachHang
    {
        private DataAccess.KhachHang access = new DataAccess.KhachHang();

        public DataTable Table
        {
            get { return access.Table; }
            set { access.Table = value; }
        }
        public DataTable SelectAll()
        {
            return access.SelectAll();
        }
        public bool Them(DataTransfer.KhachHang kh)
        {
            return access.Them(kh);
        }
        public bool Xoa(string maKhachHang)
        {
            return access.Xoa(maKhachHang);
        }
        public bool Sua(DataTransfer.KhachHang kh)
        {
            return access.Sua(kh);
        }
        public string AutoGenerateID()
        {
            //mã định danh
            string id = "KH";

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
