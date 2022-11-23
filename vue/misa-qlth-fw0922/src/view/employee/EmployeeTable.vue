<template lang="">
    <div class="table-content">
        <MLoader v-if="tableLoadingStatus"/>
        <table>
            <thead>
                <tr>
                    <th>
                        <div class="m-checkbox">
                            <input class="checkbox-real" type="checkbox">
                            <div class="checkbox-pseudo"></div>
                        </div>
                    </th>
                    <th>Số hiệu cán bộ</th>
                    <th>Họ và tên</th>
                    <th>Số điện thoại</th>
                    <th>Tổ chuyên môn</th>
                    <th title="Quản lý theo môn">QL theo môn</th>
                    <th title="Quản lý theo kho, phòng">QL kho, phòng</th>
                    <th title="Đào tạo quản lý thiết bị">Đào tạo QLTB</th>
                    <th>Đang làm việc</th>
                    <th></th>
                </tr>
            </thead>
            <tbody v-if="notFound" class="table--not-found"> Không tìm thấy kết quả nào </tbody>
            <tbody v-else>
                <tr @dblclick="onClickEdit(employee)" v-for="employee in employeeList" :key="employee.rmployeeID" :id="employee.rmployeeID">
                    <td class="m-a-center">
                        <div class="m-checkbox">
                            <input class="checkbox-real" type="checkbox">
                            <div class="checkbox-pseudo"></div>
                        </div>
                    </td>
                    <td class="m-a-left"> {{employee.employeeCode}} </td>
                    <td class="m-a-left" @click="onClickEdit(employee)"> {{employee.employeeName}} </td>
                    <td class="m-a-left"> {{employee.employeePhoneNumber}} </td>
                    <td class="m-a-left" :title="employee.departmentName"> {{employee.departmentName}} </td>
                    <td class="m-a-left" :title="employee.subjectNames.map((i) => {return i})">
                        <span v-for="(subject, index) in employee.subjectNames" :key="index">
                             {{subject}}
                             <span v-if="index < employee.subjectNames.length-1">, </span>
                        </span>
                        <!-- {{employee.subjects.join(", ")}} -->
                    </td>
                    <td class="m-a-left" :title="employee.roomNames.map((i) => {return i})">
                        <span v-for="(room, index) in employee.roomNames" :key="index">
                             {{room}}
                             <span v-if="index < employee.roomNames.length-1">, </span>
                        </span>
                    </td>
                    <td class="m-a-center" v-if="employee.employeeIsDeviceManager">
                        <div class="m-icon m-icon-24 icon-check"></div>
                    </td>
                    <td v-else></td>
                    <td class="m-a-center" v-if="employee.employeeWorkingStatus">
                        <div class="m-icon m-icon-24 icon-check"></div>
                    </td>
                    <td v-else></td>
                    <td class="m-a-center">
                        <button @click="onClickEdit(employee)" class="m-icon button-icon icon--data-action icon-edit"
                            title="chỉnh sửa"></button>
                        <button @click="onClickDeleteEmployee(employee.employeeID)" class="m-icon button-icon icon--data-action icon-remove"
                            title="xóa"></button>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    <!-- <MsDialog :class="{showD:isShowDialog}"/> -->
    <div class="m-popup-container" :class="{showD:isShowDialog}">
        <div class="m-dialog">
            <div class="dialog__header">
                Thông báo
            </div>
            <div class="dialog__content">
                Bạn có chắc chắn muốn xóa Cán bộ giáo viên đang chọn không?
            </div>
            <div class="dialog__footer">
                <button @click="closeMsDialog" class="m-button m-btn-style2">Đóng</button>
                <button @click="deleteEmployee" class="m-button m-btn-style1">Đồng ý</button>
            </div>
            <button title="Đóng" @click="closeMsDialog" class="m-icon m-icon-24 button-icon icon-close" id="closeDialog"></button>
        </div>
        <MLoader v-if="loadingStatus"/>
    </div>
</template>

<script>
import axios from "axios";
import {PAGE_SIZE} from "./../../resources";
import MLoader from "../../components/base/MLoader.vue";
// import MsDialog from './MsDialog.vue'
export default {
  name: "EmployeeTable",
  components: {
    MLoader,
    //MsDialog
  },

  data() {
    return {
      employeeList: null,
      isShowDialog: false,
      curEmpId: null,
      loadingStatus: false,
      tableLoadingStatus: true,
      notFound: false,
    };
  },
  
  created() {
    // Hiển thị Loader
    this.tableLoadingStatus = true;  
    // Lấy dữ liệu từ API
    this.loadData();
  },

  methods: {
    /*
     * FEAT: Gọi API và lấy (GET tất cả dữ liệu của danh sách nhân viên) với Axios
     * Author: KhaiND (28/10/2022)
     */
    loadData(keyword, pageIndex) {
      try {
        if (!keyword) {
          this.tableLoadingStatus = true;
          if(!pageIndex)
            pageIndex = 1;
          var url = "http://localhost:35241/api/v1/Employees/search?pageIndex=" + pageIndex + "&pageSize=" + PAGE_SIZE;
          axios
            .get(url)
            .then((response) => {
              this.employeeList = response.data.data;
              console.log(this.employeeList);
              this.$emit("getTotal",response.data.totalRecord);
              this.notFound = false;
              // Ẩn Loader
              this.tableLoadingStatus = false;
            });
        }
        else {
          this.tableLoadingStatus = true;
          url = "http://localhost:35241/api/v1/Employees/search?keyword=" + keyword + "&pageIndex=" + pageIndex + "&pageSize=" + PAGE_SIZE;
          axios
            .get(url)
            .then((response) => {
              this.employeeList = response.data.data;
              console.log(this.employeeList);
              this.$emit("getTotal",response.data.totalRecord);
              this.notFound = false;
              // Ẩn Loader
              this.tableLoadingStatus = false;
            })
            .catch((res) => {
              if(res.response.status == 404) {
                // Xử lý kết quả
                this.$emit("getTotal", 0);
                this.notFound = true;
                // Ẩn Loader
                this.tableLoadingStatus = false;
              }
            });
        }
      } catch (error) {
        this.employeeList = null;
        console.log(error);
      }
    },

    /**
     * Sự kiện bấm vào lựa chọn chỉnh sửa thì hiển thị Form chỉnh sửa thông tin cán bộ, giáo viên
     * @param {Object} employee đối tượng dữ liệu tương ứng với thông tin cán bộ, gáo viên
     * AUthor: KhaiND (28/10/2022)
     */
    onClickEdit(employee) {
      this.$emit("showPopup", employee);
      // console.log(employee);
      // this.$emit('getId', $event);
      // this.employeeSelectedId = employee.id;
    },

    /**
     * Xóa một cán bộ, giáo viên
     * @param {Int} id id tương ứng của cán bộ, giáo viên
     * Author: KhaiND (29/10/2022)
     */
    onClickDeleteEmployee(id) {
      //Show pop-up confirm
      this.isShowDialog = true;
      // Chỉ định Id của Employee cần xóa
      this.curEmpId = id;
    },
    
    /**
     * Thực hiện xóa Cán bộ, giáo viên đã được chỉ định khi bấm xóa và sau đó xác nhận đồng ý xóa
     * Author: KhaiND (08/11/2022)
     */
    async deleteEmployee() {
      // Hiển thị Loader
      this.loadingStatus = true;
      //Call API
      var url = "http://localhost:35241/api/v1/Employees/" + this.curEmpId;
      await axios.delete(url);
      // Reset lại Id và đóng Dialog
      this.isShowDialog = false;
      this.curEmpId = null;
      // Ẩn Loader
      this.loadingStatus = false;
      // Reload va toast
      this.$emit("showToast", "Xóa thành công cán bộ, giáo viên");
      await this.loadData();
    },
    /**
     * Đóng dialog xác nhận xóa cán bộ
     * Author: KhaiND (08/11/2022)
     */
    closeMsDialog() {
      this.isShowDialog = false;
      this.curEmpId = null;
    },
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