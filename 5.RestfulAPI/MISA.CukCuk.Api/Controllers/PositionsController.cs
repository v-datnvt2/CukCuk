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
{
    [Route("api/[controller]")]
    [ApiController]
    public class PositionsController : ControllerBase
    {
        [HttpGet]
        public IActionResult getAllPosition()
        {
            //Truy cập data base
            //1. Khai báo thông tin kết nối
            var localConnection = DBInfo.localConnection;

            //2.Khởi tạo đối tượng kết nối với csdl
            IDbConnection dbConnection = new MySqlConnection(localConnection);

            //3.Lấy dữ liệu 
            String sqlCommand = "SELECT * FROM Position";
            var Positions = dbConnection.Query<object>(sqlCommand);
            var response = StatusCode(200, Positions);
            return response;
        }

        [HttpGet("{PositionId}")]
        public IActionResult getPositionById(Guid PositionId)
        {
            //Khai báo thông tin kết nối
            var localConnection = DBInfo.localConnection;

            //Khỏi tạo đối tượng kết nối với csdl
            IDbConnection dbConnection = new MySqlConnection(localConnection);

            //Tạo query lấy dữ liẹu
            string sqlCommand = $"SELECT * FROM Position WHERE PositionId= '{PositionId}'";
            DynamicParameters dynamicParams = new DynamicParameters();
            dynamicParams.Add("@PositionId", PositionId);

            //Thực hiện query lấy dữ liệu
            Position resPosition = dbConnection.QueryFirstOrDefault<Position>(sqlCommand);
            var response = StatusCode(200, resPosition);
            return response;
        }

        [HttpGet("NewPositionCode")]
        public IActionResult getNewPositionCode()
        {
            //Khai báo thông tin kết nối
            var localConnection = DBInfo.localConnection;

            //Khỏi tạo đối tượng kết nối với csdl
            IDbConnection dbConnection = new MySqlConnection(localConnection);

            //Thực hiện query lấy mảng mã nhân viên  từ csdl
            string sqlCommand = "SELECT PositionCode FROM Position ORDER BY PositionCode DESC LIMIT 1";
            var PositionCode = dbConnection.QueryFirstOrDefault<string>(sqlCommand);


            // Xử lí sinh mã  mới NV-xxx
            int currentMax = 0;

            try
            {
                int codeValue = int.Parse(PositionCode.Split("-")[1]);
                currentMax = codeValue;
            }
            catch (Exception)
            {
                currentMax = 0;
            }

            string newPositionCode = "VT-" + (currentMax + 1);
            var response = StatusCode(200, newPositionCode);
            return response;
        }

        [HttpPost]
        public IActionResult InsertPosition(Position Position)
        {
            //Tạm thay đổi dữ liệu
            Position.PositionId = Guid.NewGuid();

            //Truy cập data base
            //1. Khai báo thông tin kết nối
            var localConnection = DBInfo.localConnection;

            //2.Khởi tạo đối tượng kết nối với csdl
            IDbConnection dbConnection = new MySqlConnection(localConnection);
            var dynamicParams = new DynamicParameters();

            //3. Tạo câu truy vấn thêm mới dữ liệu
            string columnNames = string.Empty;
            string columnsValues = string.Empty;

            var properties = Position.GetType().GetProperties();
            foreach (var prop in properties)
            {
                //Lấy tên prop
                var propName = prop.Name;

                //Lấy giá trị của prop
                var propValue = prop.GetValue(Position);

                dynamicParams.Add($"@{propName}", propValue);
                columnNames += $"{propName},";
                columnsValues += $"@{propName},";

            }
            columnNames = columnNames.Remove(columnNames.Length - 1, 1);
            columnsValues = columnsValues.Remove(columnsValues.Length - 1, 1);

            //4. Thực thi SQL để thêm mới dữ liệu
            string sqlCommand = $"INSERT INTO Position ({columnNames}) VALUES ({columnsValues})";
            var rowAffected = dbConnection.Execute(sqlCommand, param: dynamicParams);
            var response = StatusCode(200, rowAffected);
            return response;
        }

        [HttpPut("{PositionId}")]
        public IActionResult UpdatePosition(Guid PositionId, Position department)
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

                dynamicParams.Add($"@{propName}", propValue);
                updateColumnString += $"{propName} = '@{propName}' , ";
            }
            updateColumnString = updateColumnString.Remove(updateColumnString.Length - 2, 2);
            dynamicParams.Add($"@PositionId", PositionId);

            //4. Thực thi sql để update dữ liệu
            String sqlCommand = $"UPDATE Position SET {updateColumnString} WHERE PositionId= @PositionId";
            var rowAffected = dbConnection.Execute(sqlCommand, param: dynamicParams);
            var response = StatusCode(200, rowAffected);
            return response;
        }

    }
}
