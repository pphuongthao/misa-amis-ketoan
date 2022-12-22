using Dapper;
using MISA.AMIS.Core.Interfaces.Repository;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MISA.AMIS.Core;

namespace MISA.AMIS.Infrastructure.Repository
{
    public class DepartmentRepository : BaseRepository<Department>, IDDepartmentRepository
    {
        /*public DepartmentRepository()
        {

        }
        /// <summary>
        /// true - đã tồn tại
        /// false - chưa có trong hệ thống
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns></returns>
        public bool CheckDepartmentCodeExists(string departmentId)
        {
            using (MySqlConnection = new MySqlConnection(ConnectionString))
            {
                var sql = "SELECT DepartmentID FROM Department WHERE DepartmentID = @DepartmentId";
                var parameters = new DynamicParameters();
                parameters.Add("@DepartmentId", departmentId);
                var res = MySqlConnection.QueryFirstOrDefault(sql: sql, param: parameters);
                if (res == null)
                    return false;
                return true;
            }
        }
*/
        
    }
}
