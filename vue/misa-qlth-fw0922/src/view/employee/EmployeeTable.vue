<template lang="">
    <div class="table-content">
        <MLoader v-if="tableLoadingStatus"/>
        <table>
            <thead>
                <tr>
                    <th style="width: 40px; padding: 0px;">
                        <div class="m-checkbox" style="margin: 0px auto;">
                            <input @change="onCheckAll" v-bind:checked="checkAllEmplyee" class="checkbox-real" type="checkbox">
                            <div class="checkbox-pseudo"></div>
                        </div>
                    </th>
                    <th>{{thText.Code}}</th>
                    <th>{{thText.Name}}</th>
                    <th>{{thText.Phone}}</th>
                    <th>{{thText.Department}}</th>
                    <th title="Quản lý theo môn">{{thText.Subject}}</th>
                    <th title="Quản lý theo kho, phòng">{{thText.Room}}</th>
                    <th title="Đào tạo quản lý thiết bị">{{thText.DeviceManager}}</th>
                    <th>{{thText.Working}}</th>
                    <th></th>
                </tr>
            </thead>
            <tbody v-if="errorResult" class="table--not-found"> {{msgResult}} </tbody>
            <tbody v-else>
                <tr @dblclick="onClickEdit(employee)" v-for="employee in employeeList" :key="employee.rmployeeID" :id="employee.rmployeeID">
                    <td class="m-a-center" style="padding: 0px;">
                        <div class="m-checkbox" style="margin: 0px auto;">
                            <input @change="checkEmployee(employee.employeeID)" class="checkbox-real" type="checkbox"
                              v-bind:checked="checkSelected(employee.employeeID)">
                            <div class="checkbox-pseudo"></div>
                        </div>
                    </td>
                    <td class="m-a-left" style="max-width: 114px;"> {{employee.employeeCode}} </td>
                    <td class="m-a-left" style="min-width: 183px; color: #03AE66; cursor: pointer;" @click="onClickEdit(employee)"> {{employee.employeeName}} </td>
                    <td class="m-a-left" style="max-width: 100px;"> {{employee.employeePhoneNumber}} </td>
                    <td class="m-a-left" style="max-width: 100px;" :title="employee.departmentName"> {{employee.departmentName}} </td>
                    <td class="m-a-left" style="max-width: 100px;" :title="employee.subjectNames.map((i) => {return i})">
                        <span v-for="(subject, index) in employee.subjectNames" :key="index">
                             {{subject}}
                             <span v-if="index < employee.subjectNames.length-1">, </span>
                        </span>
                        <!-- {{employee.subjects.join(", ")}} -->
                    </td>
                    <td class="m-a-left" style="min-width: 160px; max-width: 160px;" :title="employee.roomNames.map((i) => {return i})">
                        <span v-for="(room, index) in employee.roomNames" :key="index">
                             {{room}}
                             <span v-if="index < employee.roomNames.length-1">, </span>
                        </span>
                    </td>
                    <td class="m-a-center" style="max-width: 100px;" v-if="employee.employeeIsDeviceManager">
                        <div class="m-icon m-icon-24 icon-check" style="margin: 0px auto;"></div>
                    </td>
                    <td style="max-width: 100px;" v-else></td>
                    <td class="m-a-center" style="max-width: 100px;" v-if="employee.employeeWorkingStatus">
                        <div class="m-icon m-icon-24 icon-check" style="margin: 0px auto;"></div>
                    </td>
                    <td style="max-width: 100px;" v-else></td>
                    <td style="min-width: 70px; max-width: 72px; padding: 0px;" class="m-a-center">
                        <button @click="onClickEdit(employee)" class="m-icon button-icon icon--data-action icon-edit"
                            title="chỉnh sửa"></button>
                        <button @click="onClickDeleteEmployee(employee.employeeID)" class="m-icon button-icon icon--data-action icon-remove"
                            title="xóa"></button>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    <div class="m-popup-container mask-upper" :class="{showD:isShowDialog}">
        <MsDialog @onConfirm="onConfirmDelete" @onClose="closeMsDialog" :dialogMessage="confirmMessage"
          :btn2Show="false" :btnDefaultText="dialogBtnDefaultText"/>
        <MLoader v-if="loadingStatus"/>
    </div>
</template>

<script>
import CommonText from "./../../utils/resources/common";
import { PAGE_SIZE, BASE_URL } from "../../utils/resources/common";
import EmployeeText from "./../../utils/resources/employee";
import axios from "axios";
import MLoader from "../../components/base/MLoader.vue";
import MsDialog from "./../../components/base/MsDialog.vue";

export default {
  name: "EmployeeTable",
  components: {
    MLoader,
    MsDialog,
  },

  data() {
    return {
      dialogText: CommonText.Dialog,
      toastMsg: CommonText.Toast.Message,
      errorResultText: CommonText.ErrorResult,
      btnText: CommonText.Button,
      thText: EmployeeText.TableHead,
      employeeList: null, // Danh sách Cán bộ, giáo viên đã load được để render ra bảng
      isShowDialog: false, // Trạng thái có hiển thị Dialog cảnh báo xóa hay không
      curEmpId: null, // Id của Cán bộ, giáo viên đang Forcus
      loadingStatus: false, // Trạng thái có hiển thị mask Loader cho toàn bộ content hay không
      tableLoadingStatus: true, // Trạng thái có hiển thị mask Loader cho phần thân dữ liệu của bảng hay không
      errorResult: false, // Trạng thái load dữ liệu cho bảng Cán bộ, giáo viên có lỗi hay không
      msgResult: "", // Thông điệp đi kèm kết quả load fail
      empSelectedIds: [], // Danh sách Id của Cán bộ, giáo viên đang được check
      checkAllEmplyee: false, // Có đang chọn tất cả Cán bộ, giáo viên trên màn hình hay không
      confirmMessage: CommonText.Dialog.Message.DeleteEmployee,
      deleteManyMode: 0, // Action xóa là xóa 1 hay xóa nhiều,
      curPageIndex: 1,
      curKeyword: "",
      dialogBtnDefaultText: CommonText.Button.Confirm
    };
  },

  created() {
    // Hiển thị Loader
    this.tableLoadingStatus = true;
    // Lấy dữ liệu từ API
    this.loadData();
  },

  methods: {
    /**
     * Hiển thị mask Loading của toàn bộ trang
     * Author: KhaiND (26/11/2022)
     */
    startLoading() {
      this.loadingStatus = true;
    },

    /**
     * Ẩn mask Loading của toàn bộ trang
     * Author: KhaiND (26/11/2022)
     */
    endLoading() {
      this.tableLoadingStatus = false;
    },

    /**
     * Xử lý khi gọi API load dữ liệu thành công (Cán bộ, giáo viên)
     * Author: KhaiND (26/11/2022)
     */
    thenLoadAPI(response) {
      this.checkAllEmplyee = false;
      this.empSelectedIds = [];
      this.employeeList = response.data.data;
      this.$emit("getTotal", response.data.totalRecord);
      this.errorResult = false;
      // Ẩn Loader
      this.tableLoadingStatus = false;
    },

    /**
     * Xử lý khi gọi API load dữ liệu thất bại (Cán bộ, giáo viên)
     * Author: KhaiND (26/11/2022)
     */
    catchLoadAPI(response) {
      try {
        switch(response.response.status) {
          case 404:
            // Xử lý kết quả
            this.$emit("getTotal", 0);
            this.errorResult = true;
            this.msgResult = this.errorResultText.NotFound;
            // Ẩn Loader
            this.tableLoadingStatus = false;
            break;
          case 400: //Thêm Các TOAST==================
            // Xử lý kết quả
            this.$emit("getTotal", 0);
            this.employeeList = null;
            this.$emit("showToast", this.errorResultText.InvalidSearch, 0);
            this.errorResult = true;
            this.msgResult = this.errorResultText.InvalidSearch;
            // Ẩn Loader
            this.tableLoadingStatus = false;
            break;
          case 500:
            // Xử lý kết quả
            this.$emit("getTotal", 0);
            this.employeeList = null;
            this.$emit("showToast", this.errorResultText.Error500, 0);
            this.errorResult = true;
            this.msgResult = this.errorResultText.Error500;
            // Ẩn Loader
            this.tableLoadingStatus = false;
            break;
          default:
            this.$emit("getTotal", 0);
            this.employeeList = null;
            this.$emit("showToast", this.errorResultText.Error500, 0);
            this.errorResult = true;
            this.msgResult = this.errorResultText.Error500;
            // Ẩn Loader
            this.tableLoadingStatus = false;
            break;
        }
      }
      catch(error) {
        this.$emit("getTotal", 0);
        this.employeeList = null;
        this.$emit("showToast", this.errorResultText.Error500, 0);
        this.errorResult = true;
        this.msgResult = this.errorResultText.Error500;
        // Ẩn Loader
        this.tableLoadingStatus = false;
      }
    },

    /*
     * Gọi API và lấy (GET tất cả dữ liệu của danh sách nhân viên) với Axios
     * Author: KhaiND (28/10/2022)
     */
    loadData(keyword, pageIndex) {
      try {
        this.curPageIndex = pageIndex;
        this.$emit("setPageIndex", this.curPageIndex);
        this.curKeyword = keyword;
        if (!keyword) {
          this.tableLoadingStatus = true;
          if (!pageIndex) pageIndex = 1;
          var url = BASE_URL + "Employees/search?pageIndex=" + pageIndex + "&pageSize=" + PAGE_SIZE;
          axios.get(url)
            .then((response) => {
              this.thenLoadAPI(response);
            })
            .catch((response) => this.catchLoadAPI(response));
        } else {
          this.tableLoadingStatus = true;
          url = BASE_URL + "Employees/search?keyword=" + keyword + "&pageIndex=" + pageIndex + "&pageSize=" + PAGE_SIZE;
          axios.get(url)
            .then((response) => this.thenLoadAPI(response))
            .catch((response) => this.catchLoadAPI(response));
        }
      } catch (error) {
        this.employeeList = null;
        this.$emit("showToast", this.errorResultText.Error500, 0);
        console.log(error);
      }
    },

    /**
     * Load dữ liệu với trang tìm kiếm hiện tại
     * Author: KhaiND (29/11/2022)
     */
    loadCurData() {
      this.loadData(this.curKeyword, this.curPageIndex);
    },

    /**
     * Mỗi lần check/uncheck vào chekcbox đầu dàng dữ liệu tương ứng của bảng Cán bộ, giáo viên thì thêm/bỏ Id tương ứng tỏng danh sách chọn
     * Author: KhaiND (23/11/2022)
     */
    checkEmployee(id) {
      if (this.empSelectedIds.includes(id) && this.empSelectedIds != undefined) {
        this.empSelectedIds.splice(this.empSelectedIds.indexOf(id), 1);
      } else {
        this.empSelectedIds.push(id);
      }
      // Kiểm tra xem nếu đã check hết các lựa chọn thì tự động tick ô Tất cả
      if (this.empSelectedIds.length == this.employeeList.length) {
        this.checkAllEmplyee = true;
      } else {
        this.checkAllEmplyee = false;
      }
    },

    /**
     * Sự kiện khi bấm vào checkbox ở đầu bảng thì chọn tất cả Cán bộ, giáo viên tương ứng đang hiển thị
     * Author: KhaiND (25/11/2022)
     */
    onCheckAll() {
      try {
        this.checkAllEmplyee = !this.checkAllEmplyee;
        this.empSelectedIds = [];
        if (this.checkAllEmplyee) {
          for (var i = 0; i < this.employeeList.length; i++) {
            
            this.empSelectedIds.push(this.employeeList[i].employeeID);
          }
        }
      }
      catch(error) {
        console.log(error);
      }
    },

    /**
     * Kiểm tra xem Cán bộ, giáo viên tương ứng đã được chọn hay chưa, nếu đã được chọn thì bind động để ô checkbox tương ứng được checked
     * @param {String} employeeID giá trị ID của Cán bộ, giáo viên cần kiểm tra 
     * Author: KhaiND (25/11/2022)
     */
    checkSelected(employeeID) {
      if (this.empSelectedIds == undefined) return false;
      return this.empSelectedIds.includes(employeeID);
    },

    /**
     * Sự kiện bấm vào lựa chọn chỉnh sửa thì hiển thị Form chỉnh sửa thông tin cán bộ, giáo viên
     * @param {Object} employee đối tượng dữ liệu tương ứng với thông tin cán bộ, gáo viên
     * AUthor: KhaiND (28/10/2022)
     */
    onClickEdit(employee) {
      this.$emit("showPopup", employee);
    },

    /**
     * Xóa một cán bộ, giáo viên
     * @param {Int} id id tương ứng của cán bộ, giáo viên
     * Author: KhaiND (29/10/2022)
     */
    onClickDeleteEmployee(id) {
      this.deleteManyMode = 0;
      //Show pop-up confirm
      this.confirmMessage = this.dialogText.Message.DeleteEmployee;
      this.isShowDialog = true;
      // Chỉ định Id của Employee cần xóa
      this.curEmpId = id;
    },

    /**
     * Sự kiện khi người dùng bấm chọn vào tính năng xóa nhiều thì kiểm tra dữ liệu chọn và hiển thị thông báo
     * Author: KhaiND (24/11/2022)
     */
    onClickDeleteMany() {
      try {
        // Kiểm tra danh sách chọn
        if (this.empSelectedIds == null || this.empSelectedIds.length <= 0) {
          this.$emit("showToast", this.toastMsg.Employee.NoSelected, 3);
        }
        else {
          // Hiển thị Dialog
          this.confirmMessage = this.dialogText.Message.DeleteManyEmployee;
          this.isShowDialog = true;
          // Gọi đúng API
          this.deleteManyMode = 1;
        }
      }
      catch(error) {
        console.log(error);
        this.$emit("showToast", this.toastMsg.Error500, 0);
      }
    },

    /**
     * Event khi bấm xác nhận đồng ý xóa trên Dialog thì kiểm tra xem là xóa 1 hay xóa nhiều và thực hiện tương ứng
     * Author: KhaiND (24/11/2022)
     */
    onConfirmDelete() {
      if(this.deleteManyMode == 1) {
        this.deleteManyEmployee();
      }
      else {
        this.deleteEmployee();
      }
    },

    /**
     * Thực hiện xóa Cán bộ, giáo viên đã được chỉ định khi bấm xóa và sau đó xác nhận đồng ý xóa
     * Author: KhaiND (08/11/2022)
     */
    async deleteEmployee() {
      try {
        // Hiển thị Loader
        this.loadingStatus = true;
        //Call API
        var url = BASE_URL + "Employees/" + this.curEmpId;
        await axios.delete(url);
        // Reset lại Id và đóng Dialog
        this.isShowDialog = false;
        this.curEmpId = null;
        // Ẩn Loader
        this.loadingStatus = false;
        // Reload va toast
        this.$emit("showToast", this.toastMsg.Employee.DeleteSuccess, 1);
        if(this.employeeList.length == 1 && this.curPageIndex > 1) {
           this.curPageIndex -= 1; 
        }
        await this.loadData(this.curKeyword, this.curPageIndex);
      }
      catch(error) {
        console.log(error);
        this.$emit("showToast", this.toastMsg.Error500, 0);
      }
    },

    /**
     * Chức năng xóa nhiều Cán bộ, giáo viên
     * Author: KhaiND (23/11/2022)
     */
    async deleteManyEmployee() {
      try {
        var me = this;
        // Hiển thị Loader
        this.loadingStatus = true;
        //Call API
        // await this.empSelectedIds.forEach((employeeID) => {
        //   var url = BASE_URL + "Employees/" + employeeID;
        //   axios.delete(url);  // Nên call bằng API xóa nhiều
        // });
        console.log(JSON.stringify(me.empSelectedIds));
        await axios.post(BASE_URL + "Employees/deleteMany", me.empSelectedIds);
        // Đóng Dialog
        this.isShowDialog = false;
        // Ẩn Loader
        this.loadingStatus = false;
        // Reload va toast
        this.$emit("showToast", this.toastMsg.Employee.DeleteManySucessPre + this.empSelectedIds.length + this.toastMsg.Employee.DeleteManySucessSfx, 1);
        this.empSelectedIds = [];
        if(this.checkAllEmplyee && this.curPageIndex > 1) {
          this.curPageIndex -= 1;
        }
        await this.loadData(this.curKeyword, this.curPageIndex);
      }
      catch(error) {
        console.log(error);
        this.$emit("showToast", this.toastMsg.Error500, 0);
      }
    },

    /**
     * Đóng dialog xác nhận xóa cán bộ
     * Author: KhaiND (08/11/2022)
     */
    closeMsDialog() {
      this.isShowDialog = false;
      this.curEmpId = null;
    },

    // showMsDialog(msg, btn2Show, btn2Text, btnDefaultText) {
    //   this.confirmMessage = msg;
    //   this.dialogBtn2Show = btn2Show;
    //   this.dialogBtn2Text = btn2Text;
    //   this.dialogBtnDefaultText = btnDefaultText;
    //   this.isShowDialog = true;
    //   console.log("TABLE" + this.btn2Show + this.btn2Text)
    // }
  },
};
</script>

<style scoped>
.table--not-found {
  background-color: inherit;
  position: absolute;
  top: 56px;
  left: 0px;
  right: 0px;
  text-align: center;
}
</style>