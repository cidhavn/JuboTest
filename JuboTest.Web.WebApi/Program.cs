using JuboTest;
using JuboTest.Repository.Jubo;
using JuboTest.Service.Management;
using JuboTest.Web.WebApi;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

var appSetting = builder.Configuration.GetSection("AppSetting").Get<AppSetting>();

// Add services to the container.

builder.Services.AddCors();
builder
    .Services
    .AddControllersWithViews()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonDateTimeOffsetConverter.Default());
        options.JsonSerializerOptions.Converters.Add(new JsonDateTimeOffsetConverter.Nullable());
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "JuboTest API", Version = "1.0.0", });

    option.IncludeXmlComments(Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, "Api.xml"));
});

builder.Services.AddBase();
builder.Services.AddJuboRepository(() => builder.Configuration.GetSection("JuboDB").Get<JuboRepositorySetting>());
builder.Services.AddManagementService();

var app = builder.Build();

// Configure the HTTP request pipeline.

//app.UseHttpsRedirection();

app.UseCors(builder =>
{
    // 當 header 設定為 'Access-Control-Allow-Credentials: true' 時，不可將 Access-Control-Allow-Origin 設為 wildcard (*)
    if (appSetting.AllowCorsDomains.IsNullOrEmpty() == false) builder.WithOrigins(appSetting.AllowCorsDomains);

    builder.AllowCredentials();
    builder.AllowAnyHeader();
    builder.AllowAnyMethod();
});

app.UseAuthorization();

app.MapControllers();

app.UseSwagger();
app.UseSwaggerUI(option =>
{
    option.SwaggerEndpoint("/swagger/v1/swagger.json", "RESTful API v1.0.0");
    option.DefaultModelsExpandDepth(-1);
});

app.Run();