using BasicCs;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.CukCuk.Api.Model;
using MISA.CukCuk.Api.Properties;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.CukCuk.Api.Controllers
{ /// <summary>
  /// 
  /// 
  /// 
  /// 
  /// 
  /// Anh kiểm tra EmployeeController
  /// Mục này em chưa làm RESTFUL
  /// 
  /// 
  /// 
  /// 
  /// 
  /// 
  /// 
  /// 
  /// 
  /// 
  /// 
  /// </summary>
  ///  /// <summary>
  /// 
  /// 
  /// 
  /// 
  /// 
  /// Anh kiểm tra EmployeeController
  /// Mục này em chưa làm RESTFUL
  /// 
  /// 
  /// 
  /// 
  /// 
  /// 
  /// 
  /// 
  /// 
  /// 
  /// 
  /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        [HttpGet]
        public IActionResult getAllCustomer()
        {
            //Truy cập data base
            //1. Khai báo thông tin kết nối
            var localConnection = DBInfo.localConnection;

            //2.Khởi tạo đối tượng kết nối với csdl
            IDbConnection dbConnection = new MySqlConnection(localConnection);

            //3.Lấy dữ liệu 
            String sqlCommand = "SELECT * FROM Customer";
            var customers = dbConnection.Query<object>(sqlCommand);
            var response = StatusCode(200, customers);
            return response;
        }

        [HttpGet("{customerId}")]
        public IActionResult getCustomerById(Guid customerId)
        {
            //Khai báo thông tin kết nối
            var localConnection = DBInfo.localConnection;

            //Khỏi tạo đối tượng kết nối với csdl
            IDbConnection dbConnection = new MySqlConnection(localConnection);

            //Tạo query lấy dữ liẹu
            string sqlCommand = $"SELECT * FROM Customer WHERE customerId= '{customerId}'";
            DynamicParameters dynamicParams = new DynamicParameters();
            dynamicParams.Add("@customerId", customerId);

            //Thực hiện query lấy dữ liệu
            Customer resCustomer = dbConnection.QueryFirstOrDefault<Customer>(sqlCommand);
            var response = StatusCode(200, resCustomer);
            return response;
        }

        [HttpGet("NewCustomerCode")]
        public IActionResult getNewCustomerCode()
        {
            //Khai báo thông tin kết nối
            var localConnection = DBInfo.localConnection;

            //Khỏi tạo đối tượng kết nối với csdl
            IDbConnection dbConnection = new MySqlConnection(localConnection);

            //Thực hiện query lấy mảng mã nhân viên  từ csdl
            string sqlCommand = "SELECT CustomerCode FROM Customer ORDER BY CustomerCode DESC LIMIT 1";
            var customerCode = dbConnection.QueryFirstOrDefault<string>(sqlCommand);

            // Xử lí sinh mã  mới
            int currentMax = 0;

            try
            {
                int codeValue = int.Parse(customerCode.ToString().Split("-")[1]);
                if (currentMax < codeValue)
                {
                    currentMax = codeValue;
                }
            }
            catch (Exception)
            {
                var errorResponse = StatusCode(500, 1);
                return errorResponse;
            }


            string newCustomerCode = "KH-" + (currentMax + 1);
            var response = StatusCode(200, newCustomerCode);
            return response;
        }

        [HttpPost]
        public IActionResult InsertCustomer( Customer customer)
        {
            //Tạm thay đổi dữ liệu
            customer.CustomerId = Guid.NewGuid();

            //Truy cập data base
            //1. Khai báo thông tin kết nối
            var localConnection = DBInfo.localConnection;

            //2.Khởi tạo đối tượng kết nối với csdl
            IDbConnection dbConnection = new MySqlConnection(localConnection);
            var dynamicParams = new DynamicParameters();

            //3. Tạo câu truy vấn thêm mới dữ liệu
            string columnNames = string.Empty;
            string columnsValues = string.Empty;

            var properties = customer.GetType().GetProperties();
            foreach (var prop in properties)
            {
                //Lấy tên prop
                var propName = prop.Name;

                //Lấy giá trị của prop
                var propValue = prop.GetValue(customer);

                dynamicParams.Add($"@{propName}", propValue);
                columnNames += $"{propName},";
                columnsValues += $"@{propName},";

            }
            columnNames = columnNames.Remove(columnNames.Length - 1, 1);
            columnsValues = columnsValues.Remove(columnsValues.Length - 1, 1);

            //4. Thực thi SQL để thêm mới dữ liệu
            String sqlCommand = $"INSERT INTO Customer ({columnNames}) VALUES ({columnsValues})";
            var rowAffected = dbConnection.Execute(sqlCommand, param: dynamicParams);
            var response = StatusCode(200, rowAffected);
            return response;
        }

        [HttpPut("{customerId}")]
        public IActionResult UpdateCustomer(Guid customerId, Customer department)
        {
            //Truy cập data base
            //1. Khai báo thông tin kết nối
            var localConnection = DBInfo.connectionString;

            //2.Khởi tạo đối tượng kết nối với csdl
            IDbConnection dbConnection = new MySqlConnection(localConnection);
            var dynamicParams = new DynamicParameters();

            //3. Tạo câu truy vấn thêm mới dữ liệu
            string updateColumnString = string.Empty;

            var properties = department.GetType().GetProperties();
            foreach (var prop in properties)
            {
                //Lấy tên prop
                var propName = prop.Name;

                //Lấy giá trị của prop
                var propValue = prop.GetValue(department);

                dynamicParams.Add($"@{propName}", propValue);
                updateColumnString += $"{propName} = '@{propName}' , ";
            }
            updateColumnString = updateColumnString.Remove(updateColumnString.Length - 2, 2);
            dynamicParams.Add($"@customerId", customerId);

            //4. Thực thi sql để update dữ liệu
            String sqlCommand = $"UPDATE Customer SET {updateColumnString} WHERE customerId= @customerId";
            var rowAffected = dbConnection.Execute(sqlCommand, param: dynamicParams);
            var response = StatusCode(200, rowAffected);
            return response;
        }

    }
}
