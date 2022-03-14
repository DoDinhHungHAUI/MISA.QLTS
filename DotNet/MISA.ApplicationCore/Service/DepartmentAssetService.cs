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
    /// service xử lý nghiệp vụ cho phòng ban
    /// </summary>
    public class DepartmentAssetService : BaseService<DepartmentAsset>, IDepartmentAssetService
    {
    
        #region Constructor
        public DepartmentAssetService(IDepartmentAssetRepository departmentAssetRepository) : base(departmentAssetRepository)
        {
            
        }
        #endregion
    }
}
