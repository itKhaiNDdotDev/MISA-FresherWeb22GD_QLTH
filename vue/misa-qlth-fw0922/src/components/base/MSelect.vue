<template>
  <div class="m-dropdown">
    <input @click="toggleMe" v-model="value" v-text="value" type="text"
      class="m-icon icon-down m-input" readonly
    />
    <div class="m-icon icon-down m-input tag-list">
      <div class="m-tag" v-for="(item, index) in selected" :key="index">
        <span class="m-tag-text">{{ item }}</span>
        <div class="m-icon m-icon-24 icon-x"></div>
      </div>
    </div>
    <div class="dropdown__option" :class="{ showOpt: isShow }">
      <div class="dropdown__option--item option--checkall">
        <div class="item--check m-checkbox">
          <input @click="checkAll()" v-model="isCheckAll" class="checkbox-real" type="checkbox"/>
          <div class="checkbox-pseudo"></div>
        </div>
        <div class="item--text">Tất cả</div>
      </div>
      <div v-for="item in options" :key="item.id" class="dropdown__option--item">
        <div class="item--check m-checkbox">
          <input v-bind:value="item.id" v-model="selectedIds"
            @change="updateCheckall()" class="checkbox-real" type="checkbox"
          />
          <div class="checkbox-pseudo"></div>
        </div>
        <div class="item--text">{{ item.name }}</div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "MSelect",
  props: ["options"],
  data() {
    return {
      isShow: false,
      isCheckAll: false,
      // options: [
      //   { id: "1", name: "JavaScript" },
      //   { id: "2", name: "JavaScript" },
      //   { id: "3", name: "Ruby" },
      //   { id: "4", name: "Ruby" },
      //   { id: "5", name: "PHP" },
      //   { id: "6", name: "Elixir" },
      // ],
      selectedIds: []
    };
  },
  methods: {
    /**
     * Ẩn hiện danh sách option
     * Auth: KhaiND (05/11/2022)
     */
    toggleMe() {
      this.isShow = !this.isShow;
    },
    /**
     * Chức năng chọn tất cả option
     * Auth: KhaiND (05/11/2022)
     */
    checkAll: function () {
      this.isCheckAll = !this.isCheckAll;
      this.selectedIds = [];
      if (this.isCheckAll) {
        // Check all
        this.options.forEach(opt => {
          this.selectedIds.push(opt.id);
          console.log(opt.id);
        });
      }
    },
    /**
     * Mỗi lần check một option thì kiểm tra xem, nếu đã check hết phần tử thì đặt check của Tất cả thành true (checked)
     * Auth: KhaiND (05/11/2022)
     */
    updateCheckall: function () {
      if (this.selectedIds.length == this.options.length) {
        this.isCheckAll = true;
      } else {
        this.isCheckAll = false;
      }
    },
  },
  /**
   * Mỗi lần có thay đổi trong danh sách chọn thì kiểm tra các id được chọn để trả về danh sách tên lựa chọn tương ứng cho việc hiển thị tag
   * Auth: KhaiND (05/11/2022)
   */
  computed: {
    selected() {
      var sltName = [];
      this.options.forEach(opt => {
        this.selectedIds.forEach( val => {
          if(opt.id == val)
            sltName.push(opt.name);
        });
      });
      return sltName;
    },
  },
};
</script>

<style scoped>
input {
  position: absolute;
  top: 0px;
  left: 0px;
  right: 0px;
  opacity: 0;
  z-index: 1;
}
.tag-list {
  background-color: #FFFFFF;
  position: absolute;
  top: 0px;
  left: 0px;
  right: 0px;
  z-index: 0;
}

.dropdown__option.showOpt {
  display: block;
  top: 36px;
}

.option--checkall {
  border-bottom: 1px solid #cccccc;
  padding-bottom: 4px;
  margin-bottom: 4px;
  position: sticky;
  top: 0px;
  background-color: #ffffff;
  z-index: 1;
}

::-webkit-scrollbar-track {
  margin-top: 34px;
}
</style>