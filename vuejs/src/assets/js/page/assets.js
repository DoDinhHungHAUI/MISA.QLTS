$(document).ready(function(){
    let assets =  new AssetsJS();
})
 
/**
 * Class quản lý các sự kiện cho trang Employee
 * CreatedBy : DDHUNG (11/11/2021)
**/
class AssetsJS extends BaseJS{

    //#region constructor
    constructor(){
        super();
        this.initEvents();
        this.loadDataCombobox();
        this.initEventAssetsJS();
    }
    //#endregion

    initEventAssetsJS()
    {
        var me = this;

        //Sự kiện click  khi thêm mới
        $('#add-assets').click(me.btnAddClick.bind(this));

        //Sự kiện khi click button [Hủy] trên dialog
        $('#btn-close-popup').click(function(){
            CommonJS.closeFormPopup();
        })

        //Sự kiện khi click nút close (x) trên dialog
        $('#close-popup').click(function(){
            CommonJS.closeFormPopup();
        })

        //Sự kiện click vào item của combobox trên toolbar phục vụ cho việc lọc dữ liệu
        $('.m-toolbar-combobox').on('click' , 'div.m-combobox-item' , me.itemComboboxToolbarSelected.bind(me))
    }

    /**
     * Method xử lý khi nhấn button [Thêm nhân viên]
     * CreatedBy : DDHung (14/10/2021)
     *
    */
    btnAddClick()
     {
        try {

            //Hiển thị dialog thông tin chi tiết
            CommonJS.showFormPopup();
           
        } catch (error) {
            console.log(error);
        }
    }

    /**
     *  Method xử lý khi selected combobox trên toolbar
     * CreatedBy : (15/11/2021)
     *
     */
     itemComboboxToolbarSelected(sender){

        try {
            var me = this;
            let itemSelected = $(sender.currentTarget);
            let text = itemSelected.text().replaceAll('\n' , '').trim();
            let value = itemSelected.attr('value');
            let parentElement = itemSelected.parent();
            parentElement.siblings('.m-combobox-input').val(text);

            //Gán value cho element combobox

            // itemSelected.parents('.m-combobox').attr('value', value);
            itemSelected.parents('.m-combobox').data('value', value);
            itemSelected.parents('.m-combobox').attr('value', text);

            //Chuyển đổi item được selected
            var cbItems = parentElement.children();
            for (const itemchild of cbItems) {
                itemchild.classList.remove('m-item-selected');
                var iconRemove = itemchild.children;
                if(iconRemove[0])
                {
                    itemchild.removeChild(iconRemove[0]);
                }
            }

            let comboboxButton = parentElement.siblings('.m-combobox-button');
            comboboxButton.empty();
            comboboxButton.html("<i class='fas fa-chevron-down'></i>");

            $(sender.currentTarget).addClass('m-item-selected');
            $(sender.currentTarget).html("<i class='fas fa-check'></i>" + text);

            parentElement.toggleClass('m-hidden-dropdown');
        } catch (error) {
            console.length(error);
        }

        //me.filterDataCombobox();
    }


}