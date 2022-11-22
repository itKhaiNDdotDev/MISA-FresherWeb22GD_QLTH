using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.FW0922GD.QLTH.Common.Entities.DTOs
{
    /// <summary>
    /// DTO trả về thông tin danh sách đối tượng cho trang tương ứng kèm tổng số bản ghi trên toàn bộ trang
    /// </summary>
    /// <typeparam name="T">class định kiểu của đối tượng Entity muốn lấy</typeparam>
    /// Created By: KhaiND (21/11/2022)
    public class PagingResult<T>
    {
        /// <summary>
        /// Tổng số bản ghi thỏa mãn
        /// </summary>
        public int totalRecord { get; set; }

        /// <summary>
        /// Đối tượng dữ liệu chứa danh sách chi tiết các bản ghi
        /// </summary>
        public List<T> data { get; set; }
    }
}
