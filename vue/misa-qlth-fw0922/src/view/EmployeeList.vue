<template lang="">
    <!-- DS cán bộ -->
    <employee-toolbar @searchEmployee="searchEmployee" @showPopup="onClickOpenForm"></employee-toolbar>
    <employee-table :key="tableKey" @showToast="showToast" ref="tableData" @showPopup="onClickOpenForm" v-if="true" @getTotal="bindTotal"></employee-table>
    <no-data v-else @showPopup="onClickOpenForm"></no-data>
    <!-- @getId="getSelectedId" -->
    <employee-paging :key="pagingKey" ref="pagging" :searchText="searchText" :propPageIndex="pageIndex" @searchEmployee="searchEmployee" :totalRecord="totalRecord"></employee-paging>
    <employee-form :key="formKey" @showToast="showToast" v-if="isShowForm" @hidePopup="onClickClosePopUp" @reloadData="reloadEmployeeData" :employeeSelectedId="employeeSelectedId"></employee-form>
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
     * Binding dữ liệu tổng số bản ghi từ việc load dữ liệu table sang
     * Author: KhaiND (17/11/2022)
     */
    bindTotal(value) {
      this.totalRecord = value;
      console.log(this.totalRecord);
    },

    /**
     * Sự kiện bấm vào nút Thêm - Hiển thị pop-up thêm mới employee
     * Viết chung ở view EmployeeList, các components con call qua $emits
     * Author: KhaiND (29/10/2022)
     */
    onClickOpenForm(employee) {
      this.showEmployeeForm(true);
      console.log(employee);
      this.employeeSelectedId = employee.id;
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

    /**
     * Reload lại trang
     * Author: KhaiND (30/10/2022)
     */
    reloadEmployeeData() {
      // this.$forceUpdate();
      //this.$refs.tableData.loadData();
      this.tableKey += 1;
      this.formKey += 1;
      this.employeeSelectedId = 0;
    },

    /**
     * Gọi đến tính năng tìm kiếm trong component Table để load dữ liêu danh sách Employee tương ứng
     * Author: KhaiND (04/11/2022)
     */
    searchEmployee(keyword, pageIndex) {
      this.searchText = keyword;
      if (!pageIndex) pageIndex = 1;
      console.log(pageIndex);
      //this.totalRecord = this.$refs.tableData.loadData(keyword, pageIndex);
      this.$refs.tableData.loadData(keyword, pageIndex);
      //this.totalRecord = this.$refs.tableData.totalRecord;
      //console.log("TOTAL RC: " + this.totalRecord);
      this.pagingKey += 1;
    },

  /**
   * Gọi emit để show toast message
   * AuthorL KhaiND (30/10/2022)
   */
    showToast(mes) {
      this.$emit("showToast", mes);
      // console.log("Tu FORM RA LIST");
    },
  },
};
</script>

<style lang="">
</style>