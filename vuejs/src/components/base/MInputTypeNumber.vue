<template lang="">
    <div>
        <input 
            type="text" class="input-popup-left input-typenumber-popup input-text input-text" 
            @blur = "validateFieldRequired($event)" 
            v-bind:fieldName = "fieldName" v-model = "fixedAsset[fieldName]"
            v-bind:nameToValidate = "nameToValidate"
            v-bind:tabindex = "tabindex"
            @keydown="restricCharacter($event)"
            required
        />
        <div class="icon-top" v-on:click = "augmentNumberClick( fieldName, fixedAsset[fieldName])"></div>
        <div class="icon-down" v-on:click = "reduceNumberClick( fieldName, fixedAsset[fieldName])"></div>
    </div>
</template>
<script>
export default {
    props : {
        fieldName : {
            type : String
        },
        fixedAsset : {
            type : Object
        },
        nameToValidate : {
            type : String
        },
        tabindex : {
            type:Number
        }
    },
    methods: {

        /**
         * Method thực hiện validate các trường bắt buộc nhập
         * CreatedBy : DDHung (16/11/2021)
        */
        validateFieldRequired : function(event){
            this.$emit('validateFieldRequired' , event);
        },

        /**
         * Method thực hiện việc tăng giá trị của ô input lên 1 khi click vào tăng giá trị tại ô input đó
         * createdBy : DDHung (17/11/2021)
        */
        augmentNumberClick : function(fieldName , value)
        {
            this.$emit('augmentNumberClick' , fieldName , value);
        },

        /**
         * Method thực hiện việc giảm giá trị của ô input lên 1 khi click vào giảm giá trị tại ô input đó
         * createdBy : DDHung (17/11/2021)
        */
        reduceNumberClick : function(fieldName , value)
        {
            this.$emit('reduceNumberClick' , fieldName , value);
        },

        /**
         * Method hạn chế nhập ký tự chữ vào ô input chỉ được phép nhập số
         * CreatedBy : DDHung (17/11/2021)
         */
        restricCharacter : function(event)
        {
            this.$emit("restricCharacter" , event);
        }
    },    
}
</script>

<style lang="">
    
</style>