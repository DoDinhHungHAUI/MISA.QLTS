<template lang="">
    <div class="dialog-confirm-model">
        <div class="wrap-dialog-confirm"></div>
        <div class="m-dialog-confirm">
            
            <div class="content-dialog-confirm">
                <div class="icon-dialog-confirm"> 
                    <i class="fas fa-exclamation-triangle"></i>
                </div>
                <div class="text-dialog-confirm" v-html = "messageConfirm"></div>
            </div>
            <div class="button-dialog-confirm" v-show = "isCancelSaveConfirm">
                <button class="m-btn-cancel btn-out-line" v-on:click = "btnNotHideDialogConfirm">Không</button>
                <button class="m-btn m-btn-delete btn-delete-record" v-on:click = "btnHideDialogConfirm">Hủy bỏ</button>
            </div>
            <div class="button-dialog-confirm" v-show = "isSaveConfirm">
                <button class="m-btn-cancel btn-out-line" v-on:click = "btnHideDialogConfirm">Hủy bỏ</button>
                <button class="m-btn-cancel btn-sub" v-on:click = "btnNotHideDialogConfirm">Không lưu</button>
                <button class="m-btn m-btn-delete btn-delete-record" v-on:click = "btnSaveInfoConfirm">Lưu</button>
            </div>
            <div class="button-dialog-confirm" v-show = "isDeleteConfirm">
                <button class="m-btn-cancel btn-out-line" v-on:click = "btnNotHideDialogConfirm">Không</button>
                <button class="m-btn m-btn-delete btn-delete-record" v-on:click = "btnConfirmDelete">Xóa</button>
            </div>
        </div>
    </div>
</template>
<script>


export default {
    props : {
        messageConfirm : {
            type : String
        },
        isSaveConfirm : {
            type:Boolean
        },
        isCancelSaveConfirm : {
            type : Boolean
        },
        isDeleteConfirm : {
            type : Boolean
        }
    },
    methods: {

        /**
         * Method xác nhận việc đóng popup thêm/sửa tài sản
         * CreatedBy : DDHung (18/11/2021)
        */
        btnHideDialogConfirm :function()
        {
            this.$emit("btnHideDialogConfirm")
        },

        /**
         * Method xác nhận việc hủy đóng popup thêm/sửa tài sản
         * CreatedBy : DDHung (18/11/2021)
        */
        btnNotHideDialogConfirm : function()
        {
            this.$emit("btnNotHideDialogConfirm")
        },

        /**
         * Method dùng để lưu thông tin khi sửa 1 bản ghi tài sản
         * CreatedBy : DDHung (18/11/2021)
        */
        btnSaveInfoConfirm : function()
        {
            this.$emit("btnSaveInfoConfirm")
        },

        /**
         * Method dùng để xóa tài sản khi người dùng xác nhận xóa
         * CreatedBy : DDHung (19/11/2021)
        */
        btnConfirmDelete : function()
        {
            this.$emit("btnConfirmDelete");
        }
    },
}
</script>
<style lang="">
    
</style>