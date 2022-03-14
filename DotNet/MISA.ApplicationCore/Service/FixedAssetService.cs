
using Microsoft.AspNetCore.Http;
using MISA.ApplicationCore.Entity;
using MISA.ApplicationCore.Interface.Repository;
using MISA.ApplicationCore.Interface.Service;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MISA.ApplicationCore.Service
{
    /// <summary>
    /// Service xử lý nghiệp vụ cho tài sản
    /// </summary>
    public class FixedAssetService : BaseService<FixedAsset>, IFixedAssetService
    {
        #region Declare
        /// <summary>
        /// Thực hiện việc thao tác với database
        /// </summary>
        IFixedAssetRepository _fixedAssetRepository;
        #endregion

        #region Constructor
        public FixedAssetService(IFixedAssetRepository fixedAssetRepository) : base(fixedAssetRepository)
        {
            _fixedAssetRepository = fixedAssetRepository;
        }

        #endregion

        #region Method

        /// <summary>
        /// Thực hiện kiểm tra dữ liệu/nghiệp vụ tùy chỉnh cho tài sản
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>True - dữ liệu hợp lệ,  false - dữ liệu không hợp lệ</returns>
        public override bool ValidateCustom(FixedAsset entity)
        {
            var isValid = true;
            var depreciationRate = (float)Math.Round((1 / (float)entity.LifeTime), 3);
            if (entity.DepreciationRate != depreciationRate)
            {
                _serviceResult.Data = new
                {
                    devMsg = Properties.Resources.Msg_NotValiddDepreciationRate,
                    userMsg = Properties.Resources.Msg_NotValiddDepreciationRate
                };
                _serviceResult.MisaCode = MISAEnum.NotValid;
                _serviceResult.Messeger = Properties.Resources.Msg_NotValiddDepreciationRate;
                isValid = false;
            }
            var depreciationYear = entity.Cost * Convert.ToDecimal(entity.DepreciationRate);
            if (entity.Cost < depreciationYear)
            {
                _serviceResult.Data = new
                {
                    devMsg = Properties.Resources.Msg_NotValidDepreciationYear,
                    userMsg = Properties.Resources.Msg_NotValidDepreciationYear
                };
                _serviceResult.MisaCode = MISAEnum.NotValid;
                _serviceResult.Messeger = Properties.Resources.Msg_NotValidDepreciationYear;
                isValid = false;
            }
            return isValid;
        }

        /// <summary>
        /// Method sử dụng để xóa nhiều bản ghi của bảng tài sản
        /// </summary>
        /// <param name="listId">Danh sách id của tài sản</param>
        /// <returns>Trả về số bản ghi ảnh hưởng khi xóa</returns>
        /// CreatedBy : DĐHung (19/11/2021)
        public int DeleteMultipleRecords(string listId)
        {
            return _fixedAssetRepository.DeleteMultipleRecords(listId);
        }

        /// <summary>
        /// Method sử dụng để nhập khẩu dữ liệu từ file excel
        /// </summary>
        /// <param name="formFile">Tệp nhập khẩu</param>
        /// <param name="cancellationToken">Tham số tùy chọn xử lý đa luồng</param>
        /// <returns>Thông báo kết quả trả về</returns>
        public async Task<ServiceResult> ImportFixedAssetDataFromExcel(IFormFile formFile, CancellationToken cancellationToken)
        {
            if (formFile == null || formFile.Length <= 0)
            {
                _serviceResult.Data = new
                {
                    devMsg = Properties.Resources.Msg_FileExcelEmpty,
                    userMsg = Properties.Resources.Msg_FileExcelEmpty
                };
                _serviceResult.MisaCode = MISAEnum.NotValid;
                _serviceResult.Messeger = Properties.Resources.Msg_FileExcelEmpty;
                return _serviceResult;
            }

            if (!Path.GetExtension(formFile.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
            {
                _serviceResult.Data = new
                {
                    devMsg = Properties.Resources.Msg_NotFileExcel,
                    userMsg = Properties.Resources.Msg_NotFileExcel
                };
                _serviceResult.MisaCode = MISAEnum.NotValid;
                _serviceResult.Messeger = Properties.Resources.Msg_NotFileExcel;
                return _serviceResult;
            }

            var list = new List<FixedAsset>();

            using (var stream = new MemoryStream())
            {
                await formFile.CopyToAsync(stream, cancellationToken);

                using (var package = new ExcelPackage(stream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                    var rowCount = worksheet.Dimension.Rows;
                    var fixedAssetCategories = _fixedAssetRepository.GetFixedAssetCategory() as List<FixedAssetCategory>;
                    var departmentFixedAssets = _fixedAssetRepository.GetDepartmentFixedAsset() as List<DepartmentAsset>;

                    for (int row = 2; row <= rowCount; row++)
                    {
                        FixedAsset fixedAsset = new FixedAsset();
                        fixedAsset.FixedAssetId = Guid.NewGuid();
                        fixedAsset.FixedAssetCode = worksheet.Cells[row, 1].Value.ToString().Trim();
                        fixedAsset.FixedAssetName = worksheet.Cells[row, 2].Value == null ? null : worksheet.Cells[row, 2].Value.ToString().Trim();
                        //fixedAsset.DepartmentAssetId = worksheet.Cells[row, 3].Value == null ? null : Guid.Parse(worksheet.Cells[row, 3].Value.ToString().Trim());

                        var departmentAssetName = worksheet.Cells[row, 3].Value == null ? null : worksheet.Cells[row, 3].Value.ToString().Trim();
                        var deparmentFixedAsset = departmentFixedAssets.Where(item => item.DepartmentAssetCode.ToString().Trim() == departmentAssetName.ToString()).FirstOrDefault();
                        fixedAsset.DepartmentAssetId = Guid.Parse(deparmentFixedAsset.DepartmentAssetId.ToString());

                        var fixedAssetCategoryName = worksheet.Cells[row, 4].Value == null ? null : worksheet.Cells[row, 4].Value.ToString().Trim();
                        var fixedAssetCategory = fixedAssetCategories.Where(item => item.FixedAssetCategoryCode.ToString().Trim() == fixedAssetCategoryName.ToString()).FirstOrDefault();
                        fixedAsset.FixedAssetCategoryId = Guid.Parse(fixedAssetCategory.FixedAssetCategoryId.ToString());

                        //fixedAsset.FixedAssetCategoryId = worksheet.Cells[row, 4].Value == null ? null : Guid.Parse(worksheet.Cells[row, 4].Value.ToString().Trim());
                        fixedAsset.Quantity = worksheet.Cells[row, 5].Value == null ? null : int.Parse(worksheet.Cells[row, 5].Value.ToString().Trim());
                        fixedAsset.Cost = worksheet.Cells[row, 6].Value == null ? 0 : Decimal.Parse(worksheet.Cells[row, 6].Value.ToString().Trim());
                        fixedAsset.DepreciationRate = worksheet.Cells[row, 7].Value == null ? null : float.Parse(worksheet.Cells[row, 7].Value.ToString().Trim());
                        fixedAsset.PurchaseDate = worksheet.Cells[row, 8].Value == null ? DateTime.Now : DateTime.Parse(worksheet.Cells[row, 8].Value.ToString().Trim());
                        fixedAsset.TrackedYear = worksheet.Cells[row, 9].Value == null ? null : int.Parse(worksheet.Cells[row, 9].Value.ToString().Trim());
                        fixedAsset.LifeTime = worksheet.Cells[row, 10].Value == null ? null : int.Parse(worksheet.Cells[row, 10].Value.ToString().Trim());
                        fixedAsset.ProductionYear = worksheet.Cells[row, 11].Value == null ? null : int.Parse(worksheet.Cells[row, 11].Value.ToString().Trim());
                        fixedAsset.CreatedDate = DateTime.Now;
                        list.Add(fixedAsset);
                    }
                }

                //Validate dữ liệu

                //  + Validate các trường required
                var isvalid = ValidateDataFromExcel(list[0], list);
                if (isvalid == false)
                {
                    return _serviceResult;
                }

                //Lưu danh sách dữ liệu xuống database
                _serviceResult.Data = _fixedAssetRepository.ImportFixedAssetDataFromExcel(list);
                _serviceResult.MisaCode = MISAEnum.IsValid;
                return _serviceResult;
            }
        }

        /// <summary>
        /// Method sử dụng để validate dữ liệu từ file excel nhập khẩu
        /// </summary>
        /// <param name="fixedAsset">Thông tin tài sản</param>
        /// <param name="list">Danh sách tài sản</param>
        /// <returns>True - dữ liệu hợp lệ , false - dữ liệu không hợp lệ</returns>
        /// createdBy : DDHung (24/11/2021)
        private bool ValidateDataFromExcel(FixedAsset fixedAsset, List<FixedAsset> list)
        {
            var isValid = true;
            //Đọc các Property : 
            var properties = fixedAsset.GetType().GetProperties();

            //Check required (bắt buộc nhập)
            var listPropertyRequired = new List<String>();
            var isValidRequired = true;

            //Check Duplicate (Trùng lặp dữ liệu )
            var listRowsDuplicateCode = new List<int>();
            var isValidDuplicateCode = true;

            //Check tỷ lệ hao mòn = 1/số năm sử dụng
            var listRowNotvalidDepreciationRate = new List<int>();
            var isValidDepreciationRate = true;

            //Check hao mòn năm < nguyên giá
            var listRowNotvalidDepreciationYear = new List<int>();
            var isValidDepreciationYear = true;

            for (int i = 0; i < list.Count; i++)
            {
                foreach (var property in properties)
                {
                    var propertyValue = property.GetValue(list[i]);
                    //var displayName = property.GetCustomAttributes(typeof(DisplayName), true);
                    var displayName = string.Empty;
                    var displayNameAttributes = property.GetCustomAttributes(typeof(DisplayName), true);
                    if (displayNameAttributes.Length > 0)
                    {
                        displayName = (displayNameAttributes[0] as DisplayName).PropertyName;
                    }

                    //Kiểm tra xem có attribute cần phải validate không : 
                    if (property.IsDefined(typeof(Required), false))
                    {
                        //Check bắt buộc nhập
                        if (propertyValue == null && listPropertyRequired.Contains(displayName) == false)
                        {
                            isValidRequired = false;
                            isValid = false;
                            listPropertyRequired.Add(displayName);
                        }
                    }

                    //Check trùng dữ liệu
                    if (property.IsDefined(typeof(CheckDuplicate), false))
                    {
                        var entityDuplicate = _baseRepository.GetEntityByProperty(list[i], property);
                        if (entityDuplicate != null)
                        {
                            isValid = false;
                            isValidDuplicateCode = false;
                            listRowsDuplicateCode.Add(i);
                        }
                    }
                }
                if (isValid == true)
                {
                    //check tỷ lệ hao mòn
                    if (list[i].DepreciationRate != (1 / (float)list[i].LifeTime))
                    {
                        listRowNotvalidDepreciationRate.Add(i);
                        isValidDepreciationRate = false;
                        isValid = false;
                    }
                    //Check hao mòn năm

                    var depreciationYear = list[i].Cost * Convert.ToDecimal(list[i].DepreciationRate);
                    if (list[i].Cost < depreciationYear)
                    {
                        listRowNotvalidDepreciationYear.Add(i);
                        isValid = false;
                        isValidDepreciationYear = false;
                    }
                }
            }
            if (isValidRequired == false)
            {
                //Thông báo kết quả trả về các trường required chưa được nhập trong tệp nhập khẩu
                var messageNotValid = "";
                foreach (var item in listPropertyRequired)
                {
                    messageNotValid += item + " - ";
                }
                messageNotValid = string.Format(Properties.Resources.Msg_NotValidRequiredFileExcel, messageNotValid.Substring(0, messageNotValid.Length - 2));
                _serviceResult.Data = new
                {
                    devMsg = messageNotValid,
                    userMsg = messageNotValid
                };
                _serviceResult.MisaCode = MISAEnum.NotValid;
                _serviceResult.Messeger = Properties.Resources.Msg_IsNotValid;
                return false;
            }
            else if (isValidDuplicateCode == false)
            {
                //Thông báo kết quả trả về khi mã tài sản ở tệp nhập khẩu bị duplicate
                var messageNotValid = "";
                foreach (var item in listRowsDuplicateCode)
                {
                    messageNotValid += (item + 2) + " - ";
                }
                messageNotValid = string.Format(Properties.Resources.Msg_DuplicateDataFileExcel, messageNotValid.Substring(0, messageNotValid.Length - 2));
                _serviceResult.Data = new
                {
                    devMsg = messageNotValid,
                    userMsg = messageNotValid
                };
                _serviceResult.MisaCode = MISAEnum.NotValid;
                _serviceResult.Messeger = Properties.Resources.Msg_IsNotValid;
                return false;
            }
            else if (isValidDepreciationYear == false)
            {
                //Thông báo kết quả trả về khi hao mòn năm lớn hơn nguyên giá
                var messageNotValid = "";
                foreach (var item in listRowNotvalidDepreciationYear)
                {
                    messageNotValid += (item + 2) + " - ";
                }
                messageNotValid = string.Format(Properties.Resources.Msg_NotValidDepreciationYearFromExcel, messageNotValid.Substring(0, messageNotValid.Length - 2));
                _serviceResult.Data = new
                {
                    devMsg = messageNotValid,
                    userMsg = messageNotValid
                };
                _serviceResult.MisaCode = MISAEnum.NotValid;
                _serviceResult.Messeger = Properties.Resources.Msg_IsNotValid;
                return false;
            }
            else if (isValidDepreciationRate == false)
            {
                //Thông báo kết quả trả về khi Tỷ lệ hao mòn khác 1/số năm sử dụng
                var messageNotValid = "";
                foreach (var item in listRowNotvalidDepreciationRate)
                {
                    messageNotValid += (item + 2) + " - ";
                }
                messageNotValid = string.Format(Properties.Resources.Msg_NotValidDepreciationRateFromExcel, messageNotValid.Substring(0, messageNotValid.Length - 2));
                _serviceResult.Data = new
                {
                    devMsg = messageNotValid,
                    userMsg = messageNotValid
                };
                _serviceResult.MisaCode = MISAEnum.NotValid;
                _serviceResult.Messeger = Properties.Resources.Msg_IsNotValid;
                return false;
            }
            return true;
        }

        #endregion


        /// <summary>
        /// Method lấy ra id từ một danh sách dựa vào name
        /// </summary>
        /// <returns>Trả về object được tìm thấy</returns>
        //Object getIdFromListByName(List<Object> listToFindId , string name)
        //{
        //    var result = listToFindId.Find(item => item.abc == name)
        //}

    }
}
