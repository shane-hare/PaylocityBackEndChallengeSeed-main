using Api.Models;

namespace Api.Models.Deduction
{
    public class EmployeeDeduction : BaseDeduction, IEmployeeDeduction
    {
        public EmployeeDeduction()
        {
            DeductionType = DeductionRelationship.Employee;
        }

        public decimal Deduct(Employee employee)
        {
            decimal deduct = 0;

            if (employee == null)
            {
                throw new ArgumentNullException("Employee can't be null to calculate deductions");
            }

            deduct = 1000;

            return deduct;
        }
    }
}
