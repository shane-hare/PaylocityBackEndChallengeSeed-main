using Api.Dtos.Dependent;
using Api.Dtos.Employee;
using Api.Models;
using Api.Models.Converters;
using Api.Models.Database;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class DependentsController : ControllerBase
{
    readonly IDataRepository _repository;

    public DependentsController(IDataRepository repository)
    {
        _repository = repository;
    }

    [SwaggerOperation(Summary = "Get dependent by id")]
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<GetDependentDto>>> Get(int id)
    {
        var Success = false;
        var data = new GetDependentDto();

        try
        {
            var dependent = _repository.GetDependent(id);

            if (dependent == null)
            {
                return NotFound();
            }

            //ToDo: Check If employee is found, custom response message
            data = DependentDtoConverter.Convert(dependent);

            Success = true;
        }
        //TODO: Seperate
        catch (Exception ex)
        {
            data = null; //Dont put blatent exception in response
            Success = false;
        }

        var result = new ApiResponse<GetDependentDto>
        {
            Data = data,
            Success = Success
        };

        return result;
    }

    [SwaggerOperation(Summary = "Get all dependents")]
    [HttpGet("")]
    public async Task<ActionResult<ApiResponse<List<GetDependentDto>>>> GetAll()
    {
        var Success = false;
        var data = new List<GetDependentDto>();

        try
        {
            var dependents = _repository.GetDependents();

            data = dependents.ConvertAll(new Converter<Dependent, GetDependentDto>(DependentDtoConverter.Convert));

            Success = true;
        }
        //TODO: Seperate
        catch (Exception ex)
        {
            data = null; //Dont put blatent exception in response
            Success = false;
        }

        var result = new ApiResponse<List<GetDependentDto>>
        {
            Data = data,
            Success = Success
        };

        return result;
    }
}
