using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.FW0922GD.QLTH.DL.BaseDL
{
    /// <summary>
    /// Interface định nghĩa các method dùng chung cho nhiều DL
    /// </summary>
    /// <typeparam name="T">Class của đối tượng dữ liệu tương ứng</typeparam>
    /// Created By: KhaiND (16/11/2022)
    public interface IBaseDL<T>
    {
        /// <summary>
        /// Lấy danh sách tất cả bản ghi
        /// </summary>
        /// <returns>Danh sách tất cả bản ghi</returns>
        /// Created By: KhaiND (16/11/2022)
        public IEnumerable<T> GetAll();

        /// <summary>
        /// Lấy thông tin một bản ghi theo ID
        /// </summary>
        /// <param name="recordID">ID của bản ghi muốn lấy</param>
        /// <returns>Thông tin 1 bản ghi theo ID</returns>
        /// Created By: KhaiND (16/11/2022)
        public T GetByID(Guid recordID);

        /// <summary>
        /// Thêm mới một bản ghi
        /// </summary>
        /// <param name="record">Dữ liệu của bản ghi muốn thêm mới</param>
        /// <returns>ID của bản ghi vừa thêm mới</returns>
        /// Created By: KhaiND (19/11/2022)
        public Guid Insert(T record);

        /// <summary>
        /// Cập nhật thông tin một bản ghi
        /// </summary>
        /// <param name="record">Dữ liệu của bản ghi muốn thay đổi</param>
        /// <returns>ID của bản ghi vừa cập nhật</returns>
        /// Created By: KhaiND (19/11/2022)
        public Guid Update(Guid recordID, T record);

        /// <summary>
        /// Xóa một bản ghi thông qua ID
        /// </summary>
        /// <param name="recordID">ID của bản ghi muốn xóa</param>
        /// <returns>ID của bản ghi vừa xóa</returns>
        /// Created By: KhaiND (19/11/2022)
        public Guid Delete(Guid recordID);
    }
}
