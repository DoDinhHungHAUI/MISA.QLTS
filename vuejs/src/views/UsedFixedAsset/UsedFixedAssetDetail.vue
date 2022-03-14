<template lang="">
    <div class="m-popup toggle-popup">
        <div class="wrap-popup"></div>
        <div class="popup-info-voucher">
            <div class="header-popup header-popup-voucher m-wrap-tooltip">
                <span>Sửa chứng từ ghi tăng</span>
                <button class="close-popup" id="close-popup" v-on:click = "hidedDialogVoucher" tabindex = "5" @focus = "onFocusButton($event)" @blur = "onBlurClose($event)"></button>
                <span class="m-tooltiptext m-tooltip-close">Đóng</span>
            </div>
            <div class = "wrap-form-voucher">
                <div class="content-popup content-popup-voucher" ref="contentPoupupVoucher">
                    <div class = "title-info-voucher">
                        <span>Thông tin chứng từ</span>
                    </div>
                    <div class = "form-Voucher">
                        <div class = "row-form">
                            <div class="input-form">
                                <label>Mã chứng từ <span style="color: red;">*</span></label><br />
                                <input type="text" class="input-popup first-field-popup input-text" placeholder = "Nhập mã chứng từ" title = ""
                                    @blur = "validateFieldRequired($event)" fieldName = "voucherCode" required maxlength = "150"
                                    v-model = "voucher.voucherCode" nameToValidate = "Mã chứng từ" ref="voucherCode" tabindex = "1"
                                />
                                <span class="tooltiptext"></span>
                            </div>
                            <div class="input-form">
                                <label>Ngày chứng từ <span style="color: red;">*</span></label><br />
                                <!-- <input type="text" class="input-popup input-text" placeholder = "Nhập ngày chứng từ" required /> -->
                                <date-pick
                                    v-bind:months = "months" v-bind:weekdays= "weekdays" :format="'DD/MM/YYYY'" value-format="MM/DD/YYYY"
                                    fieldName = "voucherDate" @blur = "validateFieldRequired($event)" class="datepickerRequired"
                                    nameToValidate = "Ngày chứng từ" v-model = "voucher.voucherDate"
                                >
                                </date-pick>
                                <span class="tooltiptext"></span>
                                <!-- <date-pick
                                    fieldName = "PurchaseDate" v-model = "fixedAsset.PurchaseDate"
                                    v-bind:months = "months" v-bind:weekdays= "weekdays"  :format="'DD/MM/YYYY'" value-format="MM/DD/YYYY"
                                    @blur = "validateFieldRequired($event)"  class="datepickerRequired" nameToValidate = "Ngày mua"
                                    ref="PurchaseDate"
                                ></date-pick> -->
                            </div>
                            <div class="input-form">
                                <label>Ngày ghi tăng <span style="color: red;">*</span></label><br />
                                <!-- <input type="text" class="input-popup input-text" placeholder = "Nhập ngày ghi tăng" required /> -->
                                <date-pick
                                    v-bind:months = "months" v-bind:weekdays= "weekdays" :format="'DD/MM/YYYY'" value-format="MM/DD/YYYY"
                                    fieldName = "usedFixedAssetDate" @blur = "validateFieldRequired($event)" class="datepickerRequired"
                                    nameToValidate = "Ngày ghi tăng" v-model = "voucher.usedFixedAssetDate"
                                >
                                </date-pick>
                                <span class="tooltiptext"></span>
                            </div>
                        </div>
                        <div class = "row-form row-form-bottom">
                            <div class="input-form">
                                <label>Ghi chú </label><br />
                                <input type="text" class="input-popup input-text input-row" title = "" placeholder = "Nhập ghi chú"
                                    v-model = "voucher.description" value = "" tabindex = "4"
                                />
                            </div>
                        </div>
                    </div>
                    <div class = "form-fixedasset">
                        <div class="header-popup-fixedasset">
                            <span class = "info-detail">Thông tin chi tiết</span>
                        </div>
                        <div class = "toolbar-popup-fixedasset">
                            <input type="text" class = "icon-search input-search" id = "input-search" placeholder = "Tìm kiếm " style="width : 200px" v-model = "inputSearch" />
                            <div class ="choose-fixedAsset" v-on:click = "btnChooseFixedAsset">
                                <div class = "icon-choose-fixedasset">
                                    <i class="fas fa-plus"></i>
                                </div>
                                Chọn tài sản
                            </div>
                        </div>
                    </div>
                    <div class="grid-detail grid-detail-popup">
                        <div class="grid-detail-fixedasset-popup">
                            <table border="0" width="100%" cellpadding="0">
                                <thead class="thead-table thead-grid-fixedasset-popup">
                                    <tr>
                                        <th class="stt-table align-center">STT</th>
                                        <th>Mã tài sản</th>
                                        <th>Tên tài sản</th>
                                        <th>Bộ phận sử dụng</th>
                                        <th class="align-right column-cost-dialog">Nguyên giá</th>
                                        <th class="align-right column-cost-dialog column-depreciationrate-year-dialog">Hao mòn năm</th>
                                        <th class="align-right column-fixedassetrest-dialog">Giá trị còn lại</th>
                                    </tr>
                                </thead>
                                <tbody v-if="listFixedAssetChoosedLoad.length > 0">
                                    <tr v-for = "(item, index) in listFixedAssetChoosedLoad" :key="item.FixedAssetId" @mouseover = "showFeatureRecord(item.FixedAssetId)"
                                        :ref = "item.FixedAssetId" @mouseleave = "hideFeatureRecord(item.FixedAssetId)"
                                    >
                                        <td class="align-center">{{startSTT + index}}</td>
                                        <td v-bind:title = "[formatText(item.FixedAssetCode) ? item.FixedAssetCode : '']" v-bind:class = "{'format-text' :  formatText(item.FixedAssetCode)}">{{item.FixedAssetCode}}</td>
                                        <td v-bind:title = "[formatText(item.FixedAssetName) ? item.FixedAssetName : '']" v-bind:class = "{'format-text' :  formatText(item.FixedAssetName)}">{{item.FixedAssetName | formatNoContent}}</td>
                                        <td v-bind:title = "[formatText(item.DepartmentAssetName) ? item.DepartmentAssetName : '']" v-bind:class = "{'format-text' :  formatText(item.DepartmentAssetName)}">{{item.DepartmentAssetName | formatNoContent}}</td>
                                        <td class="align-right">{{item.Cost | formatMoney | formatNoContent}}</td>
                                        <td class="align-right">{{item.FixedAssetCummulative | formatMoney | formatNoContent}}</td>
                                        <td class="align-right td-feature-grid">
                                            {{item.FixedAssetRest | formatMoney |formatNoContent}}
                                            <div class = "feature hide-feature"><!-- hide-feature -->
                                                <div class = "icon-edit-record wrap-icongrid-tooltip" v-on:click = "editRecordGridPopup(item.FixedAssetId , item.Cost)">
                                                    <span class="grid-tooltiptext showTooltip-filterbar">Sửa</span>
                                                </div>
                                                <div class = "icon-delete-record wrap-icongrid-tooltip" v-on:click = "deleteRecordGridPopup(item.FixedAssetId)">
                                                    <span class="grid-tooltiptext showTooltip-filterbar">Xóa</span>
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                </tbody>
                                <tbody v-else>
                                    <div class = "no-content-grid no-content-grid-poup">
                                        Không có dữ liệu
                                    </div>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
            <div class = "wrap-summary-paging">
                <div class="grid-summary-popup">
                    <div class = "total-cost-dialog">{{totalCost | formatMoney}}</div>
                    <div class = "total-depreciationrate-year-dialog">{{totalFixedAssetCummulative | formatMoney}}</div>
                    <div class = "total-fixedassetrest-dialog">{{totalFixedAssetRest | formatMoney}}</div>
                </div>

                <!-- <FixedAssetChoosedPaging
                    v-bind:totalRecord = "totalRecord"
                    v-bind:totalPage = "totalPage"
                    v-on:btnPagingFixedAssetChoosed = "btnPagingFixedAssetChoosed"
                /> -->

                <MPaging
                    v-bind:totalRecord = "totalRecord"
                    v-bind:totalPage = "totalPage"
                    v-on:btnPaging = "btnPaging"
                    v-bind:customClass = "'paging-usedfixedasset'"
                />

            </div>

            <div class = "wrap-footer-popup">
                <div class="footer-popup">
                    <div class="wrap-button-footer">
                        <button type="button" class="m-btn-cancel" id="btn-close-popup" v-on:click = "btnCancelClick">
                            Hủy
                        </button>
                        <button type="button" class="m-btn m-btn-save" id="btnSave" v-on:click = "btnSaveClick">
                            Lưu
                        </button>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>
<script>

import axios from 'axios'

/* File Javascript */
import {CommonFunction} from '../../assets/js/common/commonfunction'
import {Resource} from '../../assets/js/common/resource'
import {Common} from '../../assets/js/common/common'
import {Enum} from '../../assets/js/common/enum'
import {ToastJS} from '../../assets/js/common/messagetoast'
/* import library */
import DatePick from 'vue-date-pick';
import 'vue-date-pick/dist/vueDatePick.css';

// import FixedAssetChoosedPaging from './FixedAssetChoosedPaging.vue'

import MPaging from '../../components/base/MPaging.vue'

export default {
    data() {
        return {
            checkValidate : true,
            weekdays : ['T2' , 'T3' ,'T4' , 'T5' , 'T6' , 'T7', 'CN'],
            months : ['Tháng 1' , 'Tháng 2' , 'Tháng 3' , 'Tháng 4' ,'Tháng 5' , 'Tháng 6' , 'Tháng 7' , 'Tháng 8' , 'Tháng 9' , 'Tháng 10' , 'Tháng 11' , 'Tháng 12'],
            voucher : {
                voucherId :"",
                voucherCode : "",
                voucherDate : CommonFunction.formatDate(Date.now()),
                usedFixedAssetDate : CommonFunction.formatDate(Date.now()),
                description : ""
            },
            totalRecord : 0,
            totalPage : 1,
            listFixedAssetChoosedLoad : [],
            currentPage : 1,
            startSTT : 1,
            timeout: null,
            txtsearch : '',
            listFixedAssetForSearch : [],
            totalCost : null,
            totalFixedAssetCummulative : null,
            totalFixedAssetRest : null,
            checkVoucherChange : {
                voucherId :"",
                voucherCode : "",
                voucherDate : CommonFunction.formatDate(Date.now()),
                usedFixedAssetDate : CommonFunction.formatDate(Date.now()),
                description : ""
            },
            checkListFixedAssetChoosed : [],
            //Biến dùng cho việc tính toán width của các cột summary tương ứng các cột trên grid (tính toán 1 lần duy nhất cho việc render lần đầu tiên)
            isCalculateWidthSummary: true,
            // voucherDTO : {
            //     voucherViewModel : null,
            //     fixedAssetViewModels : []
            // }
        }
    },
    props : {
        isAddOrEdit : {
            typeof : Number
        },
        listFixedAssetChoosed : {
            typeof : Array
        },
        voucherEdit : {
            typeof : Object
        },
        voucherDTO : {
            typeof:Object
        }
    },
    components : {
        // FixedAssetChoosedPaging,
        DatePick,
        MPaging
    },

    // mounted() {

    //     // Căn chỉnh kích thước cho cột summary của grid trên popup
    //     var gridDetailPopup = document.querySelector('.grid-detail-popup')
    //     var gridSummary = document.querySelector('.grid-summary-popup')

    //     var costDialog = gridDetailPopup.querySelector('.column-cost-dialog')
    //     var depreciationrateYearDialog = gridDetailPopup.querySelector('.column-depreciationrate-year-dialog')
    //     var fixedassetrestDialog = gridDetailPopup.querySelector('.column-fixedassetrest-dialog')

    //     var totalCost = gridSummary.querySelector('.total-cost-dialog')
    //     var totalDepreciationrateYear = gridSummary.querySelector('.total-depreciationrate-year-dialog')
    //     var totalFixedassetrest = gridSummary.querySelector('.total-fixedassetrest-dialog')
    //     console.log(totalFixedassetrest)
    //     totalCost.style.width = costDialog.clientWidth + "px"
    //     totalDepreciationrateYear.style.width = depreciationrateYearDialog.clientWidth + "px"
    //     totalFixedassetrest.style.width = fixedassetrestDialog.clientWidth + "px"
    // },

    mounted() {

        //Gán sự kiện onblur cho hai ô input ngày chứng từ và ngày ghi tăng
        var divVoucherDate = this.$refs.contentPoupupVoucher.querySelectorAll('div[fieldname="voucherDate"]')
        var inputUsedFixedAssetDate = divVoucherDate[0].getElementsByTagName('input')[0];
        var divFixedAssetDate = this.$refs.contentPoupupVoucher.querySelectorAll('div[fieldname="usedFixedAssetDate"]')
        var inputusedFixedAssetDate= divFixedAssetDate[0].getElementsByTagName('input')[0];
        inputUsedFixedAssetDate.setAttribute("tabindex" , "2");
        inputusedFixedAssetDate.setAttribute("tabindex", "3");

        //Xử lý việc tự động sinh dấu "/" khi nhập dữ liệu là ngày tháng
        inputUsedFixedAssetDate.addEventListener('keyup', function(e) {
            var self = this;
            if(!e.target.value)
            {
                e.target.classList.add('border-red')
                var tooltip = e.target.parentElement.parentElement.querySelector('.tooltiptext')
                tooltip.classList.add('showTooltip')
                var nameToValidate = e.target.parentElement.getAttribute('nametovalidate')
                tooltip.innerText = Resource[Common.LanguageCode].Validate.Required(nameToValidate)
            }

            CommonFunction.handleInputValueDate(self , e);
        });

        inputusedFixedAssetDate.addEventListener('keyup' , function(e){
            var self = this;
            CommonFunction.handleInputValueDate(self , e);
        })

        //Focus vào input Mã chứng từ
        this.$refs.voucherCode.focus();

    },

    updated() {

        //Căn chỉnh kích thước cho cột summary của grid trên popup
        var gridDetailPopup = document.querySelector('.grid-detail-popup')
        var gridSummary = document.querySelector('.grid-summary-popup')

        var costDialog = gridDetailPopup.querySelector('.column-cost-dialog')
        var depreciationrateYearDialog = gridDetailPopup.querySelector('.column-depreciationrate-year-dialog')
        var fixedassetrestDialog = gridDetailPopup.querySelector('.column-fixedassetrest-dialog')

        var totalCost = gridSummary.querySelector('.total-cost-dialog')
        var totalDepreciationrateYear = gridSummary.querySelector('.total-depreciationrate-year-dialog')
        var totalFixedassetrest = gridSummary.querySelector('.total-fixedassetrest-dialog')
        totalCost.style.width = costDialog.clientWidth + "px"
        totalDepreciationrateYear.style.width = depreciationrateYearDialog.clientWidth + "px"
        totalFixedassetrest.style.width = fixedassetrestDialog.clientWidth + "px"
    },

    created() {

        //Danh sách để load lên grid
        this.listFixedAssetChoosedLoad = this.listFixedAssetChoosed.slice(0 , Resource.SearchPaging.PageSizeVoucherDialog - 1);

        if(this.isAddOrEdit == Enum.FormModel.Add)
        {
            axios.get(Resource.API.Host + Resource.API.Vouchers + Resource.API.GetNewCodeVoucher)
            .then(res => {
                this.voucher.voucherCode = res.data
            })
            .catch(error => {
                console.log(error)
            })
        }
        else{
            this.voucher.voucherCode = this.voucherEdit.VouchersCode
            this.voucher.voucherDate = CommonFunction.formatDate(this.voucherEdit.VouchersDate)
            this.voucher.usedFixedAssetDate = CommonFunction.formatDate(this.voucherEdit.UsedFixedAssetDate)
            this.voucher.description = this.voucherEdit.Description

            this.checkVoucherChange = Object.assign({} , this.voucher);

            // this.checkCompareObject = this.voucherEdit
            //Lấy danh sách tài sản có trong chứng từ đó
            axios.get(Resource.API.Host + Resource.API.FixedAssets + Resource.API.getFixedAssetByVoucherId(this.voucherEdit.VoucherId))
            .then(res => {
                this.$emit("fixedAssetChoosedClickEdit" ,res.data)
                // this.checkListFixedAssetChoosed = Object.assign([] , res.data)
            })
            .catch(error => {
                console.log(error)
            })

            //Sử dụng để check sự thay đổi của nguồn kinh phí
            axios.get(Resource.API.Host + Resource.API.FixedAssets + Resource.API.getFixedAssetByVoucherId(this.voucherEdit.VoucherId))
            .then(res => {
                this.checkListFixedAssetChoosed = res.data
            })
            .catch(error => {
                console.log(error)
            })
        }
    },

    methods: {

        /**
         * Method được gọi khi blur khỏi nút close trên dialog
         * CreatedBy : DDHung (22/11/2021)
         */
        onBlurClose : function(event)
        {
            var inputFirst = this.$refs.contentPoupupVoucher.getElementsByClassName('first-field-popup');
            var button = event.target;
            button.classList.remove("border-tab")
            inputFirst[0].focus();
        },

        /**
         * Method được gọi khi focus vào button trên dialog detail tài sản
         * CreatedBy : DDHung (22/11/2021)
        */
        onFocusButton : function(event)
        {
            var button = event.target;
            button.classList.add("border-tab")
        },


        /**
         *Method thực hiện sửa 1 record trên grid
         CreatedBy : DDHung (18/12/2021)
        */
        editRecordGridPopup : function(fixedAssetId , cost)
        {
            var self = this;
            axios.get(Resource.API.Host + Resource.API.OriginFundingFixedAsset + Resource.API.getOriginFundingByFixedAssetId(fixedAssetId))
            .then(res => {
                console.log(res.data)
                self.$emit("transmitOriginFuns" , res)
                self.$emit("showDialogOriginFunding" ,fixedAssetId ,cost)
            })
            .catch(error => {
                console.log(error)
            })
        },

        /**
         *Method thực hiện xóa 1 record trên grid
         CreatedBy : DDHung (18/12/2021)
        */
        deleteRecordGridPopup : function(fixedAssetId)
        {
           this.$emit("deleteRecordGridPopup" ,fixedAssetId )
        },

        /**
         * Sự kiện khi di chuyển chuột ra khỏi bản ghi (mouseleave) dùng để ẩn nội dung cột chức năng.
         * CreatedBy : DDHung (19/11/2021)
        */
        hideFeatureRecord : function(fixedAssetId)
        {
            try {
                var me = this;
                var trSelected = me.$refs[fixedAssetId];
                var feature = trSelected[0].querySelector('.feature');
                feature.classList.add('hide-feature');
            } catch (error) {
                console.log(error);
            }
        },

        /**
         * Sự kiện khi hover vào một bản ghi trên table dùng để show nội dung cột chức năng
         * CreatedBy : DDHung (19/11/2021)
         */
        showFeatureRecord : function(fixedAssetId)
        {
            try {
                var me = this;
                var trSelected = me.$refs[fixedAssetId];
                var feature = trSelected[0].querySelector('.feature');
                if(feature.classList.contains('hide-feature'))
                {
                    feature.classList.remove('hide-feature');
                }
            } catch (error) {
                console.log(error);
            }
        },

        /**
         * Method thực hiện close Popup form chứng từ
         * CreatedBy : DDHung (16/12/2021)
        */
        hidedDialogVoucher : function(){
            this.$emit("hidedDialogVoucher")
        },

        /**
         * Method thực hiện việc show dialog chọn tài sản để ghi tăng
         * CreatedBy : DDHung (17/11/2021)
        */
        btnChooseFixedAsset : function()
        {
            this.$emit("btnChooseFixedAsset")
        },

        /**
         * Method thực hiện validate các trường bắt buộc nhập
         * CreatedBy : DDHung (16/11/2021)
        */
        validateFieldRequired : function(event){
            try {

                // var self = this;
                var input = event.target;
                var fieldName = input.getAttribute('fieldname');
                var nameToValidate = input.getAttribute('nameToValidate');
                var value = this.voucher[fieldName];
                var tootlTip = input.parentElement.querySelector('.tooltiptext');
                if(input.classList.contains("input-typenumber-popup"))
                {
                    tootlTip = input.parentElement.parentElement.querySelector('.tooltiptext');
                }
                //Bắt buộc nhập
                if(!value){
                    // self.ListFieldNotValidRequired.push(nameToValidate);
                    input.classList.add('border-red');
                    //gán dữ liệu (text) cho tooltip
                    tootlTip.classList.add("showTooltip")
                    tootlTip.innerText = Resource[Common.LanguageCode].Validate.Required(nameToValidate);
                    // input.setAttribute('title' , Resource[Common.LanguageCode].Validate.Required(nameToValidate));
                    input.setAttribute('validate' , false)
                    this.checkValidate = false;
                }
                else{
                    input.classList.remove('border-red');
                    // input.removeAttribute('title')
                    tootlTip.classList.remove("showTooltip")
                    input.removeAttribute('validate')
                }
            } catch (error) {
                console.log(error)
            }
        },

        /**
         * Method thực hiện validate datepicker bắt buộc chọn
         * CreatedBy : DDHung (17/11/2021)
        */
        validateDatePickerRequired(){
            try {
                var dialogContent =  this.$refs.contentPoupupVoucher;
                var datePickers = dialogContent.querySelectorAll('.datepickerRequired');

                for(let datePicker of datePickers)
                {
                    var fieldName = datePicker.getAttribute('fieldname')
                    var nametovalidate = datePicker.getAttribute("nametovalidate")
                    var toolTip = datePicker.parentElement.querySelector(".tooltiptext")
                    if(!this.voucher[fieldName]){
                        // this.ListFieldNotValidRequired.push(nametovalidate)
                        datePicker.firstElementChild.classList.add('border-red')
                        toolTip.classList.add("showTooltip")
                        toolTip.innerText = Resource[Common.LanguageCode].Validate.Required(nametovalidate)
                        // datePicker.firstElementChild.setAttribute('title' , Resource[Common.LanguageCode].Validate.Required(nametovalidate));
                        datePicker.firstElementChild.setAttribute('validate' , false)
                        this.checkValidate = false
                    }
                    else{
                        datePicker.firstElementChild.classList.remove('border-red')
                        // datePicker.firstElementChild.removeAttribute('title')
                        toolTip.classList.remove("showTooltip")
                        datePicker.firstElementChild.removeAttribute('validate')
                    }
                }
            } catch (error) {
                console.log(error);
            }
        },

        /**
         * Method dùng để validate grid tài sản trên dialog chi tiết chứng từ khi không có tài sản nào được chọn
         * CreatedBy : DDHung (22/12/2021)
        */
        validateNoContentFixedAsset : function()
        {
            if(this.listFixedAssetChoosed == 0)
            {
                if(this.checkValidate)
                {
                    //show dialog thông báo
                    this.$emit("showDialogNotValid" , Resource[Common.LanguageCode].Validate.NoContentGridDialog)
                    this.checkValidate = false
                }
            }
        },

        /**
         * Method xử lý sự kiện khí bấm nút hủy
         * CreatedBy : DDHung (27/12/2021) 
        */
        btnCancelClick : function()
        {
            var checkCompareObject = CommonFunction.compareObject(this.voucher , this.checkVoucherChange) && CommonFunction.compareObjectArray(this.checkListFixedAssetChoosed , this.listFixedAssetChoosed)
            if(checkCompareObject)
            {
                this.$emit("hidedDialogVoucher")
            }else{
                var dataToAddOrEditVoucher = CommonFunction.createObjectVoucher(this.voucher);
                //Nếu có thay đổi => show Confirm dialog trước khi sửa
                this.$emit("confirmSaveInfoVoucher" , Resource[Common.LanguageCode].MessageDialogConfirm.ConfirmSaveInfo , true)
                //Gửi dữ liệu thay đổi lên Component UsedFixedAssetPage để khi bấm xác nhận lưu thì sẽ lưu dữ liệu đó xuống database
                this.$emit("transmitVoucher" , dataToAddOrEditVoucher , this.listFixedAssetChoosed)
            }
        },

        /**
         * Method thực hiện việc Thêm/Sửa tài sản
         * CreatedBy : DDHung (16/11/2021)
        */
        btnSaveClick(){
            try {
                var me = this;
                this.checkValidate = true;
                var dialogContent =  this.$refs.contentPoupupVoucher;
                var inputRequires = dialogContent.querySelectorAll('input[required]');
                for(let inputRequired of inputRequires)
                {
                    inputRequired.focus();
                    inputRequired.blur();
                }
                me.validateDatePickerRequired();

                me.validateNoContentFixedAsset();

                var dataToAddOrEditVoucher = CommonFunction.createObjectVoucher(this.voucher);

                // this.voucherDTO.voucherViewModel = dataToAddOrEditVoucher;
                // this.voucherDTO.fixedAssetViewModels = this.listFixedAssetChoosed;

                if(this.checkValidate){
                    this.$emit("transportVoucherDTO" ,dataToAddOrEditVoucher,this.listFixedAssetChoosed)
                    if(this.isAddOrEdit == Enum.FormModel.Add){
                        axios.post(Resource.API.Host + Resource.API.Vouchers + Resource.API.InsertVoucher , this.voucherDTO)
                        .then(() => {
                            this.$emit('hidedDialogVoucher');
                            this.$emit('updateVoucher');
                            this.$emit('showToastSuccess' , ToastJS.successMessage);
                        })
                        .catch(error => {
                            if(error.response.status == 400){
                                this.$emit('showDialogNotValid' , error.response.data.Data.userMsg)
                            }
                            else{
                                // this.$emit('showDialogNotValid' ,ToastJS.errorMessage)
                                console.log("There was an error!", error)
                            }
                        })
                    }else{
                        var checkCompareObject = CommonFunction.compareObject(this.voucher , this.checkVoucherChange) && CommonFunction.compareObjectArray(this.checkListFixedAssetChoosed , this.listFixedAssetChoosed)
                        if(checkCompareObject)
                        {
                            //nếu chưa có thay đổi gì bấm lưu => close form chi tiết chứng từ
                            this.$emit("hidedDialogVoucher")
                        }else{
                            //Nếu có thay đổi => show Confirm dialog trước khi sửa
                            // this.$emit("confirmSaveInfoVoucher" , Resource[Common.LanguageCode].MessageDialogConfirm.ConfirmSaveInfo , true)
                            //Gửi dữ liệu thay đổi lên Component UsedFixedAssetPage để khi bấm xác nhận lưu thì sẽ lưu dữ liệu đó xuống database
                            this.$emit("transmitVoucher" , dataToAddOrEditVoucher , this.listFixedAssetChoosed)
                            this.$emit("btnSaveInfoConfirm")
                        }
                    }
                }else{
                    var inputNotValid = dialogContent.querySelectorAll('input[validate="false"]')
                    inputNotValid[0].focus();
                }
            } catch (error) {
                console.log(error)
            }
        },

        /**
         * Method định dạng text khi nội dung hiển thị quá dài
         * CreatedBy : DDHung (16/11/2021)
        */
        formatText : function(value)
        {
            var pixel = CommonFunction.measureText(value);
            if(pixel > 145){
                return true
            }
            return false
        },

        /**
         * Method xử lý phân trang cho tài sản đã được chọn trên dilog chi tiết chứng từ
         * CreatedBy : DDHung (17/12/2021)
        */
        btnPaging : function(currentPage)
        {
            this.currenPage = currentPage
            this.startSTT = ((currentPage - 1) *  Resource.SearchPaging.PageSizeVoucherDialog + 1);
            var indexStart = (currentPage - 1) * Resource.SearchPaging.PageSizeVoucherDialog
            console.log(this.txtsearch)
            if(this.txtsearch)
            {
                console.log(this.listFixedAssetForSearch)
                this.listFixedAssetChoosedLoad = this.listFixedAssetForSearch.slice(indexStart ,  Resource.SearchPaging.PageSizeVoucherDialog + indexStart)
            }else{
                this.listFixedAssetChoosedLoad = this.listFixedAssetChoosed.slice(indexStart ,  Resource.SearchPaging.PageSizeVoucherDialog + indexStart)
            }
        }
    },

    computed: {
        inputSearch : {
            get(){
                return this.txtsearch
            },
            set(val){
                if (this.timeout) clearTimeout(this.timeout)
                this.timeout = setTimeout(() => {
                    this.txtsearch = val
                }, 300)
            }
        }
    },
    watch : {

        'voucher.voucherCode' : function(voucherCode)
        {
            var inputVoucherCode = this.$refs.voucherCode;
            var tootlTip = inputVoucherCode.parentElement.querySelector('.tooltiptext');
            var maxLength = parseInt(inputVoucherCode.getAttribute('maxlength'));
            if(voucherCode.length > 0){
                //Validate độ dài
                if(voucherCode.length >= maxLength)
                {
                    let nameToValidate = inputVoucherCode.getAttribute('nameToValidate');
                    inputVoucherCode.classList.add('border-red');
                    tootlTip.classList.add("showTooltip");
                    tootlTip.innerText = Resource[Common.LanguageCode].Validate.MaxLenght(nameToValidate , maxLength);
                    inputVoucherCode.setAttribute('validate' , false);
                }
                else{
                    inputVoucherCode.classList.remove('border-red');
                    tootlTip.classList.remove("showTooltip");
                    inputVoucherCode.removeAttribute('validate');
                }
            }else{
                let nameToValidate = inputVoucherCode.getAttribute('nameToValidate');
                inputVoucherCode.classList.add('border-red');
                tootlTip.classList.add("showTooltip");
                tootlTip.innerText = Resource[Common.LanguageCode].Validate.Required(nameToValidate);
                inputVoucherCode.setAttribute('validate' , false);
            }
        },

        listFixedAssetChoosed : function(listFixedAssetChoosed)
        {
            //Load lại dữ liệu
            this.listFixedAssetChoosedLoad = this.listFixedAssetChoosed.slice(0 ,  Resource.SearchPaging.PageSizeVoucherDialog)
            //Tính toán lại tổng bản ghi và số trang
            this.totalRecord = listFixedAssetChoosed.length
            this.totalPage = CommonFunction.calcTotalPage(this.totalRecord , Resource.SearchPaging.PageSizeVoucherDialog)

            //Summary
            var totalCost = 0;
            var totalFixedAssetCummulative = 0;
            var totalFixedAssetRest = 0;
            this.listFixedAssetChoosedLoad.forEach(item => {
                totalCost += item.Cost
                totalFixedAssetCummulative += item.FixedAssetCummulative
                totalFixedAssetRest += item.FixedAssetRest
            });
            this.totalCost = totalCost
            this.totalFixedAssetCummulative = totalFixedAssetCummulative
            this.totalFixedAssetRest = totalFixedAssetRest
        },

        // listFixedAssetChoosedLoad : function(listFixedAssetChoosedLoad)
        // {
        //     if(this.txtsearch)
        //     {
        //         this.totalRecord = listFixedAssetChoosedLoad.length
        //         this.totalPage = CommonFunction.calcTotalPage(this.totalRecord , Resource.SearchPaging.PageSizeVoucherDialog)
        //     }
        //     else{
        //         this.totalRecord = this.listFixedAssetChoosed.length
        //         this.totalPage = CommonFunction.calcTotalPage(this.totalRecord , Resource.SearchPaging.PageSizeVoucherDialog)
        //     }
        // },
        txtsearch : function(value)
        {
            if(value)
            {
                var dataSearch = this.listFixedAssetChoosed.filter(res => {
                    return CommonFunction.removeVietnameseTones(res["FixedAssetName"]).toLocaleLowerCase().includes(CommonFunction.removeVietnameseTones(value).toLocaleLowerCase());
                })
                this.listFixedAssetForSearch = dataSearch
                this.listFixedAssetChoosedLoad = dataSearch.slice(0 , Resource.SearchPaging.PageSizeVoucherDialog)
                this.totalRecord = dataSearch.length
                this.totalPage = CommonFunction.calcTotalPage(this.totalRecord , Resource.SearchPaging.PageSizeVoucherDialog)
            }
            else{
                this.totalRecord = this.listFixedAssetChoosed.length
                this.totalPage = CommonFunction.calcTotalPage(this.totalRecord , Resource.SearchPaging.PageSizeVoucherDialog)
                this.listFixedAssetChoosedLoad = this.listFixedAssetChoosed.slice(0 , Resource.SearchPaging.PageSizeVoucherDialog)
                this.listFixedAssetForSearch = []
            }
        }

    },

    filters : {
        /**
         * Hàm thực hiện format money
         * CreatedBy : DDHung (16/11/2021)
        */
        formatMoney : function(value){
            // if (value) {
            //     return Number(value).toFixed(0).replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1.");
            // }
            // return "";
            var moneyFormated = CommonFunction.formatMoney(value);
            return moneyFormated;
        },

        /**
         * Nếu trường nào không có dữ liệu thì sẽ hiển thị lên Grid là "Không có dữ liệu"
         * CreatedBy : DDHung (16/11/2021)
        */
        formatNoContent : function(value)
        {
            if(value)
            {
                return value;
            }else{
                return Enum.noContentGrid
            }
        },

    }
}
</script>
<style lang="">

</style>