using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.ApplicationCore.Entity;
using MISA.ApplicationCore.Interface.Repository;
using MISA.ApplicationCore.Interface.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MISA.QLTS.Controllers
{
    /// <summary>
    /// API tài sản
    /// </summary>
    public class FixedAssetsController : BaseEntitiesController<FixedAsset>
    {
        #region Declare
        /// <summary>
        /// Thực hiện việc kết nối CSDL 
        /// </summary>
        IFixedAssetRepository _fixedAssetRepository;

        /// <summary>
        /// Service tài sản thực hiện việc xử lý các nghiệp vụ cần có của tài sản
        /// </summary>
        IFixedAssetService _fixedAssetService;
        #endregion
        #region Constructor
        public FixedAssetsController(IFixedAssetRepository fixedAssetRepository, IFixedAssetService fixedAssetService) : base(fixedAssetService)
        {
            _fixedAssetRepository = fixedAssetRepository;
            _fixedAssetService = fixedAssetService;
        }
        #endregion
        #region Method

        /// <summary>
        /// Method Lấy danh sách tài sản
        /// </summary>
        /// <returns>Trả về danh sách tài sản</returns>
        /// CreatedBy : DDHung (17/11/2021)
        public override IActionResult Get()
        {
            var fixedAssets = _fixedAssetRepository.GetEntities();
            return Ok(fixedAssets);
        }

        /// <summary>
        /// Method sinh tự động mã tài sản mỗi khi thêm một tài sản mới
        /// </summary>
        /// <returns>Mã tài sản tự động sinh mới</returns>
        /// CreatedBy : DDHung (17/11/2021)
        [HttpGet("getCode")]
        public IActionResult GetNewCodeFixedAsset()
        {
            var newCodeFixedAsset = _fixedAssetRepository.GetNewCodeFixedAsset();
            return Ok(newCodeFixedAsset);
        }

        /// <summary>
        /// Method sử dụng để xóa nhiều bản ghi của bảng tài sản
        /// </summary>
        /// <param name="listId">Danh sách id của tài sản</param>
        /// <returns>Trả về số bản ghi ảnh hưởng khi xóa</returns>
        /// CreatedBy : DĐHung (19/11/2021)
        [HttpDelete("deleteMulti")]
        public IActionResult DeleteMultipleRecord([FromQuery] string listId)
        {
            var rowAffect = _fixedAssetService.DeleteMultipleRecords(listId);
            return Ok(rowAffect);
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
        [HttpGet("search")]
        public object GetFixedAssetPaging(string searchText, Guid? departmentFixedAssetId, Guid? fixedAssetCategoryId, int pageSize, int PageIndex)
        {
            return _fixedAssetRepository.GetFixedAssetPaging(searchText, departmentFixedAssetId, fixedAssetCategoryId, pageSize, PageIndex);
        }

        /// <summary>
        /// Lấy danh sách tài sản dựa vào id ghi tăng
        /// </summary>
        /// <param name="idUsedFixedAsset">Id ghi tăng</param>
        /// <returns>Danh sách tài sản</returns>
        /// CreatedBy : DDHung(15/12/2021)
        [HttpGet("fixedAsset")]
        public object GetFixedAssetByVoucherId(Guid? voucherId)
        {
            return _fixedAssetRepository.GetFixedAssetByVoucherId(voucherId);
        }

        /// <summary>
        /// Lấy danh sách tài sản chưa được ghi tăng
        /// </summary>
        /// <param name="searchText">Ký tự muốn tìm kiếm</param>
        /// <param name="m_pageSize">Số bản ghi trên một trang</param>
        /// <param name="m_PageIndex">Trang hiện tại</param>
        /// <returns>Danh sách tài sản chưa ghi tăng</returns>
        /// CreatedBy : DDHung (17/12/2021)
        [HttpGet("fixedAssetNotUsed")]
        public object GetFixedAssetNotUsed(string searchText, int pageSize, int pageIndex)
        {
            return _fixedAssetRepository.GetFixedAssetNotUsed(searchText , pageSize , pageIndex);
        }

        /// <summary>
        /// APT Thực hiện việc nhập khẩu dữ liệu từ file excel
        /// </summary>
        /// <param name="formFile">Tệp nhập khẩu</param>
        /// <param name="cancellationToken">Tham số tùy chọn xử lý đa luồng</param>
        /// <returns>201 : Nhập khẩu thành công</returns>
        /// CreatedBy : DDHung (23/11/2021)

        [HttpPost("import")]
        public async Task<IActionResult> ImportDataFromFileExcel(IFormFile formFile, CancellationToken cancellationToken)
        {
            var serviceResult = await _fixedAssetService.ImportFixedAssetDataFromExcel(formFile, cancellationToken);

            if (serviceResult.MisaCode == MISAEnum.NotValid)
            {
                return BadRequest(serviceResult);
            }
            return StatusCode(201, serviceResult);
        }
        
        #endregion
    }
}
