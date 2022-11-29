<template lang="">
    <div class="m-dialog">
        <div class="dialog__header">
            Thông báo
        </div>
        <div class="dialog__content">
            {{dialogMessage}}
        </div>
        <div class="dialog__footer">
            <button @click="onClickClose" class="m-button m-btn-style2">Đóng</button>
            <button @click="onClickBtn2" v-if="btn2Show" class="m-button m-btn-style2">{{btn2Text}}</button>
            <button @click="onClickConfirm" class="m-button m-btn-style1">Đồng ý</button>
        </div>
        <button title="Đóng" @click="onClickClose" class="m-icon m-icon-24 button-icon icon-close" id="closeDialog"></button>
    </div>
</template>
<script>
export default {
  name: "MsDialog",
  props: ["isShow", "dialogMessage", "btn2Text", "btn2Show"],

  methods: {
    /**
     * Gọi emit đến Table để đóng Dialog
     * Author: KhaiND (23/11/2022)
     */
    onClickClose() {
        this.$emit("onClose");
    },

    /**
     * CConfirm đồng ý thì gọi emit để xóa cán bộ, giáo viên
     * Author: KhaiND (23/11/2022)
     */
    onClickConfirm() {
        this.$emit("onConfirm");
    },

    /**
     * Sự kiện bấm vào nút thứ 2 (tạm thời chính là nút ở giữa nếu có)
     * Author: KhaiND (28/11/2022)
     */
    onClickBtn2() {
        this.$emit("onClickBtn2");
    }
  },
};
</script>

<style>
.showD {
  display: block !important;
}
</style>