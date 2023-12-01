using Api.Models.Deduction;

namespace Api.Dtos.Paycheck
{
    public class GetPaychexDto
    {
        public System.Guid Id { get; set; }
        public decimal Balance { get; set; }
        public DateTime DateOfPaycheck { get; set; }

        public ICollection<IDeduction> Deductions { get; set; } = new List<IDeduction>();
    }
}
