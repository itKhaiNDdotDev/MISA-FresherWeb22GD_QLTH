<template lang="">
    <div class="sidebar" :class="{toggleSidebar:isHide}">
            <div class="sidebar-head">
                <div class="m-logo icon-logo-thietbi"></div>
                <div class="m-title">{{textRender.AppTitle}}</div>
                <button @click="toggleSidebar" class="m-icon icon-sidebar button-icon"></button>
            </div>
            <div class="sidebar-menu">
                <button id="showToastInfo" class="menu-item m-icon icon-tongquan">
                    <div class="menu-item--text">{{textRender.Overview}}</div>
                </button>
                <!-- <button id="showToastFail" class="menu-item m-icon icon-thietbi">
                    <div class="menu-item--text">Thiết bị</div>
                </button> -->
                <router-link to="/device" class="menu-item m-icon icon-thietbi">
                    <div class="menu-item--text">{{textRender.Device}}</div>
                </router-link>
                <button class="menu-item m-icon icon-muontra">
                    <div class="menu-item--text">{{textRender.Payment}}</div>
                </button>
                <!-- <button id="menuBaoCao" class="menu-item m-icon icon-baocao">
                    <div class="menu-item--text">Báo cáo</div>
                </button> -->
                <router-link to="/report" class="menu-item m-icon icon-baocao">
                    <div class="menu-item--text">{{textRender.Report}}</div>
                </router-link>
                <!-- <button id="menuHeThong" class="menu-item m-icon icon-hethong" autofocus>
                    <div class="menu-item--text">Hệ thống</div>
                </button> -->
                <router-link to="/employee" class="menu-item m-icon icon-hethong" ref='focusMe'>
                    <div class="menu-item--text">{{textRender.System}}</div>
                </router-link>
            </div>
        </div>
</template>

<script>
import TextRsc from "./../../utils/resources/fixlayout";

export default {
    name: 'TheSidebar',
    data() {
        return {
            textRender: TextRsc.Menu,
            isHide: false
        }
    },
    mounted() {
        //this.$nextTick( () => this.$refs.focusMe.focus() )
    },
    methods: {
        /**
         * Func: Thay đổi giá trị tương ứng ẩn/hiện sidebar
         * Author: KhaiND (30/10/2022)
         */
        toggleSidebar() {
            this.isHide = !this.isHide;
        }
    },
}
</script>

<style lang="">
    
</style>