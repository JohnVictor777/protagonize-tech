using System.Data;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Sinks.MSSqlServer;
using TaskManager.API.Data;
using TaskManager.API.Repositories;
using TaskManager.API.Services;

var builder = WebApplication.CreateBuilder(args);

#region Configurar o Serilog para logging
var outputTemplate = "[{Timestamp:dd-MM-yyyy HH:mm:ss}] [{Level}] {Message}{NewLine}{Exception}";

var sinkOptions = new MSSqlServerSinkOptions
{
    TableName = "Logs",
    AutoCreateSqlTable = true,
};

var columnOptions = new ColumnOptions();
columnOptions.Store.Add(StandardColumn.LogEvent);
columnOptions.Store.Remove(StandardColumn.Properties);
columnOptions.LogEvent.DataLength = 4000;
columnOptions.Id.DataType = SqlDbType.BigInt;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console(outputTemplate: outputTemplate)
    .WriteTo.File(
        "logs/log-.txt",
        rollingInterval: RollingInterval.Day,
        outputTemplate: outputTemplate
        )
    .WriteTo.MSSqlServer(
        connectionString: builder.Configuration.GetConnectionString("DefaultConnection"),
        sinkOptions: sinkOptions,
        columnOptions: columnOptions
    )
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Host.UseSerilog();

#endregion

#region Adicionar serviços ao contêiner de injeção de dependência

builder.Services.AddDbContext<TaskManagerDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "MyPolicy",
    policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddControllers();
builder.Services.AddScoped<ITarefaService, TarefaService>();
builder.Services.AddScoped<ITarefaRepository, TarefaRepository>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#endregion


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();
app.UseCors("MyPolicy");
app.UseMiddleware<TaskManager.API.Shared.Middlewares.RequestLoggingMiddleware>();
app.MapControllers();
app.MapGet("/health", () => new { status = "OK" });

app.Run();
