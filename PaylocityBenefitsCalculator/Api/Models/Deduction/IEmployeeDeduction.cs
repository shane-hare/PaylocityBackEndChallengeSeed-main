namespace Api.Models.Deduction
{
    public interface IEmployeeDeduction
    {
        DeductionRelationship DeductionType { get; set; }
        decimal Deduct(Employee employee);
    }

}
