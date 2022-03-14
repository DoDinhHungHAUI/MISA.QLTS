<template lang="">
     <div class="tool-bar">
        <div class="toolbar-left">
            <p>Ghi tăng tài sản</p>
        </div>
        <div class="toolbar-right">
            <button class="m-btn m-btn-default" id="add-usedFixedAsset" tabindex="1" v-on:click = "addVoucherClick">
                <div class="btn-add-text">Thêm</div>
            </button>
            <button class="m-btn-collapse-grid" v-on:click = "btnZoomMasterGrid">
                <div class="btn-icon-collapse"></div>
                <div class="btn-icon-down">
                    <i class="fas fa-chevron-down" aria-hidden="true"></i>
                </div>
            </button>
            <div class = "change-layout hide-change-layout">
                <div class = "change-layout-horizontal" v-on:click = "clickLayoutVertical">
                    <div class = "btn-icon-zoomout icon-layout-horizontal"></div>
                    <div class = "text-layout-horizontal">Layout dọc</div>
                </div>
                <div class = "change-layout-vertical" v-on:click = "clickLayoutHorizontal">
                    <div class = "btn-icon-collapse icon-layout-vertical"></div>
                    <div class = "text-layout-vertical">Layout ngang</div>
                </div>
            </div>
        </div>
    </div>
</template>
<script>
export default {
    methods: {

        /**
         * Method chọn chế độ layout ngang
         * CreatedBy : DDHung (24/12/2021)
        */
        clickLayoutHorizontal : function()
        {
            var toolBar = document.querySelector('.tool-bar')
            var gridUsedFixedAsset = document.querySelector('.grid-used-fixed-assets')
            if(gridUsedFixedAsset.classList.contains('zoom-master-grid'))
            {
                gridUsedFixedAsset.classList.remove('zoom-master-grid')
                let iconCollapseGridMaster = toolBar.querySelector('.m-btn-collapse-grid .btn-icon-zoomout');
                iconCollapseGridMaster.classList.remove('btn-icon-zoomout');
                iconCollapseGridMaster.classList.add('btn-icon-collapse')
            }

            var layoutChange = toolBar.querySelector('.change-layout')
            layoutChange.classList.add('hide-change-layout')
        },

        /**
         * Method chọn chế độ layout dọc
         * CreatedBy : DDHung (24/12/2021)
        */
        clickLayoutVertical : function()
        {
            var toolBar = document.querySelector('.tool-bar')
            var gridUsedFixedAsset = document.querySelector('.grid-used-fixed-assets')
            if(!gridUsedFixedAsset.classList.contains('zoom-master-grid'))
            {

                gridUsedFixedAsset.classList.add('zoom-master-grid')
                let iconCollapseGridMaster = toolBar.querySelector('.m-btn-collapse-grid .btn-icon-collapse');
                iconCollapseGridMaster.classList.remove('btn-icon-collapse');
                iconCollapseGridMaster.classList.add('btn-icon-zoomout')

                // let iconCollapseGridMaster = toolBar.querySelector('.m-btn-collapse-grid .btn-icon-zoomout');
                // iconCollapseGridMaster.classList.remove('btn-icon-zoomout');
                // iconCollapseGridMaster.classList.add('btn-icon-collapse')
            }

            var layoutChange = toolBar.querySelector('.change-layout')
            layoutChange.classList.add('hide-change-layout')
        },

        /**
         * Method xử lý việc phóng to , thu nhỏ của Grid Master (Table chứng từ) 
         * CreatedBy : DDHung (16/12/2021)
        */
        btnZoomMasterGrid : function()
        {
            // var gridUsedFixedAsset = document.querySelector('.grid-used-fixed-assets')
            var toolBar = document.querySelector('.tool-bar')
            var layoutChange = toolBar.querySelector('.change-layout')
            layoutChange.classList.toggle('hide-change-layout')
            // gridUsedFixedAsset.classList.toggle('zoom-master-grid')
            // if(gridUsedFixedAsset.classList.contains('zoom-master-grid'))
            // {
            //     let iconCollapseGridMaster = toolBar.querySelector('.btn-icon-collapse');
            //     iconCollapseGridMaster.classList.remove('btn-icon-collapse');
            //     iconCollapseGridMaster.classList.add('btn-icon-zoomout')
            // }
            // else{
            //     let iconCollapseGridMaster = toolBar.querySelector('.btn-icon-zoomout');
            //     iconCollapseGridMaster.classList.remove('btn-icon-zoomout');
            //     iconCollapseGridMaster.classList.add('btn-icon-collapse')
            // }
        },

        /**
         * Method xử lý khi click vào button thêm 
         * createdBy : DDHung (16/12/2021)
        */
        addVoucherClick : function()
        {
            this.$emit('showDialogVoucher')
            this.$emit('setStatusActionVoucher')
        }
    },
}
</script>
<style lang="">
    
</style>