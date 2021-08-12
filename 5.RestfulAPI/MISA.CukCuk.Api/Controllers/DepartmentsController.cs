using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.CukCuk.Api.Controllers.CommonFunction;
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
  /// </summary> /// <summary>
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
    public class DepartmentsController : ControllerBase
    {
        [HttpGet]
        public IActionResult getAllDepartment()
        {
            //Truy cập data base
            //1. Khai báo thông tin kết nối
            var localConnection = DBInfo.localConnection;

            //2.Khởi tạo đối tượng kết nối với csdl
            IDbConnection dbConnection = new MySqlConnection(localConnection);

            //3.Lấy dữ liệu 
            string sqlCommand = "SELECT * FROM Department";
            var customers = dbConnection.Query<object>(sqlCommand);
            var response = StatusCode(200, customers);
            return response;
        }

        [HttpGet("{departmentId}")]
        public IActionResult getDepartmentById(Guid departmentId)
        {
            //Khai báo thông tin kết nối
            var localConnection = DBInfo.localConnection;

            //Khỏi tạo đối tượng kết nối với csdl
            IDbConnection dbConnection = new MySqlConnection(localConnection);

            //Tạo query lấy dữ liẹu
            string sqlCommand = $"SELECT * FROM Department WHERE departmentId= '{departmentId}'";
            DynamicParameters dynamicParams = new DynamicParameters();
            dynamicParams.Add("@departmentId", departmentId);

            //Thực hiện query lấy dữ liệu
            Department resDepartment = dbConnection.QueryFirstOrDefault<Department>(sqlCommand);
            var response = StatusCode(200, resDepartment);
            return response;
        }

        [HttpPost]
        public IActionResult InsertDepartment(Department department)
        {
            //Tạm thay đổi dữ liệu
            department.DepartmentId = Guid.NewGuid();

            //Truy cập data base
            //1. Khai báo thông tin kết nối
            var localConnection = DBInfo.localConnection;

            //2.Khởi tạo đối tượng kết nối với csdl
            IDbConnection dbConnection = new MySqlConnection(localConnection);
            var dynamicParams = new DynamicParameters();

            //3. Tạo câu truy vấn thêm mới dữ liệu
            string columnNames = string.Empty;
            string columnsValues = string.Empty;

            var properties = department.GetType().GetProperties();
            foreach (var prop in properties)
            {
                //Lấy tên prop
                var propName = prop.Name;

                //Lấy giá trị của prop
                var propValue = prop.GetValue(department);

                dynamicParams.Add($"@{propName}", propValue);
                columnNames += $"{propName},";
                columnsValues += $"@{propName},";

            }
            columnNames = columnNames.Remove(columnNames.Length - 1, 1);
            columnsValues = columnsValues.Remove(columnsValues.Length - 1, 1);

            //4. Thực thi SQL để thêm mới dữ liệu
            String sqlCommand = $"INSERT INTO Department ({columnNames}) VALUES ({columnsValues})";
            var rowAffected = dbConnection.Execute(sqlCommand, param: dynamicParams);
            var response = StatusCode(200, rowAffected);
            return response;
        }


        [HttpPut("{departmentId}")]
        public IActionResult UpdateDepartment(Guid departmentId, Department department)
        {
            //Truy cập data base
            //1. Khai báo thông tin kết nối
            var localConnection = DBInfo.localConnection;

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
                //if (prop.PropertyType.ToString().Equals("DateTime"))
                //{
                //    propValue = CommonFn.toCsharpDate(propValue.ToString());
                //}

                dynamicParams.Add($"@{propName}", propValue);
                updateColumnString += $"{propName} = '@{propName}' , ";
            }
            updateColumnString = updateColumnString.Remove(updateColumnString.Length - 2, 2);
            dynamicParams.Add($"@departmentId", departmentId);

            //4. Thực thi sql để update dữ liệu
            string sqlCommand = $"UPDATE Department SET {updateColumnString} WHERE DepartmentId= @departmentId";
            var rowaffected = dbConnection.Execute(sqlCommand, param: dynamicParams);
            var response = StatusCode(200, rowaffected);
            return response;
        }
    }
}
