using Authentication.Blazor.Client;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);


//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
//builder.Services.AddScoped<AuthenticationStateProvider, JwtAuthenticationStateProvider>();
//builder.Services.AddAuthorizationCore();

builder.Services.AddMudServices();



await builder.Build().RunAsync();
