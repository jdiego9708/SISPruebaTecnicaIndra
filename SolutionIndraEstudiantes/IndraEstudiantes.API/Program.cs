using IndraEstudiantes.AccesoDatos.Dacs;
using IndraEstudiantes.AccesoDatos.Interfaces;
using IndraEstudiantes.Entidades.Herramientas.Interfaces;
using IndraEstudiantes.Entidades.Herramientas;
using Microsoft.OpenApi.Models;
using IndraEstudiantes.Servicios.Interfaces;
using IndraEstudiantes.Servicios.Servicios;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Lista de puntos finales para la prueba de Indra Estudiantes por parte de Juan Diego Duque", Version = "v1.1" });
});
//INYECCION DE DEPENDENCIAS
builder.Services.AddTransient<IConexionDac, ConexionDac>()
                .AddTransient<IRestHelper, RestHelper>()
                .AddTransient<ICalificacionesServicio, CalificacionesServicio>()
                .AddTransient<IEstudiantesServicio, EstudiantesServicio>()
                .AddTransient<ICalificacionesDac, CalificacionesDac>()
                .AddTransient<IEstudiantesDac, EstudiantesDac>();

builder.Services.AddCors();

builder.Services.AddControllers(options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseCors(options =>
    {
        options.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
}

app.UseHttpsRedirection();

app.UseSwagger();

app.UseSwaggerUI();

app.UseHttpsRedirection();

app.MapControllers();

app.UseAuthentication();

app.UseRouting();

app.UseAuthorization();

app.UseDefaultFiles();

app.UseStaticFiles();

app.Run();
