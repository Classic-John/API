using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Tema_1.DataBase;

var builder = WebApplication.CreateBuilder(args);
// Disable SSL certificate verification
//AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
builder.Configuration.AddJsonFile("appsettings.json", optional: false);

builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Covid statistics", Version = "v1" });
});

// add your database context service
builder.Services.AddDbContext<Reports>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SQLEXPRESS")));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Covid Statistics v1"));
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();