<template lang="">
    <div class="grid-master">
        <table border="0" width="100%" cellpadding="0">
            <thead class="thead-table thead-grid-usedfixed-asset">
                <tr>
                    <th class="checkbox-table">
                        <input type="checkbox"  class = "grid-checkbox checkbox-header-grid">
                    </th>
                    <th class="stt-table align-center m-wrap-tooltip">
                        STT
                        <span class="m-tooltiptext m-tooltip-STT">Số thứ tự</span>
                    </th>
                    <th>Số chứng từ</th>
                    <th class = "align-center">Ngày chứng từ</th>
                    <th class = "align-center">Ngày ghi tăng</th>
                    <th class="align-right total-cost-master">Tổng nguyên giá</th>
                    <th class = "column-describe">Nội dung</th>
                </tr>
            </thead>
            <tbody ref = "tbodyVouchers" v-if="vouchers.length > 0">
                <tr v-for = "(item, index) in vouchers" :key="item.VoucherId"
                    @click = "handleClickRow(item.VoucherId , $event)" :ref = "item.VoucherId"
                    @contextmenu="rightClickHandler($event , item.VoucherId)" 
                    @mouseover = "showFeatureRecord(item.VoucherId)" @mouseleave = "hideFeatureRecord(item.VoucherId)"
                >   
                    <th class="checkbox-table">
                        <input type = "hidden" v-bind:value = "item.VoucherId" class = "input-hidden" />
                        <input type="checkbox" class = "grid-checkbox" v-on:click = "btnSeclectedDelete(item.VoucherId,$event)">
                    </th>
                    <td class="stt-table align-center">{{startSTT + index}}</td>
                    <td class = "m-voucherscode" @click = "clickVoucherCode(item.VoucherId)" >{{item.VouchersCode}}</td>
                    <td class = "align-center">{{item.VouchersDate | formatDate}}</td>
                    <td class = "align-center">{{item.UsedFixedAssetDate | formatDate}}</td>
                    <td class="align-right">{{item.TotalCost | formatMoney}}</td>
                    <td class = "column-description">
                        <span>{{item.Description}}</span>
                        <div class = "feature grid-master-feature hide-feature"><!-- hide-feature -->
                            <div class = "edit-record wrap-icongrid-tooltip" v-on:click = "btnShowDialogToEdit(item.VoucherId)">
                                <span class="grid-tooltiptext showTooltip-filterbar">Sửa</span>
                            </div>
                            <div class = "icon-delete-record wrap-icongrid-tooltip" v-on:click = "btnDeleteRecord(item.VoucherId)" >  
                                <span class="grid-tooltiptext showTooltip-filterbar tooltiptext-replication">Xóa</span>
                            </div>
                        </div>    
                    </td>
                </tr>
                <MLoaderGrid v-show = "isShowLoader" />
            </tbody>
             <tbody v-else>
                <div class = "no-content-grid">
                    Không có dữ liệu
                </div>
                <MLoaderGrid v-show = "isShowLoader" />
            </tbody>
        </table>

        <MMenuContext 
            v-bind:textAdd = "contextMenu.add"
            v-bind:textEdit = "contextMenu.edit"
            v-bind:textDelete = "contextMenu.delete"
        />
        
    </div>
</template>
<script>

/* Import Component */
import MLoaderGrid from '../../components/base/MLoaderGrid.vue'
import MMenuContext from '../../components/base/MMenuContext.vue'

/* File Javascript */
import { CommonFunction } from '../../assets/js/common/commonfunction'
import {Enum} from '../../assets/js/common/enum'
/* Import library */
import axios from 'axios'

/* File Javascript */
import {Resource} from '../../assets/js/common/resource'
// import {Common} from '../../assets/js/common/common'
export default {

    data() {
        return {
            startSTT : 1,
            listIdSelectedRecord : [],
            isClickCheckbox : false,
            sttPrevShiftClick : 0,
            checkGrowAndReduceStt : 0,
            isOnlyUpdated : true,
            contextMenu : {
                add : Resource.ContextMenu.Vouchers.add,
                edit : Resource.ContextMenu.Vouchers.edit,
                delete : Resource.ContextMenu.Vouchers.delete
            },
            checkIdLoadDetailGrid : null,
            listCheckChangeVouchers : [],
        }
    },
    props : {
        vouchers : {
            typeof : Array
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
    components : {
        MLoaderGrid,
        MMenuContext
    },
    created() {
        this.startSTT = ((this.currentPage - 1) * 40 + 1);
        this.listCheckChangeVouchers = this.vouchers
    },

    updated() {
        
        if(this.listCheckChangeVouchers != this.vouchers)
        {
            //kiểm tra khi chọn hết các dòng trên grid thì ô checkbox trên header sẽ được chọn
            var tbody = this.$refs.tbodyVouchers;
            CommonFunction.handleCheckboxHeader(tbody);
            this.listCheckChangeVouchers = this.vouchers;
        }

        if(this.firstVoucherData && this.isAddOrEdit == Enum.FormModel.Add || this.isOnlyUpdated == true)
        {
            let trTables = this.$refs.tbodyVouchers.children;
            for(let trTable of trTables)
            {
                if(trTable.classList.contains('selected-record'))
                {
                    trTable.classList.remove('selected-record')
                    var checkbox = trTable.querySelector(".grid-checkbox");
                    checkbox.checked = false;
                }
            }

            var trSelected = this.$refs[this.firstVoucherData.VoucherId];
            trSelected[0].classList.add("selected-record");
            var checkboxSelected = trSelected[0].querySelector(".grid-checkbox");
            checkboxSelected.checked = true;
            document.getElementById("context-menu").classList.remove("active");
            this.isOnlyUpdated = false;
        }
    },

    methods: {

        /**
         * Method thực hiện việc selected record phục vụ cho việc xóa dữ liệu
         * createdBy : DDHung (19/11/2021)
        */
        btnSeclectedDelete : function(voucherId , event)
        {
            try {
                var self = this;
                this.isClickCheckbox = true;
                var checkbox =  event.target;
                var trSelected = self.$refs[voucherId];
                if(checkbox.checked)
                {
                    trSelected[0].classList.add("selected-record");
                    this.listIdSelectedRecord.push(voucherId);
                }else{
                    trSelected[0].classList.remove('selected-record')
                    var newArray = CommonFunction.arrayRemove(this.listIdSelectedRecord , voucherId);
                    this.listIdSelectedRecord = newArray;
                }
                document.getElementById("context-menu").classList.remove("active");
                self.$emit("insertVoucherIdToListId" , this.listIdSelectedRecord);
            } catch (error) {
                console.log(error)
            }
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
         * Sự kiện khi hover vào một bản ghi trên table dùng để show nội dung cột chức năng
         * CreatedBy : DDHung (19/11/2021)
         */
        showFeatureRecord : function(voucherId)
        {
            try {
                var me = this;
                var trSelected = me.$refs[voucherId];
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
         * Sự kiện khi di chuyển chuột ra khỏi bản ghi (mouseleave) dùng để ẩn nội dung cột chức năng.
         * CreatedBy : DDHung (19/11/2021)
        */
        hideFeatureRecord : function(voucherId)
        {
            try {
                var me = this;
                var trSelected = me.$refs[voucherId];
                var feature = trSelected[0].querySelector('.feature');
                feature.classList.add('hide-feature');
            } catch (error) {
                console.log(error);
            }
        },
        
        /**
         * Method xử lý khi click vô mã chứng từ trên grid
         * CreatedBy : DDHung (19/12/2021)
        */

        clickVoucherCode : function(voucherId)
        {
           this.btnShowDialogToEdit(voucherId);
        },

        /**
        * Method dùng để show dialog sửa chứng từ khi click mã chứng từ trên grid
        * CreatedBy : DDHung (19/12/2021)
        */  
        btnShowDialogToEdit : function(voucherId)
        {
            try {
                var self = this;
                self.$emit('passIsAddOrEdit' , Enum.FormModel.Edit)
                axios.get(Resource.API.Host + Resource.API.Vouchers +  '/' + `${voucherId}`)
                .then((res) => {
                    self.$emit('passVoucherEdit',  res.data)
                    self.$emit('showDialogVoucher')
                })
                .catch((res) => {
                    console.log(res);
                })
            } catch (error) {
                console.log(error)
            }
        },

        /**
         * xử lý khi click vào một bản ghi trên Grid 
         * CreatedBy : DDHung (24/11/2021)
        */
        handleClickRow : function(voucherId , event)
        {
            try {
                var self = this;
                if(event.ctrlKey)
                {
                    CommonFunction.handleCtrlClickRowGrid(self , voucherId);
                    var checkContainFixAssetId = this.listIdSelectedRecord.includes(voucherId);
                    if(checkContainFixAssetId == false)
                    {
                        this.listIdSelectedRecord.push(voucherId);
                    }   
                }else if(event.shiftKey){
                    console.log("shift");
                    //Khởi tạo mảng lưu trữ STT các row đã được selected
                    var listSttRowSelected = [];
                    //Stt của dòng mà người dùng shift click vào
                    var sttRowShiftClick = event.target.parentElement.querySelector(".stt-table").textContent;
                    var sttFocus = 0;
                    var trFocus = null;
                    for(let item of this.listIdSelectedRecord)
                    {
                        trFocus = this.$refs[item];
                        sttFocus = trFocus[0].querySelector(".stt-table").textContent;
                        listSttRowSelected.push(parseInt(sttFocus));
                    }
                    var sttCurrent = sttRowShiftClick;
                    var trSelectedByShiftClick = null;
                    //Lần đầu tiên chạy Shift Click
                    if(this.sttPrevShiftClick == 0)
                    {
                        this.sttPrevShiftClick = sttRowShiftClick;
                        if(parseInt(sttFocus) > parseInt(sttRowShiftClick))
                        {
                            this.checkGrowAndReduceStt = -1;//đang giảm
                            trSelectedByShiftClick = trFocus[0].previousElementSibling;
                            while(sttCurrent != sttFocus)
                            {
                                sttFocus = trSelectedByShiftClick.querySelector(".stt-table").textContent;
                                trSelectedByShiftClick.classList.add("selected-record");
                                let idRecordSelected = trSelectedByShiftClick.querySelector(".input-hidden").value;
                                this.listIdSelectedRecord.push(idRecordSelected);
                                let checkboxSelected = trSelectedByShiftClick.querySelector(".grid-checkbox");
                                checkboxSelected.checked = true;
                                trSelectedByShiftClick = trSelectedByShiftClick.previousElementSibling;
                            }
                        }
                        else{
                            this.checkGrowAndReduceStt = 1;//Đang tăng
                            // var sttCurrent = sttRowShiftClick;
                            trSelectedByShiftClick = trFocus[0].nextElementSibling;
                            while(sttCurrent != sttFocus)
                            {
                                sttFocus = trSelectedByShiftClick.querySelector(".stt-table").textContent;
                                trSelectedByShiftClick.classList.add("selected-record");
                                let idRecordSelected = trSelectedByShiftClick.querySelector(".input-hidden").value;
                                this.listIdSelectedRecord.push(idRecordSelected);
                                let checkboxSelected = trSelectedByShiftClick.querySelector(".grid-checkbox");
                                checkboxSelected.checked = true;
                                trSelectedByShiftClick = trSelectedByShiftClick.nextElementSibling;
                            }
                        }
                    }else{
                        //Đang tăng
                        if(parseInt(this.sttPrevShiftClick) < parseInt(sttRowShiftClick) && this.checkGrowAndReduceStt == 1)
                        {
                            trFocus = this.$refs[this.listIdSelectedRecord[this.listIdSelectedRecord.length - 1]];
                            trSelectedByShiftClick = trFocus[0].nextElementSibling;
                            sttFocus = Math.max(...listSttRowSelected);
                            while(sttCurrent != sttFocus)
                            {
                                sttFocus = trSelectedByShiftClick.querySelector(".stt-table").textContent;
                                trSelectedByShiftClick.classList.add("selected-record");
                                let idRecordSelected = trSelectedByShiftClick.querySelector(".input-hidden").value;
                                this.listIdSelectedRecord.push(idRecordSelected);
                                let checkboxSelected = trSelectedByShiftClick.querySelector(".grid-checkbox");
                                checkboxSelected.checked = true;
                                trSelectedByShiftClick = trSelectedByShiftClick.nextElementSibling;
                            }
                        }
                        else if(parseInt(this.sttPrevShiftClick) > parseInt(sttRowShiftClick) && this.checkGrowAndReduceStt == 1)
                        {
                            //Đang tăng thì giảm
                            //Bỏ hết các dòng đã được selected trước đó
                            let trTables = self.$refs.tbodyVouchers.children;
                            for(let trTable of trTables)
                            {
                                if(trTable.classList.contains('selected-record'))
                                {
                                    trTable.classList.remove('selected-record')
                                    let checkbox = trTable.querySelector(".grid-checkbox");
                                    checkbox.checked = false;
                                }
                            }
                            trFocus = this.$refs[this.listIdSelectedRecord[0]];
                            trSelectedByShiftClick = trFocus[0];
                            sttFocus = Math.min(...listSttRowSelected);
                            this.listIdSelectedRecord.length = 0;
                            while(sttCurrent != sttFocus)
                            {
                                sttFocus = trSelectedByShiftClick.querySelector(".stt-table").textContent;
                                trSelectedByShiftClick.classList.add("selected-record");
                                let idRecordSelected = trSelectedByShiftClick.querySelector(".input-hidden").value;
                                this.listIdSelectedRecord.push(idRecordSelected);
                                let checkboxSelected = trSelectedByShiftClick.querySelector(".grid-checkbox");
                                checkboxSelected.checked = true;
                                trSelectedByShiftClick = trSelectedByShiftClick.previousElementSibling;
                            }
                        }
                        else if(parseInt(this.sttPrevShiftClick) < parseInt(sttRowShiftClick) && this.checkGrowAndReduceStt == -1)
                        {
                            //Đang giảm thì lại tăng
                            //Bỏ hết các dòng đã được selected trước đó
                            let trTables = self.$refs.tbodyVouchers.children;
                            for(let trTable of trTables)
                            {
                                if(trTable.classList.contains('selected-record'))
                                {
                                    trTable.classList.remove('selected-record')
                                    let checkbox = trTable.querySelector(".grid-checkbox");
                                    checkbox.checked = false;
                                }
                            }
                            trFocus = this.$refs[this.listIdSelectedRecord[0]];
                            trSelectedByShiftClick = trFocus[0];
                            sttFocus = Math.max(...listSttRowSelected);
                            this.listIdSelectedRecord.length = 0;
                            while(sttCurrent != sttFocus)
                            {
                                sttFocus = trSelectedByShiftClick.querySelector(".stt-table").textContent;
                                trSelectedByShiftClick.classList.add("selected-record");
                                let idRecordSelected = trSelectedByShiftClick.querySelector(".input-hidden").value;
                                this.listIdSelectedRecord.push(idRecordSelected);
                                let checkboxSelected = trSelectedByShiftClick.querySelector(".grid-checkbox");
                                checkboxSelected.checked = true;
                                trSelectedByShiftClick = trSelectedByShiftClick.nextElementSibling;
                            }
                        }
                        else if(parseInt(this.sttPrevShiftClick) > parseInt(sttRowShiftClick) && this.checkGrowAndReduceStt == -1)
                        {
                            //Đang giảm
                            trFocus = this.$refs[this.listIdSelectedRecord[this.listIdSelectedRecord.length - 1]];
                            trSelectedByShiftClick = trFocus[0].previousElementSibling;
                            sttFocus = Math.min(...listSttRowSelected);
                            while(sttCurrent != sttFocus)
                            {
                                sttFocus = trSelectedByShiftClick.querySelector(".stt-table").textContent;
                                trSelectedByShiftClick.classList.add("selected-record");
                                let idRecordSelected = trSelectedByShiftClick.querySelector(".input-hidden").value;
                                this.listIdSelectedRecord.push(idRecordSelected);
                                let checkboxSelected = trSelectedByShiftClick.querySelector(".grid-checkbox");
                                checkboxSelected.checked = true;
                                trSelectedByShiftClick = trSelectedByShiftClick.previousElementSibling;
                            }
                        }
                        //Check xem đang tăng hay giảm
                        if(parseInt(sttRowShiftClick) > parseInt(this.sttPrevShiftClick))
                        {
                            //lớn hơn so với stt shift click trước đó
                            this.checkGrowAndReduceStt = 1;
                            this.sttPrevShiftClick = sttRowShiftClick;
                        }else if(parseInt(sttRowShiftClick) < parseInt(this.sttPrevShiftClick)){
                            //nhỏ hơn so với stt shift click trước đó
                            this.checkGrowAndReduceStt = -1;
                            this.sttPrevShiftClick = sttRowShiftClick;
                        }
                    }
                }else{
                    if(this.isClickCheckbox == false)
                    {
                        var trTables = self.$refs.tbodyVouchers.children;
                        CommonFunction.handleClickRowGrid(self , voucherId ,trTables)
                        if(self.checkIdLoadDetailGrid != voucherId)
                        {
                            self.checkIdLoadDetailGrid = voucherId;
                            self.getListFixedAssetClickRowMaster(voucherId);
                        }
                        self.listIdSelectedRecord = []
                        this.sttPrevShiftClick = 0;
                        self.listIdSelectedRecord.push(voucherId)
                    }
                    else{
                        this.isClickCheckbox = false;
                    }
                }
                self.$emit("insertVoucherIdToListId" , this.listIdSelectedRecord); 
            } catch (error) {
                console.log(error)
            }
        },

        /**
         * Lấy danh sách tài sản dựa vào id ghi tăng
         * CreatedBy : DDHung(15/12/2021)
        */
        getListFixedAssetClickRowMaster : function(voucherId)
        {
            var self = this;
            self.$emit("showLoaderDetailGrid" , true)
            axios.get(Resource.API.Host + Resource.API.FixedAssets + Resource.API.getFixedAssetByVoucherId(voucherId))
            .then(res => {
                self.$emit("getFixedAssetClickRowMaster",res.data)
                self.$emit("showLoaderDetailGrid" , false)
            })
            .catch(error => {
                self.$emit("showLoaderDetailGrid" , false)
                console.log(error)
            })
        },

        /**
         * Sự kiện khi click chuột phải vào một bản ghi trên Grid 
         * CreatedBy : DDHung (24/11/2021)
        */
        rightClickHandler : function(event , voucherId)
        {   
            var self = this;
            // this.fixedAssetIdToDeleteContextMenu = fixedAssetId;
            // this.listIdSelectedRecord = [];
            // this.listIdSelectedRecord.push(fixedAssetId);
            var trTables = self.$refs.tbodyVouchers.children;
            CommonFunction.rightClickHandler(event , voucherId , trTables ,self);
        },
        
    },

    filters : {
        /**
         * Hàm thực hiện format date sang dạng (ngày/tháng/năm)
         * CreatedBy : DDHung (25/10/2021)
        */
        formatDate : function(value){
            // if(value)
            // {
            //     var date = new Date(value);
            //     let day = date.getDate();
            //     let month = date.getMonth() + 1;
            //     let year = date.getFullYear();
            //     day = day < 10 ? '0' + day : day;
            //     month = month < 10 ? '0' + month : month;
            //     return day + '/' + month + '/' + year;
            // }else{
            //     return "";
            // }

            var dateFormated = CommonFunction.formatDate(value)
            return dateFormated
        },

        formatMoney : function(value)
        {
            var moneyFormated = CommonFunction.formatMoney(value);
            return moneyFormated;
        }
    },

    watch : {
        currentPage : function(val)
        {
            this.startSTT = ((val - 1) * 40 + 1);
        },

        firstVoucherData : function(firstVoucherData){
            if(firstVoucherData)
            {
                //selected vào dòng đầu tiên trên grid master mỗi khi xóa dữ liệu
                this.getListFixedAssetClickRowMaster(firstVoucherData.VoucherId)
                //selected vào dòng đầu tiên trên grid
                var trSelected = this.$refs[this.firstVoucherData.VoucherId];
                if(trSelected)
                {
                    console.log(firstVoucherData)
                    trSelected[0].classList.add("selected-record");
                    var checkboxSelected = trSelected[0].querySelector(".grid-checkbox");
                    checkboxSelected.checked = true;
                    document.getElementById("context-menu").classList.remove("active");
                }
            }
        }
    }
}
</script>
<style lang="">
    
</style>