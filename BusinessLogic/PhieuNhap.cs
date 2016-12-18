using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
   public class PhieuNhap
    {
        private DataAccess.PhieuNhap access = new DataAccess.PhieuNhap();

        public DataTable Table
        {
            get { return access.Table; }
            set { access.Table = value; }
        }
        public DataTable SelectAll()
        {
            return access.SelectAll();
        }
        public bool Them(DataTransfer.PhieuNhap pn)
        {
            return access.Them(pn);
        }
        public bool Xoa(string maPhieu)
        {
            return access.Xoa(maPhieu);
        }
        public bool Sua(DataTransfer.PhieuNhap pn)
        {
            return access.Sua(pn);
        }
        public string AutoGenerateID()
        {
            //mã định danh
            string id = "PN";

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
