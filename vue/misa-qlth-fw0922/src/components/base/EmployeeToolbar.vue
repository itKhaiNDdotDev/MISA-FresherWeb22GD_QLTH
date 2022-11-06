<template lang="">
    <div class="toolbar">
        <div class="toolbar-left">
            <div id="searchEmployee" class="m-input--search">
                <input type="text" class="m-input">
                <div class="hor-line"></div>
                <button class="icon-search" title="Tìm kiếm theo tên, số điện thoại,..."></button>
            </div>
        </div>
        <div class="toolbar-right">
            <button @click="onClickBtnAdd" class="m-button m-btn-style1">Thêm</button>
            <button class="m-button m-btn-style2">Xuất khẩu</button>
            <button id="btnMore" class="m-btn-style2 m-btn-style4 icon-more" title="Tính năng khác"></button>
        </div>
    </div>
</template>

<script>
export default {
    name: 'EmployeeToolbar',
    // props: ["isShowPopup"],
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
}
</script>

<style lang="">
    
</style>