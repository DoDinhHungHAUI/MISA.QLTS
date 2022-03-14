using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.ApplicationCore.Entity;
using MISA.ApplicationCore.Interface.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Infrastructure
{
    public class DepartmentAssetRepository : BaseRepository<DepartmentAsset> , IDepartmentAssetRepository
    {
        #region Constructor
        public DepartmentAssetRepository(IConfiguration configuration) : base(configuration)
        {

        }
        #endregion

        #region Method

        /// <summary>
        /// Lấy danh sách phòng ban
        /// </summary>
        /// <returns>Danh sách phòng ban</returns>
        public IEnumerable<object> Get()
        {
            var departmentAssets = _dbConnection.Query<Object>($"Proc_Get{_tableName}", commandType: CommandType.StoredProcedure).ToList();
            return departmentAssets;
        }

        /// <summary>
        /// Lấy danh sách phòng ban theo id
        /// </summary>
        /// <returns>Phòng ban</returns>
        public Object GetDepartentAssetById(string departmentAssetId)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@departmentAssetId", departmentAssetId);
            var departmentAsset = _dbConnection.Query<Object>($"Proc_Get{_tableName}ById", param : parameters , commandType: CommandType.StoredProcedure).FirstOrDefault();
            return departmentAsset;
        }
        #endregion
    }
}
