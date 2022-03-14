using MISA.ApplicationCore.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.ApplicationCore.Interface.Repository
{
    public interface IOriginFundingFixedAssetRepository : IBaseRepository<OriginFundingFixedAsset>
    {
        /// <summary>
        /// Lấy danh sách nguồn kinh phí dựa vào id tài sản
        /// </summary>
        /// <param name="fixedAssetId">Id tài sản</param>
        /// <returns>Danh sách nguồn kinh phí</returns>
        public List<OriginFundingFixedAsset> GetOriginFundingByFixedAssetId(Guid? fixedAssetId);

        /// <summary>
        /// Thêm hoặc sửa lại thông tin nguồn kinh phí
        /// </summary>
        /// <param name="originFundingFixedAssets">Danh sách nguồn kinh phí muốn cập nhật</param>
        /// <returns>Số bản ghi bị ảnh hưởng</returns>
        /// CreatedBy : DDHung (21/12/2021)
        public int UpdateOriginFundingFixedAsset(List<OriginFundingFixedAsset> originFundingFixedAssets);
    }
}
