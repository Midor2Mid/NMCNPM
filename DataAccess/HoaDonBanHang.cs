using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class HoaDonBanHang : DBConnect
    {
        private DataTable dt;
        public DataTable Table
        {
            get { return dt; }
            set { dt = value; }
        }
        public HoaDonBanHang()
        {
            //nạp bảng lên
            dt = SelectAll();

            //đặt khóa chính
            dt.PrimaryKey = new DataColumn[] { dt.Columns[0] };
        }
        public DataTable SelectAll()
        {
            return GetData("select * from HOADONBANHANG");
        }
        public bool Them(DataTransfer.HoaDonBanHang hd)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("select * from HOADONBANHANG", Connection);

                //tạo dòng mới và chèn dữ liệu vào
                DataRow r = dt.NewRow();
                r["MaHoaDon"] = hd.MaHoaDon;
                r["MaKhachHang"] = hd.MaKhachHang;
                r["MaNhanVien"] = hd.MaNhanVien;
                r["NgayLap"] = hd.NgayLap;
                r["ThanhTien"] = hd.ThanhTien;
                r["DaThu"] = hd.DaThu;
                r["GhiChu"] = hd.GhiChu;
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
        public bool Xoa(string maHoaDon)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("select * from HOADONBANHANG", Connection);

                //tìm dòng
                DataRow r = dt.Rows.Find(maHoaDon);

                //tìm và xóa tất cả khóa ngoại
                DataAccess.CTHoaDonBanHang cthd = new CTHoaDonBanHang();
                DataRow[] cthd_row = cthd.Table.Select("MaHoaDon like '" + maHoaDon + "'");
                foreach (DataRow item in cthd_row)
                {
                    cthd.Xoa(item["MaHoaDon"].ToString(), item["MaSanPham"].ToString());
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
        public bool Sua(DataTransfer.HoaDonBanHang hd)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("select * from HOADONBANHANG", Connection);

                //tìm dòng
                DataRow r = dt.Rows.Find(hd.MaHoaDon);

                //cập nhật dòng
                if (r != null)
                {
                    r["MaHoaDon"] = hd.MaHoaDon;
                    r["MaKhachHang"] = hd.MaKhachHang;
                    r["MaNhanVien"] = hd.MaNhanVien;
                    r["NgayLap"] = hd.NgayLap;                
                    r["ThanhTien"] = hd.ThanhTien;
                    r["DaThu"] = hd.DaThu;
                    r["GhiChu"] = hd.GhiChu;
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
