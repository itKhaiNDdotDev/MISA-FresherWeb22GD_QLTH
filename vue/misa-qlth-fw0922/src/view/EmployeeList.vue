<template lang="">
    <!-- DS cán bộ -->
    <employee-toolbar :key="toolbarKey" @onClickDeleteMany="onClickDeleteMany" @searchEmployee="searchEmployee" @showToast="showToast" @startLoading="startLoading" @endLoading="endLoading" @showPopup="onClickOpenForm"></employee-toolbar>
    <employee-table :key="tableKey" @setPageIndex="setPageIndex" @reloadData="reloadEmployeeData" @showToast="showToast" ref="tableData" @showPopup="onClickOpenForm" v-if="true" @getTotal="bindTotal"></employee-table>
    <no-data v-else @showPopup="onClickOpenForm"></no-data>
    <!-- @getId="getSelectedId" -->
    <employee-paging :key="pagingKey" ref="pagingTable" :searchText="searchText" :propPageIndex="pageIndex" @searchEmployee="searchEmployee" :totalRecord="totalRecord"></employee-paging>
    <employee-form :key="formKey" @showDialog="showDialog" @showToast="showToast" v-if="isShowForm" @hidePopup="onClickClosePopUp" @reloadData="reloadEmployeeData" :employeeSelectedId="employeeSelectedId"></employee-form>
</template>

<script>
import EmployeeToolbar from "./employee/EmployeeToolbar.vue";
import EmployeeTable from "./employee/EmployeeTable.vue";
import EmployeePaging from "./employee/EmployeePaging.vue";
import EmployeeForm from "./employee/EmployeeForm.vue";
import NoData from "./../components/base/NoData.vue";

export default {
  name: "EmployeeList",
  components: {
    EmployeeToolbar,
    EmployeeTable,
    EmployeePaging,
    EmployeeForm,
    NoData,
  },

  data() {
    return {
      isShowForm: false,
      employeeSelectedId: null,
      tableKey: 0,
      formKey: 0,
      totalRecord: null,
      searchText: null,
    };
  },

  methods: {
    /**
     * Hiển thị mask Loading
     * Author: (26/11/2022)
     */
    startLoading() {
      this.$refs.tableData.startLoading();
    },

    /**
     * Ẩn mask Loading
     * Author: KhaiND (26/11/2022)
     */
    endLoading() {
      this.$refs.tableData.endLoading();
    },

    /**
     * Binding dữ liệu tổng số bản ghi từ việc load dữ liệu table sang
     * Author: KhaiND (17/11/2022)
     */
    bindTotal(value) {
      this.totalRecord = value;
    },

    /**
     * Thực hiện gọi set pageIndex ở paging
     * Author: KhaiND (28/11/2022)
     */
    setPageIndex(value) {
      if(this.$refs.pagingTable != null) {
        this.$refs.pagingTable.setPageIndex(value);
      }
    },

    /**
     * Sự kiện bấm vào nút Thêm - Hiển thị pop-up thêm mới employee
     * Viết chung ở view EmployeeList, các components con call qua $emits
     * Author: KhaiND (29/10/2022)
     */
    onClickOpenForm(employee) {
      try {
        this.showEmployeeForm(true);
        this.employeeSelectedId = employee.employeeID;
      } catch (error) {
        console.log(error);
        this.employeeSelectedId = {};
      }
    },

    /**
     * Sự kiện bấm vào nút Đóng hoặc icon x trên Form - ẩn pop-up thêm mới employee
     * Viết chung ở view EmployeeList, các components con call qua $emits
     * Author: KhaiND (29/10/2022)
     */
    onClickClosePopUp() {
      this.showEmployeeForm(false);
    },

    /**
     * Feat: Ẩn hiện pop-up Form chi tiết Employee
     * @param {Boolean} isShow - true: hiện pop-up Form, false: ẩn pop-up Form
     * Author: KhaiND (29/10/2022)
     */
    showEmployeeForm(isShow) {
      this.isShowForm = isShow;
    },

    // /**
    //  * Chỉ load lại dữ liệu bảng danh sách Cán bộ trang hiện tại
    //  * Author: KhaiND (29/11/2022)
    //  */
    // loadCurEmployeeTable() {
    //   this.$refs.tableData.loadCurData();
    //   console.log("Vao duoc LIST");
    // },

    /**
     * Reload lại trang
     * Author: KhaiND (30/10/2022)
     */
    reloadEmployeeData() {
      try {
        // this.$forceUpdate();
        //this.$refs.tableData.loadData();
        this.toolbarKey += 1;
        this.tableKey += 1;
        this.formKey += 1;
        this.pagingKey += 1;
        this.employeeSelectedId = 0;
      } catch (error) {
        console.log(error);
        this.toolbarKey = 1;
        this.tableKey = 1;
        this.formKey = 1;
        this.pagingKey = 1;
        this.employeeSelectedId = 0;
      }
    },

    /**
     * Gọi đến tính năng tìm kiếm trong component Table để load dữ liêu danh sách Employee tương ứng
     * Author: KhaiND (04/11/2022)
     */
    searchEmployee(keyword, pageIndex) {
      this.searchText = keyword;
      if (!pageIndex) pageIndex = 1;
      //this.totalRecord = this.$refs.tableData.loadData(keyword, pageIndex);
      this.$refs.tableData.loadData(keyword, pageIndex);
      //this.totalRecord = this.$refs.tableData.totalRecord;
      //console.log("TOTAL RC: " + this.totalRecord);
      //this.pagingKey += 1;
    },

    /**
     * Gọi emit để show toast message
     * AuthorL KhaiND (30/10/2022)
     */
    showToast(mes, statusRes) {
      this.$emit("showToast", mes, statusRes);
    },

    /**
     * Gọi vào phương thức xử lý khi action bấm vào nút xóa nhiều
     * Author: KhaiND (24/11/2022)
     */
    onClickDeleteMany() {
      this.$refs.tableData.onClickDeleteMany();
    },

    // showDialog(msg, btn2Show, btn2Text, btnDefaultText) {
    //   this.$refs.tableData.showMsDialog(msg, btn2Show, btn2Text, btnDefaultText);
    // }
  },
};
</script>

<style lang="">
</style>