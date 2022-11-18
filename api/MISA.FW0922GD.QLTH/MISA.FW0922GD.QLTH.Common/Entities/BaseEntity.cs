using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.FW0922GD.QLTH.Common.Entities
{
    /// <summary>
    /// BaseEntity chứa các trường thông tin tái sử dụng ở nhiều Entity
    /// </summary>
    /// Author: KhaiND (17/11/2022)
    public class BaseEntity
    {
        #region Property

        /// <summary>
        /// Người tạo bản ghi
        /// </summary>
        public string? CreatedBy { get; set; } //= Resource.DefaultUser;

        /// <summary>
        /// Thời gian tọa bản ghi
        /// </summary>
        public DateTime CreatedDate { get; set; } //= DateTime.Now;

        /// <summary>
        /// Người chỉnh sửa bản ghi lần gần nhất
        /// </summary>
        public string? ModifiedBy { get; set; } //= Resource.DefaultUser;

        /// <summary>
        /// Thời gian chỉnh sửa bản ghi lần gần nhất
        /// </summary>
        public DateTime ModifiedDate { get; set; } //= DateTime.Now;

        #endregion
    }
}
