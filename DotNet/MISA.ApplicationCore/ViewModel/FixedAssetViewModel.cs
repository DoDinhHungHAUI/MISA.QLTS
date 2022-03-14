using MISA.ApplicationCore.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.ApplicationCore.ViewModel
{
    /// <summary>
    /// Class lấy những thông tin cần thiết của tài sản phục vụ cho việc load dữ liệu lên dao diện
    /// CreatedBy DDHung(17/11/2021)
    /// </summary>
    public class FixedAssetViewModel : FixedAsset
    {
        #region Property
      
        /// <summary>
        /// hao mòn lũy kế
        /// </summary>
        public decimal FixedAssetCummulative { get; set; }

        /// <summary>
        /// Giá trị còn lại
        /// </summary>
        public decimal FixedAssetRest { get; set; }
        #endregion
    }
}
