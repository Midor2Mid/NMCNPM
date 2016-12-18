//Written by @phungnlg

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public abstract class DBConnect
    {
        protected SqlConnection _connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""|DataDirectory|\Database\QuanLyNhaSach.mdf"";Integrated Security=True");


        public SqlConnection Connection
        {
            get { return _connection; }
        }
        protected void OpenConnection()
        {
            Connection.Open();
        }
        protected void CloseConnection()
        {
            Connection.Close();
        }
        public DataTable GetData(string sqlCommand)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand, Connection);

            //mở kết nối
            OpenConnection();

            //lấp dữ liệu vào bảng
            adapter.Fill(dt);

            //đóng kết nối
            CloseConnection();
            return dt;
        }
        public int ExecuteCommand(string sqlCommand)
        {
            SqlCommand command = new SqlCommand(sqlCommand, Connection);

            OpenConnection();

            int row = command.ExecuteNonQuery();

            CloseConnection();

            return row;
        }
        public string AutoGenerateNumber()
        {
            StringBuilder result = new StringBuilder(3);
            Random rand = new Random();

            for (int i = 0; i < result.Capacity; i++)
            {
                result.Append((char)rand.Next('0', '9'));
            }

            return result.ToString();
        }
    }
}
