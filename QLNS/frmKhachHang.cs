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
    public partial class frmKhachHang : DevExpress.XtraEditors.XtraForm
    {
        public frmKhachHang()
        {
            InitializeComponent();     
            KhoiTaoCacBUSCanThiet();

        }
        public void NapGiaoDien()
        {
            gridKH.DataSource = BUS.KhachHang.Table;
            //lblSoHoaDon.Text = string.Format("Tổng cộng: {0} hóa đơn", gridViewHD.RowCount);
            //cboKhachHang.Enabled = false;
        }
        public void KhoiTaoCacBUSCanThiet()
        {

            if (BUS.KhachHang == null)
                BUS.KhachHang = new BusinessLogic.KhachHang();
        }
        
        private void frmKhachHang_Load(object sender, EventArgs e)
        {
            NapGiaoDien();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            frmThemKH frm = new frmThemKH();
            frm.ShowDialog();
        }
    }
}

