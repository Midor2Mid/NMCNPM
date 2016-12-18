using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class CTHoaDonBanHang : DBConnect
    {
        private DataTable dt;

        public DataTable Table
        {
            get { return dt; }
            set { dt = value; }
        }
        public CTHoaDonBanHang()
        {
            //nạp bảng lên
            dt = SelectAll();

            //đặt khóa chính
            dt.PrimaryKey = new DataColumn[] { dt.Columns[0], dt.Columns[1] };
        }
        public DataTable SelectAll()
        {
            return GetData("select * from CTHOADONBANHANG");
        }
        public bool Them(DataTransfer.CTHoaDonBanHang cthd)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("select * from CTHOADONBANHANG", Connection);

                //tạo dòng mới và chèn dữ liệu vào
                DataRow r = dt.NewRow();
                r["MaHoaDon"] = cthd.MaHoaDon;
                r["MaSanPham"] = cthd.MaSanPham;
                r["SoLuong"] = cthd.SoLuong;

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
        public bool Xoa(string maHoaDon, string maSanPham)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("select * from CTHOADONBANHANG", Connection);

                //tìm dòng
                DataRow r = dt.Rows.Find(new object[] { maHoaDon, maSanPham });

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
        public bool Sua(DataTransfer.CTHoaDonBanHang cthd)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("select * from CTHOADONBANHANG", Connection);

                //tìm dòng
                DataRow r = dt.Rows.Find(new object[] { cthd.MaHoaDon, cthd.MaSanPham });

                //cập nhật dòng
                if (r != null)
                {
                    r["MaHoaDon"] = cthd.MaHoaDon;
                    r["MaSanPham"] = cthd.MaSanPham;
                    r["SoLuong"] = cthd.SoLuong;
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
