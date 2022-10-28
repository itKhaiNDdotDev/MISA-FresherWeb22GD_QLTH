// HIỂN THỊ CÁC LOẠI TOAST MESSAGE TRONG 3S RỒI ẨN ĐU
document.getElementById("toastSuccess").style.display = 'none';
// document.getElementById("btnSave").addEventListener("click", function () {
//     console.log("SHOW");
//     document.getElementById("toastSuccess").style.display = 'grid';
//     setTimeout(function () {
//         document.getElementById("toastSuccess").style.display = 'none';
//     }, 3000);
// });
document.getElementById("toastFail").style.display = 'none';
document.getElementById("showToastFail").addEventListener("click", function () {
    console.log("SHOW");
    document.getElementById("toastFail").style.display = 'grid';
    setTimeout(function () {
        document.getElementById("toastFail").style.display = 'none';
    }, 3000);
});
document.getElementById("toastWarning").style.display = 'none';
document.getElementById("btnClose").addEventListener("click", function () {
    console.log("SHOW");
    document.getElementById("toastWarning").style.display = 'grid';
    setTimeout(function () {
        document.getElementById("toastWarning").style.display = 'none';
    }, 3000);
});
document.getElementById("toastInfo").style.display = 'none';
document.getElementById("showToastInfo").addEventListener("click", function () {
    console.log("SHOW");
    document.getElementById("toastInfo").style.display = 'grid';
    setTimeout(function () {
        document.getElementById("toastInfo").style.display = 'none';
    }, 3000);
});

// Nếu đồng ý lưu dữ liệu thay đổi thì đóng form thêm thông tin
document.getElementById("saveChangeData").addEventListener("click", function () {
    console.log("ClOSED FORM");
    document.getElementById("loader").style.display = 'block';
        console.log("Hien loader");
        setTimeout(function () {
            console.log("Dang an loader");
            document.getElementById("loader").style.display = 'none';
            document.getElementById("popupInsertForm").style.display = 'none';
            document.getElementById("popupSaveDialog").style.display = 'none';
        }, 3000);
});
// Bấm vào button Thêm để mở form thêm thông tin
document.getElementById("btnAddForm").addEventListener("click", function () {
    document.getElementById("loader").style.display = 'block';
    console.log("Hien loader");
    setTimeout(function () {
        console.log("OPEN FORM");
        document.getElementById("popupInsertForm").style.display = 'block';
        document.getElementById("loader").style.display = 'none';
        console.log("Dang an loader");
    }, 500);
});
// Bấm vào icon close trên form thì hiển thị dialog nhắc nhở việc lưu thông tin thay đổi
document.getElementById("closeInsertForm").addEventListener("click", function () {
    document.getElementById("popupSaveDialog").style.display = 'block';
});
// Bấm vào nút Đóng trên dialog nhắc nhở lưu thông tin thay đổi thì đóng dialog này lại, trả lại form nhập liệu
document.getElementById("closeSaveDialog").addEventListener("click", function () {
    document.getElementById("popupSaveDialog").style.display = 'none';
});

// Bấm vào nút biểu tượng xóa ở bản ghi đầu tiên trong bảng thì hiện ra dialog confirm xóa bản ghi
document.getElementById("btnDel").addEventListener("click", function () {
    console.log("OPEN DL");
    document.getElementById("popupDialog").style.display = 'block';
});
// Bấm vào biểu tượng close (x) trên dialog confirm xóa bản ghi để đóng dialog này
document.getElementById("closeDialog").addEventListener("click", function () {
    console.log("ClOSED DL");
    document.getElementById("popupDialog").style.display = 'none';
});

// Giữ trạng thái forcus (CSS) cho tab Hệ thống bên Menu Sidebar khi đang thao tác ở màn hình quản lý phần content
// if(document.getElementById("sysContent").style.display != 'none')
// {
//     document.getElementById("navSystem").style.forcu
// }    Giu trang thai forcus cho menu

// Bấm vào tab Báo cáo ở Menu Sidebar thì hiện ra trang no-data
document.getElementById("noData").style.display = 'none';
document.getElementById("menuBaoCao").addEventListener("click", function () {
    document.getElementById("noData").style.display = 'flex';
});
// // Bám vào tab Hệ thống trên Menu Sidebar thì ẩn trang no-data đi để trả lại màn hình quản trị thông tin cán bộ
// document.getElementById("menuHeThong").addEventListener("click", function () {
//     document.getElementById("noData").style.display = 'none';
// });

// Hiển thị Loader khi đang load dữ liệu
if (document.getElementById("menuHeThong") != document.activeElement) //CHUA CHUAN DAU
{
    // Bám vào tab Hệ thống trên Menu Sidebar thì ẩn trang no-data đi để trả lại màn hình quản trị thông tin cán bộ
    document.getElementById("menuHeThong").addEventListener("click", function () {
        document.getElementById("loader").style.display = 'block';
        console.log("Hien loader");
        setTimeout(function () {
            document.getElementById("noData").style.display = 'none';
            console.log("Tat no-data, bat bang len");
            document.getElementById("loader").style.display = 'none';
            console.log("Dang an loader");
        }, 500);
    });
}

// Hiển thị loader cho thân bảng khi bấm icon More
document.getElementById("btnMore").addEventListener("click", function () {
    document.getElementById("tableDataLoader").style.display = 'block';
    console.log("Hien loader");
    setTimeout(function () {
        document.getElementById("tableDataLoader").style.display = 'none';
        console.log("Dang an loader");
    }, 3000);
});