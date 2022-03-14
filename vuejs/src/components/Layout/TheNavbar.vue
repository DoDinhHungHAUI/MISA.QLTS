<template lang="">
    <div class="navbar" v-bind:class = "{'navbar-collapse' : isCollapseMenu}">
        <div class="logo-site">
            <div class="logo-box">
                <div class="qlts-logo"></div>
            </div>
            <div class="qlts-text">MISA QLTS</div>
        </div>
        <div class="navbar-content">
            <div class="nav-item wrap-tooltip-navbar">
                <div class="nav-item-icon nav-item-icon-document-overview nav-item-icon-selected"></div>
                <div class="nav-item-text">Tổng quan</div>
                <span class="tooltiptext-navbar">Tổng quan</span>
            </div>
            <!-- <router-link class="nav-item wrap-tooltip-navbar" to="/fixedasset">
               <div class="nav-item-icon nav-item-icon-assets"></div>   
                <div class="nav-item-text">Tài sản</div>
                <div class="navbar-item-icon-drop">
                    <i class="fas fa-chevron-down"></i>
                </div>
                <span class="tooltiptext-navbar">Tài sản</span>
            </router-link> -->

            <div class = "wrap-module-fixed-asset">
                <router-link class="nav-item wrap-tooltip-navbar nav-fixed-asset"  to="/fixedasset">
                    <div class="nav-item-icon nav-item-icon-assets"></div>   
                    <div class="nav-item-text" @click = "showSubmenuFixedAsset($event)">Tài sản</div>
                    <span class="tooltiptext-navbar">Tài sản</span>
                </router-link>

                <div class = "submenu-module-fixedAsset">
                     <!-- <router-link class="nav-item wrap-tooltip-navbar"  to="/fixedasset">
                        <div class = "icon-right-item-submenu">
                            <i class="fas fa-angle-right"></i>
                        </div>
                        <div class="nav-item-text">Ghi tăng</div>
                    </router-link> -->
                    <div class = "item-submenu-fixedAsset">
                        <router-link class="nav-item wrap-tooltip-navbar"  to="/usedfixedasset">
                            <div class = "icon-right-item-submenu">
                                <i class="fas fa-angle-right"></i>
                            </div>
                            <div class="nav-item-text" @click = "handleSelectedSubMenu">Ghi tăng</div>
                        </router-link>
                        <!-- <div class="nav-item wrap-tooltip-navbar">
                            <div class = "icon-right-item-submenu">
                                <i class="fas fa-angle-right"></i>
                            </div>
                            <div class="nav-item-text">Ghi tăng</div>
                        </div>   -->
                        <div class="nav-item wrap-tooltip-navbar">
                            <div class = "icon-right-item-submenu">
                                <i class="fas fa-angle-right"></i>
                            </div>
                            <div class="nav-item-text">Thay đổi thông tin</div>
                        </div>  
                        <div class="nav-item wrap-tooltip-navbar">
                            <div class = "icon-right-item-submenu">
                                <i class="fas fa-angle-right"></i>
                            </div>
                            <div class="nav-item-text">Đánh giá lại</div>
                        </div>  
                        <div class="nav-item wrap-tooltip-navbar">
                            <div class = "icon-right-item-submenu">
                                <i class="fas fa-angle-right"></i>
                            </div>
                            <div class="nav-item-text">Tính hao mòn</div>
                        </div>  
                        <div class="nav-item wrap-tooltip-navbar">
                            <div class = "icon-right-item-submenu">
                                <i class="fas fa-angle-right"></i>
                            </div>
                            <div class="nav-item-text">Điều chuyển tài sản</div>
                        </div>  
                        <div class="nav-item wrap-tooltip-navbar">
                            <div class = "icon-right-item-submenu">
                                <i class="fas fa-angle-right"></i>
                            </div>
                            <div class="nav-item-text">Khi giảm</div>
                        </div>  
                        <div class="nav-item wrap-tooltip-navbar">
                            <div class = "icon-right-item-submenu">
                                <i class="fas fa-angle-right"></i>
                            </div>
                            <div class="nav-item-text">Kiểm kê</div>
                        </div>  
                        <div class="nav-item wrap-tooltip-navbar">
                            <div class = "icon-right-item-submenu">
                                <i class="fas fa-angle-right"></i>
                            </div>
                            <div class="nav-item-text">Khác</div>
                        </div>  
                    </div>
                </div>
            </div>


            <div class="nav-item wrap-tooltip-navbar">
                <div class="nav-item-icon nav-item-icon-assetsHTDB"></div>
                <div class="nav-item-text">Tài sản HT-ĐB</div>
                <div class="navbar-item-icon-drop">
                    <i class="fas fa-chevron-down"></i>
                </div>
                <span class="tooltiptext-navbar">Tài sản HT-ĐB</span>
            </div>
            <a class="nav-item wrap-tooltip-navbar">
                <div class="nav-item-icon nav-item-icon-tool"></div>
                <div class="nav-item-text">Công cụ dụng cụ</div>
                <div class="navbar-item-icon-drop">
                    <i class="fas fa-chevron-down"></i>
                </div>
                <span class="tooltiptext-navbar">Công cụ</span>
            </a>

            <a class="nav-item wrap-tooltip-navbar">
                <div class="nav-item-icon nav-item-icon-category"></div>
                <div class="nav-item-text">Danh mục</div>
                <span class="tooltiptext-navbar">Danh mục</span>
            </a>
            <div class="nav-item wrap-tooltip-navbar">
                <div class="nav-item-icon nav-item-icon-search"></div>
                <div class="nav-item-text">Tra cứu</div>
                <div class="navbar-item-icon-drop">
                    <i class="fas fa-chevron-down"></i>
                </div>
                <span class="tooltiptext-navbar">Tra cứu</span>
            </div>
            <div class="nav-item wrap-tooltip-navbar">
                <div class="nav-item-icon nav-item-icon-report"></div>
                <div class="nav-item-text">Báo cáo</div>
                <div class="navbar-item-icon-drop">
                    <i class="fas fa-chevron-down"></i>
                </div>
                <span class="tooltiptext-navbar">Báo cáo</span>
            </div>
        </div>
        <div class="bottom-navbar">
            <div class="collapse-menu" v-on:click = "onClickCollapseNavbar">
            
            </div>
        </div>
    </div>
</template>
<script>
export default {
    props : {
        isCollapseMenu : {
            type : Boolean
        }
    },
    methods: {
        /**
         * Method xử lý viêck đóng mở menu 
         * CreatedBy : DDHung (25/12/2021)
        */
        onClickCollapseNavbar : function()
        {
            this.$emit("onClickToggleNavbar")
        },
        
        /**
         * Method xử lý việc đóng mở submenu
         * CreatedBy : DDHung (25/12/2021) 
        */
        showSubmenuFixedAsset : function()
        {
            var navbar = document.querySelector('.navbar')
            var subMenuFixedAsset = navbar.querySelector('.submenu-module-fixedAsset')
            subMenuFixedAsset.classList.toggle('show-submenu-module-fixedAsset')
        },

        handleSelectedSubMenu : function(event)
        {
            var wrapSubmenu = event.target.parentElement.parentElement.parentElement.parentElement;
            var navFixedAsset = wrapSubmenu.querySelector('.nav-fixed-asset');
            navFixedAsset.style.background = "#1aa4c8"
            navFixedAsset.style.color = "#fff"
        }
    },
}
</script>
<style lang="">
    
</style>