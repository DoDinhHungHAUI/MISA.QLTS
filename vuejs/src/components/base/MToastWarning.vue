<template lang="">
    <div class="toast-model">
        <div class="m-toast warning-toast">
            <div class="icon-toast toast-icon-warning">
                <i class="fas fa-exclamation"></i>
            </div>
            <div class="content-toast">{{messageWarning}}</div>
            <div class="close-toast icon-toast-warning">
                <button type="button">
                    <i class="fas fa-times"></i>
                </button>
            </div>
        </div>
    </div>
</template>
<script>
export default {    
    props : {
        messageWarning : {
            type : String
        }
    }
}
</script>
<style lang="">
    
</style>