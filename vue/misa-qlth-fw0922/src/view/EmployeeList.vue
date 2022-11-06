<template lang="">
    <!-- DS cán bộ -->
    <employee-toolbar  @showPopup="onClickOpenForm"></employee-toolbar>
    <employee-table ref="tableData" @showPopup="onClickOpenForm" v-model:employeeData="employeeList" v-if="true"></employee-table>
    <no-data v-else @showPopup="onClickOpenForm"></no-data>
    <!-- @getId="getSelectedId" -->
    <employee-paging></employee-paging>
    <employee-form :isShow="isShowPopup" @hidePopup="onClickClosePopUp" @reloadData="reloadEmployeeData" :employeeSelectedId="employeeSelectedId"></employee-form>
</template>

<script>
import EmployeeToolbar from "./../components/base/EmployeeToolbar.vue";
import EmployeeTable from "./../components/base/EmployeeTable.vue";
import EmployeePaging from "./../components/base/EmployeePaging.vue";
import EmployeeForm from "./EmployeeForm.vue";
import NoData from "./../components/base/NoData.vue"
export default {
  name: "EmployeeList",
  components: {
    EmployeeToolbar,
    EmployeeTable,
    EmployeePaging,
    EmployeeForm,
    NoData
  },
  data() {
    return {
      isShowPopup: false,
      employeeSelectedId: null,
      employeeData: null
    };
  },
  // watch: {
  //   employeeSelectedId: function(value)
  //   {
  //     alert(value);
  //   }
  // },
  mounted() {
    console.log("---------------------------")
    console.log(this.employeeData)
  },
  methods: {
    /**
     * Sự kiện bấm vào nút Thêm - Hiển thị pop-up thêm mới employee
     * Viết chung ở view EmployeeList, các components con call qua $emits
     * Auth: KhaiND (29/10/2022)
     */
    onClickOpenForm(employee) {
      this.showEmployeeForm(true);
      console.log(employee);
      this.employeeSelectedId = employee.id;
    },
    /**
     * Sự kiện bấm vào nút Đóng hoặc icon x trên Form - ẩn pop-up thêm mới employee
     * Viết chung ở view EmployeeList, các components con call qua $emits   
     * Auth: KhaiND (29/10/2022)
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
      this.isShowPopup = isShow;
    },
    // //Get ELEMETN
    // getSelectedId(event) {
    //   console.log(event.target.parentElement);
    // }
    reloadEmployeeData() {
      this.$refs.tableData.loadData();
    }
  }
};
</script>

<style lang="">
</style>