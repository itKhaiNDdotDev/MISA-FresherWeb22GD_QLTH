using MISA.FW0922GD.QLTH.Common.Constants;
using MISA.FW0922GD.QLTH.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.FW0922GD.QLTH.Common.Entities
{
    /// <summary>
    /// Thông tin cán bộ, giáo viên
    /// </summary>
    /// Created By: KhaiND (17/11/2022)
    [Table("employee")]
    public class Employee : BaseEntity
    {
        #region Property

        /// <summary>
        /// ID cán bộ, giáo viên
        /// </summary>
        [Key]
        public Guid EmployeeID { get; set; }

        /// <summary>
        /// Số hiệu cán bộ
        /// </summary>
        [Required(ErrorMessage = Mesage.EMPLOYEE_CODE_REQUIRED)]
        public string EmployeeCode { get; set; }

        /// <summary>
        /// Họ và tên cán bộ, giáo viên
        /// </summary>
        [Required(ErrorMessage = Mesage.EMPLOYEE_NAME_REQUIRED)]
        public string EmployeeName { get; set; }

        /// <summary>
        /// Đường dẫn đến nơi lưu ảnh đại diện của cán bộ, giáo viên
        /// </summary>
        public string? EmployeeImageUrl { get; set; }

        /// <summary>
        /// Số điện thoại của cán bộ, giáo viên
        /// </summary>
        [Phone(ErrorMessage = Mesage.PHONE_INVALID)]
        public string? EmployeePhoneNumber { get; set; }

        /// <summary>
        /// Email của cán bộ, giáo viên
        /// </summary>
        [EmailAddress(ErrorMessage = Mesage.EMAIL_INVALID)]
        public string? EmployeeEmail { get; set; }

        /// <summary>
        /// Khóa ngoại tham chiếu đến Tổ bộ môn của cán bộ, giáo viên
        /// </summary>
        public Guid? DepartmentID { get; set; }

        /// <summary>
        /// Trình độ nghiệp vụ quản lý thiết bị
        /// </summary>
        public bool EmployeeIsDeviceManager { get; set; }

        /// <summary>
        /// Trạng thái có còn làm việc hay không
        /// </summary>
        public bool EmployeeWorkingStatus { get; set; }

        /// <summary>
        /// Ngày nghỉ việc
        /// </summary>
        public DateTime? EmployeeQuitDate { get; set; }

        /// <summary>
        /// Danh sách ID của Môn học được phân công
        /// </summary>
        public List<Guid>? SubjectIDs {get; set;}

        /// <summary>
        /// Danh sách ID của Kho, phòng được phân công
        /// </summary>
        public List<Guid>? RoomIDs { get; set; }

        #endregion
    }
}
