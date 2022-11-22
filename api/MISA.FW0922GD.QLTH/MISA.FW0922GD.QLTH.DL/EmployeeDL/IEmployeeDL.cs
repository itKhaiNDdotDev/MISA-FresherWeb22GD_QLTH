using MISA.FW0922GD.QLTH.Common.Entities;
using MISA.FW0922GD.QLTH.Common.Entities.DTOs;
using MISA.FW0922GD.QLTH.DL.BaseDL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.FW0922GD.QLTH.DL.EmployeeDL
{
    public interface IEmployeeDL : IBaseDL<Employee>
    {
        /// <summary>
        /// Lấy danh sách Cán bộ, giáo viên được tìm kiếm theo Tên, Số điện thoại hoặc Email và phân trang
        /// </summary>
        /// <param name="keyword">Từ khóa để tìm kiếm</param>
        /// <param name="offset">Thứ tự bản ghi bắt đầu của trang tính từ 0 trong Database</param>
        /// <param name="limmit">Số lượng bản ghi trên mỗi trang</param>
        /// <returns>Danh sách Cán bộ, giáo viên tìm thấy tương ứng với trang được chỉ định</returns>
        /// Created By: KhaiND (21/11/2022)
        public PagingResult<EmployeeResponse> GetSearchPaging(string? keyword, int offset = 0, int limmit = 32);

        /// <summary>
        /// Lấy số hiệu cán bộ lớn nhất
        /// </summary>
        /// <returns>Số hiệu cán bộ lớn nhất</returns>
        /// Created By: KhaiND (20/22/2022)
        public string GetMaxCode();

        /// <summary>
        /// Xóa đồng thời nhiều Cán bộ, giáo viên thông qua danh sách ID
        /// </summary>
        /// <param name="employeeIDs">Danh sách ID của các Cán bộ, giáo viên muốn xóa</param>
        /// <returns>Danh sách ID của các Các cán bộ, giáo viên đã xóa</returns>
        /// Created By: KhaiND (22/11/2022)
        public List<Guid> DeleteMany(List<Guid> employeeIDs);
    }
}
