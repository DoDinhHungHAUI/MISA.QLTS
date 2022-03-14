using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.ApplicationCore.DTO;
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
    /// Repository ghi tăng thực hiện các kết nối với Database
    /// </summary>
    public class VoucherRepository : BaseRepository<Voucher>, IVoucherRepository
    {
        #region Constructor
        public VoucherRepository(IConfiguration configuration) : base(configuration)
        {

        }
        #endregion

        #region method

        /// <summary>
        /// Lọc dữ liệu kết hợp giữa search , filter, paging. 
        /// </summary>
        /// <param name="searchText">Ký tự muốn tìm kiếm (số chứng từ và nội dung) </param>
        /// <param name="pageSize">Số lượng record trên 1 trang</param>
        /// <param name="m_PageIndex">Trang hiện tại</param>
        /// <returns>Danh sách chứng từ được tìm thấy</returns>
        /// CreatedBy : DDHung (15/12/2021)
        public object GetVoucherPaging(string searchText, int pageSize, int pageIndex)
        {
            //Tính lại pageIndex
            pageIndex = (pageIndex - 1) * pageSize;
            //Build tham số đầu vào cho store
            var input = searchText != null ? searchText : string.Empty;
            DynamicParameters parameters = new DynamicParameters();
            var totalRecord = 0;
            var totalPage = 0;
            var totalCost = default(decimal);
            parameters.Add("@m_VouchersCode", input);
            parameters.Add("@m_Description", input);
            parameters.Add("@m_PageSize", pageSize);    
            parameters.Add("@m_PageIndex", pageIndex);
            parameters.Add("@m_TotalRecord", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("@m_TotalPage", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("@m_TotalCost", dbType: DbType.Decimal, direction: ParameterDirection.Output);
            var usedFixedAssets = _dbConnection.Query<VoucherViewModel>("Proc_GetVoucherPaging", param: parameters, commandType: CommandType.StoredProcedure).ToList();
            totalRecord = parameters.Get<int>("@m_TotalRecord");
            totalPage = parameters.Get<int>("@m_TotalPage");
            totalCost = parameters.Get<decimal>("@m_TotalCost");
            return new
            {
                Data = usedFixedAssets,
                TotalRecord = totalRecord,
                TotalPage = totalPage,
                TotalCost = totalCost,
            };
        }

        /// <summary>
        /// Method lấy mã sinh tự động của Chứng từ
        /// </summary>
        /// <returns>Mã mới được sinh tự động</returns>
        /// CreatedBy : DDHung (17/12/2021)
        public string GetNewCodeVoucher()
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("newVoucherCode", dbType: DbType.String, direction: ParameterDirection.Output);
            _dbConnection.Query<string>("Proc_GetNewVoucherCode", param: parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
            var newVoucherCode = parameters.Get<string>("newVoucherCode");
            return newVoucherCode;
        }

        /// <summary>
        /// Thêm chứng từ
        /// </summary>
        /// <param name="voucher">Chứng từ cần thêm</param>
        /// <param name="fixedAssets">Danh sách tài sản ghi tăng</param>
        /// <returns>Số dòng chứng từ bị ảnh hưởng</returns>
        /// CreatedBy : DDHung (18/12/2021)
        public int InsertVoucher(Voucher voucher , List<FixedAsset> fixedAssets)
        {
            var rowAffects = 0;
            using (var transaction = _dbConnection.BeginTransaction())
            {
                var dynamicProps = new DynamicParameters();

                //Lấy id tự động
                voucher.VoucherId = System.Guid.NewGuid();

                //Lấy ra các properties của đối tượng
                var props = voucher.GetType().GetProperties();
                //Duyệt từng propertity
                foreach (var prop in props)
                {
                    //Lấy tên của property
                    var propName = prop.Name;
                    var propValue = prop.GetValue(voucher);
                    var param = propName;
                    dynamicProps.Add(param, propValue);
                }

                rowAffects = _dbConnection.Execute($"Proc_Insert{_tableName}", dynamicProps, commandType: CommandType.StoredProcedure);

                //string processQuery =
                //"UPDATE FixedAsset " +
                //"(FixedAssetId, FixedAssetCode, FixedAssetName, OrganizationId, OrganizationCode, OrganizationName, DepartmentAssetId, DepartmentAssetCode, DepartmentAssetName, FixedAssetCategoryId, FixedAssetCategoryCode, FixedAssetCategoryName, PurchaseDate, Cost, Quantity, DepreciationRate, TrackedYear, LifeTime, ProductionYear, Active, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate) VALUES " +
                //"(@FixedAssetId, @FixedAssetCode , @FixedAssetName , @OrganizationId,@OrganizationCode ,@OrganizationName, @DepartmentAssetId,@DepartmentAssetCode,@DepartmentAssetName, @FixedAssetCategoryId,@FixedAssetCategoryCode, @FixedAssetCategoryName ,@PurchaseDate,@Cost,@Quantity,@DepreciationRate,@TrackedYear,@LifeTime , @ProductionYear , 0, '', @CreatedDate, '', NULL)";
                //rowAffects = _dbConnection.Execute(processQuery, fixedAssets);
                string processQuery = "UPDATE FixedAsset fa SET fa.VoucherId = '" + voucher.VoucherId + "' , fa.StatusFixedAsset = 2 WHERE fa.FixedAssetId = @FixedAssetId";
                var rowAffectFixedAsset = _dbConnection.Execute(processQuery, fixedAssets);
                transaction.Commit();
            }
            return rowAffects;
        }

        /// <summary>
        /// Sửa chứng từ
        /// </summary>
        /// <param name="voucher">Chứng từ cần sửa</param>
        /// <param name="fixedAssets">Danh sách tài sản ghi tăng</param>
        /// <returns>Thông báo kết quả trả về</returns>
        /// CreatedBy : DDHung (18/12/2021)
        public int UpdateVoucher(Voucher voucher, List<FixedAsset> fixedAssets)
        {
            var rowAffects = 0;
            using (var transaction = _dbConnection.BeginTransaction())
            {
                //var dynamicProps = new DynamicParameters();
                ////Lấy ra các properties của đối tượng
                //var props = voucher.GetType().GetProperties();
                ////Duyệt từng propertity
                //foreach (var prop in props)
                //{
                //    //Lấy tên của property
                //    var propName = prop.Name;
                //    var propValue = prop.GetValue(voucher);
                //    var param = propName;
                //    dynamicProps.Add(param, propValue);
                //}

                //rowAffects = _dbConnection.Execute($"Proc_Update{_tableName}", dynamicProps, commandType: CommandType.StoredProcedure);

                rowAffects = UpdateVoucherEdit(voucher);

                List<FixedAsset> fixedAssetsByVoucherId = GetFixedAssetByVoucherId(voucher.VoucherId);

                UpdateStatusFixedAssetBefore(fixedAssetsByVoucherId);

                UpdateStatusFixedAssetAfter(fixedAssets , voucher);

                //string processQuery = "UPDATE FixedAsset fa SET fa.VoucherId = '" + voucher.VoucherId + "' , fa.StatusFixedAsset = 2 , fa.Cost = @Cost WHERE fa.FixedAssetId = @FixedAssetId";
                //var rowAffectFixedAsset = _dbConnection.Execute(processQuery, fixedAssets);
                transaction.Commit();
            }
            return rowAffects;
        }


        /// <summary>
        /// Cập nhật lại tất cả danh sách tài sản ghi tăng của chứng từ sau khi khi thay đổi chứng từ có trạng thại là 2 (đang sử dụng )
        /// </summary>
        /// <param name="fixedAssets">Danh sách tài sản ghi tăng của chứng từ sau khi thay đổi chứng từ</param>
        /// CreatedBy : DDHung (23/12/2021)
        private void UpdateStatusFixedAssetAfter(List<FixedAsset> fixedAssets , Voucher voucher)
        {
            string processQuery = "UPDATE FixedAsset fa SET fa.VoucherId = '" + voucher.VoucherId + "' , fa.StatusFixedAsset = 2 WHERE fa.FixedAssetId = @FixedAssetId";
            _dbConnection.Execute(processQuery, fixedAssets);
        }


        /// <summary>
        /// Cập nhật lại tất cả danh sách tài sản ghi tăng của chứng từ trước khi thay đổi chứng từ có trạng thại là 1 (chưa sử dụng )
        /// </summary>
        /// <param name="fixedAssetsByVoucherId">Danh sách tài sản ghi tăng của chứng từ trước khi thay đổi chứng từ</param>
        /// CreatedBy : DDHung (23/12/2021)
        private void UpdateStatusFixedAssetBefore(List<FixedAsset> fixedAssetsByVoucherId)
        {
            string processQuery = "UPDATE FixedAsset fa SET fa.VoucherId = '" + Guid.NewGuid() + "' , fa.StatusFixedAsset = 1 WHERE fa.FixedAssetId = @FixedAssetId";
             _dbConnection.Execute(processQuery, fixedAssetsByVoucherId);
        }


        /// <summary>
        /// Lấy danh sách tài sản dựa vào id chứng từ
        /// </summary>
        /// <param name="voucherId">Id chứng từ</param>
        /// <returns>Danh sách tài sản</returns>
        /// CreatedBy : DDHung(15/12/2021)
        public List<FixedAsset> GetFixedAssetByVoucherId(Guid? voucherId)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@m_VoucherId", voucherId);
            List<FixedAsset> fixedAssets = _dbConnection.Query<FixedAsset>("Pro_GetFixedAssetByVoucherId", param: parameters, commandType: CommandType.StoredProcedure).ToList();
            return fixedAssets;
        }



        /// <summary>
        /// Cập nhật lại chứng từ mỗi khi sửa
        /// </summary>
        /// <param name="voucher">Chứng từ sửa</param>
        /// <returns>số bản ghi bị ảnh hưởng</returns>
        /// CreatedBy : DDHung(23/12/2021)
        private int UpdateVoucherEdit(Voucher voucher)
        {
            var rowAffects = 0;
            var dynamicProps = new DynamicParameters();
            //Lấy ra các properties của đối tượng
            var props = voucher.GetType().GetProperties();
            //Duyệt từng propertity
            foreach (var prop in props)
            {
                //Lấy tên của property
                var propName = prop.Name;
                var propValue = prop.GetValue(voucher);
                var param = propName;
                dynamicProps.Add(param, propValue);
            }
            rowAffects = _dbConnection.Execute($"Proc_Update{_tableName}", dynamicProps, commandType: CommandType.StoredProcedure);
            return rowAffects;
        }


        /// <summary>
        /// Xóa chứng từ
        /// </summary>
        /// <param name="voucher">Chứng từ cần sửa</param>
        /// <param name="fixedAssets">Danh sách tài sản ghi tăng</param>
        /// <returns>Thông báo kết quả trả về</returns>
        /// CreatedBy : DDHung (18/12/2021)
        public int DeleteVoucher(string id, List<FixedAsset> fixedAssets)
        {
            var rowAffects = 0;
            using (var transaction = _dbConnection.BeginTransaction())
            {
                //string processQuery = "UPDATE FixedAsset fa SET fa.VoucherId = '" + null + "' , fa.StatusFixedAsset = 1 WHERE fa.FixedAssetId = @FixedAssetId";
                //var rowAffectFixedAsset = _dbConnection.Execute(processQuery, fixedAssets);

                var queryString = $"delete from {_tableName} where {_tableName}Id = @entityId";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@entityId", id);
                rowAffects = _dbConnection.Execute(queryString, param: parameters, commandType: CommandType.Text);

                //string processQuery = "UPDATE FixedAsset fa SET fa.VoucherId = '" + null + "' , fa.StatusFixedAsset = 1 WHERE fa.FixedAssetId = @FixedAssetId";
                string processQuery = "UPDATE FixedAsset fa SET fa.StatusFixedAsset = 1 WHERE fa.FixedAssetId = @FixedAssetId";
                var rowAffectFixedAsset = _dbConnection.Execute(processQuery, fixedAssets);
                transaction.Commit();
            }
            return rowAffects;
        }   
        
        /// <summary>
        /// Lấy danh sách tài sản dựa vào id chứng từ
        /// </summary>
        /// <param name="id">Id chứng từ</param>
        /// <returns>Danh sách tài sản</returns>
        public List<FixedAsset> GetFixedAssetByVoucherId(string id)
        {
            var queryString = $"select * from FixedAsset where VoucherId = @voucherId";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@voucherId", id);
            List<FixedAsset> fixedAssets = _dbConnection.Query<FixedAsset>(queryString, param: parameters, commandType: CommandType.Text).ToList();
            return fixedAssets;
        }


        /// <summary>
        /// Method sử dụng để xóa nhiều bản ghi của bảng chứng từ và cập nhật lại trạng thái của tài sản 
        /// nằm trong danh sách chứng từ vừa xóa đó
        /// </summary>
        /// <param name="listId">Danh sách id của chứng từ</param>
        /// <returns>Trả về số bản ghi ảnh hưởng khi xóa</returns>    
        /// CreatedBy : DDHung (22/12/2021)
        public int DeleteMultipleRecords(string listId)
        {
            var rowAffects = 0;
            using (var transaction = _dbConnection.BeginTransaction())
            {
                rowAffects = DeleteMultipleVouchers(listId);
                UpdateStatusFixedAssets(listId);
                transaction.Commit();
            }
            return rowAffects;
        }    


        /// <summary>
        /// Method thực hiện việc xóa nhiều bản ghi chứng từ dựa vào danh sách id chứng từ
        /// </summary>
        /// <param name="listId">Danh sách id chứng từ</param>
        /// <returns>Số bản ghi bị ảnh hưởng</returns>
        /// CreatedBy : DDHung (22/12/2021)
        private int DeleteMultipleVouchers(string listId)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("listId", listId);
            var rowAffect = _dbConnection.Execute("Proc_DeleteMultipleVoucher", param: parameters, commandType: CommandType.StoredProcedure);
            return rowAffect;
        }


        /// <summary>
        /// Method thực hiện việc cập nhật trạng thái của tài sản dựa vào danh sách id chứng từ
        /// </summary>
        /// <param name="listId">Danh sách id chứng từ</param>
        /// <returns>Số bản ghi bị ảnh hưởng sau khi cập nhật</returns>
        /// CreatedBy : DDHung (22/12/2021)
        private void UpdateStatusFixedAssets(string listId)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("listId", listId);
            _dbConnection.Execute("Proc_UpdateStatusFixedAssetByMuitipleVoucherId", param: parameters, commandType: CommandType.StoredProcedure);
        }


        #endregion
    }
}
