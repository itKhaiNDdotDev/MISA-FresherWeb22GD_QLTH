<template lang="">
    <div class="footer">
        <button class="m-icon button-icon m-icon-24 icon-movetofirst" title="Về trang đầu"></button>
        <button class="m-icon button-icon m-icon-24 icon-back" title="Về trang trước"></button>
        <input class="m-page-number">
        <button class="m-icon button-icon m-icon-24 icon-next" title="Trang kế tiếp"></button>
        <button class="m-icon button-icon m-icon-24 icon-movetolast" title="Đến trang cuối"></button>
        <div class="m-total-page">1/2 trang &nbsp; &nbsp; (56 giáo viên)</div>
    </div>
</template>

<script>
export default {
    name: 'EmployeePaging'
}
</script>

<style scoped>
    input {
        padding: 0px 12px;
        outline: none;
        box-sizing: border-box;
        text-align: center;
    }
</style>