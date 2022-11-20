using MISA.FW0922GD.QLTH.BL.BaseBL;
using MISA.FW0922GD.QLTH.Common.Entities;
using MISA.FW0922GD.QLTH.Common.Entities.DTOs;
using MISA.FW0922GD.QLTH.DL.BaseDL;
using MISA.FW0922GD.QLTH.DL.EmployeeDL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MISA.FW0922GD.QLTH.BL.EmployeeBL
{
    public class EmployeeBL : BaseBL<Employee>, IEmployeeBL
    {

        #region Field

        private IEmployeeDL _employeeDL;

        #endregion

        #region Constructor

        public EmployeeBL(IEmployeeDL employeeDL) : base(employeeDL)
        {
            _employeeDL = employeeDL;
        }

        #endregion

        #region Method

        /// <summary>
        /// Gợi ý mã nhân viên mới tự tăng thêm một giá trị từ mã nhân viên lớn nhất
        /// </summary>
        /// <returns>Mã nhân viên mới tự tăng</returns>
        /// Created By: KhaiND (20/22/2022)
        public string GetNewCode()
        {
            // Lây SHCB lớn nhất từ tầng DL
            var maxCode = _employeeDL.GetMaxCode();

            // Tách lấy phần số
            int newDecCode = 0;
            if (Int32.TryParse(Regex.Match(maxCode, @"\d+").Value, out int decCode))
            {
                newDecCode = decCode + 1;
            }

            // Ghép giá trị số hiệu tự tăng với tiền tố và trả về kết quả
            return "SHCB" + newDecCode;
        }

        #endregion
    }
}
