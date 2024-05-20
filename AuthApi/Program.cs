using AuthApi.Application;
using AuthApi.Application.RoleAndPermission;
using AuthApi.Common;
using AuthApi.EFcore;
using AuthApi.Permissions;
using AuthApi.PermissionsControl;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddHttpContextAccessor();

// Add services to the container.
builder.Services.AddDbContext<ProContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
builder.Services.AddControllers().AddMvcOptions(op=>op.Filters.Add<SecurityControllerFilter>());
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IPermissionExposer, ManagmentSystemExposer>();
builder.Services.AddTransient<IAuthHelper, AuthHelper>();
builder.Services.AddSingleton<IPasswordHasher, PasswordHasher>();
builder.Services.AddTransient<IUserApplication, UserApplication>();
builder.Services.AddTransient<IRoleApplication, RoleApplication>();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();



//jwt
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,//validate token on server side
            ValidateAudience = false,//validate on client side
            ValidateLifetime = true,//time to expire token
            ValidateIssuerSigningKey = true, //validate key 
            ValidIssuer = "https://localhost:7102", //valid server address 
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("this is my custom Secret key for authentication"))  //the key that token will encrypt by  
        };
    });

builder.Services.AddCors(options =>
{

    options.AddPolicy("EnableCors", policy =>
        {
            policy
            .WithOrigins()
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials()
                .Build();

        });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("EnableCors");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
