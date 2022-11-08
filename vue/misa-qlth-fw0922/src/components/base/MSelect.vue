<template>
  <div class="m-dropdown" @blur="isShow=false">
    <input
      @click="toggleMe"
      type="text"
      class="m-icon icon-down m-input"
      readonly
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
          <input
            v-bind:checked="isCheckAll"
            @change="checkAll"
            class="checkbox-real"
            type="checkbox"
          />
          <div class="checkbox-pseudo"></div>
        </div>
        <div class="item--text">Tất cả</div>
      </div>
      <div
        v-for="item in options"
        :key="item.itemId"
        class="dropdown__option--item"
      >
      <!-- @change="updateCheckall()" -->
        <div class="item--check m-checkbox">
          <input
            v-bind:value="item.itemId"
            v-bind:checked="checkSelected(item.itemId)"
            @change="changeSelection"
            class="checkbox-real"
            type="checkbox"
          />
          <div class="checkbox-pseudo"></div>
        </div>
        <div class="item--text">{{ item.itemName }}</div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "MSelect",
  emits: ['selectRoom'],
  props: {
    options: Array,
    selectedIds: Array,
    isCheckAll: Boolean,
    checkSelected: {
      type: Function
    }
  },
  data() {
    return {
      isShow: false,
      // isCheckAll: false,
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
    // checkAll: function () {
    //   this.isCheckAll = !this.isCheckAll;
    //   this.selectedIds = [];
    //   if (this.isCheckAll) {
    //     // Check all
    //     // this.options.forEach(opt => {
    //     //   this.selectedIds.push(opt.itemId);
    //     //   console.log(opt.itemId);
    //     // });
    //     for (var i = 0; i < this.options.length; i++) {
    //       this.SelectIds.push(this.options[i].itemId);
    //     }
    //   }
    // },
    /**
     * Mỗi lần check một option thì kiểm tra xem, nếu đã check hết phần tử thì đặt check của Tất cả thành true (checked)
     * Auth: KhaiND (05/11/2022)
     */
    // updateCheckall: function () {
    //   if (this.selectedIds.length == this.options.length) {
    //     this.isCheckAll = true;
    //   } else {
    //     this.isCheckAll = false;
    //   }
    // },
    /**
     * Lời gọi emit đến hàm xử lý việc check/uncheck cho options tương ứng
     * @param {event} event - sự kiện thay đổi tương ứng với options checkbox
     * Auth: KhaiND (07/11/2022)
     */
    changeSelection: function(event){
      console.log(event);
      this.$emit('selectOpt', event.target.value);
    },
    checkAll() {
      this.$emit("checkAll", null);
    }
    // checkSelected(val) {
    //   this.$emit('checkSelected', val);
    //   console.log("Bắt giá trị CHECKED: " + this.$emit('checkSelected', val));
    // }
  },
  /////////////////// DEBUG //////////////////////////
  watch: {
    selectedIds() {
      console.log("WATCH SELECTED_ID: ");
      console.log(this.selectedIds);
    },
    isCheckAll() {
      console.log("Giá trị của isCheckAll: " + this.isCheckAll);
    }
  },
  /**
   * Mỗi lần có thay đổi trong danh sách chọn thì kiểm tra các id được chọn để trả về danh sách tên lựa chọn tương ứng cho việc hiển thị tag
   * Auth: KhaiND (05/11/2022)
   */
  computed: {
    selected() {
      var sltName = [];
      // this.options.forEach(opt => {
      //   this.selectedIds.forEach( val => {
      //     if(opt.itemId == val)
      //       sltName.push(opt.itemName);
      //   });
      // });
      if(this.selectedIds == undefined)
        console.log("EMPTY SELECTED");
      else
      {
        for (var i = 0; i < this.options.length; i++) {
        for (var j = 0; j < this.selectedIds.length; j++) {
          if(this.options[i].itemId == this.selectedIds[j])
            sltName.push(this.options[i].itemName);
        }
      }
      }
      
      // console.log("SELECTED: ");
      // console.log(this.selectedIds);
      return sltName;
    },
  },
};
</script>

<style scoped>
.m-dropdown {
  position: relative;
  height: 32px;
}

input {
  position: absolute;
  top: 0px;
  left: 0px;
  right: 0px;
  opacity: 0;
  z-index: 1;
}

.tag-list {
  background-color: #ffffff;
  position: absolute;
  top: 0px;
  left: 0px;
  right: 0px;
  z-index: 0;
}
input:focus+.tag-list {
  border-color: #0DD469;
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