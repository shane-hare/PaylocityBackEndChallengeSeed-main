namespace Api.Models.Deduction
{
    public interface IDeduction
    {
        DeductionRelationship DeductionType { get; set; }
        decimal Amount { get; set; }
    }
}
