<template lang="">
    <div class="content" v-bind:class = "{'content-collapse' : isCollapseMenu}">
        <router-view></router-view>
    </div>
</template>
<script>
export default {
    props : {
        isCollapseMenu : {
            type : Boolean
        }
    }
}
</script>
<style lang="">
    
</style>