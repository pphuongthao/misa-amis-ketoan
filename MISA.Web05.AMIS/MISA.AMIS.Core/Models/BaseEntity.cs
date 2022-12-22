namespace MISA.AMIS.Core
{
    public class BaseEntity
    {
        #region properties

        /// <summary>
        /// Ngày tạo
        /// </summary>
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Người tạo
        /// </summary>
        public string? CreatedBy { get; set; }

        /// <summary>
        /// Ngày chỉnh sửa gần nhất
        /// </summary>
        public DateTime ModifiedDate { get; set; }

        /// <summary>
        /// Người thực hiện chỉnh sửa
        /// </summary>
        public string? ModifiedBy { get; set; }
        #endregion
    }
}
