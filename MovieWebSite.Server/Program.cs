using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using MovieWebSite.Server.Data;
using MovieWebSite.Server.Repository.IRepository;
using MovieWebSite.Server.Repository;
using Server.Utility.Services;
using Server.Utility.Interfaces;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Server.Model.DTO;
using Server.Model.AuthModels;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<KestrelServerOptions>(options =>
{
    options.Limits.MaxRequestBodySize = int.MaxValue; 
});

builder.Services.Configure<FormOptions>(x =>
{
    x.ValueLengthLimit = int.MaxValue; 
    x.MultipartBodyLengthLimit = int.MaxValue; 
    x.MultipartHeadersLengthLimit = int.MaxValue; 
});

builder.Services.AddControllers();

builder.Services.AddDbContext<ApplicationDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddAuthorization();
builder.Services.AddIdentityApiEndpoints<ApplicationUser>().AddRoles<IdentityRole>().AddEntityFrameworkStores<ApplicationDBContext>().AddDefaultTokenProviders(); 
builder.Services.AddIdentityCore<ApplicationUser>(options => {
    options.SignIn.RequireConfirmedEmail = true;
    options.Password.RequireDigit = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 0;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(3);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;
    options.User.RequireUniqueEmail = true;
    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
});



builder.Services.AddScoped<IUnitOfWork, UnitOfWorks>();
builder.Services.AddTransient<IEmailService, EmailService>();
builder.Services.AddTransient<IBlurhasher, Blurhasher>();
builder.Services.Configure<StripeSettingDTO>(builder.Configuration.GetSection("Stripe"));
builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
    {
        options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey
        });
        options.OperationFilter<SecurityRequirementsOperationFilter>();
    });


var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();
//app.UseAuthentication();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}
app.MapIdentityApi<ApplicationUser>();

app.UseHttpsRedirection();


app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();

