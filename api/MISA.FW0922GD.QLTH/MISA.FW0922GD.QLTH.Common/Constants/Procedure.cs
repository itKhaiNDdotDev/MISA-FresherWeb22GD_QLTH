using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.FW0922GD.QLTH.Common.Constants
{
    /// <summary>
    /// Format tên của các Procedure
    /// </summary>
    /// Created By: KhaiND (18/11/2022)
    public class Procedure
    {

        /// <summary>
        /// Format tên của Procedure lấy tất cả bản ghi
        /// </summary>
        public static string GET_ALL = "Proc_{0}_GetAll";

        /// <summary>
        /// Format tên của Procedure lấy một bản ghi theo ID
        /// </summary>
        public static string GET_BY_ID = "Proc_{0}_GetByID";

        /// <summary>
        /// Format tên của Procedure lấy Số hiệu cán bộ hiện tại của Cán bộ, giáo viên
        /// </summary>
        public static string GET_MY_CODE = "Proc_{0}_GetMyCode";

        /// <summary>
        /// Format tên của Procedure thêm mới một bản ghi
        /// </summary>
        public static string INSERT = "Proc_{0}_Insert";

        /// <summary>
        /// Format tên của Procedure cập nhật thông tin một bản ghi
        /// </summary>
        public static string UPDATE = "Proc_{0}_Update";

        /// <summary>
        /// Format tên của Procedure xóa một bản ghi
        /// </summary>
        public static string DELETE = "Proc_{0}_DeleteByID";
    }
}
