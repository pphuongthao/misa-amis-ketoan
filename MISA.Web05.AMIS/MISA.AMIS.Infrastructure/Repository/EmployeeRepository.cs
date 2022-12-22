using MISA.AMIS.Core;
using MISA.AMIS.Core.Interfaces.Repository;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace MISA.AMIS.Infrastructure.Repository
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository()
        {
           
        }
        #region Methods

        /// <summary>
        /// Check trùng mã nhân viên
        /// VTPTHAO (20/06/2022)
        /// </summary>
        /// <param name="employeeCode">mã nhân viên</param>
        /// <returns>true - đã tồn tại</returns>
        /// <returns>false - chưa có trong hệ thống</returns>
        public bool CheckEmployeeCodeExists(string employeeCode)
        {
            using (MySqlConnection = new MySqlConnection(ConnectionString))
            {
                var sql = "SELECT EmployeeCode FROM Employee WHERE EmployeeCode = @EmployeeCode";
                var parameters = new DynamicParameters();
                parameters.Add("@EmployeeCode", employeeCode);
                var res = MySqlConnection.QueryFirstOrDefault(sql: sql, param: parameters);
                if(res == null)
                    return false;
                return true;
            }
        }
        /// <summary>
        /// Override lại hàm Lấy toàn bộ dữ liệu để đưa thêm các thuộc tính khác vào
        /// VTPTHAO (30/06/2022)
        /// </summary>
        /// <returns>list đối tượng nhân viên</returns>
        public override IEnumerable<Employee> Get()
        {
            using (MySqlConnection = new MySqlConnection(ConnectionString))
            {
                var sql = $"Proc_GetEmployee";
                var employees = MySqlConnection.Query<Employee>(sql, commandType:System.Data.CommandType.StoredProcedure);
                return employees;
            }
        }
        /// <summary>
        /// phân trang
        /// VTPTHAO (10/7/2022)
        /// </summary>
        /// <param name="pageIndex">trang bắt đầu</param>
        /// <param name="pageSize">số lượng bản ghi trên 1 trang</param>
        /// <param name="filter">key tìm kiếm</param>
        /// <returns>
        /// recordStart (trang bắt đầu), recordEnd (trang cuối), totalRecord (tổng trang)
        /// Data (các bản ghi)
        /// </returns>
        public object GetPaging(int pageIndex, int pageSize, string? filter = "")
        {
            using (MySqlConnection = new MySqlConnection(ConnectionString))
            {
                var sql = $"Proc_GetEmployeePaging";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@t_Filter", filter);
                parameters.Add("@t_PageIndex", pageIndex);
                parameters.Add("@t_PageSize", pageSize);
                parameters.Add("@t_TotalRecord", direction: System.Data.ParameterDirection.Output);
                parameters.Add("@t_RecordStart", direction: System.Data.ParameterDirection.Output);
                parameters.Add("@t_RecordEnd", direction: System.Data.ParameterDirection.Output);
                var employees = MySqlConnection.Query<Employee>(sql, param: parameters, commandType: System.Data.CommandType.StoredProcedure);
                int totalRecord = parameters.Get<int>("t_TotalRecord");
                int recordStart = parameters.Get<int>("t_RecordStart");
                int recordEnd = parameters.Get<int>("t_RecordEnd");
                return new
                {
                    TotalRecord = totalRecord,
                    RecordStart = recordStart,
                    RecordEnd = recordEnd,
                    Data = employees
                };
            }
        }
        /// <summary>
        /// thực hiện nhập khẩu dữ liệu
        /// VTPTHAO (9/7/2022)
        /// </summary>
        /// <param name="employees">danh sách đối tượng</param>
        /// <returns>Số lượng bản ghi thêm thành công</returns>
        public int Import(IEnumerable<Employee> employees)
        {
            using (MySqlConnection = new MySqlConnection(ConnectionString))
            {
                var employeesInserted = 0;
                foreach (var employee in employees)
                {
                    var rowInsert = MySqlConnection.Execute("Proc_InsertEmployee", employees, commandType: System.Data.CommandType.StoredProcedure);
                    if (rowInsert != null)
                        employeesInserted += rowInsert;
                }
                return employeesInserted;
            }
        }

        /// <summary>
        /// Thực hiện tự sinh mã nhân viên mới
        /// VTPTHAO (5/7/2022)
        /// </summary>
        /// <returns>ok - mã nhân viên mới, failed - message lỗi</returns>
        public string GetNewEmployeeCode()
        {
            using (MySqlConnection = new MySqlConnection(ConnectionString))
            {
                var sqlQuery = "Proc_NewEmployeeCode";
                var res = MySqlConnection.QueryFirst<string>(sql: sqlQuery, commandType: System.Data.CommandType.StoredProcedure);
                return res;
            }
        }

        #endregion
    }
}
