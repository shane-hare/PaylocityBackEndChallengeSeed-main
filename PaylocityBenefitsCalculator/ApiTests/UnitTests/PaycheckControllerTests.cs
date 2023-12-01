using Api.Controllers;
using Api.Dtos.Paycheck;
using Api.Models;
using Api.Models.Converters;
using Api.Models.Database;
using Api.Models.Deduction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ApiTests.UnitTests
{
    public class PaycheckControllerTests
    {
        IDataRepository _repository = new InMemoryRepository();

        [Fact]
        public void PaycheckController_NoEmployee()
        {
            PaycheckController controller = new PaycheckController(_repository);

            var result = controller.Get(int.MinValue);

            Assert.NotNull(result);            

        }

        [Fact]
        public void PaycheckController_NoPaycheck()
        {
            PaycheckController controller = new PaycheckController(_repository);

            var result = controller.Get(int.MinValue);

            Assert.NotNull(result);
        }

        [Fact]
        public void PaycheckController_Paycheck()
        {
            DateTime now = DateTime.Now;
            System.Guid id = System.Guid.NewGuid();

            BaseDeduction deduction = new BaseDeduction()
            {
                DeductionType = DeductionRelationship.Employee,
                Amount = 1000.0m
            };

            Paycheck pay = new Paycheck();
            pay.Id = id;
            pay.Balance = 10000;
            pay.DateOfPaycheck = now;
            pay.Deductions = new List<IDeduction>();
            pay.Deductions.Add(deduction);

            var employee = _repository.GetEmployee(1);

            _repository.AddPaycheck(employee, pay);

            PaycheckController controller = new PaycheckController(_repository);

            var result = controller.GetPaycheck(id);

            var data = result.Result.Value.Data;

            var converted = PaycheckDtoConverter.Convert(pay);
             
            Assert.NotNull(result);
            Assert.Equal(data, converted);
        }

        public void PaycheckController_PaycheckCalculateDeductions()
        {
            DateTime now = DateTime.Now;
            System.Guid id = System.Guid.NewGuid();

            PaycheckController controller = new PaycheckController(_repository);


            var employee = _repository.GetEmployee(1);
            var result = controller.Get(1);

            var data = result.Result.Value.Data;

            Assert.NotNull(result);

            var total = 0.0m;

            foreach(GetPaychexDto payDto in data)
            {
                foreach(BaseDeduction dec in payDto.Deductions)
                {
                    total += dec.Amount;
                }
                total += payDto.Balance;
            }

            Assert.Equal(total, employee.Salary);
            


            
            
        }
    }
}
