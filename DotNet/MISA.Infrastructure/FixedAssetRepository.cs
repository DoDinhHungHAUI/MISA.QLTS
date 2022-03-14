using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.ApplicationCore.Entity;
using MISA.ApplicationCore.Interface.Repository;
using MISA.ApplicationCore.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Infrastructure
{
    /// <summary>
    /// Repository tài sản thực hiện các kết nối với Database
    /// </summary>
    public class FixedAssetRepository : BaseRepository<FixedAsset>, IFixedAssetRepository
    {
        #region Constructor
        public FixedAssetRepository(IConfiguration configuration) : base(configuration)
        {

        }
        #endregion

        #region Method

        /// <summary>
        /// Lấy danh sách tài sản
        /// </summary>
        /// <returns>Danh sách tài sản</returns>
        /// CreatedBy : DDHung (15/11/2021)

        public override IEnumerable<FixedAssetViewModel> GetEntities()
        {
            var fixedAssets = _dbConnection.Query<FixedAssetViewModel>("Proc_GetFixedAsset", commandType: CommandType.StoredProcedure).ToList();
            return fixedAssets;
        }
        //public IEnumerable<Object> GetFixedAssets()
        //{
        //    var fixedAssets = _dbConnection.Query<Object>("Proc_GetFixedAsset", commandType: CommandType.StoredProcedure).ToList();
        //    return fixedAssets;
        //}

        /// <summary>
        /// Lấy tài sản theo id
        /// </summary>
        /// <param name="entityId">Id tài sản</param>
        /// <returns>Thông tin tài sản</returns>
        public override FixedAssetViewModel GetEntityById(string entityId)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("FixedAssetId", entityId);
            var entity = _dbConnection.Query<FixedAssetViewModel>("Proc_GetFixedAssetById", param: parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
            return entity;
        }

        /// <summary>
        /// Lấy mã code tự động sinh mới cho tài sản được thêm tiếp theo
        /// </summary>
        /// <returns>Trả về mã code tự động sinh mới</returns>
        public string GetNewCodeFixedAsset()
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("newFixedAssetCode", dbType:DbType.String , direction: ParameterDirection.Output);
            _dbConnection.Query<string>("Proc_GetNewFixedAssetCode", param: parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
            var newFixedAssetCode = parameters.Get<string>("newFixedAssetCode");
            return newFixedAssetCode;
        }

        /// <summary>
        /// Method sử dụng để xóa nhiều bản ghi của bảng tài sản
        /// </summary>
        /// <param name="listId">Danh sách id của tài sản</param>
        /// <returns>Trả về số bản ghi ảnh hưởng khi xóa</returns> 
        public int DeleteMultipleRecords(string listId)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("listId", listId);
            //parameters.Add("numberId", numberId);
            var rowAffect = 0;
            using (var transaction = _dbConnection.BeginTransaction())
            {
                rowAffect = _dbConnection.Execute("Proc_DeleteMultipleRecordFixedAsset", param: parameters, commandType: CommandType.StoredProcedure);
                transaction.Commit();
            }
            return rowAffect;
        }

        /// <summary>
        /// Lọc dữ liệu kết hợp giữa search , filter, paging. 
        /// </summary>
        /// <param name="searchText">Ký tự muốn tìm kiếm</param>
        /// <param name="departmentFixedAssetId">Id phòng ban</param>
        /// <param name="fixedAssetCategoryId">Loại tài sản</param>
        /// <param name="pageSize">Số lượng record trên 1 trang</param>
        /// <param name="m_PageIndex">Trang hiện tại</param>
        /// <returns>Danh sách tài sản được tìm thấy</returns>
        /// CreatedBy : DDHung (19/11/2021)
        public Object GetFixedAssetPaging(string searchText , Guid? departmentFixedAssetId , Guid? fixedAssetCategoryId , int pageSize , int pageIndex)
        {
            //Tính lại pageIndex
            pageIndex = (pageIndex - 1) * pageSize;

            //Build tham số đầu vào cho store
            var input = searchText != null ? searchText : string.Empty;
            DynamicParameters parameters = new DynamicParameters();
            var totalRecord = 0;
            var totalPage = 0;
            var totalQuantity = 0;
            var totalCost = default(decimal);
            var totalFixedAssetCummulative = default(decimal);
            var totalFixedAssetRest = default(decimal);

            parameters.Add("@m_FixedAssetName", input);
            parameters.Add("@m_FixedAssetCode", input);
            parameters.Add("@m_DepartmentFixedAssetId", departmentFixedAssetId);
            parameters.Add("@m_FixedAssetCategoryId", fixedAssetCategoryId);
            parameters.Add("@m_PageSize", pageSize);
            parameters.Add("@m_PageIndex", pageIndex);
            parameters.Add("@m_TotalRecord" , dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("@m_TotalPage", dbType: DbType.Int32, direction: ParameterDirection.Output);

            parameters.Add("@m_TotalQuantity", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("@m_TotalCost", dbType: DbType.Decimal, direction: ParameterDirection.Output);
            parameters.Add("@m_TotalFixedAssetCummulative", dbType: DbType.Decimal, direction: ParameterDirection.Output);
            parameters.Add("@m_TotalFixedAssetRest", dbType: DbType.Decimal, direction: ParameterDirection.Output);

            var fixedAssets = _dbConnection.Query<FixedAssetViewModel>("Proc_GetFixedAssetPaging", param: parameters, commandType: CommandType.StoredProcedure).ToList();
            totalRecord = parameters.Get<int>("@m_TotalRecord");
            totalPage = parameters.Get<int>("@m_TotalPage");

            totalQuantity = parameters.Get<int>("@m_TotalQuantity");
            totalCost = parameters.Get<decimal>("@m_TotalCost");
            totalFixedAssetCummulative = parameters.Get<decimal>("@m_TotalFixedAssetCummulative");
            totalFixedAssetRest = parameters.Get<decimal>("@m_TotalFixedAssetRest");

            return new { 
                Data = fixedAssets,
                TotalRecord = totalRecord,
                TotalPage = totalPage,
                TotalQuantity = totalQuantity,
                TotalCost = totalCost,
                TotalFixedAssetCummulative = totalFixedAssetCummulative,
                TotalFixedAssetRest = totalFixedAssetRest
            };
        }

        /// <summary>
        /// Method xử lý việc thêm 1 list danh sách tài sản được lấy từ file nhập khẩu vào database
        /// </summary>
        /// <param name="fixedAssets">Danh sách tài sản cần thêm vào database</param>
        /// <returns>Số bản ghị bị ảnh hưởng</returns>
        /// CreatedBy : DDHung (24/11/2021)
        public int ImportFixedAssetDataFromExcel(List<FixedAsset> fixedAssets)
        {
            var rowAffects = 0;
            using (var transaction = _dbConnection.BeginTransaction())
            {
                string processQuery = 
                "INSERT INTO FixedAsset " +
                "(FixedAssetId, FixedAssetCode, FixedAssetName, OrganizationId, OrganizationCode, OrganizationName, DepartmentAssetId, DepartmentAssetCode, DepartmentAssetName, FixedAssetCategoryId, FixedAssetCategoryCode, FixedAssetCategoryName, PurchaseDate, Cost, Quantity, DepreciationRate, TrackedYear, LifeTime, ProductionYear, Active, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate) VALUES " +
                "(@FixedAssetId, @FixedAssetCode , @FixedAssetName , @OrganizationId,@OrganizationCode ,@OrganizationName, @DepartmentAssetId,@DepartmentAssetCode,@DepartmentAssetName, @FixedAssetCategoryId,@FixedAssetCategoryCode, @FixedAssetCategoryName ,@PurchaseDate,@Cost,@Quantity,@DepreciationRate,@TrackedYear,@LifeTime , @ProductionYear , 0, '', @CreatedDate, '', NULL)";
                rowAffects = _dbConnection.Execute(processQuery, fixedAssets);
                transaction.Commit();
            }
            return rowAffects;
        }

        /// <summary>
        /// Lấy ra danh sách phòng ban , sử dụng để lọc dữ liệu từ name phòng bàn để lấy ra id phòng ban tương ứng
        /// phục vụ cho việc import dữ liệu từ file excel
        /// </summary>
        /// CreatedBy : DDHung (07/12/2021)
        /// <returns>Danh sách phòng ban</returns>
        public List<DepartmentAsset> GetDepartmentFixedAsset()
        {
            var departmentAssets = _dbConnection.Query<DepartmentAsset>($"Proc_GetDepartmentAsset", commandType: CommandType.StoredProcedure).ToList();
            return departmentAssets;
        }

        /// <summary>
        /// Lấy ra danh sách loại tài sản , sử dụng để lọc dữ liệu từ name loại tài sản để lấy ra id loại tài sản tương ứng
        /// phục vụ cho việc import dữ liệu từ file excel
        /// </summary>
        /// CreatedBy : DDHung (07/11/2021)
        /// <returns>Danh sách loại tài sản</returns>
        public List<FixedAssetCategory> GetFixedAssetCategory()
        {
            var fixedAssetCategories = _dbConnection.Query<FixedAssetCategory>($"Proc_GetFixedAssetCategory", commandType: CommandType.StoredProcedure).ToList();
            return fixedAssetCategories;
        }

        /// <summary>
        /// Lấy danh sách tài sản dựa vào id ghi tăng
        /// </summary>
        /// <param name="voucherId">Id ghi tăng</param>
        /// <returns>Danh sách tài sản</returns>
        /// CreatedBy : DDHung(15/12/2021)
        public object GetFixedAssetByVoucherId(Guid? voucherId)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@m_VoucherId", voucherId);
            var fixedAssets = _dbConnection.Query<FixedAssetViewModel>("Pro_GetFixedAssetByVoucherId", param: parameters, commandType: CommandType.StoredProcedure).ToList();
            return fixedAssets;
        }

        /// <summary>
        /// Lấy danh sách tài sản chưa được ghi tăng
        /// </summary>
        /// <param name="searchText">Ký tự muốn tìm kiếm</param>
        /// <param name="m_pageSize">Số bản ghi trên một trang</param>
        /// <param name="m_PageIndex">Trang hiện tại</param>
        /// <returns>Danh sách tài sản chưa ghi tăng</returns>
        /// CreatedBy : DDHung (17/12/2021)
        public Object GetFixedAssetNotUsed(string searchText, int pageSize, int pageIndex)
        {
            //Tính lại pageIndex
            pageIndex = (pageIndex - 1) * pageSize;
            //Build tham số đầu vào cho store
            var input = searchText != null ? searchText : string.Empty;
            DynamicParameters parameters = new DynamicParameters();
            var totalRecord = 0;
            var totalPage = 0;

            parameters.Add("@m_FixedAssetName", input);
            parameters.Add("@m_FixedAssetCode", input);
            parameters.Add("@m_PageSize", pageSize);
            parameters.Add("@m_PageIndex", pageIndex);
            parameters.Add("@m_TotalRecord", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("@m_TotalPage", dbType: DbType.Int32, direction: ParameterDirection.Output);

            var fixedAssetNotUsed = _dbConnection.Query<FixedAssetViewModel>("Proc_fixedAssetNotUsed", param: parameters, commandType: CommandType.StoredProcedure).ToList();
            totalRecord = parameters.Get<int>("@m_TotalRecord");
            totalPage = parameters.Get<int>("@m_TotalPage");

            return new
            {
                Data = fixedAssetNotUsed,
                TotalRecord = totalRecord,
                TotalPage = totalPage
            };
        }

        #endregion
    }
}
