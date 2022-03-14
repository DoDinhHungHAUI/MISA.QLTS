<template lang="">
    <div class="toast-model">
        <div class="m-toast success-toast">
            <div class="icon-toast toast-icon-success">
                <i class="fas fa-check"></i>
            </div>
            <div class="content-toast">{{messageSuccess}}</div>
            <!-- <div class="close-toast icon-toast-success">
                <button type="button">
                    <i class="fas fa-times"></i>
                </button>
            </div> -->
        </div>
    </div>
</template>
<script>
export default {
    props : ['messageSuccess']
}
</script>
<style lang="">
    
</style>