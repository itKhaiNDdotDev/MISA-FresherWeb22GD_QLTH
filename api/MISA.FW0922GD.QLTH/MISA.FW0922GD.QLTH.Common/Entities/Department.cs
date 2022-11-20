using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.FW0922GD.QLTH.Common.Entities
{
    /// <summary>
    /// Thông tin Tổ bộ môn
    /// </summary>
    /// Created By: KhaiND (19/11/2022)
    public class Department : BaseEntity
    {
        #region Property

        /// <summary>
        /// ID của Tổ bộ môn
        /// </summary>
        [Key]
        public Guid DepartmentID { get; set; }

        /// <summary>
        /// Số hiệu Tổ bộ môn
        /// </summary>
        [Required(ErrorMessage = "Số hiệu không được phép bỏ trống")]
        public string DepartmentCode { get; set; }

        /// <summary>
        /// Tên Tổ bộ môn
        /// </summary>
        [Required(ErrorMessage = "Tên không được phép bỏ trống")]
        public string DepartmentName { get; set; }

        /// <summary>
        /// Thông tin mô tả về Tổ bộ môn
        /// </summary>
        public string DepartmentDescription { get; set; } 

        #endregion
    }
}
