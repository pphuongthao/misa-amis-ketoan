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
    public class PositionsController : BaseController<Positions>
    {
        IPositionsService _service;
        IPositionsRepository _repository;
        #region Constructor
        /// <summary>
        /// Khởi tạo 
        /// </summary>
        /// <param name="service"></param>
        /// <param name="repository"></param>
        public PositionsController(IPositionsRepository repository, IPositionsService service) : base(repository, service)
        {
            _service = service;
            _repository = repository;
        }
        #endregion

    }
}
