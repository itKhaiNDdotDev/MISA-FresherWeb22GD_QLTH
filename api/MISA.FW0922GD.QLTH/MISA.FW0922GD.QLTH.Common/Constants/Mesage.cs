using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.FW0922GD.QLTH.Common.Constants
{
    /// <summary>
    /// Nội dung các thông điệp (Message) sử dụng trong mã nguồn
    /// </summary>
    /// Created By: KhaiND (27/11/2022)
    public class Mesage
    {
        /// <summary>
        /// Thông điệp khi để trống trường thông tin bắt buộc Số hiệu cán bộ.
        /// </summary>
        public const string EMPLOYEE_CODE_REQUIRED = "Số hiệu cán bộ không được phép để trống.";

        /// <summary>
        /// Thông điệp khi Validate trùng Số hiệu cán bộ.
        /// </summary>
        public const string EMPLOYEE_CODE_DUPLICATE = "Số hiệu cán bộ đã tồn tại! Vui lòng kiểm tra lại";

        /// <summary>
        /// Thông điệp khi để trống trường thông tin bắt buộc Họ và tên.
        /// </summary>
        public const string EMPLOYEE_NAME_REQUIRED = "Họ và tên không được phép để trống.";

        /// <summary>
        /// Thông điệp khi Số điện thoại Validae không hợp lệ.
        /// </summary>
        public const string PHONE_INVALID = "Số điện thoại không hợp lệ! Vui lòng kiểm tra lại.";

        /// <summary>
        /// Thông điệp khi Email Validae không hợp lệ.
        /// </summary>
        public const string EMAIL_INVALID = "Số điện thoại không hợp lệ! Vui lòng kiểm tra lại.";
    }
}
