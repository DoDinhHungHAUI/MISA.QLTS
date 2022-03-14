using MISA.ApplicationCore.Entity;
using MISA.ApplicationCore.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.ApplicationCore.Interface.Repository
{
    public interface IFixedAssetRepository : IBaseRepository<FixedAsset>
    {
        /// <summary>
        /// Method lấy mã sinh tự động của tài sản
        /// </summary>
        /// <returns>Mã mới được sinh tự động</returns>
        string GetNewCodeFixedAsset();
        
        /// <summary>
        /// Method sử dụng để xóa nhiều bản ghi của bảng tài sản
        /// </summary>
        /// <param name="listId">Danh sách id của tài sản</param>
        /// <returns>Trả về số bản ghi ảnh hưởng khi xóa</returns>       
        int DeleteMultipleRecords(string listId);

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
        Object GetFixedAssetPaging(string searchText, Guid? departmentFixedAssetId, Guid? fixedAssetCategoryId, int m_PageSize, int m_PageIndex);

        /// <summary>
        /// Method xử lý việc thêm 1 list danh sách tài sản được lấy từ file nhập khẩu vào database
        /// </summary>
        /// <param name="fixedAssets">Danh sách tài sản cần thêm vào database</param>
        /// <returns>Số bản ghị bị ảnh hưởng</returns>
        /// CreatedBy : DDHung (24/11/2021)
        int ImportFixedAssetDataFromExcel(List<FixedAsset> fixedAssets);

        /// <summary>
        /// Lấy ra danh sách phòng ban , sử dụng để lọc dữ liệu từ name phòng bàn để lấy ra id phòng ban tương ứng
        /// phục vụ cho việc import dữ liệu từ file excel
        /// </summary>
        /// <returns>Danh sách phòng ban</returns>
        /// CreatedBy : DDHung (07/12/2021)
        List<DepartmentAsset> GetDepartmentFixedAsset();

        /// <summary>
        /// Lấy ra danh sách loại tài sản , sử dụng để lọc dữ liệu từ name loại tài sản để lấy ra id loại tài sản tương ứng
        /// phục vụ cho việc import dữ liệu từ file excel
        /// </summary>
        /// <returns>Danh sách loại tài sản</returns>
        /// CreatedBy : DDHung (07/12/2021)
        List<FixedAssetCategory> GetFixedAssetCategory();

        /// <summary>
        /// Lấy danh sách tài sản dựa vào id ghi tăng
        /// </summary>
        /// <param name="idUsedFixedAsset">Id ghi tăng</param>
        /// <returns>Danh sách tài sản</returns>
        /// CreatedBy : DDHung(15/12/2021)
        Object GetFixedAssetByVoucherId(Guid? idUsedFixedAsset);

        /// <summary>
        /// Lấy danh sách tài sản chưa được ghi tăng
        /// </summary>
        /// <param name="searchText">Ký tự muốn tìm kiếm</param>
        /// <param name="m_pageSize">Số bản ghi trên một trang</param>
        /// <param name="m_PageIndex">Trang hiện tại</param>
        /// <returns>Danh sách tài sản chưa ghi tăng</returns>
        /// CreatedBy : DDHung (17/12/2021)
        Object GetFixedAssetNotUsed(string searchText , int m_pageSize, int m_PageIndex);
    }
}
