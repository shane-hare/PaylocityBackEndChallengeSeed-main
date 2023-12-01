namespace Api.Models.Deduction
{
    public class SalaryDeduction : BaseDeduction, IEmployeeDeduction
    {
        public SalaryDeduction() 
        {
            DeductionType = DeductionRelationship.SalaryEmployee;
        }

        public decimal Deduct(Employee employee)
        {
            if (employee == null)
            {
                throw new ArgumentNullException("Employee is null");
            }

            if (employee.Salary > 80_000)
            {
                return employee.Salary * .02m;
            }

            return 0.0m;
        }
    }
}
