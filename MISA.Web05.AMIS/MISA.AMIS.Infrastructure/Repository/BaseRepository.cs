using Dapper;
using MISA.AMIS.Core.Interfaces.Repository;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Infrastructure.Repository
{
    public class BaseRepository<MISAEntity>: IBaseRepository<MISAEntity>
    {
        #region prop
        //chuỗi kết nối
        protected string ConnectionString;
        //khởi tạo kết nối
        protected MySqlConnection MySqlConnection;
        //tên bảng
        protected string TableName;
        #endregion

        #region Constructor
        //khởi tạo giá trị
        public BaseRepository()
        {
            ConnectionString = "Host=3.0.89.182;" +
                    " Port=3306;" +
                    " Database=MISA.WEB05.VTPTHAO;" +
                    " User Id = dev;" +
                    " Password=12345678";
            TableName = typeof(MISAEntity).Name;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Thực hiện việc đọc dữ liệu trong Db - Thông tin các bảng được truyền vào
        /// VTPTHAO (28/06/2022)
        /// </summary>
        /// <returns>list đối tượng</returns>
        public virtual IEnumerable<MISAEntity> Get()
        {
            using (MySqlConnection = new MySqlConnection(ConnectionString))
            {
                var tableName = typeof(MISAEntity).Name;
                var sql = $"SELECT * FROM {TableName}";
                var employees = MySqlConnection.Query<MISAEntity>(sql);
                return employees;
            }
        }
        /// <summary>
        /// Thực hiện việc đọc dữ liệu theo khóa chính của đối tượng
        /// VTPTHAO (28/06/2022)
        /// </summary>
        /// <param name="id">mã đối tượng</param>
        /// <returns>Danh sách đối tượng theo mã </returns>
        public MISAEntity Get(Guid id)
        {
            using (MySqlConnection = new MySqlConnection(ConnectionString))
            {
                var sql = $"SELECT * FROM {TableName} WHERE {TableName}Id = @{TableName}Id";
                var parameters = new DynamicParameters();
                parameters.Add("$@{TableName}Id", id);
                var employee = MySqlConnection.QueryFirstOrDefault<MISAEntity>(sql: sql, param: parameters);
                return employee;
            }
        }
        /// <summary>
        /// Thực hiện việc thêm dữ liệu vào Db 
        /// VTPTHAO (28/06/2022)
        /// </summary>
        /// <param name="entity">đối tượng cần thêm</param>
        /// <returns>1 - thêm thành công, 0 - thêm thất bai</returns>
        public int Insert(MISAEntity entity)
        {
            using (var SqlConnection = new MySqlConnection(ConnectionString))
            {
                var sql = $"Proc_Insert{TableName}";
                SqlConnection.Open();
                using (var transaction = SqlConnection.BeginTransaction())
                {
                    var res = SqlConnection.Execute(sql: sql, param: entity, transaction:transaction, commandType: System.Data.CommandType.StoredProcedure);
                    transaction.Commit();
                    return res;
                }
                
               
            }
        }
        /// <summary>
        /// Thực hiện việc cập nhật thông tin trong Db 
        /// VTPTHAO (28/06/2022)
        /// </summary>
        /// <param name="entity">đối tượng cần cập nhật</param>
        /// <returns>1 - cập nhật thành công, 0 - cập nhật thất bai</returns>
        public int Update(MISAEntity entity)
        {
            using (MySqlConnection = new MySqlConnection(ConnectionString))
            {
                var sql = $"Proc_Update{TableName}";
                    var res = MySqlConnection.Execute(sql: sql, param: entity, commandType: System.Data.CommandType.StoredProcedure);
                    return res;
                
            }
        }
        /// <summary>
        /// Thực hiện việc xóa dữ liệu trong Db theo khóa chính của đối tượng
        /// VTPTHAO (28/06/2022)
        /// </summary>
        /// <param name="entityId">đối tượng cần xóa</param>
        /// <returns>1 - xóa thành công, 0 - xóa thất bai</returns>
        public int Delete(Guid entityId)
        {
            using (MySqlConnection = new MySqlConnection(ConnectionString))
            {
                var sql = $"DELETE FROM {TableName} WHERE {TableName}Id = @{TableName}Id";
                var parameters = new DynamicParameters();
                parameters.Add($"@{TableName}Id", entityId);
                var res = MySqlConnection.Execute(sql: sql, param: parameters);
                return res;
            }

        }


        #endregion

    }
}
