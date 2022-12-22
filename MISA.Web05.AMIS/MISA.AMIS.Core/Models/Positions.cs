namespace MISA.AMIS.Core { 
    /// <summary>
    /// Chức danh
    /// </summary>
    /// CreatedBy: VTPTHAO (22/06/2022)
    public class Positions:BaseEntity
    {
        #region Constructor
        public Positions()
        {
            this.PositionsId = Guid.NewGuid();
        }
        #endregion

        #region Properties
        /// <summary>
        /// Khóa chính
        /// </summary>
        public Guid PositionsId { get; set; }

        /// <summary>
        /// Tên chức danh
        /// </summary>
        public string PositionName { get; set; }
        
        #endregion

    }
}
