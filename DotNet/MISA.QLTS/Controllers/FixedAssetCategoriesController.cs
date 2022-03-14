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
    public class FixedAssetCategoriesController : BaseEntitiesController<FixedAssetCategory>
    {
        #region Declare
        /// <summary>
        /// Thực hiện việc kết nối CSDL 
        /// </summary>
        IFixedAssetCategoryRepository _fixedAssetCategoryRepository;

        #endregion
        #region Constructor
        public FixedAssetCategoriesController(IFixedAssetCategoryService fixedAssetCategoryService , IFixedAssetCategoryRepository fixedAssetCategoryRepository) : base(fixedAssetCategoryService)
        {
            _fixedAssetCategoryRepository = fixedAssetCategoryRepository;
        }
        #endregion
        #region Method

        /// <summary>
        /// Lấy danh sách loại tài sản
        /// </summary>
        /// <returns>Trả về danh sách loại tài sản</returns>
        public override IActionResult Get()
        {
            var fixedAssetCategories = _fixedAssetCategoryRepository.Get();
            return Ok(fixedAssetCategories);
        }

        /// <summary>
        /// Lấy loại tài sản theo Id
        /// </summary>
        /// <param name="FixAssetCategoryId">Id loại tài sản</param>
        /// <returns>Loại tài sản</returns>

        public override IActionResult Get(string id)
        {
            var fixedAssetCategory = _fixedAssetCategoryRepository.GetFixAssetCategoryById(id);
            return Ok(fixedAssetCategory);
        }

        #endregion  
    }
}
