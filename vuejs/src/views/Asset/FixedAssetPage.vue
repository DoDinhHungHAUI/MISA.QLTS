<template lang="">
    <div>
        <FixedAssetFilterbar 
            v-on:showDialogFixedAsset = "showDialogFixedAsset" 
            v-bind:departmentAssets = "departmentAssets"
            v-bind:fixedAssetCategories = "fixedAssetCategories"
            v-on:filterListFixedAsset = "filterListFixedAsset"
            v-on:setIsFixedAsset = "setIsFixedAsset"
            v-on:btnDeleteRecord = "btnDeleteRecord"
            v-on:searchByName = "searchByName"
            v-on:getData = "getData"
            v-on:showToastSuccess = "showToastSuccess"
            v-on:showDialogNotValid = "showDialogNotValid"
        /><!-- v-on:btnButtonCombobox = "btnButtonCombobox" -->
        <FixedAssetGrid 
            v-bind:fixedAssets = "fixedAssets"
            v-on:passFixedAssetEdit = "passFixedAssetEdit"
            v-on:passIsAddOrEdit = "passIsAddOrEdit"
            v-on:showDialogFixedAsset = "showDialogFixedAsset"
            v-on:insertFixedAssetIdToListId = "insertFixedAssetIdToListId"
            v-on:deleteFixedAssetIdToListId = "deleteFixedAssetIdToListId"
            v-on:btnPaging = "btnPaging"
            v-bind:totalPage = "totalPage"
            v-bind:totalRecord = "totalRecord"
            v-bind:totalCost = "totalCost"
            v-bind:totalQuantity = "totalQuantity"
            v-bind:totalFixedAssetCummulative = "totalFixedAssetCummulative"
            v-bind:totalFixedAssetRest = "totalFixedAssetRest"
            v-bind:isShowLoader = "isShowLoader"
            v-bind:currentPage = "currentPage"
            v-on:addFixedAssetContextMenu = "addFixedAssetContextMenu"
            v-on:deleteFixedAssetContextMenu = "deleteFixedAssetContextMenu"
            v-on:setIsFixedAsset = "setIsFixedAsset"
            v-on:btnDeleteRecord = "btnDeleteRecord"
        />
        <FixedAssetDetail 
            v-if="showDialog"
            v-bind:departmentAssets = "departmentAssets" 
            v-on:confirmHidedDialog = "confirmHidedDialog"
            v-on:itemComboboxClick = "itemComboboxClick"
            v-bind:fixedAssetCategories = "fixedAssetCategories"
            v-on:showDialogNotValid = "showDialogNotValid"
            v-on:showToastSuccess = "showToastSuccess"
            v-bind:isAddOrEdit = "isAddOrEdit"
            v-on:updateFixedAsset = "updateFixedAsset"
            v-bind:fixedAssetEdit = "fixedAssetEdit"
            v-on:confirmSaveInfoFixedAsset = "confirmSaveInfoFixedAsset"
            v-on:hidedDialogFixedAsset = "hidedDialogFixedAsset"
            v-on:transmitFixedAsset = "transmitFixedAsset"
            v-bind:ListFieldNotValidRequired = "ListFieldNotValidRequired"
            v-on:onFucusFirstInputNotValid = "onFucusFirstInputNotValid"
            v-on:btnSaveInfoConfirm = "btnSaveInfoConfirm"
        />
        <!-- v-on:onTransportListFieldNotValid = "onTransportListFieldNotValid" -->
        <!-- v-on:btnButtonCombobox = "btnButtonCombobox" -->

        <MToastWarning v-show = "isShowToastWarning" v-bind:messageWarning = "messageWarning" />
        <MToastError v-show = "isShowToastError" v-bind:messageError ="messageError" />
        <MToastSuccess v-show = "isShowToastSuccess" v-bind:messageSuccess = "messageSuccess" />
        <!-- <MDialogValidate 
            v-show = "isShowDialogValidate" 
            v-bind:ListFieldNotValidRequired = "ListFieldNotValidRequired" 
            v-on:btnHideDialogNotValid = "btnHideDialogNotValid"
            v-bind:messageNotValid = "messageNotValid"
        /> -->
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
    </div>
</template>
<script>
/* Import library */
import axios from 'axios'

/* Import Component */
import MToastWarning from '../../components/base/MToastWarning.vue'
import MToastError from '../../components/base/MToastError.vue'
import MToastSuccess from '../../components/base/MToastSuccess.vue'
import MDialogConfirm from '../../components/base/MDialogConfirm.vue'
import MDialogValidate from '../../components/base/MDialogValidate.vue'
import FixedAssetDetail from './FixedAssetDetail.vue'
import FixedAssetFilterbar from './FixedAssetFilterbar.vue'
import FixedAssetGrid from './FixedAssetGrid.vue'

/* File Javascript */
import {Resource} from '../../assets/js/common/resource'
import {CommonFunction} from '../../assets/js/common/commonfunction'
import {Enum} from '../../assets/js/common/enum'
import {ToastJS} from '../../assets/js/common/messagetoast'
import {Common} from '../../assets/js/common/common'

export default {
    data() {
        return {
            departmentAssets : [],
            fixedAssetCategories : [],
            fixedAssets : [],
            showDialog : false,
            isShowToastWarning : false,
            isShowToastError : false,
            isShowToastSuccess : false,
            isShowDialogConfirm : false,
            isAddOrEdit : Enum.FormModel.View,
            messageWarning : null,
            messageError : null,
            messageSuccess : null,
            messageConfirm : null,
            messageDelete : null,
            messageNotValid :"",
            fixedAssetIdForDeleteOrEdit : null,
            /**
             * Dữ liệu này dùng để load lên form chi tiết
             */
            fixedAssetEdit : Object.assign({}),
            /**
             * Dữ liệu này dùng để lưu xuống database khi muốn sửa tài sản
            */
            fixedAssetEdited :  Object.assign({}),
            totalPage : 1,
            totalRecord : 0,
            currentPage : 1,
            isCancelSaveConfirm : false,
            isSaveConfirm : false,
            isShowLoader : false,
            isShowDialogValidate : false,
            isDeleteConfirm  : false,
            /**
             * danh sách id phục vụ cho việc xóa dữ liệu
            */
            listFixedAssetIdToDelete : [],
            /**
             * danh sách các trường chưa valid bắt buộc nhập khi lưu dữ liệu
             */
            ListFieldNotValidRequired : [],
            txtSearch : '',
            cbKeyFilter : {
                DepartmentAssetId : null,
                FixedAssetCategoryId : null
            },
            totalCost : 0,
            totalQuantity : 0,
            /**
             * Hao mòn lũy kế
            */
            totalFixedAssetCummulative : 0,
            /**
             * Giá trị còn lại
            */
            totalFixedAssetRest : 0,
            /**
             * Ref của dialog 
            */
            dialogContent : null
        }
    },

    components : {
        FixedAssetDetail,
        FixedAssetFilterbar,
        FixedAssetGrid,
        MToastWarning,
        MToastError,
        MToastSuccess,
        MDialogConfirm,
        MDialogValidate
    },

    methods: {
        
        /**
         * Chức năng xóa một tài sản tại menucontext
         * CreatedBy : DDHung (24/11/2021)
        */
        deleteFixedAssetContextMenu : function(fixedAssetIdToDeleteContextMenu)
        {
            try {
                this.listFixedAssetIdToDelete = [];
                this.listFixedAssetIdToDelete.push(fixedAssetIdToDeleteContextMenu);
                console.log(this.listFixedAssetIdToDelete);
                this.isShowDialogConfirm = true;
                var fixedAssetDelete = this.fixedAssets.find(item => item.FixedAssetId == fixedAssetIdToDeleteContextMenu);
                this.messageConfirm = Resource[Common.LanguageCode].MessageDialogConfirm.DeleteConfirmOneRecord(fixedAssetDelete.FixedAssetCode , fixedAssetDelete.FixedAssetName);
                this.isDeleteConfirm = true;
            } catch (error) {
                console.log(error)
            }
        },

        /**
         * Chức năng thêm một tài sản mới tại menucontext
         * CreatedBy : DDHung (24/11/2021)
        */
        addFixedAssetContextMenu : function()
        {
            this.showDialogFixedAsset();
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
            axios.get(Resource.API.Host + Resource.API.FixedAssets + Resource.API.searchFixedAssets(self.txtSearch, this.cbKeyFilter.DepartmentAssetId , this.cbKeyFilter.FixedAssetCategoryId , Resource.SearchPaging.PageSize ,  currentPage))
            .then(res => {
                self.fixedAssets = res.data.Data;
                self.totalPage = res.data.TotalPage;
                self.totalRecord = res.data.TotalRecord;
                this.isShowLoader = false;
            })
            .catch((error) => {
                console.log(error)
                this.isShowLoader = false;
            })
        },


        /**
         * Method dùng để xóa tài sản khi người dùng xác nhận xóa
         * CreatedBy : DDHung (19/11/2021)
        */
        btnConfirmDelete : function()
        {
            try {
                var me = this;
                //gọi API xóa 1 bản ghi của tài sản
                if(this.listFixedAssetIdToDelete.length == 1)
                {
                    axios.delete(Resource.API.Host + Resource.API.FixedAssets + '/' + `${this.listFixedAssetIdToDelete[0]}`)
                    .then(res => {
                        console.log(res);
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
                else{
                    var listId = '';
                    this.listFixedAssetIdToDelete.forEach(item => {
                        listId += item + ',';
                    });
                    listId = listId.slice(0,listId.length - 1);
                    axios.delete(Resource.API.Host + Resource.API.FixedAssets + `/deleteMulti?listId=${listId}`)
                    .then(res => {
                        console.log(res);
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
            } catch (error) {
                console.log(error)
            }
        },

        /**
         * Sự kiện dùng để xóa các bản ghi được chọn 
         * CreatedBy : DDHung (19/11/2021)
        */
        btnDeleteRecord : function()
        {
            try {
                if(this.listFixedAssetIdToDelete.length == 0)
                {
                    this.showDialogNotValid(ToastJS.deleteMessageWarning)
                    // this.showToastWarning(ToastJS.deleteMessageWarning);
                }
                else if(this.listFixedAssetIdToDelete.length == 1){
                    var fixedAssetDelete = this.fixedAssets.find(item => item.FixedAssetId == this.listFixedAssetIdToDelete[0]);

                    if(fixedAssetDelete.StatusFixedAsset == Enum.IsUsedFixedAsset.UsedFixedAsset){
                        
                        axios.get(Resource.API.Host + Resource.API.Vouchers + '/' + fixedAssetDelete.VoucherId)
                        .then(res => {
                            var voucher = res.data
                            this.showDialogNotValid(Resource[Common.LanguageCode].MessageDialogConfirm.MessageFixedAssetUsed(fixedAssetDelete.FixedAssetCode , voucher.VouchersCode))
                        })
                        .catch(err => {
                            console.log(err)
                        })
                        // this.messageConfirm = Resource[Common.LanguageCode].MessageDialogConfirm.MessageFixedAssetUsed;
                    }else{
                        this.isShowDialogConfirm = true;
                        this.isDeleteConfirm = true;
                        this.messageConfirm = Resource[Common.LanguageCode].MessageDialogConfirm.DeleteConfirmOneRecord(fixedAssetDelete.FixedAssetCode , fixedAssetDelete.FixedAssetName);
                        // this.isDeleteConfirm = true;
                    }
                }
                else{
                    
                    var fixedAsset = null;
                    for (const item of this.listFixedAssetIdToDelete) {
                        if(this.fixedAssets.find(item2 => item2.FixedAssetId == item && item2.StatusFixedAsset == 2))
                        {
                            fixedAsset = this.fixedAssets.find(item3 => item3.FixedAssetId == item)
                            break;
                        }
                    }
                    
                    if(fixedAsset)
                    {
                        axios.get(Resource.API.Host + Resource.API.Vouchers + '/' + fixedAsset.VoucherId)
                        .then(res => {
                            var voucher = res.data
                            this.showDialogNotValid(Resource[Common.LanguageCode].MessageDialogConfirm.MessageFixedAssetUsed(fixedAsset.FixedAssetCode , voucher.VouchersCode))
                        })
                        .catch(err => {
                            console.log(err)
                        })
                    }else{
                        this.isShowDialogConfirm = true;
                        this.isDeleteConfirm = true;
                        var numberRecordDelete = this.listFixedAssetIdToDelete.length;
                        if(numberRecordDelete < 10)
                        {
                            numberRecordDelete = "0" + numberRecordDelete;
                        }
                        this.messageConfirm = Resource[Common.LanguageCode].MessageDialogConfirm.DeleteMultipleRecord(numberRecordDelete , Enum.GridName.FixedAsset);
                        this.isDeleteConfirm = true;
                    }
                }
            } catch (error) {
                console.log(error)
            }
        },

        /**
         * Thêm id tài sản vào danh sách id(listFixedAssetIdToDelete) để phục vụ cho việc xóa dữ liệu
         * CreatedBy : DDHung (19/11/2021)
        */
        insertFixedAssetIdToListId : function(listIdSelectedRecord)
        {
            if(listIdSelectedRecord)
            {
                this.listFixedAssetIdToDelete = listIdSelectedRecord;
            }
        },

        /**
         * remove  id tài sản ra khỏi danh sách id(listFixedAssetIdToDelete) để phục vụ cho việc xóa dữ liệu
         * CreatedBy : DDHung (19/11/2021)
        */
        deleteFixedAssetIdToListId : function(fixedAssetId)
        {
            try {
                this.listFixedAssetIdToDelete = CommonFunction.arrayRemove(this.listFixedAssetIdToDelete , fixedAssetId);
            } catch (error) {
                console.log(error)
            }
        },

        /**
         * Method thực hiện show dialog chi tiết nhân viên
         * createdBy : DDHung (16/11/2021)
        */
        showDialogFixedAsset(){
             
            this.showDialog = true;
        },

        /**
         * Method thực hiện close Popup form chi tiết tài sản
         * CreatedBy : DDHung (16/11/2021)
        */
        hidedDialogFixedAsset(){
            this.showDialog = false;
        },

        /**
         * Method lấy danh sách phòng ban từ API để load lên combobox
         * CreatedBy : DDHung (16/11/2021)
        */
        getDepartment(){
            var self = this;
            axios.get( Resource.API.Host + Resource.API.Departments)
            .then(res => {
                self.departmentAssets = res.data
            })
            .catch(error => {
                console.log(error)
            })
        },

        /**
         * Lấy danh sách loại tài sản để load lên combobox
         * CreatedBy : DDHung (16/11/2021)
        */
        getFixedAssetCategories(){
            var self = this;
            axios.get(Resource.API.Host + Resource.API.FixedAssetCategories)
            .then(res => {
                self.fixedAssetCategories = res.data
                console.log(self.fixedAssetCategories);
            })
            .catch(error => {
                console.log(error)
            })
        },

        /**
         * Method lấy danh sách tài sản từ API để load lên table
         * CreatedBy : DDHung (16/11/2021)
        */
        getData(){
            var self = this;
            this.isShowLoader = true;
            axios.get(Resource.API.Host + Resource.API.FixedAssets + Resource.API.searchFixedAssets(Resource.SearchPaging.TxtSearch , this.cbKeyFilter.DepartmentAssetId , this.cbKeyFilter.FixedAssetCategoryId , Resource.SearchPaging.PageSize ,  Resource.SearchPaging.PageIndex))
            .then(res => {
                this.isShowLoader = false;
                //Show Loader
                self.fixedAssets = res.data.Data;
                self.totalPage = res.data.TotalPage;
                self.totalRecord = res.data.TotalRecord;
                self.totalQuantity = res.data.TotalQuantity;
                self.totalCost = res.data.TotalCost;
                self.totalFixedAssetCummulative = res.data.TotalFixedAssetCummulative;
                self.totalFixedAssetRest = res.data.TotalFixedAssetRest;
            })
            .catch(error => {
                this.isShowLoader = false;
                console.log(error)
            })
        },

        /**
         * Method thực hiện show/Ẩn combobox
         * CreatedBy : DDHung (16/11/2021)
        */
        // btnButtonCombobox(event){
        //     CommonFunction.btnButtonCombobox(event);
        // },

        /**
         * Method thực hiện việc selected các item của combobox
         * CreatedBy : DDHung (25/10/2021)
         */
        itemComboboxClick(data){
            CommonFunction.selectedItemCombobox(data);
        },

        /**
         * Method dùng để lọc dữ liệu table bằng combobox
         * CreatedBy : DDHung (28/10/2021)
        */
        filterListFixedAsset : function(data)
        {
            try {
                var self = this;
                this.isShowLoader = true;
                CommonFunction.selectedItemCombobox(data);
                var fieldName = data.event.target.parentElement.parentElement.getAttribute('fieldName');
                this.cbKeyFilter[fieldName] = data.id;
                axios.get(Resource.API.Host + Resource.API.FixedAssets + Resource.API.searchFixedAssets(data.txtsearch , this.cbKeyFilter.DepartmentAssetId , this.cbKeyFilter.FixedAssetCategoryId , Resource.SearchPaging.PageSize ,  Resource.SearchPaging.PageIndex))
                .then(res => {
                    self.currentPage = 1;
                    self.isShowLoader =false;
                    self.fixedAssets = res.data.Data;
                    self.totalPage = res.data.TotalPage;
                    self.totalRecord = res.data.TotalRecord
                })
                .catch(error => {
                    this.isShowLoader = false;
                    console.log(error)
                })
            } catch (error) {
                console.log(error)
            }
        },

        /**
         * Method dùng để tìm kiếm tài sản theo mã và tên
         * CreatedBy : DDHung (19/11/2021)
        */
        searchByName :function(txtSearch){
            try {
                var self = this;
                self.txtSearch = txtSearch;
                axios.get(Resource.API.Host + Resource.API.FixedAssets + Resource.API.searchFixedAssets(self.txtSearch , this.cbKeyFilter.DepartmentAssetId , this.cbKeyFilter.FixedAssetCategoryId , Resource.SearchPaging.PageSize ,  Resource.SearchPaging.PageIndex))
                .then(res => {
                    self.currentPage = 1;
                    self.fixedAssets = res.data.Data;
                    self.totalPage = res.data.TotalPage;
                    self.totalRecord = res.data.TotalRecord
                })
                .catch(error => {
                    console.log(error)
                })
            } catch (error) {
                console.log(error)
            }
        },

        /**
         * Method dùng để show toast cảnh báo cho người dùng
         * CreatedBy : DDHung (16/11/2021)
        */
        showToastWarning : function(message){
            try {
                this.messageWarning = message;
                this.isShowToastWarning = true;
                setTimeout(() => {
                    this.isShowToastWarning = false
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
         * Method show dialog not valid
        */
        showDialogNotValid : function(message)
        {
            this.messageNotValid = '<span>' +  message  + '</span>';
            this.isShowDialogValidate = true;
        },

         /**
         * Method dùng để show toast thông báo Thêm/Sửa thành công nhân viên
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
         * Method cập nhật lại dữ liệu khi bấm thêm hoặc sửa
         * CreatedBy : DDHung(17/11/2021)
        */
        updateFixedAsset : function(){
            this.getData();
        },

        /**
         * Method dùng để đánh dấu là thêm tài sản
         * CreatedBy : DDHung (17/11/2021)
         */
        setIsFixedAsset : function(){
            this.isAddOrEdit = Enum.FormModel.Add
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
         * method : lấy thông tin tài sản cần sửa để load lên dialog
         * CreatedBy : DDHung (11/18/2021)
        */
        passFixedAssetEdit : function(obj){
            this.fixedAssetEdit = Object.assign({},obj);
        },

        /*      
        * method hiển thị dialog confirm cho người dùng về việc đóng popup thêm/sửa tài sản
        * CreatedBy : DDHung (18/11/2021)
        */
        confirmHidedDialog : function(message , isCancelSaveConfirm){
            try {
                this.isShowDialogConfirm = true;
                this.messageConfirm = message;
                this.isCancelSaveConfirm = isCancelSaveConfirm;
                console.log(this.isCancelSaveConfirm)
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
                this.hidedDialogFixedAsset();
                //Ẩn bỏ các button của dialog confirm
                this.isSaveConfirm = false;
                this.isCancelSaveConfirm = false;
                this.isDeleteConfirm = false;
            } catch (error) {
                console.log(error);
            }
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
         * Method dùng để xác nhận việc có sửa dữ liệu hay là không.
         * CreatedBy : DDHung (18/11/2021)
        */
        confirmSaveInfoFixedAsset : function(message , isSaveConfirm)
        {
            try {
                this.isShowDialogConfirm = true;
                this.messageConfirm = message;
                this.isSaveConfirm = isSaveConfirm;
            } catch (error) {
                console.log(error);
            }
        },

        /**
         * Method dùng để lưu thông tin khi sửa 1 bản ghi tài sản
         * CreatedBy : DDHung (18/11/2021)
        */
        btnSaveInfoConfirm : function()
        {
            try {
                var me = this;
                var dataToEditFixedAsset = CommonFunction.createObjectToAddOrEdit(this.fixedAssetEdited);
                axios.put(Resource.API.Host + Resource.API.FixedAssets +  '/' + `${this.fixedAssetEdit.FixedAssetId}`,dataToEditFixedAsset)
                .then(() => {
                    me.hidedDialogFixedAsset();
                    me.isShowDialogConfirm = false;
                    //Ẩn bỏ các button của dialog confirm
                    me.isSaveConfirm = false;
                    me.isCancelSaveConfirm = false;
                    me.isDeleteConfirm = false;
                    me.updateFixedAsset();
                    me.showToastSuccess(ToastJS.successMessage);
                })
                .catch(error => {
                    if(error.response.status == 400){
                        this.showDialogNotValid(error.response.data.Data.userMsg)
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
         * Method thực hiện lấy dữ liệu của tài sản đã sửa để phục vụ cho việc lưu dữ liệu đã sửa đó xuống database
         * CreatedBy : DDHung(18/11/2021)
        */
        transmitFixedAsset : function(fixedAssetEdited)
        {
            if(fixedAssetEdited)
            {
                this.fixedAssetEdited = Object.assign({},fixedAssetEdited);
            }
        },

        /**
         * Xử lý khi nhấn button Hủy trên dialog thông báo not valid các trường bắt buộc nhập
         * CreatedBy : DDHung (22/11/2021)
         */
        btnHideDialogNotValid : function()
        {
            try {
                this.ListFieldNotValidRequired = [];
                this.messageNotValid = "";
                this.isShowDialogValidate = false;

                // //Focus vào ô nhập liệu đầu tiên không thỏa mãn valid trên dialog confirm
                // var inputNotValid = this.dialogContent.querySelectorAll('input[validate="false"]')
                // inputNotValid[0].focus();
                
            } catch (error) {
                console.log(error)
            }
        },

        /**
         * Method focus vào ô nhập liệu đầu tiên trên dialog khi không thỏa mãn valid dữ liệu
         * CreatedBy : DDHung (2/12/2021) 
        */
        onFucusFirstInputNotValid : function(dialogContent)
        {

            if(dialogContent)
            {
                this.dialogContent = dialogContent;
            }
        },
    },


    created() {
        this.getDepartment();
        this.getFixedAssetCategories();
        this.getData();
    },
}
</script>
<style lang="">
    
</style>