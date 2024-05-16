using AuthAspRazorPages.Application;
using AuthAspRazorPages.Application.RoleAndPermission;
using AuthAspRazorPages.Common;
using AuthAspRazorPages.EFcore;
using AuthAspRazorPages.Permissions;
using AuthAspRazorPages.PermissionsControl;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddHttpContextAccessor();

builder.Services.AddDbContext<ProContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddTransient<IPermissionExposer, ManagmentSystemExposer>();
builder.Services.AddTransient<IAuthHelper, AuthHelper>();
builder.Services.AddSingleton<IPasswordHasher, PasswordHasher>();
builder.Services.AddTransient<IUserApplication, UserApplication>();
builder.Services.AddTransient<IRoleApplication, RoleApplication>();
builder.Services.AddTransient<IFileUploader, FileUploader>();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();




builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = SameSiteMode.Lax;
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
               .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, o =>
               {
                   o.LoginPath = new PathString("/Login");
                   o.LogoutPath = new PathString("/Login");
                   o.AccessDeniedPath = new PathString("/AccessDenied");
               });


builder.Services.AddAuthorization
    (options =>
    {
        //options.AddPolicy("AdminArea", builder => builder.RequireRole(Roles.Administrator));

        //options.AddPolicy("Login", builder => builder.RequireRole());

    });

builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizeAreaFolder("Administration","/Login","Login");
}).AddMvcOptions(op=>op.Filters.Add<SecurityPageFilter>());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.MapRazorPages();

app.Run();
