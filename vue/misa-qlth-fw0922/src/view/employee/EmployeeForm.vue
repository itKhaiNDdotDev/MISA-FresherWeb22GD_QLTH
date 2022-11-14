<template lang="">
    <div class="m-popup-container isShowPopup" @keyup.esc="onClickClose">
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
                            <input @blur="onBlurEmpCode" tabindex="1" v-model="employee.employeeCode" id="eId" type="text"
                            :class="{inputError:eCodeInvalid}" class="m-input" mrequired>
                            <div class="m-error-message" v-if="eCodeInvalid" style="left: 280px">
                                <div class="error-text">Mã cán bộ không được bỏ trống.</div>
                                <div class="error-arrow"></div>
                            </div>
                        </div>
                        <div class="m-input-container">
                            <label for="">Số điện thoại</label>
                            <input tabindex="3" v-model="employee.employeePhoneNumber" type="text" class="m-input">
                        </div>
                        <div class="m-input-container m-dropdown-container">
                            <label>Tổ bộ môn</label>
                            <div class="m-dropdown">
                                <input tabindex="5" @click="selectDepartment"
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
                            <input @blur="onBlurEmpName" ref='empNameInput' tabindex="2" v-model="employee.employeeName" type="text"
                            :class="{inputError:eNameInvalid}" class="m-input" mrequired />
                            <div class="m-error-message" v-if="eNameInvalid" style="left: 314px">
                                <div class="error-text">Họ và tên không được bỏ trống.</div>
                                <div class="error-arrow"></div>
                            </div>
                        </div>
                        <div class="m-input-container">
                            <label for="">Email</label>
                            <input tabindex="4" v-model="employee.employeeEmail" type="text" class="m-input">
                        </div>
                        <div class="m-input-container m-dropdown-container">
                            <label title="Quản lý theo môn">QL theo môn</label>
                            <MSelect :tabindex="6" :options="subjectData" :selectedIds="employee.subjectIds" 
                              @selectOpt="selectSubject" @checkAll="selectAllSubject"
                              :checkSelected="checkSubjectIdSelected" :isCheckAll="checkAllSubject"/>
                        </div>
                    </div>
                    <div class="form__content--bottom">
                        <div class="m-input-container m-dropdown-container" style="width: 100%;">
                            <label title="Quản lý theo kho, phòng">QL kho, phòng</label>
                            <MSelect :tabindex="7" :options="roomData" :selectedIds="employee.roomIds" 
                              @selectOpt="selectRoom" @checkAll="selectAllRoom"
                              :checkSelected="checkRoomIdSelected" :isCheckAll="checkAllRoom"/>
                        </div>
                        <div class="m-checkbox-container">
                            <div class="m-checkbox">
                                <input tabindex="8" v-model="employee.isDeviceManager" class="checkbox-real" type="checkbox">
                                <div class="checkbox-pseudo"></div>
                            </div>
                            <label for="" style="margin-right: 20px;" title="Trình độ nghiệp vụ quản lý thiết bị">Trình độ
                                nghiệp vụ QLTB</label>
                        </div>
                        <div class="m-checkbox-container">
                            <div id="boxEheckQuit" class="m-checkbox check--quit-date">
                                <input tabindex="9" v-model="employee.workingStatus" class="checkbox-real" type="checkbox" checked>
                                <div class="checkbox-pseudo"></div>
                            </div>
                            <label for="" style="margin-right: 20px;">Đang làm việc</label>
                        </div>
                        <div v-if="!employee.workingStatus" id="eQuitDate" class="m-input-container m-date-input quit-date">
                            <label for="">Ngày nghỉ việc</label>
                            <input tabindex="10" type="date" v-model="employee.quitDate" class="m-input m-input-date" placeholder="dd-mm-yyyyy" name="date">
                        </div>
                        <div v-else style="width: 240px; height: 32px;" class="m-input-container m-date-input quit-date"></div>
                    </div>
                </div>
                <div class="form__footer">
                    <button tabindex="11" @click="onClickClose" class="btn-close m-button m-btn-style2">Đóng</button>
                    <button tabindex="12" @click="onClickSave" class="btn-save m-button m-btn-style1">Lưu</button>
                </div>
            </div>
            <button title="Đóng" @click="onClickClose" class="m-icon m-icon-24 button-icon icon-close"></button>
        </div>
        <MLoader v-if="loadingStatus"/>
    </div>
</template>

<script>
import axios from "axios";
import MSelect from "../../components/base/MSelect.vue";
import MLoader from "../../components/base/MLoader.vue";

export default {
  name: "EmployeeForm",
  components: {
    MSelect,
    MLoader,
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
      loadingStatus: false,
      valid: false,
      eCodeInvalid: false,
      eNameInvalid: false
    };
  },
  props: ["employeeSelectedId"],
  watch: {  // CHƯA HOẠT ĐỘNG => Bỏ và mounted
  },

  methods: {
    /**
     * Theo dõi giá trị value tương ứng employeeId được chọn, nếu thay đổi thì hiện form tương ứng thông tin cán bộ
     * @params {int} Id của cán bộ tương ứng id của đối tượng khi click trong bảnh
     * Auth: KhaiND (29/10/2022)
     */
    //employeeSelectedId: this.bindEmployee()
    async bindEmployee(value) {
      var me = this;
      if (value != null) {
        me.formTitle = "Chỉnh sửa hồ sơ Cán bộ, giáo viên";
        // Hien thi Loader
        me.loadingStatus = true;
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
            console.log(me.employee);
            // Ẩn Loader
            me.loadingStatus = false;
          })
          .catch(function (response) {
            console.log(response);
            // Ẩn Loader
            me.loadingStatus = false;
          });
        // Sửa dữ liệu
        me.postMode = 0;
      } else {
        // Hien thi Loader
        me.loadingStatus = true;
        // Đổi form Title
        me.formTitle = "Thêm hồ sơ Cán bộ, giáo viên";
        // Khởi tạo đối tượng rỗng employee
        me.employee = { workingStatus: true, subjectIds: [], roomIds: [] };
        //Gợi ý Số hiệu CB mới
        await axios //DỂ TRONG MOUNTED
          .get("https://localhost:44344/api/Employees/newcode")
          .then(function (res) {
            me.employee.employeeCode = res.data;
            // Ẩn Loader
            me.loadingStatus = false;
          });
        //Thêm mới
        me.postMode = 1;
      }
      console.log("Da bind duoc Id ve Form: " + value);
      // if(me.employee.roomIds.length == me.roomData.length) {
      //   me.checkAllRoom = true;
      // }
    },

    /**
     * Sự kiện nhập xong EmployeeCode chuyển qua ô khác - kiểm tra rỗng
     * Auth: KhaiND (08/11/2022)
     */
    onBlurEmpCode() {
      if (!this.employee.employeeCode) {
        this.eCodeInvalid = true;
      } else {
        this.eCodeInvalid = false;
      }
    },

    /**
     * Sự kiện nhập xong EmployeeName chuyển qua ô khác - kiểm tra rỗng
     * Auth: KhaiND (09/11/2022)
     */
    onBlurEmpName() {
      if (!this.employee.employeeName) {
        this.eNameInvalid = true;
      } else {
        this.eNameInvalid = false;
      }
    },

    onBlurDepartment() {
      this.isShowDepartment = false;
      console.log("Ẩn DEP");
    },

    /**
     * Bắt giá trị roomId của các kho phòng được chọn khi check vào ô tương ứng
     * Auth: KhaiND (07/11/2022)
     */
    selectRoom: function (value) {
      var rooms = this.employee.roomIds;
      // Nếu giá trị tương ứng đã được check (đã tồn tại trong danh sách chọn)
      if (rooms != undefined && rooms.includes(parseInt(value))) {
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
      if (subjects != undefined && subjects.includes(parseInt(value))) {
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
     * Sự kiện bấm nút Lưu trên form chi tiết Employee ============= CẦN XEM LẠI VALIDATE
     * Auth: KhaiND (30/10/2022)
     */
    async onClickSave() {
      var me = this;
      // Hiển thị loader
      this.loadingStatus = true;
      // Validate dữ liệu
      if (me.employee.employeeCode == "") {
        // Hiển thị thông báo bắt buộc
        me.eCodeInvalid = true;
        this.loadingStatus = false;
      } else if (me.employeeName == "") {
        me.eNameInvalid = true;
        // Ẩn Loader
        this.loadingStatus = false;
      } else {
        me.valid = true;
        me.eCodeInvalid = false;
        me.eNameInvalid = false;
      }
      // Build object thông tin cán bộ
      if (me.valid) {
        // Gọi API thực hiện lưu dữ liệu
        if (this.postMode == 1) {
          console.log("POST");
          await axios
            .post("https://localhost:44344/api/Employees", me.employee)
            .then(function () {
              console.log("SUCCESS");
            })
            .catch(function () {});
          this.$emit("showToast", "Thêm mới thành công cán bộ, giáo viên");
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
          this.$emit("showToast", "Cập nhật thành công cán bộ, giáo viên");
        }
        // Ẩn Loader
        this.loadingStatus = false;
        // Đưa ra thông báo hoàn thành và ẩn form
        // Đóng form
        this.$emit("hidePopup", null);
        // Reload và toast
        await this.$emit("reloadData", null);
      }
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
     * Sự kiện ẩn hiện select cho Tổ bộ môn khi bấm vào input tương ứng
     * AuthL KhaiND (02/11/2022)
     */
    selectDepartment() {
      this.isShowDepartment = !this.isShowDepartment;
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
  },

  // Khi khởi tạo thì load luôn dữ liệu cần thiết
  created() {
    // Hiện Loader => tắt khi đến các hàm xử lý tương ứng với mode thêm hoặc sửa ở watch
    //this.loadingStatus = true;  //======================================= GIỮ
    // Gọi hàm load dữ liệu
    this.onLoadDSR();
  },

  mounted() {
    this.bindEmployee(this.employeeSelectedId);
    this.$nextTick(() => this.$refs.empNameInput.focus())
    console.log(`this.$refs.empNameInput`, this.$refs.empNameInput);
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
  .m-insert-form {
    position: absolute;
    top: 50%;
    left: 50%;
    transform: translate(-50%, -50%);
    background-color: #FFFFFF;
    display: flex;
    color: #4D4F5C;
}

.insert-form--left {
    height: 100%;
    width: 160px;
    display: flex;
    flex-direction: column;
    align-items: center;
    padding: 18px;
    box-sizing: border-box;
}

.insert-form--left .upload-avatar {
    background-image: url(../../assets/Images/no_avatar.png);
    background-color: #CCCCCC;
    background-position: center;
    background-size: contain;
    position: relative;
    width: 140px;
    height: 190px;
}
.insert-form--left .upload-avatar--text {
    background-color: #03AE66;
    color: #FFFFFF;
    height: 32px;
    width: 100%;
    display: flex;
    justify-content: center;
    align-items: center;
    position: absolute;
    bottom: 0px;
}
.insert-form--left .upload-avatar:hover > .upload-avatar--text {
    background-color: #02BF70;
}
.insert-form--left .form--fullname {
    margin: 12px 0px 4px 0px;
    font-size: 14px;
    text-align: center;
}

.m-insert-form .hor-line {
    width: 1px;
    height: 320px;
    align-self: center;
    background-color: #CCCCCC;
}

.insert-form--right {
    display: flex;
    flex-direction: column;
}

.form__header {
    font-size: 16px;
    font-family: OpenSansSemiBold;
    padding: 16px;
}

.form__content {
    display: grid;
    grid-template-columns: auto auto;
    grid-template-rows: auto auto;
}

.form__content .m-tag { /*Bor rieeg no ra */
    color: #000000;
}

.form__content--left {
    margin-left: 16px;
    display: flex;
    flex-direction: column;
}
.form__content--left .m-input-container .m-input, .form__content--left .dropdown__option {
    width: 160px;
    margin-left: 16px;
}

.form__content--right .m-input, .form__content--right .m-dropdown {
    min-width: 210px;
}

.form__content--bottom {
    grid-column-start: 1;
    grid-column-end: 3;
    display: flex;
    align-items: center;
    flex-wrap: wrap;
    margin-left: 16px;
    justify-content: space-between;
}

.form__content--bottom .m-dropdown {
    margin-left: 16px;
}

.form__footer {
    height: 64px;
    box-sizing: border-box;
    display: flex;
    align-items: center;
    justify-content: right;
    padding-right:  26px;
}

.m-insert-form .icon-close {
    position: absolute;
    top: 12px;
    right: 12px;
}
</style>