namespace Api.Models.Deduction
{
    public interface IDependentDeduction
    {
        DeductionRelationship DeductionType { get; set; }
        decimal Deduct(Dependent employee);
    }
}
