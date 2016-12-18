//Written by @sontq
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class LoaiSanPham : DBConnect
    {
        private DataTable dt;


        public DataTable Table
        {
            get { return dt; }
            set { dt = value; }
        }
        public LoaiSanPham()
        {
            //nạp bảng lên
            dt = SelectAll();

            //đặt khóa chính
            dt.PrimaryKey = new DataColumn[] { dt.Columns[0] };
        }
        public DataTable SelectAll()
        {
            return GetData("select * from LOAISANPHAM");
        }
        public bool Them(DataTransfer.LoaiSanPham ls)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("select * from LOAISANPHAM", Connection);

                //Tạo dòng mới và chèn dữ liệu vào
                DataRow r = dt.NewRow();
                r["MaLoai"] = ls.MaLoai;
                r["TenLoai"] = ls.TenLoai;
                dt.Rows.Add(r);

                //Cập nhật vào cơ sở dữ liệu
                SqlCommandBuilder cm = new SqlCommandBuilder(da);
                da.Update(dt);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool Xoa(string maLoai)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("select * from LOAISANPHAM", Connection);

                //tìm dòng
                DataRow r = dt.Rows.Find(maLoai);

                //Kiểm tra khóa ngoại
                DataAccess.CTHoaDonBanHang Ctbh = new DataAccess.CTHoaDonBanHang();
                DataRow[] ctbh_row = Ctbh.Table.Select("MaLoai like '" + maLoai + "'");
                foreach (DataRow item in ctbh_row)
                    Ctbh.Xoa(item["MaHoaDon"].ToString(), item["MaLoai"].ToString());

                //DataAccess.CTPhieuNhap Ctpn = new DataAccess.CTPhieuNhap();
                //DataRow[] ctpn_row = Ctpn.Table.Select("MaSanPham like '" + maSanPham + "'");
                //foreach (DataRow item in ctpn_row)
                //    Ctpn.Xoa(item["MaPhieu"].ToString(), item["MaSanPham"].ToString());


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
        public bool Sua(DataTransfer.LoaiSanPham ls)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("select * from LOAISANPHAM", Connection);

                //tìm dòng
                DataRow r = dt.Rows.Find(ls.MaLoai);

                //cập nhật dòng
                if (r != null)
                {
                    r["MaLoai"] = ls.MaLoai;
                    r["TenLoai"] = ls.TenLoai;

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
