using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Core.Interfaces.Services
{
    public interface IEmployeeService: IBaseService<Employee>
    {
        #region Methods
        /// <summary>
        /// Thực hiện xuất khẩu ra file excel
        /// VTPTHAO (11/07/2022)
        /// </summary>
        /// <returns>file excel bao gồm toàn bộ danh sách nhân viên</returns>
        ExcelPackage Export();
        /// <summary>
        /// Thực hiện nhập khẩu dữ liệu
        /// VTPTHAO (11/07/2022)
        /// </summary>
        /// <param name="fileImport">Tệp nhập khẩu dữ liệu</param>
        /// <returns>Danh sách nhân viên trong tệp nhập khẩu kèm theo trạng thái đã nhập khẩu hay chưa</returns>
        IEnumerable<Employee> Import(IFormFile fileImport);
        #endregion
    }
}
