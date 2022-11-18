<template lang="">
    <div class="m-toast toast-message-success" :class="{showToast:isShow}">
        <div v-if="toastType==1" class="toast-icon icon-toast-success"></div>
        <div v-else-if="toastType==2" class="toast-icon icon-toast-warning"></div>
        <div v-else-if="toastType==3" class="toast-icon icon-toast-info"></div>
        <div v-else class="toast-icon icon-toast-fail"></div>
        <div class="toast-title">Thành công</div>
        <div class="toast-content">{{toastMes}}</div>
    </div>
</template>
<script>
export default {
    name: "MsToast",
    props: ["isShow", "toastType", "toastMes"],
    setup() {
        const RES_OK = 1;
        const RES_WARNING = 2;
        const RES_INFO = 3;
        console.log(RES_OK+RES_WARNING+RES_INFO);
    }
}
</script>

<style scoped>
    .m-toast.showToast {
        display: grid !important;
    }
</style>