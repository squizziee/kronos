using Kronos.Machina.API.Extensions;
using Kronos.Machina.Application.Extensions;
using Kronos.Machina.Infrastructure.Extensions;
using MediatR;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("Settings/SanitizerSettings.json", false);
builder.Configuration.AddJsonFile("Settings/VideoStorageSettings.json", false);
builder.Configuration.AddJsonFile("Settings/FFmpegConfig.json", false);

builder.Services.AddBackgroundJobProvider();
builder.Services.AddApplication();
builder.Services.AddInfrastructureOptions(builder.Configuration);
builder.Services.AddInfrastructureImplementations(builder.Configuration);
builder.Services.AddDatabase(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddAuthentication();
//builder.Services.AddAuthorization();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllers();

app.Run();