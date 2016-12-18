using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess
{
    public class NguoiDung : DBConnect
    {
        private DataTable dt;

        public DataTable Table
        {
            get { return dt; }
            set { dt = value; }
        }
        public NguoiDung()
        {
            //nạp bảng lên
            dt = SelectAll();

            //đặt khóa chính
            dt.PrimaryKey = new DataColumn[] { dt.Columns[0] };
        }
        public DataTable SelectAll()
        {
            return GetData("select * from NGUOIDUNG");
        }
        public bool Them(DataTransfer.NguoiDung nd)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("select * from NGUOIDUNG", Connection);

                //tạo dòng mới và chèn dữ liệu vào
                DataRow r = dt.NewRow();
                r["MaNguoiDung"] = nd.MaNhanVien;
                r["TenDangNhap"] = nd.TenDangNhap;
                r["MatKhau"] = nd.MatKhau;
                r["HoTen"] = nd.HoTen;
                r["GioiTinh"] = nd.GioiTinh;
                r["MaDacQuyen"] = nd.MaDacQuyen;
                r["NgaySinh"] = nd.NgaySinh;
                r["SoDienThoai"] = nd.SoDienThoai;
                r["Email"] = nd.Email;
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
        public bool Xoa(string maNguoiDung)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("select * from NGUOIDUNG", Connection);

                //tìm dòng
                DataRow r = dt.Rows.Find(maNguoiDung);

                //tìm và xóa tất cả khóa ngoại
                DataAccess.HoaDonBanHang hdbh = new HoaDonBanHang();
                DataRow[] hdbh_row = hdbh.Table.Select("MaNguoiDung like '" + maNguoiDung + "'");
                foreach (DataRow item in hdbh_row)
                {
                    hdbh.Xoa(item["MaHoaDon"].ToString());
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
        public bool Sua(DataTransfer.NguoiDung nd)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("select * from NGUOIDUNG", Connection);

                //tìm dòng
                DataRow r = dt.Rows.Find(nd.MaNhanVien);

                //cập nhật dòng
                if (r != null)
                {
                    r["MaNguoiDung"] = nd.MaNhanVien;
                    r["TenDangNhap"] = nd.TenDangNhap;
                    r["MatKhau"] = nd.MatKhau;
                    r["HoTen"] = nd.HoTen;
                    r["GioiTinh"] = nd.GioiTinh;
                    r["MaDacQuyen"] = nd.MaDacQuyen;
                    r["NgaySinh"] = nd.NgaySinh;
                    r["SoDienThoai"] = nd.SoDienThoai;
                    r["Email"] = nd.Email;
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
        public bool DangNhap(string name, string pass)
        {
            DataRow[] r = dt.Select(string.Format("TenDangNhap = '{0}'", name));
            return r.Length > 0 && r[0]["MatKhau"].ToString() == pass;
        }
    }
}
