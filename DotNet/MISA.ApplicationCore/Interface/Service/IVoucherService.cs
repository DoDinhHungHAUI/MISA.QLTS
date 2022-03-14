using MISA.ApplicationCore.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.ApplicationCore.Interface.Service
{
    public interface IVoucherService : IBaseService<Voucher>
    {
        /// <summary>
        /// Thêm chứng từ
        /// </summary>
        /// <param name="voucher">Chứng từ cần thêm</param>
        /// <param name="fixedAssets">Danh sách tài sản ghi tăng</param>
        /// <returns>Thông báo kết quả trả về</returns>
        /// CreatedBy : DDHung (18/12/2021)
        ServiceResult InsertVoucher(Voucher voucher, List<FixedAsset> fixedAssets);

        /// <summary>
        /// Sửa chứng từ
        /// </summary>
        /// <param name="voucher">Chứng từ cần sửa</param>
        /// <param name="fixedAssets">Danh sách tài sản ghi tăng</param>
        /// <returns>Thông báo kết quả trả về</returns>
        /// CreatedBy : DDHung (18/12/2021)
        ServiceResult UpdateVoucher(Voucher voucher, List<FixedAsset> fixedAssets);

        /// <summary>
        /// Xóa chứng từ
        /// </summary>
        /// <param name="voucher">Chứng từ cần sửa</param>
        /// <param name="fixedAssets">Danh sách tài sản ghi tăng</param>
        /// <returns>Thông báo kết quả trả về</returns>
        /// CreatedBy : DDHung (18/12/2021)
        int DeleteVoucher(string id, List<FixedAsset> fixedAssets);
    }
}
