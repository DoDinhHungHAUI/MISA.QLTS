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
    /// <summary>
    /// Service xử lý nghiệp vụ cho Loại tài sản
    /// </summary>
    public class FixedAssetCategoryService : BaseService<FixedAssetCategory>, IFixedAssetCategoryService
    {
       
        #region Constructor
        public FixedAssetCategoryService(IFixedAssetCategoryRepository fixedAssetCategoryRepository) : base(fixedAssetCategoryRepository)
        {
            
        }
        #endregion
      
    }
}
