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
    public class OriginFundingFixedAssetRepository : BaseRepository<OriginFundingFixedAsset> , IOriginFundingFixedAssetRepository
    {
        #region Constructor
        public OriginFundingFixedAssetRepository(IConfiguration configuration) : base(configuration)
        {

        }

        /// <summary>
        /// Lấy danh sách nguồn kinh phí dựa vào id tài sản
        /// </summary>
        /// <param name="fixedAssetId">Id tài sản</param>
        /// <returns>Danh sách nguồn kinh phí</returns>
        public List<OriginFundingFixedAsset> GetOriginFundingByFixedAssetId(Guid? fixedAssetId)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@m_FixedAssetId", fixedAssetId);
            var fixedAssets = _dbConnection.Query<OriginFundingFixedAsset>("Proc_GetOriginFundingByFixedAssetId", param: parameters, commandType: CommandType.StoredProcedure).ToList();
            return fixedAssets;
        }

        /// <summary>
        /// Thêm hoặc sửa lại thông tin nguồn kinh phí
        /// </summary>
        /// <param name="originFundingFixedAssets">Danh sách nguồn kinh phí muốn cập nhật</param>
        /// <returns>Số bản ghi bị ảnh hưởng</returns>
        /// CreatedBy : DDHung (21/12/2021)
        public int UpdateOriginFundingFixedAsset(List<OriginFundingFixedAsset> originFundingFixedAssets)
        {
            var rowAffects = 0;
            using (var transaction = _dbConnection.BeginTransaction())
            {
                var originFundingDelete = originFundingFixedAssets.First();

                rowAffects += DeleteOriginFundingFixedAsset(originFundingDelete.FixedAssetId);

                rowAffects += InsertOriginFundingFixedAsset(originFundingFixedAssets);

                //Tính toán lại nguyên giá
                var fixedAssetCost = originFundingFixedAssets.Sum(item => item.ValueOriginFunding);
                rowAffects += UpdateCost(fixedAssetCost, originFundingDelete.FixedAssetId);

                transaction.Commit();
            }
            return rowAffects;
        }

        /// <summary>
        /// Method thực hiện việc xóa nguồn gốc kinh phí dựa vào id tài sản
        /// </summary>
        /// <param name="fixedAssetId">Id tài sản</param>
        /// <returns>Số bản ghi bị ảnh hưởng</returns>
        /// CreatedBy : DDHung (21/12/2021)
        private int DeleteOriginFundingFixedAsset(Guid? fixedAssetId)
        {
            var rowAffects = 0;
            DynamicParameters parameters = new DynamicParameters();
            //var originFundingDelete = originFundingFixedAssets.First();
            parameters.Add("@m_FixedAssetId", fixedAssetId);

            //string updateQuery = "UPDATE OriginFunding_FixedAsset of SET of.OriginFundingId = @OriginFundingId, of.ValueOriginFunding = @ValueOriginFunding WHERE of.OriginFundingFixedAssetId = @OriginFundingFixedAssetId";
            rowAffects = _dbConnection.Execute("Proc_DeleteOriginFundByFixedAssetId", param: parameters, commandType: CommandType.StoredProcedure);
            return rowAffects;
        }

        /// <summary>
        /// Method thực hiện thêm danh sách nguồn gốc kinh phí với tài sản tương ứng
        /// </summary>
        /// <param name="originFundingFixedAssets">Danh sách nguồn gốc kinh phí muốn thêm với tài sản tương ứng</param>
        /// <returns>Số bản ghi bị ảnh hưởng</returns>
        /// CreatedBy : DDHung (21/12/2021)
        private int InsertOriginFundingFixedAsset(List<OriginFundingFixedAsset> originFundingFixedAssets)
        {
            var rowAffects = 0;
            string insertQuery =
                "INSERT INTO OriginFunding_FixedAsset " +
                "(OriginFundingFixedAssetId, FixedAssetId,OriginFundingId , ValueOriginFunding, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate) VALUES " +
                "(@OriginFundingFixedAssetId, @FixedAssetId , @OriginFundingId, @ValueOriginFunding ,'', @CreatedDate, '', NULL)";

            foreach (var item in originFundingFixedAssets)
            {
                item.OriginFundingFixedAssetId = Guid.NewGuid();
                item.CreatedDate = DateTime.Now;
            }

            rowAffects = _dbConnection.Execute(insertQuery, originFundingFixedAssets);
            return rowAffects;
        }

        /// <summary>
        /// Method cập nhập lại nguyên giá tài sản mỗi khi thêm , sửa , giá nguồn gốc kinh phí tương ứng
        /// </summary>
        /// <param name="cost">Nguyên giá để cập nhật</param>
        /// <param name="fixedAssetId">Id tài sản</param>
        /// <returns>Số bản ghi bị ảnh hưởng</returns>
        /// CreatedBy : DDHung (21/12/2021)
        private int UpdateCost(decimal? cost , Guid? fixedAssetId)
        {
            var rowAffects = 0;
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@m_Cost", cost);
            parameters.Add("@m_FixedAssetId", fixedAssetId);
            rowAffects += _dbConnection.Execute("Proc_UpdateCost", param: parameters, commandType: CommandType.StoredProcedure);
            return rowAffects;
        }

        
        #endregion
    }
}
