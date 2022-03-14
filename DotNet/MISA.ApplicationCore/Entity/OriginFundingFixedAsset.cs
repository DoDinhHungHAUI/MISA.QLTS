using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.ApplicationCore.Entity
{
    /// <summary>
    /// Class trung gian giữa bảng nguồn và bảng tài sản
    /// </summary>
    public class OriginFundingFixedAsset : BaseEntity
    {
        /// <summary>
        /// Id class trung gian giữa bảng nguồn và bảng tài sản
        /// </summary>
        public Guid? OriginFundingFixedAssetId { get; set; }
        /// <summary>
        /// Id tài sản
        /// </summary>
        public Guid? FixedAssetId { get; set; }
        /// <summary>
        /// Id nguồn gốc kinh phí
        /// </summary>
        public Guid? OriginFundingId { get; set; }
        /// <summary>
        /// giá trị nguồn gốc kinh phí
        /// </summary>
        public decimal? ValueOriginFunding { get; set; }

    }
}
