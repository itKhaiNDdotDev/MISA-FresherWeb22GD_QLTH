$(document).ready(function () {
    // Giữ trạng thái forcus menu
    $("#menuHeThong").focus();
    $("#searchEmployee").focus();

    //FOMR
    //Auto value Mã CB, auto focus họ tên
    $("#popupInsertForm").click(function (e) { 
        e.preventDefault();
        
    });
    $("eName").focus();


    editEmployee();


    // Ẩn hiện quit date
    $("#boxEheckQuit").click(function () {
        $("#eChckQuit").toggle();
        if ($("#eChckQuit").is(':checked')) {
            $("#eQuitDate").hide();
            console.log("CHEKCED DATE!");
        }
        else {
            $("#eQuitDate").show();
            console.log("UNNNNNNCHEKCED DATE!");
        }
    });

    // Click vào nút lưu để lưu dữ liệu nhập vào form
    $("#popupInsertForm .btn-save").click(function () {
        // Validate dữ liệu
        console.log("Click save");
        validateData();
        // Thu thập dữ liệu

        // Gọi API thực hiện đẩy dữ liệu

        // Kiểm tra kết quả trả về và đưa ra thông báo

        /////////////////////TEST
        setTimeout(function () {
            document.getElementById("toastSuccess").style.display = 'none';
        }, 3000);
    });
});

// Mode sửa form
function editEmployee() {
    $(".icon-edit").click(function() { 
        console.log("Open Form")
        $("#popupInsertForm").show();
        $("#popupInsertForm .form__header").text() = "Sửa hồ sơ Cán bộ giáo viên";
    });
}

// Xóa bản ghi
function removeEmployee() {

}

/**
 * Feat: Thực hiện kiểm tra và validate dữ liệu trong form
 * @returns true nếu validate thành công, false với trường hợp ngược lại
 * Author: KhaiND (25/10/2022)
 */
function validateData() {
    // Các thông tin bắt buộc nhập
    var inputRequireds = $("#popupInsertForm [mrequired]");
    console.log(inputRequireds);
    for (const inputRq of inputRequireds) {
        var val = inputRq.value;
        if (!val) {
            // Add border đỏ
            $(inputRq).addClass("m-input--error");
            $(inputRq).next().show();
        }
        else {
            $(inputRq).removeClass("m-input--error");
            $(inputRq).next().hide();
        }
    }

    return true;
}