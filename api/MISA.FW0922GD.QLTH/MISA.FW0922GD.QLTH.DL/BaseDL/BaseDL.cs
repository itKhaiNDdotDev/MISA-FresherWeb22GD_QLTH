using Dapper;
using MISA.FW0922GD.QLTH.Common.Constants;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.FW0922GD.QLTH.DL.BaseDL
{
    /// <summary>
    /// Class impliment các method dùng chung cho nhiều DL
    /// </summary>
    /// <typeparam name="T">Class của đối tượng dữ liệu tương ứng</typeparam>
    /// Created By: KhaiND (16/11/2022)
    public class BaseDL<T> : IBaseDL<T>
    {

        /// <summary>
        /// Lấy danh sách tất cả bản ghi
        /// </summary>
        /// <returns>Danh sách tất cả bản ghi</returns>
        /// Created By: KhaiND (16/11/2022)
        public IEnumerable<T> GetAll()
        {
            // Chuẩn bị câu lệnh SQL
            string storedProcedureName = String.Format(Procedure.GET_ALL, typeof(T).Name);

            // Khởi tạo kết nối đến Database MySQL
            var records = new List<T>();
            using (var mySQLConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                // Thực hiện gọi truy vấn vào Database
                records = (List<T>) mySQLConnection.Query<T>(storedProcedureName, commandType: System.Data.CommandType.StoredProcedure); 
            }

            // Xử lý kết quả trả về
            return records;
        }

        /// <summary>
        /// Lấy thông tin một bản ghi theo ID
        /// </summary>
        /// <param name="recordID">ID của bản ghi muốn lấy</param>
        /// <returns>Thông tin 1 bản ghi theo ID</returns>
        /// Created By: KhaiND (16/11/2022)
        public T GetByID(Guid recordID)
        {
            // Khởi tạo kết nối đến Database MySQL
            var mySQLConnection = new MySqlConnection(DatabaseContext.ConnectionString);

            // Chuẩn bị câu lệnh SQL
            string storedProcedureName = $"Proc_{typeof(T).Name}_GetByID";

            // Chuẩn bị tham số đầu vào
            var parameters = new DynamicParameters();
            parameters.Add($"{typeof(T).Name}ID", recordID);

            // Thực hiện gọi truy vấn vào Database
            var record = mySQLConnection.QueryFirstOrDefault<T>(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);

            // Xử lý kết quả trả về
            if(record != null)
                return record;
            return null;
        }

    }
}
