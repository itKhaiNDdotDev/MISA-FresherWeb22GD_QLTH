using MISA.FW0922GD.QLTH.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.FW0922GD.QLTH.Common.Entities.DTOs
{
    /// <summary>
    /// DTO cho các thông tin của một Employee muốn trình bày trong bảng danh sách Cán bộ, giáo viên
    /// </summary>
    /// Created By: KhaiND (21/11/2022)
    public class EmployeeResponse
    {
        #region Property

        /// <summary>
        /// ID cán bộ, giáo viên
        /// </summary>
        public Guid EmployeeID { get; set; }

        /// <summary>
        /// Số hiệu cán bộ
        /// </summary>
        public string EmployeeCode { get; set; }

        /// <summary>
        /// Họ và tên cán bộ, giáo viên
        /// </summary>
        public string EmployeeName { get; set; }

        /// <summary>
        /// Số điện thoại của cán bộ, giáo viên
        /// </summary>
        public string EmployeePhoneNumber { get; set; }

        /// <summary>
        /// Tên Tổ bộ môn của cán bộ, giáo viên
        /// </summary>
        public string? DepartmentName { get; set; }

        /// <summary>
        /// Danh sách tên các Môn học mà Cán bộ, giáo viên được phân công
        /// </summary>
        public List<string>? SubjectNames { get; set; }

        /// <summary>
        /// Danh sách tên các Kho, phòng mà Cán bộ, giáo viên được phân công
        /// </summary>
        public List<string>? RoomNames { get; set; }

        /// <summary>
        /// Trình độ nghiệp vụ quản lý thiết bị
        /// </summary>
        public IsDeviceManager EmployeeIsDeviceManager { get; set; }

        /// <summary>
        /// Trạng thái có còn làm việc hay không
        /// </summary>
        public WorkingStatus EmployeeWorkingStatus { get; set; }

        #endregion
    }
}
