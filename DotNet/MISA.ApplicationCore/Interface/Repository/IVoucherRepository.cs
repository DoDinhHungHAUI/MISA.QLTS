using MISA.ApplicationCore.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.ApplicationCore.Interface.Repository
{
    public interface  IVoucherRepository : IBaseRepository<Voucher>
    {
        /// <summary>
        /// Method lấy mã sinh tự động của Chứng từ
        /// </summary>
        /// <returns>Mã mới được sinh tự động</returns>
        /// CreatedBy : DDHung (17/12/2021)
        string GetNewCodeVoucher();

        /// <summary>
        /// Lấy dữ liệu kết hợp giữa search , paging. 
        /// </summary>
        /// <param name="searchText">Ký tự muốn tìm kiếm (số chứng từ và nội dung)</param>
        /// <param name="pageSize">Số lượng record trên 1 trang</param>
        /// <param name="m_PageIndex">Trang hiện tại</param>
        /// <returns>Danh sách chứng từ được tìm thấy</returns>
        /// CreatedBy : DDHung (19/11/2021)
        Object GetVoucherPaging(string searchText, int pageSize, int m_PageIndex);

        /// <summary>
        /// Thêm chứng từ
        /// </summary>
        /// <param name="voucher">Chứng từ cần thêm</param>
        /// <param name="fixedAssets">Danh sách tài sản ghi tăng</param>
        /// <returns>Số dòng chứng từ bị ảnh hưởng</returns>
        /// CreatedBy : DDHung (18/12/2021)
        public int InsertVoucher(Voucher voucher, List<FixedAsset> fixedAssets);


        /// <summary>
        /// Sửa chứng từ
        /// </summary>
        /// <param name="voucher">Chứng từ cần sửa</param>
        /// <param name="fixedAssets">Danh sách tài sản ghi tăng</param>
        /// <returns>Thông báo kết quả trả về</returns>
        /// CreatedBy : DDHung (18/12/2021)
        public int UpdateVoucher(Voucher voucher, List<FixedAsset> fixedAssets);


        /// <summary>
        /// Xóa chứng từ
        /// </summary>
        /// <param name="voucher">Chứng từ cần sửa</param>
        /// <param name="fixedAssets">Danh sách tài sản ghi tăng</param>
        /// <returns>Thông báo kết quả trả về</returns>
        /// CreatedBy : DDHung (18/12/2021)
        public int DeleteVoucher(string id, List<FixedAsset> fixedAssets);

        /// <summary>
        /// Lấy danh sách tài sản dựa vào id chứng từ
        /// </summary>
        /// <param name="id">Id chứng từ</param>
        /// <returns>Danh sách tài sản</returns>
        public List<FixedAsset> GetFixedAssetByVoucherId(string id);


        /// <summary>
        /// Method sử dụng để xóa nhiều bản ghi của bảng chứng từ và cập nhật lại trạng thái của tài sản 
        /// nằm trong danh sách chứng từ vừa xóa đó
        /// </summary>
        /// <param name="listId">Danh sách id của chứng từ</param>
        /// <returns>Trả về số bản ghi ảnh hưởng khi xóa</returns>    
        /// CreatedBy : DDHung (22/12/2021)
        int DeleteMultipleRecords(string listId);

    }
}
