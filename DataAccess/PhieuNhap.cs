using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class PhieuNhap : DBConnect
    {
        private DataTable dt;

        public DataTable Table
        {
            get { return dt; }
            set { dt = value; }
        }
        public PhieuNhap()
        {
            //nạp bảng lên
            dt = SelectAll();

            //đặt khóa chính
            dt.PrimaryKey = new DataColumn[] { dt.Columns[0] };
        }
        public DataTable SelectAll()
        {
            return GetData("select * from PHIEUNHAP");
        }
        public bool Them(DataTransfer.PhieuNhap pn)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("select * from PHIEUNHAP", Connection);

                //tạo dòng mới và chèn dữ liệu vào
                DataRow r = dt.NewRow();
                r["MaPhieu"] = pn.MaPhieu;
                r["MaNhanVien"] = pn.MaNhanVien;
                r["NgayLap"] = pn.NgayLap;
                r["TongTien"] = pn.TongTien;
                r["GhiChu"] = pn.GhiChu;

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
        public bool Xoa(string maPhieu)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("select * from PHIEUNHAP", Connection);

                //tìm dòng
                DataRow r = dt.Rows.Find(maPhieu);



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
        public bool Sua(DataTransfer.PhieuNhap pn)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("select * from PHEUNHAP", Connection);

                //tìm dòng
                DataRow r = dt.Rows.Find(pn.MaPhieu);

                //cập nhật dòng
                if (r != null)
                {

                    r["MaPhieu"] = pn.MaPhieu;
                    r["MaNhanVien"] = pn.MaNhanVien;
                    r["NgayLap"] = pn.NgayLap;
                    r["TongTien"] = pn.TongTien;
                    r["GhiChu"] = pn.GhiChu;
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
