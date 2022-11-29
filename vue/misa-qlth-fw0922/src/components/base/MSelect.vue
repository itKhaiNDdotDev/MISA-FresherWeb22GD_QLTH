<template>
  <div class="m-dropdown" @blur="isShow=false">
    <input :tabindex="tabindex"
      @click="toggleMe"
      type="text" :title="selecteds.map((i) => {return i.itemName})"
      class="m-icon icon-down m-input"
      readonly
    />
    <div class="m-icon icon-down m-input tag-list">
      <div class="tag-list">
      <div class="m-tag" v-for="(item, index) in selecteds" :key="index">
        <span class="m-tag-text">{{ item.itemName }}</span>
        <div @click="changeSelection(item.itemId)" class="m-icon m-icon-24 icon-x"></div>
      </div>
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
            @change="changeSelection(item.itemId)"
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
    },
    tabindex: Number
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
     * Author: KhaiND (05/11/2022)
     */
    toggleMe() {
      this.isShow = !this.isShow;
    },

    /**
     * Chức năng chọn tất cả option
     * Author: KhaiND (05/11/2022)
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
     * Author: KhaiND (05/11/2022)
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
     * Author: KhaiND (07/11/2022)
     */
    changeSelection: function(itemId){
      // pram: event - ở trên Select thì ko truyền tham số khi gọi method nữa
      // this.$emit('selectOpt', event.target.value);
      this.$emit('selectOpt', itemId);
    },

    /**
     * Lời gọi emit thực hiện checkall lựa chọn
     * Author: KhaiND (05/11/2022)
     */
    checkAll() {
      this.$emit("checkAll", null);
    }

    // checkSelected(val) {
    //   this.$emit('checkSelected', val);
    //   console.log("Bắt giá trị CHECKED: " + this.$emit('checkSelected', val));
    // }
  },

  /**
   * Mỗi lần có thay đổi trong danh sách chọn thì kiểm tra các id được chọn để trả về danh sách tên lựa chọn tương ứng cho việc hiển thị tag
   * Auth: KhaiND (05/11/2022)
   */
  computed: {
    selecteds() {
      try {
        var sltObjs = [];
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
              if(this.options[i].itemId == this.selectedIds[j]) {
                var sltObj = {
                  itemId: this.options[i].itemId,
                  itemName: this.options[i].itemName
                }
                sltObjs.push(sltObj);
              }
            }
          }
        }
        console.log(sltObjs);
        return sltObjs;
      }
      catch(error) {
        console.log(error);
        return null;
      }
    },
  },
};
</script>

<style scoped>
.m-dropdown {
  position: relative;
  height: 32px;
  width: 100%;
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
  /* position: absolute;
  top: 0px;
  left: 0px;
  width: 100%;
  z-index: 0; */
  /* flex: 1; */
  width: 100%;
  min-height: 32px;
  flex-wrap: nowrap;
  /* overflow: auto; */
  overflow: hidden;
}
input:focus+.tag-list {
  border-color: #0DD469;
}

.m-input .tag-list {
  position: absolute;
  top: 1px;
  left: 1px;
  bottom: 1px;
  right: 28px;
  width: calc(100% - 56px);
  min-height: 24px !important;
  background-color: inherit;
}

/* Employee Form - Tag list of combobox */
.form__content--right .tag-list {
    max-width: 512px;
}
  
.form__content--bottom .tag-list {
    max-width: 780px;
}

.tag-list .icon-x {
  z-index: 1;
  cursor: pointer;
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

</style>