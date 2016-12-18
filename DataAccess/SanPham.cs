using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class SanPham : DBConnect
    {
        private DataTable dt;

        
        public DataTable Table
        {
            get { return dt; }
            set { dt = value; }
        }
        public SanPham()
        {
            //nạp bảng lên
            dt = SelectAll();

            //đặt khóa chính
            dt.PrimaryKey = new DataColumn[] { dt.Columns[0] };
        }
        public DataTable SelectAll()
        {
            return GetData("select * from SANPHAM");
        }
        public bool Them(DataTransfer.SanPham sp)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("select * from SANPHAM", Connection);

                //Tạo dòng mới và chèn dữ liệu vào
                DataRow r = dt.NewRow();
                r["MaSanPham"] = sp.MaSanPham;
                r["TenSanPham"] = sp.TenSanPham;
                r["GiaNhap"] = sp.GiaNhap;
                r["GiaBan"] = sp.GiaBan;
                r["MaLoai"] = sp.MaLoai;
                r["TonKho"] = sp.TonKho;
                
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
        public bool Xoa(string maSanPham)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("select * from SANPHAM", Connection);

                //tìm dòng
                DataRow r = dt.Rows.Find(maSanPham);

                //Kiểm tra khóa ngoại
                DataAccess.CTHoaDonBanHang Ctbh = new DataAccess.CTHoaDonBanHang();
                DataRow[] ctbh_row = Ctbh.Table.Select("MaSanPham like '" + maSanPham + "'");
                foreach (DataRow item in ctbh_row)
                    Ctbh.Xoa(item["MaHoaDon"].ToString(), item["MaSanPham"].ToString());
              
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
        public bool Sua(DataTransfer.SanPham sp)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("select * from SANPHAM", Connection);

                //tìm dòng
                DataRow r = dt.Rows.Find(sp.MaSanPham);

                //cập nhật dòng
                if (r != null)
                {
                    r["MaSanPham"] = sp.MaSanPham;
                    r["TenSanPham"] = sp.TenSanPham;
                    r["GiaNhap"] = sp.GiaNhap;
                    r["GiaBan"] = sp.GiaBan;
                    r["MaLoai"] = sp.MaLoai;
                    r["TonKho"] = sp.TonKho;                   
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
