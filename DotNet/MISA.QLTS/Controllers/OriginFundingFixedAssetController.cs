using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.ApplicationCore.Entity;
using MISA.ApplicationCore.Interface.Repository;
using MISA.ApplicationCore.Interface.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.QLTS.Controllers
{

    public class OriginFundingFixedAssetController : BaseEntitiesController<OriginFundingFixedAsset>
    {
         #region Declare
        /// <summary>
        /// Thực hiện việc kết nối CSDL 
        /// </summary>
        IOriginFundingFixedAssetRepository _originFundingFixedAssetRepository;
        #endregion

        #region Constructor
        public OriginFundingFixedAssetController(IOriginFundingFixedAssetRepository originFundingFixedAssetRepository, IOriginFundingFixedAssetService originFundingFixedAssetService) : base(originFundingFixedAssetService)
        {
            _originFundingFixedAssetRepository = originFundingFixedAssetRepository;
        }

        /// <summary>
        /// Lấy danh sách nguồn kinh phí dựa vào id tài sản
        /// </summary>
        /// <param name="fixedAssetId">Id tài sản</param>
        /// <returns>Danh sách tài sản</returns>
        /// CreatedBy : DDHung(21/12/2021)
        [HttpGet("originFunds")]
        public List<OriginFundingFixedAsset> GetOriginFundingByFixedAssetId(Guid? fixedAssetId)
        {
            List<OriginFundingFixedAsset> lstOriginFundingFixedAsset = _originFundingFixedAssetRepository.GetOriginFundingByFixedAssetId(fixedAssetId).ToList();
            return lstOriginFundingFixedAsset;
        }

        /// <summary>
        /// Thêm hoặc sửa lại thông tin nguồn kinh phí
        /// </summary>
        /// <param name="originFundingFixedAssets">Danh sách nguồn kinh phí muốn cập nhật</param>
        /// <returns>Số bản ghi bị ảnh hưởng</returns>
        /// CreatedBy : DDHung (21/12/2021)
        [HttpPost("updateOriginFund")]
        public IActionResult UpdateOriginFundingFixedAsset(List<OriginFundingFixedAsset> originFundingFixedAssets)
        {
            var rowAffect = _originFundingFixedAssetRepository.UpdateOriginFundingFixedAsset(originFundingFixedAssets);
            return Ok(rowAffect);
        }

        #endregion
    }
}
