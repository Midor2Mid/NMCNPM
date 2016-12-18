using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class SanPham
    {
        private DataAccess.SanPham access = new DataAccess.SanPham();

        public DataTable Table
        {
            get { return access.Table; }
            set { access.Table = value; }
        }

        public DataTable SelectAll()
        {
            return access.SelectAll();
        }

        public bool Them(DataTransfer.SanPham sp)
        {
            return access.Them(sp);
        }
        public bool Xoa(string maSanPham)
        {
            return access.Xoa(maSanPham);
        }
        public bool Sua(DataTransfer.SanPham sp)
        {
            return access.Sua(sp);
        }
        public string AutoGenerateID()
        {
            //mã định danh
            string id = "SP";

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
