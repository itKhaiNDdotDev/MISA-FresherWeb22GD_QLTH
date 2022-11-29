using MISA.FW0922GD.QLTH.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.FW0922GD.QLTH.Common.Entities.DTOs
{
    /// <summary>
    /// Đối tượng kết quả trả về khi gọi API gặp lỗi
    /// </summary>
    /// Created By: KhaiND (19/11/2022)
    public class ErrorResult
    {
        /// <summary>
        /// Mã lỗi
        /// </summary>
        public GDErrorCode ErrorCode { get; set; }

        /// <summary>
        /// Thông điệp trả về cho Dev
        /// </summary>
        public string DevMsg { get; set; }

        /// <summary>
        /// Thông điệp trả về cho người dùng
        /// </summary>
        public string UserMsg { get; set; }

        /// <summary>
        /// Thông tin chi tiết về lỗi
        /// </summary>
        public object MoreInfo { get; set; }

        /// <summary>
        /// ID dùng để trace lỗi khi log lại
        /// </summary>
        public string TraceId { get; set; }
    }
}
