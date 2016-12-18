//Written by @sontq
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class LoaiSanPham
    {
        private DataAccess.LoaiSanPham access = new DataAccess.LoaiSanPham();

        public DataTable Table
        {
            get { return access.Table; }
            set { access.Table = value; }
        }

        public DataTable SelectAll()
        {
            return access.SelectAll();
        }

        public bool Them(DataTransfer.LoaiSanPham ls)
        {
            return access.Them(ls);
        }
        public bool Xoa(string maSanPham)
        {
            return access.Xoa(maSanPham);
        }
        public bool Sua(DataTransfer.LoaiSanPham ls)
        {
            return access.Sua(ls);
        }
        public string AutoGenerateID()
        {
            //mã định danh
            string id = "LS";

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
