<template lang="">
    <div class="grid grid-used-fixed-assets">
        <div class="content-grid wrap-master-detail-grid">
            <UsedFixedAssetGridMaster 
                v-bind:vouchers = "vouchers"
                v-bind:currentPage = "currentPage"
                v-bind:isShowLoader = "isShowLoader"
                v-on:getFixedAssetClickRowMaster = "getFixedAssetClickRowMaster"
                v-on:showLoaderDetailGrid = "showLoaderDetailGrid"
                v-on:passVoucherEdit = "passVoucherEdit"
                v-on:showDialogVoucher = "showDialogVoucher"
                v-on:passIsAddOrEdit = "passIsAddOrEdit"
                v-on:btnDeleteRecord = "btnDeleteRecord"
                v-on:insertVoucherIdToListId = "insertVoucherIdToListId"
                v-bind:firstVoucherData = "firstVoucherData"
                v-bind:isAddOrEdit = "isAddOrEdit"
            />
            <UsedFixedAssetGridDetail 
                v-on:btnPaging = "btnPaging"
                v-bind:totalPage = "totalPage"
                v-bind:totalRecord = "totalRecord"
                v-bind:totalCost = "totalCost"
                v-bind:currentPage = "currentPage"
                v-bind:listFixedAssetClickedRowMaster = "listFixedAssetClickedRowMaster"
                v-bind:isShowLoaderDetailGrid = "isShowLoaderDetailGrid"
            />
        </div>
    </div>
</template>
<script>

/* Component  */
import UsedFixedAssetGridDetail from './UsedFixedAssetGridDetail.vue'
import UsedFixedAssetGridMaster from './UsedFixedAssetGridMaster.vue'

/* File Javascript */

export default {

    data() {
        return {
            listFixedAssetClickedRowMaster : [],
            isShowLoaderDetailGrid : false,
        }
    },

    components : {
        UsedFixedAssetGridDetail,
        UsedFixedAssetGridMaster,
    },
    props : {
        vouchers : {
            type : Array
        },
        isShowLoader : {
            type : Boolean
        },
        isAddOrEdit : {
            type : Number
        },
        totalPage : {
            type : Number
        },
        totalRecord : {
            type : Number
        },
        totalCost : {
            type : Number
        },
        currentPage : {
            type : Number
        },
        firstVoucherData : {
            type : Object
        }
    },
    
    methods: {

        /**
         * Thêm id chứng từ vào danh sách id(listVoucherIdToDelete) để phục vụ cho việc xóa dữ liệu
         * CreatedBy : DDHung (22/12/2021)
        */
        insertVoucherIdToListId : function(listIdSelectedRecord)
        {
            this.$emit("insertVoucherIdToListId" , listIdSelectedRecord)
        },


        /**
         * Method thực hiện việc xóa bản ghi của chứng từ
         * CreatedBy : DDHung(20/12/2021)
        */
        btnDeleteRecord : function(voucherId)
        {
            this.$emit("btnDeleteRecord" , voucherId)
        },

        /**
         * Method để check xem đang thêm hay đang sửa
         * CreatedBy : DDHung (19/12/2021) 
        */
        passIsAddOrEdit : function(val)
        {
            this.$emit("passIsAddOrEdit" , val)
        },

        /**
         * method : lấy thông tin chứng từ cần sửa để load lên dialog
         * CreatedBy : DDHung (19/11/2021)
        */
        passVoucherEdit : function(obj){
            this.$emit('passVoucherEdit',  obj)
        },

        /**
         * Method thực hiện show dialog chi tiết chứng từ
         * createdBy : DDHung (19/12/2021)
        */
        showDialogVoucher(){
            this.$emit("showDialogVoucher")
        },

        /**
         * Sự kiện kích hoạt khi chuyển sang trang khác
         * createdBy : DDHung (19/11/2021)
         * 
        */
        btnPaging : function(currentPage)
        {
            this.$emit("btnPaging" , currentPage);
        },

        /**
         * Lấy danh sách tài sản dựa vào id ghi tăng
         * CreatedBy : DDHung(15/12/2021)
        */
        getFixedAssetClickRowMaster : function(fixeAssets)
        {
           this.listFixedAssetClickedRowMaster = fixeAssets
        },

        /**
         * show loader tại grid details mỗi khi chọn 1 dòng trên grid master
         * CreatedBy : DDHung (16/12/2021) 
        */
        showLoaderDetailGrid : function(isCheckLoader)
        {
            this.isShowLoaderDetailGrid = isCheckLoader
        },
    },   
}
</script>
<style lang="">
    
</style>