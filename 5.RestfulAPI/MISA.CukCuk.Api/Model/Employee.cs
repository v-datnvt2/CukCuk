using MISA.CukCuk.Api.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.CukCuk.Api.Model
{
    public class Employee:Person
    {
        #region Property

        /// <summary>
        /// ID nhân viên
        /// </summary>
        public Guid EmployeeId { get; set; }

        /// <summary>
        /// Mã nhân viên
        /// </summary>
        public string EmployeeCode { get; set; }

        /// <summary>
        /// Số cmnd /cccd
        /// </summary>
        public string IdentityNumber { get; set; }

        /// <summary>
        /// Ngày cấp cmt/ cccd
        /// </summary>
        public DateTime? IdentityDate { get; set; }

        /// <summary>
        /// Nơi cấp cmt/ cccd
        /// </summary>
        public string IdentityPlace { get; set; }

        /// <summary>
        /// Ngày bắt đầu làm việc
        /// </summary>
        public DateTime? JoinDate { get; set; }

        /// <summary>
        /// Tình trạng hôn nhân
        /// </summary>
        public MartialStatus? MartialStatus { get; set; }

        /// <summary>
        /// Trình độ học vấn
        /// </summary>
        public EducationalBackground? EducationalBackground { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Guid? QualificationId { get; set; }

        /// <summary>
        /// ID phòng ban
        /// </summary>
        public Guid? DepartmentId { get; set; }

        /// <summary>
        /// ID chức vụ
        /// </summary>
        public Guid? PositionId { get; set; }

        /// <summary>
        /// Tình trạng công việc
        /// </summary>
        public WorkStatus? WorkStatus { get; set; }


        /// <summary>
        /// Mã số thuế cá nhân
        /// </summary>
        public string PersonalTaxCode { get; set; }

        /// <summary>
        /// Lương cơ bản
        /// </summary>
        public double Salary { get; set; }

        /// <summary>
        /// Ngày tạo
        /// </summary>
        //public DateTime? CreatedDate { get; set; }

        ///// <summary>
        ///// Người tạo
        ///// </summary>
        //public string CreatedBy { get; set; }

        ///// <summary>
        ///// Ngày chỉnh sửa
        ///// </summary>
        //public DateTime? ModifiedDate { get; set; }

        ///// <summary>
        ///// Người chỉnh sửa
        ///// </summary>
        //public string ModifiedBy { get; set; }

        #endregion
    }
}
