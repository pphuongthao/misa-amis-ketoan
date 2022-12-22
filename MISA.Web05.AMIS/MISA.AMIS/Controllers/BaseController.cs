using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.Core;
using MISA.AMIS.Core.Exceptions;
using MISA.AMIS.Core.Interfaces.Repository;
using MISA.AMIS.Core.Interfaces.Services;
using MISA.AMIS.Core.Resources;

namespace MISA.AMIS.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BaseController<BaseEntity> : ControllerBase
    {
        #region Constructor
        IBaseRepository<BaseEntity> _repository;
        IBaseService<BaseEntity> _service;
        string langCode = Common.LanguageCode;
        
        public BaseController(IBaseRepository<BaseEntity> repository, IBaseService<BaseEntity> service)
        {
            _repository = repository;
            _service = service;
        }
        #endregion

        #region Methods 
        #region HttpGet
        /// <summary>
        /// Lấy ra toàn bộ danh sách
        /// VTPTHAO (30/06/2022)
        /// </summary>
        /// <returns>200: lấy dữ liệu thành công</returns>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var entity = _repository.Get();
                return Ok(entity);
            }
            catch (Exception ex)
            {

                return HandleException(ex);
            }
        }
        /// <summary>
        /// Lấy ra danh sách theo id
        /// VTPTHAO (30/06/2022)
        /// </summary>
        /// id: Đối tượng có mã tương ứng
        /// <returns>200: lấy dữ liệu thành công</returns>
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            try
            {
                var entity = _repository.Get(id);
                return Ok(entity);
            }
            catch (Exception ex)
            {

                return HandleException(ex);
            }
        }
        #endregion

        #region HttpPost
        /// <summary>
        /// Thêm mới bản ghi
        /// VTPTHAO (30/06/2022)
        /// </summary>
        /// <param name="entity"> Đối tượng </param>
        /// <returns>201: thêm thành công</returns>
        [HttpPost]
        public IActionResult Post(BaseEntity entity)
        {
            try
            {
                //trả kết quả về client
                var res = _service.InsertService(entity);
                return StatusCode(201, res);
            }
            catch (Exception ex)
            {

                return HandleException(ex);
            }
        }
        #endregion

        #region HttpPut
        /// <summary>
        /// Cập nhật bản ghi
        /// VTPTHAO (30/06/2022)
        /// </summary>
        /// <param name="entity">Đối tượng</param>
        /// <returns>200: sửa thành công</returns>
        [HttpPut]
        public IActionResult Put(BaseEntity entity)
        {
            try
            {
                var res = _service.UpdateService(entity);
                return Ok(res);
            }
            catch (Exception ex)
            {

                return HandleException(ex);
            }
        }
        #endregion

        #region HttpDelete
        /// <summary>
        /// Xóa bản ghi
        /// VTPTHAO (30/06/2022)
        /// <param name="id"> Đối tượng có mã tương ứng</param>
        /// </summary>
        /// <returns>200: xóa thành công</returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                var res = _repository.Delete(id);
                return Ok(res);
            }
            catch (Exception ex)
            {

                return HandleException(ex);
            }
        }
        #endregion

        #region Exception
        /// <summary>
        /// Xử lý exception
        /// VTPTHAO (30/06/2022)
        /// </summary>
        /// <param name="ex">Biến khi có lỗi Exception</param>
        /// <returns>Mã lỗi tương ứng và nội dung chi tiết</returns>
        /// <returns>400: lỗi phía client</returns>
        /// <returns>500: lỗi phía backend</returns>
        private IActionResult HandleException(Exception ex)
        {
            // ghi log vào hệ thống
            // ......
            if (ex is MISAValidateException)
            {
                var res = new
                {
                    devMsg = ex.Message,
                    data = ex.Data,
                    userMsg = ex.Message
                };
                return StatusCode(400, res);
            }
            else
            {
                var res = new
                {
                    devMsg = ex.Message,
                    userMsg = Resource.ResourceManager.GetString($"ErrorException_{langCode}")
                };
                return StatusCode(500, res);
            }

        }
        #endregion

        #endregion
    }
}
