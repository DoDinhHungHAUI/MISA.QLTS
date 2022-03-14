
// import {Resource} from '../common/resource'
// import {Common} from '../common/common'

import {Resource} from './resource'
import { Common } from './common'

export const CommonFunction = {

    /**
     * Hàm convert định dạng tiền thành số ( VD : 1.000.000 -> 1000000)
     * CreatedBy : DDHung (26/10/2021)
    */
    convertMoneyToNumber : function(money){
        if(money)
        {
            return money.replaceAll('.' , '').trim();
        }
        return "";
    },

    /**
     * Hàm định dạng hiển thị tiền tệ
     * @param {any} money :  bất cữ kiểu giữ liệu gì
     * CreatedBy : DDHUNG (26/10/2021)
    */
    formatMoney : function(money){
        if (money) {
            return Number(money).toFixed(0).replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1.");
        }
        return "";
    },

    /**
     * Hàm trả về mã số (VD : Mã số của mã nhân viên)
     * @param {*} employeeCode : Truyền vào mã 
     * @returns : trả về mã số (loại bỏ chữ số)
     * CreatedBy : DDHung (26/10/2021)
    */

    formatFieldCode : function(fieldCode){
        for(let i = fieldCode.length - 1 ; i >= 0 ; i--)
        {
            if(/[0-9]/.test(fieldCode[i]) == false)
            {
                return Number(fieldCode.slice(i + 1));
            }
        }
        return -1;
    },

    /**
     * Hàm tìm trường có mã số lớn nhất
     * @param {any} res :  danh sách dữ liệu
     * CreatedBy : DDHung (26/10/2021)
    */

    findMaxFieldCode : function(res , nameFieldCode)
    {
        return Math.max.apply(Math, res.map(function(o) { return CommonFunction.formatFieldCode(o[nameFieldCode])}))
    },

    /**
     * Format dữ liệu ngày tháng sang tháng/ngày/năm  -> Để lưu xuống Database
     * @param {*} date : bất cữ kiểu giữ liệu gì
     * @returns trả về dữ liệu ngày tháng có dạng tháng/ngày/năm
     * CreatedBy : DDHung (26/10/2021)
    */
    formatDateToSave : function(value){
        // if(value)
        // {
        //     let day = new Date(value).getDate();
        //     let month = new Date(value).getMonth() + 1;
        //     let year = new Date(value).getFullYear();
        //     day = day < 10 ? '0' + day : day;
        //     month = month < 10 ? '0' + month : month;
        //     return year + '-' + month + '-' + day;
        // }
        // else{
        //     return "";
        // }
        if(value)
        {
            var arrValue = value.split('/');
            return arrValue[1] + '/' + arrValue[0] + '/' + arrValue[2]
        }
        else{
            return "";
        }
    },

    /**
     * Hàm thực hiện format date sang dạng (ngày/tháng/năm)
     * CreatedBy : DDHung (25/10/2021)
    */
    formatDate : function(value){
        if(value)
        {
            var date = new Date(value);
            let day = date.getDate();
            let month = date.getMonth() + 1;
            let year = date.getFullYear();
            day = day < 10 ? '0' + day : day;
            month = month < 10 ? '0' + month : month;
            return day + '/' + month + '/' + year;
        }else{
            return "";
        }
    },

    /**
     * Lấy ra năm của date lấy từ ô input
     * CrearedBy : DDHung (18/11/2021)
     */
    getYearFromDate : function(value){
        if(value)
        {
            var date = new Date(value);
            let year = date.getFullYear();
            return year;
        }else{
            return "";
        }
    },

    /**
     * Hàm thực hiện tính độ dài (width) của đoạn text(str)
     * @param {String} str : Text đầu vào
     * @param {Number} fontSize : fontSize đoạn text
     * @returns : độ dài pixel của đoạn text (str)
     * CreatedBy : DDHung (27/10/2021)
     */
    measureText : function(str, fontSize = 13) {
        if(str){
            const widths = [0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0.2796875,0.2765625,0.3546875,0.5546875,0.5546875,0.8890625,0.665625,0.190625,0.3328125,0.3328125,0.3890625,0.5828125,0.2765625,0.3328125,0.2765625,0.3015625,0.5546875,0.5546875,0.5546875,0.5546875,0.5546875,0.5546875,0.5546875,0.5546875,0.5546875,0.5546875,0.2765625,0.2765625,0.584375,0.5828125,0.584375,0.5546875,1.0140625,0.665625,0.665625,0.721875,0.721875,0.665625,0.609375,0.7765625,0.721875,0.2765625,0.5,0.665625,0.5546875,0.8328125,0.721875,0.7765625,0.665625,0.7765625,0.721875,0.665625,0.609375,0.721875,0.665625,0.94375,0.665625,0.665625,0.609375,0.2765625,0.3546875,0.2765625,0.4765625,0.5546875,0.3328125,0.5546875,0.5546875,0.5,0.5546875,0.5546875,0.2765625,0.5546875,0.5546875,0.221875,0.240625,0.5,0.221875,0.8328125,0.5546875,0.5546875,0.5546875,0.5546875,0.3328125,0.5,0.2765625,0.5546875,0.5,0.721875,0.5,0.5,0.5,0.3546875,0.259375,0.353125,0.5890625]
            const avg = 0.5279276315789471
            return str
            .split('')
            .map(c => c.charCodeAt(0) < widths.length ? widths[c.charCodeAt(0)] : avg)
            .reduce((cur, acc) => acc + cur) * fontSize
        }else{
            return 0
        }
    },

    /**
     * Hàm thực hiện selected vào item của combobox
     * CreatedBy : DDHung (28/10/2021)
    */
    selectedItemCombobox : function(data){
        
        var itemCombobox;
        if(data.event.target)
        {
            itemCombobox = data.event.target;
        }
        else{
            itemCombobox = data.event;
        }
        itemCombobox.parentElement.previousSibling.previousElementSibling.value = data.name;
    
        var cbItems =  itemCombobox.parentElement.children;
        for(const cbitem of cbItems)
        {
            cbitem.classList.remove('m-item-selected');
            var iconRemove = cbitem.children;
            if(iconRemove[0])
            {
                cbitem.removeChild(iconRemove[0]);
            }
        }
        itemCombobox.classList.add("m-item-selected")
        itemCombobox.innerHTML = "<i class='fas fa-check'></i>" + data.name.toString();
        itemCombobox.parentElement.classList.toggle("m-hidden-dropdown")
        itemCombobox.parentElement.previousSibling.innerHTML = "<i class='fas fa-chevron-down'></i>";
    },

    /**
     * Method dùng để tạo một object mới dùng để thêm mới hoặc sửa vào database
     * CreatedBy : DDHung (17/12/2021)
    */
    createObjectVoucher : function(voucher)
    {
        var newObject = {
            VouchersCode : voucher.voucherCode,
            VouchersDate : CommonFunction.formatDateToSave(voucher.voucherDate),
            UsedFixedAssetDate : CommonFunction.formatDateToSave(voucher.usedFixedAssetDate),
            Description : voucher.description,
        }
        return newObject;
    },

    /**
     * Method dùng để tạo một object mới dùng để thêm hoặc sửa vào database
     * CreatedBy : DDHung (17/11/2021)
    */
     createObjectToAddOrEdit : function(fixedAssetEdited)
     {
        var  ProductionDate = CommonFunction.formatDateToSave(fixedAssetEdited.ProductionDate);
        var newObject = {
            FixedAssetCode :fixedAssetEdited.FixedAssetCode,
            FixedAssetName :fixedAssetEdited.FixedAssetName,
            DepartmentAssetId :fixedAssetEdited.DepartmentAssetId,
            DepartmentAssetCode :fixedAssetEdited.DepartmentAssetCode,
            DepartmentAssetName :fixedAssetEdited.DepartmentAssetName,
            FixedAssetCategoryId :fixedAssetEdited.FixedAssetCategoryId, //CommonFunction.formatDateToSave(fixedAsset.FixedAssetCategoryId),
            FixedAssetCategoryCode :fixedAssetEdited.FixedAssetCategoryCode,
            FixedAssetCategoryName :fixedAssetEdited.FixedAssetCategoryName,
            Quantity :fixedAssetEdited.Quantity,//Number
            Cost : CommonFunction.convertMoneyToNumber(fixedAssetEdited.Cost),
            DepreciationRate :CommonFunction.ConvertDepreciationRateToNumber(fixedAssetEdited.DepreciationRate),
            PurchaseDate : CommonFunction.formatDateToSave(fixedAssetEdited.PurchaseDate),
            // PurchaseDate : fixedAssetEdited.PurchaseDate,
            TrackedYear :fixedAssetEdited.TrackedYear,
            LifeTime :fixedAssetEdited.LifeTime,
            ProductionYear : CommonFunction.getYearFromDate(ProductionDate)
            // ProductionYear : CommonFunction.getYearFromDate(fixedAssetEdited.ProductionDate)
        }
        return newObject;
    },

    /**
     * Method dùng xóa một phần tử trong mảng
     * CreatedBy : DDHung (17/11/2021)
    */
    arrayRemove : function(arr, value) { 
        return arr.filter(function(ele){ 
            return ele != value; 
        });
    },

    /**
     * Thực hiện việc Autocomplete khi focus vào combobox
     * @param {*} cbSelected 
     * @param {*} buttonCombobox 
    */
    onFocusToggleItemCombobox : function(cbSelected , buttonCombobox) {
        if(cbSelected.classList.contains("m-hidden-dropdown"))
        {
            cbSelected.classList.remove("m-hidden-dropdown");
            if(buttonCombobox.children[0].classList.contains("fa-chevron-up")){
                buttonCombobox.innerHTML = "<i class='fas fa-chevron-down'></i>";
            }else{
                buttonCombobox.innerHTML = "<i class='fas fa-chevron-up'></i>";
            }
        }
    },

    /**
     * Ẩn đi việc chọn item của combobox khi blur khỏi combobox
     * @param {*} cbSelected 
     * @param {*} buttonCombobox 
    */
    onHideItemCombobox : function(cbSelected , buttonCombobox) {
        if(cbSelected.classList.contains("m-hidden-dropdown") == false)
        {
            cbSelected.classList.add("m-hidden-dropdown");
            if(buttonCombobox.children[0].classList.contains("fa-chevron-up")){
                buttonCombobox.innerHTML = "<i class='fas fa-chevron-down'></i>";
            }else{
                buttonCombobox.innerHTML = "<i class='fas fa-chevron-up'></i>";
            }
        }
    },

    /**
     * Convert giá trị ô tỷ lệ hao mòn thành number (0,76 -> 0.76)
     * @param {*} value 
     */
    ConvertDepreciationRateToNumber : function(value) {
        if(value)
        {
            return value.toString().replace(',', ".");
        }
        return "";
    },

    /**
     * Convert giá trị ô tỷ lệ hao mòn  (0.76 -> 0,76) để load lên form
     * @param {*} value 
     */
    ConvertDepreciationRateToLoad : function(value) {
        if(value)
        {
            return value.toString().replace('.', ",");
        }
        return "";
    },
    
    /**
     * Hàm xử lý khi click vào button của combobox
     * @param {*} event 
     */
    btnButtonCombobox : function(event) {
        var cbSelected = event.target;
        if(event.target.classList.contains('m-combobox-button'))
        {
            cbSelected =  event.target;
        }else{
            cbSelected =  event.target.parentElement;
        }
        cbSelected.nextSibling.classList.toggle("m-hidden-dropdown");
        if(cbSelected.children[0].classList.contains("fa-chevron-up")){
            cbSelected.innerHTML = "<i class='fas fa-chevron-down'></i>";
        }else{
            cbSelected.innerHTML = "<i class='fas fa-chevron-up'></i>";
        }
    },

    /**
     * Hàm xử lý khi click chuột phải vô 1 row trên Gridview
     * CreatedBy : DDHung (2/12/2021)
    */
    rightClickHandler : function(event , id , trTables , self) {
        event.preventDefault();
        for(let trTable of trTables)
        {
            if(trTable.classList.contains('selected-record'))
            {
                trTable.classList.remove('selected-record')
                let checkboxSelected = trTable.querySelector(".grid-checkbox");
                checkboxSelected.checked = false;
            }
        }

        // var length = trTable.length;
        // for(let i = 0 ; i < length ; i++)
        // {
        //     if(trTable[i].classList.contains('selected-record'))
        //     {
        //         trTable[i].classList.remove('selected-record')
        //         break;
        //     }
        // }
        var trSelected = self.$refs[id];
        trSelected[0].classList.add("selected-record");
        var checkboxSelected = trSelected[0].querySelector(".grid-checkbox");
        checkboxSelected.checked = true;

        var contextElement = document.getElementById("context-menu");
        contextElement.style.top = event.clientY + "px";
        contextElement.style.left = event.clientX + "px";
        contextElement.classList.add("active");
    },

    /**
     * Method xử lý việc format value của date mỗi khi người dùng nhập tay vào ô input date
     * @param {*} str 
     * @param {*} max 
     * @returns 
     * CreatedBy : 07/12/2021
     */

    formatValueDatePicker(str, max) {
        if (str.charAt(0) !== '0' || str == '00') {
            var num = parseInt(str);
            if (isNaN(num) || num <= 0 || num > max) num = 1;
            str = num > parseInt(max.toString().charAt(0)) && num.toString().length == 1 ? '0' + num : num.toString();
        }
        return str;
    },

    /**
     * Xử lý tự động sinh dấu "/" mỗi khi nhập vào ô ngày tháng
     * @param {*} self 
     * CreatedBy : 07/12/2021
     */
    handleInputValueDate(self , e)
    {
        // self.type = 'text';
        // var input = self.value;
        // if (/\D\/$/.test(input)) input = input.substr(0, input.length - 3);
        // var values = input.split('/').map(function(v) {
        //     return v.replace(/\D/g, '')
        // });
        // if (values[0]) values[0] = CommonFunction.formatValueDatePicker(values[0], 31);
        // if (values[1]) values[1] = CommonFunction.formatValueDatePicker(values[1], 12);
        // var output = values.map(function(v, i) {
        //     return v.length == 2 && i < 2 ? v + ' / ' : v;
        // });
        // self.value = output.join('').substr(0, 14);

        self.type = 'text';
        var input = self.value;
        var key = e.keyCode || e.charCode;

        if (key == 8 || key == 46)    // here's where it checks if backspace or delete is being pressed
            return false;
            
        if (/\D\/$/.test(input)) input = input.substr(0, input.length - 1);
        var values = input.split('/').map(function(v) {
            return v.replace(/\D/g, '')
        });
        if (values[0]) values[0] = CommonFunction.formatValueDatePicker(values[0], 31);
        if (values[1]) values[1] = CommonFunction.formatValueDatePicker(values[1], 12);
        var output = values.map(function(v, i) {
            return v.length == 2 && i < 2 ? v + '/' : v;
        });
        self.value = output.join('').substr(0, 10);
    },

    /**
     * Method xử lý việc chọn (checked) ô checkbox trên header của grid mỗi khi tất cả các dòng trên grid được chọn
     * @param {*} tbody : tbody trên grid
     * CreatedBy : DDHung (10/12/2021)
    */
    handleCheckboxHeader : function(tbody){
        var lstTrInGrid = tbody.querySelectorAll('tr');
        var lstTrSelectedInGrid = tbody.querySelectorAll('tr.selected-record');
        var checkboxHeader = tbody.parentElement.querySelector(".checkbox-header-grid");
        if(parseInt(lstTrInGrid.length) == parseInt(lstTrSelectedInGrid.length))
        {
            //chọn ô checkbox trên header
            checkboxHeader.checked = true;
        }else{
            checkboxHeader.checked = false;
        }
    },


    /**
     * Method xử lý việc chọn dữ liệu đã được chọn trước đó trên combobox  tại dialog nguồn kinh phí
     * CreatedBy : DDHung (16/12/2021)
    */
    validateDuplicateCbox : function()
    {
        var isCheckDupliacteOrgin = true;
        var wrapOrginFunding = document.querySelector('.wrap-form-orgin-funding')  
        var listCombobox = wrapOrginFunding.querySelectorAll('.m-custom-combobox')
        //Validate combobox nguồn kinh phí 
        for (const cbox of listCombobox) {
            if(cbox.classList.contains('border-red'))
            {
                isCheckDupliacteOrgin = false
                break;
            }
        }
        return isCheckDupliacteOrgin
    },

    /**
     * Method xử lý việc validate khi chưa chọn dữ liệu tại combobox trên dialog nguồn kinh phí
     * CreatedBy : DDHung (26/12/2021) 
    */
    validateRequiredCbox : function()
    {
        var noContentValueOrigin = true
        var wrapOrginFunding = document.querySelector('.wrap-form-orgin-funding')   
        var listCombobox = wrapOrginFunding.querySelectorAll('.m-custom-combobox')
        //Validate combobox nguồn kinh phí khi chưa chọn dữ liệu
        for (const cbox of listCombobox) {
            var input = cbox.querySelector('.m-combobox-input')
            if(!input.value)
            {
                cbox.classList.add('border-red')
                let errorMessage =  cbox.parentElement.querySelector(".error-duplicate-origin")
                errorMessage.innerText =  Resource[Common.LanguageCode].ValidateDialogOrigin.RequiredCombobox
                noContentValueOrigin = false
            }
        }
        return noContentValueOrigin;
    },

    /**
     * Validate ô nhập giá trị nguồn kinh phí 
     * CreatedBy : DDHung (26/12/2021)
    */
    validateOriginFunding : function()
    {
        var requiredOrigin = true;
        var wrapOrginFunding = document.querySelector('.wrap-form-orgin-funding')       
        var listOriginCost = wrapOrginFunding.querySelectorAll(".input-origin-cost-value")
        for(const inputOrigin of listOriginCost)
        {
            if(!inputOrigin.value)
            {
                var errorMessage = inputOrigin.parentElement.querySelector('.error-required-origin')
                errorMessage.innerText = Resource[Common.LanguageCode].ValidateDialogOrigin.Required
                inputOrigin.classList.add('border-red')
                requiredOrigin = false
            }
        }
        return requiredOrigin
    },


    /**
     * Method xử lý khi bấm ctrl + click vào một row trên Grid
     * @param {*} self 
     * @param {*} fixedAssetId 
     */
    handleCtrlClickRowGrid : function(self , fixedAssetId)
    {
        var trSelected = self.$refs[fixedAssetId];
        trSelected[0].classList.add("selected-record");
        var checkboxSelected = trSelected[0].querySelector(".grid-checkbox");
        checkboxSelected.checked = true;
        
        document.getElementById("context-menu").classList.remove("active");
    },

    /**
     * Hàm xử lý việc chọn 1 row của grid
     * @param {*} self 
     * @param {*} fixedAssetId : Id của record được chọn trên grid
     * CreatedBy : DDHung (2/12/2021)
     */
    handleClickRowGrid : function(self , id , trTables)
    {
        for(let trTable of trTables)
        {
            if(trTable.classList.contains('selected-record'))
            {
                trTable.classList.remove('selected-record')
                var checkbox = trTable.querySelector(".grid-checkbox");
                checkbox.checked = false;
            }
        }
        var trSelected = self.$refs[id];
        trSelected[0].classList.add("selected-record");
        var checkboxSelected = trSelected[0].querySelector(".grid-checkbox");
        checkboxSelected.checked = true;
        document.getElementById("context-menu").classList.remove("active");
    },

    handleClickRow : function(self) {
        var trTables = self.$refs.tbodyFixedAsset.children;

        for(let trTable of trTables)
        {
            if(trTable.classList.contains('selected-record'))
            {
                trTable.classList.remove('selected-record')
                var checkbox = trTable.querySelector(".grid-checkbox");
                checkbox.checked = false;
                break;
            }
        }

        // var length = trTable.length;
        // for(let i = 0 ; i < length ; i++)
        // {
        //     if(trTable[i].classList.contains('selected-record'))
        //     {
        //         trTable[i].classList.remove('selected-record')
        //         break;
        //     }
        // }
        document.getElementById("context-menu").classList.remove("active");
    },

    /**
     * Method thực hiện validate các trường bắt buộc nhập
     * CreatedBy : DDHung (16/11/2021)
    */

    // validateFieldRequired : function(self , event)
    // {
    //     try {
            
    //         var input = event.target;
    //         var fieldName = input.getAttribute('fieldname');
    //         var nameToValidate = input.getAttribute('nameToValidate');
    //         var value = this.fixedAsset[fieldName];
    //         var tootlTip = input.parentElement.querySelector('.tooltiptext');
    //         if(input.classList.contains("input-typenumber-popup"))
    //         {
    //             tootlTip = input.parentElement.parentElement.querySelector('.tooltiptext');
    //         }
    //         if(!value){
    //             self.ListFieldNotValidRequired.push(nameToValidate);
    //             input.classList.add('border-red');
    //             //gán dữ liệu (text) cho tooltip
    //             tootlTip.classList.add("showTooltip")
    //             tootlTip.innerText = Resource[Common.LanguageCode].Validate.Required(nameToValidate);
    //             // input.setAttribute('title' , Resource[Common.LanguageCode].Validate.Required(nameToValidate));
    //             input.setAttribute('validate' , false)
    //             this.checkValidate = false;
    //         }
    //         else{
    //             input.classList.remove('border-red');
    //             // input.removeAttribute('title')
    //             tootlTip.classList.remove("showTooltip")
    //             input.removeAttribute('validate')
    //         }
            
    //     } catch (error) {
    //         console.log(error)
    //     }
    // },

    /**
     * Hàm tính tổng số lượng page của grid
     * @param {*} m_TotalRecord  : Tổng số bản ghi
     * @param {*} m_PageSize 
     * @returns 
     */
    calcTotalPage : function(m_TotalRecord , m_PageSize)
    {
        var m_Remainder = m_TotalRecord % m_PageSize;
        if(m_Remainder > 0){
            return Math.floor(m_TotalRecord / m_PageSize) + 1;
        }
        return m_TotalRecord / m_PageSize;
    },

    /**
     * Method tính tổng nguyên giá từ danh sách nguồn kinh phí ứng với tài sản
     * CreatedBy : DDHung (22/12/2021) 
    */
    calTotalCost : function(listOriginFundingToSave)
    {
        var totalCost = 0;
        listOriginFundingToSave.forEach(item => {
            totalCost += parseFloat(CommonFunction.convertMoneyToNumber(item.ValueOriginFunding))
        });
        return totalCost;
    },

    /**
     * Hàm dùng để check xem phần tử đó có nằm trong trong array hay ko 
     * @param {*} item : Phần từ muốn check
     * @param {*} array : Array để check
     * @returns :(true - không nằm trong array , false - nằm trong array)
    */
    checkNotContaintItemInArray : function(item , array)
    {
        var check = array.includes(item);
        return !check
    },

    /**
     * Hàm kiểm tra xem có phải object hay không
     * @param {*} object 
     * @returns : true - là object , false - không phải là object
     * CreatedBy : DDHung (19/12/2021)
     */
    isObject : function(object) {
        return object != null && typeof object === 'object';
    },

    /**
     * hàm dùng để so sánh 2 object
     * @param {*} object1 
     * @param {*} object2 
     * @returns true - 2 object đó bằng nhau , false - 2 object đó khác nhau
     * CreatedBy : DDHung (19/12/2021)
     */
    compareObject : function(object1, object2) {
        const keys1 = Object.keys(object1)
        //const keys2 = Object.keys(object2)
        // if (keys1.length !== keys2.length) {
        //     return false;
        // }
        for (const key of keys1) {
            const val1 = object1[key];
            const val2 = object2[key];
            const areObjects = CommonFunction.isObject(val1) && CommonFunction.isObject(val2);
            if (areObjects && !CommonFunction.compareObject(val1, val2) || !areObjects && val1 !== val2 ) {
                return false;
            }
        }
        return true;
    },

    /**
     * hàm dùng để so sánh 2 array
     * @param {*} array1 
     * @param {*} array2 
     * @returns true - nếu 2 array bằng nhau , false - 2 array khác nhau
     */
    compareObjectArray : function(array1 , array2)
    {
        if (array1.length !== array2.length) {
            return false
        } else {
            for (let i = 0; i < array1.length; i++) {
                if (!CommonFunction.compareObject(array1[i] , array2[i])) {
                    return false
                }
            }
            console.log("oke")
            return true;
        }
    },

    /**
     * Hàm chuyển ký tự có dấu thành ký tự không dấu phục vụ cho việc tìm kiếm
     * @param str : chuỗi đầu vào muốn chuyển sang ký tự không dấu
     * CreatedBy : (28/10/2021)
    */
    removeVietnameseTones :function(str){
        str = str.replace(/à|á|ạ|ả|ã|â|ầ|ấ|ậ|ẩ|ẫ|ă|ằ|ắ|ặ|ẳ|ẵ/g,"a");
        str = str.replace(/è|é|ẹ|ẻ|ẽ|ê|ề|ế|ệ|ể|ễ/g,"e");
        str = str.replace(/ì|í|ị|ỉ|ĩ/g,"i");
        str = str.replace(/ò|ó|ọ|ỏ|õ|ô|ồ|ố|ộ|ổ|ỗ|ơ|ờ|ớ|ợ|ở|ỡ/g,"o");
        str = str.replace(/ù|ú|ụ|ủ|ũ|ư|ừ|ứ|ự|ử|ữ/g,"u");
        str = str.replace(/ỳ|ý|ỵ|ỷ|ỹ/g,"y");
        str = str.replace(/đ/g,"d");
        str = str.replace(/À|Á|Ạ|Ả|Ã|Â|Ầ|Ấ|Ậ|Ẩ|Ẫ|Ă|Ằ|Ắ|Ặ|Ẳ|Ẵ/g, "A");
        str = str.replace(/È|É|Ẹ|Ẻ|Ẽ|Ê|Ề|Ế|Ệ|Ể|Ễ/g, "E");
        str = str.replace(/Ì|Í|Ị|Ỉ|Ĩ/g, "I");
        str = str.replace(/Ò|Ó|Ọ|Ỏ|Õ|Ô|Ồ|Ố|Ộ|Ổ|Ỗ|Ơ|Ờ|Ớ|Ợ|Ở|Ỡ/g, "O");
        str = str.replace(/Ù|Ú|Ụ|Ủ|Ũ|Ư|Ừ|Ứ|Ự|Ử|Ữ/g, "U");
        str = str.replace(/Ỳ|Ý|Ỵ|Ỷ|Ỹ/g, "Y");
        str = str.replace(/Đ/g, "D");
        // Some system encode vietnamese combining accent as individual utf-8 characters
        // Một vài bộ encode coi các dấu mũ, dấu chữ như một kí tự riêng biệt nên thêm hai dòng này
        str = str.replace(/\u0300|\u0301|\u0303|\u0309|\u0323/g, ""); // ̀ ́ ̃ ̉ ̣  huyền, sắc, ngã, hỏi, nặng
        str = str.replace(/\u02C6|\u0306|\u031B/g, ""); // ˆ ̆ ̛  Â, Ê, Ă, Ơ, Ư
        // Remove extra spaces
        // Bỏ các khoảng trắng liền nhau
        str = str.replace(/ + /g," ");
        str = str.trim();
        // Remove punctuations
        // Bỏ dấu câu, kí tự đặc biệt
        str = str.replace(/!|@|%|\^|\*|\(|\)|\+|\\=|\\<|\\>|\?|\/|,|\.|\\:|\\;|\\'|\\"|\\&|\\#|\[|\]|~|\$|_|`|-|{|}|\||\\/g," ");
        return str;
    },

}