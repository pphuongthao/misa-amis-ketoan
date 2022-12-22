using Microsoft.AspNetCore.Http;
using MISA.AMIS.Core.Exceptions;
using MISA.AMIS.Core.Interfaces.Repository;
using MISA.AMIS.Core.Interfaces.Services;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MISA.AMIS.Core.Services
{
    public class EmployeeService : BaseService<Employee>, IEmployeeService
    {
        #region Constructor
        IEmployeeRepository _repository;
        string langCode = Common.LanguageCode;
        
        public EmployeeService(IEmployeeRepository repository) : base(repository)
        {
            _repository = repository;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Thực hiện ghi đè lại hàm Validate dữ liệu
        /// VTPTHAO (20/06/2022)
        /// </summary>
        /// <param name="employee">đối tượng nhân viên</param>
        /// <returns>true -  hợp lệ, false - không hợp lệ</returns>
  
        protected override bool Validate(Employee employee)
        {
            // Check bắt buộc nhập
            // 1. Mã nhân viên bắt buộc nhập
            if (string.IsNullOrEmpty(employee.EmployeeCode))
            {
                IsValid = false;
                ErrorValidateMsgs.Add(Resources.Resource.ResourceManager.GetString($"ErrorEmployeeCode_{langCode}"));
            }
            // 2. Tên nhân viên bắt buộc nhập
            if (string.IsNullOrEmpty(employee.EmployeeName))
            {
                IsValid = false;
                ErrorValidateMsgs.Add(Resources.Resource.ResourceManager.GetString($"ErrorEmployeeName_{langCode}"));
            }
            // 3. Phòng ban bắt buộc nhập
            /*if (Guid.Empty(employee.DepartmentId))
            {
                IsValid = false;
                ErroValidateMsgs.Add(Resources.Resource.ErrorDepartmentId);
            }*/
            // Check trùng mã bắt buộc nhập
            if (_repository.CheckEmployeeCodeExists(employee.EmployeeCode) == true)
            {
                IsValid = false;
                ErrorValidateMsgs.Add(Resources.Resource.ResourceManager.GetString($"HaveEmployeeCode_{langCode}"));
            }
            //4. check email đúng định dạng
            if (!string.IsNullOrEmpty(employee.Email))
            {
                if (!Regex.IsMatch(employee.Email, @"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$"))
                {
                    ErrorValidateMsgs.Add(Resources.Resource.ResourceManager.GetString($"ErrorEmail_{langCode}"));
                    IsValid = false;
                }
            }
            // Check ngày tháng phải nhỏ hơn ngày hiện tại
            if (employee.DateOfBirth > DateTime.Today)
            {
                ErrorValidateMsgs.Add(Resources.Resource.ResourceManager.GetString($"ErrorDateOfBirth_{langCode}"));
                IsValid = false;
            }
            if (employee.IdentityDate > DateTime.Today)
            {
                ErrorValidateMsgs.Add(Resources.Resource.ResourceManager.GetString($"ErrorIdentityDate_{langCode}"));
                IsValid = false;
            }
            return IsValid;
        }
        /// <summary>
        /// Thực hiện ghi đè lại hàm Validate dữ liệu khi sửa
        /// VTPTHAO (08/07/2022)
        /// </summary>
        /// <param name="employee">đối tượng nhân viên</param>
        /// <returns>true -  hợp lệ, false - không hợp lệ</returns>
        protected override bool ValidateUpdate(Employee employee)
        {
            // Check bắt buộc nhập
            // 1. Mã nhân viên bắt buộc nhập
            if (string.IsNullOrEmpty(employee.EmployeeCode))
            {
                IsValid = false;
                ErrorValidateMsgs.Add(Resources.Resource.ResourceManager.GetString($"ErrorEmployeeCode_{langCode}"));
            }
            // 2. Tên nhân viên bắt buộc nhập
            if (string.IsNullOrEmpty(employee.EmployeeName))
            {
                IsValid = false;
                ErrorValidateMsgs.Add(Resources.Resource.ResourceManager.GetString($"ErrorEmployeeName_{langCode}"));
            }
            //3. check email đúng định dạng
            if (!string.IsNullOrEmpty(employee.Email))
            {
                if (!Regex.IsMatch(employee.Email, @"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$"))
                {
                    ErrorValidateMsgs.Add(Resources.Resource.ResourceManager.GetString($"ErrorEmail_{langCode}"));
                    IsValid = false;
                }
            }
            // Check ngày tháng phải nhỏ hơn ngày hiện tại
            if (employee.DateOfBirth > DateTime.Today)
            {
                ErrorValidateMsgs.Add(Resources.Resource.ResourceManager.GetString($"ErrorDateOfBirth_{langCode}"));
                IsValid = false;
            }
            if (employee.IdentityDate > DateTime.Today)
            {
                ErrorValidateMsgs.Add(Resources.Resource.ResourceManager.GetString($"ErrorIdentityDate_{langCode}"));
                IsValid = false;
            }
            return IsValid;
        }

        /// <summary>
        /// Thực hiện sửa thông tin nhân viên
        /// VTPTHAO (20/06/2022)
        /// </summary>
        /// <param name="employee">đối tượng nhân viên</param>
        public int UpdateService(Employee employee)
        {
            //Nếu validate hợp lệ thì thực hiện thêm mới
            return _repository.Update(employee);
        }
        /// <summary>
        /// Nhập khẩu dữ liệu
        /// VTPTHAO (8/7/2022)
        /// </summary>
        /// <param name="file">file chứa dữ liệu</param>
        /// <returns>input: file</returns>
        public IEnumerable<Employee> Import(IFormFile fileImport)
        {
            //validate tệp
            /*if (fileImport == null || fileImport.Length <= 0)
            {
                return DemoResponse<List<UserInfo>>.GetResult(-1, "fileImport is empty");
            }*/

            if (!Path.GetExtension(fileImport.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
            {
                throw new Exception(MISA.AMIS.Core.Resources.Resource.ErrorFile);
            }

            var employees = new List<Employee>();

            using (var stream = new MemoryStream())
            {
                fileImport.CopyToAsync(stream);

                using (var package = new ExcelPackage(stream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                    var rowCount = worksheet.Dimension.Rows;

                    for (int row = 2; row <= rowCount; row++)
                    {
                        var employeeCode = ConvertToString(worksheet.Cells[row, 2].Value);
                        var employeeName = ConvertToString(worksheet.Cells[row, 3].Value);
                        var genderName = ConvertToString(worksheet.Cells[row, 4].Value);
                        var dateOfBirth = ConvertToDate(worksheet.Cells[row, 5].Value);
                        var identityNumber = ConvertToString(worksheet.Cells[row, 6].Value);
                        var positionName = ConvertToString(worksheet.Cells[row, 7].Value);
                        var departmentName = ConvertToString(worksheet.Cells[row, 8].Value);
                        var bankNumber = ConvertToString(worksheet.Cells[row, 9].Value);
                        var bankName = ConvertToString(worksheet.Cells[row, 10].Value);
                        var bankBranchName = ConvertToString(worksheet.Cells[row, 11].Value);

                        employees.Add(new Employee
                        {
                            EmployeeCode = employeeCode,
                            EmployeeName = employeeName,
                            DateOfBirth = dateOfBirth,
                            IdentityNumber = identityNumber,
                            PositionName = positionName,
                            DepartmentName = departmentName,
                            BankNumber = bankNumber,
                            BankName = bankName,
                            BankBranchName = bankBranchName,
                        });
                    }
                }
            }
            return employees;
        }
        /// <summary>
        /// thiết lập giá trị cho mỗi ô
        /// VTPTHAO (22/7/2022)
        /// </summary>
        /// <param name="cell">Địa chỉ của ô</param>
        /// <param name="value">Giá trị cần truyền vào</param>
        private void SetCell(ExcelRange cell, string? value)
        {
            // Set border
            var border = cell.Style.Border;
            border.Bottom.Style =
                border.Top.Style =
                border.Left.Style =
                border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            // Set font name
            cell.Style.Font.Name = "Times New Roman";
            // Set font size
            cell.Style.Font.Size = 11;
            // Set value
            cell.Value = value;
        }
        private void SetCellCenter(ExcelRange cell, string? value)
        {
            SetCell(cell, value);
            // căn giữa
            cell.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
        }
        /// <summary>
        /// xuất khẩu dữ liệu ra file excel
        /// VTPTHAO (22/7/2022)
        /// </summary>
        /// <returns>excel package</returns>
        public ExcelPackage Export()
        {
            List<Employee> employeeList = new List<Employee>();
            employeeList = _repository.Get().ToList();
            ExcelPackage package = new ExcelPackage();
            ExcelWorksheet workSheet = package.Workbook.Worksheets.Add(Resources.Resource.FileName);
            workSheet.Cells.Style.Font.Name = "Arial";
            workSheet.Cells[1, 1].Value = Resources.Resource.FileName;
            workSheet.Cells[1, 1, 1, 9].Merge = true;
            workSheet.Cells[1, 1, 1, 9].Style.Font.Size = 16;
            workSheet.Cells[1, 1, 1, 9].Style.Font.Bold = true;
            workSheet.Cells[1, 1, 1, 9].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            // mảng các tên cột
            string[] heads = { "STT", "Mã nhân viên", "Tên nhân viên", "Giới tính", "Ngày sinh", "Chức danh", "Đơn vị", "Số tài khoản", "Tên ngân hàng" };

            // chỉ số cột hiện tại
            int colIndex = 1;
            foreach (var item in heads)
            {

                var cell = workSheet.Cells[3, colIndex];
                // Set màu thành gray
                var fill = cell.Style.Fill;
                fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
                // Căn chỉnh border
                var border = cell.Style.Border;
                border.Bottom.Style =
                    border.Top.Style =
                    border.Left.Style =
                    border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                // Font bold
                cell.Style.Font.Bold = true;
                // Font size
                cell.Style.Font.Size = 10;
                // Text center
                cell.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                // set value
                cell.Value = item;

                colIndex++;
            }
            // hàng bắt đầu ghi dữ liệu
            int rowStart = 4;
            // số thứ tự
            int index = 1;
            foreach (var item in employeeList)
            {
                SetCell(workSheet.Cells[string.Format("A{0}", rowStart)], index.ToString());
                SetCell(workSheet.Cells[string.Format("B{0}", rowStart)], item.EmployeeCode);
                SetCell(workSheet.Cells[string.Format("C{0}", rowStart)], item.EmployeeName);
                SetCell(workSheet.Cells[string.Format("D{0}", rowStart)], item.GenderName);
                SetCellCenter(workSheet.Cells[string.Format("E{0}", rowStart)], string.Format("{0:dd/MM/yyyy}", item.DateOfBirth));
                SetCell(workSheet.Cells[string.Format("F{0}", rowStart)], item.PositionName);
                SetCell(workSheet.Cells[string.Format("G{0}", rowStart)], item.DepartmentName);
                SetCell(workSheet.Cells[string.Format("H{0}", rowStart)], item.BankNumber);
                SetCell(workSheet.Cells[string.Format("I{0}", rowStart)], item.BankName);
                index++;
                rowStart++;
            }
            workSheet.Cells["A:AZ"].AutoFitColumns();
            return package;
        }
        /// <summary>
        /// Xử lý dữ liệu chuỗi trước khi xuất file
        /// VTPTHAO (20/07/2022)
        /// </summary>
        /// <param name="value">đối tượng được truyền vào</param>
        /// <returns>đối tượng đúng định dạng</returns>
        private string ConvertToString(object? value)
        {
            if (value != null)
                return value.ToString();
            else return null;
        }
        /// <summary>
        /// Xử lý thông tin ngày tháng
        /// VTPTHAO (20/07/2022)
        /// </summary>
        /// <param name="value">đối tượng được truyền vào</param>
        /// <returns>ngày tháng đúng định dạng</returns>
        private DateTime? ConvertToDate(object? value)
        {
            DateTime dateConvert;
            if (value != null)
            {
                if (DateTime.TryParse(value.ToString(), out dateConvert))
                {
                    return dateConvert;
                }
                return null;
            }
            else
            {
                return null;
            }
        }
        #endregion
    }
}
