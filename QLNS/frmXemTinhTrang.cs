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
using System.Globalization;
namespace QLNS
{
    public partial class frmXemTinhTrang : DevExpress.XtraEditors.XtraForm
    {
        public frmXemTinhTrang()
        {
            InitializeComponent();
            KhoiTaoTbResult();
            KhoiTaoCacBUSCanThiet();

        }
        public void NapGiaoDien()
        {
            gridSP.DataSource = BUS.SanPham.Table;
            //lblSoHoaDon.Text = string.Format("Tổng cộng: {0} hóa đơn", gridViewHD.RowCount);
            cboSanPham.Enabled = false;
        }
        public void KhoiTaoCacBUSCanThiet()
        {
 
            if (BUS.SanPham == null)
                BUS.SanPham = new BusinessLogic.SanPham();
        }
        private DataTable tbResult;
        public void KhoiTaoTbResult()
        {
            tbResult = new DataTable();
            tbResult.Columns.Add("MaSanPham");
            tbResult.Columns.Add("TenSanPham");
            tbResult.Columns.Add("GiaNhap");
            tbResult.Columns.Add("GiaBan");
            tbResult.Columns.Add("MaLoai");
            tbResult.Columns.Add("TonKho");
           

            tbResult.PrimaryKey = new DataColumn[] { tbResult.Columns[0] };
        }
        public void TimKiem(bool sanpham)
        {
            DataRow[] result;

            string filterExpression = string.Empty;

            
                
            if (sanpham)
                filterExpression += string.Format("  MaSanPham = '{0}' ",// (ngay ? "and" : string.Empty),
                    cboSanPham.EditValue);
           

            result = BUS.SanPham.Table.Select(filterExpression);
            tbResult.Clear();
            if (result.Length > 0)
            {
                foreach (var item in result)
                {
                    tbResult.ImportRow(item);
                }
            }
            gridSP.DataSource = tbResult;
        }
        public void NapGiaoDienPanelTimKiem()
        {
            //combobox staff
            cboSanPham.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenSanPham"));
            cboSanPham.Properties.DataSource = BUS.SanPham.Table;
            cboSanPham.Properties.DisplayMember = "TenSanPham";
            cboSanPham.Properties.ValueMember = "MaSanPham";

           
        }
        private void frmXemTinhTrang_Load(object sender, EventArgs e)
        {
            NapGiaoDienPanelTimKiem();
           
            NapGiaoDien();
        }

       

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            TimKiem(chkTimTheoSanPham.Checked);
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            Form pn = new frmLapPhieuNhapKho();
            pn.Text = "Lập phiếu nhập kho";
            pn.MdiParent = this.MdiParent;
            pn.Show();
            
        }

        private void chkTimTheoSanPham_CheckedChanged(object sender, EventArgs e)
        {
            cboSanPham.Enabled = chkTimTheoSanPham.Checked;
        }
    }
}