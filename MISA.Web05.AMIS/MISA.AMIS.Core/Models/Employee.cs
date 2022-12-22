using MISA.AMIS.Core.Enum;

namespace MISA.AMIS.Core
{
    /// <summary>
    /// Nhân viên
    /// </summary>
    /// CreatedBy: VTPTHAO (22/06/2022)
    public class Employee: BaseEntity
    {
        #region Contructor
        public Employee()
        {
            EmployeeID = Guid.NewGuid();
            DepartmentId = Guid.NewGuid();
        }
        #endregion

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
        public Gender? Gender { get; set; }
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
        public string? IdentityNumber { get; set; }
        /// <summary>
        /// Ngày cấp
        /// </summary>
        public DateTime? IdentityDate { get; set; }
        /// <summary>
        /// Nơi cấp
        /// </summary>
        public string? IdentityPlace { get; set; }
        /// <summary>
        /// Email
        /// </summary>
        public string? Email { get; set; }
        /// <summary>
        /// Số điện thoại
        /// </summary>
        public string? PhoneNumber { get; set; }
        /// <summary>
        /// Khóa ngoại
        /// </summary>
        public Guid DepartmentId { get; set; }
       // public Guid? PositionsId { get; set; }

        /// <summary>
        /// Số điện thoại cố định
        /// </summary>
        public string? PhoneStatic { get; set; }
        /// <summary>
        /// Tài khoản ngân hàng
        /// </summary>
        public string? BankNumber { get; set; }
        /// <summary>
        /// Tên ngân hàng
        /// </summary>
        public string? BankName { get; set; }
        /// <summary>
        /// Chi nhánh
        /// </summary>
        public string? BankBranchName { get; set; }
        /// <summary>
        /// Tên chức danh
        /// </summary>
        public string? PositionName { get; set; }
        /// <summary>
        /// Tên phòng ban
        /// </summary>
        public string? DepartmentName { get; set; }
        //Tên giới tính
        public string? GenderName
        {
            get
            {
                var langCode = Common.LanguageCode;
                switch (Gender)
                {
                    case Enum.Gender.Male:
                        return Resources.Resource.ResourceManager.GetString($"Gender_{langCode}_Male");
                    case Enum.Gender.Famale:
                        return Resources.Resource.ResourceManager.GetString($"Gender_{langCode}_Female"); ;
                    case Enum.Gender.Other:
                        return Resources.Resource.ResourceManager.GetString($"Gender_{langCode}_Other"); ;
                    default:
                        break;
                }
                return "";
            }
        }
        #endregion

        #region Import
        /// <summary>
        /// Import có thành công hay không
        /// </summary>
        public bool? ImportSuccess { get; set; } = false;
        /// <summary>
        /// Danh sách lỗi khi import
        /// </summary>
        public List<string>? ListErrorImport = new List<string>();
        #endregion
    }
}
