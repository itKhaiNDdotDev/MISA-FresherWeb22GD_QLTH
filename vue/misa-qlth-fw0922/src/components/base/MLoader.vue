<template lang="">
    <div class="m-loading-wrapper" :class="{showLoader:isShow}">
        <div class="m-loading">
            <div class="loading--circle"></div>
            <div class="loading--circle"></div>
            <div class="loading--circle"></div>
            <div class="loading--circle"></div>
            <div class="loading--text">Đang tải</div>
        </div>
    </div>
</template>

<script>
export default {
    name: "MLoader",
    props: ["isShow"]
};
</script>

<style scoped>
    .m-loading-wrapper.showLoader {
        display: block !important;
    }
</style>