using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.FW0922GD.QLTH.Common.Enums
{
    /// <summary>
    /// Enum sử dụng định nghĩa các mã lỗi khi có lỗi xảy ra
    /// </summary>
    /// Created By: KhaiND (18/11/2022)
    public enum GDErrorCode
    {
        /// <summary>
        /// Lỗi gặp exception
        /// </summary>
        Exception = 1,

        /// <summary>
        /// Lỗi trùng số hiệu cán bộ
        /// </summary>
        DuplicateCode = 2,

        /// <summary>
        /// Lỗi dữ liệu đầu vào không hợp lệ
        /// </summary>
        InvalidData = 3
    }
}
