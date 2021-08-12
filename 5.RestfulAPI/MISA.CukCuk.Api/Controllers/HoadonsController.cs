using BasicCs;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using MISA.CukCuk.Api.Properties;
using MySqlConnector;
using System;
using System.Data;

namespace MISA.CukCuk.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HoadonsController : ControllerBase
    {
        [HttpGet]
        public IActionResult getAllCustomer()
        {
            //Truy cập data base
            //1. Khai báo thông tin kết nối
            var connectionString = DBInfo.localConnection;

            //2.Khởi tạo đối tượng kết nối với csdl
            IDbConnection dbConnection = new MySqlConnection(connectionString);

            //3.Lấy dữ liệu 
            String sqlCommand = "SELECT * FROM Employee";
            var customers = dbConnection.Query<object>(sqlCommand);
            var response = StatusCode(200, customers);
            return response;
        }

        [HttpPost]
        public IActionResult InsertCustomer(Customer customer)
        {
            //Truy cập data base
            //1. Khai báo thông tin kết nối
            var connectionString = DBInfo.connectionString;

            //2.Khởi tạo đối tượng kết nối với csdl
            IDbConnection dbConnection = new MySqlConnection(connectionString);

            //3. Tạo câu truy vấn thêm mới dữ liệu
            string columnNames = string.Empty;
            string columnsValues = string.Empty;

            var properties = customer.GetType().GetProperties();

            String sqlCommand = "INSERT INTO Department ({}) VALUES ({})";
            var customers = dbConnection.Query<object>(sqlCommand);
            var response = StatusCode(200, customers);
            return response;
        }

        [HttpGet("test/{name}/{age}")]
        public String test1(string name, int? age)
        {
            return ($"Tên {name} Tuổi: {age}");
        }
    }
}
