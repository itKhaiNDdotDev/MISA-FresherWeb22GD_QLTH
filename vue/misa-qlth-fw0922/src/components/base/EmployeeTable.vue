<template lang="">
    <div class="table-content">
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
                    <td class="m-a-left" title="Tổ Ngữ văm" v-else-if="employee.departmentId == 5"> Tổ Ngữ văm </td>
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
                        <button @click="deleteEmployee(employee.id)" class="m-icon button-icon icon--data-action icon-remove"
                            title="xóa"></button>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    <!-- <MsDialog :class="showD:isShowDialog"/> -->
</template>

<script>
import axios from 'axios'
// import MsDialog from './MsDialog.vue'
export default {
    name: 'EmployeeTable',
    // components: {
    //     MsDialog
    // },
    // beforeCreate() {    
    // },
    created() {
        // Lấy dữ liệu từ API
        this.loadData();
        console.log("========== ")
        console.log(this.employeeList)
    },
    //Other LCHooks
    methods: {
        /*
        * FEAT: Gọi API và lấy (GET tất cả dữ liệu của danh sách nhân viên) với Axios
        * Author: KhaiND (28/10/2022)
        */
        loadData() {
            try {
                // var me = this;
                // var pagingReq = {
                //     keyword: "Lan",
                //     pageIndex: 1,
                //     pageSize: 15
                // };
                // axios.get("https://localhost:44344/api/Employees/getopt", {headers: {'Content-Type': 'application/json'}}, pagingReq)
                axios.get("https://localhost:44344/api/Employees")
                .then(response => {
                    this.employeeList = response.data;
                    console.log(this.employeeList);
                });
            } catch (error) {
                this.employeeList == null;
                console.log(error);
            }
        },
        /**
         * Bấm vào tên - Form chỉnh sửa thông tin
         */
        onClickEdit(employee) {
            this.$emit('showPopup', employee);
            // console.log(employee);
            // this.$emit('getId', $event);
            // this.employeeSelectedId = employee.id;
        },
        /**
         * Xoas nhaan vien
         */
        async deleteEmployee(id) {
            //Show pop-up confirm
            //Call API
            var url = "https://localhost:44344/api/Employees/" + id;
           await axios.delete(url);
            // Dialog confirm

            // Reload va toast
            await this.loadData();
        }
    },
    data() {
        return {
            employeeList: null,
            isShowDialog: false
            // employeeSelectedId: null
        }
    },
}
</script>

<style lang="">
    
</style>