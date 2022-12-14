using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.FW0922GD.QLTH.BL.BaseBL;
using MISA.FW0922GD.QLTH.Common;
using MISA.FW0922GD.QLTH.Common.Entities.DTOs;
using MISA.FW0922GD.QLTH.Common.Enums;
using System.Runtime.Versioning;

namespace MISA.FW0922GD.QLTH.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BasesController<T> : ControllerBase
    {

        #region Field

        private IBaseBL<T> _baseBL;

        #endregion

        #region Constructor

        public BasesController(IBaseBL<T> baseBL)
        {
            _baseBL = baseBL;
        }

        #endregion

        #region Method

        /// <summary>
        /// API lấy danh sách tất cả bản ghi
        /// </summary>
        /// <returns>Danh sách tất cả bản ghi</returns>
        /// Created By: KhaiND (19/11/2022)
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var records = _baseBL.GetAll();

                if (records != null)
                {
                    return StatusCode(StatusCodes.Status200OK, records);
                }

                return StatusCode(StatusCodes.Status200OK, new List<T>());
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
        /// API lấy thông tin bản ghi thông qua ID
        /// </summary>
        /// <param name="recordID">ID của bản ghi</param>
        /// <returns>Thông tin chi tiết của bản ghi</returns>
        /// Created By: KhaiND (19/11/2022)
        [HttpGet("{recordID}")]
        public IActionResult GetByID([FromRoute] Guid recordID)
        {
            try
            {
                var record = _baseBL.GetByID(recordID);

                if(record != null)
                {
                    return StatusCode(StatusCodes.Status200OK, record);
                }

                return StatusCode(StatusCodes.Status404NotFound);
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
        /// API thêm mới một bản ghi
        /// </summary>
        /// <param name="record">Dữ liệu của bản ghi muốn thêm</param>
        /// <returns>ID của bản ghi vừa được thêm</returns>
        /// Created By: KhaiND (19/11/2022)
        [HttpPost]
        public IActionResult Insert([FromBody] T record)
        {
            try
            {
                var result = _baseBL.Insert(record);

                if (result.Success)
                {
                    return StatusCode(StatusCodes.Status201Created, result.Data);
                }

                var errorResult = new ErrorResult();
                errorResult = (ErrorResult) result.Data;
                errorResult.TraceId = HttpContext.TraceIdentifier;
                return StatusCode(StatusCodes.Status400BadRequest, result.Data);
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
        /// API cập nhật thông tin một bản ghi
        /// </summary>
        /// <param name="recordID">ID của bản ghi muốn chỉnh sửa</param>
        /// <param name="record">Dữ liệu muốn đưa vào cập nhật cho bản ghi</param>
        /// <returns>ID của bản ghi vừa cập nhật</returns>
        /// Created By: KhaiND (19/11/2022)
        [HttpPut("{recordID}")]
        public IActionResult Update([FromRoute] Guid recordID, [FromBody] T record)
        {
            try
            {
                // Xét 404 khi recordID không tìm thấy
                var foundRecord = _baseBL.GetByID(recordID);
                if (foundRecord == null)
                {
                    StatusCode(StatusCodes.Status404NotFound);
                }    

                // TÌm thấy thì gọi BL tiến hành thủ tục cập nhật
                var result = _baseBL.Update(recordID, record);

                if (result.Success)
                {
                    return StatusCode(StatusCodes.Status200OK, result.Data);
                }

                var errorResult = new ErrorResult();
                errorResult = (ErrorResult) result.Data;
                errorResult.TraceId = HttpContext.TraceIdentifier;
                return StatusCode(StatusCodes.Status400BadRequest, result.Data);
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
        /// API xóa một bản ghi thông qua ID
        /// </summary>
        /// <param name="recordID">ID của bản ghi muốn xóa</param>
        /// <returns>ID của bản ghi vừa xóa</returns>
        /// Created By: KhaiND (19/11/2022)
        [HttpDelete("{recordID}")]
        public IActionResult Delete([FromRoute] Guid recordID)
        {
            try
            {
                // Xét trường hợp không tìm thấy
                var record = _baseBL.GetByID(recordID);
                if(record == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound);
                }    

                Guid resID = _baseBL.Delete(recordID);

                if (resID != Guid.Empty)
                {
                    return StatusCode(StatusCodes.Status200OK, resID);
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
