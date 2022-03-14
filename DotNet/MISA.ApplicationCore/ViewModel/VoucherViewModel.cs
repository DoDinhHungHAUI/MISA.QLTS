using MISA.ApplicationCore.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.ApplicationCore.ViewModel
{
    public class VoucherViewModel : Voucher
    {
        #region Property

        /// <summary>
        /// Tổng nguyên giá
        /// </summary>
        public decimal? TotalCost { get; set; }

        #endregion
    }
}
