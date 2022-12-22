using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.Core;
using MISA.AMIS.Core.Exceptions;
using MISA.AMIS.Core.Interfaces.Repository;
using MISA.AMIS.Core.Interfaces.Services;

namespace MISA.AMIS.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DepartmentsController : BaseController<Department>
    {
        #region Constructor
        //Nghiệp vụ thêm sửa
        IDepartmentService _service;
        //thực hiện thêm, sửa, xóa không có nghiệp vụ
        IDDepartmentRepository _repository;
        public DepartmentsController(IDDepartmentRepository repository, IDepartmentService service) : base(repository, service)
        {
            _service = service;
            _repository = repository;
        }
        #endregion

    }
}
