using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using Microsoft.AspNetCore.Mvc;
using MISA.FW0922GD.QLTH.BL.BaseBL;
using MISA.FW0922GD.QLTH.BL.EmployeeBL;
using MISA.FW0922GD.QLTH.DL;
using MISA.FW0922GD.QLTH.DL.BaseDL;
using MISA.FW0922GD.QLTH.DL.EmployeeDL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Add CORS Policy (By: KhaiND - 20/22/2022)
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "myAllowSpecificOrigins",
                      policy =>
                      {
                          policy.AllowAnyOrigin()
                                .AllowAnyHeader()
                                .AllowAnyMethod();
                      });
});

// Turn off Automatic Model State Validation (By: KhaiND - 26/11/2022)
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Dependency Injection
builder.Services.AddScoped(typeof(IBaseDL<>), typeof(BaseDL<>));
builder.Services.AddScoped(typeof(IBaseBL<>), typeof(BaseBL<>));
builder.Services.AddScoped<IEmployeeDL, EmployeeDL>();
builder.Services.AddScoped<IEmployeeBL, EmployeeBL>();

// Lấy dữ liệu ConnectionString từ file appsettings.Development.json
DatabaseContext.ConnectionString = builder.Configuration.GetConnectionString("MySql");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Aply CORS Midlewares
app.UseCors("myAllowSpecificOrigins");

app.UseAuthorization();

app.MapControllers();

app.Run();
