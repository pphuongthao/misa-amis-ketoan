namespace MISA.AMIS.Common.Models
{
    /// <summary>
    /// Nhân viên
    /// </summary>
    /// CreatedBy: VTPTHAO (22/06/2022)
    public class Employee: BaseEntity
    {
        #region Properties
        /// <summary>
        /// Khóa chính
        /// </summary>
        public Guid EmployeeID { get; set; }

        /// <summary>
        /// Mã nhân viên
        /// </summary>
        public string EmployeeCode { get; set; }
        /// <summary>
        /// Tên nhân viên
        /// </summary>
        public string EmployeeName { get; set; }
        /// <summary>
        /// Giới tính
        /// </summary>
        public int? Gender { get; set; }
        /// <summary>
        /// Ngày sinh
        /// </summary>
        public DateTime? DateOfBirth { get; set; }
        /// <summary>
        /// Địa chỉ
        /// </summary>
        public string? Address { get; set; }
        /// <summary>
        /// Số chứng minh thư nhân dân
        /// </summary>
        public string IdentityNumber { get; set; }
        /// <summary>
        /// Ngày cấp
        /// </summary>
        public DateTime IdentityDate { get; set; }
        /// <summary>
        /// Nơi cấp
        /// </summary>
        public string? IdentityPlace { get; set; }
        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Số điện thoại
        /// </summary>
        public string PhoneNumber { get; set; }
        /// <summary>
        /// Khóa ngoại
        /// </summary>
        public Guid? DepartmentId { get; set; }
        public Guid? PositionId { get; set; }

        /// <summary>
        /// Số điện thoại cố định
        /// </summary>
        public string PhoneStatic { get; set; }
        /// <summary>
        /// Tài khoản ngân hàng
        /// </summary>
        public string BankNumber { get; set; }
        /// <summary>
        /// Tên ngân hàng
        /// </summary>
        public string BankName { get; set; }
        /// <summary>
        /// Chi nhánh
        /// </summary>
        public string BankBranchName { get; set; }
        

        #endregion

    }
}
