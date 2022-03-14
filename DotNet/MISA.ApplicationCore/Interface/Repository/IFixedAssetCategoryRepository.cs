using MISA.ApplicationCore.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.ApplicationCore.Interface.Repository
{
    public interface IFixedAssetCategoryRepository : IBaseRepository<FixedAssetCategory>
    {

        /// <summary>
        /// Lấy danh sách loại tài sản
        /// </summary>
        /// <returns>Danh sách loại tài sản</returns>
        /// CreatedBy : DDHung(14/11/2021)
        IEnumerable<Object> Get();

        /// <summary>
        /// Lấy loại tài sản theo Id
        /// </summary>
        /// <param name="FixAssetCategoryId">Id loại tài sản</param>
        /// <returns>Loại tài sản</returns>
        Object GetFixAssetCategoryById(string FixAssetCategoryId);
    }
}
