using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.ApplicationCore.Entity
{
    public class Voucher : BaseEntity
    {
        #region Property
        /// <summary>
        /// Id chứng từ
        /// </summary> 
        [PrimaryKey]
        public Guid VoucherId { get; set; }

        /// <summary>
        /// Số chứng từ
        /// </summary>
        [DisplayName("Mã chứng từ")]
        [Required]
        [CheckDuplicate]
        public string VouchersCode { get; set; }

        /// <summary>
        /// Ngày chứng từ
        /// </summary>
        [DisplayName("Ngày chứng từ")]
        [Required]
        public DateTime VouchersDate { get; set; }
        /// <summary>
        /// Ngày ghi tăng
        /// </summary>
        [DisplayName("Ngày ghi tăng")]
        [Required]
        public DateTime UsedFixedAssetDate { get; set; }
        /// <summary>
        /// Nội dung
        /// </summary>
        [MaxLength(150, "Nội dung không nhập quá 150 ký tự")]
        public string Description { get; set; }
        #endregion
    }
}
