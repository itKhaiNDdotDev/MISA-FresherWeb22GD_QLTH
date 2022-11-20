using MISA.FW0922GD.QLTH.Common.Entities;
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
        /// Lấy số hiệu cán bộ lớn nhất
        /// </summary>
        /// <returns>Số hiệu cán bộ lớn nhất</returns>
        /// Created By: KhaiND (20/22/2022)
        public string GetMaxCode();
    }
}
