using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Core.Interfaces.Repository
{
    /// <summary>
    /// Thực hiện nghiệp vụ
    /// </summary>
    public interface IEmployeeRepository: IBaseRepository<Employee>
    {
        #region Methods
        /// <summary>
        /// Thực hiện sinh mã nhân viên mới
        /// VTPTHAO (06/07/2022)
        /// </summary>
        /// <returns>true: Mã nhân viên mới</returns>
        /// 
        string GetNewEmployeeCode();
        /// <summary>
        /// Check mã nhân viên
        /// VTPTHAO (06/07/2022)
        /// </summary>
        /// <param name="employeeCode">mã nhân viên</param>
        /// <returns>true-đã tồn tại, false - chưa có trong hệ thống</returns>
        Boolean CheckEmployeeCodeExists(string employeeCode);

        /// <summary>
        /// Thực hiện nghiệp vụ tìm kiếm và phân trang
        /// VTPTHAO (06/07/2022)
        /// </summary>
        /// <param name="filter"> Từ khóa tìm kiếm</param>
        /// <param name="pageIndex"> thứ tự trang </param>
        /// <param name="pageSize"> số lượng bản ghi 1 trang </param>
        /// <returns></returns>
        object GetPaging(int pageIndex, int pageSize, string? filter = "");


        //số bản ghi đã thêm thành công
        int Import(IEnumerable<Employee> employees);
        #endregion


    }
}
