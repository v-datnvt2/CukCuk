using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.CukCuk.Api.Model
{
    public class Department : CommonInfo
    {
        /// <summary>
        /// ID phòng ban
        /// </summary>
        /// CreatedBy NHHung (06/08)
        public Guid DepartmentId { get; set; }

        /// <summary>
        /// Mã phòng ban
        /// </summary>
        /// CreatedBy NHHung (06/08)
        public String DepartmentCode { get; set; }

        /// <summary>
        /// Tên phòng ban
        /// </summary>
        /// CreatedBy NHHung (06/08)
        public String DepartmentName { get; set; }

        /// <summary>
        /// Mô tả
        /// </summary>
        /// CreatedBy NHHung (06/08)
        public string Description { get; set; }


    }
}
