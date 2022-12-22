using MISA.AMIS.Core.Exceptions;
using MISA.AMIS.Core.Interfaces.Repository;
using MISA.AMIS.Core.Interfaces.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Core.Services
{
    public class BaseService<MISAEntity>: IBaseService<MISAEntity>
    {
        #region Constructor
        IBaseRepository<MISAEntity> _repository;
        protected List<string> ErrorValidateMsgs;
        protected bool IsValid = true;
        
        public BaseService(IBaseRepository<MISAEntity> repository)
        {
            ErrorValidateMsgs = new List<string>();
            _repository = repository;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Thêm mới dữ liệu
        /// VTPTHAO (20/06/2022)
        /// </summary>
        /// <param name="entity">Đối tượng</param>
        /// <returns>true -  hợp lệ, false - không hợp lệ</returns>
        /// <exception cref="MISAValidateException">Hàm thực hiện gán lại data cho việc validate</exception>
        public int InsertService(MISAEntity entity)
        {
            //validate dữ liệu
            var isValid = Validate(entity);
            //Thực hiện thêm mới
            if(isValid == true)
            {
                var res = _repository.Insert(entity);
                return res;
            }
            else
            {
                throw new MISAValidateException(ErrorValidateMsgs);

            }
        }
        /// <summary>
        /// Thực hiện validate dữ liệu
        /// VTPTHAO (20/06/2022)
        /// </summary>
        /// <param name="entity"Đối tượng></param>
        /// <returns>true -  hợp lệ, false - không hợp lệ</returns>
        protected virtual bool Validate(MISAEntity entity)
        {
            return true;
        }
        /// <summary>
        /// Thực hiện validate dữ liệu khi sửa
        /// VTPTHAO (20/06/2022)
        /// </summary>
        /// <param name="entity">đối tượng</param>
        /// <returns>true -  hợp lệ, false - không hợp lệ</returns>
        protected virtual bool ValidateUpdate(MISAEntity entity)
        {
            return true;
        }
        /// <summary>
        /// Thực hiện sửa dữ liệu
        /// VTPTHAO (20/06/2022)
        /// </summary>
        /// <param name="entity">đối tượng</param>
        /// <returns>true -  hợp lệ, false - không hợp lệ</returns>
        public int UpdateService(MISAEntity entity)
        {
            //validate dữ liệu
            var isValid = Validate(entity);
            //Thực hiện sửa dữ liệu
            if(isValid == true)
            {
                var res = _repository.Update(entity);
                return res;
            }
            else
            {
                throw new MISAValidateException(ErrorValidateMsgs);
            }
        }

        #endregion
    }
}
