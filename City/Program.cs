using City.Services;
using Microsoft.AspNetCore.StaticFiles;
using Serilog;

//configure serilog - 3rd party logger
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.File($"Logs/City.txt", rollingInterval : RollingInterval.Day)
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

//Configur
// Add services to the container.

//builder.Services.AddControllers();     //defualt one
builder.Services.AddControllers(options =>
    {
        options.ReturnHttpNotAcceptable = true;         //this doesnt return response if accept headers are other than default/json
    }).AddNewtonsoftJson()
    .AddXmlDataContractSerializerFormatters();        //Supports XML to API

//thiw will clear all default logging providers 
//builder.Logging.ClearProviders();
//builder.Logging.AddConsole();   

//3rd party logger
builder.Host.UseSerilog();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//adding service to support multiple files in FileController
builder.Services.AddSingleton<FileExtensionContentTypeProvider>();

//Registering Local Mail Service -- Concrete Implementation providing class directly
//builder.Services.AddTransient<LocalMailService>();

//Registering Local Mail Service -- Using Interface
#if DEBUG
builder.Services.AddTransient<ILocalMailService, LocalMailService>();
#else
builder.Services.AddTransient<ILocalMailService, CloudMailService>();
#endif

//Local Data Storage Class can be registered using Singleton Lifetime 

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
    endpoints.MapControllers()
);

//app.MapControllers();



//Prints Hello World -- W/O Controllers
//app.Run(async (context) =>
//{
//    await context.Response.WriteAsync("Hello World!");
//});

app.Run();
