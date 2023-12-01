using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.OpenApi.Models;

using Api.Models.Database;
using Microsoft.AspNetCore.Rewrite;
using Api.Models.Deduction;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Employee Benefit Cost Calculation Api",
        Description = "Api to support employee benefit cost calculations"
    });
});

var allowLocalhost = "allow localhost";
builder.Services.AddCors(options =>
{
    options.AddPolicy(allowLocalhost,
        policy => { policy.WithOrigins("http://localhost:3000", "http://localhost"); });
});

//Document:
//We add a Singleton here right now as we only want one database connection,
//making multiple database connections each time an object is created is significant overhead
builder.Services.AddSingleton<IDataRepository, InMemoryRepository>();

//This could have been Factories
builder.Services.AddTransient<IEmployeeDeduction, EmployeeDeduction>();
builder.Services.AddTransient<IEmployeeDeduction, SalaryDeduction>();

builder.Services.AddTransient<IDependentDeduction, DependentDeduction>();
builder.Services.AddTransient<IDependentDeduction, AgeDeduction>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(allowLocalhost);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
