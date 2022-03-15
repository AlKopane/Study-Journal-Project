using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using StudyJournal.DataBase;
using StudyJournal.DataBase.Data;
using StudyJournal.University;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using StudyJournal.DataBase.Entities;



var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Logging.ClearProviders();
builder.Logging.AddConsole();


builder.Services.Configure<StudyJournal.University.Models.MailSettings>(builder.Configuration.GetSection("MailSettings"));
StudyJournal.University.UniversityInitializer.RegisterDbContext(builder.Services, builder.Configuration.GetConnectionString("DefaultConnection"));
StudyJournal.University.UniversityInitializer.RegisterServices(builder.Services);

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<DataBaseContext>();
    DbInitializer.Initialize(context);   
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


CreateHostBuilder(args).Build().Run();

static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration(builder =>
    {
        builder.AddUserSecrets<Program>();
    })
    .ConfigureWebHostDefaults(webBuilder =>
    {
        webBuilder.UseStartup<Program>();
    });

public partial class Program { }