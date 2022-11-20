using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.FW0922GD.QLTH.Common.Entities
{
    /// <summary>
    /// Thông tin Môn học
    /// </summary>
    /// Created By: KhaiND (19/11/2022)
    public class Subject : BaseEntity
    {
        #region Property

        /// <summary>
        /// ID của Môn học
        /// </summary>
        [Key]
        public Guid SubjectID { get; set; }

        /// <summary>
        /// Số hiệu Môn học
        /// </summary>
        [Required(ErrorMessage = "Số hiệu không được phép bỏ trống")]
        public string SubjectCode { get; set; }

        /// <summary>
        /// Tên Môn học
        /// </summary>
        [Required(ErrorMessage = "Tên không được phép bỏ trống")]
        public string SubjectName { get; set; }

        #endregion
    }

}
