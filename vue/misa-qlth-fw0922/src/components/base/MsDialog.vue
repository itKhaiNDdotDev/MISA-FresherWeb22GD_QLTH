<template lang="">
    <div class="m-popup-container">
        <div class="m-dialog">
            <div class="dialog__header">
                Thông báo
            </div>
            <div class="dialog__content">
                Bạn có chắc chắn muốn xóa Cán bộ giáo viên đang chọn không?
            </div>
            <div class="dialog__footer">
                <button class="m-button m-btn-style2">Đóng</button>
                <button class="m-button m-btn-style1">Đồng ý</button>
            </div>
            <button class="m-icon m-icon-24 button-icon icon-close" id="closeDialog" title="Close"></button>
        </div>
    </div>
</template>
<script>
export default {
    name: "MsDialog",
    props: ["isShow"],
    methods: {
        onClickCfDel() {
            return true;
        },

    },
}
</script>

<style>
    .showD {
        display: block !important;
    }
</style>