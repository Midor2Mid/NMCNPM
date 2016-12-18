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
    public partial class frmSach : DevExpress.XtraEditors.XtraForm
    {
        public frmSach()
        {
            InitializeComponent();
            KhoiTaoCacBUSCanThiet();
        }
        public void NapGiaoDien()
        {
            gridSach1.DataSource = BUS.SanPham.Table;
            gridSach2.DataSource = BUS.SanPham.Table;
        }
        public void KhoiTaoCacBUSCanThiet()
        {

            if (BUS.SanPham == null)
                BUS.SanPham = new BusinessLogic.SanPham();
        }
        private void gridSach1_Load(object sender, EventArgs e)
        {
            NapGiaoDien();
        }

        private void gridSach2_Load(object sender, EventArgs e)
        {
            NapGiaoDien();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            frmThemLoai frm = new frmThemLoai();
            frm.ShowDialog();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            frmThemSach frm = new frmThemSach();
            frm.ShowDialog();
        }
    }
}