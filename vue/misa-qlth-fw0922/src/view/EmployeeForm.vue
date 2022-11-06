<template lang="">
    <div class="m-popup-container" :class="{isShowPopup:isShow}">
        <div class="m-insert-form">
            <div class="insert-form--left">
                <button class="upload-avatar m-icon button-icon">
                    <div class="upload-avatar--text">Chọn ảnh</div>
                </button>
                <div v-if="employee.employeeName" class="form--fullname">{{employee.employeeName}}</div>
                <div v-else class="form--fullname">Họ và tên</div>
                <div v-if="employee.employeeCode" class="form--e-id">{{employee.employeeCode}}</div>
                <div v-else class="form--e-id">Số hiệu cán bộ</div>
            </div>
            <div class="hor-line"></div>
            <div class="insert-form--right">
                <div class="form__header">
                    {{formTitle}}
                </div>
                <div class="form__content">
                    <div class="form__content--left">
                        <div class="m-input-container">
                            <label for="eId">Số hiệu cán bộ <span style="color: #FA3939;">*</span></label>
                            <input v-model="employee.employeeCode" id="eId" type="text" class="m-input" mrequired>
                            <div class="m-error-message" style="display: none;">
                                <div class="error-text">Mã cán bộ không được bỏ trống.</div>
                                <div class="error-arrow"></div>
                            </div>
                        </div>
                        <div class="m-input-container">
                            <label for="">Số điện thoại</label>
                            <input v-model="employee.employeePhoneNumber" type="text" class="m-input">
                        </div>
                        <div class="m-input-container m-dropdown-container">
                            <label>Tổ bộ môn</label>
                            <div class="m-dropdown">
                                <input @click="selectDepartment" v-model="curDpmName" type="text" class="m-icon icon-down m-input" readonly>
                                <div class="dropdown__option" :class="{showDown:isShowDepartment}">
                                    <div v-for="(item) in departmentData" :key="item.departmentId" class="dropdown__option--item">{{item.departmentName}}</div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="form__content--right">
                        <div class="m-input-container">
                            <label for="eName">Họ và tên <span style="color: #FA3939;">*</span></label>
                            <input v-model="employee.employeeName" id="eName" type="text" class="m-input" mrequired>
                            <div class="m-error-message" style="display: none;">
                                <div class="error-text">Họ và tên không được bỏ trống.</div>
                                <div class="error-arrow"></div>
                            </div>
                        </div>
                        <div class="m-input-container">
                            <label for="">Email</label>
                            <input v-model="employee.employeeEmail" type="text" class="m-input">
                        </div>
                        <div class="m-input-container m-dropdown-container">
                            <label>QL theo môn</label>
                            <div class="m-dropdown">
                                <div @click="selectSubject" class="m-icon icon-down m-input tag-list">
                                  <div class="m-tag" v-for="(item, index) in employee.subjects" :key="index">
                                    <span class="m-tag-text">{{item.subjectName}}</span>
                                    <div class="m-icon m-icon-24 icon-x"></div>
                                  </div>
                                </div>
                                <div class="dropdown__option" :class="{showDown:isShowSubject}">
                                    <div v-for="(item) in subjectData" :key="item.subjectId" class="dropdown__option--item">
                                      <div class="item--check m-checkbox">
                                            <input class="checkbox-real" type="checkbox">
                                            <div class="checkbox-pseudo"></div>
                                      </div>
                                      <div class="item--text">{{item.subjectName}}</div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="form__content--bottom">
                        <div class="m-input-container m-dropdown-container" style="width: 100%;">
                            <label>QL kho, phòng</label>
                            <div class="m-dropdown">
                                <div @click="selectRoom" class="m-icon icon-down m-input tag-list">
                                  <div class="m-tag" v-for="(item, index) in employee.rooms" :key="index">
                                    <span class="m-tag-text">{{item.roomName}}</span>
                                    <div class="m-icon m-icon-24 icon-x"></div>
                                  </div>
                                </div>
                                <div class="dropdown__option" :class="{showDown:isShowRoom}">
                                    <div v-for="(item) in roomData" :key="item.roomId" class="dropdown__option--item">
                                      <div class="item--check m-checkbox">
                                            <input class="checkbox-real" type="checkbox">
                                            <div class="checkbox-pseudo"></div>
                                      </div>
                                      <div class="item--text">{{item.roomName}}</div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="m-checkbox-container">
                            <div class="m-checkbox">
                                <input class="checkbox-real" type="checkbox">
                                <div class="checkbox-pseudo"></div>
                            </div>
                            <label for="" style="margin-right: 20px;" title="Trình độ nghiệp vụ quản lý thiết bị">Trình độ
                                nghiệp vụ QLTB</label>
                        </div>
                        <div class="m-checkbox-container">
                            <div id="boxEheckQuit" class="m-checkbox check--quit-date">
                                <input class="checkbox-real" type="checkbox" checked>
                                <div class="checkbox-pseudo"></div>
                            </div>
                            <label for="" style="margin-right: 20px;">Đang làm việc</label>
                        </div>
                        <div id="eQuitDate" class="m-input-container m-date-input quit-date">
                            <label for="">Ngày nghỉ việc</label>
                            <input type="date" class="m-input m-input-date" placeholder="dd-mm-yyyyy" name="date">
                        </div>
                    </div>
                </div>
                <div class="form__footer">
                    <button @click="onClickClose" id="btnClose" class="btn-close m-button m-btn-style2">Đóng</button>
                    <button @click="onClickSave" class="btn-save m-button m-btn-style1">Lưu</button>
                </div>
            </div>
            <button @click="onClickClose" class="m-icon m-icon-24 button-icon icon-close" id="closeInsertForm"></button>
        </div>
    </div>
</template>

<script>
import axios from "axios";

export default {
  name: "EmployeeForm",
  data() {
    return {
      // isShow: true,
      employee: {},
      formTitle: "Thêm hồ sơ Cán bộ, giáo viên",
      departmentData: {},
      subjectData: {},
      roomData: {},
      isShowDepartment: false,
      isShowRoom: false,
      isShowSubject: false,
      curDpmName: null,
      postMode: 1,
      listSubjcet: []
    };
  },
  props: ["isShow", "employeeSelectedId"], //{isShow:Boolean},
  watch: {
    /**
     * Theo dõi giá trị value tương ứng employeeId được chọn, nếu thay đổi thì hiện form tương ứng thông tin cán bộ
     * @params {int} Id của cán bộ tương ứng id của đối tượng khi click trong bảnh
     * Auth: KhaiND (29/10/2022)
     */
    employeeSelectedId: async function (value) {
      var me = this;
      if (value != null) {
        me.formTitle = "Chỉnh sửa hồ sơ Cán bộ, giáo viên";
        // Hien thi Loader

        // Call API, nho check Status code va dua ra thong bao
        var url = "https://localhost:44344/api/Employees/" + value;
        console.log("Watching: " + value);
        axios
          .get(url)
          .then(function (response) {
            me.employee = response.data;
            // Kiểm tra Id tổ bộ môn và gán tên tổ bộ môn tương ứng cho cán bộ được chọn
            me.departmentData.forEach((item) => {
              if (item.departmentId == response.data.departmentId) {
                me.curDpmName = item.departmentName;
              }
            });
            console.log("+++++++++" + me.curDpmName);
            console.log(me.employee);
            //An Loader
          })
          .catch(function (response) {
            console.log(response);
          });
        //An Loader

        // Cất dữ liệu
        me.postMode = 0;
      } else {
        me.formTitle = "Thêm hồ sơ Cán bộ, giáo viên";
        me.employee = {};
        //Gợi ý Số hiệu CB mới
        await axios //DỂ TRONG MOUNTED
          .get("https://localhost:44344/api/Employees/newcode")
          .then(function (res) {
            me.employee.employeeCode = res.data;
          });
        //Thêm mới
        me.postMode = 1;
      }
      console.log("Da bind duoc Id ve Form: " + value);
    },
  },
  methods: {
    /**
     * Sự kiện khi bấm vào nút Đóng hoặc icon x trên Form thì ẩn pop-up Form đi
     * Auth: KhaiND (29/10/2022)
     */
    onClickClose() {
      this.$emit("hidePopup", null);
    },
    /**
     * Sự kiện bấm nút Lưu trên form chi tiết Employee
     * Auth: KhaiND (30/10/2022)
     */
    async onClickSave() {
      var me = this;
      // Validate dữ liệu
      // Build object thông tin cán bộ
      // Gọi API thực hiện lưu dữ liệu
      if (this.postMode == 1) {
        console.log("POST");
        await axios
          .post("https://localhost:44344/api/Employees", me.employee)
          .then(function () {
            console.log("SUCCESS");
          })
          .catch(function () {});
        // Đóng form
        this.$emit("hidePopup", null);
        // Reload và toast
        this.$emit("reloadData", null);
      } else {
        console.log("PUT");
        var pUrl =
          "https://localhost:44344/api/Employees/" + me.employeeSelectedId;
        await axios
          .put(pUrl, me.employee)
          .then(function () {
            console.log("PUT Ok");
          })
          .catch(function () {});
        // Đóng form
        this.$emit("hidePopup", null);
        // Reload và toast
        await this.$emit("reloadData", null);
      }

      // Đưa ra thông báo hoàn thành và ẩn form
    },
    /**
     * Gọi API lấy danh sách tổ bộ môn, môn học, kho phòng
     * Auth: KhaiND (30/10/2022)
     */
    onLoadDSR() {
      var me = this;
      // Lấy danh sách môn
      try {
        axios
          .get("https://localhost:44344/api/Subjects")
          .then(function (response) {
            me.subjectData = response.data;
            // console.log(me.subjectData);
            //An Loader
          })
          .catch(function (response) {
            console.log(response);
          });
      } catch (error) {
        console.log(error);
      }
      // Lấy danh sách Kho phòng
      try {
        axios
          .get("https://localhost:44344/api/Rooms")
          .then(function (response) {
            me.roomData = response.data;
            // console.log(me.roomData);
            //An Loader
          })
          .catch(function (response) {
            console.log(response);
          });
      } catch (error) {
        console.log(error);
      }
      // Lập danh sách Tổ bộ môn
      this.departmentData = [
        {
          departmentId: 0,
          departmentName: "Tổ Toán - Lý - Hóa",
        },
        {
          departmentId: 1,
          departmentName: "Tổ Toán - Tin",
        },
        {
          departmentId: 2,
          departmentName: "Tổ Hóa - Sinh",
        },
        {
          departmentId: 3,
          departmentName: "Tổ Anh văn",
        },
        {
          departmentId: 4,
          departmentName: "Tổ Sử - Địa - GDCD",
        },
        {
          departmentId: 5,
          departmentName: "Tổ Ngữ văn",
        },
        {
          departmentId: 6,
          departmentName: "Tổ Lái",
        },
      ];
    },
    /**
     * ẩn hiện select
     */
    selectDepartment() {
      this.isShowDepartment = !this.isShowDepartment;
    },
    selectRoom() {
      this.isShowRoom = !this.isShowRoom;
    },
    selectSubject() {
      this.isShowSubject = !this.isShowSubject;
    },
  },
  created() {
    this.onLoadDSR();
  },
};
</script>

<style scoped>

</style>