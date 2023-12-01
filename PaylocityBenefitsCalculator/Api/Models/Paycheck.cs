using Api.Models.Deduction;
using System.Collections;

namespace Api.Models
{
    public class Paycheck
    {
        public System.Guid Id { get; set; }
        public decimal Balance { get; set; }
        public DateTime DateOfPaycheck{get;set;}

        public ICollection<IDeduction> Deductions { get; set; } = new List<IDeduction>();

    }
}
