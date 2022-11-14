<template lang="">
    <div>
        Report
        <!-- <MSelect :options="mdata" :selectedIds="checkList"/> -->
        <button @click="changeCheck">Change</button>
    </div>
</template>

<script>
// import MSelect from "./../components/base/MSelect.vue";
export default {
  name: "ReportCpn",
  components: {
    // MSelect,
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
      checkList: [1, 2, 3]
    };
  },
  methods: {
    changeCheck() {
      this.checkList = [1, 3, 5];
    }
  },
  updated() {
    console.log(this.checkList);
  }
};
</script>

<style scoped>
/* div {
        background-color: aqua;
    } */
</style>