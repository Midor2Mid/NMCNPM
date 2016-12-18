using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess
{
    public class DacQuyen : DBConnect
    {
        private DataTable dt;
        public DataTable Table
        {
            get { return dt; }
            set { dt = value; }
        }
        public DacQuyen()
        {
            //nạp bảng lên
            dt = SelectAll();

            //đặt khóa chính
            dt.PrimaryKey = new DataColumn[] { dt.Columns[0] };
        }
        public DataTable SelectAll()
        {
            return GetData("select * from DACQUYEN");
        }
        public bool Them(DataTransfer.DacQuyen dq)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("select * from DACQUYEN", Connection);

                //tạo dòng mới và chèn dữ liệu vào
                DataRow r = dt.NewRow();
                r["MaDacQuyen"] = dq.MaDacQuyen;
                r["TenDacQuyen"] = dq.TenDacQuyen;
                dt.Rows.Add(r);

                //cập nhật vào CSDL
                SqlCommandBuilder cm = new SqlCommandBuilder(da);
                da.Update(dt);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool Xoa(string maDacQuyen)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("select * from DACQUYEN", Connection);

                //tìm dòng
                DataRow r = dt.Rows.Find(maDacQuyen);

                //tìm và xóa tất cả khóa ngoại
                DataAccess.NguoiDung nd = new NguoiDung();
                DataRow[] nd_row = nd.Table.Select("MaDacQuyen like '" + maDacQuyen + "'");
                foreach (DataRow item in nd_row)
                {
                    nd.Xoa(item["MaNguoiDung"].ToString());
                }

                //xóa dòng
                if (r != null)
                    r.Delete();

                //cập nhật vào CSDL
                SqlCommandBuilder cm = new SqlCommandBuilder(da);
                da.Update(dt);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool Sua(DataTransfer.DacQuyen dq)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("select * from DACQUYEN", Connection);

                //tìm dòng
                DataRow r = dt.Rows.Find(dq.MaDacQuyen);

                //cập nhật dòng
                if (r != null)
                {
                    r["MaDacQuyen"] = dq.MaDacQuyen;
                    r["TenDacQuyen"] = dq.TenDacQuyen;
                }

                //cập nhật vào CSDL
                SqlCommandBuilder cm = new SqlCommandBuilder(da);
                da.Update(dt);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
