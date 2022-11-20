using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.FW0922GD.QLTH.BL.EmployeeBL;
using MISA.FW0922GD.QLTH.Common;
using MISA.FW0922GD.QLTH.Common.Entities;
using MISA.FW0922GD.QLTH.Common.Entities.DTOs;
using MISA.FW0922GD.QLTH.Common.Enums;

namespace MISA.FW0922GD.QLTH.API.Controllers
{
    //[Route("api/v1/[controller]")]
    [ApiController]
    public class EmployeesController : BasesController<Employee>
    {

        #region Field

        private IEmployeeBL _employeeBL; 

        #endregion

        #region Constructor

        public EmployeesController(IEmployeeBL employeeBL) : base(employeeBL)
        {
            _employeeBL = employeeBL;
        }

        #endregion

        #region Method

        /// <summary>
        /// API trả về số hiệu cán bộ mới tự động tăng
        /// </summary>
        /// <returns>Số hiệu cán bộ mới</returns>
        [HttpGet("newCode")]
        public IActionResult GetNewCode()
        {
            try
            {
                var newCode = _employeeBL.GetNewCode();

                return StatusCode(StatusCodes.Status200OK, newCode);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                {
                    ErrorCode = GDErrorCode.Exception,
                    DevMsg = Resources.Exception_DevMsg,
                    UserMsg = Resources.Exception_UserMsg,
                    MoreInfo = Resources.Exception_MoreInfo,
                    TraceId = HttpContext.TraceIdentifier
                });
            }
        }

        #endregion
    }
}
