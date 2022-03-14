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
    public class FixedAssetCategoryRepository : BaseRepository<FixedAssetCategory>, IFixedAssetCategoryRepository
    {
        #region Constructor
        public FixedAssetCategoryRepository(IConfiguration configuration) : base(configuration)
        {

        }
        #endregion

        #region Method
        /// <summary>
        /// Lấy danh sách loại tài sản
        /// </summary>
        /// <returns>Trả về danh sách loại tài sản</returns>
        public IEnumerable<object> Get()
        {
            var fixedAssetCategories = _dbConnection.Query<Object>($"Proc_Get{_tableName}", commandType: CommandType.StoredProcedure).ToList();
            return fixedAssetCategories;
        }

        /// <summary>
        /// Lấy loại tài sản theo Id
        /// </summary>
        /// <param name="FixAssetCategoryId">Id loại tài sản</param>
        /// <returns>Loại tài sản</returns>
        public Object GetFixAssetCategoryById(string FixAssetCategoryId)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@FixedAssetCategoryId", FixAssetCategoryId);

            var fixAssetCategory = _dbConnection.Query<Object>($"Proc_Get{_tableName}ById", param: parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
            return fixAssetCategory;
        }
        #endregion
    }
}
