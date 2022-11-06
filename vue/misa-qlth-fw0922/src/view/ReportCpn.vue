<template lang="">
    <div>
        Report
        <MSelect v-model="value" :options="['A', 'B']"/>
    </div>
</template>

<script>
import MSelect from "./../components/base/MSelect.vue"
export default {
    name: "ReportCpn",
    components: {
        MSelect
    }
}
</script>

<style scoped>
    div {
        background-color: aqua;
    }
</style>