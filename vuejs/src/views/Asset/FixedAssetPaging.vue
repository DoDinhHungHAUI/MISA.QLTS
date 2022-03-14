<template lang="">
    <div class="paging">
        <div class="info-page">Tổng số : <span class="total-record">{{totalRecord}}</span> bản ghi</div>

        <div class="m-combobox m-combobox-paging" >
            <input type="text" class="m-combobox-input m-combobox-input-paging" value="40" />
            <button class="m-combobox-button m-combobox-button-paging">
                
            </button>
            <div class="m-combobox-select">
                <div class="m-combobox-item">
                    20
                </div>
                <div class="m-combobox-item">
                    30
                </div>
                <div class="m-combobox-item">
                    40
                </div>
                <div class="m-combobox-item">
                    50
                </div>
            </div>
        </div>
        <div class="list-page">
          
             <v-pagination v-model="currentPage"
                  :page-count="totalPage"
                  :classes="bootstrapPaginationClasses"
                  :labels="paginationAnchorTexts"></v-pagination>
        </div>
        <div class="total-value-column">
            <div class="total-quantity">
                <span>{{totalQuantity | formatNumber}}</span>
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
</template>
<script>

/* Import Javascript */
import {Resource} from '../../assets/js/common/resource'

/* Important Component */
import vPagination from 'vue-plain-pagination'
import { Enum } from '../../assets/js/common/enum'

export default {
    components : {
        vPagination
    },
    data() {
        return {
            bootstrapPaginationClasses: {
                ul: 'pagination',
                li: '',
                liActive: 'active-current-page',
                liDisable: 'disabled',
                button: 'page-link',
                
            },
            paginationAnchorTexts: {
                prev: '.',
                next: '.',
            },
            currentPage : 1,
            //Biến dùng cho việc tính toán width của các cột summary tương ứng các cột trên grid (tính toán 1 lần duy nhất cho việc render lần đầu tiên)
            isCalculateWidthSummary: true,
        }
    },
    props : {
        totalPage : {
            type : Number
        },
        totalRecord : {
            type:Number
        },
        totalCost : {
            type :Number
        },
        totalQuantity : {
            type : Number
        },
        totalFixedAssetCummulative : {
            type : Number
        },
        totalFixedAssetRest : {
            type :Number
        },
    },

    updated() {

        if(this.isCalculateWidthSummary == true)
        {
            var grid = document.querySelector('.grid-fixed-assets')
            var quantityGrid = grid.querySelector('.align-right-quantity')
            var costGrid = grid.querySelector('.cost-grid')
            var accumulatedGrid = grid.querySelector('.accumulated-grid')
            var esidualValueGrid = grid.querySelector('.esidual-value-grid')
            var statusGrid = grid.querySelector('.status-grid')

            var summary = document.querySelector('.total-value-column')
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

    methods : {

        /**
         * Method xử lý khi muốn quay về trang trước đó
         * CreatedBy : DDHung(19/11/2021)
        */
        btnPrevPage : function(){
            this.currentPage = this.currentPage > 1 ? Number(this.currentPage) - 1 : this.currentPage;
            console.log(this.currentPage);
        },

        /**
         * Method xử lý khi muốn next sang trang tiếp theo
         * CreatedBy : DDHung(19/11/2021)
        */
        btnNextPage : function(){
            this.currentPage = this.currentPage < Math.ceil(this.totalRecord/(Resource.SearchPaging.PageSize)) ? Number(this.currentPage) + 1 : this.currentPage;
            console.log(this.currentPage);
        },

        /**
         * Method xử lý khi click vô page (Chuyển sang trang khác)
         * CreatedBy : DDHung(19/11/2021)
        */
        btnPageClick : function(numberOfPage)
        {
            this.currentPage = numberOfPage;
            console.log(this.currentPage);
        },

        /**
         * Method xử lý việc active vào trang hiện tại
         * CreatedBy : DDHung (19/11/2021)
         */
        isActivePaging : function(value){
            if(this.currentPage == value){
                return true
            }
            return false
        }
    },
    watch : {
        currentPage: function (currentPage) {
            this.$emit("btnPaging" , currentPage)
        },
    },
    filters : {

        /**
         * Hàm thực hiện format money
         * CreatedBy : DDHung (16/11/2021)
        */
        formatMoney : function(value){
            if (value) {
                return Number(value).toFixed(0).replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1.");
            }
            return "";
        },

        /**
         * Hàm thực hiện format number (10000 -> 10.000)
         * CreatedBy : DDHung (16/11/2021)
        */
        formatNumber : function(value)
        {
            if(value)
            {
                return  value.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ".");
            }
            return "";
        }
    }
}
</script>
<style lang="">
    
</style>