using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.ApplicationCore.Entity
{
    /// <summary>
    /// Phòng ban quản lý tài sản
    /// </summary>
    public class DepartmentAsset : BaseEntity
    {
        #region Property
        /// <summary>
        /// Id phòng ban (khóa chính )
        /// </summary>
        public Guid DepartmentAssetId { get; set; }
        /// <summary>
        /// Mã phòng ban
        /// </summary>
        public string DepartmentAssetCode { get; set; }
        /// <summary>
        /// Tên phòng ban
        /// </summary>
        public string DepartmentAssetName { get; set; }
        /// <summary>
        /// Ghi chú
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Có phải là cha không
        /// </summary>
        public int? IsParent { get; set; }
        /// <summary>
        /// Id phòng ban cha
        /// </summary>
        public Guid? ParentId { get; set; }
        /// <summary>
        /// Id của đơn vị
        /// </summary>
        public Guid? OrganizationId { get; set; }
        #endregion
    }
}