<template lang="">
    <div class="toolbar">
        <div class="toolbar-left">
            <div class="m-input--search">
                <input v-model="searchText" @keyup.enter="searchEmployee" type="text" class="m-input" placeholder="Tìm kiếm theo tên, số điện thoại hoặc email">
                <div class="hor-line"></div>
                <button @click="searchEmployee" class="icon-search" title="Tìm kiếm theo tên, số điện thoại hoặc email"></button>
            </div>
        </div>
        <div class="toolbar-right">
            <button @click="onClickBtnAdd" class="m-button m-btn-style1">{{buttonText.Add}}</button>
            <button @click="onClickExport" class="m-button m-btn-style2">{{buttonText.Export}}</button>
            <button @click="onClickMore" class="m-btn-style2 m-btn-style4 icon-more" :class="{extend:showMore}" title="Tính năng khác"></button>
        </div>
        <div class="toolbar-extend" v-if="showMore">
            <div class="m-arrow"></div>
            <button @click="onClickDeleteMany" class="m-button m-btn-style2 icon-delete m-extend-button">{{buttonText.Delete}}</button>
            <button class="m-button m-btn-style1">{{buttonText.Import}}</button>
        </div>
    </div>
</template>

<script>
import CommonText, {BASE_URL} from "./../../utils/resources/common";
import axios from "axios";
import { ResultStatus } from "./../../utils/enums/status";

export default {
  name: "EmployeeToolbar",
  components: {
    // MsDialog,
  },

  data() {
    return {
      buttonText: CommonText.Button,
      searchText: "",
      showMore: false
    };
  },

  methods: {
    /**
     * Sự kiện bấm vào nút Thêm trên Toolbar - Hiển thị form thêm mới Employee
     * Auth: KhaiND(29/10/2022)
     */
    onClickBtnAdd() {
      this.$emit("showPopup", 0);
    },

    /**
     * Tính năng tìm kiếm
     * Auth: KhaiND (09/11/2022)
     */
    searchEmployee() {
      this.$emit("searchEmployee", this.searchText);
    },

    /**
     * SỰ kiện bấm vào More, ẩn hiện các nút bấm tính năng mở rộng
     * Auth: KhaiND (23/11/2022)
     */
    onClickMore() {
      this.showMore = !this.showMore;
    },

    /**
     * Sự kiện bấm vào nút xóa nhiều, mở Dialog confirm xóa nhiều
     */
    onClickDeleteMany() {
      this.$emit("onClickDeleteMany");
    },

    /**
     * Sự kiện khi bấm vào nút Xuất khẩu thì xuất danh sách nhân viên ra file Excel và tiến hành tải xuống
     * Author: KhaiND (26/11/2022)
     */
    onClickExport() {
      try {
        // loading
        this.$emit("startLoading");
        // Tải file Export
        axios.get(BASE_URL + "Employees/export")
        .then(response => {
          window.open(response.request.responseURL);
          this.$emit("endLoading");
        })
        .catch(response => {
          // Xử lý lỗi
          console.log(response);
          this.$emit("endLoading");
          this.$emit("showToast", CommonText.Toast.Message.Error500, ResultStatus.FAIL);
        });
      }
      catch(error) {
        console.log(error);
          this.$emit("endLoading");
          this.$emit("showToast", CommonText.Toast.Message.Error500, ResultStatus.FAIL);
      }
    }
  }
};
</script>

<style scoped>
  .icon-more.extend {
    background-color: #F8F8F8;
  }

  .toolbar {
    position: relative;
  }

  .toolbar-extend {
    position: absolute;
    top: calc(100% - 12px);
    right: 16px;
    z-index: 2;
    display: flex;
    flex-direction: column;
    background-color: #FFFFFF;
    border-radius: 4px;
    box-shadow: 0px 0px 10px #D5D5D5;
    padding: 4px 8px;
  }

  .m-arrow {
    background-color: inherit;
    width: 6px;
    height: 6px;
    position: absolute;
    top: 0px;
    right: 8px;
    transform: translate(-50%, -50%) rotate(45deg);
  }

.toolbar-extend .m-button {
  margin: 4px 0px;
}

 .m-extend-button {
  background-repeat: no-repeat;
  background-position: 6px center;
 }

 .m-input--search input::placeholder{
  font-style: italic;
 }
</style>