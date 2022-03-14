using MISA.ApplicationCore.Entity;
using MISA.ApplicationCore.Interface.Repository;
using MISA.ApplicationCore.Interface.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.ApplicationCore.Service
{
    public class VoucherService : BaseService<Voucher>, IVoucherService
    {
        /// <summary>
        /// Thực hiện việc thao tác với database
        /// </summary>
        IVoucherRepository _voucherRepository;

        #region Constructor
        public VoucherService(IVoucherRepository voucherRepository) : base(voucherRepository)
        {
            _voucherRepository = voucherRepository;
        }


        /// <summary>
        /// Thêm chứng từ
        /// </summary>
        /// <param name="voucher">Chứng từ cần thêm</param>
        /// <param name="fixedAssets">Danh sách tài sản ghi tăng</param>
        /// <returns>Số dòng chứng từ bị ảnh hưởng</returns>
        /// CreatedBy : DDHung (18/12/2021)
        public ServiceResult InsertVoucher(Voucher voucher, List<FixedAsset> fixedAssets)
        {
            voucher.EntityState = EntityState.AddNew;
            //Thực hiện validate
            var isValidate = Validate(voucher);
            if (isValidate == true)
            {
                _serviceResult.Data = _voucherRepository.InsertVoucher(voucher , fixedAssets);
                _serviceResult.MisaCode = MISAEnum.IsValid;
                return _serviceResult;
            }
            else
            {
                return _serviceResult;
            }
        }

        /// <summary>
        /// Sửa chứng từ
        /// </summary>
        /// <param name="voucher">Chứng từ cần sửa</param>
        /// <param name="fixedAssets">Danh sách tài sản ghi tăng</param>
        /// <returns>Thông báo kết quả trả về</returns>
        /// CreatedBy : DDHung (18/12/2021)
        public ServiceResult UpdateVoucher(Voucher voucher, List<FixedAsset> fixedAssets)
        {
            voucher.EntityState = EntityState.Update;

            //Thực hiện validate
            var isValidate = Validate(voucher);
            if (isValidate == true)
            {
                _serviceResult.Data = _voucherRepository.UpdateVoucher(voucher , fixedAssets);
                _serviceResult.MisaCode = MISAEnum.IsValid;
                return _serviceResult;
            }
            else
            {
                return _serviceResult;
            }
        }

        /// <summary>
        /// Xóa chứng từ
        /// </summary>
        /// <param name="voucher">Chứng từ cần sửa</param>
        /// <param name="fixedAssets">Danh sách tài sản ghi tăng</param>
        /// <returns>Thông báo kết quả trả về</returns>
        /// CreatedBy : DDHung (18/12/2021)
        public int DeleteVoucher(string id, List<FixedAsset> fixedAssets)
        {
            return _voucherRepository.DeleteVoucher(id, fixedAssets);
        }

        #endregion
    }
}
