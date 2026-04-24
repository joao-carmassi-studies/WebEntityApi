using WebEntityApi.Extensions;
using WebEntityApi.Models;
using WebEntityApi.Repository;
using WebEntityApi.Service;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<WebEntityApiDbContext>();
builder.Services.AddScoped<Dal<User>>();
builder.Services.AddScoped<Dal<Tenant>>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<TenantService>();

builder.Services.AddOpenApi();

var app = builder.Build();

app.UseGlobalExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
