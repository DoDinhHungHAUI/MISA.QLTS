<template lang="">
    <div class="dialog-confirm-model">
        <div class="wrap-dialog-confirm"></div>
        <div class="m-dialog-confirm">

            <div class="content-dialog-confirm">
                <div class="icon-dialog-confirm">
                    <i class="fas fa-exclamation-triangle"></i>
                </div>
                <div class="text-dialog-confirm" v-html = "messageNotValid"></div>

                <!-- <div v-show = "isShowContextNotValid" class="text-dialog-confirm">{{messageDialogValidate}} <span class = "list-novalid-required">{{lisFieldNotValidRequired}}</span></div> -->
                <!-- <div v-show = "isShowContextMSG" class="text-dialog-confirm"><span class = "list-novalid-required">{{lisFieldNotValidRequired}}</span></div> -->
            </div>
            <div class="button-dialog-confirm">
                <button class="m-btn m-btn-delete btn-delete-record" v-on:click = "btnHideDialogNotValid">Hủy bỏ</button>
            </div>
        </div>
    </div>
</template>
<script>
/* Import File Javascript */
// import { Resource } from '../../assets/js/common/resource'
// import {Common} from '../../assets/js/common/common'

export default {
    data() {
        return {

        }
    },
    methods: {
        /**
         * Method xác nhận việc hủy đóng popup
         * CreatedBy : DDHung (18/11/2021)
        */
        btnHideDialogNotValid : function()
        {
            this.$emit("btnHideDialogNotValid")
        },
    },
    props : {
        messageNotValid : {
            type :String
        }
    },
}
</script>
<style lang="">

</style>