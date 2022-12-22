namespace MISA.AMIS.Common.Models { 
    /// <summary>
    /// Chức danh
    /// </summary>
    /// CreatedBy: VTPTHAO (22/06/2022)
    public class Positions:BaseEntity
    {
        #region Properties
        /// <summary>
        /// Khóa chính
        /// </summary>
        public Guid PositionID { get; set; }

        /// <summary>
        /// Tên chức danh
        /// </summary>
        public string PositionName { get; set; }
        
        #endregion

    }
}
