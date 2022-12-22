using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MISA.AMIS.Core.Exceptions;
using MISA.AMIS.Core.Interfaces.Repository;
using MISA.AMIS.Core.Interfaces.Services;

namespace MISA.AMIS.Core.Services
{
    public class DepartmentService :BaseService<Department>, IDepartmentService
    {
        IDDepartmentRepository _repository;
        string langCode = Common.LanguageCode;
        public DepartmentService(IDDepartmentRepository repository): base(repository)
        {
            _repository = repository;
        }

        #region Methods
        /// <summary>
        /// Validate dữ liệu cho phòng ban
        /// VTPTHAO (02/07/2022)
        /// </summary>
        /// <param name="department">phòng ban</param>
        /// <returns>true -  hợp lệ, false - không hợp lệ</returns>
        protected override bool Validate(Department department)
        {
            // 1. Tên phòng ban không được phép để trống:
            if (string.IsNullOrEmpty(department.DepartmentName))
            {
                IsValid = false;
                ErrorValidateMsgs.Add(Resources.Resource.ResourceManager.GetString($"ErrorDepartmentId_{langCode}"));
            }
            return IsValid;
        }
        #endregion
    }
}
