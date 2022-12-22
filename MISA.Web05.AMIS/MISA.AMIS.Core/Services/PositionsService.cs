using MISA.AMIS.Core.Interfaces.Repository;
using MISA.AMIS.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Core.Services
{
    public class PositionsService :BaseService<Positions>, IPositionsService
    {
        IPositionsRepository _repository;
        string langCode = Common.LanguageCode;

        public PositionsService(IPositionsRepository repository) : base(repository)
        {
            _repository = repository;
        }
        #region Methods
        /// <summary>
        /// Validate dữ liệu cho vị trí
        /// VTPTHAO (02/07/2022)
        /// </summary>
        /// <param name="positions">vị trí</param>
        /// <returns>true: hợp lệ, false: không hợp lệ</returns>
        protected override bool Validate(Positions positions)
        {
            // 1. Tên phòng ban không được phép để trống:
            if (string.IsNullOrEmpty(positions.PositionName))
            {
                IsValid = false;
                ErrorValidateMsgs.Add(Resources.Resource.ResourceManager.GetString($"ErrorPositionName_{langCode}"));
            }
            return IsValid;
        }
        #endregion
    }
}
