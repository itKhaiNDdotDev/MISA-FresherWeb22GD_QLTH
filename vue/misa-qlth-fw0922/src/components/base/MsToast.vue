<template lang="">
    <div class="m-toast" :class="{toastFail:true, toastSuccess:toastType==1, toastWarning:toastType==2, toastInfo:toastType==3}">
        <div v-if="toastType==1" class="toast-icon icon-toast-success"></div>
        <div v-else-if="toastType==2" class="toast-icon icon-toast-warning"></div>
        <div v-else-if="toastType==3" class="toast-icon icon-toast-info"></div>
        <div v-else class="toast-icon icon-toast-fail"></div>
        <div class="toast-title">{{toastTitle}}</div>
        <div class="toast-content">{{toastMes}}</div>
    </div>
</template>
<script>
export default {
  name: "MsToast",
  
  props: ["toastType", "toastMes"],
  data() {
    return {
      toastTitle: "Thành công",
    };
  },

  created() {
    if (this.toastType == 1) {
      this.toastTitle = "Thành công";
    } else if (this.toastType == 2) {
        this.toastTitle = "Cảnh báo";
    } else if (this.toastType == 3) {
        this.toastTitle = "Thông báo";
    } else {
        this.toastTitle = "Lỗi";
    }
  },
};
</script>

<style scoped>
.m-toast {
  display: grid !important;
}
</style>