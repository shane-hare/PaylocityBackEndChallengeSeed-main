using Api.Dtos.Dependent;
using Api.Dtos.Employee;
using Api.Dtos.Paycheck;
using Api.Models;
using Api.Models.Converters;
using Api.Models.Database;
using Api.Models.Deduction;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class PaycheckController : ControllerBase
{
    readonly IDataRepository _repository;
    readonly List<IEmployeeDeduction> _employeeDeductions;
    readonly List<IDependentDeduction> _depenentDeductions;


    public PaycheckController(IDataRepository repository)
    //List<IEmployeeDeduction> employeeDeductions, List<IDependentDeduction> depenentDeductions
    {
        _repository = repository;

        //TODO inject transint instead of initialize here, was getting error
        //Ran out of Time

        _employeeDeductions = new List<IEmployeeDeduction>();
        _employeeDeductions.Add( new EmployeeDeduction() );
        _employeeDeductions.Add(new SalaryDeduction());

        _depenentDeductions = new List<IDependentDeduction>();
        _depenentDeductions.Add(new DependentDeduction());
        _depenentDeductions.Add(new AgeDeduction());

        //_employeeDeductions = employeeDeductions;
        //_depenentDeductions = depenentDeductions;

    }

    [SwaggerOperation(Summary = "Create Paycheck for Employee")]
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<List<GetPaychexDto>>>> Get(int id)
    {
        var Success = false;
        var Message = "";
        var data = new List<GetPaychexDto>();

        try
        {
            var employee = _repository.GetEmployee(id);

            if (employee == null)
            {
                return new ApiResponse<List<GetPaychexDto>>
                {
                    Data = null,
                    Success = true,
                    Message = "No Employee Found"
                };
            }

            DateTime now = DateTime.Now;

            for ( int i = 0; i <= 26; i++)
            {
                Paycheck pay = new Paycheck();
                pay.Id = System.Guid.NewGuid();
                pay.DateOfPaycheck = now.AddDays(14); //Could be UTC NOW
                now = pay.DateOfPaycheck;
                pay.Balance = employee.Salary / 26; //This needs to be global from a table / Calculate paychecks pay Month

                foreach (IEmployeeDeduction empDed in _employeeDeductions)
                {
                    decimal amount = empDed.Deduct(employee);

                    //Add Qualify for Deduction function to Ignore not qualifying Deductions

                    pay.Deductions.Add(new BaseDeduction()
                    {
                        DeductionType = empDed.DeductionType,
                        Amount = amount / 2
                    });

                    pay.Balance -= amount;


                }

                foreach (IDependentDeduction depDed in _depenentDeductions)
                {
                    foreach (Dependent dep in employee.Dependents)
                    {
                        //Add Qualify for Deduction function

                        decimal amount = depDed.Deduct(dep);

                        pay.Deductions.Add(new BaseDeduction()
                        {
                            DeductionType = depDed.DeductionType,
                            Amount = amount / 2
                        }); ;

                        pay.Balance -= amount;
                    }

                }

                var paycheck = PaycheckDtoConverter.Convert(pay);

                data.Add(paycheck);

                //This should really be thead safe to make sure all paychecks are added
                _repository.AddPaycheck(employee, pay);

                Success = true;
            }

        }
        //TODO: Seperate
        catch (Exception ex)
        {
            data = null; //Dont put blatent exception in response
            Success = false;
        }

        var result = new ApiResponse<List<GetPaychexDto>>
        {
            Data = data,
            Success = Success,
            Message = Message
        };

        return result;
    }

    [SwaggerOperation(Summary = "Get paycheck by Guid for employee")]
    [HttpGet("view/{guid}")]
    public async Task<ActionResult<ApiResponse<GetPaychexDto>>> GetPaycheck(System.Guid guid)
    {
        var Success = false;
        var data = new GetPaychexDto();

        try
        {
            var paycheck = _repository.GetPaycheck(guid);

            if (paycheck == null)
            {
                return NotFound();
            }

            data = PaycheckDtoConverter.Convert(paycheck);

            Success = true;
        }
        //TODO: Seperate
        catch (Exception ex)
        {
            data = null; //Dont put blatent exception in response
            Success = false;
        }

        var result = new ApiResponse<GetPaychexDto>
        {
            Data = data,
            Success = Success
        };

        return result;
    }


}

