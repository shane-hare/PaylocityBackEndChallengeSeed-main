using Api.Dtos.Dependent;
using Api.Dtos.Employee;
using Api.Models;
using Api.Models.Converters;
using Api.Models.Database;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class EmployeesController : ControllerBase
{
    readonly IDataRepository _repository;

    public EmployeesController(IDataRepository repository)
    {
        _repository = repository;
    }

    [SwaggerOperation(Summary = "Get employee by id")]
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<GetEmployeeDto>>> Get(int id)
    {
        var Success = false;
        var data = new GetEmployeeDto();

        try
        {
            var employee = _repository.GetEmployee(id);

            //ToDo: Check If employee is found, custom response message
            data = EmployeeDtoConverter.Convert(employee);

            Success = true;
        }
        //TODO: Seperate
        catch(Exception ex)
        {
            data = null; //Dont put blatent exception in response
            Success = false;
        }

        var result = new ApiResponse<GetEmployeeDto>
        {
            Data = data,
            Success = Success
        };

        return result;
    }

    [SwaggerOperation(Summary = "Get all employees")]
    [HttpGet("")]
    public async Task<ActionResult<ApiResponse<List<GetEmployeeDto>>>> GetAll()
    {
        var Success = false;
        var data = new List<GetEmployeeDto>();

        try
        {
            var employees = _repository.GetEmployees();

            var employeesDto = employees.ConvertAll(new Converter<Employee, GetEmployeeDto>(
                EmployeeDtoConverter.Convert));

            Success = true;
        }
        //TODO: Seperate
        catch (Exception ex)
        {
            data = null; //Dont put blatent exception in response
            Success = false;
        }

        var result = new ApiResponse<List<GetEmployeeDto>>
        {
            Data = data,
            Success = Success
        };

        return result;
    }


}
