 <template lang="">
    <div class="m-combobox m-toolbar-combobox m-combobox-loadData" v-bind:class="{ 'm-custom-combobox': isCustomCombobox }" v-bind:idCheckOriginFunding = "idCheckOriginFunding" v-bind:fieldName = "idItem" v-bind:name = "nameItem" nameUrl = "FixedAssetCategories" id="cbFixedAssetCategory">
        <input type="text" class="m-combobox-input icon-combobox" v-bind:placeholder = "mPlaceholder" value="" @focus = "onFocusToggleItemCombobox" /> <!-- @blur="onHideItemCombobox" -->
        <button class="m-combobox-button m-combobox-item" v-on:click = "btnButtonCombobox($event)" ref = "buttonCombobox">
            <i class="fas fa-chevron-down"></i>
        </button>
        <div class="m-combobox-select m-hidden-dropdown" ref = "listTtemCombobox">
           <div class="m-combobox-item" v-bind:value = "item[idItem]" v-on:click = "itemComboboxClick(item[idItem] , item[nameItem] , $event)" v-for = "item in dataCombobox" :key = "item[idItem]">
                {{item[nameItem]}}
            </div>
        </div>
    </div>
</template>
<script>

/* Import File Javascript */
import {CommonFunction} from '../../assets/js/common/commonfunction'

export default {
    
    props : {
        dataCombobox : {
            type : Array
        },
        idItem : {
            type : String
        },
        nameItem : {
            type : String
        },
        mPlaceholder : {
            type : String
        },
        isCustomCombobox : {
            type : Boolean
        },
        idCheckOriginFunding : {
            type : Number
        },
        originFundingId : {
            type : String
        }
    },

    created() {
        if(!this.isCustomCombobox)
        {
            this.isCustomCombobox = false
        }
    },

    updated() {

        if(this.originFundingId)
        {
            var toolbarCombobox = document.querySelector("div[idCheckOriginFunding='" + this.idCheckOriginFunding + "']")
            var cbSelecte = toolbarCombobox.querySelector('.m-combobox-select')
            var itemCbSelected = cbSelecte.querySelector("div[value='" + this.originFundingId + "']")
            var textSelected = itemCbSelected.innerText
    
            var data = {
                name : textSelected.trim(),
                event : itemCbSelected,
                idCheckOriginFunding : this.idCheckOriginFunding
            }
            // this.$emit("selectedItemCombobox",data)
            CommonFunction.selectedItemCombobox(data)
            itemCbSelected.parentElement.classList.toggle("m-hidden-dropdown")
        }
    },

    methods: {

        /**
         * Thực hiện việc Autocomplete khi focus vào combobox
         * @param {*} cbSelected 
         * @param {*} buttonCombobox 
        */
        onFocusToggleItemCombobox : function()
        {
            var cbSelected = this.$refs.listTtemCombobox;
            var buttonCombobox = this.$refs.buttonCombobox;
            CommonFunction.onFocusToggleItemCombobox(cbSelected , buttonCombobox);
        },

        /**
         * Method xử lý việc show/Ẩn combobox
         * CreatedBy : DDHung (16/11/2021)
        */
        btnButtonCombobox : function(event)
        {
            // this.$emit('btnButtonCombobox' , event);
            CommonFunction.btnButtonCombobox(event);
        },

        /**
         * Method xử lý việc select giá trị của combobox
         * CreatedBy : DDHung (16/11/2021)
        */
        itemComboboxClick : function(id , name , event){
            this.$emit('itemComboboxClick' , id , name ,event);
        },
    },

    // watch: {
    //     originFundingId : function(id)
    //     {

    //     }
    // },
}
</script>
<style lang="">
    
</style>