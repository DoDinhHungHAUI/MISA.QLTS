<template lang="">
    <div class="m-popup toggle-popup">
        <div class="wrap-popup"></div>
        <div class="popup-choose-assets">
            <div class="header-popup header-choose-fixedasset m-wrap-tooltip">
                Chọn tài sản ghi tăng
                <button class="close-popup" id="close-popup" v-on:click = "btnCloseChooseFixedAsset">
                     
                </button>
                <span class="m-tooltiptext m-tooltip-close">Đóng</span>
            </div>
            <div class = "wrap-form-choose-fixedasset">
                <div class="content-popup content-choose-fixedasset">
                    <div class = "toolbar-popup-fixedasset toolbar-choose-fixedasset">
                        <input type="text" class = "icon-search input-search" id = "input-search" placeholder = "Tìm kiếm " style="width : 200px" v-model = "inputSearch" />
                    </div>
                    <div class="grid-detail grid-choose-fixedasset">
                        <div class="grid-detail-fixedasset-popup detail-choose-fixedasset">
                            <table border="0" width="100%" cellpadding="0">
                                <thead class="thead-table thead-grid-fixedasset-popup">
                                    <tr>
                                        <th class="checkbox-table">
                                            <input type="checkbox" class = "grid-checkbox checkbox-header-grid">
                                        </th>
                                        <th class="stt-table align-center">STT</th>
                                        <th>Mã tài sản</th>
                                        <th>Tên tài sản</th>
                                        <th>Bộ phận sử dụng</th>
                                        <th class="align-right column-cost-dialog">Nguyên giá</th>
                                        <th class="align-right column-fixedassetrest-dialog">Giá trị còn lại</th>
                                    </tr>
                                </thead>
                                <tbody v-if="dialogFixedAssetToChoose.fixedAssetToChoose.length > 0" ref="tbodyFixedAsset">
                                    <tr v-for = "(item, index) in dialogFixedAssetToChoose.fixedAssetToChoose" :key="item.FixedAssetId"
                                        @click = "handleClickRow(item.FixedAssetId , $event)" :ref = "item.FixedAssetId"
                                    >
                                        <th class="checkbox-table">
                                            <input type = "hidden" v-bind:value = "item.FixedAssetId" class = "input-hidden" />
                                            <input type="checkbox" class = "grid-checkbox">
                                        </th>
                                        <td class="stt-table align-center">{{startSTT + index}}</td>
                                        <td v-bind:title = "[formatText(item.FixedAssetCode) ? item.FixedAssetCode : '']" v-bind:class = "{'format-text' :  formatText(item.FixedAssetCode)}">{{item.FixedAssetCode}}</td>
                                        <td v-bind:title = "[formatText(item.FixedAssetName) ? item.FixedAssetName : '']" v-bind:class = "{'format-text' :  formatText(item.FixedAssetName)}">{{item.FixedAssetName | formatNoContent}}</td>
                                        <td v-bind:title = "[formatText(item.DepartmentAssetName) ? item.DepartmentAssetName : '']" v-bind:class = "{'format-text' :  formatText(item.DepartmentAssetName)}">{{item.DepartmentAssetName | formatNoContent}}</td>
                                        <td class="align-right">{{item.Cost | formatMoney | formatNoContent}}</td>
                                        <td class="align-right">{{item.FixedAssetRest | formatMoney |formatNoContent}}</td>
                                    </tr>
                                    <MLoaderGrid v-show = "dialogFixedAssetToChoose.isShowLoader" />
                                </tbody>
                            </table>
                        </div>
                    </div>        
                </div>
            </div>
            <!-- <FixedAssetPagingDialog 
                v-bind:totalPage = "dialogFixedAssetToChoose.totalPage"
                v-bind:totalRecord = "dialogFixedAssetToChoose.totalRecord"
                v-on:btnPagingFixedAssetDialog = "btnPagingFixedAssetDialog"
            /> -->

            <MPaging 
                v-bind:totalPage = "dialogFixedAssetToChoose.totalPage"
                v-bind:totalRecord = "dialogFixedAssetToChoose.totalRecord"
                v-on:btnPaging = "btnPaging"
                v-bind:customClass = "'paging-usedfixedasset paging-choosed-fixedasset'"
            />

            <div class="footer-popup">
                <div class="wrap-button-footer">
                    <button type="button" class="m-btn-cancel" id="btn-close-popup">
                        Hủy
                    </button>
                    <button type="button" class="m-btn m-btn-save" id="btnSave" v-on:click = "btnSaveFixedAssetChoosed">
                        Lưu
                    </button>
                </div>
            </div>
        </div>
    </div>
</template>
<script>
/* Component */
// import FixedAssetPagingDialog from './FixedAssetPagingDialog.vue'
import MLoaderGrid from '../../components/base/MLoaderGrid.vue'
import MPaging from '../../components/base/MPaging.vue'
/* File Javascript */
import { CommonFunction } from '../../assets/js/common/commonfunction'
import {Enum} from '../../assets/js/common/enum'

export default {
    data() {
        return {
            startSTT : 1,
            txtsearch : '',
            listIdSelectedRecord : [],
            sttPrevShiftClick : 0,
            checkGrowAndReduceStt : 0,
            isClickCheckbox : false,
        }
    },
    props : {
        dialogFixedAssetToChoose :{
            typeof : Object
        }
    },
    components : {
        // FixedAssetPagingDialog,
        MPaging,
        MLoaderGrid
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
    methods: {
        /**
         * Method thực hiện việc đóng dialog chọn tài sản
         * CreatedBy : DDHung (17/12/2021) 
        */
        btnCloseChooseFixedAsset : function()
        {
            this.$emit("btnCloseChooseFixedAsset")
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
         * Method thực hiện việc phân trang cho grid tài sản trên dialog chọn tài sản ghi tăng
         * CreatedBy : DDHung (17/12/2021) 
        */
        btnPaging : function(currentPage)
        {
            this.$emit("btnPagingFixedAssetDialog" , currentPage)
        },

        /**
         * xử lý khi click vào một bản ghi trên Grid 
         * CreatedBy : DDHung (24/11/2021)
        */

        handleClickRow :function(fixedAssetId , event)
        {
            var self = this;
            if(event.ctrlKey)
            {
                CommonFunction.handleCtrlClickRowGrid(self , fixedAssetId);
                var checkContainFixAssetId = this.listIdSelectedRecord.includes(fixedAssetId);
                if(checkContainFixAssetId == false)
                {
                    this.listIdSelectedRecord.push(fixedAssetId);
                }   
            }else if(event.shiftKey)
            {
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
                }
                else{
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
                        let trTables = self.$refs.tbodyFixedAsset.children;
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
                        let trTables = self.$refs.tbodyFixedAsset.children;
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
                    var trTables = self.$refs.tbodyFixedAsset.children;
                    CommonFunction.handleClickRowGrid(self , fixedAssetId , trTables);

                    this.listIdSelectedRecord = [];
                    this.sttPrevShiftClick = 0;
                    this.listIdSelectedRecord.push(fixedAssetId);
                }
                else{
                    this.isClickCheckbox = false;
                }
            }

            //kiểm tra khi chọn hết các dòng trên grid thì ô checkbox trên header sẽ được chọn
            var tbody = self.$refs.tbodyFixedAsset;
            CommonFunction.handleCheckboxHeader(tbody);
        },

        /**
         * Method lấy ra danh sách tài sản đã được chọn trên dialog để ghi tăng
         * CreatedBy : DDHung (17/11/2021)
        */
        btnSaveFixedAssetChoosed : function()
        {
            var self = this
            var listFixedAssetChoosed = []
            this.listIdSelectedRecord.forEach(item => {
                var fixedAssetChoosed = self.dialogFixedAssetToChoose.fixedAssetToChoose.find(obj => obj.FixedAssetId == item)
                listFixedAssetChoosed.push(fixedAssetChoosed)
            });
            this.$emit("btnSaveFixedAssetChoosed",listFixedAssetChoosed)
        }
    },
    
    watch : {
        'dialogFixedAssetToChoose.currentPage' : function(val)
        {
            this.startSTT = ((val - 1) * 40 + 1);
        },
        txtsearch : function()
        {
            this.$emit('searchFixedAssetDialog' , this.txtsearch)
        }
    },
    filters : {
        /**
         * Hàm thực hiện format tài sản đã được sử dụng hay chưa 
         * Nếu là 1 - Đã được sử dụng , 2 - chưa được sử dụng
         * CreatedBy : DDHung (13/12/2021)
         */
        formatStatusFixedAsset : function(val)
        {
            if(val == Enum.IsUsedFixedAsset.UsedFixedAsset)//Tài sản đã hoặc đang được sử dụng (chưa ghi tang)
            {
                return Enum.IsUsedFixedAsset.TextUsedFixedAsset
            }else{//chưa ghi tang
                return Enum.IsUsedFixedAsset.TextNotUsedFixedAsset
            }
        },

        /**
         * Hàm thực hiện format money
         * CreatedBy : DDHung (16/11/2021)
        */
        formatMoney : function(value){
            
            var moneyFormated = CommonFunction.formatMoney(value)
            return moneyFormated
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
        }
    }
}
</script>
<style lang="">
    
</style>