//1. Usings to work with EntityFramework
using Microsoft.EntityFrameworkCore;
using UniversityApiBackend.DataAccess;
using UniversityApiBackend.Services;
using UniversityApiBackend.Services.ChapterService;
using UniversityApiBackend.Services.CoursesService;
using UniversityApiBackend.Services.StudentServices;

var builder = WebApplication.CreateBuilder(args);

// 2. Connection with SQL Server
const string ConexionName = "UniversityDB";
var connectionString = builder.Configuration.GetConnectionString(ConexionName);

//3. Add Context to Services of Builder (desde la carpeta DataAccess)

builder.Services.AddDbContext<UniversityDBContext>(options => options.UseSqlServer(connectionString));


// Add services to the container.

builder.Services.AddControllers();

//4. Inyección de dependencias de los servicios
builder.Services.AddScoped<IStudentsService, StudentsService>();
builder.Services.AddScoped<ICoursesService, CoursesService>();
builder.Services.AddScoped<IChapterService, ChapterService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//5. Habilitar el CORS

builder.Services.AddCors
    (options =>
        {
            options.AddPolicy(name: "CorsPolicy", builder =>
             {
                 builder.AllowAnyOrigin();
                 builder.AllowAnyMethod();
                 builder.AllowAnyHeader();
             });
        }
    );


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

// 6. Decirle a la aplicacion que haga uso de CORS

app.UseCors("CorsPolicy");

app.Run();
