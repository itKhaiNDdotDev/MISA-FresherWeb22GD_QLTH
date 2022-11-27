using MISA.FW0922GD.QLTH.BL.BaseBL;
using MISA.FW0922GD.QLTH.Common.Entities;
using MISA.FW0922GD.QLTH.Common.Entities.DTOs;
using MISA.FW0922GD.QLTH.DL.BaseDL;
using MISA.FW0922GD.QLTH.DL.EmployeeDL;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static OfficeOpenXml.ExcelErrorValue;

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
        /// Lấy danh sách Cán bộ, giáo viên được tìm kiếm theo Tên, Số điện thoại hoặc Email và phân trang
        /// </summary>
        /// <param name="keyword">Từ khóa truyền vào để tìm kiếm</param>
        /// <param name="pageIndex">Số trang hiện tại</param>
        /// <param name="pageSize">Số bản ghi trên một trang</param>
        /// <returns>Danh sách Cán bộ, giáo viên tìm thấy kèm thông tin phân trang cần thiết (tổng số kết quả)</returns>
        /// Created By: KhaiND (21/11/2022)
        public PagingResult<EmployeeResponse> GetSearchPaging(string? keyword, int pageIndex = 1, int pageSize = 32)
        {
            int offset, limit;
            if (pageIndex < 1)
            {
                offset = 0;
            }
            else
            {
                offset = (pageIndex - 1) * pageSize;
            }
            if (pageSize < 1)
            {
                limit = 1;
            }
            else
            {
                limit = pageSize;
            }

            return _employeeDL.GetSearchPaging(keyword, offset, limit);
        }

        /// <summary>
        /// Xuất file Excel danh sách dữ liệu tất cả Cán bộ, giáo viên theo định dạng của bảng phân trang
        /// </summary>
        /// <returns>File danh sách tất cả Cán bộ, giáo viên</returns>
        /// Created By: KhaiND (26/11/2022)
        public Stream GetAllExport()
        {
            var employees = _employeeDL.GetAllExport();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var stream = new MemoryStream();
            var package = new ExcelPackage(stream);
            var workSheet = package.Workbook.Worksheets.Add("DANH SÁCH CBTB");
            package.Workbook.Properties.Author = "KhaiND";
            package.Workbook.Properties.Title = "Danh sách cán bộ giáo viên";
            FormatExcel(workSheet, employees);
            package.Save();
            stream.Position = 0; // Đặt con trỏ về đầu file để đọc
            return package.Stream;
        }

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

        /// <summary>
        /// Xóa đồng thời nhiều Cán bộ, giáo viên thông qua danh sách ID
        /// </summary>
        /// <param name="employeeIDs">Danh sách ID của các Cán bộ, giáo viên muốn xóa</param>
        /// <returns>Danh sách ID của các Các cán bộ, giáo viên đã xóa</returns>
        /// Created By: KhaiND (22/11/2022)
        public List<Guid> DeleteMany(List<Guid> employeeIDs)
        {
            return _employeeDL.DeleteMany(employeeIDs);
        }

        /// <summary>
        /// Kiểm tra số hiệu cán bộ có trùng hay không
        /// </summary>
        /// <param name="employeeCode">Số hiệu cán bộ muốn kiểm tra</param>
        /// <returns>true nếu có trùng và false nếu SHCB chưa tồn tại</returns>
        /// Created By: KhaiND 24/11/2022
        public bool CheckDuplicateCode(string employeeCode)
        {
            return _employeeDL.checkDuplicateCode(employeeCode);
        }

        /// <summary>
        /// Xử lý cho việc định dạng dữ liệu được export ra hiển thị trong file Excel
        /// </summary>
        /// <param name="workSheet">workSheet cho đối tượng Excel tương ứng của EPPlus</param>
        /// <param name="employees">Dữ liệu danh sách Cán bộ, giáo viên muốn export</param>
        /// Created By: KhaiND (26/11/2022)
        protected void FormatExcel(ExcelWorksheet workSheet, IEnumerable<EmployeeResponse> employees)
        {
            // Dựng phần tiêu đề của bảng
            using (var topCell = workSheet.Cells["A1:I2"])
            {
                topCell.Style.Font.Bold = true;
                topCell.Style.Font.Size = 16;
                topCell.Style.Font.Name = "Arial";
                topCell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                topCell.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            }
            workSheet.Cells["A1:I1"].Merge = true;
            workSheet.Cells["A2:I2"].Merge = true;
            workSheet.Cells["A1:I1"].Value = "DANH SÁCH CÁN BỘ - GIÁO VIÊN";


            // Style chung tất cả các ô trong bảng dữ liệu
            using (var contentCell = workSheet.Cells[$"A3:I{employees.Count() + 3}"])
            {
                //contentCell.Style.Border.BorderAround(ExcelBorderStyle.Thin);
                contentCell.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                contentCell.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                contentCell.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                contentCell.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                contentCell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                contentCell.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                contentCell.Style.Font.Name = "Times New Roman";
                contentCell.Style.Font.Size = 11;
                contentCell.AutoFitColumns();
            }
            workSheet.Cells[$"H4:I{employees.Count() + 3}"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            workSheet.Cells[$"H4:I{employees.Count() + 3}"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

            // Dựng ô tiêu đề từng cột của bảng dữ liệu
            workSheet.Cells["A3"].Value = "STT";
            workSheet.Cells["B3"].Value = "Số hiệu cán bộ";
            workSheet.Cells["C3"].Value = "Họ và tên";
            workSheet.Cells["D3"].Value = "Số điện thoại";
            workSheet.Cells["E3"].Value = "Tổ chuyên môn";
            workSheet.Cells["F3"].Value = "Quản lý theo môn";
            workSheet.Cells["G3"].Value = "Quản lý kho - phòng";
            workSheet.Cells["H3"].Value = "Đào tạo QLTB";
            workSheet.Cells["I3"].Value = "Đang làm việc";

            // Style cho ô tiêu đề các cột của bảng dữ liệu
            using (var headCell = workSheet.Cells["A3:I3"])
            {
                headCell.Style.Font.Bold = true;
                headCell.Style.Font.Size = 10;
                headCell.Style.Font.Name = "Arial";
                headCell.Style.Font.Color.SetColor(Color.Black);
                headCell.Style.Fill.PatternType = ExcelFillStyle.Solid;
                headCell.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                headCell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                headCell.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            }

            // Lấy dữ liệu từ danh sách nhân viên render vào ô tương ứng trong bảng
            int rowIndex = 1;
            foreach (var employee in employees)
            {
                workSheet.Cells[$"A{rowIndex + 3}"].Value = rowIndex;
                workSheet.Cells[$"B{rowIndex + 3}"].Value = employee.EmployeeCode;
                workSheet.Cells[$"C{rowIndex + 3}"].Value = employee.EmployeeName;
                workSheet.Cells[$"D{rowIndex + 3}"].Value = employee.EmployeePhoneNumber;
                workSheet.Cells[$"E{rowIndex + 3}"].Value = employee.DepartmentName;
                workSheet.Cells[$"F{rowIndex + 3}"].Value = string.Join(", ", employee.SubjectNames);
                workSheet.Cells[$"G{rowIndex + 3}"].Value = string.Join(", ", employee.RoomNames);
                workSheet.Cells[$"H{rowIndex + 3}"].Value = employee.EmployeeIsDeviceManager == Common.Enums.IsDeviceManager.DeviceManager? "x" : "";
                workSheet.Cells[$"I{rowIndex + 3}"].Value = employee.EmployeeWorkingStatus == Common.Enums.WorkingStatus.Working? "x" : "";
                rowIndex++;
                //workSheet.Columns.AutoFit();
            }
            workSheet.Columns.AutoFit();
        }

        #endregion
    }
}
