using DevExpress.XtraEditors;
using QLNS.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLNS
{
    public partial class frmPhucHoi : DevExpress.XtraEditors.XtraForm
    {
        public frmPhucHoi()
        {
            InitializeComponent();
        }

        private string _Duongdan = Settings.Default.Backup_Path;
        protected SqlConnection _connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""|DataDirectory|\Database\QuanLyNhaSach.mdf"";Integrated Security=True");

        public SqlConnection Connection
        {
            get { return _connection; }
        }

        /// <summary>
        /// Lấy đường dẫn file sao lưu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog FileBrowser = new OpenFileDialog();
            FileBrowser.Filter = "Backup files (*.bak)|*.bak|All files (*.*)|*.*";
            if (FileBrowser.ShowDialog() == DialogResult.OK)
            {
                txtDuongdan.Text = FileBrowser.FileName;
            }
        }

        /// <summary>
        /// Thực hiện chức năng phục hồi
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPhucHoi_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtDuongdan.Text.Length == 0) // Kiểm tra đường dẫn có tồn tại
                    throw new IndexOutOfRangeException();

                FileInfo file = new FileInfo(txtDuongdan.Text);
                if (file.Exists)
                {
                    if (Restore(txtDuongdan.Text))
                    {
                        XtraMessageBox.Show("Phục hồi thành công !", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        if (ckeDeleteAfterRestore.Checked)
                        {
                            frmSaoLuu frmSl = new frmSaoLuu();
                            frmSl.DeleteFile(txtDuongdan.Text);
                        }

                        txtDuongdan.Text = string.Empty;
                    }
                }
                else
                {
                    XtraMessageBox.Show("Bản sao lưu không tồn tại !", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (IndexOutOfRangeException)
            {
                XtraMessageBox.Show("Bạn chưa chọn bản cần phục hồi !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch
            {
                XtraMessageBox.Show("Lỗi hệ thống !", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ckeRestoredefault_CheckedChanged(object sender, EventArgs e)
        {
            if (ckeRestoredefault.Checked)
            {
                btnOpen.Enabled = false;
                ckeDeleteAfterRestore.Checked = false;
                ckeDeleteAfterRestore.Enabled = false;
                txtDuongdan.Text = Settings.Default.Backup_Path + @"\Sl_full.bak";
            }
            else
            {
                txtDuongdan.Text = string.Empty;
                btnOpen.Enabled = true;
                ckeDeleteAfterRestore.Enabled = true;
            }
        }

        public bool Restore(string Duongdan)
        {
            try
            {
                string NameDatabases = "[" + Environment.CurrentDirectory + @"\Database\QuanLyNhaSach.MDF" + "]";

                string sqlCommand_0 = "USE [master]";

                string sqlCommand_1 = @"RESTORE DATABASE " + NameDatabases + " FROM DISK = N'" + Duongdan + "' WITH  FILE = 1,  NOUNLOAD, REPLACE , STATS = 5";

                SqlCommand command_0 = new SqlCommand(sqlCommand_0, Connection);

                SqlCommand command_1 = new SqlCommand(sqlCommand_1, Connection);

                OpenConnection();

                command_0.ExecuteNonQuery();

                command_1.ExecuteNonQuery();

                CloseConnection();

                return true;
            }
            catch
            {
                CloseConnection();

                return false;
            }
        }

        public void OpenConnection()
        {
            Connection.Open();
        }
        public void CloseConnection()
        {
            Connection.Close();
        }
    }
}
