using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace BusinessLogic
{
    public class DacQuyen
    {
        private DataAccess.DacQuyen access = new DataAccess.DacQuyen();

        public DataTable Table
        {
            get { return access.Table; }
            set { access.Table = value; }
        }
        public DataTable SelectAll()
        {
            return access.SelectAll();
        }
        public bool Them(DataTransfer.DacQuyen dq)
        {
            return access.Them(dq);
        }
        public bool Xoa(string maDacQuyen)
        {
            return access.Xoa(maDacQuyen);
        }
        public bool Sua(DataTransfer.DacQuyen dq)
        {
            return access.Sua(dq);
        }
        public string AutoGenerateID()
        {
            //mã định danh
            string id = "DQ";

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
