using MISA.FW0922GD.QLTH.BL.BaseBL;
using MISA.FW0922GD.QLTH.Common.Entities;
using MISA.FW0922GD.QLTH.Common.Entities.DTOs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.FW0922GD.QLTH.BL.EmployeeBL
{
    public interface IEmployeeBL : IBaseBL<Employee>
    {
        /// <summary>
        /// Lấy danh sách Cán bộ, giáo viên được tìm kiếm theo Tên, Số điện thoại hoặc Email và phân trang
        /// </summary>
        /// <param name="keyword">Từ khóa truyền vào để tìm kiếm</param>
        /// <param name="pageIndex">Số trang hiện tại</param>
        /// <param name="pageSize">Số bản ghi trên một trang</param>
        /// <returns>Danh sách Cán bộ, giáo viên tìm thấy kèm thông tin phân trang cần thiết (tổng số kết quả)</returns>
        /// Created By: KhaiND (21/11/2022)
        public PagingResult<EmployeeResponse> GetSearchPaging(string? keyword, int pageIndex = 1, int pageSize = 32);

        /// <summary>
        /// Gợi ý mã nhân viên tự tăng thêm một giá trị từ mã nhân viên lớn nhất
        /// </summary>
        /// <returns>Mã nhân viên mới tự tăng</returns>
        /// Created By: KhaiND (20/22/2022)
        public string GetNewCode();

        /// <summary>
        /// Xóa đồng thời nhiều Cán bộ, giáo viên thông qua danh sách ID
        /// </summary>
        /// <param name="employeeIDs">Danh sách ID của các Cán bộ, giáo viên muốn xóa</param>
        /// <returns>Danh sách ID của các Các cán bộ, giáo viên đã xóa</returns>
        /// Created By: KhaiND (22/11/2022)
        public List<Guid> DeleteMany(List<Guid> employeeIDs);
    }
}
