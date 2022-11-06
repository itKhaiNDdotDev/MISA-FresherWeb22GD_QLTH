<template lang="">
    <div>
        Report
        <MSelect :options="mdata"/>
    </div>
</template>

<script>
import MSelect from "./../components/base/MSelect.vue";
export default {
  name: "ReportCpn",
  components: {
    MSelect,
  },
  data() {
    return {
      mdata: [
        { id: "1", name: "JavaScript" },
        { id: "2", name: "JavaScript" },
        { id: "3", name: "Ruby" },
        { id: "4", name: "Ruby" },
        { id: "5", name: "PHP" },
        { id: "6", name: "Elixir" },
      ],
    };
  },
};
</script>

<style scoped>
/* div {
        background-color: aqua;
    } */
</style>