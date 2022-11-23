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
        /// API tìm kiếm Cán bộ, giáo viên theo từ khóa của Họ tên, Số điện thoại, Email và phân trang
        /// </summary>
        /// <param name="keyword">Từ khóa tìm kiếm</param>
        /// <param name="pageIndex">Số trang hiện tại</param>
        /// <param name="pageSize">Số bản ghi trên một trang</param>
        /// <returns>Dữ liệu danh sách cán bộ tương ứng kết quả tìm kiếm và phân trang</returns>
        [HttpGet("search")]
        public IActionResult GetSearchPaging([FromQuery] string? keyword, [FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 32)
        {
            try
            {
                // Không cần xét các trường hợp đầu vào trống vì đã xét trong Dattabase và DL
                var result = _employeeBL.GetSearchPaging(keyword, pageIndex, pageSize);
                if(result == null || result.totalRecord == 0 || result.data == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound);
                }
                return StatusCode(StatusCodes.Status200OK, result);
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

        /// <summary>
        /// API xóa đồng thời nhiều Cán bộ, giáo viên từ danh sách ID
        /// </summary>
        /// <param name="employeeIDs">Danh sách ID của các Cán bộ, giáo viên muốn xóa</param>
        /// <returns>Danh sách ID của các Cán bộ, giáo viên đã xóa</returns>
        /// Created By: KhaiND (22/11/2022)
        [HttpDelete("deleteMany")]
        public IActionResult DeleteMany([FromBody] List<Guid> employeeIDs)
        {
            try
            {
                var deletedIDs = _employeeBL.DeleteMany(employeeIDs);
                if(deletedIDs != null && deletedIDs.Count > 0)
                {
                    return StatusCode(StatusCodes.Status200OK, deletedIDs);
                }
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                {
                    ErrorCode = GDErrorCode.Exception,
                    DevMsg = Resources.Exception_DevMsg,
                    UserMsg = Resources.Exception_UserMsg,
                    MoreInfo= Resources.Exception_MoreInfo,
                    TraceId = HttpContext.TraceIdentifier
                });
            }
        }

        #endregion
    }
}
