namespace Api.Models.Database
{
    public interface IDataRepository
    {
        public Employee? GetEmployee(int id);

        public List<Employee>? GetEmployees();

        public Dependent? GetDependent(int id);

        public List<Dependent?> GetDependents();

        public Paycheck? GetPaycheck(System.Guid id);

        public void AddPaycheck(Employee employee, Paycheck paycheck);       
    }
}
