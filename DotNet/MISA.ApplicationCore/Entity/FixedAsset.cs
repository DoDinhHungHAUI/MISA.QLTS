using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.ApplicationCore.Entity
{
    /// <summary>
    /// Tài sản
    /// </summary>
    public class FixedAsset : BaseEntity
    {
        #region Property
        /// <summary>
        /// Id tài sản
        /// </summary> 
        [PrimaryKey]
        public Guid FixedAssetId { get; set; }

        /// <summary>
        /// Mã tài sản
        /// </summary>
        [DisplayName("Mã tài sản")]
        [Required]
        [CheckDuplicate]
        public string FixedAssetCode { get; set; }

        /// <summary>
        /// Tên tài sản
        /// </summary>
        [DisplayName("Tên tài sản")]
        [MaxLength(150 , "Tên tài sản không nhập quá 150 ký tự")]
        [Required]
        public string FixedAssetName { get; set; }

        /// <summary>
        /// Id đơn vị
        /// </summary>
        public Guid? OrganizationId { get; set; }

        /// <summary>
        /// Mã đơn vị
        /// </summary>
        public string OrganizationCode { get; set; }

        /// <summary>
        /// Tên đơn vị
        /// </summary>
        public string OrganizationName { get; set; }

        /// <summary>
        /// Id phòng ban
        /// </summary>
        [DisplayName("Id phòng ban")]
        [Required]
        public Guid? DepartmentAssetId { get; set; }

        /// <summary>
        /// Mã phòng ban
        /// </summary>
        public string DepartmentAssetCode { get; set; }

        /// <summary>
        /// Tên phòng ban
        /// </summary>
        public string DepartmentAssetName { get; set; }

        /// <summary>
        /// Id loại tài sản
        /// </summary>
        [DisplayName("Id loại tài sản")]
        [Required]
        public Guid? FixedAssetCategoryId { get; set; }

        /// <summary>
        /// Mã loại tài sản
        /// </summary>
        public string FixedAssetCategoryCode { get; set; }

        /// <summary>
        /// Tên loại tài sản
        /// </summary>
        public string FixedAssetCategoryName { get; set; }

        /// <summary>
        /// Ngày mua
        /// </summary>
        [DisplayName("Ngày mua")]
        [Required]
        public DateTime? PurchaseDate { get; set; }

        /// <summary>
        /// Nguyên giá
        /// </summary>
        [DisplayName("Nguyên giá")]
        [Required]
        public decimal? Cost { get; set; }

        /// <summary>
        /// Số lượng
        /// </summary>
        [DisplayName("Số lượng")]
        [Required]
        public int? Quantity { get; set; }

        /// <summary>
        /// Tỷ lệ hao mòn
        /// </summary>
        [DisplayName("Tỷ lệ hao mòn")]
        [Required]
        public float? DepreciationRate { get; set; }

        /// <summary>
        /// Năm bắt đầu theo dõi tài sản trên phần mềm
        /// </summary>
        [Required]
        public int? TrackedYear { get; set; }

        /// <summary>
        /// Số năm sử dụng
        /// </summary>
        [DisplayName("Số năm sử dụng")]
        [Required]
        public int? LifeTime { get; set; }

        /// <summary>
        /// Năm sử dụng
        /// </summary>
        [DisplayName("Năm sử dụng")]
        [Required]
        public int? ProductionYear { get; set; }

        /// <summary>
        /// Sử dụng
        /// </summary>
        public int? Active { get; set; }

        /// <summary>
        /// Trạng thái tài sản (1-chưa được sử dụng , 2- đã được sử dụng)
        /// </summary>
        public int? StatusFixedAsset { get; set; }

        /// <summary>
        /// Id chứng từ tài sản
        /// </summary>
        public Guid? VoucherId { get; set; }

        #endregion
    }
}

