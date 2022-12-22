using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.Core;
using MISA.AMIS.Core.Exceptions;
using MISA.AMIS.Core.Interfaces.Repository;
using MISA.AMIS.Core.Interfaces.Services;
using MISA.AMIS.Core.Resources;
using OfficeOpenXml;

namespace MISA.AMIS.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmployeesController : BaseController<Employee>
    {
        #region Constructor
        IEmployeeRepository _repository;
        IEmployeeService _service;

        //mã code ở resource
        string langCode = Common.LanguageCode; 

        //Gán giá trị khi được thêm vào
        public EmployeesController(IEmployeeRepository repository, IEmployeeService service) : base(repository, service)
        {
            _repository = repository;
            _service = service;
        }
        #endregion

        #region Methods
        #region HttpGet
        /// <summary>
        /// Lấy mã nhân viên mới
        /// VTPTHAO (06/07/2022)
        /// </summary>
        /// <param>NewEmployee: mã mới nhân viên</param>
        /// <returns>ok - mã nhân viên mới, failed - message</returns>
        [HttpGet("NewEmployee")]
        public IActionResult GetNewEmployeeCode()
        {
            try
            {
                var data = _repository.GetNewEmployeeCode();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return (ActionResult)HandleException(ex);
            }
        }
        #endregion

        /// <summary>
        /// Phân trang
        /// VTPTHAO (06/07/2022)
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="pageIndex">Trang hiện tại</param>
        /// <param name="pageSize">Số bản ghi của trang</param>
        /// <param name="filter">đối tượng tìm kiếm</param>
        /// <returns>
        /// input: recordStart (trang bắt đầu), recordEnd (trang cuối), totalRecord (tổng trang)
        /// output: Data (các bản ghi)
        /// </returns>
        [HttpGet("filter")]
        public IActionResult GetPaging(int pageIndex, int pageSize, string? filter)
        {
            try
            {
                var res = _repository.GetPaging(pageIndex, pageSize, filter);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }
        /// <summary>
        /// Nhập khẩu dữ liệu từ file
        /// VTPTHAO (8/7/2022)
        /// </summary>
        /// <param name="fileImport">input: file</param>
        /// <returns>danh sách các đối tượng</returns>
        [HttpPost("import")]
        public IActionResult Import(IFormFile fileImport)
        {
            try
            {
                var employees = _service.Import(fileImport);
                return Ok(employees);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }
        /// <summary>
        /// Xuất khẩu dữ liệu ra file
        /// VTPTHAO (17/7/2022)
        /// </summary>
        /// <returns>file excel</returns>
        [HttpGet("export")]
        public FileContentResult Export()
        {
            try
            {
                ExcelPackage employees = _service.Export();
                // ghi dũ liệu ra file
                using (var stream = new MemoryStream())
                {
                    employees.SaveAs(stream);
                    var content = stream.ToArray();

                    var file = new FileContentResult(content, "application/octet-stream");
                    file.FileDownloadName = "Danh_sach_nhan_vien.xlsx";
                    return file;
                }
            }
            catch (Exception)
            {
                throw;

                //return (ActionResult)HandleException(ex);
            }
        }
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
    }
}

