<template lang="">
    <div class="m-popup-container isShowPopup" @keyup.esc="onClickClose">
        <div class="m-insert-form">
            <div class="insert-form--left">
                <button class="upload-avatar m-icon button-icon">
                    <div class="upload-avatar--text">{{formText.SelectImage}}</div>
                </button>
                <div v-if="employee.employeeName" class="form--fullname">{{employee.employeeName}}</div>
                <div v-else class="form--fullname">{{formText.Name}}</div>
                <div v-if="employee.employeeCode" class="form--e-id">{{employee.employeeCode}}</div>
                <div v-else class="form--e-id">{{formText.Code}}</div>
            </div>
            <div class="hor-line"></div>
            <div class="insert-form--right">
                <div class="form__header">
                    {{formTitle}}
                </div>
                <div class="form__content">
                    <div class="form__content--left">
                        <div class="m-input-container">
                            <label for="eId">{{formText.Code}} <span style="color: #FA3939;">*</span></label>
                            <input @blur="onBlurEmpCode" tabindex="1" v-model="employee.employeeCode" id="eId" type="text"
                              :class="{inputError:errorEmployee.CodeInvalid}" class="m-input" mrequired>
                            <div class="m-error-message" v-if="errorEmployee.CodeInvalid" style="left: 280px">
                                <div class="error-text">{{errorEmployee.CodeMsg}}</div>
                                <div class="error-arrow"></div>
                            </div>
                        </div>
                        <div class="m-input-container">
                            <label for="">{{formText.Phone}}</label>
                            <input @blur="onBlurPhoneNumber" tabindex="3" v-model="employee.employeePhoneNumber" type="text"
                              class="m-input" :class="{inputError:errorEmployee.PhoneInvalid}">
                            <div class="m-error-message" v-if="errorEmployee.PhoneInvalid" style="left: 280px">
                                <div class="error-text">{{errorEmployee.PhoneMsg}}</div>
                                <div class="error-arrow"></div>
                            </div>
                        </div>
                        <div class="m-input-container m-dropdown-container">
                            <label>{{formText.Department}}</label>
                            <div class="m-dropdown">
                                <input tabindex="5" @click="selectDepartment"
                                type="text" class="m-icon icon-down m-input"
                                v-model="curDepName"
                                readonly>
                                <div class="dropdown__option" :class="{showDown:isShowDepartment}">
                                    <div v-for="(item) in departmentData" :key="item.departmentID" @click="cbooseDepartment(item.departmentID)" class="dropdown__option--item">{{item.departmentName}}</div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="form__content--right">
                        <div class="m-input-container">
                            <label for="eName">{{formText.Name}}<span style="color: #FA3939;">*</span></label>
                            <input @blur="onBlurEmpName" ref='empNameInput' tabindex="2" v-model="employee.employeeName" type="text"
                            :class="{inputError:errorEmployee.NameEmpty}" class="m-input" mrequired />
                            <div class="m-error-message" v-if="errorEmployee.NameEmpty" style="left: 314px">
                                <div class="error-text">{{errorEmployee.NameMsg}}</div>
                                <div class="error-arrow"></div>
                            </div>
                        </div>
                        <div class="m-input-container">
                            <label for="">{{formText.Email}}</label>
                            <input @blur="onBlurEmail" tabindex="4" v-model="employee.employeeEmail" type="text"
                              class="m-input" :class="{inputError:errorEmployee.EmailInvalid}">
                            <div class="m-error-message" v-if="errorEmployee.EmailInvalid" style="left: 314px">
                                <div class="error-text">{{errorEmployee.EmailMsg}}</div>
                                <div class="error-arrow"></div>
                            </div>
                        </div>
                        <div class="m-input-container m-dropdown-container">
                            <label title="Quản lý theo môn">{{formText.Subject}}</label>
                            <MSelect :tabindex="6" :options="subjectData" :selectedIds="employee.subjectIDs" 
                              @selectOpt="selectSubject" @checkAll="selectAllSubject"
                              :checkSelected="checkSubjectIdSelected" :isCheckAll="checkAllSubject"/>
                        </div>
                    </div>
                    <div class="form__content--bottom">
                        <div class="m-input-container m-dropdown-container" style="width: 100%;">
                            <label title="Quản lý theo kho, phòng">{{formText.Room}}</label>
                            <MSelect :tabindex="7" :options="roomData" :selectedIds="employee.roomIDs" 
                              @selectOpt="selectRoom" @checkAll="selectAllRoom"
                              :checkSelected="checkRoomIdSelected" :isCheckAll="checkAllRoom"/>
                        </div>
                        <div class="m-checkbox-container">
                            <div class="m-checkbox">
                                <input tabindex="8" v-model="employee.employeeIsDeviceManager" class="checkbox-real" type="checkbox">
                                <div class="checkbox-pseudo"></div>
                            </div>
                            <label for="" style="margin-right: 20px;" title="Trình độ nghiệp vụ quản lý thiết bị">{{formText.DeviceManager}}</label>
                        </div>
                        <div class="m-checkbox-container">
                            <div id="boxEheckQuit" class="m-checkbox check--quit-date">
                                <input tabindex="9" v-model="employee.employeeWorkingStatus" class="checkbox-real" type="checkbox">
                                <div class="checkbox-pseudo"></div>
                            </div>
                            <label for="" style="margin-right: 20px;">{{formText.Working}}</label>
                        </div>
                        <div v-if="!employee.employeeWorkingStatus" id="eQuitDate" class="m-input-container m-date-input quit-date">
                            <label for="">{{formText.QuitDate}}</label>
                            <input tabindex="10" type="date" v-model="employee.employeeQuitDate" class="m-input m-input-date" placeholder="dd-mm-yyyyy" name="date">
                        </div>
                        <div v-else style="width: 240px; height: 32px;" class="m-input-container m-date-input quit-date"></div>
                    </div>
                </div>
                <div class="form__footer">
                    <button tabindex="11" @click="onClickClose" class="btn-close m-button m-btn-style2">{{buttonText.Close}}</button>
                    <button tabindex="12" @click="onClickSave" class="btn-save m-button m-btn-style1">{{buttonText.Save}}</button>
                </div>
            </div>
            <button title="Đóng" @click="onClickClose" class="m-icon m-icon-24 button-icon icon-close"></button>
        </div>
        <MLoader v-if="loadingStatus"/>
    </div>
</template>

<script>
import EmployeeText from "./../../utils/resources/employee";
import {BASE_URL} from "./../../utils/resources/common"
import CommonText from "./../../utils/resources/common"
import axios from "axios";
import {isEmail} from "./../../utils/formatData.js";
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
      buttonText: CommonText.Button,
      dialogText: CommonText.Dialog, // Chưa dùng
      toastMsg: CommonText.Toast.Message,
      formText: EmployeeText.Form, // Các text resource để trình bày trong nội dung của các thẻ trên Form
      employee: {}, // Đối tượng employee tương ứng dữ liệu đang render trên Form
      formTitle: EmployeeText.Form.FormTitlePost,
      departmentData: {}, // Dữ liệu tất cả lựa chọn của Tổ bộ môn
      subjectData: {}, //  Dữ liệu tất cả lựa chọn của Môn học
      roomData: {}, // Dữ liệu tất cả lựa chọn của Kho, phòng
      isShowDepartment: false, // Trạng thái hiển thị danh sách lựa chọn Tổ bộ môn
      curDpmName: null, // Tên Tổ bộ môn đang đươc chọn
      postMode: 1, // Trạng thái cất dữ liệu là POST (thêm mới) tương ứng 1 và ngược lại, PUT (chỉnh sửa) tương ứng 0
      checkAllRoom: false, // Trạng thái kiểm tra xem đã chọn tất cả Kho, phòng hay chưa
      checkAllSubject: false, // Trạng thái kiểm tra xem đã chọn tất cả Môn học hay chưa
      loadingStatus: false, // Trạng thái hiện thị mask Loader
      valid: false, // Dữ liệu cho action Lưu có họp lệ hay không
      errorEmployee: {
        CodeInvalid: false,
        NameEmpty: false,
        PhoneInvalid: false,
        EmailInvalid: false,
        CodeMsg: "",
        NameMsg: "",
        PhoneMsg: "",
        EmailMsg: ""
      }
    };
  },

  props: [
    "employeeSelectedId" /*Nhận Id của Cán bộ tương ứng đang được chọn từ ngoài Table để render dữ liệu lên Form*/,
  ],

  watch: {
    // CHƯA HOẠT ĐỘNG => Bỏ và mounted
  },

  methods: {
    /**
     * Xử lý khi có lỗi API
     * Author: KhaiND (26/11/2022)
     */
    catchAPI(response, msg400) {  // Xét các trường hợp API GET nữa
      switch (response.response.status) {
        case 400:
          // Ẩn Loader
          this.loadingStatus = false;
          this.$emit("showToast", msg400, 0);
          break;
        case 500:
          // Ẩn Loader
          this.loadingStatus = false;
          this.$emit("showToast", this.toastMsg.Error500, 0);
          break;
        case 404:
          // Ẩn Loader
          this.loadingStatus = false;
          this.$emit("showToast", this.toastMsg.ErrorDefault, 0);
          break;
        default:
          // Ẩn Loader
          this.loadingStatus = false;
          this.$emit("showToast", this.toastMsg.ErrorDefault, 0);
          break;
      }
    },

    /**
     * Theo dõi giá trị value tương ứng employeeId được chọn, nếu thay đổi thì hiện form tương ứng thông tin cán bộ
     * @params {int} Id của cán bộ tương ứng id của đối tượng khi click trong bảnh
     * Author: KhaiND (29/10/2022)
     */
    //employeeSelectedId: this.bindEmployee() WATCH
    async bindEmployee(value) {
      try {
        var me = this;
        if (value != null) {
          me.formTitle = me.formText.FormTitleEdit;
          // Hien thi Loader
          me.loadingStatus = true;
          // Call API, nho check Status code va dua ra thong bao
          var url = BASE_URL + "Employees/" + value;
          axios
            .get(url)
            .then(function (response) {
              me.employee = response.data;
              // Kiểm tra các Môn học nếu được chọn hết thì chủ động checkAll
              if(me.employee.subjectIDs.length == me.subjectData.length) {
                me.checkAllSubject = true;
              }
              // Kiểm tra các Kho, phòng nếu được chọn hết thì chủ động checkAll
              if(me.employee.roomIDs.length == me.roomData.length) {
                me.checkAllRoom = true;
              }
              // Ẩn Loader
              me.loadingStatus = false;
            })
            .catch(function (response) {
              console.log(response);
              // Ẩn Loader
              me.loadingStatus = false;
              // Đóng FORM trả lại giao diện bảng và thông báo lỗi
              me.onClickClose();
              me.$emit("showToast", me.toastMsg.ErrorDefault, 0);
            });
          // Sửa dữ liệu
          me.postMode = 0;
        } else {
          // Hien thi Loader
          me.loadingStatus = true;
          // Đổi form Title
          me.formTitle =  me.formText.FormTitlePost;
          // Khởi tạo đối tượng rỗng employee
          me.employee = {
            subjectIDs: [],
            roomIDs: [],
            employeeIsDeviceManager: false,
            employeeWorkingStatus: true,
          };
          //Gợi ý Số hiệu CB mới
          await axios //DỂ TRONG MOUNTED
            .get(BASE_URL + "Employees/newCode")
            .then(function (res) {
              me.employee.employeeCode = res.data;
              // Ẩn Loader
              me.loadingStatus = false;
            })
            .catch((error) => {
              console.log(error);
            });
          //Thêm mới
          me.postMode = 1;
        }
      }
      catch(error) {
        console.log(error);
        // Đóng FORM trả lại giao diện bảng và thông báo lỗi
        me.onClickClose();
        me.$emit("showToast", me.toastMsg.ErrorDefault, 0);
      }
    },

    /**
     * Sự kiện nhập xong EmployeeCode chuyển qua ô khác - kiểm tra rỗng
     * Auth: KhaiND (08/11/2022)
     */
    onBlurEmpCode() {
      try {
        // Kiểm tra đã nhập SHCB chưa
        if (!this.employee.employeeCode) {
          this.errorEmployee.CodeInvalid = true;
          this.errorEmployee.CodeMsg =  this.formText.ErrorMsg.CodeEmpty;     
        } else { // Kiểm tra xem có trùng mã không
          this.errorEmployee.CodeInvalid = false;
          // Gọi API để kiểm tra
          var url = BASE_URL + "Employees/checkDuplicateCode?employeeCode=" + this.employee.employeeCode;
          axios.get(url).then((response) => {
            if(response.data) {
              this.errorEmployee.CodeInvalid = true;
              this.errorEmployee.CodeMsg = this.formText.ErrorMsg.CodeDuplicate;
            }
            else {
              this.errorEmployee.CodeInvalid = false;
            }
          })
          .catch((error) => {
            console.log(error);
          });
          // Xử lý trường họp sửa, trùng mã với chính mình không thành vấn đề
        }
      }
      catch(error) {
        console.log(error);
      }
    },

    /**
     * Sự kiện nhập xong EmployeeName chuyển qua ô khác - kiểm tra rỗng
     * Author: KhaiND (09/11/2022)
     */
    onBlurEmpName() {
      try {
        // Kiểm tra xem đã nhập tên chưa
        if (!this.employee.employeeName) {
          this.errorEmployee.NameEmpty = true;
          this.errorEmployee.NameMsg = this.formText.ErrorMsg.NameEmpty;
        } else {
          this.errorEmployee.NameEmpty = false;
        }
      }
      catch(error) {
        console.log(error);
      }
    },

    /**
     * Sự kiện khi thoát forcus khỏi Department thì đóng các lựa chọn
     * Author: KhaiND (29/10/2022)
     */
    onBlurDepartment() {
      this.isShowDepartment = false;
    },

    /**
     * Action khi thoát forcus khỏi ô nhập Số điện thoại thì validate và hiển thị thông báo lỗi
     * Author: KhaiND (24/11/2022)
     */
    onBlurPhoneNumber() {
      try {
        if(this.employee.employeePhoneNumber.length < 7 || this.employee.employeePhoneNumber.length > 20) {
          if(this.employee.employeePhoneNumber.length == 0) {
            this.errorEmployee.PhoneInvalid = false;
          }
          else {
            this.errorEmployee.PhoneInvalid = true;
            this.errorEmployee.PhoneMsg = this.formText.ErrorMsg.PhoneInvalid;
          }
        }
      }
      catch(error) {
        console.log(error);
      }
    },

    /**
     * Action khi thoát forcus khỏi ô nhập Email thì validate và hiển thị thông báo lỗi
     * Author: KhaiND (24/11/2022)
     */
    onBlurEmail() {
      try {
        if(this.employee.employeeEmail) {
          if(!isEmail(this.employee.employeeEmail)) {
              this.errorEmployee.EmailInvalid = true;
              this.errorEmployee.EmailMsg = this.formText.ErrorMsg.EmailInvalid;
          }
          else {
            this.errorEmployee.EmailInvalid = false;
          }
        }
      }
      catch(error) {
        console.log(error);
      }
    },

    /**
     * Bắt giá trị roomID của các kho phòng được chọn khi check vào ô tương ứng
     * Author: KhaiND (07/11/2022)
     */
    selectRoom: function (value) {
      try {
        var rooms = this.employee.roomIDs;
        // Nếu giá trị tương ứng đã được check (đã tồn tại trong danh sách chọn)
        if (rooms != undefined && rooms.includes(value)) {
          // Loại bỏ nó đi (bấm vào thì uncheck)
          this.employee.roomIDs = rooms.filter((x) => {
            // ParseInt sang Guid
            return x != value;
          });
        }
        // Ngược lại, chwa check thì check (thêm giá trị tương ứng vào danh sách chọn)
        else {
          this.employee.roomIDs.push(value);
        }
        // Kiểm tra xem nếu đã check hết các lựa chọn thì tự động tick ô Tất cả
        if (this.employee.roomIDs.length == this.roomData.length) {
          this.checkAllRoom = true;
        } else {
          this.checkAllRoom = false;
        }
      }
      catch(error) {
        console.log(error);
      }
    },

    /**
     * Thực hiện chọn, bỏ chọn tất cả options của Kho, phòng
     * Author: KhaiND (08/11/2022)
     */
    selectAllRoom() {
      try {
        this.checkAllRoom = !this.checkAllRoom;
        this.employee.roomIDs = [];
        if (this.checkAllRoom) {
          for (var i = 0; i < this.roomData.length; i++) {
            this.employee.roomIDs.push(this.roomData[i].itemId);
          }
        }
      }
      catch(error) {
        console.log(error);
      }
    },

    /**
     * Kiểm tra xem giá trị có trong danh sách chọn không, nếu có thì trả về true, ngược lại false
     * Nếu true, dùng emit để gọi và set trạng thái chekcbox tương ứng là checked
     * @param {String} value - giá trị Id tương ứng với lựa chọn - cần parse sang Int khi xử lý
     * Author: KhaiND (08/11/2022)
     */
    checkRoomIdSelected(value) {
      if (this.employee.roomIDs == undefined) return false;
      return this.employee.roomIDs.includes(value);
    },

    /**
     * Bắt giá trị subjectID của các môn được chọn khi check vào ô tương ứng
     * Author: KhaiND (08/11/2022)
     */
    selectSubject(value) {
      try {
        var subjects = this.employee.subjectIDs;
        // Nếu giá trị tương ứng đã được check (đã tồn tại trong danh sách chọn)
        if (subjects != undefined && subjects.includes(value)) {
          // Loại bỏ nó đi (bấm vào thì uncheck)
          this.employee.subjectIDs = subjects.filter((x) => {
            // ParseInt sang Guid
            return x != value;
          });
        }
        // Ngược lại, chwa check thì check (thêm giá trị tương ứng vào danh sách chọn)
        else {
          this.employee.subjectIDs.push(value);
        }
        // Kiểm tra xem nếu đã check hết các lựa chọn thì tự động tick ô Tất cả
        if (this.employee.subjectIDs.length == this.subjectData.length) {
          this.checkAllSubject = true;
        } else {
          this.checkAllSubject = false;
        }
      }
      catch(error) {
        console.log(error);
      }
    },

    /**
     * Thực hiện chọn, bỏ chọn tất cả options của môn
     * Author: KhaiND (08/11/2022)
     */
    selectAllSubject() {
      try {
        this.checkAllSubject = !this.checkAllSubject;
        this.employee.subjectIDs = [];
        if (this.checkAllSubject) {
          for (var i = 0; i < this.subjectData.length; i++) {
            this.employee.subjectIDs.push(this.subjectData[i].itemId);
          }
        }
      }
      catch(error) {
        console.log(error);
      }
    },

    /**
     * Kiểm tra xem giá trị có trong danh sách chọn không, nếu có thì trả về true, ngược lại false
     * Nếu true, dùng emit để gọi và set trạng thái chekcbox tương ứng là checked
     * @param {String} value - giá trị Id tương ứng với lựa chọn - cần parse sang Int khi xử lý
     * Author: KhaiND (08/11/2022)
     */
    checkSubjectIdSelected(value) {
      if (this.employee.roomIDs == undefined) return false;
      return this.employee.subjectIDs.includes(value);
    },

    /**
     * Sự kiện khi bấm vào nút Đóng hoặc icon x trên Form thì ẩn pop-up Form đi
     * Auth: KhaiND (29/10/2022)
     */
    onClickClose() {
      this.$emit("hidePopup", null);
    },

    /**
     * Xử lý khi gọi API cất dữ liệu thành công
     * Author: KhaiND (26/11/2022)
     */
    thenSaveAPI(response, msg) {
      // response chưa dùng đến
      // Thông báo thành công
      this.$emit("showToast", msg, 1);
      // Ẩn Loader
      this.loadingStatus = false;
      // Đóng form
      this.$emit("hidePopup", null);
      // Reload dữ liệu
      this.$emit("reloadData", null);
    },

    /**
     * Sự kiện bấm nút Lưu trên form chi tiết Employee
     * Author: KhaiND (30/10/2022)
     */
    async onClickSave() {
      try {
        var me = this;
        // Hiển thị loader
        this.loadingStatus = true;
  
        // Validate dữ liệu
        me.valid = ((!me.errorEmployee.CodeInvalid) && (!me.errorEmployee.NameEmpty) && (!me.errorEmployee.PhoneInvalid) && (!me.errorEmployee.EmailInvalid))
  
        // Build object thông tin cán bộ
        if (me.valid) {
          // Gọi API thực hiện lưu dữ liệu
          if (this.postMode == 1) {
            // POST
            await axios
              .post(BASE_URL + "Employees", me.employee)
              .then((response) => me.thenSaveAPI(response, me.toastMsg.Employee.InsertSuccess))
              .catch((respomse) => me.catchAPI(respomse, me.toastMsg.InvalidInsert));
          } else {
            // PUT
            var pUrl = BASE_URL + "Employees/" + me.employeeSelectedId;
            await axios
              .put(pUrl, me.employee)
              .then((response) => me.thenSaveAPI(response, me.toastMsg.Employee.UpdateSuccess))
              .catch((respomse) => me.catchAPI(respomse, me.toastMsg.InvalidUpdate));
          }
        }
        else {
          me.loadingStatus = false;
          if(this.postMode) {
            me.$emit("showToast", me.toastMsg.InvalidInsert, 0);
          }
          else {
            me.$emit("showToast", me.toastMsg.InvalidUpdate, 0);
          }
        }
      }
      catch(error) {
        console.log(error);
        this.loadingStatus = false;
        this.$emit("showToast", me.toastMsg.ErrorDefault, 0);
      }
    },

    /**
     * Sự kiện khi click vào option tương ứng tổ bộ môn thì chọn tổ bộ môn đó (theo departmentID)
     * @param {int} depId - departmnemtId định danh tổ bộ môn
     * Author: KhaiND (05/11/2022)
     */
    cbooseDepartment(depId) {
      try {
        this.employee.departmentID = depId;
        this.selectDepartment();
      }
      catch(error) {
        console.log(error);
      }
    },

    /**
     * Sự kiện ẩn hiện select cho Tổ bộ môn khi bấm vào input tương ứng
     * Author: KhaiND (02/11/2022)
     */
    selectDepartment() {
      this.isShowDepartment = !this.isShowDepartment;
    },

    /**
     * Gọi API lấy danh sách tổ bộ môn, môn học, kho phòng
     * Author: KhaiND (30/10/2022)
     */
    onLoadDSR() {
      var me = this;
      // Lấy danh sách môn
      try {
        axios
          .get(BASE_URL + "Subjects")
          .then(function (response) {
            me.subjectData = response.data;
            // Đối các trường ID và Name thành itemID và itemName tiện cho thao tác với MSelect
            me.subjectData = me.subjectData.map(function (obj) {
              obj["itemId"] = obj["subjectID"]; // Assign new key
              delete obj["subjectID"]; // Delete old key
              obj["itemName"] = obj["subjectName"]; // Assign new key
              delete obj["subjectName"]; // Delete old key
              return obj;
            });
          })
          .catch(function (response) {
            console.log(response);
            this.$emit("showToast", this.toastMsg.ErrorDefault, 0);
            this.onClickClose();
            this.loadingStatus = false;
          });
      } catch (error) {
        console.log(error);
      }
      // Lấy danh sách Kho phòng
      try {
        axios
          .get(BASE_URL + "Rooms")
          .then(function (response) {
            me.roomData = response.data;
            // Đối các trường ID và Name thành itemID và itemName tiện cho thao tác với MSelect
            me.roomData = me.roomData.map(function (obj) {
              obj["itemId"] = obj["roomID"]; // Assign new key
              delete obj["roomID"]; // Delete old key
              obj["itemName"] = obj["roomName"]; // Assign new key
              delete obj["roomName"]; // Delete old key
              return obj;
            });
          })
          .catch(function (response) {
            console.log(response);
            this.$emit("showToast", this.toastMsg.ErrorDefault, 0);
            this.onClickClose();
            this.loadingStatus = false;
          });
      } catch (error) {
        console.log(error);
      }
      // Lấy danh sách Tổ bộ môn
      try {
        axios
          .get(BASE_URL + "Departments")
          .then(function (response) {
            me.departmentData = response.data;
          })
          .catch(function (response) {
            console.log(response);
            this.$emit("showToast", this.toastMsg.ErrorDefault, 0);
            this.onClickClose();
            this.loadingStatus = false;
          });
      } catch (error) {
        console.log(error);
        this.$emit("showToast", "Có lỗi xảy ra!", 0);
        this.onClickClose();
        this.loadingStatus = false;
      }
    },
  },

  // Khi khởi tạo thì load luôn dữ liệu cần thiết
  created() {
    // Hiện Loader => tắt khi đến các hàm xử lý tương ứng với mode thêm hoặc sửa ở watch
    //this.loadingStatus = true;  //======================================= GIỮ
    // Gọi hàm load dữ liệu
    this.onLoadDSR();
    this.bindEmployee(this.employeeSelectedId);
  },

  // Auto forcus vào ô input họ tên khi mở form
  mounted() {
    // this.bindEmployee(this.employeeSelectedId);
    this.$nextTick(() => this.$refs.empNameInput.focus());
  },

  /**
   * Khi có thay đổi thì bắt giá trị của department được chọn và render tên tổ bộ môn tuuownng ứng
   * Author: KhaiND (05/11/2022)
   */
  computed: {
    curDepName() {
      try {
        var depName = "";
        if (this.departmentData != undefined || this.departmentData != null) {
          this.departmentData.forEach((opt) => {
            if (opt.departmentID == this.employee.departmentID)
              depName = opt.departmentName;
          });
        }
        return depName;
      }
      catch (error) {
        console.log(error);
        return null;
      }
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
  background-color: #ffffff;
  display: flex;
  color: #4d4f5c;
}

.insert-form--left {
  height: 100%;
  display: flex;
  flex-direction: column;
  align-items: center;
  box-sizing: border-box;
  padding: 24px;
}

.insert-form--left .upload-avatar {
  background-image: url(../../assets/Images/no_avatar.png);
  background-color: #cccccc;
  background-position: center;
  background-size: contain;
  position: relative;
  width: 140px;
  height: 190px;
}
.insert-form--left .upload-avatar--text {
  background-color: #03ae66;
  color: #ffffff;
  height: 32px;
  width: 100%;
  display: flex;
  justify-content: center;
  align-items: center;
  position: absolute;
  bottom: 0px;
}
.insert-form--left .upload-avatar:hover > .upload-avatar--text {
  background-color: #02bf70;
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
  background-color: #cccccc;
}

.insert-form--right {
  display: flex;
  flex-direction: column;
}

.form__header {
  font-size: 16px;
  font-family: OpenSansSemiBold;
  padding: 24px;
}

.form__content {
  display: grid;
  grid-template-columns: auto auto;
  grid-template-rows: auto auto;
}

.form__content .m-tag {
  /*Bor rieeg no ra */
  color: #000000;
}

.form__content--left {
  margin-left: 24px;
  display: flex;
  flex-direction: column;
}
.form__content--left label {
  min-width: 100px;
  margin-right: 12px;
}
.form__content--left .m-input-container .m-input,
.form__content--left .dropdown__option {
  width: 160px;
}

.form__content--right {
  /* min-width: 336px; */
  margin-right: 8px;
}
.form__content--right label {
  min-width: 80px;
  margin-right: 12px;
}
.form__content--right .m-input,
.form__content--right .m-dropdown {
  min-width: 210px;
}

.form__content--bottom {
  grid-column-start: 1;
  grid-column-end: 3;
  display: flex;
  align-items: center;
  flex-wrap: wrap;
  margin-left: 24px;
  margin-right: 8px;
  justify-content: space-between;
}
.form__content--bottom .m-dropdown-container label {
  min-width: 100px;
}
.form__content--bottom label {
  margin-right: 12px;
}

.form__footer {
  height: 64px;
  box-sizing: border-box;
  display: flex;
  align-items: center;
  justify-content: right;
  padding-right: 24px;
  padding-bottom: 24px;
}

.m-insert-form .icon-close {
  position: absolute;
  top: 12px;
  right: 12px;
}
</style>