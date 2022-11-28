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
using System.Text.RegularExpressions;
using MISA.FW0922GD.QLTH.Common.Constants;
using MISA.FW0922GD.QLTH.DL.EmployeeDL;

namespace MISA.FW0922GD.QLTH.BL.BaseBL
{
    public class BaseBL<T> : IBaseBL<T>
    {
        #region Field

        private IBaseDL<T> _baseDL;

        private IEmployeeDL _employeeDL; // Phục vụ cho kiểm tra trùng mã

        #endregion

        #region Constructor

        public BaseBL(IBaseDL<T> baseDL, IEmployeeDL employeeDL)
        {
            _baseDL = baseDL;
            _employeeDL = employeeDL;
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
            var validateResult = this.ValidateRequestData(null, record, DataAction.Post);
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
                        },
                        TraceId = null
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
        public ServiceResult Update(Guid recordID, T record)
        {
            var validateResult = ValidateRequestData(recordID, record, DataAction.Put);
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
                        },
                        TraceId = null
                    }
                };
            }
            return new ServiceResult
            {
                Success = true,
                Data = _baseDL.Update(recordID, record)
            };
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
        /// Created By: KhaiND (27/11/2022)
        private ServiceResult ValidateRequestData(Guid? recordID, T record, DataAction dataAction)
        {
            // Lấy danh sách thuộc tính của Entity
            var properties = typeof(T).GetProperties();

            // Duyệt qua từng thuộc tính
            var validateFailures = new List<string>();
            foreach (var property in properties)
            {
                var propertyValue = property.GetValue(record);

                // Validate các trường thông tin bắt buộc
                var requiredAttribute = (RequiredAttribute?)Attribute.GetCustomAttribute(property, typeof(RequiredAttribute));
                if (requiredAttribute != null && string.IsNullOrEmpty(propertyValue?.ToString()))
                {
                    validateFailures.Add(requiredAttribute.ErrorMessage);
                }

                // Validate Số điện thoại
                var phoneAttribute = (PhoneAttribute?)Attribute.GetCustomAttribute(property, typeof(PhoneAttribute));
                if (phoneAttribute != null && IsExistValue(propertyValue) && !IsValidPhone(propertyValue.ToString()))
                {
                    validateFailures.Add(phoneAttribute.ErrorMessage);
                }

                // Validate Email
                var emailAttribute = (EmailAddressAttribute?)Attribute.GetCustomAttribute(property, typeof(EmailAddressAttribute));
                if (emailAttribute != null && IsExistValue(propertyValue) && !IsValidEmail(propertyValue.ToString()))
                {
                    validateFailures.Add(emailAttribute.ErrorMessage);
                }

                // Valiate trùng mã
                if (property.Name == "EmployeeCode")
                {
                    if (dataAction == DataAction.Post)
                    {
                        // Kiểm tra trùng mã
                        if (_employeeDL.checkDuplicateCode(propertyValue.ToString()))
                        {
                            validateFailures.Add(Mesage.EMPLOYEE_CODE_DUPLICATE);
                        }
                    }
                    else if (dataAction == DataAction.Put)
                    {

                        // Xét trường hợp không là mã của chính mình
                        if (propertyValue.ToString() != _baseDL.GetMyCode(recordID.Value))
                        {
                            if (_employeeDL.checkDuplicateCode(propertyValue.ToString()))
                            {
                                validateFailures.Add(Mesage.EMPLOYEE_CODE_DUPLICATE);
                            }
                        }
                    }
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

        /// <summary>
        /// Thực hiện kiểm tra xem giá trị dữ liệu đã được nhập hay chưa
        /// </summary>
        /// <param name="propertyValue">Dữ liệu tương ứng prop cần kiểm tra</param>
        /// <returns>true nếu đã nhập dữ liệu và false với trường hợp ngược lại</returns>
        private static bool IsExistValue(object? propertyValue)
        {
            return !string.IsNullOrEmpty(propertyValue?.ToString()) && propertyValue?.ToString().Length > 0;
        }

        /// <summary>
        /// Kiểm tra xem email có định dạng hợp lệ hay không
        /// </summary>
        /// <param name="inputEmail">Chuỗi email cần kiểm tra</param>
        /// <returns>true nếu email là hợp lệ và false với trường hợp ngược lại</returns>
        private static bool IsValidEmail(string inputEmail)
        {
            //Regex validMailPartern = new Regex(@"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|" + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)" + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$");
            Regex validMailPartern = new Regex(@"^([a-zA-Z0-9])+([a-zA-Z0-9\._-])*@([a-zA-Z0-9_-])+([a-zA-Z0-9\._-]+)+$");
            return validMailPartern.IsMatch(inputEmail);
        }

        /// <summary>
        /// Kiểm tra xem số điện thoại có định dạng hợp lệ hay không
        /// </summary>
        /// <param name="phoneNumber">Chuỗi số điện thoại cần kiểm tra</param>
        /// <returns>true nếu số điện thoại là hợp lệ và false với trường hợp ngược lại</returns>
        /// Created By: KhaiND (27/11/2022)
        private static bool IsValidPhone(string phoneNumber)
        {
            Regex numberPartern = new Regex(@"\d+");
            return numberPartern.IsMatch(phoneNumber) && (phoneNumber.Length >= Number.PHONE_MIN_LENGTH) && (phoneNumber.Length <= Number.PHONE_MAX_LENGTH);
        }

        #endregion

    }
    }
