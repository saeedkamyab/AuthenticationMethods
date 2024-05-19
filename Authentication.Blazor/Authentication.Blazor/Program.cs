using Authentication.Blazor;
using Authentication.Blazor.Client.Pages;
using Authentication.Blazor.Components;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.IdentityModel.Tokens;
using MudBlazor.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();



builder.Services.AddMudServices();



//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//})
//   .AddJwtBearer(options =>
//   {
//       options.TokenValidationParameters = new TokenValidationParameters
//       {
//           ValidateIssuer = true,
//           ValidateAudience = true,
//           ValidateLifetime = true,
//           ValidateIssuerSigningKey = true,
//           ValidIssuer = "https://localhost:7102",
          
//           IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("this is my custom Secret key for authentication"))
//       };
//   });

//builder.Services.AddAuthorization();
//builder.Services.AddRazorPages();
//builder.Services.AddServerSideBlazor();
//builder.Services.AddScoped<AuthenticationStateProvider, JwtAuthenticationStateProvider>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

//app.UseAuthentication();


app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(Authentication.Blazor.Client._Imports).Assembly);

app.Run();
