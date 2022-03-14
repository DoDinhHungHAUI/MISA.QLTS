using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.ApplicationCore.DTO;
using MISA.ApplicationCore.Entity;
using MISA.ApplicationCore.Interface.Repository;
using MISA.ApplicationCore.Interface.Service;
using MISA.ApplicationCore.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.QLTS.Controllers
{
    public class VouchersController : BaseEntitiesController<Voucher>
    {
        #region Declare
        /// <summary>
        /// Thực hiện việc kết nối CSDL 
        /// </summary>
        IVoucherRepository _voucherRepository;

        /// <summary>
        /// Service ghi tăng thực hiện việc xử lý các nghiệp vụ cần có của ghi tăng
        /// </summary>
        IVoucherService _voucherService;

        private readonly IMapper _mapper;

        #endregion

        #region Constructor
        public VouchersController(IVoucherRepository voucherRepository , IMapper mapper, IVoucherService voucherService) : base(voucherService)
        {
            _mapper = mapper;
            _voucherRepository = voucherRepository;
            _voucherService = voucherService;
        }

        /// <summary>
        /// lấy dữ liệu kết hợp giữa search, paging. 
        /// </summary>
        /// <param name="searchText">Ký tự muốn tìm kiếm</param>
        /// <param name="pageSize">Số lượng record trên 1 trang</param>
        /// <param name="m_PageIndex">Trang hiện tại</param>
        /// <returns>Danh sách chứng từ được tìm thấy</returns>
        /// CreatedBy : DDHung (15/12/2021) 
        [HttpGet("search")]
        public object GetVoucherPaging(string searchText, int pageSize, int pageIndex)
        {
            return _voucherRepository.GetVoucherPaging(searchText, pageSize, pageIndex);
        }

        /// <summary>
        /// Method sinh tự động mã chứng từ mỗi khi thêm một chứng từ mới
        /// </summary>
        /// <returns>Mã chứng từ tự động sinh mới</returns>
        /// CreatedBy : DDHung (17/12/2021)
        [HttpGet("getCode")]
        public IActionResult GetNewCodeVoucher()
        {
            var newCodeFixedAsset = _voucherRepository.GetNewCodeVoucher();
            return Ok(newCodeFixedAsset);
        }

        /// <summary>
        /// API thêm chứng từ
        /// </summary>
        /// <param name="voucherDTO">dữ liệu chứng từ khi thêm</param>
        /// <returns>Thông báo kết quả trả về</returns>

        [HttpPost("insertVoucher")]
        public IActionResult InsertVoucher(VoucherDTO voucherDTO)
        {
            Voucher voucher = _mapper.Map<Voucher>(voucherDTO.voucherViewModel);
            List<FixedAsset> fixedAssets = _mapper.Map<List<FixedAsset>>(voucherDTO.fixedAssetViewModels);

            var serviceResult = _voucherService.InsertVoucher(voucher , fixedAssets);
            if (serviceResult.MisaCode == MISAEnum.NotValid)
            {
                return BadRequest(serviceResult);
            }
            return StatusCode(201, serviceResult);
        }

        /// <summary>
        /// API sửa chứng từ
        /// </summary>
        /// <param name="voucherDTO">dữ liệu chứng từ khi sủa</param>
        /// <returns>Thông báo kết quả trả về</returns>

        [HttpPut("updateVoucher/{id}")]
        public IActionResult UpdateVoucher([FromRoute] string id, [FromBody] VoucherDTO voucherDTO)
        {
            Voucher voucher = _mapper.Map<Voucher>(voucherDTO.voucherViewModel);
            List<FixedAsset> fixedAssets = _mapper.Map<List<FixedAsset>>(voucherDTO.fixedAssetViewModels);

            var keyProperty = voucher.GetType().GetProperty($"{typeof(Voucher).Name}Id");
            if (keyProperty.PropertyType == typeof(Guid))
            {
                keyProperty.SetValue(voucher, Guid.Parse(id));
            }
            else if (keyProperty.PropertyType == typeof(int))
            {
                keyProperty.SetValue(voucher, int.Parse(id));
            }
            else
            {
                keyProperty.SetValue(voucher, id);
            }

            var serviceResult = _voucherService.UpdateVoucher(voucher , fixedAssets);
            if (serviceResult.MisaCode == MISAEnum.NotValid)
            {
                return BadRequest(serviceResult);
            }
            return Ok(serviceResult);
        }

        /// <summary>
        /// Method sử dụng để xóa nhiều bản ghi của bảng chứng từ và cập nhật lại trạng thái của tài sản 
        /// nằm trong danh sách chứng từ vừa xóa đó
        /// </summary>
        /// <param name="listId">Danh sách id của chứng từ</param>
        /// <returns>Trả về số bản ghi ảnh hưởng khi xóa</returns>    
        /// CreatedBy : DDHung (22/12/2021)
        [HttpDelete("deleteMulti")]
        public IActionResult DeleteMultipleRecord([FromQuery] string listId)
        {
            var rowAffect = _voucherRepository.DeleteMultipleRecords(listId);
            return Ok(rowAffect);
        }


        /// <summary>
        /// Xóa chứng từ
        /// </summary>
        /// <param name="voucher">Chứng từ cần sửa</param>
        /// <param name="fixedAssets">Danh sách tài sản ghi tăng</param>
        /// <returns>Thông báo kết quả trả về</returns>
        /// CreatedBy : DDHung (18/12/2021)
        [HttpDelete("deleteVoucher/{id}")]
        public IActionResult DeleteVoucher([FromRoute] string id)
        {
            List<FixedAsset> fixedAssets = _voucherRepository.GetFixedAssetByVoucherId(id);
            var rowAffects = _voucherService.DeleteVoucher(id , fixedAssets);
            return Ok(rowAffects);
        }

      

        #endregion
    }
}
