using Microsoft.AspNetCore.Http;
using MISA.ApplicationCore.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MISA.ApplicationCore.Interface.Service
{
    public interface IFixedAssetService : IBaseService<FixedAsset>
    {
        /// <summary>
        /// Method sử dụng để xóa nhiều bản ghi của bảng tài sản
        /// </summary>
        /// <param name="listId">Danh sách id của tài sản</param>
        /// <returns>Trả về số bản ghi ảnh hưởng khi xóa</returns>
        int DeleteMultipleRecords(string listId);

        /// <summary>
        /// Method Thực hiện việc nhập khẩu dữ liệu từ file excel
        /// </summary>
        /// <param name="formFile">Tệp nhập khẩu</param>
        /// <param name="cancellationToken">Tham số tùy chọn xử lý đa luồng</param>
        /// <returns>Số dòng bị ảnh hưởng</returns>
        /// CreatedBy : DDHung (23/11/2021)
        Task<ServiceResult> ImportFixedAssetDataFromExcel(IFormFile formFile, CancellationToken cancellationToken);
    }
}
