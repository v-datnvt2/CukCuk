using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.CukCuk.Api.Model
{
    public class CommonInfo
    {
        public DateTime CreatedDate { get; set; }
        public String CreatedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public String ModifiedBy { get; set; }
    }
}
