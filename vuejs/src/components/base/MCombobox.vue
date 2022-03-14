<template lang="">
    <div class="m-combobox m-combobox-popup m-combobox-loadData" v-bind:class="{ 'comboboxRequired': required }" v-bind:nameId = "idItem" v-bind:fieldNameItem = "nameItem" v-bind:tableNameItem = "tableNameItem" v-bind:nameToValidate = "nameToValidate">
        <input type = "text" @keyup = "keyMonitor" v-model = "inputSearch" class="m-combobox-input input-text"  v-bind:tabindex = "tabindex" v-bind:placeholder = "mPlaceholder" value = "" @focus = "onFocusToggleItemCombobox" @blur = "onHideItemCombobox" />
        <button class="m-combobox-button" v-on:click = "btnButtonCombobox($event)" ref="buttonCombobox">
            <i class="fas fa-chevron-down"></i>
        </button>
        <div class="m-combobox-select m-hidden-dropdown" ref = "listTtemCombobox">
           <div class="m-combobox-item" v-bind:value = "item[idItem]" v-on:mousedown = "itemComboboxClick(item[idItem] , item[codeItem] , item[nameItem] , $event)" v-for = "item in filteredOptions" :key="item[idItem]">
                {{item[codeItem]}}
            </div>
        </div>
    </div>
</template>
<script>
/* Import Javascript */
import { CommonFunction } from '../../assets/js/common/commonfunction';
import {Resource} from '../../assets/js/common/resource'
import {Common} from '../../assets/js/common/common'
import { Enum } from '../../assets/js/common/enum';

export default {
    data() {
        return {
            searchCombobox : '',
            filteredOptions : [],
            timeout: null,
            selected : {},
            isEventCombobox : Enum.ComboboxModel.Default
        }
    },
    computed: {
        /**
         * Lấy và Gán dữ liệu khi nhập vào ô input của combobox phục vụ cho việc lọc dữ liệu
         * CreatedBy : DDHung (27/11/2021) 
        */
        inputSearch : {
            get(){
                return this.searchCombobox
            },
            set(val){
                if (this.timeout) clearTimeout(this.timeout)
                this.timeout = setTimeout(() => {
                    this.searchCombobox = val
                }, 150)
            }
        }
    },
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
        codeItem : {
            type : String
        },
        mPlaceholder : {
            type : String
        },
        required : {
            type : Boolean
        },
        tableNameItem : {
            type:String
        },
        nameToValidate : {
            type : String
        },
        tabindex :{
            type : Number
        },     
        valueIdItem : {
            type : String
        },
        isAddOrEdit : {
            type : Number
        } 
    },
    created() {
        this.filteredOptions = this.dataCombobox;
        if(this.isAddOrEdit == Enum.FormModel.Edit || this.isAddOrEdit == Enum.FormModel.Replication)
        {
            var dataItemSelectedCombobox = this.dataCombobox.find(val => val[this.idItem] == this.valueIdItem);
            this.selected.id = dataItemSelectedCombobox[this.idItem];
            this.selected.code = dataItemSelectedCombobox[this.codeItem];
            this.selected.name = dataItemSelectedCombobox[this.nameItem];
        }
    },  
    methods: {
        
        /**
         * Thực hiện việc Autocomplete khi focus vào combobox
         * CreatedBy : DDHung (16/11/2021) 
        */
        onFocusToggleItemCombobox : function()
        {
            // if(this.selected.id)
            // {
            //     this.searchCombobox = this.selected.code;
            // }
            this.filteredOptions = this.dataCombobox;
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
            this.$emit('btnButtonCombobox' , event);
        },

        /**
         * Method xử lý việc select giá trị của combobox
         * CreatedBy : DDHung (16/11/2021)
        */
        itemComboboxClick : function(id ,code ,name , event){
            this.isEventCombobox = Enum.ComboboxModel.MouseDown;
            this.selected.id = id;
            this.selected.code = code;
            this.selected.name = name;
            this.$emit('itemComboboxClick' , id , code,name ,event);
        },

        /**
         * Method xử lý sự kiện khi blur khỏi combobox
         * CreatedBy : DDHung 25/11/2021
        */
        onHideItemCombobox : function()
        {
            const cbSelected = this.$refs.listTtemCombobox;
            if(this.isEventCombobox == Enum.ComboboxModel.MouseDown)
            {
                this.isEventCombobox = Enum.ComboboxModel.Default;
            }
            else{
                
                if(!this.selected.id)
                {
                    this.selected = {};
                    var cbRequired = document.querySelector(`[fieldNameItem="${this.nameItem}"]`);
                    if(cbRequired)
                    {
                        cbRequired.classList.add('border-red');
                        var toolTip = cbRequired.parentElement.querySelector(".tooltiptext");
                        toolTip.classList.add("showTooltip")
                        toolTip.innerText = Resource[Common.LanguageCode].Validate.Required(this.nameToValidate);
                        // cbRequired.setAttribute('title' , Resource[Common.LanguageCode].Validate.Required(this.nameToValidate));
                        cbRequired.setAttribute('validate' , false)
                        this.checkValidate = false;
                    } 
                    //Đổi lại icon tại nút button của combobox
                    var buttonCb = cbRequired.getElementsByTagName("button");
                    buttonCb[0].innerHTML = Resource.Icon.FaChevronDown.toString();
                    
                }else{
                    var itemSelected = cbSelected.querySelectorAll(`div[value="${this.selected.id}"]`)[0];
                    this.itemComboboxClick(this.selected.id , this.selected.code,this.selected.name ,itemSelected);
                    this.isEventCombobox = Enum.ComboboxModel.Default;
                }
            }
            if(cbSelected.classList.contains("m-hidden-dropdown") == false)
            {
                cbSelected.classList.add("m-hidden-dropdown");
            }
        },

        /**
         * Lọc dữ liệu trên combobox khi bấm enter 
         * CreatedBy : DDHung (27/11/2021)
        */
        keyMonitor: function(event) {
            if (event.key === "Enter" && this.filteredOptions[0])
            {
                var filteredOptions =  this.filteredOptions[0];
                var id = filteredOptions[this.idItem];
                var code = filteredOptions[this.codeItem];
                var name = filteredOptions[this.nameItem];
                var cbSelected = this.$refs.listTtemCombobox;
                var itemSelected = cbSelected.querySelectorAll(`div[value="${id}"]`)[0];
                this.itemComboboxClick(id,code,name,itemSelected);
            }
        }
    },
    watch : {

        /**
         * Mỗi khi tìm kiếm trên combobox thì sẽ lọc ra các item thỏa mãn điều kiện tìm kiếm đó.
         * CreatedBy : DDHung (26/11/2021)
        */
        searchCombobox : function(val)
        {
            //lọc dữ liệu trên combobox
            this.filteredOptions = this.dataCombobox.filter(res => {
                return CommonFunction.removeVietnameseTones(res[this.codeItem]).toLocaleLowerCase().includes(CommonFunction.removeVietnameseTones(val).toLocaleLowerCase());
            })
            if(this.filteredOptions[0])
            {
                var filteredOptions =  this.filteredOptions[0];
                this.selected.id = filteredOptions[this.idItem];
                this.selected.code = filteredOptions[this.codeItem];
                this.selected.name = filteredOptions[this.nameItem];
            }
            else{
                this.selected = {}
            }
        }
    }
}

</script>
<style>
    
</style>