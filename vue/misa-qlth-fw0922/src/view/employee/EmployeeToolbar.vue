<template lang="">
    <div class="toolbar">
        <div class="toolbar-left">
            <div class="m-input--search">
                <input v-model="searchText" type="text" class="m-input">
                <div class="hor-line"></div>
                <button @click="searchEmployee" @keyup.enter="searchEmployee" class="icon-search" title="Tìm kiếm theo tên, số điện thoại,..."></button>
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
    data() {
        return {
            searchText: ""
        }
    },
    // props: ["isShowPopup"],
    methods: {
        /**
         * Sự kiện bấm vào nút Thêm trên Toolbar - Hiển thị form thêm mới Employee
         * Auth: KhaiND(29/10/2022)
         */
        onClickBtnAdd() {
            this.$emit('showPopup', 0)
            console.log('click Add');
        },
        /**
         * Tính năng tìm kiếm
         * Auth: KhaiND (09/11/2022)
         */
        searchEmployee() {
            this.$emit('searchEmployee', this.searchText);
        }
    }
}
</script>

<style lang="">
    
</style>