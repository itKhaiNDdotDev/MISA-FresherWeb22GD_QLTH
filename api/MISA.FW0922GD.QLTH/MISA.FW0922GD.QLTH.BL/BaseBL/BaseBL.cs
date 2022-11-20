using MISA.FW0922GD.QLTH.Common.Entities.DTOs;
using MISA.FW0922GD.QLTH.Common.Entities;
using MISA.FW0922GD.QLTH.DL.BaseDL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MISA.FW0922GD.QLTH.Common.Enums;
using MISA.FW0922GD.QLTH.Common;
using System.Net.Http;

namespace MISA.FW0922GD.QLTH.BL.BaseBL
{
    public class BaseBL<T> : IBaseBL<T>
    {
        #region Field

        private IBaseDL<T> _baseDL;

        #endregion

        #region Constructor

        public BaseBL(IBaseDL<T> baseDL)
        {
            _baseDL = baseDL;
        }

        #endregion

        #region Method

        /// <summary>
        /// Lấy danh sách tất cả bản ghi
        /// </summary>
        /// <returns>Danh sách tất cả bản ghi</returns>
        /// Created By: KhaiND (18/11/2022)
        public IEnumerable<T> GetAll()
        {
            return _baseDL.GetAll();
        }

        /// <summary>
        /// Lấy thông tin một bản ghi theo ID
        /// </summary>
        /// <param name="recordID">ID của bản ghi muốn lấy</param>
        /// <returns>Thông tin 1 bản ghi theo ID</returns>
        /// Created By: KhaiND (18/11/2022)
        public T GetByID(Guid recordID)
        {
            return _baseDL.GetByID(recordID);
        }

        /// <summary>
        /// Thêm mới một bản ghi
        /// </summary>
        /// <param name="record">Dữ liệu của bản ghi muốn thêm mới</param>
        /// <returns>ID của bản ghi vừa thêm mới</returns>
        /// Created By: KhaiND (19/11/2022)
        public ServiceResult Insert(T record)
        {
            var validateResult = ValidateRequestData(record);
            if (!validateResult.Success)
            {
                return new ServiceResult
                {
                    Success = false,
                    Data = new ErrorResult
                    {
                        ErrorCode = GDErrorCode.InvalidData,
                        DevMsg = Resources.Invalid_DevMsg,
                        UserMsg = Resources.Invalid_UserMsg,
                        MoreInfo = new
                        {
                            Detail = validateResult.Data,
                            Resources.Invalid_MoreInfo
                        }
                    }
                };
            }
            return new ServiceResult
            {
                Success = true,
                Data = _baseDL.Insert(record)
            };
        }

        /// <summary>
        /// Cập nhật thông tin một bản ghi
        /// </summary>
        /// <param name="record">Dữ liệu của bản ghi muốn thay đổi</param>
        /// <returns>ID của bản ghi vừa cập nhật</returns>
        /// Created By: KhaiND (19/11/2022)
        public Guid Update(Guid recordID, T record)
        {
            return _baseDL.Update(recordID, record);
        }

        /// <summary>
        /// Xóa một bản ghi thông qua ID
        /// </summary>
        /// <param name="recordID">ID của bản ghi muốn xóa</param>
        /// <returns>ID của bản ghi vừa xóa</returns>
        /// Created By: KhaiND (19/11/2022)
        public Guid Delete(Guid recordID)
        {
            return _baseDL.Delete(recordID);
        }

        /// <summary>
        /// Validate dữ liệu đầu vào của bản ghi
        /// </summary>
        /// <param name="record">Đối tượng bản ghi dữ liệu được truyền lên</param>
        /// <returns>Đối tượng kết quả Service Validate trả về</returns>
        private ServiceResult ValidateRequestData(T record)
        {
            // Lấy danh sách thuộc tính của Entity
            var properties = typeof(T).GetProperties();

            // Duyệt qua từng thuộc tính
            var validateFailures = new List<string>();
            foreach (var property in properties)
            {
                var propertyValue = property.GetValue(record);
                var requiredAtribute = (RequiredAttribute?)Attribute.GetCustomAttribute(property, typeof(RequiredAttribute));
                if (requiredAtribute != null && string.IsNullOrEmpty(propertyValue?.ToString()))
                {
                    validateFailures.Add(requiredAtribute.ErrorMessage);
                }
            }

            if (validateFailures.Count > 0)
            {
                return new ServiceResult
                {
                    Success = false,
                    Data = validateFailures
                };
            }
            return new ServiceResult { Success = true };
        }

        #endregion

    }
}
