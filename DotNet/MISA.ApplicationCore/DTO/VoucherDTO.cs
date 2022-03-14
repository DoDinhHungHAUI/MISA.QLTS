using MISA.ApplicationCore.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.ApplicationCore.DTO
{
    /// <summary>
    /// DTO chứng từ
    /// </summary>
    public class VoucherDTO
    {
        /// <summary>
        /// ViewModel chứng từ
        /// </summary>
        public VoucherViewModel voucherViewModel { get; set; }
        /// <summary>
        /// Danh sách ViewModel tài sản
        /// </summary>
        public List<FixedAssetViewModel> fixedAssetViewModels { get; set; }
    }
}
