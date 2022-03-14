<template lang="">
    <div id="context-menu">
        <div class="item" v-on:click = "addFixedAssetContextMenu">
            <i class="fas fa-plus" aria-hidden="true"></i>
            {{textAdd}}
        </div>
        <div class="item" v-on:click = "deleteFixedAssetContextMenu">
            <i class="far fa-trash-alt"></i>
            {{textDelete}}
        </div>
        <div class="item" v-on:click = "editFixedAssetContextMenu">
            <i class="far fa-edit"></i>
            {{textEdit}}
        </div>
    </div>
</template>
<script>
export default {

    props : {
        textAdd : {
            typeof : String
        },
        textEdit : {
            typeof : String
        },
        textDelete : {
            typeof : String
        },
    },

    methods: {
        /**
         * Chức năng thêm một tài sản mới tại menucontext
         * CreatedBy : DDHung (24/11/2021)
        */
        addFixedAssetContextMenu : function()
        {
            this.$emit("addFixedAssetContextMenu")
        },

        /**
         * Chức năng xóa một tài sản tại menucontext
         * CreatedBy : DDHung (24/11/2021)
        */
        deleteFixedAssetContextMenu : function()
        {
            this.$emit("deleteFixedAssetContextMenu")
        },

        /**
         * chức năng sửa một tài sản tại menucontext
         * CreatedBy : DDHung (24/11/2021)
        */
        editFixedAssetContextMenu : function()
        {
            this.$emit("editFixedAssetContextMenu")
        }
    },
}
</script>
<style lang="">
    
</style>