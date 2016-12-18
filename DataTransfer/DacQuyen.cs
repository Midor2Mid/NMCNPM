using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransfer
{
    public class DacQuyen
    {
        #region Field
        private string _maDacQuyen;
        private string _tenDacQuyen;
        #endregion

        #region Properties
        
        public string MaDacQuyen
        {
            get { return _maDacQuyen; }
            set { _maDacQuyen = value; }
        }
        public string TenDacQuyen
        {
            get { return _tenDacQuyen; }
            set { _tenDacQuyen = value; }
        }
        #endregion

        #region Methods
        public DacQuyen(string maDacQuyen, string tenDacQuyen)
        {
            MaDacQuyen = maDacQuyen;
            TenDacQuyen = tenDacQuyen;
        }
        #endregion
    }
}
