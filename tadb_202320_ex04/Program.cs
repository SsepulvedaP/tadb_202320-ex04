using Microsoft.AspNetCore.Cors.Infrastructure;
using tadb_202320_ex04.DbContexts;
using tadb_202320_ex04.Repositories;
using tadb_202320_ex04.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddSingleton<MongoDbContext>();

//El DBContext a utilizar
//Los repositorios
builder.Services.AddSingleton<MongoDbContext>();
builder.Services.AddScoped<AutobusesRepository, AutobusesRepository>();
builder.Services.AddScoped<CargadoresRepository, CargadoresRepository>();
builder.Services.AddScoped<Horarios_operacionRepository, Horarios_operacionRepository>();
builder.Services.AddScoped<Programacion_autobusesRepository, Programacion_autobusesRepository>();
builder.Services.AddScoped<Programacion_cargadoresRepository, Programacion_cargadoresRepository>();
// Add services to the container.
builder.Services.AddScoped<AutobusService>();
builder.Services.AddScoped<CargadorService>();
builder.Services.AddScoped<Horarios_OperacionService>();
builder.Services.AddScoped<Programacion_autobusesService>();
builder.Services.AddScoped<Programacion_cargadoresServices>();


builder.Services.AddControllers()
    .AddJsonOptions(
        options => options.JsonSerializerOptions.PropertyNamingPolicy = null);

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
