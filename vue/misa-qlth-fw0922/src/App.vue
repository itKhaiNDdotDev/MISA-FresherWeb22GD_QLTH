<template lang="">
  <div class="container">
    <!-- <img alt="Vue logo" src="./assets/logo.png"> -->
    <!-- <HelloWorld msg="Welcome to Your Vue.js App"/> -->
    <the-sidebar></the-sidebar>
    <the-header @click="showToast"></the-header>
    <the-content @showToast="showToast"></the-content>
    <div class="toasts-container">
      <ms-toast :isShow="isShowToast" :toastType="1" :toastMes="toastMes"></ms-toast>
    </div>
  </div>
</template>

<script>
// import HelloWorld from './components/HelloWorld.vue'
import TheSidebar from "./components/layout/TheSideBar.vue";
import TheHeader from "./components/layout/TheHeader.vue";
import TheContent from "./components/layout/TheContent.vue";
import MsToast from "./components/base/MsToast.vue"

export default {
  name: "App",
  components: {
    // HelloWorld,
    TheSidebar,
    TheHeader,
    TheContent,
    MsToast,
  },
  data() {
    return {
      isShowToast: false,
      toastMes: "Xóa thành công cán bộ, giáo viên"
    }
  },
  methods: {
    showToast(mes) {
      var me = this;
      me.isShowToast = true;
      me.toastMes = mes;
      setTimeout(function () {
        me.isShowToast = false;
        console.log("HIDE TOAST");
    }, 3000);
    }
  },
};
</script>

<style>
@import url(style/main.css);

/* #app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  margin-top: 60px;
} */
</style>
