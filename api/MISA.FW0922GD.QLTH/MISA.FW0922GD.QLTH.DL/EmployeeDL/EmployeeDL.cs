using Dapper;
using MISA.FW0922GD.QLTH.Common;
using MISA.FW0922GD.QLTH.Common.Constants;
using MISA.FW0922GD.QLTH.Common.Entities;
using MISA.FW0922GD.QLTH.Common.Entities.DTOs;
using MISA.FW0922GD.QLTH.DL.BaseDL;
using MySqlConnector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace MISA.FW0922GD.QLTH.DL.EmployeeDL
{
    public class EmployeeDL : BaseDL<Employee>, IEmployeeDL
    {
        /// <summary>
        /// Lấy danh sách Cán bộ, giáo viên được tìm kiếm theo Tên, Số điện thoại hoặc Email và phân trang
        /// </summary>
        /// <param name="keyword">Từ khóa để tìm kiếm</param>
        /// <param name="offset">Thứ tự bản ghi bắt đầu của trang tính từ 0 trong Database</param>
        /// <param name="limmit">Số lượng bản ghi trên mỗi trang</param>
        /// <returns>Danh sách Cán bộ, giáo viên tìm thấy tương ứng với trang được chỉ định</returns>
        /// Created By: KhaiND (21/11/2022)
        public PagingResult<EmployeeResponse> GetSearchPaging(string? keyword, int offset = 0, int limmit = 32)
        {
            // Chuẩn bị tham số đầu vào
            var parameters = new DynamicParameters();
            parameters.Add("@Keyword", keyword);
            parameters.Add("Offset", offset);
            parameters.Add("Limit", limmit);

            // Chuẩn bị câu lệnh SQL
            string storedProcedureName = "Proc_Employee_GetSearchPaging";

            // Khởi tọa kết nối đến Database MySQL
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                // Thực hiện gọi truy vấn vào Database
                var multipleResult = mySqlConnection.QueryMultiple(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);

                var employees = multipleResult.Read<EmployeeResponse>().ToList();
                foreach (var employee in employees)
                {
                    // Chuẩn bị tham số đầu vào là EmployeeID để truy xuất khóa ngoại n-n
                    var paramEmployeeID = new DynamicParameters();
                    paramEmployeeID.Add("EmployeeID", employee.EmployeeID);

                    // Khởi tọa kết nối con phục vụ truy vấn danh sách tên từ khóa ngoại
                    using (var subMySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
                    {
                        // Thực hiện truy vấn để lấy danh sách tên Môn học của Cán bộ, giáo viên
                        string storeProcedureSubject = "Proc_Employee_GetSubjectNamesByID";
                        var subjectNames = subMySqlConnection.Query<string>(storeProcedureSubject, paramEmployeeID, commandType: System.Data.CommandType.StoredProcedure);
                        employee.SubjectNames = subjectNames.ToList();

                        // Thực hiện truy vấn để lấy danh sách tên Kho, phòng của Cán bộ, giáo viên
                        string storeProcedureRoom = "Proc_Employee_GetRoomNamesByID";
                        var roomNames = subMySqlConnection.Query<string>(storeProcedureRoom, paramEmployeeID, commandType: System.Data.CommandType.StoredProcedure);
                        employee.RoomNames = roomNames.ToList();
                    }
                }

                int totalRecord = multipleResult.Read<int>().Single();
                return new PagingResult<EmployeeResponse>
                {
                    totalRecord = totalRecord,
                    data = employees
                };
            }
        }

        /// <summary>
        /// Lấy ra tất cả dữ liệu Cán bộ, giáo viên theo định dạng của bảng phân trang
        /// </summary>
        /// <returns>Danh sách thông tin tất cả Cán bộ, giáo viên</returns>
        /// Created By: KhaiND (26/11/2022)
        public IEnumerable<EmployeeResponse> GetAllExport()
        {
            // Chuẩn bị câu lệnh SQL
            string storedProcedureName = "Proc_Employee_GetAllExport";

            // Khởi tọa kết nối đến Database MySQL
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                // Thực hiện gọi truy vấn vào Database
                var employees = mySqlConnection.Query<EmployeeResponse>(storedProcedureName, commandType: System.Data.CommandType.StoredProcedure);

                foreach (var employee in employees)
                {
                    // Chuẩn bị tham số đầu vào là EmployeeID để truy xuất khóa ngoại n-n
                    var paramEmployeeID = new DynamicParameters();
                    paramEmployeeID.Add("EmployeeID", employee.EmployeeID);

                    // Khởi tọa kết nối con phục vụ truy vấn danh sách tên từ khóa ngoại
                    using (var subMySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
                    {
                        // Thực hiện truy vấn để lấy danh sách tên Môn học của Cán bộ, giáo viên
                        string storeProcedureSubject = "Proc_Employee_GetSubjectNamesByID";
                        var subjectNames = subMySqlConnection.Query<string>(storeProcedureSubject, paramEmployeeID, commandType: System.Data.CommandType.StoredProcedure);
                        employee.SubjectNames = subjectNames.ToList();

                        // Thực hiện truy vấn để lấy danh sách tên Kho, phòng của Cán bộ, giáo viên
                        string storeProcedureRoom = "Proc_Employee_GetRoomNamesByID";
                        var roomNames = subMySqlConnection.Query<string>(storeProcedureRoom, paramEmployeeID, commandType: System.Data.CommandType.StoredProcedure);
                        employee.RoomNames = roomNames.ToList();
                    }
                }

                return employees;
            }
        }

        /// <summary>
        /// Overide: Lấy thông tin một Cán bộ, giáo viên theo ID
        /// </summary>
        /// <param name="recordID">ID của Cán bộ, giáo viên muốn lấy</param>
        /// <returns>Thông tin chi tiết của Cán bộ, giáo viên theo ID</returns>
        /// Created By: KhaiND (20/11/2022)
        public Employee GetByID(Guid employeeID)
        {
            // Chuẩn bị tham số đầu vào
            var parameters = new DynamicParameters();
            parameters.Add("EmployeeID", employeeID);

            // Chuẩn bị câu lệnh SQL
            string storedProcedureName = "Proc_Employee_GetByID";
            string storedProcedureGetSubjects = "Proc_EmployeeWithSubject_GetByEmployeeID";
            string storedProcedureGetRooms = "Proc_EmployeeWithRoom_GetByEmployeeID";

            // Khởi tạo kết nối đến Database MySQL
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                // Thực hiện gọi truy vấn vào Database
                var employee = mySqlConnection.QueryFirstOrDefault<Employee>(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);

                if (employee != null)
                {
                    var subjects = (List<Guid>)mySqlConnection.Query<Guid>(storedProcedureGetSubjects, parameters, commandType: System.Data.CommandType.StoredProcedure);
                    var rooms = (List<Guid>)mySqlConnection.Query<Guid>(storedProcedureGetRooms, parameters, commandType: System.Data.CommandType.StoredProcedure);
                    employee.SubjectIDs = subjects;
                    employee.RoomIDs = rooms;
                }

                return employee;
            }
        }

        /// <summary>
        /// Thực hiện lấy số hiệu cán bộ lớn nhất
        /// </summary>
        /// <returns>Số hiệu cán bộ lớn nhất</returns>
        /// Created By: KhaiND (20/22/2022)
        public string GetMaxCode()
        {
            // Chuẩn bị câu lệnh SQL
            string storedProcedureName = "Proc_Employee_GetMaxCode";

            // Khởi tạo kết nối đến Database MySQL
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                // Thực hiện gọi truy vấn vào Database và trả về kết quả
                var maxCode = mySqlConnection.Query<string>(storedProcedureName, commandType: System.Data.CommandType.StoredProcedure).First();
                return maxCode;
            }
        }

        /// <summary>
        /// Overide: Thêm mới một Cán bộ, giáo viên
        /// </summary>
        /// <param name="employee">Dữ liệu của Cán bộ, giáo viên muốn thêm mới</param>
        /// <returns>ID của Cán bộ, giáo viên vừa thêm mới</returns>
        /// Created By: KhaiND (20/11/2022)
        public Guid Insert(Employee employee)
        {
            // Chuẩn bị tham số đầu vào
            var parameters = new DynamicParameters();
            var newID = Guid.NewGuid();
            parameters.Add("EmployeeID", newID);
            parameters.Add("EmployeeCode", employee.EmployeeCode);
            parameters.Add("EmployeeName", employee.EmployeeName);
            parameters.Add("EmployeeImageUrl", employee.EmployeeImageUrl);
            parameters.Add("EmployeePhoneNumber", employee.EmployeePhoneNumber);
            parameters.Add("EmployeeEmail", employee.EmployeeEmail);
            parameters.Add("DepartmentID", employee.DepartmentID);
            parameters.Add("EmployeeIsDeviceManager", employee.EmployeeIsDeviceManager);
            parameters.Add("EmployeeWorkingStatus", employee.EmployeeWorkingStatus);
            parameters.Add("EmployeeQuitDate", employee.EmployeeQuitDate);
            parameters.Add("CreatedBy", employee.CreatedBy);
            parameters.Add("ModifiedBy", employee.ModifiedBy);

            // Chuẩn bị câu lệnh SQL
            string storedProcedureName = "Proc_Employee_Insert";

            // Khởi tạo kết nối đến Database MySQL
            int affactedRecordCount = 0;
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                // Thực hiện gọi truy vấn vào Database
                affactedRecordCount = mySqlConnection.Execute(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);
                if (affactedRecordCount > 0)
                {
                    // Thục hiện thêm các cặp khóa ngoại Môn học
                    if (employee.SubjectIDs != null && employee.SubjectIDs.Count > 0)
                    {
                        string storedProcedureSubject = "Proc_EmployeeWithSubject_Insert";
                        foreach (var subjectID in employee.SubjectIDs)
                        {
                            var paramSubject = new DynamicParameters();
                            paramSubject.Add("EmployeeWithSubjectID", Guid.NewGuid());
                            paramSubject.Add("EmployeeID", newID);
                            paramSubject.Add("SubjectID", subjectID);
                            paramSubject.Add("CreatedBy", employee.CreatedBy);
                            paramSubject.Add("ModifiedBy", employee.ModifiedBy);

                            affactedRecordCount += mySqlConnection.Execute(storedProcedureSubject, paramSubject, commandType: System.Data.CommandType.StoredProcedure);
                        }
                    }

                    // Thục hiện thêm các cặp khóa ngoại Kho, phòng
                    if (employee.RoomIDs != null && employee.RoomIDs.Count > 0)
                    {
                        string storedProcedureRoom = "Proc_EmployeeWithRoom_Insert";
                        foreach (var roomID in employee.RoomIDs)
                        {
                            var paramRoom = new DynamicParameters();
                            paramRoom.Add("EmployeeWithRoomID", Guid.NewGuid());
                            paramRoom.Add("EmployeeID", newID);
                            paramRoom.Add("RoomID", roomID);
                            paramRoom.Add("CreatedBy", employee.CreatedBy);
                            paramRoom.Add("ModifiedBy", employee.ModifiedBy);

                            affactedRecordCount += mySqlConnection.Execute(storedProcedureRoom, paramRoom, commandType: System.Data.CommandType.StoredProcedure);
                        }
                    }
                }
            }

            // Xử lý kết quả trả về
            if (affactedRecordCount > 0)
            {
                return newID;
            }
            else
            {
                return Guid.Empty;
            }
        }

        /// <summary>
        /// Cập nhật thông tin một bản ghi
        /// </summary>
        /// <param name="record">Dữ liệu của bản ghi muốn thay đổi</param>
        /// <returns>ID của bản ghi vừa cập nhật</returns>
        /// Created By: KhaiND (19/11/2022)
        public Guid Update(Guid employeeID, Employee employee)
        {
            // Chuẩn bị tham số đầu vào
            var parameters = new DynamicParameters();
            parameters.Add("EmployeeID", employeeID);
            parameters.Add("EmployeeCode", employee.EmployeeCode);
            parameters.Add("EmployeeName", employee.EmployeeName);
            parameters.Add("EmployeeImageUrl", employee.EmployeeImageUrl);
            parameters.Add("EmployeePhoneNumber", employee.EmployeePhoneNumber);
            parameters.Add("EmployeeEmail", employee.EmployeeEmail);
            parameters.Add("DepartmentID", employee.DepartmentID);
            parameters.Add("EmployeeIsDeviceManager", employee.EmployeeIsDeviceManager);
            parameters.Add("EmployeeWorkingStatus", employee.EmployeeWorkingStatus);
            parameters.Add("EmployeeQuitDate", employee.EmployeeQuitDate);
            parameters.Add("CreatedBy", employee.CreatedBy);
            parameters.Add("ModifiedBy", employee.ModifiedBy);

            // Chuẩn bị câu lệnh SQL
            string storedProcedureName = "Proc_Employee_Update";

            // Khởi tạo kết nối đến Database MySQL
            int affactedRecordCount = 0;
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                // Thực hiện gọi truy vấn vào Database
                affactedRecordCount = mySqlConnection.Execute(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);
                if (affactedRecordCount > 0)
                {
                    // Xóa các cặp khóa ngoại cũ đi
                    string storeProcedureDeleteSubject = "Proc_EmployeeWithSubject_DeleteByEmployeeID";
                    affactedRecordCount += mySqlConnection.Execute(storeProcedureDeleteSubject, parameters, commandType: System.Data.CommandType.StoredProcedure);
                    string storeProcedureDeleteRoom = "Proc_EmployeeWithRoom_DeleteByEmployeeID";
                    affactedRecordCount += mySqlConnection.Execute(storeProcedureDeleteRoom, parameters, commandType: System.Data.CommandType.StoredProcedure);

                    // Thục hiện thêm các cặp khóa ngoại Môn học
                    if (employee.SubjectIDs != null && employee.SubjectIDs.Count > 0)
                    {
                        string storedProcedureSubject = "Proc_EmployeeWithSubject_Insert";
                        foreach (var subjectID in employee.SubjectIDs)
                        {
                            var paramSubject = new DynamicParameters();
                            paramSubject.Add("EmployeeWithSubjectID", Guid.NewGuid());
                            paramSubject.Add("EmployeeID", employeeID);
                            paramSubject.Add("SubjectID", subjectID);
                            paramSubject.Add("CreatedBy", employee.CreatedBy);
                            paramSubject.Add("ModifiedBy", employee.ModifiedBy);

                            affactedRecordCount += mySqlConnection.Execute(storedProcedureSubject, paramSubject, commandType: System.Data.CommandType.StoredProcedure);
                        }
                    }

                    // Thục hiện thêm các cặp khóa ngoại Kho, phòng
                    if (employee.RoomIDs != null && employee.RoomIDs.Count > 0)
                    {
                        string storedProcedureRoom = "Proc_EmployeeWithRoom_Insert";
                        foreach (var roomID in employee.RoomIDs)
                        {
                            var paramRoom = new DynamicParameters();
                            paramRoom.Add("EmployeeWithRoomID", Guid.NewGuid());
                            paramRoom.Add("EmployeeID", employeeID);
                            paramRoom.Add("RoomID", roomID);
                            paramRoom.Add("CreatedBy", employee.CreatedBy);
                            paramRoom.Add("ModifiedBy", employee.ModifiedBy);

                            affactedRecordCount += mySqlConnection.Execute(storedProcedureRoom, paramRoom, commandType: System.Data.CommandType.StoredProcedure);
                        }
                    }
                }
            }

            // Xử lý kết quả trả về
            if (affactedRecordCount > 0)
            {
                return employeeID;
            }
            else
            {
                return Guid.Empty;
            }
        }

        /// <summary>
        /// Xóa một bản ghi thông qua ID
        /// </summary>
        /// <param name="recordID">ID của bản ghi muốn xóa</param>
        /// <returns>ID của bản ghi vừa xóa</returns>
        /// Created By: KhaiND (19/11/2022)
        public Guid Delete(Guid employeeID)
        {
            // Chuẩn bị tham số đầu vào
            var parameters = new DynamicParameters();
            parameters.Add("EmployeeID", employeeID);

            // Chuẩn bị câu lệnh SQL
            string storedProcedureName = "Proc_Employee_DeleteByID";
            string storeProcedureDeleteRoom = "Proc_EmployeeWithRoom_DeleteByEmployeeID";
            string storeProcedureDeleteSubject = "Proc_EmployeeWithSubject_DeleteByEmployeeID";

            // Khởi tạo kết nối đến Database MySQL
            int affactedRecordCount = 0;
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                mySqlConnection.Open();
                // Sử dụng Transaction
                using (var transaction = mySqlConnection.BeginTransaction())
                {
                    try
                    {
                        // Thực hiện gọi truy vấn vào Database
                        affactedRecordCount = mySqlConnection.Execute(storeProcedureDeleteSubject, parameters, transaction, commandType: System.Data.CommandType.StoredProcedure);
                        affactedRecordCount += mySqlConnection.Execute(storeProcedureDeleteRoom, parameters, transaction, commandType: System.Data.CommandType.StoredProcedure);
                        affactedRecordCount += mySqlConnection.Execute(storedProcedureName, parameters, transaction, commandType: System.Data.CommandType.StoredProcedure);

                        transaction.Commit();
                        mySqlConnection.Close();

                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        mySqlConnection.Close();
                        Console.WriteLine(ex.Message);
                        affactedRecordCount = 0;
                    }
                }

                // Xử lý kết quả trả về
                if (affactedRecordCount > 0)
                {
                    return employeeID;
                }
                else
                {
                    return Guid.Empty;
                }
            }
        }

        /// <summary>
        /// Xóa đồng thời nhiều Cán bộ, giáo viên thông qua danh sách ID
        /// </summary>
        /// <param name="employeeIDs">Danh sách ID của các Cán bộ, giáo viên muốn xóa</param>
        /// <returns>Danh sách ID của các Các cán bộ, giáo viên đã xóa</returns>
        /// Created By: KhaiND (22/11/2022)
        public List<Guid> DeleteMany(List<Guid> employeeIDs)
        {
            // Kiểm tra đầu vào
            var deletedIDs = new List<Guid>();
            if (employeeIDs == null || employeeIDs.Count <= 0)
            {
                return deletedIDs;
            }

            // Chuẩn bị tham số đầu vào
            var parameters = new DynamicParameters();
            string listEmployeeIDsString = $"('{String.Join("','", employeeIDs)}')";
            parameters.Add("ListEmployeeIDsString", listEmployeeIDsString);

            // Chuẩn bị câu lệnh truy vấn
            string storedProcedureName = "Proc_Employee_DeleteManyByIDs";
            string storeProcedureDeleteRoom = "Proc_EmployeeWithRoom_DeleteByEmployeeID";
            string storeProcedureDeleteSubject = "Proc_EmployeeWithSubject_DeleteByEmployeeID";

            // Khởi tọa kết nối đến Database MySQL
            var affactedRecordCount = 0;
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                mySqlConnection.Open();
                // Sử dụng Transaction
                using (var transaction = mySqlConnection.BeginTransaction())
                {
                    try
                    {
                        // Truy vấn xóa khóa ngoại
                        foreach (var employeeID in employeeIDs)
                        {
                            var deleteFKParameters = new DynamicParameters();
                            deleteFKParameters.Add("EmployeeID", employeeID);

                            affactedRecordCount = mySqlConnection.Execute(storeProcedureDeleteSubject, deleteFKParameters, transaction, commandType: System.Data.CommandType.StoredProcedure);
                            affactedRecordCount += mySqlConnection.Execute(storeProcedureDeleteRoom, deleteFKParameters, transaction, commandType: System.Data.CommandType.StoredProcedure);
                        }
                        // Thực hiện gọi truy vấn xóa nhiều vào Database
                        affactedRecordCount += mySqlConnection.Execute(storedProcedureName, parameters, transaction, commandType: System.Data.CommandType.StoredProcedure);

                        transaction.Commit();
                        mySqlConnection.Close();

                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        mySqlConnection.Close();
                        Console.WriteLine(ex.Message);
                        affactedRecordCount = 0;
                    }
                }
            }

            // Xử lý kết quả trả về
            if (affactedRecordCount > 0)
            {
                deletedIDs = employeeIDs;
            }
            return deletedIDs;
        }

        /// <summary>
        /// Kiểm tra số hiệu cán bộ có trùng hay không
        /// </summary>
        /// <param name="employeeCode">Số hiệu cán bộ muốn kiểm tra</param>
        /// <returns>true nếu có trùng và false nếu SHCB chưa tồn tại</returns>
        /// Created By: KhaiND 24/11/2022
        public bool checkDuplicateCode(string employeeCode)
        {
            // Chuẩn bị tham số đầu vào
            var parameters = new DynamicParameters();
            parameters.Add("EmployeeCode", employeeCode);

            // Chuẩn bị câu lệnh SQL
            string storedProcedureName = "Proc_Employee_CheckExistCode";
            // Khởi tạo kết nối đến Database MySQL
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                // Thực hiện gọi truy vấn vào Database
                return mySqlConnection.QueryFirstOrDefault<bool>(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);
            }
        }
    }
}
