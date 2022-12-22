using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Core.Interfaces.Repository
{
    public interface IBaseRepository<MISAEntity>
    {
        #region Methods
        /// <summary>
        /// Lấy toàn bộ dữ liệu
        /// VTPTHAO (22/06/2022)
        /// </summary>
        /// <returns></returns>
        IEnumerable<MISAEntity> Get();
        /// <summary>
        /// Lấy theo id
        /// VTPTHAO (22/06/2022)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        MISAEntity Get(Guid id);
        /// <summary>
        /// Thêm mới
        /// VTPTHAO (22/06/2022)
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        int Insert(MISAEntity employee);
        /// <summary>
        /// Sửa dữ liệu
        /// VTPTHAO (22/06/2022)
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        int Update(MISAEntity employee);
        /// <summary>
        /// Xóa dữ liệu
        /// VTPTHAO (22/06/2022)
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        int Delete(Guid employeeId);
        #endregion
    }
}
