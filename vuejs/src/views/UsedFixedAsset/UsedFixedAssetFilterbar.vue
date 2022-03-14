<template lang="">
     <div class="filter-bar">
        <div class="wrap-filter">
            <div class="filter-left">
                <div>
                    <input type="text" class="icon-search input-search" id="input-search" placeholder="Tìm kiếm " style="width : 200px" v-model = "inputSearch" />
                </div>
            </div>
            <div class="filter-right">
                <div class="m-icon-print wrap-icongrid-tooltip">
                    <span class="grid-tooltiptext showTooltip-filterbar tooltiptext-replication">In</span>
                </div>
                <div class="m-icon-delete wrap-icongrid-tooltip" v-on:click = "btnIconDeleteFilterBar">
                    <span class="grid-tooltiptext showTooltip-filterbar tooltiptext-replication">Xóa</span>
                </div>
                <div class="m-icon-info wrap-icongrid-tooltip"></div>
            </div>
        </div>
    </div>
</template>
<script>
export default {
    data() {
        return {
            timeout: null,
            txtsearch : '',
        }
    },

    methods: {
        btnIconDeleteFilterBar : function()
        {
            this.$emit("btnIconDeleteFilterBar")
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