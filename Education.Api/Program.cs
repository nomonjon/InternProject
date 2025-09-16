using Education.Api.Controllers;
using Education.Api.Data;
using Education.Api.Dtos;
using Education.Api.Interfaces;
using Education.Api.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddOpenApi();
builder.Services.AddDbContext<EducationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("EducationDb")));

builder.Services.AddScoped<ICityRepo, CityRepo>();
builder.Services.AddScoped<IDepartmentRepo, DepartmentRepo>();
builder.Services.AddScoped<ISubjectRepo, SubjectRepo>();
builder.Services.AddScoped<IStudentRepo, StudentRepo>();
builder.Services.AddScoped<ITeacherRepo, TeacherRepo>();
builder.Services.AddScoped<IStudentSubjectRepo, StudentSubjectRepo>();
builder.Services.AddScoped<ITeacherSubjectRepo, TeacherSubjectRepo>();
builder.Services.AddScoped<IFilterRepo, FiltreRepo>();

builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();


app.Run();
