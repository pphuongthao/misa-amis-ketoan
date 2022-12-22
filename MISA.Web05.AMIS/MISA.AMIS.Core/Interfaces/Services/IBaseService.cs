using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Core.Interfaces.Services
{
    public interface IBaseService<MISAEntity>
    {
        #region Constructor
        /// <summary>
        /// Thêm dữ liệu
        /// VTPTHAO (22/06/2022)
        /// </summary>
        /// <param name="entity">Đối tượng</param>
        int InsertService(MISAEntity entity);
        /// <summary>
        /// Sửa dữ liệu
        /// VTPTHAO (22/06/2022)
        /// </summary>
        /// <param name="entity">Đối tượng</param>
        int UpdateService(MISAEntity entity);
        #endregion
    }
}
