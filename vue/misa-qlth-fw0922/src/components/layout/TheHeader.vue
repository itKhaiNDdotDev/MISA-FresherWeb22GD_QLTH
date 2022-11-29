<template lang="">
    <div class="header">
        <div class="header-left">
            <div class="header-title">{{textRender.PageTitle}}</div>
            <button class="m-icon m-icon-24 icon-help button-icon" title="Trợ giúp"></button>
        </div>
        <div class="header-right">
            <button class="m-icon m-icon-32 icon-notification button-icon" title="Thông báo">
                <div class="m-red-new-noti"></div>
            </button>
            <div class="hor-line"></div>
            <div class="m-account">
                <div class="acc-name">{{textRender.AccountName}}</div>
                <button class="m-icon m-icon-24 icon-down button-icon" title="Quản lý tài khoản"></button>
                <img src="../../assets/Images/KhaiND.png" alt="ảnh đại diện" class="acc-avatar">
                <!-- <CAvatar src="/assets/Images/Khanh-Huyen-removebg-preview.png"/> -->
            </div>
        </div>
    </div>
</template>

<script>
import TextRsc from "./../../utils/resources/fixlayout";

export default {
  name: "TheHeader",
  data() {
        return {
            textRender: TextRsc.Hader,
        }
    },
};
</script>

<style lang="">
</style>