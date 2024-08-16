using CompanyV4.Application.Services;
using CompanyV4.Domain;
using CompanyV4.Domain.Interfaces;
using CompanyV4.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Konfigurasi koneksi ke PostgreSQL
var connectionStringPostgre = builder.Configuration.GetConnectionString("PostgreSQLConnection");
builder.Services.AddDbContext<CompanyContext>(options => options.UseNpgsql(connectionStringPostgre));

// Register options pattern configurations
builder.Services.Configure<ProjectOptions>(builder.Configuration.GetSection("ProjectOptions"));
builder.Services.Configure<EmployeeOptions>(builder.Configuration.GetSection("EmployeeOptions"));
builder.Services.Configure<DepartmentOptions>(builder.Configuration.GetSection("DepartmentOptions"));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IDepartementService, DepartementService>();
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<IWorksOnService, WorksService>();



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
