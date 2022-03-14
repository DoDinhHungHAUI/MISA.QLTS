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
    /// <summary>
    /// API phòng ban
    /// </summary>
    public class DepartmentAssetsController : BaseEntitiesController<DepartmentAsset>
    {
        #region Declare
        IDepartmentAssetRepository _departmentAssetRepository;
        #endregion
        #region Constructor
        public DepartmentAssetsController(IDepartmentAssetService departmentAssetService , IDepartmentAssetRepository departmentAssetRepository) : base(departmentAssetService)
        {
            _departmentAssetRepository = departmentAssetRepository;
        }
        #endregion

        #region Method

        /// <summary>
        /// Lấy danh sách phòng ban
        /// </summary>
        /// <returns>Trả về danh sách phòng ban</returns>
        public override IActionResult Get()
        {
            var departmentAssets = _departmentAssetRepository.Get();
            return Ok(departmentAssets);
        }

        /// <summary>
        /// Lấy danh sách phòng ban theo id
        /// </summary>
        /// <returns>Phòng ban</returns>
        public override IActionResult Get(string id)
        {
            var departmentAsset = _departmentAssetRepository.GetDepartentAssetById(id);
            return Ok(departmentAsset);
        }

        #endregion

    }
}
