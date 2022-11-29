using ClosedXML.Excel;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.FW0922GD.QLTH.BL.EmployeeBL;
using MISA.FW0922GD.QLTH.Common;
using MISA.FW0922GD.QLTH.Common.Entities;
using MISA.FW0922GD.QLTH.Common.Entities.DTOs;
using MISA.FW0922GD.QLTH.Common.Enums;
using MISA.FW0922GD.QLTH.DL.EmployeeDL;
using System.Data;
using System.Text;

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
        /// API xuất khẩu dữ liệu danh sách cán bộ ra file Excel
        /// </summary>
        /// <returns>Flie Excel danh sách cán bộ</returns>
        /// Created By: KhaiND (27/11/2022)
        [HttpGet("export")]
        public IActionResult Export()
        {
            try
            {
                var stream = _employeeBL.GetAllExport();
                string fileName = "Danh_sach_can_bo_giao_vien.xlsx";
                Response.Headers.Add("Content-Disposition", "attachment; filename=" + fileName);
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
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
        [HttpPost("deleteMany")]
        public IActionResult DeleteMany([FromBody] List<Guid> employeeIDs)
        {
            try
            {
                var deletedIDs = _employeeBL.DeleteMany(employeeIDs);
                if (deletedIDs != null && deletedIDs.Count > 0)
                {
                    return StatusCode(StatusCodes.Status200OK, deletedIDs);
                }
                return StatusCode(StatusCodes.Status400BadRequest, new ErrorResult
                {
                    ErrorCode = GDErrorCode.DeleteFailed,
                    DevMsg = Resources.DeleteFailed_DevMsg,
                    UserMsg = Resources.DeleteFailed_UserMsg,
                    MoreInfo = Resources.DeleteFailed_MoreInfo,
                    TraceId = HttpContext.TraceIdentifier
                });
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

        /// <summary>
        /// API kiểm tra trùng Số hiệu Cán bộ
        /// </summary>
        /// <param name="employeeCode">Số hiệu Cán bộ muốn kiểm tra</param>
        /// <returns>Kết quả kiểm tra nếu SHCB đã tồn tại thì trả về 1, ngược lại trả về 0</returns>
        [HttpGet("checkDuplicateCode")]
        public IActionResult CheckDuplicatCode([FromQuery] Guid? employeeID, [FromQuery] string employeeCode)
        {
            try
            {
                var result = _employeeBL.CheckDuplicateCode(employeeID, employeeCode);
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

        #endregion
    }
}
