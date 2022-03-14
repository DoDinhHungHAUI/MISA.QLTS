<template lang="">
    <div class="filter-bar">
        <div class="filter-left">
            <div>
                <input type="text" class = "icon-search input-search" id = "input-search" placeholder = "Tìm kiếm theo Mã,Tên tài sản" style="width : 200px" v-model = "inputSearch" />
            </div>

            <MComboboxFilter 
                v-bind:dataCombobox = "departmentAssets" 
                v-bind:idItem = "'DepartmentAssetId'" 
                v-bind:nameItem = "'DepartmentAssetName'" 
                v-bind:mPlaceholder = "'Chọn mã bộ phận sử dụng'"
                v-on:itemComboboxClick = "itemComboboxClick"
            /><!--v-on:btnButtonCombobox = "btnButtonCombobox"-->

            <MComboboxFilter 
                v-bind:dataCombobox = "fixedAssetCategories" 
                v-bind:idItem = "'FixedAssetCategoryId'" 
                v-bind:nameItem = "'FixedAssetCategoryName'" 
                v-bind:mPlaceholder = "'Chọn mã loại tài sản'"
                v-on:itemComboboxClick = "itemComboboxClick"
            /><!--v-on:btnButtonCombobox = "btnButtonCombobox"-->
            
        </div>
        <div class="filter-right">
            <button class="m-btn m-btn-default" id="add-assets" tabindex="1" v-on:click = "addFixedAssetClick">
                <div class="m-btn-icon icon-add">
                    <i class="fas fa-plus"></i>
                </div>
                <div class="btn-text">Thêm tài sản</div>
            </button>
            <input style="display: none" type="file" ref="fileInput" @change="onFilePicked" />
            <div class = "wrap-btnfilter-tooltip">
                <button class="m-second-button m-btn-exportExcel" id="btnExportExcel" v-on:click="onPickFile">
                </button>
                <span class="filterbar-tooltiptext tooltiptext-excel showTooltip-filterbar">Nhập khẩu dữ liệu</span>
            </div>  
            <div class = "wrap-btnfilter-tooltip">
                <button class="m-second-button m-btn-delete" id="btnDelete" v-on:click = "btnDeleteRecord">
                </button>
                <span class="filterbar-tooltiptext delete-tooltiptext showTooltip-filterbar">Xóa</span>
            </div>
        </div>
    </div>
</template>
<script>
import axios from 'axios'

/* Import Component */
import MComboboxFilter from '../../components/base/MComboboxFilter.vue'

/* Import File Javascript */
import {Resource} from '../../assets/js/common/resource'
import { ToastJS } from '../../assets/js/common/messagetoast'

export default {
    data() {
        return {
            timeout: null,
            txtsearch : '',
        }
    },
    props : {
        departmentAssets : {
            type : Array
        },
        fixedAssetCategories : {
            type : Array
        },
    },
    components : {
        MComboboxFilter
    },
    

    methods: {

        /**
         * Sự kiện dùng để xóa các bản ghi được chọn 
         * CreatedBy : DDHung (19/11/2021)
        */
        btnDeleteRecord : function()
        {
            this.$emit("btnDeleteRecord");    
        },

        /**
         * Method xử lý khi click vào button thêm tài sản
         * createdBy : DDHung (16/11/2021)
        */
        addFixedAssetClick : function(){
            this.$emit('showDialogFixedAsset')
            this.$emit('setIsFixedAsset')
        },

        /**
         * Method xử lý khi click button của combobox
         * CreatedBy : DDHung (16/11/2021)
        */
        // btnButtonCombobox : function(event){
        //     this.$emit('btnButtonCombobox' , event)
        // },

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
                txtsearch : this.txtsearch
            }
            this.$emit('filterListFixedAsset',data);
        },

        /**
         * Sự kiện được sử dụng cho việc upload file excel phục vụ cho việc nhập khẩu dữ liệu
         * CreatedBy : DDHung (23/11/2021)
        */
        onPickFile : function()
        {
            this.$refs.fileInput.click();
        },

        /**
         * Xử kiện xử lý việc nhập khẩu dữ liệu từ file execl
         * CreatedBy : DDHung (23/11/2021)
        */
        onFilePicked : function(event)
        {
            try {
                const files = event.target.files;
                let formData = new FormData();
                formData.append('formFile',files[0]);
                axios.post(Resource.API.Host + Resource.API.FixedAssets + Resource.API.ImportDataFromExcel, formData,{ headers: {'Content-Type': 'multipart/form-data'}
                }).then(() => {
                    //show toast thông báo nhập khẩu thành công
                    this.$emit("showToastSuccess",ToastJS.successImportExcelMessage)
                    this.$emit("getData")
                    this.$refs.fileInput.value = '';
                }).catch(error => {
                    if(error.response.status == 400)
                    {
                        this.$emit("showDialogNotValid" , error.response.data.Data.userMsg)
                        this.$refs.fileInput.value = '';
                    }
                })
            } catch (error) {
                console.log(error);
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
        txtsearch : function()
        {
            this.$emit('searchByName' , this.txtsearch)
        }
    }
}
</script>
<style lang="">
    
</style>