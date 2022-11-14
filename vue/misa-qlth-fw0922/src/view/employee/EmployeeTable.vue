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
            <tbody>
                <tr @dblclick="onClickEdit(employee)" v-for="employee in employeeList" :key="employee.Id" :id="employee.id">
                    <td class="m-a-center">
                        <div class="m-checkbox">
                            <input class="checkbox-real" type="checkbox">
                            <div class="checkbox-pseudo"></div>
                        </div>
                    </td>
                    <td class="m-a-left"> {{employee.employeeCode}} </td>
                    <td class="m-a-left" @click="onClickEdit(employee)"> {{employee.employeeName}} </td>
                    <td class="m-a-left"> {{employee.employeePhoneNumber}} </td>
                    <td class="m-a-left" title="Tổ Toán - Lý - Hóa" v-if="employee.departmentId == 0"> Tổ Toán - Lý - Hóa </td>
                    <td class="m-a-left" title="Tổ Toán - Tin" v-else-if="employee.departmentId == 1"> Tổ Toán - Tin </td>
                    <td class="m-a-left" title="Tổ Hóa - Sinh" v-else-if="employee.departmentId == 2"> Tổ Hóa - Sinh </td>
                    <td class="m-a-left" title="Tổ Anh văn" v-else-if="employee.departmentId == 3"> Tổ Anh văn </td>
                    <td class="m-a-left" title="Tổ Sử - Địa - GDCD " v-else-if="employee.departmentId == 4"> Tổ Sử - Địa - GDCD </td>
                    <td class="m-a-left" title="Tổ Ngữ văn" v-else-if="employee.departmentId == 5"> Tổ Ngữ văn </td>
                    <td class="m-a-left" title="Tổ Lái" v-else-if="employee.departmentId == 6"> Tổ Lái </td>
                    <td class="m-a-left" title="" v-else> </td>
                    <td class="m-a-left" :title="employee.subjects.map((i) => {return i.subjectName})">
                        <span v-for="(subject, index) in employee.subjects" :key="index">
                             {{subject.subjectName}}
                             <span v-if="index < employee.subjects.length-1">, </span>
                        </span>
                        <!-- {{employee.subjects.join(", ")}} -->
                    </td>
                    <td class="m-a-left" :title="employee.rooms.map((i) => {return i.roomName})">
                        <span v-for="(room, index) in employee.rooms" :key="index">
                             {{room.roomName}}
                             <span v-if="index < employee.rooms.length-1">, </span>
                        </span>
                    </td>
                    <td class="m-a-center" v-if="employee.isDeviceManager">
                        <div class="m-icon m-icon-24 icon-check"></div>
                    </td>
                    <td v-else></td>
                    <td class="m-a-center" v-if="employee.workingStatus">
                        <div class="m-icon m-icon-24 icon-check"></div>
                    </td>
                    <td v-else></td>
                    <td class="m-a-center">
                        <button @click="onClickEdit(employee)" class="m-icon button-icon icon--data-action icon-edit"
                            title="chỉnh sửa"></button>
                        <button @click="onClickDeleteEmployee(employee.id)" class="m-icon button-icon icon--data-action icon-remove"
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
      totalRecord: null,
      isShowDialog: false,
      curEmpId: null,
      loadingStatus: true,
      tableLoadingStatus: true,
      // employeeSelectedId: null
    };
  },
  
  // beforeCreate() {
  // },
  created() {
    // Hiển thị Loader
    this.tableLoadingStatus = true;  
    // Lấy dữ liệu từ API
    this.loadData();
  },

  //Other LCHooks
  methods: {
    /*
     * FEAT: Gọi API và lấy (GET tất cả dữ liệu của danh sách nhân viên) với Axios
     * Author: KhaiND (28/10/2022)
     */
    loadData(keyword) {
      try {
        // axios.get("https://localhost:44344/api/Employees/getopt", {headers: {'Content-Type': 'application/json'}}, pagingReq)
        if (!keyword) {
          this.tableLoadingStatus = true;
          axios
            .get("https://localhost:44344/api/Employees")
            .then((response) => {
              this.employeeList = response.data;
              console.log(this.employeeList);
              // Ẩn Loader
              this.tableLoadingStatus = false;      
            });
        }
        else {
          this.tableLoadingStatus = true;
          var url = "https://localhost:44344/api/Employees/getopt?keyword=" + keyword + "&pageIndex=1&pageSize=100";
          axios
            .get(url)
            .then((response) => {
              this.employeeList = response.data.data;
              this.totalRecord = response.data.totalRecords;
              console.log(this.employeeList);
              // Ẩn Loader
              this.tableLoadingStatus = false;
            });
        }
      } catch (error) {
        this.employeeList = null;
        console.log(error);
      }
      return this.totalRecord;
    },

    /**
     * Sự kiện bấm vào lựa chọn chỉnh sửa thì hiển thị Form chỉnh sửa thông tin cán bộ, giáo viên
     * @param {Object} employee đối tượng dữ liệu tương ứng với thông tin cán bộ, gáo viên
     * AUth: KhaiND (28/10/2022)
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
     * Auth: KhaiND (29/10/2022)
     */
    onClickDeleteEmployee(id) {
      //Show pop-up confirm
      this.isShowDialog = true;
      // Chỉ định Id của Employee cần xóa
      this.curEmpId = id;
    },
    
    /**
     * Thực hiện xóa Cán bộ, giáo viên đã được chỉ định khi bấm xóa và sau đó xác nhận đồng ý xóa
     * AuthL KhaiND (08/11/2022)
     */
    async deleteEmployee() {
      // Hiển thị Loader
      this.loadingStatus = true;
      //Call API
      var url = "https://localhost:44344/api/Employees/" + this.curEmpId;
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
     * Auth: KhaiND (08/11/2022)
     */
    closeMsDialog() {
      this.isShowDialog = false;
      this.curEmpId = null;
    },
  },
};
</script>

<style lang="">
</style>