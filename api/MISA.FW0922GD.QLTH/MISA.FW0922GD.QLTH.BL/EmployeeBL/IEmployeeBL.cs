using MISA.FW0922GD.QLTH.BL.BaseBL;
using MISA.FW0922GD.QLTH.Common.Entities;
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
        /// Gợi ý mã nhân viên tự tăng thêm một giá trị từ mã nhân viên lớn nhất
        /// </summary>
        /// <returns>Mã nhân viên mới tự tăng</returns>
        /// Created By: KhaiND (20/22/2022)
        public string GetNewCode();
    }
}
