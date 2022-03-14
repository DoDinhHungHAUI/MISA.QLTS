

export const Resource = {
    VN : {
        Validate : {
            Required : function(nameToValidate) {
                return `Cần phải nhập thông tin ${nameToValidate}`;
            },
            MaxLenght : function(nameToValidate , maxLength)
            {
                return `Thông tin ${nameToValidate} không được vượt quá ${maxLength} ký tự`;
            },
            NoContentGridDialog : "Không có tài sản nào được chọn.Vui lòng chọn tài sản"
        },
        MessageDialogConfirm : {
            MessageFixedAssetUsed : function(fixedAssetCode , voucherCode){
                return `Tài sản <span class = "highline-text-confirm">${fixedAssetCode}</span> đã phát sinh chứng từ ghi tăng .Tại chứng từ <span class = "highline-text-confirm">${voucherCode}</span>`
            },
            ConfirmHidePopupFixedAsset : "Bạn có muốn hủy bỏ khai báo tài sản này?",
            ConfirmSaveInfoFixedAsset : "Thông tin thay đổi sẽ không được cập nhật nếu bạn không lưu.Bạn có muốn lưu các thay đổi này?",
            ConfirmSaveInfo : "Thông tin thay đổi sẽ không được cập nhật nếu bạn không lưu.Bạn có muốn lưu các thay đổi này?",
            DeleteConfirmOneRecord : function(fixedAssetCode , fixedAssetName) {
                return `Bạn có muốn xóa tài sản <span class = "highline-text-confirm"> ${fixedAssetCode} - ${fixedAssetName} ? </span>`
            },
            DeleteConfirm : function( GridName, Code , Name) {
                if(Name)
                {
                    return `Bạn có muốn xóa ${GridName} <span class = "highline-text-confirm"> ${Code} - ${Name} ? </span>`
                }
                return `Bạn có muốn xóa ${GridName} <span class = "highline-text-confirm"> ${Code} ? </span>`
            },
            DeleteMultipleRecord : function(numberRecordDelete , tableName)
            {
                return `<span class = "highline-text-confirm">${numberRecordDelete}</span> ${tableName} đã được chọn. Bạn có muốn xóa các ${tableName} này ra khỏi danh sách?`
            }
        },
        MessageDialogValidate : {
            Required : "Cần phải nhập thông tin"
        },
        ValidateDialogOrigin : {
            Required : "Vui lòng nhập giá trị",
            Choosed : "Nguồn kinh phí này đã được chọn",
            RequiredCombobox : "Vui lòng chọn nguồn kinh phí"
        }
    },
    API : {
        Host : "https://localhost:44376",
        Departments : "/api/v1/DepartmentAssets",
        FixedAssetCategories : "/api/v1/FixedAssetCategories",
        FixedAssets : '/api/v1/FixedAssets',
        Vouchers : '/api/v1/Vouchers',
        GetNewCodeFixedAsset : "/getCode",
        GetNewCodeVoucher : "/getCode",
        ImportDataFromExcel : "/import",
        InsertVoucher : '/insertVoucher',
        UpdateVoucher : '/updateVoucher',
        OriginFunding : '/api/v1/OriginFunding',
        OriginFundingFixedAsset : '/api/v1/OriginFundingFixedAsset',
        updateOriginFunds : '/api/v1/OriginFundingFixedAsset/updateOriginFund',
        
        getOriginFundingByFixedAssetId : function(fixedAssetId)
        {
            return `/originFunds?fixedAssetId=${fixedAssetId}`
        },
        searchFixedAssets : function(txtSearch , DepartmentAssetId , FixedAssetCategoryId , pageSize , PageIndex)
        {
            var departmentAssetIdToSearch = DepartmentAssetId != null ? `departmentFixedAssetId=${DepartmentAssetId}` : "departmentFixedAssetId";
            var fixedAssetCategoryIdToSearch = FixedAssetCategoryId != null ? `fixedAssetCategoryId=${FixedAssetCategoryId}` : "fixedAssetCategoryId";
            return `/search?searchText=${txtSearch}&${departmentAssetIdToSearch}&${fixedAssetCategoryIdToSearch}&pageSize=${pageSize}&PageIndex=${PageIndex}`
        },

        searchVouchers : function(txtSearch , pageSize , pageIndex)
        {
            return `/search?searchText=${txtSearch}&pageSize=${pageSize}&pageIndex=${pageIndex}`
        },

        searchFixedAssetChoose : function(txtSearch , pageSize , pageIndex)
        {
            return `/fixedAssetNotUsed?searchText=${txtSearch}&pageSize=${pageSize}&pageIndex=${pageIndex}`
        },

        getFixedAssetByVoucherId : function(voucherId)
        {
            return `/fixedAsset?voucherId=${voucherId}`
        },

        deleteVoucher : function(voucherId)
        {
            return `/deleteVoucher/${voucherId}`
        },
    },
    SearchPaging : {
        PageSize : 40,
        PageIndex : 1,
        TxtSearch : '',
        PageSizeVoucherDialog : 10
    },

    ContextMenu : {
        FixedAsset : {
            add : "Thêm tài sản",
            edit : "Sửa tài sản",
            delete : "Xóa tài sản"
        },
        Vouchers : {
            add : "Thêm chứng từ",
            edit : "Sửa chứng từ",
            delete : "Xóa chứng từ"
        }
    },

    Icon : {
        FaChevronDown : "<i class='fas fa-chevron-down'></i>",
        FaCheck : "<i class='fas fa-check'></i>"
    }
}