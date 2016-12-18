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
using System.Globalization;

namespace QLNS
{
    public partial class frmHoaDonBanHang : DevExpress.XtraEditors.XtraForm
    {
        public frmHoaDonBanHang()
        {
            InitializeComponent();
            KhoiTaoCacBUSCanThiet();
            KhoiTaoTbResult();
        }
        private DataTransfer.HoaDonBanHang hd;
        private DataTable tbResult;

        public void NapGiaoDien()
        {
            gridHD.DataSource = BUS.HoaDonBanHang.Table;
            lblSoHoaDon.Text = string.Format("Tổng cộng: {0} hóa đơn", gridViewHD.RowCount);
            cboKhachHang.Enabled = cboNhanVien.Enabled = false;
        }
        public void NapGiaoDienPanelTimKiem()
        {
            //combobox staff
            cboNhanVien.Properties.Columns.Add(new LookUpColumnInfo("HoTen"));
            cboNhanVien.Properties.DataSource = BUS.NguoiDung.Table;
            cboNhanVien.Properties.DisplayMember = "HoTen";
            cboNhanVien.Properties.ValueMember = "MaNguoiDung";

            //combobox customer
            cboKhachHang.Properties.Columns.Add(new LookUpColumnInfo("HoTen"));
            cboKhachHang.Properties.DataSource = BUS.KhachHang.Table;
            cboKhachHang.Properties.DisplayMember = "HoTen";
            cboKhachHang.Properties.ValueMember = "MaKhachHang";

            chkFindNgay.Checked = true;
            chkFindNhanVien.Checked = chkFindKhachHang.Checked = false;

            dateFrom.DateTime = DateTime.Now;
            dateTo.DateTime = DateTime.Now;
        }
        public void KhoiTaoCacBUSCanThiet()
        {
            if (BUS.HoaDonBanHang == null)
                BUS.HoaDonBanHang = new BusinessLogic.HoaDonBanHang();
            if (BUS.NguoiDung == null)
                BUS.NguoiDung = new BusinessLogic.NguoiDung();
            if (BUS.KhachHang == null)
                BUS.KhachHang = new BusinessLogic.KhachHang();
        }
        public void KhoiTaoTbResult()
        {
            tbResult = new DataTable();
            tbResult.Columns.Add("MaHoaDon");
            tbResult.Columns.Add("MaKhachHang");
            tbResult.Columns.Add("MaNhanVien");
            tbResult.Columns.Add("NgayLap");
            tbResult.Columns.Add("ThanhTien");
            tbResult.Columns.Add("DaThu");
            tbResult.Columns.Add("GhiChu");

            tbResult.PrimaryKey = new DataColumn[] { tbResult.Columns[0] };
        }
        public void TimKiem(bool ngay, bool nv, bool kh)
        {
            DataRow[] result;

            string filterExpression = string.Empty;

            if (ngay)
                filterExpression += " NgayLap >= '" + dateFrom.DateTime.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture) +
                                          "' and NgayLap <= '" + dateTo.DateTime.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture) + "'";
            if (nv)
                filterExpression += string.Format(" {0} MaNhanVien = '{1}' ", (ngay ? "and" : string.Empty), cboNhanVien.EditValue);
            if (kh)
                filterExpression += string.Format(" {0} MaKhachHang = '{1}' ", (nv ? "and" : string.Empty), cboKhachHang.EditValue);

            result = BUS.HoaDonBanHang.Table.Select(filterExpression);
            tbResult.Clear();
            if(result.Length > 0)
            {
                foreach(var item in result)
                {
                    tbResult.ImportRow(item);
                }
            }
            gridHD.DataSource = tbResult;
        }

        private void frmHoaDonBanHang_Load(object sender, EventArgs e)
        {
            NapGiaoDien();
            NapGiaoDienPanelTimKiem();
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            TimKiem(chkFindNgay.Checked, chkFindNhanVien.Checked, chkFindKhachHang.Checked);
            lblSoHoaDon.Text = string.Format("Tổng cộng: {0} hóa đơn", gridViewHD.RowCount);
        }

        private void btnXemTatCa_Click(object sender, EventArgs e)
        {
            gridHD.DataSource = BUS.HoaDonBanHang.Table;
            lblSoHoaDon.Text = string.Format("Tổng cộng: {0} hóa đơn", gridViewHD.RowCount);
        }

        private void chkFindNgay_CheckedChanged(object sender, EventArgs e)
        {
            dateFrom.Enabled = dateTo.Enabled = chkFindNgay.Checked;
        }

        private void chkFindNhanVien_CheckedChanged(object sender, EventArgs e)
        {
            cboNhanVien.Enabled = chkFindNhanVien.Checked;
        }

        private void chkFindKhachHang_CheckedChanged(object sender, EventArgs e)
        {
            cboKhachHang.Enabled = chkFindKhachHang.Checked;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            Form hd = new frmLapHoaDonBanHang();
            hd.Text = "Lập hóa đơn bán hàng";
            hd.MdiParent = this.MdiParent;
            hd.Show();
            NapGiaoDien();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow r = gridViewHD.GetDataRow(gridViewHD.FocusedRowHandle);
                hd = new DataTransfer.HoaDonBanHang(r["MaHoaDon"].ToString(), r["MaKhachHang"].ToString(),
                r["MaNhanVien"].ToString(), Convert.ToDateTime(r["NgayLap"]), 
                Convert.ToDouble(r["ThanhTien"]), Convert.ToBoolean(r["DaThu"]), r["GhiChu"].ToString());

                Form sua = new frmLapHoaDonBanHang(hd);
                sua.Text = "Sửa hóa đơn bán hàng";
                sua.MdiParent = this.MdiParent;
                sua.Show();
                NapGiaoDien();
            }
            catch
            {
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow r = gridViewHD.GetDataRow(gridViewHD.FocusedRowHandle);
                if (XtraMessageBox.Show("Bạn có muốn xóa hóa đơn số " + r["MaHoaDon"].ToString() + "?" + Environment.NewLine +
                    "Thao tác này sẽ xóa tất cả các chi tiết sản phẩm của hóa đơn này!",
                    "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (BUS.HoaDonBanHang.Xoa(r["MaHoaDon"].ToString()))
                    {
                        XtraMessageBox.Show("Đã xóa hóa đơn thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        NapGiaoDien();
                    }
                    else
                        XtraMessageBox.Show("Có lỗi xảy ra!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch
            {
                XtraMessageBox.Show("Có lỗi xảy ra!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gridHD_DoubleClick(object sender, EventArgs e)
        {
            btnSua_Click(sender, e);
        }

        
    }
}