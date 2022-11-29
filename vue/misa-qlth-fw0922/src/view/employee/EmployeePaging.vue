<template lang="">
    <div class="footer">
        <button @click="onClickMoveToFirst" class="m-icon button-icon m-icon-24 icon-movetofirst" title="Về trang đầu"></button>
        <button @click="onClickPrevios" class="m-icon button-icon m-icon-24 icon-back" title="Về trang trước"></button>
        <input class="m-page-number" v-model="pageIndex" @keyup.enter="searchEmployeePagging">
        <button @click="onClickNext" class="m-icon button-icon m-icon-24 icon-next" title="Trang kế tiếp"></button>
        <button @click="onClickMoveToLast" class="m-icon button-icon m-icon-24 icon-movetolast" title="Đến trang cuối"></button>
        <div class="m-total-page">{{pageIndex}}/{{Math.ceil(totalRecord/pageSize)}} {{pagingText.Page}} &nbsp; &nbsp; ({{totalRecord}} {{pagingText.Employee}})</div>
    </div>
</template>

<script>
import {PAGE_SIZE} from "../../utils/resources/common";
import EmployeeText from "./../../utils/resources/employee";

export default {
    name: 'EmployeePaging',
    props: ["totalRecord", "searchText", "propPageIndex"],
    data() {
        return {
            pagingText: EmployeeText.Paging, // Text Content của các thẻ tương ứng hiển thị
            pageIndex: 1,
            pageSize: PAGE_SIZE
        }
    },
    
    watch: { /// CHưa dùng
        pageIndex: function(value) {
            if(value > Math.ceil(this.totalRecord/PAGE_SIZE)) {
                console.log("OK");
                this.pageIndex = Math.ceil(this.totalRecord/PAGE_SIZE)
                this.$emit('searchEmployee', this.searchText, this.pageIndex);
            }
        }
    },

    methods: {
        /**
         * Sự kiện khi người dùng bấm vào icon tương ứng thì chuyển về trang đầu
         * Auth: KhaiND (17/11/2022)
         */
        onClickMoveToFirst() {
            try {
                if(this.pageIndex != 1) {
                    this.pageIndex = 1;
                    this.$emit('searchEmployee', this.searchText, this.pageIndex);
                }
            }
            catch(error) {
                console.log(error);
                this.pageIndex = 1;
            }
        },
        /**
         * Sự kiện khi người dùng bấm vào icon tương ứng thì chuyển về trang cuối
         * Auth: KhaiND (17/11/2022)
         */
        onClickMoveToLast() {
            try {
                if(this.pageIndex != Math.ceil(this.totalRecord/PAGE_SIZE)) {
                    this.pageIndex = Math.ceil(this.totalRecord/PAGE_SIZE);
                    this.$emit('searchEmployee', this.searchText, this.pageIndex);
                }
            }
            catch(error) {
                console.log(error);
                this.pageIndex = 1;
            }
        },
        /**
         * Sự kiện khi người dùng bấm vào icon tương ứng thì chuyển về trang trước
         * Auth: KhaiND (17/11/2022)
         */
        onClickPrevios() {
            try {
                if(this.pageIndex > 1)
                {
                    this.pageIndex -= 1;
                    this.$emit('searchEmployee', this.searchText, this.pageIndex);
                }
            }
            catch(error) {
                console.log(error);
                this.pageIndex = 1;
            }
        },

        /**
         * Sự kiện khi người dùng bấm vào icon tương ứng thì chuyển về trang sau
         * Auth: KhaiND (17/11/2022)
         */
        onClickNext() {
            try {
                if(this.pageIndex < Math.ceil(this.totalRecord/PAGE_SIZE)) {
                    this.pageIndex += 1;
                    this.$emit('searchEmployee', this.searchText, this.pageIndex);
                }
            }
            catch(error) {
                console.log(error);
                this.pageIndex = 1;
            }
        },

        /**
         * Gọi lại việc tìm kiếm dữ liệu án bộ theo keyword đang sử dụng và phân trang, đi đến kết quả trang tương ứng pageIndex
         * Auth: KhaiND (18/11/2022)
         */
        searchEmployeePagging() {
            try {
                if(this.pageIndex < 1)
                    this.pageIndex = 1;
                if(this.pageIndex > Math.ceil(this.totalRecord/PAGE_SIZE))
                    this.pageIndex = Math.ceil(this.totalRecord/PAGE_SIZE);
                this.$emit('searchEmployee', this.searchText, this.pageIndex);
            }
            catch(error) {
                console.log(error);
                this.$emit('searchEmployee', "", 1);
            }
        },

        /**
         * Thiết lập giá trị số trang hiện tại hiển thị ở ô số trang
         * Author: KhaiND (28/11/2022)
         */
        setPageIndex(value) {
            this.pageIndex = value;
        },
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