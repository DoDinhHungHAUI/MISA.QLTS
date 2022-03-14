<template lang="">
    <div class="grid grid-fixed-assets">
        <div class="content-grid">
            <table border="0" width="100%" cellpadding = "0">
                <thead class="thead-table">
                    <tr>
                        <th class="checkbox-table">
                            <input type="checkbox" class = "grid-checkbox checkbox-header-grid" @click = "clickCheckboxAll($event)">
                        </th>
                        <th class="stt-table align-center m-wrap-tooltip">
                            STT
                            <span class="m-tooltiptext m-tooltip-STT">Số thứ tự</span>
                        </th>
                        <th>Mã tài sản</th>
                        <th>Tên tài sản</th>
                        <th>Loại tài sản</th>
                        <th>Bộ phẩn sử dụng</th>
                        <th class="align-right align-right-quantity">Số lượng</th>
                        <th class="align-right cost-grid">Nguyên giá</th>
                        <th class="align-right accumulated-grid m-wrap-tooltip">
                            HM/KH lũy kế
                            <span class="m-tooltiptext m-tooltip-thead-rest">Hào mòn/Khấu hao lũy kế</span>
                        </th>
                        <th class="align-right esidual-value-grid">Giá trị còn lại</th>
                        <th class = "freature-grid status-grid">Trạng thái</th>
                    </tr>
                </thead>
                <tbody ref="tbodyFixedAsset" v-if="fixedAssets.length > 0">
                    <tr v-for = "(item, index) in fixedAssets" :key="item.FixedAssetId" :ref = "item.FixedAssetId" @mouseover = "showFeatureRecord(item.FixedAssetId)" 
                        @mouseleave = "hideFeatureRecord(item.FixedAssetId)" @contextmenu="rightClickHandler($event , item.FixedAssetId)" tabindex = "0"
                        @click = "handleClickRow(item.FixedAssetId , $event)" @dblclick = "handleDblClickRow(item.FixedAssetId)" @keydown = "upDownRecordGrid($event , item.FixedAssetId)"
                    >
                        <th class="checkbox-table">
                            <input type = "hidden" v-bind:value = "item.FixedAssetId" class = "input-hidden" />
                            <input type="checkbox" class = "grid-checkbox"  v-on:click = "btnSeclectedDelete(item.FixedAssetId,$event)">
                        </th>
                        <td class="stt-table align-center">{{startSTT + index}}</td>
                        <td v-bind:title = "[formatText(item.FixedAssetCode) ? item.FixedAssetCode : '']" v-bind:class = "{'format-text' :  formatText(item.FixedAssetCode)}">{{item.FixedAssetCode}}</td>
                        <td v-bind:title = "[formatText(item.FixedAssetName) ? item.FixedAssetName : '']" v-bind:class = "{'format-text' :  formatText(item.FixedAssetName)}">{{item.FixedAssetName | formatNoContent}}</td>
                        <td v-bind:title = "[formatText(item.FixedAssetCategoryName) ? item.FixedAssetCategoryName : '']" v-bind:class = "{'format-text' :  formatText(item.FixedAssetCategoryName)}">{{item.FixedAssetCategoryName | formatNoContent}}</td>
                        <td v-bind:title = "[formatText(item.DepartmentAssetName) ? item.DepartmentAssetName : '']" v-bind:class = "{'format-text' :  formatText(item.DepartmentAssetName)}">{{item.DepartmentAssetName | formatNoContent}}</td>
                        <td class="align-right align-right-quantity">{{item.Quantity | formatMoney | formatNoContent}}</td>
                        <td class="align-right">{{item.Cost | formatMoney | formatNoContent}}</td>
                        <td class="align-right">{{item.FixedAssetCummulative | formatMoney | formatNoContent}}</td>
                        <td class="align-right">{{item.FixedAssetRest | formatMoney |formatNoContent}}</td>
                        
                        <!-- <td class = "freature-grid">
                            <div class = "feature hide-feature">
                                <div class = "edit-record wrap-icongrid-tooltip" v-on:click = "btnShowDialogToEdit(item.FixedAssetId)">
                                    <span class="grid-tooltiptext showTooltip-filterbar">Sửa</span>
                                </div>
                                <div class = "replication-record wrap-icongrid-tooltip" v-on:click = "btnShowDialogToReplication(item.FixedAssetId)"> 
                                    <span class="grid-tooltiptext showTooltip-filterbar">Nhân bản</span>
                                </div>
                            </div>
                        </td> -->

                        <td class = "wrap-icon-feature">
                            {{item.StatusFixedAsset | formatStatusFixedAsset}}

                            <div class = "feature hide-feature"><!-- hide-feature -->
                                <div class = "edit-record wrap-icongrid-tooltip" v-on:click = "btnShowDialogToEdit(item.FixedAssetId)">
                                    <span class="grid-tooltiptext showTooltip-filterbar">Sửa</span>
                                </div>
                                <div class = "replication-record wrap-icongrid-tooltip" v-on:click = "btnShowDialogToReplication(item.FixedAssetId)"> 
                                    <span class="grid-tooltiptext showTooltip-filterbar tooltiptext-replication">Nhân bản</span>
                                </div>
                                <div class = "icon-delete-record wrap-icongrid-tooltip" v-on:click = "btnDeleteRecord(item.FixedAssetId)" >  
                                    <span class="grid-tooltiptext showTooltip-filterbar tooltiptext-replication">Xóa</span>
                                </div>
                            </div>
                        </td>
                    </tr>
                    <MLoaderGrid v-show = "isShowLoader" />
                </tbody>
                <!-- <tbody v-else ref="tbodyFixedAsset">
                    <div class = "no-content-grid">
                        Không có dữ liệu
                    </div>
                    <MLoaderGrid v-show = "isShowLoader" />
                </tbody> -->
            </table>
        </div>
        
        <div class = "grid-summary-fixed-asset">
                <div class="wrap-total-summary-fixedasset">
                    <div class="total-quantity">
                    <!-- <span>{{totalQuantity | formatNumber}}</span> -->
                    <span>{{totalQuantity}}</span>
                </div>
                <div class="total-cost">
                    <span>{{totalCost | formatMoney}}</span>
                </div>
                <div class="total-hmkh">
                    <span>{{totalFixedAssetCummulative | formatMoney}}</span>
                </div>
                <div class="total-ResidualValue">
                    <span>{{totalFixedAssetRest | formatMoney}}</span>
                </div>
            </div>
        </div>

        <!-- <FixedAssetPaging 
            v-bind:totalPage = "totalPage"
            v-bind:totalRecord = "totalRecord"
            v-on:btnPaging = "btnPaging"
            v-bind:totalCost = "totalCost"
            v-bind:totalQuantity = "totalQuantity"
            v-bind:totalFixedAssetCummulative = "totalFixedAssetCummulative"
            v-bind:totalFixedAssetRest = "totalFixedAssetRest"
            
        /> -->

        <MPaging 
            v-bind:totalPage = "totalPage"
            v-bind:totalRecord = "totalRecord"
            v-on:btnPaging = "btnPaging"
            v-bind:customClass = "'paging'"
        />

        <!-- v-bind:currentPage = "currentPage" -->
        <MMenuContext 
            v-on:addFixedAssetContextMenu = "addFixedAssetContextMenu" 
            v-on:deleteFixedAssetContextMenu = "deleteFixedAssetContextMenu" 
            v-on:editFixedAssetContextMenu = "editFixedAssetContextMenu"
            v-bind:textAdd = "contextMenu.add"
            v-bind:textEdit = "contextMenu.edit"
            v-bind:textDelete = "contextMenu.delete"
        />

    </div>
</template>
<script>

/* Import Library */
import axios from 'axios'

/* Import Component */
// import FixedAssetPaging from './FixedAssetPaging.vue'
import MLoaderGrid from '../../components/base/MLoaderGrid.vue'
import MMenuContext from '../../components/base/MMenuContext.vue'
import MPaging from '../../components/base/MPaging.vue'
/* File Javascript */
import { CommonFunction } from '../../assets/js/common/commonfunction'
import {Enum} from '../../assets/js/common/enum'
import {Resource} from '../../assets/js/common/resource'
export default {
    data() {
        return {
            startSTT : 1,
            fixedAssetIdToDeleteContextMenu : null,
            isClickCheckbox : false,
            listIdSelectedRecord : [],
            sttPrevShiftClick : 0,
            checkGrowAndReduceStt : 0,
            /* Danh sách dùng để check sự thay đổi của danh sách fixedAssets (danh sách mà sẽ được render lên grid) */
            listCheckChangeFixedAssets : [],
            /* Biến dùng cho việc tính toán width của các cột summary tương ứng các cột trên grid (tính toán 1 lần duy nhất cho việc render lần đầu tiên) */
            isCalculateWidthSummary: true,
            contextMenu : {
                add : Resource.ContextMenu.FixedAsset.add,
                edit : Resource.ContextMenu.FixedAsset.edit,
                delete : Resource.ContextMenu.FixedAsset.delete
            }
        }
    },
    props : {
        fixedAssets : {
            type : Array 
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
        totalQuantity : {
            type : Number,
        },
        totalFixedAssetCummulative : {
            type : Number,
        },
        totalFixedAssetRest : {
            type : Number,
        },
        isShowLoader : {
            type : Boolean
        },
        currentPage : {
            type : Number
        },
    },
    created() {
        this.startSTT = ((this.currentPage - 1) * 40 + 1);
        this.listCheckChangeFixedAssets = this.fixedAssets;
    },
   
    updated() {
        if(this.listCheckChangeFixedAssets != this.fixedAssets)
        {
            //kiểm tra khi chọn hết các dòng trên grid thì ô checkbox trên header sẽ được chọn
            var tbody = this.$refs.tbodyFixedAsset;
            CommonFunction.handleCheckboxHeader(tbody);
            this.listCheckChangeFixedAssets = this.fixedAssets;
        }

        if(this.isCalculateWidthSummary == true)
        {
            var grid = document.querySelector('.grid-fixed-assets')
            var quantityGrid = grid.querySelector('.align-right-quantity')
            var costGrid = grid.querySelector('.cost-grid')
            var accumulatedGrid = grid.querySelector('.accumulated-grid')
            var esidualValueGrid = grid.querySelector('.esidual-value-grid')
            var statusGrid = grid.querySelector('.status-grid')

            var summary = document.querySelector('.wrap-total-summary-fixedasset')
            var totalQuantity = summary.querySelector('.total-quantity')
            var totalCost = summary.querySelector('.total-cost')
            var totalHmkh = summary.querySelector('.total-hmkh')
            var totalResidualValue = summary.querySelector('.total-ResidualValue')
            
            totalQuantity.style.width = quantityGrid.clientWidth + "px"
            totalCost.style.width = costGrid.clientWidth + "px"
            totalHmkh.style.width = accumulatedGrid.clientWidth + "px"
            totalResidualValue.style.width = esidualValueGrid.clientWidth + "px"
            summary.style.right = statusGrid.clientWidth + Enum.PaddingTdGrid + "px"

            this.isCalculateWidthSummary = false
        }
    },

    components : {
        // FixedAssetPaging,
        MLoaderGrid,
        MMenuContext,
        MPaging
    },
    methods: {

        /**
         * Method thực hiện việc xóa bản ghi của chứng từ
         * CreatedBy : DDHung(20/12/2021)
        */
        btnDeleteRecord : function(fixedAssetId)
        {
            this.$emit("btnDeleteRecord" , fixedAssetId)
        },

        /**
         *  Sự kiện click vào ô checkbox trên header thì sẽ chọn tất cả ô checkbox trên grid và ngược lại
         * createdBy : DDHung(2/12/2021) 
        */
        clickCheckboxAll : function(event)
        {
            var self = this;
            var checkBoxHeader = event.target;
            //checked hết tất cả checkbox trong grid
            var trTables = self.$refs.tbodyFixedAsset.children;
            if(checkBoxHeader.checked)
            {
                for(let trTable of trTables)
                {
                    if(trTable.classList.contains('selected-record'))
                    {
                        continue;
                    }
                    else{
                        trTable.classList.add('selected-record')
                        let checkbox = trTable.querySelector(".grid-checkbox");
                        checkbox.checked = true;
                    }
                }
            }else{
                for(let trTable of trTables)
                {
                    if(trTable.classList.contains('selected-record'))
                    {
                        trTable.classList.remove('selected-record')
                        let checkbox = trTable.querySelector(".grid-checkbox");
                        checkbox.checked = false;
                    }
                    
                }
            }
        },

        /**
         * Sự kiện khi click chuột phải vào một bản ghi trên Grid 
         * CreatedBy : DDHung (24/11/2021)
        */
        rightClickHandler : function(event , fixedAssetId)
        {   
            var self = this;
            this.fixedAssetIdToDeleteContextMenu = fixedAssetId;
            this.listIdSelectedRecord = [];
            this.listIdSelectedRecord.push(fixedAssetId);
            var trTables = self.$refs.tbodyFixedAsset.children;
            CommonFunction.rightClickHandler(event , fixedAssetId , trTables ,self);
        },
        
        /**
         * Method sử lý sự kiện lên xuống dòng bằng bàn phím (sự kiện keydown)
         * CreatedBy : DDHung (06/12/2021)
        */
        upDownRecordGrid : function(event , fixedAssetId)
        {
            var self = this;
            if(event.keyCode == 38 || event.keyCode == 40){
                //dịch chuyển lên trên , xuống dưới bằng bàn phím
                let trFocus = this.$refs[fixedAssetId];
                let checkboxSelected = trFocus[0].querySelector(".grid-checkbox");
                this.listIdSelectedRecord = [];
                let trNextFocus;
                if(event.keyCode == 38)
                {
                    trNextFocus = trFocus[0].previousElementSibling
                }
                else{
                    trNextFocus = trFocus[0].nextElementSibling
                }
                if(trNextFocus && trNextFocus.tagName == 'TR')
                {
                    trFocus[0].classList.remove("selected-record");
                    checkboxSelected.checked = false;
                    trNextFocus.classList.add("selected-record");
                    checkboxSelected =  trNextFocus.querySelector(".grid-checkbox");
                    checkboxSelected.checked = true;
                    let idRecordSelected = trNextFocus.querySelector(".input-hidden").value;
                    trNextFocus.focus();
                    this.listIdSelectedRecord.push(idRecordSelected);
                    self.$emit("insertFixedAssetIdToListId" , this.listIdSelectedRecord)
                }
            }   
        },

        /**
         * Xử lý khi double click vào một bản ghi trên Grid
         * CreatedBy : DDHung (6/12/2021)
         */
        handleDblClickRow : function(fixedAssetId)
        {
            this.btnShowDialogToEdit(fixedAssetId);
        },

        /**
         * xử lý khi click vào một bản ghi trên Grid 
         * CreatedBy : DDHung (24/11/2021)
         */
        handleClickRow :function(fixedAssetId , event)
        {
            try {
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
                    console.log("Shift")
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
                }   
                else{
                    if(this.isClickCheckbox == false)
                    {
                        var trTables = self.$refs.tbodyFixedAsset.children;
                        CommonFunction.handleClickRowGrid(self , fixedAssetId , trTables);

                        this.listIdSelectedRecord = [];
                        this.sttPrevShiftClick = 0;
                        this.listIdSelectedRecord.push(fixedAssetId);
                    }
                    else{
                        this.isClickCheckbox =false;
                    }
                }
                self.$emit("insertFixedAssetIdToListId" , this.listIdSelectedRecord);

                //kiểm tra khi chọn hết các dòng trên grid thì ô checkbox trên header sẽ được chọn
                var tbody = self.$refs.tbodyFixedAsset;
                CommonFunction.handleCheckboxHeader(tbody);

                // var lstTrInGrid = tbody.querySelectorAll('tr');
                // var lstTrSelectedInGrid = tbody.querySelectorAll('tr.selected-record');
                // var checkboxHeader = document.querySelector(".checkbox-header-grid");
                // if(parseInt(lstTrInGrid.length) == parseInt(lstTrSelectedInGrid.length))
                // {
                //     //chọn ô checkbox trên header
                //     checkboxHeader.checked = true;
                // }else{
                //     checkboxHeader.checked = false;
                // }

            } catch (error) {
                console.log(error);
            }
        },

        /**
         * Chức năng xóa một tài sản tại menucontext
         * CreatedBy : DDHung (24/11/2021)
        */
        deleteFixedAssetContextMenu : function()
        {
            document.getElementById("context-menu").classList.remove("active");
            this.$emit("deleteFixedAssetContextMenu" , this.fixedAssetIdToDeleteContextMenu)
        },

        /**
         * Chức năng thêm một tài sản mới tại menucontext
         * CreatedBy : DDHung (24/11/2021)
        */
        addFixedAssetContextMenu : function()
        {   
            try {
                var self = this;
                CommonFunction.handleClickRow(self);
                this.$emit("addFixedAssetContextMenu")
                this.$emit("setIsFixedAsset")
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
         * Method thực hiện việc selected record phục vụ cho việc xóa dữ liệu
         * createdBy : DDHung (19/11/2021)
        */
        btnSeclectedDelete : function(fixedAssetId , event)
        {
            try {
                var self = this;
                this.isClickCheckbox = true;
                var checkbox =  event.target;
                var trSelected = self.$refs[fixedAssetId];
                if(checkbox.checked)
                {
                    trSelected[0].classList.add("selected-record");
                    // self.$emit("insertFixedAssetIdToListId" , fixedAssetId)
                    this.listIdSelectedRecord.push(fixedAssetId);
                }else{
                    trSelected[0].classList.remove('selected-record')
                    // self.$emit("deleteFixedAssetIdToListId" , fixedAssetId)
                    var newArray = CommonFunction.arrayRemove(this.listIdSelectedRecord , fixedAssetId);
                    this.listIdSelectedRecord = newArray;
                }
                document.getElementById("context-menu").classList.remove("active");
                self.$emit("insertFixedAssetIdToListId" , this.listIdSelectedRecord);
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
        * Method dùng để show dialog sửa tài sản khi click icon sửa
        * CreatedBy : DDHung (18/11/2021)
        */  
        btnShowDialogToEdit : function(fixedAssetId)
        {
            try {
                var self = this;
                self.$emit('passIsAddOrEdit' , Enum.FormModel.Edit)
                axios.get(Resource.API.Host + Resource.API.FixedAssets +  '/' + `${fixedAssetId}`)
                .then((res) => {
                    self.$emit('passFixedAssetEdit',  res.data)
                    self.$emit('showDialogFixedAsset')
                })
                .catch((res) => {
                    console.log(res)
                })
            } catch (error) {
                console.log(error)
            }
        },

        /**
        * Method dùng để show dialog nhân bản tài sản khi click icon nhân bản
        * CreatedBy : DDHung (18/11/2021)
        */
        btnShowDialogToReplication : function(fixedAssetId)
        {
            try {
                var self = this;
                self.$emit('passIsAddOrEdit' , Enum.FormModel.Replication)
                axios.get(Resource.API.Host + Resource.API.FixedAssets +  '/' + `${fixedAssetId}`)
                .then((res) => {
                    self.$emit('passFixedAssetEdit',  res.data)
                    self.$emit('showDialogFixedAsset');
                })
                .catch((res) => {
                    console.log(res);
                })
            } catch (error) {
                console.log(error)
            }
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
         * chức năng sửa một tài sản tại menucontext
         * CreatedBy : DDHung (24/11/2021)
        */
        editFixedAssetContextMenu : function()
        {
            try {
                var self = this;
                CommonFunction.handleClickRow(self);
                self.$emit('passIsAddOrEdit' , Enum.FormModel.Edit)//Rồi sẽ dùng enum
                axios.get(Resource.API.Host + Resource.API.FixedAssets +  '/' + `${this.fixedAssetIdToDeleteContextMenu}`)
                .then((res) => {
                    console.log(res.data)
                    // self.employeeedit =  Object.assign({}, res.data);
                    self.$emit('passFixedAssetEdit',  res.data)
                    self.$emit('showDialogFixedAsset');
                })
                .catch((res) => {
                    console.log(res);
                })
            } catch (error) {   
                console.log(error)
            }
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
            // if (value) {
            //     return Number(value).toFixed(0).replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1.");
            // }
            // return "";
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
    },
    watch : {
        currentPage : function(val)
        {
            this.startSTT = ((val - 1) * 40 + 1);
        },
    }
}
</script>
<style lang="">
    
</style>