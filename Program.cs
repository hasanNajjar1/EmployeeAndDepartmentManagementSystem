using EmployeeAndDepartmentManagementSystem.Context;
using EmployeeAndDepartmentManagementSystem.Implementation;
using EmployeeAndDepartmentManagementSystem.Implemintitions;
using EmployeeAndDepartmentManagementSystem.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

// Add Swagger generation
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Employee And Department Management API",
        Version = "first version",
        Description = "An API For Control and Manage Employee And Department Management System created by hasan najjar",
        Contact = new OpenApiContact
        {
            Name = "Hasan Yasser",
            Email = "hasan1928@live.com",
        }
    });
    //enabling xml comments
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    option.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<EaDMContext>(con => con.UseSqlServer("Data Source=LAPTOP-01TSOR8L\\SQLEXPRESS;" +
	"Initial Catalog=EmployeeAndDepartmentManagementSystem;Integrated Security=True;Trust Server Certificate=True"));

// Dependency Injection
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IAuthanticationService, AuthanticationService>();

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
