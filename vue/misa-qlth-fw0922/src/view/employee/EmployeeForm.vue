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
                            <button ref='focusMe' tabindex="1" style="margin-left: -16px; opacity: 0; width: 0px"></button>
                            <input @blur="validateCode" tabindex="1" v-model="employee.employeeCode" id="eId" type="text"
                              ref='empCodeInput' :class="{inputError:errorEmployee.CodeInvalid}" class="m-input" mrequired>
                            <div class="m-error-message" v-if="errorEmployee.CodeInvalid" style="left: 280px">
                                <div class="error-text">{{errorEmployee.CodeMsg}}</div>
                                <div class="error-arrow"></div>
                            </div>
                        </div>
                        <div class="m-input-container">
                            <label for="">{{formText.Phone}}</label>
                            <input @blur="validatePhone" tabindex="3" v-model="employee.employeePhoneNumber" type="text"
                              ref='empPhoneInput' class="m-input" :class="{inputError:errorEmployee.PhoneInvalid}">
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
                            <input @blur="validateName" tabindex="2" v-model="employee.employeeName" type="text"
                              ref='empNameInput' :class="{inputError:errorEmployee.NameEmpty}" class="m-input" mrequired />
                            <div class="m-error-message" v-if="errorEmployee.NameEmpty" style="left: 314px">
                                <div class="error-text">{{errorEmployee.NameMsg}}</div>
                                <div class="error-arrow"></div>
                            </div>
                        </div>
                        <div class="m-input-container">
                            <label for="">{{formText.Email}}</label>
                            <input @blur="validateEmail" tabindex="4" v-model="employee.employeeEmail" type="text"
                              ref='empEmailInput' class="m-input" :class="{inputError:errorEmployee.EmailInvalid}">
                            <div class="m-error-message" v-if="errorEmployee.EmailInvalid" style="left: 314px">
                                <div class="error-text">{{errorEmployee.EmailMsg}}</div>
                                <div class="error-arrow"></div>
                            </div>
                        </div>
                        <div class="m-input-container m-dropdown-container">
                            <label title="Qu???n l?? theo m??n">{{formText.Subject}}</label>
                            <MSelect :tabindex="6" :options="subjectData" :selectedIds="employee.subjectIDs" 
                              @selectOpt="selectSubject" @checkAll="selectAllSubject"
                              :checkSelected="checkSubjectIdSelected" :isCheckAll="checkAllSubject"/>
                        </div>
                    </div>
                    <div class="form__content--bottom">
                        <div class="m-input-container m-dropdown-container" style="width: 100%;">
                            <label title="Qu???n l?? theo kho, ph??ng">{{formText.Room}}</label>
                            <MSelect :tabindex="7" :options="roomData" :selectedIds="employee.roomIDs" 
                              @selectOpt="selectRoom" @checkAll="selectAllRoom"
                              :checkSelected="checkRoomIdSelected" :isCheckAll="checkAllRoom"/>
                        </div>
                        <div class="m-checkbox-container">
                            <div class="m-checkbox">
                                <input tabindex="8" v-model="employee.employeeIsDeviceManager" class="checkbox-real" type="checkbox">
                                <div class="checkbox-pseudo"></div>
                            </div>
                            <label for="" style="margin-right: 20px;" title="Tr??nh ????? nghi???p v??? qu???n l?? thi???t b???">{{formText.DeviceManager}}</label>
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
                    <button ref="btnSave" @keydown.tab="onTabEndForm" tabindex="12" @click="onClickSave" class="btn-save m-button m-btn-style1">{{buttonText.Save}}</button>
                </div>
            </div>
            <button title="????ng" @click="onClickClose" class="m-icon m-icon-24 button-icon icon-close"></button>
        </div>
        <MLoader v-if="loadingStatus"/>
    </div>
    <div class="m-popup-container mask-upper" :class="{showD:isShowDialog}">
      <MsDialog @onConfirm="onClickSave" @onClose="closeMsDialog" @onClickBtn2="onClickDialogRefuse" :dialogMessage="dialogMessage"
          :btn2Show="true" :btn2Text="dialogBtnRefuseText" :btnDefaultText="dialogBtnDefaultText"/>
    </div>
</template>

<script>
import EmployeeText from "./../../utils/resources/employee";
import {BASE_URL} from "./../../utils/resources/common"
import CommonText from "./../../utils/resources/common"
import axios from "axios";
import { isPhone, isEmail } from "./../../utils/formatData.js";
import MSelect from "../../components/base/MSelect.vue";
import MLoader from "../../components/base/MLoader.vue";
import MsDialog from "./../../components/base/MsDialog.vue";
import { ResultStatus } from "./../../utils/enums/status";

export default {
  name: "EmployeeForm",
  components: {
    MSelect,
    MLoader,
    MsDialog
  },

  data() {
    return {
      buttonText: CommonText.Button,
      dialogText: CommonText.Dialog, // Ch??a d??ng
      toastMsg: CommonText.Toast.Message,
      formText: EmployeeText.Form, // C??c text resource ????? tr??nh b??y trong n???i dung c???a c??c th??? tr??n Form
      employee: {}, // ?????i t?????ng employee t????ng ???ng d??? li???u ??ang render tr??n Form
      formTitle: EmployeeText.Form.FormTitlePost,
      departmentData: {}, // D??? li???u t???t c??? l???a ch???n c???a T??? b??? m??n
      subjectData: {}, //  D??? li???u t???t c??? l???a ch???n c???a M??n h???c
      roomData: {}, // D??? li???u t???t c??? l???a ch???n c???a Kho, ph??ng
      isShowDepartment: false, // Tr???ng th??i hi???n th??? danh s??ch l???a ch???n T??? b??? m??n
      curDpmName: null, // T??n T??? b??? m??n ??ang ??????c ch???n
      postMode: 1, // Tr???ng th??i c???t d??? li???u l?? POST (th??m m???i) t????ng ???ng 1 v?? ng?????c l???i, PUT (ch???nh s???a) t????ng ???ng 0
      checkAllRoom: false, // Tr???ng th??i ki???m tra xem ???? ch???n t???t c??? Kho, ph??ng hay ch??a
      checkAllSubject: false, // Tr???ng th??i ki???m tra xem ???? ch???n t???t c??? M??n h???c hay ch??a
      loadingStatus: false, // Tr???ng th??i hi???n th??? mask Loader
      valid: false, // D??? li???u cho action L??u c?? h???p l??? hay kh??ng
      errorEmployee: {
        CodeInvalid: false,
        NameEmpty: false,
        PhoneInvalid: false,
        EmailInvalid: false,
        CodeMsg: "",
        NameMsg: "",
        PhoneMsg: "",
        EmailMsg: ""
      },
      curEmployee: {}, // D??? li???u CB, GV load t??? CSDL v?? ch??a b??? thay ?????i do ng?????i udngf nh???p
      isShowDialog: false, // Hi???n th??? dialog confirm thay ?????i d??? li???u
      dialogMessage: "",
      dialogBtnDefaultText: CommonText.Button.Save,
      dialogBtnRefuseText: CommonText.Button.Refuse,
    };
  },

  props: [
    "employeeSelectedId" /*Nh???n Id c???a C??n b??? t????ng ???ng ??ang ???????c ch???n t??? ngo??i Table ????? render d??? li???u l??n Form*/,
  ],

  watch: {
    // CH??A HO???T ?????NG => B??? v?? mounted
  },

  methods: {
    /**
     * Khi Tab v??o control cu???i Form th?? forcus v??o ?? ?????u ti??n c???a Form
     * Author: KhaiND (29/11/2022)
     */
    onTabEndForm() {
      this.$nextTick(() => this.$refs.focusMe.focus());
    },

    /**
     * Khi Shif+Tab v??o control ?????u ti??n c???a Form th?? quay v??? control cu???i form
     * Author: KhaiND (29/11/2022)
     */
    onShifTabBeginForm() {
      this.$nextTick(() => this.$refs.btnSave.focus());
    },

    /**
     * X??? l?? khi c?? l???i API
     * Author: KhaiND (26/11/2022)
     */
    catchAPI(response, msg400) {  // X??t c??c tr?????ng h???p API GET n???a
      switch (response.response.status) {
        case 400:
          // ???n Loader
          this.loadingStatus = false;
          this.$emit("showToast", msg400, ResultStatus.FAIL);
          break;
        case 500:
          // ???n Loader
          this.loadingStatus = false;
          this.$emit("showToast", this.toastMsg.Error500, ResultStatus.FAIL);
          break;
        case 404:
          // ???n Loader
          this.loadingStatus = false;
          this.$emit("showToast", this.toastMsg.ErrorDefault, ResultStatus.FAIL);
          break;
        default:
          // ???n Loader
          this.loadingStatus = false;
          this.$emit("showToast", this.toastMsg.ErrorDefault, ResultStatus.FAIL);
          break;
      }
    },

    /**
     * Theo d??i gi?? tr??? value t????ng ???ng employeeId ???????c ch???n, n???u thay ?????i th?? hi???n form t????ng ???ng th??ng tin c??n b???
     * @params {int} Id c???a c??n b??? t????ng ???ng id c???a ?????i t?????ng khi click trong b???nh
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
          // ????? d??? li???u t??? API v??o curEmployee ????? ph???c v??? ki???m tra thay ?????i
          axios.get(url).then(function(response) {
            me.curEmployee = response.data;
          })
          .catch((response) => {
            console.log(response);
          });

          axios.get(url)
            .then(function (response) {
              me.employee = response.data;
              // ?????nh d???ng ng??y
              if(me.employee.employeeQuitDate) {
                me.employee.employeeQuitDate = me.employee.employeeQuitDate.substring(0, 10);
              }
              // Ki???m tra c??c M??n h???c n???u ???????c ch???n h???t th?? ch??? ?????ng checkAll
              if(me.employee.subjectIDs.length == me.subjectData.length) {
                me.checkAllSubject = true;
              }
              // Ki???m tra c??c Kho, ph??ng n???u ???????c ch???n h???t th?? ch??? ?????ng checkAll
              if(me.employee.roomIDs.length == me.roomData.length) {
                me.checkAllRoom = true;
              }
              // ???n Loader
              me.loadingStatus = false;
            })
            .catch(function (response) {
              console.log(response);
              // ???n Loader
              me.loadingStatus = false;
              // ????ng FORM tr??? l???i giao di???n b???ng v?? th??ng b??o l???i
              me.onClickClose();
              me.$emit("showToast", me.toastMsg.ErrorDefault, ResultStatus.FAIL);
            });
          // S???a d??? li???u
          me.postMode = 0;
        } else {
          // Hien thi Loader
          me.loadingStatus = true;
          // ?????i form Title
          me.formTitle =  me.formText.FormTitlePost;

          // Kh???i t???o ?????i t?????ng ????? check thay ?????i
          me.curEmployee = {
            subjectIDs: [],
            roomIDs: [],
            employeeIsDeviceManager: false,
            employeeWorkingStatus: true,
          };
          await axios.get(BASE_URL + "Employees/newCode")
            .then(function (res) {
              me.curEmployee.employeeCode = res.data;
            })
            .catch((error) => {
              console.log(error);
            });

          // Kh???i t???o ?????i t?????ng r???ng employee
          me.employee = {
            subjectIDs: [],
            roomIDs: [],
            employeeIsDeviceManager: false,
            employeeWorkingStatus: true,
          };
          //G???i ?? S??? hi???u CB m???i
          await axios.get(BASE_URL + "Employees/newCode")
            .then(function (res) {
              me.employee.employeeCode = res.data;
              // ???n Loader
              me.loadingStatus = false;
            })
            .catch((error) => {
              console.log(error);
              // ???n Loader
              me.loadingStatus = false;
            });
          //Th??m m???i
          me.postMode = 1;
        }
      }
      catch(error) {
        console.log(error);
        // ????ng FORM tr??? l???i giao di???n b???ng v?? th??ng b??o l???i
        me.onClickClose();
        me.$emit("showToast", me.toastMsg.ErrorDefault, ResultStatus.FAIL);
      }
    },

    /**
     * S??? ki???n nh???p xong EmployeeCode chuy???n qua ?? kh??c - ki???m tra r???ng
     * Auth: KhaiND (08/11/2022)
     */
    validateCode() {
      try {
        // Ki???m tra ???? nh???p SHCB ch??a
        if (!this.employee.employeeCode) {
          this.errorEmployee.CodeInvalid = true;
          this.errorEmployee.CodeMsg =  this.formText.ErrorMsg.CodeEmpty;     
        } else {// Ki???m tra xem c?? tr??ng m?? kh??ng
          this.errorEmployee.CodeInvalid = false;
          // G???i API ????? ki???m tra
          var url = BASE_URL + "Employees/checkDuplicateCode?employeeID=" + this.employee.employeeID + "&employeeCode=" + this.employee.employeeCode;
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
        }
      }
      catch(error) {
        console.log(error);
      }
    },

    /**
     * S??? ki???n nh???p xong EmployeeName chuy???n qua ?? kh??c - ki???m tra r???ng
     * Author: KhaiND (09/11/2022)
     */
    validateName() {
      try {
        // Ki???m tra xem ???? nh???p t??n ch??a
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
     * S??? ki???n khi tho??t forcus kh???i Department th?? ????ng c??c l???a ch???n
     * Author: KhaiND (29/10/2022)
     */
    onBlurDepartment() {
      this.isShowDepartment = false;
    },

    /**
     * Action khi tho??t forcus kh???i ?? nh???p S??? ??i???n tho???i th?? validate v?? hi???n th??? th??ng b??o l???i
     * Author: KhaiND (24/11/2022)
     */
    validatePhone() {
      try {
        if(this.employee.employeePhoneNumber) {
          if(!isPhone(this.employee.employeePhoneNumber)) {
              this.errorEmployee.PhoneInvalid = true;
              this.errorEmployee.PhoneMsg = this.formText.ErrorMsg.PhoneInvalid;
          }
          else {
            this.errorEmployee.PhoneInvalid = false;
          }
        }
        else {
          this.errorEmployee.PhoneInvalid = false;
        }
      }
      catch(error) {
        console.log(error);
      }
    },

    /**
     * Action khi tho??t forcus kh???i ?? nh???p Email th?? validate v?? hi???n th??? th??ng b??o l???i
     * Author: KhaiND (24/11/2022)
     */
    validateEmail() {
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
        else {
          this.errorEmployee.EmailInvalid = false;
        }
      }
      catch(error) {
        console.log(error);
      }
    },

    /**
     * B???t gi?? tr??? roomID c???a c??c kho ph??ng ???????c ch???n khi check v??o ?? t????ng ???ng
     * Author: KhaiND (07/11/2022)
     */
    selectRoom: function (value) {
      try {
        var rooms = this.employee.roomIDs;
        // N???u gi?? tr??? t????ng ???ng ???? ???????c check (???? t???n t???i trong danh s??ch ch???n)
        if (rooms != undefined && rooms.includes(value)) {
          // Lo???i b??? n?? ??i (b???m v??o th?? uncheck)
          this.employee.roomIDs = rooms.filter((x) => {
            // ParseInt sang Guid
            return x != value;
          });
        }
        // Ng?????c l???i, chwa check th?? check (th??m gi?? tr??? t????ng ???ng v??o danh s??ch ch???n)
        else {
          this.employee.roomIDs.push(value);
        }
        // Ki???m tra xem n???u ???? check h???t c??c l???a ch???n th?? t??? ?????ng tick ?? T???t c???
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
     * Th???c hi???n ch???n, b??? ch???n t???t c??? options c???a Kho, ph??ng
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
     * Ki???m tra xem gi?? tr??? c?? trong danh s??ch ch???n kh??ng, n???u c?? th?? tr??? v??? true, ng?????c l???i false
     * N???u true, d??ng emit ????? g???i v?? set tr???ng th??i chekcbox t????ng ???ng l?? checked
     * @param {String} value - gi?? tr??? Id t????ng ???ng v???i l???a ch???n - c???n parse sang Int khi x??? l??
     * Author: KhaiND (08/11/2022)
     */
    checkRoomIdSelected(value) {
      if (this.employee.roomIDs == undefined) return false;
      return this.employee.roomIDs.includes(value);
    },

    /**
     * B???t gi?? tr??? subjectID c???a c??c m??n ???????c ch???n khi check v??o ?? t????ng ???ng
     * Author: KhaiND (08/11/2022)
     */
    selectSubject(value) {
      try {
        var subjects = this.employee.subjectIDs;
        // N???u gi?? tr??? t????ng ???ng ???? ???????c check (???? t???n t???i trong danh s??ch ch???n)
        if (subjects != undefined && subjects.includes(value)) {
          // Lo???i b??? n?? ??i (b???m v??o th?? uncheck)
          this.employee.subjectIDs = subjects.filter((x) => {
            // ParseInt sang Guid
            return x != value;
          });
        }
        // Ng?????c l???i, chwa check th?? check (th??m gi?? tr??? t????ng ???ng v??o danh s??ch ch???n)
        else {
          this.employee.subjectIDs.push(value);
        }
        // Ki???m tra xem n???u ???? check h???t c??c l???a ch???n th?? t??? ?????ng tick ?? T???t c???
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
     * Th???c hi???n ch???n, b??? ch???n t???t c??? options c???a m??n
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
     * Ki???m tra xem gi?? tr??? c?? trong danh s??ch ch???n kh??ng, n???u c?? th?? tr??? v??? true, ng?????c l???i false
     * N???u true, d??ng emit ????? g???i v?? set tr???ng th??i chekcbox t????ng ???ng l?? checked
     * @param {String} value - gi?? tr??? Id t????ng ???ng v???i l???a ch???n - c???n parse sang Int khi x??? l??
     * Author: KhaiND (08/11/2022)
     */
    checkSubjectIdSelected(value) {
      if (this.employee.roomIDs == undefined) return false;
      return this.employee.subjectIDs.includes(value);
    },

    /**
     * B???m v??o n??t "????ng" tr??n dialog th??ng b??o thay ?????i d??? li???u th?? ????ng dialog tr??? l???i m??n h??nh form
     * Author: KhaiND (28/11/2022)
     */
    closeMsDialog() {
      this.isShowDialog = false;
    },

    /**
     * S??? ki???n b???m v??o n??t "Kh??ng" tr??n dialog x??c nh???n thay ?????i d??? li???u th?? ????ng form v?? dialog m?? kh??ng ti???n h??nh l??u
     * Author: KhaiND (29/11/2022)
     */
    onClickDialogRefuse() {
      this.isShowDialog = false;
      this.$emit("hidePopup", null);
    },

    /**
     * S??? ki???n khi b???m v??o n??t ????ng ho???c icon x tr??n Form th?? ???n pop-up Form ??i
     * Auth: KhaiND (29/10/2022)
     */
    onClickClose() {
      if(JSON.stringify(this.employee) !== JSON.stringify(this.curEmployee)) {
        this.dialogMessage = CommonText.Dialog.Message.FormDataCHanged;
        this.isShowDialog = true;
      }
      else {
        this.$emit("hidePopup", null);
      }
    },

    /**
     * X??? l?? khi g???i API c???t d??? li???u th??nh c??ng
     * Author: KhaiND (26/11/2022)
     */
    async thenSaveAPI(response, msg) {
      // response ch??a d??ng ?????n
      // Th??ng b??o th??nh c??ng
      this.$emit("showToast", msg, ResultStatus.OK);
      // ???n Loader
      this.loadingStatus = false;
      // ????ng form
      this.$emit("hidePopup", null);
      // Reload d??? li???u
      // Load to??n trang, v??? trang 1
      await this.$emit("reloadData", null);
    },

    /**
     * S??? ki???n b???m n??t L??u tr??n form chi ti???t Employee
     * Author: KhaiND (30/10/2022)
     */
    async onClickSave() {
      try {
        var me = this;
        // Hi???n th??? loader
        this.loadingStatus = true;
  
        // Validate d??? li???u
        this.validateCode();
        this.validateName();
        this.validatePhone();
        this.validateEmail();
        me.valid = ((!me.errorEmployee.CodeInvalid) && (!me.errorEmployee.NameEmpty) && (!me.errorEmployee.PhoneInvalid) && (!me.errorEmployee.EmailInvalid))
  
        // Build object th??ng tin c??n b???
        if (me.valid) {
          // G???i API th???c hi???n l??u d??? li???u
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
              .put(pUrl, me.employee) // T??? ?????ng load l???i d??? li???u hi???n th???i
              .then( async (response) => { await me.thenSaveAPI(response, me.toastMsg.Employee.UpdateSuccess) })
              .catch((respomse) => me.catchAPI(respomse, me.toastMsg.InvalidUpdate))
              .finally(() => {
                this.$emit("loadCurData");
              })
          }
        }
        else { // Validate fail
          me.loadingStatus = false;
          if(this.postMode) {
            me.$emit("showToast", me.toastMsg.InvalidInsert, ResultStatus.FAIL);
          }
          else {
            me.$emit("showToast", me.toastMsg.InvalidUpdate, ResultStatus.FAIL);
          }

          // Ki???m tra xem ?? n??o fail th?? nh???y forcus v??o ?? ???? lu??n
          if(me.errorEmployee.CodeInvalid) {
            me.$nextTick(() => me.$refs.empCodeInput.focus());
          }
          else if(me.errorEmployee.NameEmpty) {
            me.$nextTick(() => me.$refs.empNameInput.focus());
          }
          else if(me.errorEmployee.PhoneInvalid) {
            me.$nextTick(() => me.$refs.empPhoneInput.focus());
          }
          else if(me.errorEmployee.EmailInvalid) {
            me.$nextTick(() => me.$refs.empEmailInput.focus());
          }
        }
      }
      catch(error) {
        console.log(error);
        this.loadingStatus = false;
        this.$emit("showToast", me.toastMsg.ErrorDefault, ResultStatus.FAIL);
      }
    },

    /**
     * S??? ki???n khi click v??o option t????ng ???ng t??? b??? m??n th?? ch???n t??? b??? m??n ???? (theo departmentID)
     * @param {int} depId - departmnemtId ?????nh danh t??? b??? m??n
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
     * S??? ki???n ???n hi???n select cho T??? b??? m??n khi b???m v??o input t????ng ???ng
     * Author: KhaiND (02/11/2022)
     */
    selectDepartment() {
      this.isShowDepartment = !this.isShowDepartment;
    },

    /**
     * G???i API l???y danh s??ch t??? b??? m??n, m??n h???c, kho ph??ng
     * Author: KhaiND (30/10/2022)
     */
    onLoadDSR() {
      var me = this;
      // L???y danh s??ch m??n
      try {
        axios
          .get(BASE_URL + "Subjects")
          .then(function (response) {
            me.subjectData = response.data;
            // ?????i c??c tr?????ng ID v?? Name th??nh itemID v?? itemName ti???n cho thao t??c v???i MSelect
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
            this.$emit("showToast", this.toastMsg.ErrorDefault, ResultStatus.FAIL);
            this.onClickClose();
            this.loadingStatus = false;
          });
      } catch (error) {
        console.log(error);
      }
      // L???y danh s??ch Kho ph??ng
      try {
        axios
          .get(BASE_URL + "Rooms")
          .then(function (response) {
            me.roomData = response.data;
            // ?????i c??c tr?????ng ID v?? Name th??nh itemID v?? itemName ti???n cho thao t??c v???i MSelect
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
            this.$emit("showToast", this.toastMsg.ErrorDefault, ResultStatus.FAIL);
            this.onClickClose();
            this.loadingStatus = false;
          });
      } catch (error) {
        console.log(error);
      }
      // L???y danh s??ch T??? b??? m??n
      try {
        axios
          .get(BASE_URL + "Departments")
          .then(function (response) {
            me.departmentData = response.data;
          })
          .catch(function (response) {
            console.log(response);
            this.$emit("showToast", this.toastMsg.ErrorDefault, ResultStatus.FAIL);
            this.onClickClose();
            this.loadingStatus = false;
          });
      } catch (error) {
        console.log(error);
        this.$emit("showToast", this.toastMsg.ErrorDefault, ResultStatus.FAIL);
        this.onClickClose();
        this.loadingStatus = false;
      }
    },
  },

  // Khi kh???i t???o th?? load lu??n d??? li???u c???n thi???t
  created() {
    // Hi???n Loader => t???t khi ?????n c??c h??m x??? l?? t????ng ???ng v???i mode th??m ho???c s???a ??? watch
    //this.loadingStatus = true;  //======================================= GI???
    // G???i h??m load d??? li???u
    this.onLoadDSR();
    this.bindEmployee(this.employeeSelectedId);
  },

  // Auto forcus v??o ?? input h??? t??n khi m??? form
  mounted() {
    // this.bindEmployee(this.employeeSelectedId);
    this.$nextTick(() => this.$refs.empNameInput.focus());
  },

  /**
   * Khi c?? thay ?????i th?? b???t gi?? tr??? c???a department ???????c ch???n v?? render t??n t??? b??? m??n tuuownng ???ng
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