<template lang="">
    <div class="header">
        <div class="title-header">Trường Tiểu học Hải Đình</div>
        <div class="logouser-header">
            <!-- <div class="title-logouser-header">Sở tài chính</div> -->
            <!-- <div class="header-current-year">
                Năm
                <div class="current-year">2021</div>
                <div class="icon-current-year icon-top"></div>
                <div class="icon-current-year icon-down"></div>
            </div> -->
            <div class="icon-header icon-notify wrap-btnfilter-tooltip">
                <span class="filterbar-tooltiptext showTooltip-filterbar">Thông báo</span>
            </div>
            <div class="icon-header icon-toggle wrap-btnfilter-tooltip" v-on:click = "onClickToggleNavbar">
                <span class="filterbar-tooltiptext showTooltip-filterbar tooltiptext-togglemenu">Đóng mở menu</span>
            </div>
            <div class="icon-header icon-question-mark"></div>
            <div class="icon-header icon-user wrap-btnfilter-tooltip">
                <span class="filterbar-tooltiptext showTooltip-filterbar">Đăng nhập</span>
            </div>
            <div class="icon-header icon-down"></div>
        </div>
    </div>
</template>
<script>
export default {
    methods: {
        onClickToggleNavbar : function()
        {
            this.$emit("onClickToggleNavbar")
        }
    },
}
</script>
<style lang="">
    
</style>