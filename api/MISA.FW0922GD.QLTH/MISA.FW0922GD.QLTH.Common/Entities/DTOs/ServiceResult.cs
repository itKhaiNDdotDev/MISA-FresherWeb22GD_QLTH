using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.FW0922GD.QLTH.Common.Entities.DTOs
{
    /// <summary>
    /// Dữ liệu của kết quả trả về ở tầng BL
    /// </summary>
    public class ServiceResult
    {
        #region Property

        /// <summary>
        /// Trạng thái trả về cho biết yêu cầu đã được thực hiện thành công hay không
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Dữ liệu được trả về tương ứng
        /// </summary>
        public object? Data { get; set; }

        #endregion
    }
}
