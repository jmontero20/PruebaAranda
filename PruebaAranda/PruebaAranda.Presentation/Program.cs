using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using PruebaAranda.Application.Services;
using PruebaAranda.Application.Services.Interfaces;
using PruebaAranda.Domain.Repositories;
using PruebaAranda.Infrastructure.Persistence;
using PruebaAranda.Presentation.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ProductoDbContext>(options =>
                 options.UseSqlServer(builder.Configuration.GetConnectionString("ApplicationDbContext")));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Services
builder.Services.AddScoped<IProductoService, ProductoService>();
// Repositories
builder.Services.AddScoped<IProductoRepository, ProductoRepository>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});
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
app.UseMiddleware<ExceptionMiddleware>();
app.UseCors("AllowAll");
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
           Path.Combine(builder.Environment.ContentRootPath, "Imagenes")),
    RequestPath = "/Imagenes"
});
app.Run();
