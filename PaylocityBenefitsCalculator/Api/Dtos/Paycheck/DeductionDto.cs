using Api.Models;

namespace Api.Dtos.Paycheck
{
    public class DeductionDto
    {
        public DeductionRelationship DeductionType { get; set; }
        public decimal Amount { get; set; }
    }
}
