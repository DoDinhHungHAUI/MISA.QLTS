<template lang="">
    <div class = "wrap-content">
        <UsedFixedAssetToolbar 
            v-on:showDialogVoucher = "showDialogVoucher"
            v-on:setStatusActionVoucher = "setStatusActionVoucher"
        />
        <UsedFixedAssetFilterbar
            v-on:searchByName = "searchByName"
            v-on:btnIconDeleteFilterBar = "btnIconDeleteFilterBar"
        />
        <UsedFixedAssetGrid 
            v-bind:vouchers = "vouchers"
            v-on:btnPaging = "btnPaging"
            v-bind:totalPage = "totalPage"
            v-bind:totalRecord = "totalRecord"
            v-bind:totalCost = "totalCost"
            v-bind:currentPage = "currentPage"
            v-bind:isShowLoader = "isShowLoader"
            v-on:passVoucherEdit = "passVoucherEdit"
            v-on:showDialogVoucher = "showDialogVoucher"
            v-on:passIsAddOrEdit = "passIsAddOrEdit"
            v-on:btnDeleteRecord = "btnDeleteRecord"
            v-on:insertVoucherIdToListId = "insertVoucherIdToListId"
            v-bind:firstVoucherData = "firstVoucherData"
            v-bind:isAddOrEdit = "isAddOrEdit"
        />

        <UsedFixedAssetDetail 
            v-if = "showDialog"
            v-show = "VShowDialogVoucher"
            v-on:hidedDialogVoucher = "hidedDialogVoucher"
            v-on:btnChooseFixedAsset = "btnChooseFixedAsset"
            v-on:updateVoucher = "updateVoucher"
            v-bind:isAddOrEdit = "isAddOrEdit"
            v-bind:listFixedAssetChoosed = "listFixedAssetChoosed"
            v-on:deleteRecordGridPopup = "deleteRecordGridPopup"
            v-on:showToastSuccess = "showToastSuccess"
            v-on:showDialogNotValid = "showDialogNotValid"
            v-bind:voucherEdit = "voucherEdit"
            v-on:fixedAssetChoosedClickEdit = "fixedAssetChoosedClickEdit"
            v-on:transportVoucherDTO = "transportVoucherDTO"
            v-bind:voucherDTO = "voucherDTO"
            v-on:transmitVoucher = "transmitVoucher"
            v-on:showDialogOriginFunding = "showDialogOriginFunding"
            v-on:transmitOriginFuns = "transmitOriginFuns"
            v-on:btnSaveInfoConfirm = "btnSaveInfoConfirm"
            v-on:confirmSaveInfoVoucher = "confirmSaveInfoVoucher"
        /><!-- v-on:confirmSaveInfoVoucher = "confirmSaveInfoVoucher" -->

        <FixedAssetChooseDetail
            v-if = "showDialogChooseFixedAsset"
            v-on:btnCloseChooseFixedAsset = "btnCloseChooseFixedAsset"
            v-bind:dialogFixedAssetToChoose = "dialogFixedAssetToChoose"
            v-on:btnPagingFixedAssetDialog = "btnPagingFixedAssetDialog"
            v-on:searchFixedAssetDialog = "searchFixedAssetDialog"
            v-bind:isAddOrEdit = "isAddOrEdit"
            v-on:btnSaveFixedAssetChoosed = "btnSaveFixedAssetChoosed"
        />

        <MToastError v-show = "isShowToastError" v-bind:messageError = "messageError" />
        <MToastSuccess v-show = "isShowToastSuccess" v-bind:messageSuccess = "messageSuccess" />

        <MDialogValidate 
            v-show = "isShowDialogValidate" 
            v-bind:messageNotValid = "messageNotValid" 
            v-on:btnHideDialogNotValid = "btnHideDialogNotValid"
        />

        <MDialogConfirm 
            v-show = "isShowDialogConfirm" 
            v-bind:messageConfirm = "messageConfirm"
            v-bind:isSaveConfirm = "isSaveConfirm"
            v-bind:isCancelSaveConfirm = "isCancelSaveConfirm"
            v-bind:isDeleteConfirm = "isDeleteConfirm"
            v-on:btnSaveInfoConfirm = "btnSaveInfoConfirm"
            v-on:btnConfirmDelete = "btnConfirmDelete"
            v-on:btnHideDialogConfirm = "btnHideDialogConfirm"
            v-on:btnNotHideDialogConfirm = "btnNotHideDialogConfirm"
        />
        
        <OriginFundingDetail 
            v-if = "showDialogChooseOriginFunding" 
            v-on:closeDialogOriginFunding = "closeDialogOriginFunding"
            v-bind:listOriginFundingByFixedAssetId = "listOriginFundingByFixedAssetId"
            v-bind:costDefault = "costDefault"
            v-bind:fixedAssetIdDefault = "fixedAssetIdDefault"
            v-on:updateCostFixedAssetDialog = "updateCostFixedAssetDialog"
        />

    </div>
</template>
<script>

/* Import library */
import axios from 'axios'

/* Component  */
import FixedAssetChooseDetail from './FixedAssetChooseDetail.vue'
import UsedFixedAssetToolbar from './UsedFixedAssetToolbar.vue'
import UsedFixedAssetFilterbar from './UsedFixedAssetFilterbar.vue'
import UsedFixedAssetGrid from './UsedFixedAssetGrid.vue'
import UsedFixedAssetDetail from './UsedFixedAssetDetail.vue'
import MToastSuccess from '../../components/base/MToastSuccess.vue'
import MDialogValidate from '../../components/base/MDialogValidate.vue'
import MDialogConfirm from '../../components/base/MDialogConfirm.vue'
import MToastError from '../../components/base/MToastError.vue'
import OriginFundingDetail from './OriginFundingDetail.vue'
/* File Javascript */
import {Resource} from '../../assets/js/common/resource'
import {Enum} from '../../assets/js/common/enum'
import {ToastJS} from '../../assets/js/common/messagetoast'
import {Common} from '../../assets/js/common/common'

export default {

    data() {
        return {
            vouchers : [],
            isShowLoader : false,
            totalPage : 1,
            totalRecord : 0,
            currentPage : 1,
            totalCost : 0,
            txtSearch : '',
            showDialog : false,
            VShowDialogVoucher : true,
            isShowToastSuccess : false,
            isShowDialogValidate : false,
            showDialogChooseFixedAsset : false,
            showDialogChooseOriginFunding : false,
            messageSuccess : null,
            messageConfirm : null,
            isShowToastError : false,
            isAddOrEdit : Enum.FormModel.View,
            isShowDialogConfirm : false,
            isSaveConfirm : false,
            isDeleteConfirm  : false, 
            isCancelSaveConfirm : false,
            listFixedAssetChoosed : [],
            messageNotValid :"",
            messageError : null,
            costDefault : null,
            fixedAssetIdDefault : null,
            voucherEdit : {},
            listOriginFundingByFixedAssetId : [],
            listVoucherIdToDelete : [],
            voucherIdToDelete : null,
            firstVoucherData : {},
            dialogFixedAssetToChoose : {
                fixedAssetToChoose : [],
                totalRecord : 0,
                totalPage : 1,
                txtSearch : '',
                currentPage : 1,
                isShowLoader : false
            },
            voucherDTO : {
                voucherViewModel : null,
                fixedAssetViewModels : []
            }
        }
    },
    
    components : {
        UsedFixedAssetToolbar,
        UsedFixedAssetFilterbar,
        UsedFixedAssetGrid,
        UsedFixedAssetDetail,
        FixedAssetChooseDetail,
        MToastSuccess,
        MDialogValidate,
        MDialogConfirm,
        MToastError,
        OriginFundingDetail
    },

    created() {
        this.getData();
    },

    methods: {

        /**
         * Thêm id chứng từ vào danh sách id(listVoucherIdToDelete) để phục vụ cho việc xóa dữ liệu
         * CreatedBy : DDHung (22/12/2021)
        */
        insertVoucherIdToListId : function(listIdSelectedRecord)
        {
            if(listIdSelectedRecord)
            {
                this.listVoucherIdToDelete = listIdSelectedRecord;
            }
        },

        /**
         * Show popup xác nhận việc xóa dữ liệu
         * CreatedBy : DDHung (22/12/2021) 
        */
        btnIconDeleteFilterBar : function()
        {
            try {
                if(this.listVoucherIdToDelete.length == 0)
                {
                    this.showDialogNotValid(ToastJS.deleteMessageWarning)
                }
                else if(this.listVoucherIdToDelete.length == 1){
                   this.btnDeleteRecord(this.listVoucherIdToDelete[0])
                }
                else{
                    // this.isShowDialogConfirm = true;
                    // this.isDeleteConfirm = true;
                    // var numberRecordDelete = this.listVoucherIdToDelete.length;
                    // if(numberRecordDelete < 10)
                    // {
                    //     numberRecordDelete = "0" + numberRecordDelete;
                    // }
                    // this.messageConfirm = Resource[Common.LanguageCode].MessageDialogConfirm.DeleteMultipleRecord(numberRecordDelete);
                    // this.isDeleteConfirm = true;
                    this.showDialogConfirmDeleteMulti();
                }
            } catch (error) {
                console.log(error)
            }
        },

        /**
         * Show Dialog xác nhận việc xóa nhiều
         * CreatedBy :DDHung (22/12/2021) 
        */
        showDialogConfirmDeleteMulti : function()
        {
            this.isShowDialogConfirm = true;
            this.isDeleteConfirm = true;
            var numberRecordDelete = this.listVoucherIdToDelete.length;
            if(numberRecordDelete < 10)
            {
                numberRecordDelete = "0" + numberRecordDelete;
            }
            this.messageConfirm = Resource[Common.LanguageCode].MessageDialogConfirm.DeleteMultipleRecord(numberRecordDelete , Enum.GridName.Voucher);
            this.isDeleteConfirm = true;
        },

        /**
         * Method lấy danh sách nguồn kinh phí dữ vào id tài sản ( khi click vào nút sửa)
         * CreatedBy : DDHung (21/12/2021) 
        */
        transmitOriginFuns : function(res)
        {
            this.listOriginFundingByFixedAssetId = res.data
            console.log(this.listOriginFundingByFixedAssetId)
        },

        /**
         * Đóng form chọn nguồn kinh phí
         * CreatedBy : DDHung(21/12/2021) 
        */
        closeDialogOriginFunding : function()
        {
            this.showDialogChooseOriginFunding = false
            this.VShowDialogVoucher = true
        },

        /**
         * Method dùng để show dilog chọn nguồn kinh phí
         * CreatedBy : DDHung (21/12/2021)
        */
        showDialogOriginFunding : function(fixedAssetId , cost)
        {
            this.fixedAssetIdDefault = fixedAssetId
            this.costDefault = cost
            this.showDialogChooseOriginFunding = true
            this.VShowDialogVoucher = false
        },
        
        /**
         * Method thực hiện việc xóa bản ghi của chứng từ
         * CreatedBy : DDHung(20/12/2021)
        */
        btnDeleteRecord : function(voucherId)
        {   
            // var self = this;
            // self.voucherDTO.voucherViewModel = voucherDelete;
            //Gọi API lấy danh sách tài sản dựa vào id chứng từ
            // axios.get(Resource.API.Host + Resource.API.FixedAssets + Resource.API.getFixedAssetByVoucherId(this.voucherDTO.voucherViewModel.VoucherId))
            // .then(res => {
            //     self.voucherDTO.fixedAssetViewModels = [...res.data]
            // })
            // .catch(error => {
            //     console.log(error)
            // })
            
            this.voucherIdToDelete = voucherId;
            var voucherDelete = this.vouchers.find(item => item.VoucherId == voucherId);
            this.isShowDialogConfirm = true;
            this.isDeleteConfirm = true;
            this.messageConfirm = Resource[Common.LanguageCode].MessageDialogConfirm.DeleteConfirm( Enum.GridName.Voucher, voucherDelete.VouchersCode);
            // this.isDeleteConfirm = true;
        },
        
        /**
         * Method lấy liệu đã sửa trên form chi tiết chứng từ phục vụ cho việc sửa dữ liệu
         * CreatedBy : DDHung (19/12/2021)
        */
        transmitVoucher : function(voucher , listFixedAssetChoosed)
        {
            this.voucherDTO.voucherViewModel = voucher
            this.voucherDTO.fixedAssetViewModels = listFixedAssetChoosed
        },

        /**
         * Method gán dữ liệu cho voucherDTO phục vụ cho việc thêm chứng từ
         * CreatedBy : DDHung (19/12/2021) 
        */
        transportVoucherDTO : function(dataToAddVoucher , listFixedAssetChoosed)
        {
            this.voucherDTO.voucherViewModel = dataToAddVoucher
            this.voucherDTO.fixedAssetViewModels = listFixedAssetChoosed
        },

        /**
         * Method xác nhận việc hủy đóng popup thêm/sửa tài sản
         * CreatedBy : DDHung (18/11/2021)
        */
        btnNotHideDialogConfirm : function()
        {
            try {
                this.isShowDialogConfirm = false;
                //Ẩn bỏ các button của dialog confirm
                this.isSaveConfirm = false;
                this.isCancelSaveConfirm = false;
                this.isDeleteConfirm = false;
            } catch (error) {
                console.log(error);
            }
        },

        /**
         * Method dùng để đóng popup thêm/sửa tài sản
         * CreatedBy : DDHung (18/11/2021)
        */
        btnHideDialogConfirm : function(){
            try {
                this.isShowDialogConfirm = false;
                this.hidedDialogVoucher();
                //Ẩn bỏ các button của dialog confirm
                this.isSaveConfirm = false;
                this.isCancelSaveConfirm = false;
                this.isDeleteConfirm = false;
            } catch (error) {
                console.log(error);
            }
        },

        /**
         * Method dùng để xóa tài sản khi người dùng xác nhận xóa
         * CreatedBy : DDHung (19/11/2021)
        */
        btnConfirmDelete : function()
        {
            var me = this;
            try{
                
                //Gọi API Dùng để xóa chứng từ
                if(this.listVoucherIdToDelete.length == 1)
                {
                    axios.delete(Resource.API.Host + Resource.API.Vouchers + Resource.API.deleteVoucher(me.voucherIdToDelete))
                    .then(() => {
                        me.isShowDialogConfirm = false;
                        //Ẩn bỏ các button của dialog confirm
                        me.isSaveConfirm = false;
                        me.isCancelSaveConfirm = false;
                        me.isDeleteConfirm = false;
                        me.showToastSuccess(ToastJS.deleteMessageSuccess);
                        this.listVoucherIdToDelete = [];
                        me.getData();
                    })
                    .catch(() => {
                        me.isShowDialogConfirm = false;
                        //Ẩn bỏ các button của dialog confirm
                        me.isSaveConfirm = false;
                        me.isCancelSaveConfirm = false;
                        me.isDeleteConfirm = false;
                    })
                }else{
                    var listId = '';
                    this.listVoucherIdToDelete.forEach(item => {
                        listId += item + ',';
                    });
                    listId = listId.slice(0,listId.length - 1);
                    console.log(listId)
                    axios.delete(Resource.API.Host + Resource.API.Vouchers + `/deleteMulti?listId=${listId}`)
                    .then(() => {
                        me.isShowDialogConfirm = false;
                        //Ẩn bỏ các button của dialog confirm
                        me.isSaveConfirm = false;
                        me.isCancelSaveConfirm = false;
                        me.isDeleteConfirm = false;
                        me.showToastSuccess(ToastJS.deleteMessageSuccess);
                        this.listFixedAssetIdToDelete = [];
                        me.getData();
                    })
                    .catch(() => {
                        me.isShowDialogConfirm = false;
                        //Ẩn bỏ các button của dialog confirm
                        me.isSaveConfirm = false;
                        me.isCancelSaveConfirm = false;
                        me.isDeleteConfirm = false;
                    })
                }
            }catch(error)
            {
                console.log(error)
            }

            // try {
            //     var me = this;
            //     //gọi API xóa 1 bản ghi của tài sản
            //     if(this.listFixedAssetIdToDelete.length == 1)
            //     {
            //         axios.delete(Resource.API.Host + Resource.API.FixedAssets + '/' + `${this.listFixedAssetIdToDelete[0]}`)
            //         .then(res => {
            //             console.log(res);
            //             me.isShowDialogConfirm = false;
            //             //Ẩn bỏ các button của dialog confirm
            //             me.isSaveConfirm = false;
            //             me.isCancelSaveConfirm = false;
            //             me.isDeleteConfirm = false;
            //             me.showToastSuccess(ToastJS.deleteMessageSuccess);
            //             this.listFixedAssetIdToDelete = [];
            //             me.getData();
            //         })
            //         .catch(() => {
            //             me.isShowDialogConfirm = false;
            //             //Ẩn bỏ các button của dialog confirm
            //             me.isSaveConfirm = false;
            //             me.isCancelSaveConfirm = false;
            //             me.isDeleteConfirm = false;
            //         })
            //     }
            //     else{
            //         var listId = '';
            //         this.listFixedAssetIdToDelete.forEach(item => {
            //             listId += item + ',';
            //         });
            //         listId = listId.slice(0,listId.length - 1);
            //         axios.delete(Resource.API.Host + Resource.API.FixedAssets + `/deleteMulti?listId=${listId}`)
            //         .then(res => {
            //             console.log(res);
            //             me.isShowDialogConfirm = false;
            //             //Ẩn bỏ các button của dialog confirm
            //             me.isSaveConfirm = false;
            //             me.isCancelSaveConfirm = false;
            //             me.isDeleteConfirm = false;
            //             me.showToastSuccess(ToastJS.deleteMessageSuccess);
            //             this.listFixedAssetIdToDelete = [];
            //             me.getData();
            //         })
            //         .catch(() => {
            //             me.isShowDialogConfirm = false;
            //             //Ẩn bỏ các button của dialog confirm
            //             me.isSaveConfirm = false;
            //             me.isCancelSaveConfirm = false;
            //             me.isDeleteConfirm = false;
            //         })
            //     }
            // } catch (error) {
            //     console.log(error)
            // }
        },


        /**
         * Method dùng để lưu thông tin khi sửa 1 bản ghi tài sản
         * CreatedBy : DDHung (18/11/2021)
        */
        btnSaveInfoConfirm : function()
        {
            try {
                var me = this;
                axios.put(Resource.API.Host + Resource.API.Vouchers + Resource.API.UpdateVoucher + '/' + `${this.voucherEdit.VoucherId}`,this.voucherDTO)
                .then(() => {
                    me.hidedDialogVoucher();
                    me.isShowDialogConfirm = false;
                    //Ẩn bỏ các button của dialog confirm
                    me.isSaveConfirm = false;
                    me.isCancelSaveConfirm = false;
                    me.isDeleteConfirm = false;
                    me.updateVoucher();
                    me.showToastSuccess(ToastJS.successMessage);
                })
                .catch(error => {
                    if(error.response.status == 400){
                        me.showDialogNotValid(error.response.data.Data.userMsg)
                    }
                    else{
                        me.showToastError(ToastJS.errorMessage);
                        console.log("There was an error!", error);
                    }
                    me.isShowDialogConfirm = false;
                    //Ẩn bỏ các button của dialog confirm
                    me.isSaveConfirm = false;
                    me.isCancelSaveConfirm = false;
                    me.isDeleteConfirm = false;
                })
            } catch (error) {
                console.log(error);
            }
        },

        /**
         * Method dùng để xác nhận việc có sửa dữ liệu hay là không.
         * CreatedBy : DDHung (18/11/2021)
        */
        confirmSaveInfoVoucher : function(message , isSaveConfirm)
        {
            this.isShowDialogConfirm = true
            this.messageConfirm = message
            this.isSaveConfirm = isSaveConfirm
        },

        /**
         * Method lấy ra danh sách tài sản ghi click sửa chứng từ
         * CreatedBy : DDHung (19/12/2021) 
        */
        fixedAssetChoosedClickEdit : function(data)
        {
            this.listFixedAssetChoosed = data
        },

        /**
         * Method để check xem đang thêm hay đang sửa
         * CreatedBy : DDHung (18/11/2021) 
        */
        passIsAddOrEdit : function(value)
        {
            this.isAddOrEdit = value
        },

        /**
         * method : lấy thông tin chứng từ cần sửa để load lên dialog
         * CreatedBy : DDHung (19/11/2021)
        */
        passVoucherEdit : function(obj){
            this.voucherEdit = Object.assign({},obj);
        },

        /**
         * Method thực hiện show dialog chi tiết chứng từ
         * createdBy : DDHung (19/12/2021)
        */
        showDialogVoucher(){
            this.showDialog = true;
        },

        /**
         * Method show dialog valid không hợp lệ
         * CreatedBy : DDHung (19/12/2021)
        */
        showDialogNotValid : function(message)
        {
            this.messageNotValid = '<span>' +  message  + '</span>';
            this.isShowDialogValidate = true;
        },

        /**
         * Xử lý khi nhấn button Hủy trên dialog thông báo not valid các trường bắt buộc nhập
         * CreatedBy : DDHung (22/11/2021)
         */
        btnHideDialogNotValid : function()
        {
            this.messageNotValid = "";
            this.isShowDialogValidate = false;
        },

        /**
         * Method cập nhật lại dữ liệu khi bấm thêm hoặc sửa chứng từ
         * CreatedBy : DDHung(17/12/2021)
        */
        updateVoucher : function(){
            this.getData();
        },

        /**
         * Method dùng để ẩn dialog chi tiết chứng từ
         * CreatedBy : DDHung (17/12/2021) 
        */
        hidedDialogVoucher : function()
        {
            this.showDialog = false
            this.listFixedAssetChoosed = []
        },

        /**
         * Method cập nhật lại nguyên giá mỗi khi thay đổi nguồn kinh phí
         * CreatedBy : DDHung (22/12/2021)
        */
        updateCostFixedAssetDialog : function(fixedAssetIdDefault ,totalCost)
        {   
            //Cập nhật lại nguyên giá
            var fixedAsset = this.listFixedAssetChoosed.find(item => item.FixedAssetId == fixedAssetIdDefault)
            fixedAsset.Cost = totalCost
            var fixedAssetIndex = this.listFixedAssetChoosed.findIndex(item => item.FixedAssetId == fixedAssetIdDefault)
            this.listFixedAssetChoosed[fixedAssetIndex] = fixedAsset;
            this.listFixedAssetChoosed = [...this.listFixedAssetChoosed]
        },

        /**
         * Method dùng để tìm kiếm ghi tăng theo chứng từ và nội dung
         * CreatedBy : DDHung (15/12/2021)
        */
        searchByName :function(txtSearch){
            try {
                var self = this
                self.txtSearch = txtSearch
                 this.isShowLoader = true
                axios.get(Resource.API.Host + Resource.API.Vouchers + Resource.API.searchVouchers(self.txtSearch , Resource.SearchPaging.PageSize ,  Resource.SearchPaging.PageIndex))
                .then(res => {
                    self.currentPage = 1;
                    self.vouchers = res.data.Data
                    self.totalPage = res.data.TotalPage
                    self.totalRecord = res.data.TotalRecord
                    this.isShowLoader = false
                })
                .catch(error => {
                    console.log(error)
                    this.isShowLoader = false
                })
            } catch (error) {
                console.log(error)
            }
        },

        /**
         * Method lấy danh sách tài sản từ API để load lên table
         * CreatedBy : DDHung (16/11/2021)
        */
        getData(){
            var self = this;
            this.isShowLoader = true;
            axios.get(Resource.API.Host + Resource.API.Vouchers + Resource.API.searchVouchers(Resource.SearchPaging.TxtSearch , Resource.SearchPaging.PageSize ,  Resource.SearchPaging.PageIndex))
            .then(res => {
                this.isShowLoader = false;
                // //Show Loader
                self.vouchers = res.data.Data;
                self.totalPage = res.data.TotalPage;
                self.totalRecord = res.data.TotalRecord;
                // self.totalQuantity = res.data.TotalQuantity;
                self.totalCost = res.data.TotalCost;
                // self.totalFixedAssetCummulative = res.data.TotalFixedAssetCummulative;
                // self.totalFixedAssetRest = res.data.TotalFixedAssetRest;
                
                this.firstVoucherData = res.data.Data[0]
                console.log(this.firstVoucherData)

            })
            .catch(error => {
                this.isShowLoader = false;
                console.log(error)
            })
        },

        /**
         * Sự kiện kích hoạt khi chuyển sang trang khác
         * createdBy : DDHung (19/11/2021)
        */
        btnPaging : function(currentPage)
        {
            var self = this;
            self.currentPage = currentPage;
            this.isShowLoader = true;
            axios.get(Resource.API.Host + Resource.API.Vouchers + Resource.API.searchVouchers(self.txtSearch , Resource.SearchPaging.PageSize ,  currentPage))
            .then(res => {
                self.vouchers = res.data.Data
                self.totalPage = res.data.TotalPage
                self.totalRecord = res.data.TotalRecord
                this.isShowLoader = false
            })
            .catch((error) => {
                console.log(error)
                this.isShowLoader = false
            })
        },

        /**
         * Method thực hiện việc phân trang cho grid tài sản trên dialog chọn tài sản ghi tăng
         * CreatedBy : DDHung (17/12/2021) 
        */
        btnPagingFixedAssetDialog : function(currentPage)
        {
            var self = this;
            self.dialogFixedAssetToChoose.currentPage = currentPage;
            self.dialogFixedAssetToChoose.isShowLoader = true;
            axios.get(Resource.API.Host + Resource.API.FixedAssets + Resource.API.searchFixedAssetChoose(self.dialogFixedAssetToChoose.txtSearch , Resource.SearchPaging.PageSize ,  currentPage))
            .then(res => {
                self.dialogFixedAssetToChoose.isShowLoader = false
                self.dialogFixedAssetToChoose.fixedAssetToChoose = res.data.Data
                self.dialogFixedAssetToChoose.totalPage = res.data.TotalPage
                self.dialogFixedAssetToChoose.totalRecord = res.data.TotalRecord
            })
            .catch((error) => {
                self.dialogFixedAssetToChoose.isShowLoader = false
                console.log(error)
            })
        },

        /**
         * Method thực hiện việc show dialog chọn tài sản để ghi tăng 
         * CreatedBy : DDHung (17/11/2021)
        */
        btnChooseFixedAsset : function()
        {
            var self = this;
            axios.get(Resource.API.Host + Resource.API.FixedAssets + Resource.API.searchFixedAssetChoose(Resource.SearchPaging.TxtSearch , Resource.SearchPaging.PageSize ,  Resource.SearchPaging.PageIndex))
            .then(res => {
                // if(this.listFixedAssetChoosed.length == 0)
                // {
                //     self.dialogFixedAssetToChoose.fixedAssetToChoose = res.data.Data
                // }else{
                //     self.dialogFixedAssetToChoose.fixedAssetToChoose = res.data.Data
                //     res.data.Data.forEach((item , index) => {
                //         if(this.listFixedAssetChoosed.find(fixedAssetChoosed => fixedAssetChoosed.FixedAssetId == item.FixedAssetId))
                //         {
                //             self.dialogFixedAssetToChoose.fixedAssetToChoose.splice(index , 1)
                //         }
                //     });
                // }
                self.dialogFixedAssetToChoose.fixedAssetToChoose = res.data.Data
                self.dialogFixedAssetToChoose.totalPage = res.data.TotalPage
                self.dialogFixedAssetToChoose.totalRecord = res.data.TotalRecord
                self.VShowDialogVoucher = false
                self.showDialogChooseFixedAsset = true   
            })
            .catch((error) => {
                console.log(error)
            }) 
            console.log(self.dialogFixedAssetToChoose.fixedAssetToChoose)
        },

        /**
         * Method thực hiện việc đóng dialog chọn tài sản
         * CreatedBy : DDHung (17/12/2021) 
        */
        btnCloseChooseFixedAsset : function()
        {
            this.VShowDialogVoucher = true
            this.showDialogChooseFixedAsset = false   
        },

        /**
         * Method thực hiện việc tìm kiếm trên dialog chọn tài sản để ghi tăng
         * CreatedBy : DDHung (17/12/2021) 
        */
        searchFixedAssetDialog : function(txtSearch)
        {   
            var self = this
            self.dialogFixedAssetToChoose.txtSearch = txtSearch
            self.dialogFixedAssetToChoose.isShowLoader = true
            axios.get(Resource.API.Host + Resource.API.FixedAssets + Resource.API.searchFixedAssetChoose(txtSearch , Resource.SearchPaging.PageSize ,  Resource.SearchPaging.PageIndex))
            .then(res => {
                self.dialogFixedAssetToChoose.currentPage = 1;
                self.dialogFixedAssetToChoose.fixedAssetToChoose = res.data.Data
                self.dialogFixedAssetToChoose.totalPage = res.data.TotalPage
                self.dialogFixedAssetToChoose.totalRecord = res.data.TotalRecord
                self.dialogFixedAssetToChoose.isShowLoader = false
            })
            .catch(error => {
                console.log(error)
                self.dialogFixedAssetToChoose.isShowLoader = false
            })
        },

        /**
         * Method dùng để đánh dấu là thêm chứng từ
         * CreatedBy : DDHung (17/12/2021)
         */
        setStatusActionVoucher : function()
        {
            this.isAddOrEdit = Enum.FormModel.Add
        },

        /**
         * Method dùng để show toast thông báo Thêm/Sửa thành công 
         * CreatedBy : DDHung (16/11/2021)
         */
        showToastSuccess : function(message){
            try {
                this.messageSuccess = message;
                this.isShowToastSuccess = true;
                setTimeout(() => {
                    this.isShowToastSuccess = false
                } , 3000)
            } catch (error) {
                console.log(error);
            }
        },

        /**
         * Method show Toast error
         * CreatedBy : DDHung (17/11/2021)
        */
        showToastError : function(message){
            try {
                console.log(message);
                this.messageError = message;
                this.isShowToastError = true;
                setTimeout(() => {
                    this.isShowToastError = false
                } , 3000)
            } catch (error) {
                console.log(error);
            }
        },

        /**
         * Method xử lý click button lưu khi chọn xong tài sản để tăng  
         * CreatedBy : DDHung (17/12/2021)
        */
        btnSaveFixedAssetChoosed : function(listFixedAssetChoosed)
        {
            listFixedAssetChoosed.forEach(item => {
                if(!this.listFixedAssetChoosed.find(item2 => item2.FixedAssetId == item.FixedAssetId))
                {
                    this.listFixedAssetChoosed.push(item)
                }
            });
            
            this.showDialogChooseFixedAsset = false
            this.VShowDialogVoucher = true
        },

        /**
         * Method xóa record trên grid của form chi tiết chứng từ
         */
        deleteRecordGridPopup : function(fixedAssetId)
        {
            var indexToDelete = this.listFixedAssetChoosed.findIndex(item => item.FixedAssetId === fixedAssetId)
            this.listFixedAssetChoosed.splice(indexToDelete , 1)
            this.listFixedAssetChoosed = [...this.listFixedAssetChoosed]
        }
    },

}
</script>
<style lang="">
    
</style>