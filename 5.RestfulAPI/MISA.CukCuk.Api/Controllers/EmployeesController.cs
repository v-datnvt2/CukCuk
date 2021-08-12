using BasicCs;
using Dapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.CukCuk.Api.Controllers.CommonFunction;
using MISA.CukCuk.Api.Model;
using MISA.CukCuk.Api.Properties;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.CukCuk.Api.Controllers
{
   
    [Route("api/v1/[controller]")]
    [ApiController]
    [EnableCors]
    public class EmployeesController : ControllerBase
    {
        #region GET methods
        /// <summary>
        /// Lấy tất cả bản ghi trong bảng nhân viên.
        /// </summary>
        /// <returns>Danh sách nhân viên</returns>
        [HttpGet]
        public IActionResult getEmployees()
        {
            try
            {
                //Truy cập data base
                //Khai báo thông tin kết nối
                var localConnection = DBInfo.localConnection;

                //Khởi tạo đối tượng kết nối với csdl
                IDbConnection dbConnection = new MySqlConnection(localConnection);

                //Lấy dữ liệu 
                string sqlCommand = "SELECT * FROM Employee";
                var employees = dbConnection.Query<object>(sqlCommand);

                // Kiểm tra dữ liệu trả về
                if (employees.Count() > 0)
                {
                    return StatusCode(200, employees);
                }
                else
                {
                    return StatusCode(204);
                }
            }
            catch (Exception ex)
            {
                //Lỗi kết nối đến csdl
                var errorMsg = new
                {
                    devMsg = ex.Message,
                    userMsg = Properties.Resource.Exception_Msg_DBConnection,
                    errorCode = "Exception_Msg_DBConnection"
                };
                var response = StatusCode(500, errorMsg);
                return response;
            }
        }

        /// <summary>
        /// Lấy thông tin của 1 nhân viên theo ID
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns>Thông tin nhân viên</returns>
        [HttpGet("{employeeId}")]
        public IActionResult getEmployeeById(Guid employeeId)
        {
            try
            {
                //Khai báo thông tin kết nối
                var localConnection = DBInfo.localConnection;

                //Khỏi tạo đối tượng kết nối với csdl
                IDbConnection dbConnection = new MySqlConnection(localConnection);

                //Tạo query lấy dữ liẹu
                string sqlCommand = $"SELECT * FROM Employee WHERE employeeId= '{employeeId}'";
                DynamicParameters dynamicParams = new DynamicParameters();
                dynamicParams.Add("@employeeId", employeeId);

                //Thực hiện query lấy dữ liệu
                Employee resEmployee = dbConnection.QueryFirstOrDefault<Employee>(sqlCommand);

                //Kiểm tra dữ liệu trả về
                if (resEmployee != null)
                {
                    return StatusCode(200, resEmployee);
                }
                else
                {
                    return StatusCode(204);
                }

            }
            catch (Exception ex)
            {
                //Lỗi kết nối đến csdl
                var errorMsg = new
                {
                    devMsg = ex.Message,
                    userMsg = Properties.Resource.Exception_Msg_DBConnection,
                    errorCode = "Exception_Msg_DBConnection"
                };
                var response = StatusCode(500, errorMsg);
                return response;
            }
        }

        /// <summary>
        /// Sinh mã nhân viên mới theo quy tắc: mã lớn nhất trong CSDL cộng 1
        /// </summary>
        /// <returns>Mã nhân viên mới</returns>
        [HttpGet("NewEmployeeCode")]
        public IActionResult getNewEmployeeCode()
        {
            try
            {
                //Khai báo thông tin kết nối
                var localConnection = DBInfo.localConnection;

                //Khỏi tạo đối tượng kết nối với csdl
                IDbConnection dbConnection = new MySqlConnection(localConnection);

                //Thực hiện query lấy mảng mã nhân viên  từ csdl
                string sqlCommand = "SELECT EmployeeCode FROM Employee ORDER BY EmployeeCode DESC LIMIT 1";
                var employeeCode = dbConnection.QueryFirstOrDefault<string>(sqlCommand);

                //Kiểm tra dữ liệu trả về
                if (employeeCode != null)
                {
                    // Xử lí sinh mã  mới NV-xxx
                    int currentMaxCode;
                    try
                    {
                        //Lấy giá trị số lớn nhất trong mã nhân viên hiện tại
                        int codeValue = int.Parse(employeeCode.Split("-")[1]);
                        currentMaxCode = codeValue;
                    }
                    catch (Exception ex)
                    {
                        //Lỗi dữ liệu trả về không hợp lệ không ở dang NV-xxx
                        var errorMsg = new
                        {
                            devMsg = ex.Message,
                            userMsg = Properties.Resource.Exception_Msg_InvalidDBData,
                            errorCode = "Exception_Msg_InvalidDBData"
                        };
                        return StatusCode(500, errorMsg); ;
                    }

                    //Tạo xâu mã mới và trả về
                    string newEmployeeCode = "NV-" + (currentMaxCode + 1);
                    var response = StatusCode(200, newEmployeeCode);
                    return response;
                }
                else
                {
                    return StatusCode(204);
                }
            }
            catch (Exception ex)
            {
                //Lỗi kết nối đến csdl
                var errorMsg = new
                {
                    devMsg = ex.Message,
                    userMsg = Properties.Resource.Exception_Msg_DBConnection,
                    errorCode = "Exception_Msg_DBConnection"
                };
                var response = StatusCode(500, errorMsg);
                return response;
            }
        }

        /// <summary>
        /// Phân trang  và lọc dữ liệu theo các tham số truyền vào
        /// </summary>
        /// <param name="pageNumber">Trang muốn lấy dữ liệu</param>
        /// <param name="pageSize">Số bản ghi/ trang cần lấy</param>
        /// <param name="filter">Xâu chứa tên/ mã /sdt của nhân viên cần lọc</param>
        /// <param name="departmentId">ID phòng ban cần lọc</param>
        /// <param name="positionId">ID chức vụ cần lọc</param>
        /// <returns>Danh sách nhân viên thỏa mãn yêu cầu phân trang và lọc</returns>
        [HttpGet("filter")]
        public IActionResult filterEmployee(string pageNumber, string pageSize, string filter, string departmentId, string positionId)
        {
            //Khai báo thông tin kết nối
            var localConnection = DBInfo.localConnection;


            //Khỏi tạo đối tượng kết nối với csdl
            IDbConnection dbConnection = new MySqlConnection(localConnection);


            int parsedPageNumber;
            int parsedpageSize;
            Guid parsedDepartmentId;
            Guid parsedpositionId;
            //Kiểm tra tham số đầu vào 
            // pageNumber: int,  pageSize:int , departmentId: Guid ,  positionId: Guid
            try
            {
                parsedPageNumber = int.Parse(pageNumber);
                parsedpageSize = int.Parse(pageSize);
                parsedDepartmentId = Guid.Parse(departmentId);
                parsedpositionId = Guid.Parse(positionId);
            }
            catch (Exception ex)
            {
                //Param không đúng định dạng/ kiểu dữ liệu
                var errorMsg = new
                {
                    devMsg = ex.Message,
                    userMsg = Properties.Resource.Exception_Msg_Common,
                    errorCode = "Exception_Msg_InvalidParameter"
                };
                return StatusCode(400, errorMsg); ;
            }

            //Tìm index của bản ghi đầu tiên của trang trong DB
            int startIndex = (parsedPageNumber - 1) * parsedpageSize;
            startIndex = startIndex < 0 ? 0 : startIndex;

            //Tạo câu truy vấn cho các trường filter
            DynamicParameters dynamicParams = new DynamicParameters();
            string orCommandString = string.Empty;
            string andCommandString = string.Empty;

            //Thay đổi query nếu có lọc theo từ khóa
            if (filter != "" && filter != null)
            {
                orCommandString = " AND (EmployeeCode = @filter OR FullName = @filter OR PhoneNumber = @filter)";
                dynamicParams.Add("@filter", filter);
            }

            /// Thay đổi query nếu có lọc theo phòng ban
            if (departmentId.ToString() != "")
            {
                andCommandString += " AND DepartmentId= @DepartmentId";
                dynamicParams.Add("@DepartmentId", departmentId);
            }

            //Thay đổi query nếu  có lọc theo chức vụ
            if (positionId.ToString() != "")
            {
                andCommandString += " AND (PositionId= @PositionId)";
                dynamicParams.Add("@PositionId", positionId);
            }

            //Tạo query lấy dữ liệu
            string sqlCommand = $"SELECT * FROM Employee WHERE 1=1 " +
                $" {orCommandString} {andCommandString} LIMIT @startIndex,@pageSize";

            dynamicParams.Add("@startIndex", startIndex);
            dynamicParams.Add("@pageSize", pageSize);
            //Thực hiện query lấy dữ liệu
            var filteredEmployees = dbConnection.Query<Employee>(sqlCommand, param: dynamicParams);

            //Kiểm tra dữ liệu trả về
            if (filteredEmployees.Count() > 0)
            {
                return StatusCode(200, filteredEmployees);
            }
            else
            {
                return StatusCode(204);
            }
        }
        #endregion

        #region POST methods
        [HttpPost]
        public IActionResult InsertEmployee(Employee employee)
        {
            var validFields = true;
            //Kiểm tra mã nhân viên bị trống
            if (employee.EmployeeCode == "" || employee.EmployeeCode == null)
            {
                validFields = false;
            }

            //Kiếm tra định dạng email
            if (new EmailAddressAttribute().IsValid(employee.Email) == false)
            {
                validFields = false;
            }

            if (validFields == false)
            {
                //Param không đúng định dạng/ kiểu dữ liệu
                var errorMsg = new
                {
                    devMsg = "Exception_Msg_InvalidInputData",
                    userMsg = Properties.Resource.Exception_Msg_InvalidInputData,
                    errorCode = "Exception_Msg_InvalidInputData"
                };
                return StatusCode(400, errorMsg); ;
            }

            try
            {
                //Sinh ID mới cho nhân viên
                employee.EmployeeId = Guid.NewGuid();

                //Truy cập data base
                //1. Khai báo thông tin kết nối
                var localConnection = DBInfo.localConnection;

                //2.Khởi tạo đối tượng kết nối với csdl
                IDbConnection dbConnection = new MySqlConnection(localConnection);
                var dynamicParams = new DynamicParameters();

                //3. Tạo câu truy vấn thêm mới dữ liệu
                string columnNames = string.Empty;
                string columnsValues = string.Empty;

                var properties = employee.GetType().GetProperties();
                foreach (var prop in properties)
                {
                    //Lấy tên prop
                    var propName = prop.Name;

                    //Lấy giá trị của prop
                    var propValue = prop.GetValue(employee);

                    dynamicParams.Add($"@{propName}", propValue);
                    columnNames += $"{propName},";
                    columnsValues += $"@{propName},";

                }
                columnNames = columnNames.Remove(columnNames.Length - 1, 1);
                columnsValues = columnsValues.Remove(columnsValues.Length - 1, 1);

                //4. Thực thi SQL để thêm mới dữ liệu
                string sqlCommand = $"INSERT INTO Employee ({columnNames}) VALUES ({columnsValues})";
                var rowAffected = dbConnection.Execute(sqlCommand, param: dynamicParams);
                // Kiểm tra dữ liệu trả về
                if (rowAffected > 0)
                {
                    return StatusCode(201);
                }
            }
            catch (Exception ex)
            {
                var devMsg = ex.Message;
                if (devMsg.Contains("Duplicate"))
                {
                    //Dữ liệu gửi lên bị trùng
                    var errorMsg = new
                    {
                        devMsg = devMsg,
                        userMsg = Properties.Resource.Exception_Msg_DuplicateField,
                        errorCode = "Exception_Msg_DuplicateField"
                    };
                    var response = StatusCode(400, errorMsg);
                    return response;
                }
                else
                {
                    //Lỗi kết nối đến csdl
                    var errorMsg = new
                    {
                        devMsg = devMsg,
                        userMsg = Properties.Resource.Exception_Msg_DBConnection,
                        errorCode = "Exception_Msg_DBConnection"
                    };
                    var response = StatusCode(500, errorMsg);
                    return response;
                }

            }
        }

        #endregion


        /// <summary>
        /// Cập nhật thông tin nhân viên
        /// </summary>
        /// <param name="employeeId">Id nhân viên</param>
        /// <param name="employee">Object chứa thông tin nhân viên</param>
        /// <returns></returns>
        [HttpPut("{employeeId}")]
        public IActionResult UpdateEmployee(Guid employeeId, Employee employee)
        {
            //Truy cập data base
            //1. Khai báo thông tin kết nối
            var localConnection = DBInfo.localConnection;

            //2.Khởi tạo đối tượng kết nối với csdl
            IDbConnection dbConnection = new MySqlConnection(localConnection);
            var dynamicParams = new DynamicParameters();

            //3. Tạo câu truy vấn thêm mới dữ liệu
            string updateColumnString = string.Empty;

            var properties = employee.GetType().GetProperties();
            foreach (var prop in properties)
            {
                //Lấy tên prop
                var propName = prop.Name;

                //Lấy giá trị của prop
                var propValue = prop.GetValue(employee);

                dynamicParams.Add($"@{propName}", propValue);
                updateColumnString += $"{propName} = '@{propName}' , ";
            }
            updateColumnString = updateColumnString.Remove(updateColumnString.Length - 2, 2);
            dynamicParams.Add($"@employeeId", employeeId);

            //4. Thực thi sql để update dữ liệu
            String sqlCommand = $"UPDATE Employee SET {updateColumnString} WHERE employeeId= @employeeId";
            var rowAffected = dbConnection.Execute(sqlCommand, param: dynamicParams);
            var response = StatusCode(200, rowAffected);
            return response;
        }


        [HttpDelete("{employeeId}")]
        public IActionResult deleteEmployeeById(Guid employeeId)
        {
            //Khai báo thông tin kết nối
            var localConnection = DBInfo.localConnection;

            //Khỏi tạo đối tượng kết nối với csdl
            IDbConnection dbConnection = new MySqlConnection(localConnection);

            //Tạo query lấy dữ liẹu
            string sqlCommand = $"DELETE FROM Employee WHERE employeeId= '{employeeId}'";
            DynamicParameters dynamicParams = new DynamicParameters();
            dynamicParams.Add("@employeeId", employeeId);

            //Thực hiện query lấy dữ liệu
            var rowAffected = dbConnection.Execute(sqlCommand);
            var response = StatusCode(200, rowAffected);
            return response;
        }


    }
    /**
     * 
     * 
     *
     *            //var Heading = ((IDictionary<string, object>)employeeCodeRow).Keys.ToArray();
            //var details = ((IDictionary<string, object>)employeeCodeRow);
            //var employeeCode = details[Heading[0]];


      var errorMsg = new
                    {
                        devMsg = "No Content",
                        userMsg = Properties.Resource.Error_Msg_NoContent,
                        errorCode = "Error_Msg_NoContent"
                    };
     */
}
