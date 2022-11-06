<template lang="">
  <div class="container">
    <!-- <img alt="Vue logo" src="./assets/logo.png"> -->
    <!-- <HelloWorld msg="Welcome to Your Vue.js App"/> -->
    <the-sidebar></the-sidebar>
    <the-header></the-header>
    <the-content></the-content>
    <div class="toasts-container">
      <ms-toast></ms-toast>
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
    MsToast
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
