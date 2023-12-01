namespace Api.Models.Deduction
{
    public class BaseDeduction : IDeduction
    {
        public DeductionRelationship DeductionType { get; set; }
        public decimal Amount { get; set; }
    }
}
