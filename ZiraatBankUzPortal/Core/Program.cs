using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Configuration;
using System.Text;
using ZiraatBankUzPortal.Core;
using ZiraatBankUzPortal.Core.Services;
using ZiraatBankUzPortal.Shared.DataAccess;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("coreappsettings.json");
// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IOracleDataAccess, OracleDataAccess>();
builder.Services.AddSingleton<IEmployeeDataService, EmployeeDataService>();
builder.Services.AddSingleton<IMenuDataService, MenuDataService>();
builder.Services.AddSingleton<IEmployeePhoneBookService, EmployeePhoneBookService>();
builder.Services.AddSingleton<IInternalExportExcelService, InternalExportExcelService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "ZiraatBankUz Mis API V1");
});




app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapRazorPages();
    endpoints.MapControllers();
});

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
