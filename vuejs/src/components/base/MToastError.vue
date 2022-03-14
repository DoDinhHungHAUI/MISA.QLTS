<template lang="">
    <div class="toast-model">
        <div class="m-toast danger-toast">
            <div class="icon-toast toast-icon-danger">
                <i class="fas fa-exclamation-triangle"></i>
            </div>
            <div class="content-toast">{{messageError}}</div>
            <div class="close-toast icon-toast-danger">
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
        messageError : {
            type : String
        }
    }
}
</script>
<style lang="">
    
</style>