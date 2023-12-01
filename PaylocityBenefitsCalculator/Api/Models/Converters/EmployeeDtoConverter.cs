using Api.Dtos.Employee;

namespace Api.Models.Converters
{
    public class EmployeeDtoConverter
    {
        public static GetEmployeeDto? Convert(Employee employee)
        {
            if (employee == null)
            {
                return null;
            }

            var employeeDto = new GetEmployeeDto()
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Salary = employee.Salary,
                DateOfBirth = employee.DateOfBirth,
                Dependents = employee.Dependents
                                    ?.Select(dependent => DependentDtoConverter.Convert(dependent))
                                    .ToList()
            };

            return employeeDto;
        }

    }
}
