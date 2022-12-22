using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Core.Exceptions
{
    public class MISAValidateException: Exception
    {
        /// <summary>
        /// Kiểm tra thông báo lỗi validate
        /// VTPTHAO (17/07/2022)
        /// </summary>
        #region Methods
        //Biến truyền lỗi
        public string? ValidateErrorMsg { get; set; }
        //Danh sách các lỗi
        public IDictionary Errors { get; set; }
        public MISAValidateException(string errorMsg)
        {
            ValidateErrorMsg = errorMsg;
        }
        public MISAValidateException(List<string> errorMsg)
        {
            Errors = new Dictionary<string, object>();
            Errors.Add("errors", errorMsg);
        }
        public override string Message => this.ValidateErrorMsg;

        public override IDictionary Data => Errors;
        #endregion

    }
}
