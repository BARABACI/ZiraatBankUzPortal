global using Microsoft.AspNetCore.Components.Authorization;
global using Blazored.LocalStorage;
using ZiraatBankUzPortal.Client;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using ZiraatBankUzPortal.Shared.DataAccess;
using ZiraatBankUzPortal.Client.Extensions;
using ZiraatBankUzPortal.Client.Contracts;
using ZiraatBankUzPortal.Client.Services;
using ZiraatBankUzPortal.Shared.Model;
using MudBlazor;
using Microsoft.Extensions.Options;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Globalization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddSingleton<IOracleDataAccess, OracleDataAccess>();
builder.Services.AddScoped<CustomAuthStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(s => s.GetRequiredService<CustomAuthStateProvider>());
builder.Services.AddScoped<HttpResponseMessage>();
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<MenuModel>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IPageRoleService, PageRoleService>();
builder.Services.AddScoped<IEmployeePhoneBookService, EmployeePhoneBookService>();
builder.Services.AddScoped<IExcelService, ExcelService>();
builder.Services.AddScoped<IInternalExportExcelService, InternalExportExcelService>();

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");


builder.Services.AddAuthorizationCore();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddOptions();


builder.Services.AddMudServices(config =>
{
    config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomRight;
});

var host = builder.Build();
await host.SetDefaultCulture(); // Retrieves local storage value and sets the thread's current culture.
await host.RunAsync();
