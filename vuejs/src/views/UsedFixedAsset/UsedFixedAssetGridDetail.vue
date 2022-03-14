<template lang="">
    <div class="grid-detail" ref="gridDetail">
        <div class="grid-summary-master">
            <div class = "total-cost-master">{{totalCost | formatMoney}}</div>
        </div>
        
        <!-- <Paging 
            v-bind:totalPage = "totalPage"
            v-bind:totalRecord = "totalRecord"
            v-on:btnPaging = "btnPaging"
        /> -->

        <MPaging 
            v-bind:totalPage = "totalPage"
            v-bind:totalRecord = "totalRecord"
            v-on:btnPaging = "btnPaging"
            v-bind:customClass = "'paging-usedfixedasset'"
        />

        <div class="resize" id="resize" @mousedown = "mouseDownResizeGridDetail($event) ">
            <div class = "icon-collpase-layout">
                <!-- <i class="fas fa-compress"></i> -->
            </div>
        </div>

        <div class="title-grid-detail">
            <div class="left-title-grid-detail">
                <p>Thông tin chi tiết</p>
            </div>
            <div class="right-title-grid-detail">
                <div class = "m-wrap-tooltip">
                    <div class="icon-zoom-grid-detail" id="clickZoomGridDetail" v-on:click = "clickZoomGridDetail"></div>
                    <span class="m-tooltiptext m-tooltip-usedfixed-asset">{{tooltipTextZoomGrid}}</span>
                </div>
            </div>
        </div>
        <div class="grid-detail-fixedasset">
            <table border="0" width="100%" cellpadding="0">
                <thead class="thead-table thead-grid-usedfixed-asset">
                    <tr>
                        <th class="stt-table align-center m-wrap-tooltip">
                            STT
                            <span class="m-tooltiptext m-tooltip-STT">Số thứ tự</span>
                        </th>
                        <th>Mã tài sản</th>
                        <th>Tên tài sản</th>
                        <th>Bộ phận sử dụng</th>
                        <th class="align-right">Nguyên giá</th>
                        <th class="align-right">Hao mòn lũy kế</th>
                        <th class="align-right">Giá trị còn lại</th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for = "(item, index) in listFixedAssetClickedRowMaster" :key="item.FixedAssetId">
                        <td class="stt-table align-center">{{index + 1}}</td>
                        <td v-bind:title = "[formatText(item.FixedAssetCode) ? item.FixedAssetCode : '']" v-bind:class = "{'format-text' :  formatText(item.FixedAssetCode)}">{{item.FixedAssetCode | formatNoContent}}</td>
                        <td v-bind:title = "[formatText(item.FixedAssetName) ? item.FixedAssetName : '']" v-bind:class = "{'format-text' :  formatText(item.FixedAssetName)}">{{item.FixedAssetName | formatNoContent}}</td>
                        <td v-bind:title = "[formatText(item.DepartmentAssetName) ? item.DepartmentAssetName : '']" v-bind:class = "{'format-text' :  formatText(item.DepartmentAssetName)}">{{item.DepartmentAssetName | formatNoContent}}</td>
                        <td class="align-right">{{item.Cost | formatMoney | formatNoContent}}</td>
                        <td class="align-right">{{item.FixedAssetCummulative | formatMoney | formatNoContent}}</td>
                        <td class="align-right">{{item.FixedAssetRest | formatMoney |formatNoContent}}</td>
                    </tr>
                    <MLoaderGrid v-show = "isShowLoaderDetailGrid" />
                </tbody>
            </table>
        </div>
    </div>
</template>
<script>

/* Import Component */
// import Paging from './UsedFixedAssetPaging.vue'
import MLoaderGrid from '../../components/base/MLoaderGrid.vue'
import MPaging from '../../components/base/MPaging.vue'

/*Import Javascript */
import {CommonFunction} from '../../assets/js/common/commonfunction'
import { Enum } from '../../assets/js/common/enum'

export default {

    components : {
        // Paging,
        MPaging,
        MLoaderGrid
    },

    props : {
        totalPage : {
            type : Number
        },
        totalRecord : {
            type : Number
        },
        totalCost : {
            type : Number
        },
        listFixedAssetClickedRowMaster : {
            typeof : Array
        },
        isShowLoaderDetailGrid : {
            typeof : Boolean
        }
    },

    data() {
        return {
            //Tọa độ y khi resize
            yResize : null,
            gridDetailY : 0,
            tooltipTextZoomGrid : "Phóng to"
        }
    },

    mounted() {
        document.addEventListener("mouseup", this.mouseUpResizeGridDetail);
        this.gridDetailY = document.querySelector(".grid-detail").clientHeight;

        //Căn chỉnh kích thước cho cột summary
        var gridDetail = document.querySelector('.grid-detail')
        var gridMaster = document.querySelector('.grid-master')
        var columnDescribeMaster = gridMaster.querySelector('.column-describe')
        var columnCostMaster = gridMaster.querySelector('.total-cost-master')
        var summaryCost = gridDetail.querySelector('.total-cost-master')
        summaryCost.style.width = columnCostMaster.clientWidth + "px"
        summaryCost.style.right = columnDescribeMaster.clientWidth + Enum.PaddingTdGrid + "px"
    },

    methods: {
        mouseUpResizeGridDetail : function()
        {
            document.removeEventListener("mousemove", this.ResizeGridDetail);
        },

        mouseDownResizeGridDetail : function(event)
        {
            this.yResize = event.clientY
            document.addEventListener("mousemove", this.ResizeGridDetail);
        },

        ResizeGridDetail : function(e)
        {   
            var gridDetail = document.querySelector('.grid-detail')
            var heightGridFixedAsset = document.querySelector('.grid-used-fixed-assets').clientHeight
            //kích thước dịch chuyển so với lúc ban đầu chưa dịch chuyển ( lúc nhấn chuột cho đến lúc di chuyển rồi nhả chuột ra)
            var dy = this.yResize - e.y
            this.yResize = e.y
            var currentHeight = (parseInt(getComputedStyle(gridDetail, '').height) + dy) 
            console.log(currentHeight)
            if(87 -  currentHeight > 0)
            {
                gridDetail.style.height = 87 + "px";
            }
            else if(parseInt(heightGridFixedAsset) - currentHeight < 0)
            { 
                gridDetail.style.height = parseInt(heightGridFixedAsset) + "px";
            }
            else{
                gridDetail.style.height = (parseInt(getComputedStyle(gridDetail, '').height) + dy) + "px";
            }
        },

        clickZoomGridDetail : function()
        {
            var gridVoucher = document.querySelector('.grid-used-fixed-assets')
            var gridDetail = document.querySelector('.grid-detail')
            var filterBar = document.querySelector('.filter-bar')
            if(gridVoucher.classList.contains('zoom-detail-grid'))
            {
                filterBar.classList.remove('hide-filter-bar')
                gridVoucher.classList.remove('zoom-detail-grid')
                gridDetail.style.height = parseInt(this.gridDetailY) + "px"
                this.tooltipTextZoomGrid = "Phóng to"
            }
            else{
                filterBar.classList.add('hide-filter-bar')
                gridVoucher.classList.add('zoom-detail-grid')        
                gridDetail.style.height = parseInt(gridVoucher.clientHeight + 50) + "px"
                this.tooltipTextZoomGrid = "Thu nhỏ"
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
        }
    }

}
</script>
<style lang="">
    
</style>