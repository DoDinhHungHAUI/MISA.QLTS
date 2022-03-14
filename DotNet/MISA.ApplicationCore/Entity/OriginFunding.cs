using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.ApplicationCore.Entity
{
    /// <summary>
    /// Nguồn gốc kinh phí
    /// </summary>
    public class OriginFunding : BaseEntity
    {
        /// <summary>
        /// Id nguồn gốc kinh phí
        /// </summary>
        [PrimaryKey]
        public Guid OriginFundingId { get; set; }
        /// <summary>
        /// Mã nguồn gốc kinh phí
        /// </summary>
        public string OriginFundingCode { get; set; }
        /// <summary>
        /// Tên nguồn gốc kinh phí
        /// </summary>
        public string OriginFundingName { get; set; }
        /// <summary>
        /// Mô tả nguồn gốc kinh phí
        /// </summary>
        public string Description { get; set; }
    }
}
