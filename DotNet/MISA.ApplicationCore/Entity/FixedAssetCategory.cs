using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.ApplicationCore.Entity
{
    /// <summary>
    /// Loại tài sản
    /// </summary>
    public class FixedAssetCategory : BaseEntity
    {
        #region Property
        /// <summary>
        /// Id danh mục tài sản
        /// </summary>
        public Guid FixedAssetCategoryId { get; set; }
        /// <summary>
        /// Tên danh mục tài sản
        /// </summary>
        public string FixedAssetCategoryName { get; set; }
        /// <summary>
        /// Mã danh mục tài sản
        /// </summary>
        public string FixedAssetCategoryCode { get; set; }
        /// <summary>
        /// Id của đơn vị
        /// </summary>
        public Guid? OrganizationId { get; set; }
        /// <summary>
        /// Tỷ lệ hao mòn (%)
        /// </summary>
        public float DepreciationRate { get; set; }
        /// <summary>
        /// Số năm sử dụng
        /// </summary>
        public int LifeTime { get; set; }
        /// <summary>
        /// Ghi chú
        /// </summary>
        public string Description { get; set; }
        #endregion
    }
}

