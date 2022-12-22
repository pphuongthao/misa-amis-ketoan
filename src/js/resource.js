var resource = {
    Error:{
        MsgEmployeeCode : "Mã không được để trống.",
        MsgEmployeeName : "Tên không được để trống.",
        MsgDepartmentId: "Đơn vị không được để trống.",
        MsgDateOfBirth: "Ngày sinh phải nhỏ hơn ngày hiện tại.",
        MsgIdentityDate: "Ngày cấp phải nhỏ hơn ngày hiện tại.",
        MsgEmail: "Email chưa đúng định dạng.",
        DevMsg: "Có lỗi xảy ra vui lòng liên hệ MISA.",
        DataMsg: "Dữ liệu đầu vào không hợp lệ."
    },
    
    UpdateSuccess: "Cập nhật thành công!",
    InsertSuccess : "Thêm mới thành công!",
    DeleteSuccess: "Xóa thành công",
    FormNameInsert: "Thêm mới nhân viên",
    FormNameUpdate: "Cập nhật nhân viên",
    SuccessMsg: "Thao tác thành công!",
    IsError: "Có lỗi xảy ra khi tải dữ liệu về.",
    
    Gender:{
        FeMale: "Nữ",
        Male: "Nam",
        Other: "Khác",
    },
    
    QuestionDelete:{
        All: "Bạn có thực sự muốn xóa những nhân viên đã chọn không?",
        ContentDeleteLeft: "Bạn có thực sự muốn xoá Nhân viên <",
        ContentDeleteRight: "> không?",

    },
    ExistCode:{
        ContentExistLeft: "Mã nhân viên <",
        ContentExistRight: "> đã tồn tại trong hệ thống, vui lòng kiểm tra lại.",
    },

    Url:{
        Employee: "http://localhost:5187/api/v1/Employees/",
        Departments: "http://localhost:5187/api/v1/Departments/",
        Search: "http://localhost:5187/api/v1/Employees/filter",
        Export: "http://localhost:5187/api/v1/Employees/export"
    },


}
export default resource;
