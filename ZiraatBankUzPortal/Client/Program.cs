global using Microsoft.AspNetCore.Components.Authorization;
global using Blazored.LocalStorage;
using ZiraatBankUzPortal.Client;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using ZiraatBankUzPortal.Shared.DataAccess;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped<CustomAuthStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(s => s.GetRequiredService<CustomAuthStateProvider>());

builder.Services.AddAuthorizationCore();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddOptions();


builder.Services.AddMudServices();
builder.Services.AddSingleton<IOracleDataAccess, OracleDataAccess>();


//builder.Services.AddHttpClient();

await builder.Build().RunAsync();
