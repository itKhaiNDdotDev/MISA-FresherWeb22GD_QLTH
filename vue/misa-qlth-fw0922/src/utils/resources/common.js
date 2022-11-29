export const BASE_URL = "http://localhost:35241/api/v1/";
export const PAGE_SIZE = 15;

export default {
    Button: {
        Add: "Thêm",
        Export: "Xuất khẩu",
        Delete: "Xóa",
        Import: "Nhập khẩu",
        Save: "Lưu",
        Close: "Đóng",
        Confirm: "Đồng ý",
        Refuse: "Không"
    },

    Dialog: {
        Title: {
            Notice: "Thông báo"
        },
        Message: {
            DeleteEmployee: "Bạn có chắc chắn muốn xóa Cán bộ, giáo viên đang chọn không? Hê he",
            DeleteManyEmployee: "Bạn có chắc chắn muốn xóa những Cán bộ, giáo viên đang chọn không?",
            FormDataCHanged: "Dữ liệu đã bị thay đổi, bạn có muốn lưu lại không?"
        }
    },

    Toast: {
        Title: {
            Success: "Thành công",
            Error: "Lỗi",
            Warning: "Cảnh báo",
            Info: "Thông báo"
        },
        Message: {
            Employee: {
                InsertSuccess: "Thêm mới thành công Cán bộ, giáo viên.",
                UpdateSuccess: "Cập nhật thành công Cán bộ, giáo viên.",
                NoSelected: "Bạn chưa chọn Cán bộ, giáo viên nào!",
                DeleteSuccess: "Xóa thành công cán bộ, giáo viên.",
                DeleteManySucessPre: "Xóa thành công ",
                DeleteManySucessSfx: " cán bộ, giáo viên.",
            },
            Error500: "Có lỗi xảy ra! Vui lòng liên hệ MISA.",
            ErrorDefault: "Có lỗi xảy ra!",
            InvalidData: "Dữ liệu không hợp lệ.",
            InvalidInsert: "Thêm mới không thành công do dữ liệu không hợp lệ.",
            InvalidUpdate: "Cập nhật không thành công do dữ liệu không hợp lệ.",
        }
        // ENUM cho loại toast
    },

    ErrorResult: {
        NotFound: "Không tìm thấy kết quả nào",
        ConnectFail: "Lỗi kết nối",
        Error500: "Có lỗi xảy ra! Vui lòng liên hệ MISA.",
        InvalidSearch: "Tìm kiếm không thành công! Vui lòng kiểm tra lại."
    }

    //TOOLTIP
}