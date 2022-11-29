<template lang="">
    <div class="no-data">
        <div class="m-icon icon-alert"></div>
        <div class="no-data--title">Chưa có giáo viên</div>
        <div class="no-data--message">Vui lòng chọn Thêm mới từng giáo viên hoặc Nhập khẩu để lấy
            danh sách hiện có tại trường</div>
        <div class="no-data--button">
            <button @click="onClickBtnAdd" class="m-button m-btn-style1">Thêm mới</button>
            <button class="m-button m-btn-style1">Nhập khẩu</button>
        </div>
    </div>
</template>

<script>
export default {
  name: "NoData",
  methods: {
        /**
         * Sự kiện bấm vào nút Thêm trên Toolbar - Hiển thị form thêm mới Employee
         * Auth: KhaiND(29/10/2022)
         */
        onClickBtnAdd() {
            this.$emit('showPopup', 0)
            console.log('click Add');
        }
    }
};
</script>

<style lang="">
</style>