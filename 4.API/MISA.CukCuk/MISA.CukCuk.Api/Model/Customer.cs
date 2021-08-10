using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasicCs
{
    public class Customer
    {

        #region Props

        public Guid CustomerId { get; set; }
        public string FullName { get; set; }

        public int? Age { get; set; }

        public string Address { get; set; }
        #endregion
    }
}
