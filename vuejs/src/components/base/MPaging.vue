<template lang="">
    <div v-bind:class = "customClass"><!-- class="paging"  -->
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
          
            <v-pagination v-model = "currentPage"
                  :page-count = "totalPage"
                  :classes = "bootstrapPaginationClasses"
                  :labels = "paginationAnchorTexts"></v-pagination>
        </div>
    </div>
</template>
<script>

/* Important Component */
import vPagination from 'vue-plain-pagination'

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
        }
    },
    props : {
        totalPage : {
            type : Number
        },
        totalRecord : {
            type:Number
        },
        customClass : {
            type : String
        }
    },
    
    methods: {

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
}
</script>
<style lang="">
    
</style>