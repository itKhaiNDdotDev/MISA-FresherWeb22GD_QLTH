<template lang="">
    <div class="footer">
        <button @click="onClickMoveToFirst" class="m-icon button-icon m-icon-24 icon-movetofirst" title="Về trang đầu"></button>
        <button @click="onClickPrevios" class="m-icon button-icon m-icon-24 icon-back" title="Về trang trước"></button>
        <input class="m-page-number" v-model="pageIndex" @keyup.enter="searchEmployeePagging">
        <button @click="onClickNext" class="m-icon button-icon m-icon-24 icon-next" title="Trang kế tiếp"></button>
        <button @click="onClickMoveToLast" class="m-icon button-icon m-icon-24 icon-movetolast" title="Đến trang cuối"></button>
        <div class="m-total-page">{{pageIndex}}/{{Math.ceil(totalRecord/pageSize)}} trang &nbsp; &nbsp; ({{totalRecord}} giáo viên)</div>
    </div>
</template>

<script>
import {PAGE_SIZE} from "./../../resources";

export default {
    name: 'EmployeePaging',
    props: ["totalRecord", "searchText", "propPageIndex"],
    data() {
        return {
            pageIndex: 1,
            pageSize: PAGE_SIZE
        }
    },
    methods: {
        /**
         * Sự kiện khi người dùng bấm vào icon tương ứng thì chuyển về trang đầu
         * Auth: KhaiND (17/11/2022)
         */
        onClickMoveToFirst() {
            this.pageIndex = 1;
            this.$emit('searchEmployee', this.searchText, this.pageIndex);
        },
        /**
         * Sự kiện khi người dùng bấm vào icon tương ứng thì chuyển về trang cuối
         * Auth: KhaiND (17/11/2022)
         */
        onClickMoveToLast() {
            this.pageIndex = Math.ceil(this.totalRecord/PAGE_SIZE);
            this.$emit('searchEmployee', this.searchText, this.pageIndex);
        },
        /**
         * Sự kiện khi người dùng bấm vào icon tương ứng thì chuyển về trang trước
         * Auth: KhaiND (17/11/2022)
         */
        onClickPrevios() {
            if(this.pageIndex > 1)
            {
                this.pageIndex -= 1;
                this.$emit('searchEmployee', this.searchText, this.pageIndex);
            }
        },
        /**
         * Sự kiện khi người dùng bấm vào icon tương ứng thì chuyển về trang sau
         * Auth: KhaiND (17/11/2022)
         */
        onClickNext() {
            if(this.pageIndex < Math.ceil(this.totalRecord/PAGE_SIZE)) {
                this.pageIndex += 1;
                this.$emit('searchEmployee', this.searchText, this.pageIndex);
            }
        },
        /**
         * Gọi lại việc tìm kiếm dữ liệu án bộ theo keyword đang sử dụng và phân trang, đi đến kết quả trang tương ứng pageIndex
         * Auth: KhaiND (18/11/2022)
         */
        searchEmployeePagging() {
            if(this.pageIndex < 1)
                this.pageIndex = 1;
            if(this.pageIndex > Math.ceil(this.totalRecord/PAGE_SIZE))
                this.pageIndex = Math.ceil(this.totalRecord/PAGE_SIZE);
            this.$emit('searchEmployee', this.searchText, this.pageIndex);
        }
    },
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