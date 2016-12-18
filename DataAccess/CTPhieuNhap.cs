using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
   public class CTPhieuNhap:DBConnect
    {
       private DataTable dt;

        public DataTable Table
        {
            get { return dt; }
            set { dt = value; }
        }
        public CTPhieuNhap()
        {
            //nạp bảng lên
            dt = SelectAll();

            //đặt khóa chính
            dt.PrimaryKey = new DataColumn[] { dt.Columns[0] ,dt.Columns[1]};
        }
        public DataTable SelectAll()
        {
            return GetData("select * from CTPhieuNhap");
        }
        public bool Them(DataTransfer.CTPhieuNhap ctpn)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("select * from CTPHIEUNHAP", Connection);

                //tạo dòng mới và chèn dữ liệu vào
                DataRow r = dt.NewRow();
                r["MaPhieu"] = ctpn.MaPhieu;
                r["MaSanPham"] = ctpn.MaSanPham;
                r["SoLuong"] = ctpn.SoLuong;
                
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
                SqlDataAdapter da = new SqlDataAdapter("select * from CTPHIEUNHAP", Connection);

                //tìm dòng
                DataRow r = dt.Rows.Find(maPhieu);

                /*//tìm và xóa tất cả khóa ngoại
                DataAccess.HoaDonBanHang hdbh = new HoaDonBanHang();
                DataRow[] hdbh_row = hdbh.Table.Select("MaKhachHang like '" + maKhachHang + "'");
                foreach (DataRow item in hdbh_row)
                {
                    hdbh.Xoa(item["MaHoaDon"].ToString());
                }*/

                
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
        public bool Sua(DataTransfer.CTPhieuNhap ctpn)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("select * from CTPHIEUNHAP", Connection);

                //tìm dòng
                DataRow r = dt.Rows.Find(ctpn.MaPhieu);

                //cập nhật dòng
                if (r != null)
                {
                    r["MaPhieu"] = ctpn.MaPhieu;
                    r["MaSanPham"] = ctpn.MaSanPham;
                    r["SoLuong"] = ctpn.SoLuong;
                
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
