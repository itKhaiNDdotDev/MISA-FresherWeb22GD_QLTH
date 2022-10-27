// Ẩn hiện sidebar khi click icon sidebar (3 gạch)
$("#btnSidebar").click(function (e) { 
    $(this).parent().parent().toggleClass("hidden-sidebar");
});