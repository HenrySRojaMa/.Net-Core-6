using Taller1.Interfaces;
using Taller1.Models;
using Taller1.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<PruebaContext>();
builder.Services.AddScoped<IClientes, ClientesService>();
builder.Services.AddScoped<IProductos, ProductosServices>();
builder.Services.AddScoped<IFacturas, FacturasSrevices>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
