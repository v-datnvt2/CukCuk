using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.CukCuk.Api.Properties
{
    public static class DBInfo
    {
        //Remote host
        public static string connectionString = "Host= 47.241.69.179;" +
                                                "Database= MISA.CukCuk_Demo_NVMANH;" +
                                                "User Id= dev;" +
                                                "Password= 12345678;";

        //Local host
        public static string localConnection  = "Host= localhost ;" +
                                               "Database=cukcuk_demo_v1;" +
                                               "User Id = dev;" +
                                               "Password=12345678;";


    }
}
