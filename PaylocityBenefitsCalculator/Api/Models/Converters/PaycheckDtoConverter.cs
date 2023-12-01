using Api.Dtos.Employee;
using Api.Dtos.Paycheck;

namespace Api.Models.Converters
{
    public class PaycheckDtoConverter
    {
        public static GetPaychexDto? Convert(Paycheck paycheck)
        {
            if (paycheck == null)
            {
                return null;
            }

            var paycheckDto = new GetPaychexDto()
            {
                Id = paycheck.Id,
                Balance = paycheck.Balance,
                DateOfPaycheck = paycheck.DateOfPaycheck,
                Deductions = paycheck.Deductions
            };

            return paycheckDto;
        }
    }
}
