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
    public partial class frmSaoLuu : DevExpress.XtraEditors.XtraForm
    {
        public frmSaoLuu()
        {
            InitializeComponent();
        }

        protected SqlConnection _connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""|DataDirectory|\Database\QuanLyNhaSach.mdf"";Integrated Security=True");

        public SqlConnection Connection
        {
            get { return _connection; }
        }

        /// <summary>
        /// Load giao diện
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmSaoLuu_Load(object sender, EventArgs e)
        {
            ckeBackupwhenstart.Checked = Settings.Default.Backup_CheckStart;
            ckeBackupwhenexit.Checked = Settings.Default.Backup_CheckExit;

            if (Settings.Default.Backup_Path.Length > 0)
                txtDuongdan1.Text = Settings.Default.Backup_Path;
        }

        /// <summary>
        /// Lấy đường dẫn thư mục chứa file backup do người dùng chỉ định
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpen_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog FolderBrowser = new FolderBrowserDialog();
            if (FolderBrowser.ShowDialog() == DialogResult.OK)
            {
                txtDuongdan.Text = FolderBrowser.SelectedPath;
                txtNoiDung.Text = "Sao lưu ";
            }
        }

        /// <summary>
        /// Lấy đường dẫn thư mục chứa file backup mặc định
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpen1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog FolderBrowser = new FolderBrowserDialog();
            if (FolderBrowser.ShowDialog() == DialogResult.OK)
            {
                txtDuongdan1.Text = FolderBrowser.SelectedPath;
            }
        }

        /// <summary>
        /// Thực hiện hành động sao lưu 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaoLuu_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtDuongdan.Text.Length == 0) // Kiểm tra đường dẫn có tồn tại
                    throw new IndexOutOfRangeException();

                string TenSl = "SL_" + ConverDateTimeToString(DateTime.Now);

                if (BackUp_full(txtDuongdan.Text, TenSl))
                    XtraMessageBox.Show("Sao lưu thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (txtDuongdan1.Text.Length != 0)
                    BackUp_full(txtDuongdan1.Text, "Sl_full");

            }
            catch (IndexOutOfRangeException)
            {
                XtraMessageBox.Show("Bạn chưa chọn thư mục lưu !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch
            {
                XtraMessageBox.Show("Lỗi hệ thống !", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Lưu các thông số thay đổi trên thanh check edit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (ckeBackupwhenstart.Checked)
                Settings.Default.Backup_CheckStart = true;
            else
                Settings.Default.Backup_CheckStart = false;

            if (ckeBackupwhenexit.Checked)
                Settings.Default.Backup_CheckExit = true;
            else
                Settings.Default.Backup_CheckExit = false;

            Settings.Default.Backup_Path = txtDuongdan1.Text;

            Settings.Default.Save();
            XtraMessageBox.Show("Lưu thành Công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Chuyển dữ liệu kiểu thời gian thành kiểu string
        /// </summary>
        /// <param name="DateTimeNow"></param>
        /// <returns></returns>
        private string ConverDateTimeToString(DateTime DateTimeNow)
        {
            string Thoigian = DateTimeNow.ToShortDateString().Replace('/', '.');
            return Thoigian;
        }

        /// <summary>
        /// Xóa file
        /// </summary>
        /// <param name="Duongdanfile"></param>
        public void DeleteFile(string Duongdanfile)
        {
            FileInfo file = new FileInfo(Duongdanfile);

            if (file.Exists) // kiểm tra xem file có tồn tại không
                file.Delete();
        }

        /// <summary>
        /// Lấy đường dẫn file
        /// </summary>
        /// <param name="Duongdan"></param>
        /// <param name="Thoigian"></param>
        /// <returns></returns>
        public string Get_pathfile(string Duongdan, DateTime Thoigian)
        {
            string Duongdanfile = Duongdan + @"\Sl_" + ConverDateTimeToString(Thoigian) + ".bak";
            return Duongdanfile;
        }

        public bool BackUp_full(string Duongdan, string TenSl)
        {
            try
            {
                string NameDatabases = "[" + Environment.CurrentDirectory + @"\DATABASE\QuanLyNhaSach.MDF" + "]";

                string str_Backup = @"WITH NOFORMAT, INIT,  NAME = N'" + Environment.CurrentDirectory + @"\DATABASE\QuanLyNhaSach.MDF" + "-Full Database Backup', SKIP, NOREWIND, NOUNLOAD,  STATS = 10";

                string sqlCommand = @"BACKUP DATABASE " + NameDatabases + "TO DISK = N'" + Duongdan + @"\" + TenSl + ".bak'" + str_Backup;

                SqlCommand command = new SqlCommand(sqlCommand, Connection);

                OpenConnection();

                command.ExecuteNonQuery();

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
