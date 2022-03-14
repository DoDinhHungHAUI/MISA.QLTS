<template lang="">
    <div class="m-popup toggle-popup"><!-- toggle-popup -->
        <div class="wrap-popup"></div>
        <div class="popup-orgin-funding">
            <div class="header-popup header-orgin-funding m-wrap-tooltip">
                <!-- Sửa tài sản Lắp đặt mái tồn khu rữa của học sinh -->
                Sửa nguồn kinh phí tài sản
                <button class="close-popup" id="close-popup" @click = "closeDialogOriginFunding">
                </button>
                <span class="m-tooltiptext m-tooltip-close">Đóng</span>
            </div>
            <div class = "wrap-form-orgin-funding">
                <div class="content-popup content-origin-funding">
                    <div class="input-form input-form-origin-funding">
                        <label>Bộ phận sử dụng</label><br>
                        <input type="text" title="" tabindex="1" class="input-text input-disable-origin" value = "Trường tiểu học Hải Đình" disabled>
                    </div>
                    <div class="input-form input-form-origin-funding m-custom-form title-cost">
                        <span>Nguyên giá</span>
                    </div>
                    <div class="input-form input-form-origin-funding m-custom-form">
                        <div class = "label-origin-cost wrap-label-origin-cost">
                            <div class = "label-cost-declare">
                                <span>Nguồn hình thành</span>
                            </div>
                            <div class = "label-cost-value">
                                <span>Giá trị</span>
                            </div>
                        </div>
                        <div class = "origin-cost">
                    
                            <div class = "label-origin-cost origin-cost-append" v-for = "item in originFundingFixedAsset" v-bind:key = "item.idArray">
                                <div class = "label-cost-declare">
                                    <MComboboxFilter 
                                        v-bind:dataCombobox = "originFundings" 
                                        v-bind:idItem = "'OriginFundingId'" 
                                        v-bind:nameItem = "'OriginFundingName'" 
                                        v-bind:mPlaceholder = "'Chọn nguồn kinh phí'"
                                        v-on:itemComboboxClick = "itemComboboxClick"
                                        v-bind:isCustomCombobox = "isCustomCombobox"
                                        v-bind:originFundingId = "item.OriginFundingId"
                                        v-bind:idCheckOriginFunding = "item.idCheckOriginFunding"
                                    />  
                                    <span class="error-duplicate-origin"></span>
                                </div>
                                <div class = "label-cost-value">
                                    <input type="text" title="" v-bind:idCheckOriginFunding = "item.idCheckOriginFunding" class="input-text input-origin-cost-value" @keydown = "restricCharacter($event)" 
                                       @keyup = "formatCostToMoney($event)" v-bind:value = "item.ValueOriginFunding"
                                    >
                                    <span class="error-required-origin"></span>
                                </div>
                                <div class = "icon-add-origin-cost m-wrap-tooltip">
                                    <button class = "btn-add-origin-funding" @click = "btnAddOriginFunding">
                                        <i class="fas fa-plus"></i>
                                    </button>
                                    <span class="m-tooltiptext m-tooltip-add-origin-funding">Thêm nguồn kinh phí</span>
                                </div>
                                <div class = "icon-delete-origin-cost m-wrap-tooltip">
                                    <button class = "btn-delete-origin-funding" v-bind:idCheckOriginFunding = "item.idCheckOriginFunding" @click = "btnDeleteOriginFunding($event)">
                                        <i class="fas fa-minus"></i>
                                    </button>
                                    <span class="m-tooltiptext m-tooltip-delete-origin-funding">Xóa nguồn kinh phí</span>
                                </div>
                            </div>
                        </div>
                         <div class = "label-origin-cost">
                            <div class = "label-cost-declare">
                                <input type = "text" class = "title-total-cost-textbox" disabled value = "Tổng"  />
                            </div>
                            <div class = "label-cost-value">
                                <input type="text" title="" class="input-text input-origin-cost-value total-cost-origin-funding" disabled v-model = "totalCostOriginFunding" >
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="footer-popup">
                <div class="wrap-button-footer">
                    <button type="button" class="m-btn-cancel" id="btn-close-popup">
                        Hủy
                    </button>
                    <button type="button" class="m-btn m-btn-save" id="btnSave" @click = "btnSaveOriginFunding">
                        Lưu
                    </button>
                </div>
            </div>
        </div>
    </div>
</template>
<script>

import axios from 'axios'

/* Component */
import MComboboxFilter from '../../components/base/MComboboxFilter.vue'

/* File javascript */
import {Resource} from '../../assets/js/common/resource'
import {CommonFunction} from '../../assets/js/common/commonfunction'
import { Common } from '../../assets/js/common/common'

export default {

    data() {
        return {
            originFundings : [],
            isCustomCombobox : true,
            originFundingFixedAsset : [],
            listOriginFundingToSave : [],
            totalCostOriginFunding : null,
            // isCheckRequiredCombobox : false
        }
    },

    components : {
        MComboboxFilter
    },

    props : {
        listOriginFundingByFixedAssetId : {
            typeof : Array
        },
        costDefault : {
            typeof : Number
        },
        fixedAssetIdDefault : {
            typeof :String
        }
    },

    created() {
        //gọi api lấy danh sách nguồn
        axios.get(Resource.API.Host + Resource.API.OriginFunding)
        .then(res => {
            this.originFundings = res.data
        })
        .catch(error => {
            console.log(error)
        })

        if(this.listOriginFundingByFixedAssetId.length == 0)
        {
            //nguồn mặc định
            var originFunding = {
                OriginFundingFixedAssetId : null,
                idCheckOriginFunding : 1,
                OriginFundingId : '3541ff76-58f0-6d1a-e836-63d5d5eff719',
                ValueOriginFunding : CommonFunction.formatMoney(this.costDefault)
            };
            this.totalCostOriginFunding = originFunding.ValueOriginFunding
            this.originFundingFixedAsset.push(originFunding)
        }else{
            var totalCost = 0;
            //load danh sách nguồn
            this.listOriginFundingByFixedAssetId.forEach((item , index) => {
                var originFundLoad = {
                    OriginFundingFixedAssetId : item.OriginFundingFixedAssetId,
                    idCheckOriginFunding : index + 1,
                    OriginFundingId : item.OriginFundingId,
                    ValueOriginFunding : CommonFunction.formatMoney(item.ValueOriginFunding)
                }
                totalCost += parseFloat(CommonFunction.convertMoneyToNumber(originFundLoad.ValueOriginFunding))
                this.originFundingFixedAsset.push(originFundLoad)
            });
            this.totalCostOriginFunding = CommonFunction.formatMoney(totalCost)
        }
    },

    mounted() {
        var wrapOrginFunding = document.querySelector('.wrap-form-orgin-funding')
        var originCostAppends = wrapOrginFunding.querySelectorAll('.origin-cost-append')
        var firstIconDeleteOriginCost = originCostAppends[0].querySelector('.icon-delete-origin-cost')
        firstIconDeleteOriginCost.classList.add('hide-icon-delete')
    },

    methods: {

        /**
         * Method xử lý khi click lưu danh sách nguồn
         * CreatedBy : DDHung (21/12/2021) 
        */
        btnSaveOriginFunding : function()
        {
            // var isCheckDupliacteOrgin = true;
            // var noContentValueOrigin = true;
            // var wrapOrginFunding = document.querySelector('.wrap-form-orgin-funding')       
            // var listCombobox = wrapOrginFunding.querySelectorAll('.m-custom-combobox')
            // var listOriginCost = wrapOrginFunding.querySelectorAll(".input-origin-cost-value")
            // //Validate combobox nguồn kinh phí 
            // for (const cbox of listCombobox) {
            //     if(cbox.classList.contains('border-red'))
            //     {
            //         isCheckDupliacteOrgin = false
            //         break;
            //     }
            // }

            // //Validate combobox nguồn kinh phí khi chưa chọn dữ liệu
            // for (const cbox of listCombobox) {
            //     var input = cbox.querySelector('.m-combobox-input')
            //     if(!input.value)
            //     {
            //         cbox.classList.add('border-red')
            //         let errorMessage =  cbox.parentElement.querySelector(".error-duplicate-origin")
            //         errorMessage.innerText =  Resource[Common.LanguageCode].ValidateDialogOrigin.RequiredCombobox
            //     }
            // }

            // //Validate ô nhập giá trị nguồn kinh phí
            // for(const inputOrigin of listOriginCost)
            // {
            //     if(!inputOrigin.value)
            //     {
            //         var errorMessage = inputOrigin.parentElement.querySelector('.error-required-origin')
            //         errorMessage.innerText = Resource[Common.LanguageCode].ValidateDialogOrigin.Required
            //         inputOrigin.classList.add('border-red')
            //         noContentValueOrigin = false
            //     }
            // }

            var isCheckDupliacteOrgin = CommonFunction.validateDuplicateCbox();
            var noContentValueOrigin = CommonFunction.validateOriginFunding();
            var isRequiredCombobx = CommonFunction.validateRequiredCbox();

            if(isCheckDupliacteOrgin && noContentValueOrigin && isRequiredCombobx)
            {
                this.originFundingFixedAsset.forEach(item => {
                var originFundingSave = {
                    OriginFundingFixedAssetId : item.OriginFundingFixedAssetId,
                    FixedAssetId : this.fixedAssetIdDefault,
                    OriginFundingId : item.OriginFundingId,
                    ValueOriginFunding : CommonFunction.convertMoneyToNumber(item.ValueOriginFunding)
                }
                    this.listOriginFundingToSave.push(originFundingSave)
                });
            
                //Gọi Api lưu danh sách nguồn kinh phí
                axios.post(Resource.API.Host + Resource.API.updateOriginFunds , this.listOriginFundingToSave)
                .then(() => {
                    var totalCost = CommonFunction.calTotalCost(this.listOriginFundingToSave)
                    this.listOriginFundingToSave = [];
                    this.originFundingFixedAsset = [];
                    this.$emit("closeDialogOriginFunding")
                    //Cập nhật lại dữ liệu grid trên dialog chi tiết chứng từ
                    this.$emit("updateCostFixedAssetDialog" , this.fixedAssetIdDefault , totalCost)
                })
                .catch((err) => {
                    console.log(err);
                })
            }

            // console.log(this.listOriginFundingToSave)
        },


        /**
         * Đóng form chọn nguồn kinh phí
         * CreatedBy : DDHung(21/12/2021) 
        */
        closeDialogOriginFunding : function()
        {
            this.$emit("closeDialogOriginFunding")
        },

        /**
         * Method thêm một nguồn kinh phí
         * CreatedBy : DDHung (21/12/2021)
        */
        btnAddOriginFunding : function()
        {
            // var contentOriginFunding = document.querySelector('.content-origin-funding')
            // var originCost = contentOriginFunding.querySelector('.origin-cost')
            // var originFundingToAppend = contentOriginFunding.querySelector('.origin-cost-append')
            // var originFundingHtml = document.createElement('div')
            // originFundingHtml.className = 'label-origin-cost origin-cost-append'
            // originFundingHtml.innerHTML =  originFundingToAppend.innerHTML
            // originCost.appendChild(originFundingHtml)
            
            // this.btnAddOriginFunding = false;

            var newOrginFunding = {
                idCheckOriginFunding : this.originFundingFixedAsset.length + 1,
                OriginFundingId : null,
                ValueOriginFunding : null
            }
            this.originFundingFixedAsset.push(newOrginFunding)
        },

        /**
         * Method xử lý khi click vào item của combobox
         * CreatedBy : DDHung (16/11/2021)
        */
        itemComboboxClick : function(id , name , event)
        {
            var data = {
                id : id,
                name : name,
                event : event,
            }

            // this.isCheckRequiredCombobox = true;
  
            CommonFunction.selectedItemCombobox(data)
            var cbOriginFunding = event.target.parentElement.parentElement
            var toolTip = cbOriginFunding.parentElement.querySelector(".error-duplicate-origin");
            var checkDuplicate = this.originFundingFixedAsset.find(item => item.OriginFundingId == data.id);
            if(checkDuplicate)
            {
                //border red combobox mỗi khi chọn nguồn đã được chọn trước đó
                cbOriginFunding.classList.add('border-red')
                toolTip.innerText = Resource[Common.LanguageCode].ValidateDialogOrigin.Choosed
            } else{
                cbOriginFunding.classList.remove('border-red')
                toolTip.innerText = "";
            }
            // cbOriginFunding.setAttribute('originFundingId',data.id)
            var idCheckOriginFunding = cbOriginFunding.getAttribute('idCheckOriginFunding')
            var originFunding = this.originFundingFixedAsset.find(item => item.idCheckOriginFunding == idCheckOriginFunding)
            originFunding.OriginFundingId = id;
            var originFundingIndex = this.originFundingFixedAsset.findIndex(item => item.idCheckOriginFunding == idCheckOriginFunding)
            this.originFundingFixedAsset[originFundingIndex] = originFunding;
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
         * Method format tiền khi nhập 
         * CreatedBy : DDHung (21/12/2021) 
        */
        formatCostToMoney : function(e)
        {
            var value = CommonFunction.convertMoneyToNumber(e.target.value)
            e.target.value = CommonFunction.formatMoney(parseInt(value))

            var inputCostValue = e.target
            var idCheckOriginFunding = inputCostValue.getAttribute('idCheckOriginFunding')

            var originFunding = this.originFundingFixedAsset.find(item => item.idCheckOriginFunding == idCheckOriginFunding)
            originFunding.ValueOriginFunding = inputCostValue.value;
            var originFundingIndex = this.originFundingFixedAsset.findIndex(item => item.idCheckOriginFunding == idCheckOriginFunding)
            this.originFundingFixedAsset[originFundingIndex] = originFunding;

            this.totalCostOriginFunding = CommonFunction.formatMoney(CommonFunction.calTotalCost(this.originFundingFixedAsset))

            //bỏ border red validate mỗi khi nhập giá trị
            var inputOrigin = e.target
            var errorMessage = inputOrigin.parentElement.querySelector('.error-required-origin')
            if(e.target.value)
            {
                errorMessage.innerText = ""
                inputOrigin.classList.remove('border-red')
            }else{
                errorMessage.innerText = Resource[Common.LanguageCode].ValidateDialogOrigin.Required
                inputOrigin.classList.remove('border-red')
            }
        },

        /**
         * Method xóa 1 nguồn kinh kinh phí
         * CreatedBy : DDHung (21/12/2021) 
        */
        btnDeleteOriginFunding : function(e)
        {
            var btnDelete = e.target.parentElement
            var idCheckOriginFunding = btnDelete.getAttribute('idCheckOriginFunding')
            var originFundingIndex = this.originFundingFixedAsset.findIndex(item => item.idCheckOriginFunding == idCheckOriginFunding)
            this.originFundingFixedAsset.splice(originFundingIndex  , 1)
            this.totalCostOriginFunding = CommonFunction.formatMoney(CommonFunction.calTotalCost(this.originFundingFixedAsset))
        }
    },

    // watch : {
    //     valueCost : function(val){
    //         var value = CommonFunction.convertMoneyToNumber(val);
    //     }
    // }
}
</script>
<style lang="">
    
</style>