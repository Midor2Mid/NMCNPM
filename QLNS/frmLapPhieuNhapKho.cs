using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace QLNS
{
    public partial class frmLapPhieuNhapKho : DevExpress.XtraEditors.XtraForm
    {
        private DataTransfer.PhieuNhap pn;
        DataTable tbCTPN = new DataTable();
        private bool isInsert;
        public frmLapPhieuNhapKho()
        {
            InitializeComponent();
            KhoiTaoCacBUSCanThiet();
            isInsert = true;
        }
        public void KhoiTaoCacBUSCanThiet()
        {
            if (BUS.PhieuNhap == null)
                BUS.PhieuNhap = new BusinessLogic.PhieuNhap();
            if (BUS.CTPhieuNhap == null)
                BUS.CTPhieuNhap = new BusinessLogic.CTPhieuNhap();
           if (BUS.SanPham == null)
                BUS.SanPham = new BusinessLogic.SanPham();
        }
        //Nạp giao diện mặc định
        public void NapGiaoDien(DataTransfer.PhieuNhap pn = null)
        {
            if (pn == null)
            {
                pn = new DataTransfer.PhieuNhap(BUS.PhieuNhap.AutoGenerateID(), string.Empty, DateTime.Now, 0, string.Empty);

                NapGiaoDien(pn);
            }
            else
            {
                //load combobox Khách hàng
                /*
                cboMaNhanVien.Properties.Columns.Add(new LookUpColumnInfo("HoTen"));
                cboMaNhanVien.Properties.DataSource = BUS.KhachHang.Table;
                cboMaNhanVien.Properties.DisplayMember = "HoTen";
                cboMaNhanVien.Properties.ValueMember = "MaNhanVien";
                */

                txtMaPhieu.Text = pn.MaPhieu;
                dateNgayLap.EditValue = pn.NgayLap;
                //cboMaNhanVien.EditValue = pn.MaNhanVien;
                txtGhiChu.Text = pn.GhiChu;
                txtTongTien.Text = pn.TongTien.ToString();
                

                tbCTPN.Rows.Clear();
                DataRow[] rows = BUS.CTPhieuNhap.Table.Select("MaPhieu = '" + pn.MaPhieu + "'");
                foreach (DataRow item in rows)
                {
                    DataRow r = tbCTPN.NewRow();
                    DataRow _r = BUS.SanPham.Table.Select("MaSanPham = '" + item["MaSanPham"] + "'")[0];
                    r["MaSanPham"] = item["MaSanPham"];
                    r["TenSanPham"] = _r["TenSanPham"];
                    r["SoLuong"] = item["SoLuong"];
                    r["ThanhTien"] = Convert.ToInt32(r["SoLuong"]) * Convert.ToInt32(_r["GiaNhap"]);

                    tbCTPN.Rows.Add(r);
                }
            }
           // lblTongSanPham.Text = "Tổng cộng: " + BUS.SanPham.Table.Rows.Count + " sản phẩm.";
           // lblTongChonSanPham.Text = "Tổng cộng: " + tbCTHD.Rows.Count + " sản phẩm.";
        }
        public void KhoiTaoTableCTPN()
        {
            tbCTPN.Columns.Add("MaSanPham");
            tbCTPN.Columns.Add("TenSanPham");
            tbCTPN.Columns.Add("SoLuong");
            tbCTPN.Columns.Add("ThanhTien");
            tbCTPN.PrimaryKey = new DataColumn[] { tbCTPN.Columns[0] };
            tbCTPN.RowChanged += TbCTPN_RowChanged;
            tbCTPN.RowDeleted += TbCTPN_RowChanged;
            tbCTPN.TableCleared += TbCTPN_TableCleared;
        }
        private void TbCTPN_TableCleared(object sender, DataTableClearEventArgs e)
        {
           // lblTongChonSanPham.Text = "Tổng cộng: " + tbCTHD.Rows.Count + " sản phẩm.";
        }

        private void TbCTPN_RowChanged(object sender, DataRowChangeEventArgs e)
        {
           // lblTongChonSanPham.Text = "Tổng cộng: " + tbCTHD.Rows.Count + " sản phẩm.";
        }

        public void CapNhatTongTien()
        {
            long tongTien = 0;
            foreach (DataRow item in tbCTPN.Rows)
            {
                tongTien += Convert.ToInt64(item["ThanhTien"]);
            }

            txtTongTien.Text = tongTien.ToString();
        }
        public DataTransfer.PhieuNhap NapPhieuNhap()
        {
            DataTransfer.PhieuNhap pn1 = new DataTransfer.PhieuNhap(txtMaPhieu.Text, "",  dateNgayLap.DateTime, Convert.ToDouble(txtTongTien.Text),txtGhiChu.Text);

            if (string.IsNullOrEmpty(pn1.MaPhieu))
                throw new ArgumentException("Vui lòng nhập đầy đủ thông tin.");
            return pn1;
        }
        private void frmLapPhieuNhapKho_Load(object sender, EventArgs e)
        {
            KhoiTaoTableCTPN();
            gridChonSP.DataSource = BUS.SanPham.Table;
            gridCTPN.DataSource = tbCTPN;
            gridPhieuNhap.DataSource = BUS.PhieuNhap.Table;
            NapGiaoDien(pn);
        }

        private void btnChon_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow r = tbCTPN.NewRow();
                DataRow _r = gridViewChonSP.GetDataRow(gridViewChonSP.FocusedRowHandle);
                r["MaSanPham"] = _r["MaSanPham"];
                r["TenSanPham"] = _r["TenSanPham"];
                r["SoLuong"] = 1;
                r["ThanhTien"] = Convert.ToInt32(r["SoLuong"]) * Convert.ToInt32(_r["GiaNhap"]);

                tbCTPN.Rows.Add(r);
                gridViewCTPN.SelectRow(gridViewCTPN.FindRow(r));
                gridViewCTPN.Focus();
                CapNhatTongTien();
            }
            catch (ConstraintException)
            {
                XtraMessageBox.Show("Bạn đã chọn sản phẩm " + txtSanPham.Text + " rồi!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            {
                XtraMessageBox.Show("Vui lòng chọn một sản phẩm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void txtSoLuong_EditValueChanged(object sender, EventArgs e)
        {
            
        }

        private void btnTang_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow r = gridViewCTPN.GetDataRow(gridViewCTPN.FocusedRowHandle);
                DataRow _r = BUS.SanPham.Table.Rows.Find(r["MaSanPham"]);

                r["SoLuong"] = Convert.ToInt32(r["SoLuong"]) + 1;
                r["ThanhTien"] = Convert.ToInt32(r["SoLuong"]) * Convert.ToInt32(_r["GiaNhap"]);

                CapNhatTongTien();
            }
            catch
            {
                XtraMessageBox.Show("Thay đổi số lượng không hợp lệ!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnGiam_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow r = gridViewCTPN.GetDataRow(gridViewCTPN.FocusedRowHandle);
                DataRow _r = BUS.SanPham.Table.Rows.Find(r["MaSanPham"]);

                int sl = Convert.ToInt32(r["SoLuong"]);
                r["SoLuong"] = sl > 1 ? sl - 1 : sl;
                r["ThanhTien"] = Convert.ToInt32(r["SoLuong"]) * Convert.ToInt32(_r["GiaBan"]);

                CapNhatTongTien();
            }
            catch
            {
                XtraMessageBox.Show("Thay đổi số lượng không hợp lệ!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                tbCTPN.Rows.Remove(gridViewCTPN.GetDataRow(gridViewCTPN.FocusedRowHandle));
                CapNhatTongTien();
            }
            catch
            {
                XtraMessageBox.Show("Không có gì để xóa!", "Thông tin", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnXoaHet_Click(object sender, EventArgs e)
        {
            tbCTPN.Rows.Clear();
            CapNhatTongTien();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            
             if (tbCTPN.Rows.Count > 0)
             {
                 try
                 {
                     //thêm hóa đơn chính
                     this.pn = NapPhieuNhap();
                     if (isInsert)
                     {
                         BUS.PhieuNhap.Them(pn);
                     }
                     else
                     {
                         BUS.PhieuNhap.Sua(pn);
                         DataTransfer.CTPhieuNhap _ct;
                         DataRow[] rows = BUS.CTPhieuNhap.Table.Select("MaPhieu = '" + pn.MaPhieu + "'");
                         foreach (DataRow item in rows)
                         {
                             _ct = new DataTransfer.CTPhieuNhap(item["MaPhieu"].ToString(), item["MaSanPham"].ToString(), Convert.ToInt16(item["SoLuong"]));
                             BUS.CTHoaDonBanHang.Xoa(_ct.MaPhieu, _ct.MaSanPham);
                         }
                     }
                     DataTransfer.CTPhieuNhap ct;
                     foreach (DataRow r in tbCTPN.Rows)
                     {
                         ct = new DataTransfer.CTPhieuNhap(txtMaPhieu.Text, r["MaSanPham"].ToString(), Convert.ToInt32(r["SoLuong"]));
                         BUS.CTPhieuNhap.Them(ct);
                     }
                     XtraMessageBox.Show("Hóa đơn đã được lưu thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                   
                    if (isInsert)
                         NapGiaoDien();
                     else
                         Close();
                 }
                 catch (ArgumentException ex)
                 {
                     XtraMessageBox.Show(ex.Message, "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                 }
                 catch(Exception ex)
                 {
                     XtraMessageBox.Show(ex.ToString(), "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                 }
             }
             else
                 XtraMessageBox.Show("Phải chọn ít nhất một sản phẩm để tạo hóa đơn", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                 
        }

        private void gridChonSP_Click(object sender, EventArgs e)
        {

        }

        private void gridChonSP_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                txtSanPham.Text = gridViewChonSP.GetRowCellValue(gridViewChonSP.FocusedRowHandle, gridViewChonSP.Columns[0]).ToString();

                btnChon_Click(sender, e);
            }
            catch { }
        }
    }
}