using MISA.CukCuk.Api.Enum;
using MISA.CukCuk.Api.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasicCs
{
    public class Customer:Person
    {
        #region Props
        /// <summary>
        /// ID khách hàng
        /// </summary>
        public Guid CustomerId { get; set; }

        /// <summary>
        /// Mã khách hàng
        /// </summary>
        public string CustomerCode { get; set; }

        /// <summary>
        /// ID nhóm khách hàng
        /// </summary>
        public Guid? CustomerGroupId { get; set; }

        /// <summary>
        /// Số tiền nợ
        /// </summary>
        public float DebitAmount { get; set; }

        /// <summary>
        /// Mã thẻ thành viên
        /// </summary>
        public string MemberCardCode { get; set; }

        /// <summary>
        /// Tên công ty
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// Mã số thuế công ty
        /// </summary>
        public string CompanyTaxCode { get; set; }

        /// <summary>
        /// Trạng thái ngừng theo dõi
        /// </summary>
        public string IsStopFollow { get; set; }

        #endregion
    }
}
