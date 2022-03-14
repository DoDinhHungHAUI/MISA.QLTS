using MISA.ApplicationCore.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.ApplicationCore.Interface.Repository
{
    public interface IDepartmentAssetRepository : IBaseRepository<DepartmentAsset>
    {
        /// <summary>
        /// Lấy danh sách phòng ban
        /// </summary>
        /// <returns>Danh sách phòng ban</returns>
        /// CreatedBy : DDHung(14/11/2021)
        IEnumerable<Object> Get();

        /// <summary>
        /// Lấy danh sách phòng ban theo id
        /// </summary>
        /// <returns>Phòng ban</returns>
        Object GetDepartentAssetById(string departmentAssetId);
       
    }
}
