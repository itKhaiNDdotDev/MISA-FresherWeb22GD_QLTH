using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.FW0922GD.QLTH.Common.Entities
{
    /// <summary>
    /// Thông tin Kho, phòng
    /// </summary>
    /// Created By: KhaiND (19/11/2022)
    public class Room : BaseEntity
    {
        #region Property

        /// <summary>
        /// ID của Kho, phòng
        /// </summary>
        [Key]
        public Guid RoomID { get; set; }

        /// <summary>
        /// Số hiệu Kho, phòng
        /// </summary>
        [Required(ErrorMessage = "Số hiệu không được phép bỏ trống")]
        public string RoomCode { get; set; }

        /// <summary>
        /// Tên Kho, phòng
        /// </summary>
        [Required(ErrorMessage = "Tên không được phép bỏ trống")]
        public string RoomName { get; set; }

        /// <summary>
        /// Thông tin mô tả về Kho, phòng
        /// </summary>
        public string? RoomDescription { get; set; }

        #endregion
    }
}
