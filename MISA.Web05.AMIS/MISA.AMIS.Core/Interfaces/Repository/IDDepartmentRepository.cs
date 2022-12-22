
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Core.Interfaces.Repository
{
    public interface IDDepartmentRepository: IBaseRepository<Department>
    {
        /// <summary>
        /// Check mã phòng ban
        /// true-đã tồn tại, false - chưa có trong hệ thống
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        /*Boolean CheckDepartmentCodeExists(string department);*/
    }
}
