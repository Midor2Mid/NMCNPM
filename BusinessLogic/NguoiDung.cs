using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace BusinessLogic
{
    public class NguoiDung
    {
        private DataAccess.NguoiDung access = new DataAccess.NguoiDung();

        public DataTable Table
        {
            get { return access.Table; }
            set { access.Table = value; }
        }
        public DataTable SelectAll()
        {
            return access.SelectAll();
        }
        public bool Them(DataTransfer.NguoiDung nd)
        {
            return access.Them(nd);
        }
        public bool Xoa(string maNhanVien)
        {
            return access.Xoa(maNhanVien);
        }
        public bool Sua(DataTransfer.NguoiDung nd)
        {
            return access.Sua(nd);
        }
        public bool DangNhap(string name, string pass)
        {
            return access.DangNhap(name, pass);
        }
        public string AutoGenerateID()
        {
            //mã định danh
            string id = "ND";

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
