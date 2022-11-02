global using Microsoft.AspNetCore.Components.Authorization;
global using Blazored.LocalStorage;
using ZiraatBankUzPortal.Client;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using ZiraatBankUzPortal.Shared.DataAccess;
using ZiraatBankUzPortal.Client.Extensions;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<CustomAuthStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(s => s.GetRequiredService<CustomAuthStateProvider>());

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

builder.Services.AddAuthorizationCore();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddOptions();


builder.Services.AddMudServices();
builder.Services.AddSingleton<IOracleDataAccess, OracleDataAccess>();

var host = builder.Build();
await host.SetDefaultCulture(); // Retrieves local storage value and sets the thread's current culture.
await host.RunAsync();
