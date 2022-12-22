using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Core.Interfaces.Repository
{
    public interface IPositionsRepository : IBaseRepository<Positions>
    {
        /// <summary>
        /// Check mã chức danh
        /// true-đã tồn tại, false - chưa có trong hệ thống
        /// </summary>
        /// <param name="positionsCode"></param>
        /// <returns></returns>
       /* Boolean CheckPositionsCodeExists(string positionsCode);*/
    }
}
