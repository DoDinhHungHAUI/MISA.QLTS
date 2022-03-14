<template lang="">
    <div class="m-popup toggle-popup">
        <div class="wrap-popup"></div>
        <div class="popup-info-assets">
            <div class="header-popup">
                Thông tin tài sản
                <button class="close-popup" id="close-popup" v-on:click="hidedDialogFixedAsset" tabindex = "16" @focus = "onFocusButton($event)" @blur = "onBlurClose($event)"></button>
            </div>
            <div class="content-popup" ref="contentPopup">
                <div class = "form-fixedAsset form-fixedAsset-first">
                    <div class="input-form first-input-form">
                        <label>Mã tài sản <span style="color: red;">*</span></label><br />
                        <input 
                            type="text" class="input-popup-left first-field-popup input-text" title = ""
                            @blur = "validateFieldRequired($event)" fieldName = "FixedAssetCode" required
                            v-model = "fixedAsset.FixedAssetCode" nameToValidate = "Mã tài sản" ref="firstInputPopup" tabindex = "1"
                        />
                        <span class="tooltiptext"></span>
                    </div>
                    <div class="input-form first-input-form">
                        <label>Tên tài sản <span style="color: red;">*</span></label><br />
                        <input 
                            type="text" class="input-popup-right input-text" placeholder = "Nhập tên tài sản" required
                            @blur = "validateFieldRequired($event)" tabindex = "2" ref="fixedAssetName" title = "" maxlength = "150"
                            fieldName = "FixedAssetName" v-model = "fixedAsset.FixedAssetName" nameToValidate = "Tên tài sản"
                        />
                        <span class="tooltiptext fixedAssetName-tooltiptext"></span>
                    </div>
                </div>
                <div class = "form-fixedAsset">

                    <div class="input-form">
                        <label>Mã bộ phận sử dụng <span style="color: red;">*</span></label><br />
                        <MCombobox 
                            v-bind:dataCombobox = "departmentAssets" 
                            v-bind:idItem = "'DepartmentAssetId'" 
                            v-bind:codeItem = "'DepartmentAssetCode'" 
                            v-bind:nameItem = "'DepartmentAssetName'" 
                            v-bind:mPlaceholder = "'Chọn mã bộ phận sử dụng'"
                            v-on:btnButtonCombobox = "btnButtonCombobox"
                            v-on:itemComboboxClick = "itemComboboxClick"
                            v-bind:required = true
                            v-bind:tableNameItem = "'DepartmentAsset'"
                            v-bind:nameToValidate = "'Mã bộ phận sử dụng'"
                            v-bind:tabindex = "3"
                            v-bind:valueIdItem = "fixedAsset.DepartmentAssetId"
                            v-bind:isAddOrEdit = "isAddOrEdit"
                        />
                        <span class="tooltiptext"></span>
                    </div>
                    <div class="input-form">
                        <label>Tên bộ phận sử dụng</label><br />
                        <input 
                            type = "text" class="input-popup-right input-text" nameToCombobox = "DepartmentAssets" 
                            fieldName = "DepartmentAssetName" 
                            v-model = "fixedAsset.DepartmentAssetName"
                            tabindex = "4"
                        />
                        
                    </div>
                </div>
                <div class = "form-fixedAsset">
                    <div class="input-form">
                        <label>Mã loại tài sản <span style="color: red;">*</span></label><br />
                        <MCombobox 
                            v-bind:dataCombobox = "fixedAssetCategories" 
                            v-bind:idItem = "'FixedAssetCategoryId'" 
                            v-bind:codeItem = "'FixedAssetCategoryCode'"
                            v-bind:nameItem = "'FixedAssetCategoryName'" 
                            v-bind:mPlaceholder = "'Chọn mã loại tài sản'"
                            v-on:btnButtonCombobox = "btnButtonCombobox"
                            v-on:itemComboboxClick = "itemComboboxClick"
                            v-bind:required = true
                            v-bind:tableNameItem = "'FixedAssetCategory'"
                            v-bind:nameToValidate = "'Mã loại tài sản'"
                            v-bind:tabindex = "5"
                            v-bind:valueIdItem = "fixedAsset.FixedAssetCategoryId"
                            v-bind:isAddOrEdit = "isAddOrEdit"
                        />
                        <span class="tooltiptext"></span>
                    </div>
                    <div class="input-form">
                        <label>Tên loại tài sản</label><br />
                        <input 
                            type = "text" class="input-popup-right input-text" nameToCombobox = "FixedAssetCategories" 
                            fieldName = "FixedAssetCategoryName"
                            v-model = "fixedAsset.FixedAssetCategoryName"
                            tabindex = "6"
                        />
                        
                    </div>
                </div>
                <div class = "form-fixedAsset">
                    <div class="input-form input-form-typenumber">
                        <label>Số lượng <span style="color: red;">*</span></label><br />
                        <MInputTypeNumber 
                            v-on:validateFieldRequired = "validateFieldRequired"
                            v-bind:fieldName = "'Quantity'"
                            v-bind:fixedAsset = "fixedAsset"
                            v-on:augmentNumberClick = "augmentNumberClick"
                            v-on:reduceNumberClick = "reduceNumberClick"
                            v-on:restricCharacter = "restricCharacter"
                            v-bind:nameToValidate = "'Số lượng'"
                            v-bind:tabindex = "7"   
                        />
                        <span class="tooltiptext"></span>
                    </div>
                    <div class = "form-fixedAsset-right">
                        <div class="input-form-left">
                            <label>Nguyên giá <span style="color: red;">*</span></label><br />
                            <input 
                                type="text" class="input-popup-left align-right-input input-text" value="" 
                                fieldName = "Cost" 
                                v-model = "fixedAsset.Cost" @blur = "validateFieldRequired($event)" 
                                @keydown="restricCharacter($event)" nameToValidate = "Nguyên giá"
                                ref="Cost" tabindex = "8"
                                required
                            />
                            <span class="tooltiptext"></span>
                        </div>
                        <div class="input-form-right">
                            <label>Tỷ lệ hao mòn<span style = "color: red;">*</span></label><br />
                            <input 
                                type="text" class="input-popup-left input-popup-left align-right-input input-text" value="" required 
                                fieldName = "DepreciationRate" @keydown="restricCharacterDepreciationRate($event)"
                                v-model = "fixedAsset.DepreciationRate" @blur = "validateFieldRequired($event)"
                                ref="DepreciationRate" nameToValidate = "Tỷ lệ hao mòn" tabindex = "9"
                            />
                            <span class="tooltiptext"></span>
                        </div>
                    </div>  
                </div>
                <div class = "form-fixedAsset">
                    <div class="input-form">
                        <label>Ngày mua <span style = "color: red;">*</span></label><br />
                        <date-pick 
                            fieldName = "PurchaseDate" v-model = "fixedAsset.PurchaseDate" 
                            v-bind:months = "months" v-bind:weekdays= "weekdays"  :format="'DD/MM/YYYY'" value-format="MM/DD/YYYY"
                            @blur = "validateFieldRequired($event)"  class="datepickerRequired" nameToValidate = "Ngày mua"
                            ref="PurchaseDate"
                        ></date-pick>
                        <span class="tooltiptext"></span>
                    </div>
                    <div class = "form-fixedAsset-right">
                        <div class="input-form-left">
                            <label>Ngày bắt đầu sử dụng <span style="color: red;">*</span></label><br />
                            <date-pick 
                                v-bind:months = "months" 
                                v-bind:weekdays= "weekdays" :format="'DD/MM/YYYY'" value-format="MM/DD/YYYY"
                                fieldName = "ProductionDate" v-model = "fixedAsset.ProductionDate" 
                                class = "datepickerRequired" ref="ProductionDate"
                                nameToValidate = "Ngày bắt đầu sử dụng"
                            >
                            </date-pick>
                            <span class="tooltiptext"></span>
                        </div>
                        <div class="input-form-right">
                            <label>Năm theo dõi</label><br />
                            <input 
                                type="text" class="input-popup-left input-popup-left align-right-input input-text" 
                                fieldname = "TrackedYear" v-model = "fixedAsset.TrackedYear" disabled
                            />
                       
                        </div>
                    </div>
                </div>
                <div class = "form-fixedAsset"> 
                    <div class="input-form input-form-typenumber">
                        <label>Số năm sử dụng <span style="color: red;">*</span></label><br />
                        <MInputTypeNumber 
                            v-on:validateFieldRequired = "validateFieldRequired"
                            v-bind:fieldName = "'LifeTime'"
                            v-bind:fixedAsset = "fixedAsset"
                            v-on:augmentNumberClick = "augmentNumberClick"
                            v-on:reduceNumberClick = "reduceNumberClick"
                            v-on:restricCharacter = "restricCharacter"
                            v-bind:nameToValidate = "'Số năm sử dụng'"
                            v-bind:tabindex = "12"
                        />
                        <span class="tooltiptext"></span>
                    </div>
                     <div class="input-form input-form-flex">
                        <div class="input-form-left">
                            <label>Giá trị hao mòn năm <span style="color: red;">*</span></label><br />
                            <input type="text" class="input-popup-left align-right-input input-text"  
                                v-model = "fixedAsset.DepreciationYear" required @blur = "validateFieldRequired($event)" 
                                value = "" fieldname = "DepreciationYear" ref="DepreciationYear" nameToValidate = "Giá trị hao mòn năm"
                                tabindex = "13" @keydown="restricCharacter($event)"
                            />
                            <span class="tooltiptext"></span>
                        </div>
                    </div>
                </div>
            </div>
            <div class="footer-popup">
                <div class="wrap-button-footer">
                    <button type="button" class="m-btn-cancel" id="btn-close-popup" v-on:click = "hidedDialogFixedAsset" tabindex = "14" @focus = "onFocusButton($event)" @blur = "onBlurButton($event)">
                        Hủy
                    </button>
                    <button type="button" class="m-btn m-btn-save" id="btnSave" v-on:click = "btnSaveClick" tabindex = "15" @focus = "onFocusButton($event)" @blur = "onBlurButton($event)">
                        Lưu
                    </button>
                </div>
            </div>
        </div>
    </div>
</template>
<script>
import axios from 'axios'
/* Import Component */
import MCombobox from '../../components/base/MCombobox.vue'
import MInputTypeNumber from '../../components/base/MInputTypeNumber.vue'

/* File Javascript */
import {Resource} from '../../assets/js/common/resource'
import {Common} from '../../assets/js/common/common'
import {ToastJS} from '../../assets/js/common/messagetoast'
import {Enum} from '../../assets/js/common/enum'
/* import library */
import DatePick from 'vue-date-pick';
import 'vue-date-pick/dist/vueDatePick.css';
import { CommonFunction } from '../../assets/js/common/commonfunction';

export default {
    data() {
        return {
            checkValidate : true,
            fixedAsset : {
                /**
                 * Mã tài sản
                */
                FixedAssetCode : "",
                FixedAssetName : "",
                DepartmentAssetId : "",
                DepartmentAssetCode : "",
                DepartmentAssetName : "",
                FixedAssetCategoryId : "",
                FixedAssetCategoryCode : "",
                FixedAssetCategoryName : "",
                Quantity : 0,
                Cost : 0,
                DepreciationRate : 0,
                PurchaseDate : CommonFunction.formatDate(Date.now()),
                ProductionDate : CommonFunction.formatDate(Date.now()),
                TrackedYear : CommonFunction.getYearFromDate(new Date()),
                LifeTime : 0,
                DepreciationYear : 0,
            },
            
            weekdays : ['T2' , 'T3' ,'T4' , 'T5' , 'T6' , 'T7', 'CN'],
            months : ['Tháng 1' , 'Tháng 2' , 'Tháng 3' , 'Tháng 4' ,'Tháng 5' , 'Tháng 6' , 'Tháng 7' , 'Tháng 8' , 'Tháng 9' , 'Tháng 10' , 'Tháng 11' , 'Tháng 12']
        }
    },
    created() {
        try {            
            if(this.isAddOrEdit == Enum.FormModel.Add)
            {
                axios.get(Resource.API.Host + Resource.API.FixedAssets + Resource.API.GetNewCodeFixedAsset)
                .then(res => {
                    this.fixedAsset.FixedAssetCode = res.data
                })
                .catch(error => {
                    console.log(error)
                })
            }else{
                if(this.isAddOrEdit == Enum.FormModel.Edit)
                {
                    this.fixedAsset.FixedAssetCode = this.fixedAssetEdit.FixedAssetCode
                }else{
                    axios.get(Resource.API.Host + Resource.API.FixedAssets + Resource.API.GetNewCodeFixedAsset)
                    .then(res => {
                        this.fixedAsset.FixedAssetCode = res.data
                    })
                    .catch(error => {
                        console.log(error)
                    })
                }
                this.fixedAsset.FixedAssetName = this.fixedAssetEdit.FixedAssetName
                this.fixedAsset.DepartmentAssetId = this.fixedAssetEdit.DepartmentAssetId
                this.fixedAsset.DepartmentAssetName = this.fixedAssetEdit.DepartmentAssetName
                this.fixedAsset.DepartmentAssetCode = this.fixedAssetEdit.DepartmentAssetCode
                this.fixedAsset.FixedAssetCategoryId = this.fixedAssetEdit.FixedAssetCategoryId
                this.fixedAsset.FixedAssetCategoryName = this.fixedAssetEdit.FixedAssetCategoryName
                this.fixedAsset.FixedAssetCategoryCode = this.fixedAssetEdit.FixedAssetCategoryCode
                this.fixedAsset.Quantity = this.fixedAssetEdit.Quantity
                this.fixedAsset.Cost = CommonFunction.formatMoney(this.fixedAssetEdit.Cost)
                this.fixedAsset.DepreciationRate = this.fixedAssetEdit.DepreciationRate
                this.fixedAsset.PurchaseDate = CommonFunction.formatDate(this.fixedAssetEdit.PurchaseDate)
                this.fixedAsset.TrackedYear = this.fixedAssetEdit.TrackedYear
                this.fixedAsset.LifeTime = this.fixedAssetEdit.LifeTime
                this.fixedAsset.ProductionDate = CommonFunction.formatDate(Date.now()),
                this.fixedAsset.DepreciationYear = this.fixedAssetEdit.DepreciationYear
            }
        } catch (error) {
            console.log(error);
        }
    },

    mounted() {
        try {
            
            //Gán sự kiện onblur cho hai ô input ngày mua 
            var divPurchaseDate = this.$refs.contentPopup.querySelectorAll('div[fieldname="PurchaseDate"]')
            var inputPurchaseDate = divPurchaseDate[0].getElementsByTagName('input')[0];
            var divProductionDate = this.$refs.contentPopup.querySelectorAll('div[fieldname="ProductionDate"]')
            var inputProductionDate = divProductionDate[0].getElementsByTagName('input')[0];
            inputPurchaseDate.setAttribute("tabindex" , "10");
            inputProductionDate.setAttribute("tabindex", "11");

            //Xử lý việc tự động sinh dấu "/" khi nhập dữ liệu là ngày tháng
            inputProductionDate.addEventListener('keyup', function(e) {
                var self = this;
                CommonFunction.handleInputValueDate(self , e);
            });

            inputPurchaseDate.addEventListener('keyup' , function(e){
                var self = this;
                CommonFunction.handleInputValueDate(self , e);
            })

            //Focus vào input Mã tài sản
            this.$refs.firstInputPopup.focus();
            if(this.isAddOrEdit == Enum.FormModel.Edit || this.isAddOrEdit == Enum.FormModel.Replication){
                var cbPopups = this.$refs.contentPopup.getElementsByClassName('m-combobox');
                for(let cbPopup of cbPopups)
                {
                    var fieldName = cbPopup.getAttribute('nameId');
                    var cbItems = cbPopup.getElementsByClassName('m-combobox-item');

                    for(let cbItem of cbItems){
                        var value = cbItem.getAttribute('value');
                        if(this.fixedAsset[fieldName] == value)
                        {
                            cbPopup.getElementsByClassName('m-combobox-input')[0].value = cbItem.innerText.toString();
                            cbItem.classList.add("m-item-selected")
                            cbItem.innerHTML = Resource.Icon.FaCheck.toString() + cbItem.innerText.toString();
                            break;
                        }
                    }
                } 
            }

            //Disable các ô input , combobox mỗi khi sửa tài sản đã ghi tăng
            var contentPopup = this.$refs.contentPopup
            var inputTexts = contentPopup.querySelectorAll('.input-text')
            var datePickers = contentPopup.querySelectorAll('.vdpComponent>input')
            var mComboboxButtons = contentPopup.querySelectorAll('.m-combobox-button')
            var iconTops = contentPopup.querySelectorAll('.input-form .icon-top')
            var iconBottoms = contentPopup.querySelectorAll('.input-form .icon-down')
            if(this.fixedAssetEdit.StatusFixedAsset == Enum.IsUsedFixedAsset.UsedFixedAsset && this.isAddOrEdit == Enum.FormModel.Edit)
            {
                for (const input of inputTexts) {
                    if(input.getAttribute('fieldname') != 'FixedAssetName'){
                        input.setAttribute('disabled' , '')
                    }
                }

                for (const mCbox of mComboboxButtons) {
                    mCbox.setAttribute('disabled' , '')
                    mCbox.style.background = '#fafafa'
                }

                for (const datePicker of datePickers) {
                    datePicker.setAttribute('disabled','')
                }

                for(const iconTop of iconTops)
                {
                    iconTop.style.display = "none"
                    var input = iconTop.parentElement.querySelector('input')
                    input.classList.add('change-padding')
                }

                for(const iconBottom of iconBottoms)
                {
                    iconBottom.style.display = "none"
                }
            }else{

                for (const input of inputTexts) {
                    if(input.getAttribute('fieldname') != 'FixedAssetName'){
                        input.removeAttribute('disabled')
                    }
                }

                for (const mCbox of mComboboxButtons) {
                    mCbox.removeAttribute('disabled')
                    mCbox.style.background = ''
                }

                for (const datePicker of datePickers) {
                    datePicker.removeAttribute('disabled')
                }

                for(const iconTop of iconTops)
                {
                    iconTop.style.display = "block"
                    let input = iconTop.parentElement.querySelector('input')
                    input.classList.remove('change-padding')
                }

                for(const iconBottom of iconBottoms)
                {
                    iconBottom.style.display = "block"
                }

                var trackedYear = document.querySelector('input[fieldname="TrackedYear"]')
                trackedYear.setAttribute('disabled','')
            }
        } catch (error) {
            console.log(error);
        }
    },

    components : {
        MCombobox,
        DatePick,
        MInputTypeNumber
    },

    props : {
        departmentAssets : {
            type : Array
        },
        fixedAssetCategories : {
            type : Array
        },
        isAddOrEdit : {
            type : Number
        },
        fixedAssetEdit : {
            type:Object
        },
        ListFieldNotValidRequired : {
            type : Array
        }
    },

    methods: {

        /**
         * Method thực hiện close Popup form chi tiết tài sản
         * CreatedBy : DDHung (16/11/2021)
        */
        hidedDialogFixedAsset(){
            this.$emit('confirmHidedDialog', Resource[Common.LanguageCode].MessageDialogConfirm.ConfirmHidePopupFixedAsset, true);
        },

        /**
         * Method xử lý việc show/Ẩn combobox
         * CreatedBy : DDHung (16/11/2021)
        */
        btnButtonCombobox : function(event){
           this.$emit('btnButtonCombobox' , event);
        },

        /**
         * Method xử lý khi selected 1 giá trị của combobox
         * CreatedBy : DDHung (16/11/2021)
        */
        itemComboboxClick(id ,code ,name , event){
            
            try {
                var data = {
                    name : code,
                    event : event
                }
                var cbItem;
                if(event.target)
                {
                    cbItem = event.target;
                }else{
                    cbItem = event;
                }
                
                var combobox = cbItem.parentElement.parentElement;
                // var fieldName = cbItem.parentElement.parentElement.getAttribute('fieldName')
                var fieldName = combobox.getAttribute('nameId')
                this.fixedAsset[fieldName] = id;

                //Load tên tương ứng với mã đã chọn
                var fieldNameItem = combobox.getAttribute('fieldNameItem');
                
                //Kiểm tra nếu là loại tài sản thì sẽ load lên số năm sử dụng và tỷ lệ hao mòn tương ứng
                var tableNameItem = combobox.getAttribute('tableNameItem');
                if(tableNameItem == "FixedAssetCategory")
                {
                    var fixedAssetCategory = this.fixedAssetCategories.find(item =>  item['FixedAssetCategoryId'] == id);
                    this.fixedAsset.LifeTime = fixedAssetCategory['LifeTime'];
                    // this.fixedAsset.DepreciationRate = fixedAssetCategory['DepreciationRate'];
                }

                if(this.fixedAsset[fieldName])
                {
                    this.fixedAsset[fieldNameItem] = name;
                    combobox.classList.remove('border-red');
                    // combobox.removeAttribute('title')
                    var toolTip =  combobox.parentElement.querySelector(".tooltiptext");
                    toolTip.classList.remove("showTooltip");
                    combobox.removeAttribute('validate')
                }
                this.$emit('itemComboboxClick' , data);
            } catch (error) {
                console.log(error)
            }
        },

        /**
         * Method thực hiện việc tăng giá trị của ô input lên 1 khi click vào tăng giá trị tại ô input đó
         * createdBy : DDHung (17/11/2021)
         */
        augmentNumberClick(fieldName , value)
        {
            this.fixedAsset[fieldName] =  value + 1;
        },

        /**
         * Method thực hiện việc giảm giá trị của ô input lên 1 khi click vào giảm giá trị tại ô input đó
         * createdBy : DDHung (17/11/2021)
        */
        reduceNumberClick : function(fieldName , value)
        {
            this.fixedAsset[fieldName] =  value - 1;
        },

        /**
         * Method thực hiện validate các trường bắt buộc nhập
         * CreatedBy : DDHung (16/11/2021)
        */
        validateFieldRequired : function(event){
            try {

                var self = this;
                var input = event.target;
                var fieldName = input.getAttribute('fieldname');
                var nameToValidate = input.getAttribute('nameToValidate');
                var value = this.fixedAsset[fieldName];
                var tootlTip = input.parentElement.querySelector('.tooltiptext');
                if(input.classList.contains("input-typenumber-popup"))
                {
                    tootlTip = input.parentElement.parentElement.querySelector('.tooltiptext');
                }
                //Bắt buộc nhập
                if(!value){
                    self.ListFieldNotValidRequired.push(nameToValidate);
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
         * Method thực hiện validate các combobox bắt buộc chọn
         * CreatedBy : DDHung (16/11/2021)
        */
        validateComboboxRequired : function()
        {
            try {
                var dialogContent =  this.$refs.contentPopup;
                var comboboxRequired = dialogContent.querySelectorAll('.comboboxRequired');
            
                //var len = comboboxRequired.length;
                for(let element of comboboxRequired)
                {
                    var nameIdCombobox =  element.getAttribute("nameId");
                    var nameToValidate = element.getAttribute("nameToValidate");
                    var toolTip = element.parentElement.querySelector(".tooltiptext");
                    if(!this.fixedAsset[nameIdCombobox])
                    {
                        this.ListFieldNotValidRequired.push(nameToValidate);
                        //this.ListFieldNotValidRequired.push(nameToValidate);
                        element.classList.add('border-red');
                       
                        toolTip.classList.add("showTooltip");
                        toolTip.innerText = Resource[Common.LanguageCode].Validate.Required(nameToValidate);
                        // element.setAttribute('title' , Resource[Common.LanguageCode].Validate.Required(nameToValidate));
                        //element.setAttribute('validate' , false)
                        element.firstElementChild.setAttribute('validate' , false);
                        this.checkValidate = false;
                    }else{
                        element.classList.remove('border-red');
                        // element.removeAttribute('title')
                        toolTip.classList.remove("showTooltip");
                        element.firstElementChild.removeAttribute('validate')
                        console.log(element)
                    }
                }
            } catch (error) {
                console.log(error);
            }
        },

        /**
         * Method thực hiện validate datepicker bắt buộc chọn
         * CreatedBy : DDHung (17/11/2021)
        */
        validateDatePickerRequired(){
            try {
                var dialogContent =  this.$refs.contentPopup;
                var datePickers = dialogContent.querySelectorAll('.datepickerRequired');
               
                for(let datePicker of datePickers)
                {
                    var fieldName = datePicker.getAttribute('fieldname')
                    var nametovalidate = datePicker.getAttribute("nametovalidate")
                    var toolTip = datePicker.parentElement.querySelector(".tooltiptext")
                    if(!this.fixedAsset[fieldName]){
                        this.ListFieldNotValidRequired.push(nametovalidate)
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
         * Sự kiện khi blur khỏi ô tỷ lệ hao mòn
         * CreatedBy : DDHung (22/11/2021)
         */
        onBlurDepreciationRate : function(event)
        {
            try {

                //focus vào ô Ngày mua
                var divPurchaseDate = this.$refs.contentPopup.querySelectorAll('div[fieldname="PurchaseDate"]')
                var inputPurchaseDate = divPurchaseDate[0].getElementsByTagName('input')[0];
                inputPurchaseDate.focus();
                var input = event.target;
                var fieldName = input.getAttribute('fieldname');
                var nameToValidate = input.getAttribute('nameToValidate');
                var value = this.fixedAsset[fieldName];
                if(!value){
                    this.ListFieldNotValidRequired.push(nameToValidate);
                    input.classList.add('border-red');
                    input.setAttribute('title' , Resource[Common.LanguageCode].Validate.Required(nameToValidate));
                    input.setAttribute('validate' , false)
                    this.checkValidate = false;
                }
                else{
                    input.classList.remove('border-red');
                    input.removeAttribute('title')
                    input.removeAttribute('validate')
                }
            } catch (error) {
                console.log(error)
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
                var dialogContent =  this.$refs.contentPopup;
                var inputRequires = dialogContent.querySelectorAll('input[required]');
                for(let inputRequired of inputRequires)
                {
                    inputRequired.focus();
                    inputRequired.blur();
                }
                me.validateComboboxRequired();
                me.validateDatePickerRequired();
            
                var dataToAddFixedAsset = CommonFunction.createObjectToAddOrEdit(this.fixedAsset); 
                
                if(this.checkValidate)
                {
                    if(this.isAddOrEdit == Enum.FormModel.Add || this.isAddOrEdit == Enum.FormModel.Replication)
                    {
                        axios.post(Resource.API.Host + Resource.API.FixedAssets , dataToAddFixedAsset)
                        .then(() => {
                            this.$emit('hidedDialogFixedAsset');
                            this.$emit('updateFixedAsset');
                            this.$emit('showToastSuccess' , ToastJS.successMessage);
                        })
                        .catch(error => {
                            if(error.response.status == 400){
                                this.$emit('showDialogNotValid' , error.response.data.Data.userMsg)
                            }
                            else{
                                this.$emit('showDialogNotValid' ,ToastJS.errorMessage)
                                console.log("There was an error!", error);
                            }
                        })
                    }else{
                        //Confirm trước khi sửa
                        // this.$emit("confirmSaveInfoFixedAsset" , Resource[Common.LanguageCode].MessageDialogConfirm.ConfirmSaveInfo , true)
                        
                        //Gửi dữ liệu thay đổi lên Component FixedAssetPage để khi bấm xác nhận lưu thì sẽ lưu dữ liệu đó xuống database
                        this.$emit("transmitFixedAsset" , this.fixedAsset) 
                        this.$emit("btnSaveInfoConfirm")
                    }
                }else{
                    
                    // this.$emit("onTransportListFieldNotValid" , this.ListFieldNotValidRequired)
                    // this.$emit("onFucusFirstInputNotValid" , dialogContent)
                    var inputNotValid = dialogContent.querySelectorAll('input[validate="false"]')
                    inputNotValid[0].focus();
                }
            } catch (error) {
                console.log(error)
            }
        },

        /**
         * Method hạn chế nhập ký tự character vào các ô nhập vào là chữ số
         * CreatedBy : DDHung (16/11/2021)
        */
        restricCharacter : function(e){
            if ((/^\d*$/.test(e.key) == false && e.keyCode != 8 && e.keyCode != 9) || e.key < 0) {
                e.preventDefault();
            }
        },

        /**
         * Method hạn chế nhập ký tự character vào các ô tỷ lệ hao mòn
         * CreatedBy : DDHung (22/11/2021)
        */
        restricCharacterDepreciationRate : function(e){
            if (/^\d*$/.test(e.key) == false && e.keyCode != 8 && e.keyCode != 9 && e.keyCode != 190 && e.keyCode != 188 && e.keyCode != 0) {
                e.preventDefault();
            }
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
         * Method được gọi khi blur khỏi button trên dialog
         * CreatedBy : DDHung (22/11/2021) 
         */
        onBlurButton : function(event)
        {
            var button = event.target;
            button.classList.remove("border-tab")
        },

        /**
         * Method được gọi khi blur khỏi nút close trên dialog
         * CreatedBy : DDHung (22/11/2021)
         */
        onBlurClose : function(event)
        {
            var inputFirst = this.$refs.contentPopup.getElementsByClassName('first-field-popup');
            var button = event.target;
            button.classList.remove("border-tab")
            inputFirst[0].focus();
        },
    },
    watch : {
     
        'fixedAsset.FixedAssetName' : function(fixedAssetName)
        {
            var inputFixedAssetName = this.$refs.fixedAssetName;
            var tootlTip = inputFixedAssetName.parentElement.querySelector('.tooltiptext');
            var maxLength = parseInt(inputFixedAssetName.getAttribute('maxlength'));
            if(fixedAssetName.length > 0)
            {
                //Validate độ dài
                if(fixedAssetName.length >= maxLength)
                {
                    let nameToValidate = inputFixedAssetName.getAttribute('nameToValidate');
                    inputFixedAssetName.classList.add('border-red');
                    tootlTip.classList.add("showTooltip");
                    tootlTip.innerText = Resource[Common.LanguageCode].Validate.MaxLenght(nameToValidate , maxLength);
                    inputFixedAssetName.setAttribute('validate' , false);
                }
                else{
                    inputFixedAssetName.classList.remove('border-red');
                    // inputFixedAssetName.removeAttribute('title');
                    tootlTip.classList.remove("showTooltip");
                    inputFixedAssetName.removeAttribute('validate');
                }
                
            }else{
                let nameToValidate = inputFixedAssetName.getAttribute('nameToValidate');
                inputFixedAssetName.classList.add('border-red');
                tootlTip.classList.add("showTooltip");
                tootlTip.innerText = Resource[Common.LanguageCode].Validate.Required(nameToValidate);
                // inputFixedAssetName.setAttribute('title' , Resource[Common.LanguageCode].Validate.Required(nameToValidate));
                inputFixedAssetName.setAttribute('validate' , false);
            }
        },

        'fixedAsset.Cost' : function(val)
        {
            try {
                var value = CommonFunction.convertMoneyToNumber(val);
                var input = this.$refs.Cost;
                var tootlTip = input.parentElement.querySelector('.tooltiptext');
                if(val)
                {
                    input.classList.remove('border-red');
                    // input.removeAttribute('title')
                    tootlTip.classList.remove('showTooltip')
                    input.removeAttribute('validate')
                }else{
                    var nameToValidate = input.getAttribute('nameToValidate');
                    input.classList.add('border-red');
                    tootlTip.classList.add("showTooltip");
                    tootlTip.innerText = Resource[Common.LanguageCode].Validate.Required(nameToValidate);
                    // inputFixedAssetName.setAttribute('title' , Resource[Common.LanguageCode].Validate.Required(nameToValidate));
                    input.setAttribute('validate' , false);
                }
                
                //Khi Nguyên giá thay đổi
                // +> Tính hao mon năm = tỷ lệ hao mòn x nguyên giá

                var depreciationYear = value * parseFloat(CommonFunction.ConvertDepreciationRateToNumber(this.fixedAsset.DepreciationRate));
                this.fixedAsset.DepreciationYear = CommonFunction.formatMoney(depreciationYear);
                value = CommonFunction.formatMoney(value);

                this.fixedAsset.Cost = value;

            } catch (error) {
                console.log(error)
            }
        },
        'fixedAsset.DepreciationRate' : function(val){
            try {
                this.fixedAsset.DepreciationRate = CommonFunction.ConvertDepreciationRateToLoad(val);
                var input = this.$refs.DepreciationRate;
                var tootlTip = input.parentElement.querySelector('.tooltiptext');
                if(val)
                {
                    input.classList.remove('border-red');
                    // input.removeAttribute('title')
                    tootlTip.classList.remove("showTooltip")
                    input.removeAttribute('validate')
                }else{
                    var nameToValidate = input.getAttribute('nameToValidate');
                    input.classList.add('border-red');
                    tootlTip.classList.add("showTooltip");
                    tootlTip.innerText = Resource[Common.LanguageCode].Validate.Required(nameToValidate);
                    // inputFixedAssetName.setAttribute('title' , Resource[Common.LanguageCode].Validate.Required(nameToValidate));
                    input.setAttribute('validate' , false);
                }
                let depreciationRateValue = CommonFunction.ConvertDepreciationRateToNumber(this.fixedAsset.DepreciationRate);
                var cost = CommonFunction.convertMoneyToNumber(this.fixedAsset.Cost);
                var depreciationYear = depreciationRateValue * cost;
                this.fixedAsset.DepreciationYear = CommonFunction.formatMoney(depreciationYear);
            
            } catch (error) {
                console.log(error)
            }
        },
        'fixedAsset.PurchaseDate' : function(val)
        {
            try {
                if(val)
                {
                    var datePicker = this.$refs.PurchaseDate.$el;
                    var inputDatePicker = datePicker.firstElementChild;
                    inputDatePicker.classList.remove('border-red');
                    inputDatePicker.removeAttribute('title');
                    inputDatePicker.removeAttribute('validate');
                }
            } catch (error) {
                console.log(error)
            }
        },
        'fixedAsset.Quantity' : function(val){
            try {
                var input = document.querySelectorAll("input[fieldName = 'Quantity']");
                var toolTip = input[0].parentElement.parentElement.querySelector(".tooltiptext")
                if(val <= 0)
                {   
                    var nameToValidate = input[0].getAttribute('nameToValidate');
                    toolTip.classList.add("showTooltip");
                    toolTip.innerText = Resource[Common.LanguageCode].Validate.Required(nameToValidate);
                    input[0].classList.add('border-red');
                    this.fixedAsset.Quantity = 0;
                }
                else
                {
                    input[0].classList.remove('border-red');
                    toolTip.classList.remove("showTooltip");
                    input[0].removeAttribute('validate')
                    this.fixedAsset.Quantity = val;
                }
            } catch (error) {
                console.log(error);
            }
        },
        'fixedAsset.LifeTime' : function(val){
            try {
                var input = document.querySelectorAll("input[fieldName = 'LifeTime']");
                var toolTip = input[0].parentElement.parentElement.querySelector(".tooltiptext")
                if(val > 0)
                {   
                    //Tính lại tỷ lệ hao mòn bằng 1/số năm sử dụng
                    var depreciationRate = 1/val;
                    this.fixedAsset.DepreciationRate = depreciationRate.toFixed(3);
                    // var input = document.querySelectorAll("input[fieldName = 'LifeTime']");
                    input[0].classList.remove('border-red');
                    // input[0].removeAttribute('title')
                    toolTip.classList.remove("showTooltip");
                    input[0].removeAttribute('validate')
                }
                else
                {
                    input[0].classList.add('border-red');
                    var nameToValidate = input[0].getAttribute('nameToValidate');
                    toolTip.classList.add("showTooltip");
                    toolTip.innerText = Resource[Common.LanguageCode].Validate.Required(nameToValidate);
                    this.fixedAsset.LifeTime = 0;
                    this.fixedAsset.DepreciationRate = 0;
                }
            } catch (error) {
                console.log(error)
            }
        },    
        'fixedAsset.DepreciationYear' : function(val)
        {
            try {
                var input = this.$refs.DepreciationYear;
                var toolTip = input.parentElement.querySelector('.tooltiptext');
                var depreciationYear = CommonFunction.convertMoneyToNumber(val);
                console.log(depreciationYear);
                if(depreciationYear > 0)
                {
                    input.classList.remove('border-red');
                    // input.removeAttribute('title')
                    toolTip.classList.remove("showTooltip")
                    input.removeAttribute('validate')
                }else{
                    var nameToValidate = input.getAttribute('nameToValidate');
                    input.classList.add('border-red');
                    toolTip.classList.add("showTooltip");
                    toolTip.innerText = Resource[Common.LanguageCode].Validate.Required(nameToValidate);
                }
            } catch (error) {
                console.log(error)
            }
        },
        'fixedAsset.ProductionDate' : function(val)
        {
            try {
                if(val)
                {
                    console.log(val);
                    var dialogContent =  this.$refs.contentPopup;
                    var datePicker = dialogContent.querySelector('div[fieldname="ProductionDate"]');
                    var toolTip = datePicker.parentElement.querySelector(".tooltiptext");
                    if(toolTip.classList.contains("showTooltip"))
                    {
                        datePicker.firstElementChild.classList.remove('border-red');
                        toolTip.classList.remove("showTooltip")
                        datePicker.firstElementChild.removeAttribute('validate')
                    }
                }
            } catch (error) {
                console.log(error);   
            }
        },
        'fixedAsset.FixedAssetCode' : function(val)
        {
            try {
                var dialogContent =  this.$refs.contentPopup;
                var inputFixedAssetCode = dialogContent.querySelector('input[fieldname="FixedAssetCode"]');
                var toolTip = inputFixedAssetCode.parentElement.querySelector('.tooltiptext');
                if(val)
                {
                    
                    if(toolTip.classList.contains("showTooltip"))
                    {
                        inputFixedAssetCode.classList.remove('border-red'); 
                        toolTip.classList.remove("showTooltip")
                        inputFixedAssetCode.removeAttribute('validate')
                    }
                }else{
                    var nameToValidate = inputFixedAssetCode.getAttribute('nameToValidate');
                    inputFixedAssetCode.classList.add('border-red');
                    toolTip.classList.add("showTooltip");
                    toolTip.innerText = Resource[Common.LanguageCode].Validate.Required(nameToValidate);
                }
            } catch (error) {
                console.log(error);
            }
        }
    }
}
</script>
<style lang="">
    
</style>