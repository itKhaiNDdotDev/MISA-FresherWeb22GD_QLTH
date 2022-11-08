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
                            <div class="m-dropdown" @blur="isShowDepartment=false">
                                <input @click="selectDepartment" 
                                type="text" class="m-icon icon-down m-input"
                                v-model="curDepName"
                                readonly>
                                <div class="dropdown__option" :class="{showDown:isShowDepartment}">
                                    <div v-for="(item) in departmentData" :key="item.departmentId" @click="cbooseDepartment(item.departmentId)" class="dropdown__option--item">{{item.departmentName}}</div>
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
                            <label title="Quản lý theo môn">QL theo môn</label>
                            <MSelect :options="subjectData" :selectedIds="employee.subjectIds" 
                              @selectOpt="selectSubject" @checkAll="selectAllSubject"
                              :checkSelected="checkSubjectIdSelected" :isCheckAll="checkAllSubject"/>
                        </div>
                    </div>
                    <div class="form__content--bottom">
                        <div class="m-input-container m-dropdown-container" style="width: 100%;">
                            <label title="Quản lý theo kho, phòng">QL kho, phòng</label>
                            <MSelect :options="roomData" :selectedIds="employee.roomIds" 
                              @selectOpt="selectRoom" @checkAll="selectAllRoom"
                              :checkSelected="checkRoomIdSelected" :isCheckAll="checkAllRoom"/>
                        </div>
                        <div class="m-checkbox-container">
                            <div class="m-checkbox">
                                <input v-model="employee.isDeviceManager" class="checkbox-real" type="checkbox">
                                <div class="checkbox-pseudo"></div>
                            </div>
                            <label for="" style="margin-right: 20px;" title="Trình độ nghiệp vụ quản lý thiết bị">Trình độ
                                nghiệp vụ QLTB</label>
                        </div>
                        <div class="m-checkbox-container">
                            <div id="boxEheckQuit" class="m-checkbox check--quit-date">
                                <input v-model="employee.workingStatus" class="checkbox-real" type="checkbox" checked>
                                <div class="checkbox-pseudo"></div>
                            </div>
                            <label for="" style="margin-right: 20px;">Đang làm việc</label>
                        </div>
                        <div v-if="employee.workingStatus" id="eQuitDate" class="m-input-container m-date-input quit-date">
                            <label for="">Ngày nghỉ việc</label>
                            <input type="date" class="m-input m-input-date" placeholder="dd-mm-yyyyy" name="date">
                        </div>
                        <div v-else style="width: 240px; height: 32px;" class="m-input-container m-date-input quit-date"></div>
                    </div>
                </div>
                <div class="form__footer">
                    <button @click="onClickClose" class="btn-close m-button m-btn-style2">Đóng</button>
                    <button @click="onClickSave" class="btn-save m-button m-btn-style1">Lưu</button>
                </div>
            </div>
            <button title="Đóng" @click="onClickClose" class="m-icon m-icon-24 button-icon icon-close"></button>
        </div>
        <MLoader :isShow="loadingStatus"/>
    </div>
</template>

<script>
import axios from "axios";
import MSelect from "./../components/base/MSelect.vue";
import MLoader from "./../components/base/MLoader.vue";

export default {
  name: "EmployeeForm",
  components: {
    MSelect,
    MLoader
  },
  data() {
    return {
      // isShow: true,
      employee: {},
      formTitle: "Thêm hồ sơ Cán bộ, giáo viên",
      departmentData: {},
      subjectData: {},
      roomData: {},
      isShowDepartment: false,
      curDpmName: null,
      postMode: 1,
      checkAllRoom: false,
      checkAllSubject: false,
      loadingStatus: false
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
        this.loadingStatus = true;
        // Call API, nho check Status code va dua ra thong bao
        var url = "https://localhost:44344/api/Employees/" + value;
        console.log("Watching: " + value);
        axios
          .get(url)
          .then(function (response) {
            me.employee = response.data;
            // // Kiểm tra Id tổ bộ môn và gán tên tổ bộ môn tương ứng cho cán bộ được chọn
            // me.departmentData.forEach((item) => {
            //   if (item.departmentId == response.data.departmentId) {
            //     me.curDpmName = item.departmentName;
            //   }
            // });
            // console.log("+++++++++" + me.curDpmName);
            console.log(me.employee);
            //An Loader
          })
          .catch(function (response) {
            console.log(response);
          });
        //An Loader
        this.loadingStatus = false;
        // Sửa dữ liệu
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
      // if(me.employee.roomIds.length == me.roomData.length) {
      //   me.checkAllRoom = true;
      // }
    },
  },

  methods: {
    /**
     * Bắt giá trị roomId của các kho phòng được chọn khi check vào ô tương ứng
     * Auth: KhaiND (07/11/2022)
     */
    selectRoom: function (value) {
      var rooms = this.employee.roomIds;
      // Nếu giá trị tương ứng đã được check (đã tồn tại trong danh sách chọn)
      if (rooms.includes(parseInt(value))) {
        // Loại bỏ nó đi (bấm vào thì uncheck)
        this.employee.roomIds = rooms.filter((x) => {
          return x != parseInt(value);
        });
      }
      // Ngược lại, chwa check thì check (thêm giá trị tương ứng vào danh sách chọn)
      else {
        this.employee.roomIds.push(parseInt(value));
      }
      // Kiểm tra xem nếu đã check hết các lựa chọn thì tự động tick ô Tất cả
      if (this.employee.roomIds.length == this.roomData.length) {
        this.checkAllRoom = true;
      } else {
        this.checkAllRoom = false;
      }
      // console.log("LEANGTH: ");
      // console.log(this.employee.roomIds.length);
      // console.log(this.roomData.length);
    },

    /**
     * Thực hiện chọn, bỏ chọn tất cả options của Kho, phòng
     * Auth: KhaiND (08/11/2022)
     */
    selectAllRoom() {
      this.checkAllRoom = !this.checkAllRoom;
      this.employee.roomIds = [];
      if (this.checkAllRoom) {
        for (var i = 0; i < this.roomData.length; i++) {
          console.log("PUSH ROOM ID FOR ALL: " + this.roomData[i].itemId);
          this.employee.roomIds.push(this.roomData[i].itemId);
        }
      }
    },

    /**
     * Kiểm tra xem giá trị có trong danh sách chọn không, nếu có thì trả về true, ngược lại false
     * Nếu true, dùng emit để gọi và set trạng thái chekcbox tương ứng là checked
     * @param {String} value - giá trị Id tương ứng với lựa chọn - cần parse sang Int khi xử lý
     * Auth: KhaiND (08/11/2022)
     */
    checkRoomIdSelected(value) {
      if (this.employee.roomIds == undefined) return false;
      return this.employee.roomIds.includes(value);
    },

    /**
     * Bắt giá trị subjectId của các môn được chọn khi check vào ô tương ứng
     * Auth: KhaiND (08/11/2022)
     */
    selectSubject(value) {
      var subjects = this.employee.subjectIds;
      // Nếu giá trị tương ứng đã được check (đã tồn tại trong danh sách chọn)
      if (subjects.includes(parseInt(value))) {
        // Loại bỏ nó đi (bấm vào thì uncheck)
        this.employee.subjectIds = subjects.filter((x) => {
          return x != parseInt(value);
        });
      }
      // Ngược lại, chwa check thì check (thêm giá trị tương ứng vào danh sách chọn)
      else {
        this.employee.subjectIds.push(parseInt(value));
      }
      // Kiểm tra xem nếu đã check hết các lựa chọn thì tự động tick ô Tất cả
      if (this.employee.subjectIds.length == this.subjectData.length) {
        this.checkAllSubject = true;
      } else {
        this.checkAllSubject = false;
      }
    },

    /**
     * Thực hiện chọn, bỏ chọn tất cả options của môn
     * Auth: KhaiND (08/11/2022)
     */
    selectAllSubject() {
      this.checkAllSubject = !this.checkAllSubject;
      this.employee.subjectIds = [];
      if (this.checkAllSubject) {
        for (var i = 0; i < this.subjectData.length; i++) {
          this.employee.subjectIds.push(this.subjectData[i].itemId);
        }
      }
    },

    /**
     * Kiểm tra xem giá trị có trong danh sách chọn không, nếu có thì trả về true, ngược lại false
     * Nếu true, dùng emit để gọi và set trạng thái chekcbox tương ứng là checked
     * @param {String} value - giá trị Id tương ứng với lựa chọn - cần parse sang Int khi xử lý
     * Auth: KhaiND (08/11/2022)
     */
    checkSubjectIdSelected(value) {
      if (this.employee.roomIds == undefined) return false;
      return this.employee.subjectIds.includes(value);
    },

    /**
     * Sự kiện khi bấm vào nút Đóng hoặc icon x trên Form thì ẩn pop-up Form đi
     * Auth: KhaiND (29/10/2022)
     */
    onClickClose() {
      this.$emit("hidePopup", null);
      console.log("CLICK CLOSE");
    },
    /**
     * Sự kiện bấm nút Lưu trên form chi tiết Employee
     * Auth: KhaiND (30/10/2022)
     */
    async onClickSave() {
      var me = this;
      // Hiển thị loader
      this.loadingStatus = true;
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
      }
      // Ẩn Loader
      this.loadingStatus = false;
      // Đưa ra thông báo hoàn thành và ẩn form
      // Đóng form
      this.$emit("hidePopup", null);
      // Reload và toast
      await this.$emit("reloadData", null);
    },
    /**
     * Sự kiện khi click vào option tương ứng tổ bộ môn thì chọn tổ bộ môn đó (theo departmentId)
     * @param {int} depId - departmnemtId định danh tổ bộ môn
     * Auth: KhaiND (05/11/2022)
     */
    cbooseDepartment(depId) {
      this.employee.departmentId = depId;
      this.selectDepartment();
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
            me.subjectData = me.subjectData.map(function (obj) {
              obj["itemId"] = obj["subjectId"]; // Assign new key
              delete obj["subjectId"]; // Delete old key
              obj["itemName"] = obj["subjectName"]; // Assign new key
              delete obj["subjectName"]; // Delete old key
              return obj;
            });
            console.log(me.subjectData);
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
            me.roomData = me.roomData.map(function (obj) {
              obj["itemId"] = obj["roomId"]; // Assign new key
              delete obj["roomId"]; // Delete old key
              obj["itemName"] = obj["roomName"]; // Assign new key
              delete obj["roomName"]; // Delete old key
              return obj;
            });
            console.log(me.roomData);
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
     * ẩn hiện select cho Tổ bộ môn
     */
    selectDepartment() {
      this.isShowDepartment = !this.isShowDepartment;
    },
  },
  // Khi khởi tạo thì load luôn dữ liệu cần thiết
  created() {
    this.onLoadDSR();
  },
  /**
   * Khi có thay đổi thì bắt giá trị của department được chọn và render tên tổ bộ môn tuuownng ứng
   * Auth: KhaiND (05/11/2022)
   */
  computed: {
    curDepName() {
      var depName = "";
      this.departmentData.forEach((opt) => {
        if (opt.departmentId == this.employee.departmentId)
          depName = opt.departmentName;
        console.log("Đã bắt được tổ bộ môn : " + depName);
      });
      return depName;
    },
  },
};
</script>

<style scoped>
</style>