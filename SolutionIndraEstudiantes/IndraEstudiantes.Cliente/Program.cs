using IndraEstudiantes.AccesoDatos.Dacs;
using IndraEstudiantes.AccesoDatos.Interfaces;
using IndraEstudiantes.Entidades.Herramientas;
using IndraEstudiantes.Entidades.Herramientas.Interfaces;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
Assembly GetAssemblyByName(string name)
{
    return AppDomain.CurrentDomain.GetAssemblies().
           SingleOrDefault(assembly => assembly.GetName().Name == name);
}

var a = GetAssemblyByName("IndraEstudiantes.Cliente");

using var stream = a.GetManifestResourceStream("IndraEstudiantes.Cliente.appsettings.json");

var config = new ConfigurationBuilder()
            .AddJsonStream(stream)
            .Build();

builder.Configuration.AddConfiguration(config);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddScoped<IRestHelper, RestHelper>();
builder.Services.AddScoped<IConexionDac, ConexionDac>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
