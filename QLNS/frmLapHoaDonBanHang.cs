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
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;

namespace QLNS
{
    public partial class frmLapHoaDonBanHang : DevExpress.XtraEditors.XtraForm
    {
        private DataTransfer.HoaDonBanHang hdbh;
        DataTable tbCTHD = new DataTable();
        private bool isInsert;
        public frmLapHoaDonBanHang(DataTransfer.HoaDonBanHang hdbh = null)
        {
            InitializeComponent();
            this.hdbh = hdbh;
            isInsert = hdbh == null;
            KhoiTaoCacBUSCanThiet();
        }
        public void KhoiTaoCacBUSCanThiet()
        {
            if (BUS.KhachHang == null)
                BUS.KhachHang = new BusinessLogic.KhachHang();
            if (BUS.HoaDonBanHang == null)
                BUS.HoaDonBanHang = new BusinessLogic.HoaDonBanHang();
            if (BUS.CTHoaDonBanHang == null)
                BUS.CTHoaDonBanHang = new BusinessLogic.CTHoaDonBanHang();
            if (BUS.SanPham == null)
                BUS.SanPham = new BusinessLogic.SanPham();
        }

        //Nạp giao diện mặc định
        public void NapGiaoDien(DataTransfer.HoaDonBanHang hd = null)
        {
            if(hd == null)
            {
                hd = new DataTransfer.HoaDonBanHang(BUS.HoaDonBanHang.AutoGenerateID(), string.Empty, string.Empty, DateTime.Now, 0, false, string.Empty);
                
                NapGiaoDien(hd);
            }
            else
            {
                //load combobox Khách hàng
                cboMaKhachHang.Properties.Columns.Add(new LookUpColumnInfo("HoTen"));
                cboMaKhachHang.Properties.DataSource = BUS.KhachHang.Table;
                cboMaKhachHang.Properties.DisplayMember = "HoTen";
                cboMaKhachHang.Properties.ValueMember = "MaKhachHang";

                txtMaHoaDon.Text = hd.MaHoaDon;
                dateNgayLap.EditValue = hd.NgayLap;
                cboMaKhachHang.EditValue = hd.MaKhachHang;
                txtGhiChu.Text = hd.GhiChu;
                txtThanhTien.Value = (decimal)hd.ThanhTien;
                chkDaThu.Checked = hd.DaThu;

                tbCTHD.Rows.Clear();
                DataRow[] rows = BUS.CTHoaDonBanHang.Table.Select("MaHoaDon = '" + hd.MaHoaDon + "'");
                foreach (DataRow item in rows)
                {
                    DataRow r = tbCTHD.NewRow();
                    DataRow _r = BUS.SanPham.Table.Select("MaSanPham = '" + item["MaSanPham"] + "'")[0];
                    r["MaSanPham"] = item["MaSanPham"];
                    r["TenSanPham"] = _r["TenSanPham"];                  
                    r["SoLuong"] = item["SoLuong"];
                    r["ThanhTien"] = Convert.ToInt32(r["SoLuong"]) * Convert.ToInt32(_r["GiaBan"]);

                    tbCTHD.Rows.Add(r);
                }
            }
            lblTongSanPham.Text = "Tổng cộng: " + BUS.SanPham.Table.Rows.Count + " sản phẩm.";
            lblTongChonSanPham.Text = "Tổng cộng: " + tbCTHD.Rows.Count + " sản phẩm.";
        }
        public void KhoiTaoTableCTHD()
        {
            tbCTHD.Columns.Add("MaSanPham");
            tbCTHD.Columns.Add("TenSanPham");
            tbCTHD.Columns.Add("SoLuong");
            tbCTHD.Columns.Add("ThanhTien");
            tbCTHD.PrimaryKey = new DataColumn[] { tbCTHD.Columns[0] };
            tbCTHD.RowChanged += TbCTHD_RowChanged;
            tbCTHD.RowDeleted += TbCTHD_RowChanged;
            tbCTHD.TableCleared += TbCTHD_TableCleared;
        }
        private void TbCTHD_TableCleared(object sender, DataTableClearEventArgs e)
        {
            lblTongChonSanPham.Text = "Tổng cộng: " + tbCTHD.Rows.Count + " sản phẩm.";
        }

        private void TbCTHD_RowChanged(object sender, DataRowChangeEventArgs e)
        {
            lblTongChonSanPham.Text = "Tổng cộng: " + tbCTHD.Rows.Count + " sản phẩm.";
        }
        public void CapNhatSoTienHoaDon()
        {
            long tongTien = 0;
            foreach (DataRow item in tbCTHD.Rows)
            {
                tongTien += Convert.ToInt64(item["ThanhTien"]);
            }
            
            txtThanhTien.Value = tongTien;
        }
        public DataTransfer.HoaDonBanHang NapHoaDon()
        {
            DataTransfer.HoaDonBanHang hd1 = new DataTransfer.HoaDonBanHang(txtMaHoaDon.Text, cboMaKhachHang.EditValue.ToString(), "ND001", dateNgayLap.DateTime, (double)txtThanhTien.Value, chkDaThu.Checked, "");
            
            if (string.IsNullOrEmpty(hd1.MaKhachHang))
                throw new ArgumentException("Vui lòng nhập đầy đủ thông tin.");
            return hd1;
        }
        private void frmLapHoaDonBanHang_Load(object sender, EventArgs e)
        {          
            KhoiTaoTableCTHD();
            gridChonSP.DataSource = BUS.SanPham.Table;
            gridCTSP.DataSource = tbCTHD;
            NapGiaoDien(hdbh);
        }

        private void btnChonSanPham_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow r = tbCTHD.NewRow();
                DataRow _r = gridViewChonSP.GetDataRow(gridViewChonSP.FocusedRowHandle);
                r["MaSanPham"] = _r["MaSanPham"];
                r["TenSanPham"] = _r["TenSanPham"];               
                r["SoLuong"] = 1;
                r["ThanhTien"] = Convert.ToInt32(r["SoLuong"]) * Convert.ToInt32(_r["GiaBan"]);

                tbCTHD.Rows.Add(r);
                gridViewCTSP.SelectRow(gridViewCTSP.FindRow(r));
                gridViewCTSP.Focus();
                CapNhatSoTienHoaDon();
            }
            catch (ConstraintException)
            {
                XtraMessageBox.Show("Bạn đã chọn sản phẩm " + txtChonSanPham.Text + " rồi!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            {
                XtraMessageBox.Show("Vui lòng chọn một sản phẩm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void gridChonSP_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                txtChonSanPham.Text = gridViewChonSP.GetRowCellValue(gridViewChonSP.FocusedRowHandle, gridViewChonSP.Columns[0]).ToString();

                btnChonSanPham_Click(sender, e);
            }
            catch { }
        }

        private void btnTang_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow r = gridViewCTSP.GetDataRow(gridViewCTSP.FocusedRowHandle);
                DataRow _r = BUS.SanPham.Table.Rows.Find(r["MaSanPham"]);

                r["SoLuong"] = Convert.ToInt32(r["SoLuong"]) + 1;
                r["ThanhTien"] = Convert.ToInt32(r["SoLuong"]) * Convert.ToInt32(_r["GiaBan"]);

                CapNhatSoTienHoaDon();
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
                DataRow r = gridViewCTSP.GetDataRow(gridViewCTSP.FocusedRowHandle);
                DataRow _r = BUS.SanPham.Table.Rows.Find(r["MaSanPham"]);

                int sl = Convert.ToInt32(r["SoLuong"]);
                r["SoLuong"] = sl > 1 ? sl - 1 : sl;
                r["ThanhTien"] = Convert.ToInt32(r["SoLuong"]) * Convert.ToInt32(_r["GiaBan"]);

                CapNhatSoTienHoaDon();
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
                tbCTHD.Rows.Remove(gridViewCTSP.GetDataRow(gridViewCTSP.FocusedRowHandle));
                CapNhatSoTienHoaDon();
            }
            catch
            {
                XtraMessageBox.Show("Không có gì để xóa!", "Thông tin", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnXoaTatCa_Click(object sender, EventArgs e)
        {
            tbCTHD.Rows.Clear();
            txtThanhTien.Value = 0;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            
            if(tbCTHD.Rows.Count > 0)
            {
                try
                {
                    //thêm hóa đơn chính
                    this.hdbh = NapHoaDon();
                    if(isInsert)
                    {
                        BUS.HoaDonBanHang.Them(hdbh);
                    }
                    else
                    {
                        BUS.HoaDonBanHang.Sua(hdbh);
                        DataTransfer.CTHoaDonBanHang _ct;
                        DataRow[] rows = BUS.CTHoaDonBanHang.Table.Select("MaHoaDon = '" + hdbh.MaHoaDon + "'");
                        foreach(DataRow item in rows)
                        {
                            _ct = new DataTransfer.CTHoaDonBanHang(item["MaHoaDon"].ToString(), item["MaSanPham"].ToString(), Convert.ToInt16(item["SoLuong"]));
                            BUS.CTHoaDonBanHang.Xoa(_ct.MaHoaDon, _ct.MaSanPham);
                        }
                    }
                    DataTransfer.CTHoaDonBanHang ct;
                    foreach (DataRow r in tbCTHD.Rows)
                    {
                        ct = new DataTransfer.CTHoaDonBanHang(txtMaHoaDon.Text, r["MaSanPham"].ToString(), Convert.ToInt32(r["SoLuong"]));
                        BUS.CTHoaDonBanHang.Them(ct);
                    }
                    XtraMessageBox.Show("Hóa đơn đã được lưu thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (isInsert)
                        NapGiaoDien();
                    else
                        Close();
                }
                catch(ArgumentException ex)
                {
                    XtraMessageBox.Show(ex.Message, "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                catch
                {
                    XtraMessageBox.Show("Có lỗi xảy ra!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
                XtraMessageBox.Show("Phải chọn ít nhất một sản phẩm để tạo hóa đơn", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            if (isInsert)
                NapGiaoDien();
            else
                Close();
        }
    }
}