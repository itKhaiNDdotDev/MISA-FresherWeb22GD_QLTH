using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.FW0922GD.QLTH.Common.Enums
{
    /// <summary>
    /// Trạng thái có trình độ nghiệp vụ quản lý thiết bị hay không
    /// </summary>
    /// Created By: KhaiND (17/11/2022)
    public enum IsDeviceManager
    {
        /// <summary>
        /// Không có trình độ nghiệp vụ quản lý thiết bị
        /// </summary>
        NotDeviceManager = 0,

        /// <summary>
        /// Có trình độ nghiệp vụ quản lý thiết bị
        /// </summary>
        DeviceManager = 1
    }
}
