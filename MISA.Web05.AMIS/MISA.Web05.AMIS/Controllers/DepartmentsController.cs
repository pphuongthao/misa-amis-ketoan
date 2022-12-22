using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Dapper;
using MySqlConnector;
using MISA.Web05.AMIS.Models;

namespace MISA.Web05.AMIS.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        string _connectionString;
        MySqlConnection _mySqlConnection;

        public DepartmentsController()
        {
            _connectionString = "Host=3.0.89.182;" +
                    " Port=3306;" +
                    " Database=MISA.WEB05.VTPTHAO;" +
                    " User Id = dev;" +
                    " Password=12345678";
            _mySqlConnection = new MySqlConnection(_connectionString);  
        }
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                //khai báo thông tin CSDL
                
                //Khởi tạo kết nối
                

                //Lấy dữ liệu
                var sqlQuery = "SELECT * FROM Department";
                var departments = _mySqlConnection.Query<Department>(sqlQuery);
                //Trả dữ liệu về cho client
                return Ok(departments); 
            }
            catch (Exception ex)
            {
                
                var res = new
                {
                    devMsg = ex.Message,
                    userMsg = "Có lỗi xảy ra vui lòng liên hệ MISA để được trợ giúp"
                };
                return StatusCode(500, res);
            }
        }
        //Truyền qua router
        /*[HttpGet("{departmentId}/{name}")]
        public IActionResult GetById(Guid departmentId, string name)
        {
            try
            {
                //khai báo thông tin CSDL
                var connectionString = "Host=3.0.89.182;" +
                    " Port=3306;" +
                    " Database=MISA.WEB05.VTPTHAO;" +
                    " User Id = dev;" +
                    " Password=12345678";
                //Khởi tạo kết nối
                var sqlConnection = new MySqlConnection(connectionString);

                //Khai báo lệnh truy vấn
                var sqlQuery = $"SELECT * FROM Department WHERE DepartmentId = '{departmentId.ToString()}'";
                var department = sqlConnection.QueryFirstOrDefault<Department>(sqlQuery);
                return Ok(name);
            }
            catch (Exception)
            {

                throw;
            }*/
            //Truyền qua querystring
            [HttpGet("search")]
            public IActionResult GetById(Guid departmentId, [FromQuery]string name)
            {
                try
                {
                    /*//khai báo thông tin CSDL
                    var connectionString = "Host=3.0.89.182;" +
                        " Port=3306;" +
                        " Database=MISA.WEB05.VTPTHAO;" +
                        " User Id = dev;" +
                        " Password=12345678";
                    //Khởi tạo kết nối
                    var sqlConnection = new MySqlConnection(connectionString);*/

                    //Khai báo lệnh truy vấn
                    var sqlQuery = $"SELECT * FROM Department WHERE DepartmentId = '{departmentId.ToString()}'";
                    var department = _mySqlConnection.QueryFirstOrDefault<Department>(sqlQuery);
                    return StatusCode(201,name);
                }
                catch (Exception ex)
                {
                    var res = new
                    {
                        devMsg = ex.Message,
                        userMsg = "Có lỗi xảy ra vui lòng liên hệ MISA để được trợ giúp"
                    };
                    return StatusCode(500, res);

                }
            }
        [HttpPost]
        public IActionResult Post(Department department)
        {
            try
            {
                //validate dữ liệu
                //1. Tên phòng ban không được phép để trống
                if (string.IsNullOrEmpty(department.DepartmentName))
                {
                    var resService = new
                    {
                        userMsg = "Tên phòng ban không được phép để trống",
                    };
                    return BadRequest(resService);
                }
                //2. Tên phòng ban không được chứa từ Thảo
                if (department.DepartmentName.Contains("Thảo"))
                {
                    var resService = new
                    {
                        userMsg = "Tên phòng ban không được chứa từ Thảo",
                    };
                    return BadRequest(resService);
                }
                //Khởi tạo ID mới:
                /*department.DepartmentId = Guid.NewGuid();*/

                //Khởi tạo lệnh truy vấn thêm mới
                var dynamicParams = new DynamicParameters();
                dynamicParams.Add("@p_DepartmentID", department.DepartmentId);
                dynamicParams.Add("@p_DepartmentName", department.DepartmentName);
                var sqlQuery = "Proc_InsertDepartment";
                var res = _mySqlConnection.Execute(sqlQuery,param:dynamicParams, commandType:System.Data.CommandType.StoredProcedure);
                return Ok(res);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
