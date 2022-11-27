<template lang="">
    <div class="content">
        <!-- Content -->
        <router-view @showToast="showToast"></router-view>
    </div>
</template>

<script>
// import EmployeeList from './../view/EmployeeList.vue';

export default {
    name: 'TheContent',
    components: {
        // EmployeeList
    },
    methods: {
        showToast(mes, statusRes) {
            this.$emit('showToast', mes, statusRes);
        }
    },
}
</script>

<style lang="">
    
</style>