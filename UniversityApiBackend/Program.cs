//1. Usings to work with EntityFramework
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using UniversityApiBackend;
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

//7. Add Service of JWT Autorization

builder.Services.AddJwtTokenServices(builder.Configuration);

// Add services to the container.

builder.Services.AddControllers();

//4. Inyección de dependencias de los servicios
builder.Services.AddScoped<IStudentsService, StudentsService>();
builder.Services.AddScoped<ICoursesService, CoursesService>();
builder.Services.AddScoped<IChaptersService, ChaptersService>();

//8. Add Autorizathion
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("UsersOnlyPolicy", pocily => pocily.RequireClaim("UsersOlny", "Users1"));
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//9. Configurar Swagger para que tenga en cuenta el JWT como autorización.
builder.Services.AddSwaggerGen(options =>
{
    //Definimos la seguridad de la autorizacion
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization Header using Bearer Scheme"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
               new OpenApiSecurityScheme
    {
        Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id= "Bearer"
        }
    },
    new string[]
    {

    }
        }

    });
});

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
