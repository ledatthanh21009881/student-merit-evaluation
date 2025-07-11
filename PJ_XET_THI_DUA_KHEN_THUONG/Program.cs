using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PJ_XET_THI_DUA_KHEN_THUONG.Data;
using PJ_XET_THI_DUA_KHEN_THUONG.Middlewares;
using PJ_XET_THI_DUA_KHEN_THUONG.Models.Repositories.Implement;

using PJ_XET_THI_DUA_KHEN_THUONG.Models.Repositories.Interfaces;
using PJ_XET_THI_DUA_KHEN_THUONG.Models.Services.Implement;

using PJ_XET_THI_DUA_KHEN_THUONG.Models.Services.Interfaces;
using PJ_XET_THI_DUA_KHEN_THUONG.Repositories;
using PJ_XET_THI_DUA_KHEN_THUONG.Repositories.Implement;
using PJ_XET_THI_DUA_KHEN_THUONG.Services;
using PJ_XET_THI_DUA_KHEN_THUONG.Services.Implement;




//using PJ_XET_THI_DUA_KHEN_THUONG.Helpers;
//using PJ_XET_THI_DUA_KHEN_THUONG.Repositories;
//using PJ_XET_THI_DUA_KHEN_THUONG.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// ===== Cấu hình CORS =====
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins, policy =>
    {
        policy.WithOrigins("http://localhost:3000")  // FE URL
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// ===== Kết nối SQL Server =====
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ===== Inject Repository =====
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<ICriteriaRepository, CriteriaRepostiory>();

//builder.Services.AddScoped<IActivityCustomRepository, ActivityCustomRepository > ();

// ===== Inject Services =====
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ICriteriaService, CriteriaService>();


// ===== Inject Helper =====



// ===== Cấu hình JWT Authentication =====
var jwtKey = builder.Configuration["Jwt:Key"] ?? throw new ArgumentNullException("Jwt:Key not configured");
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
        };
    });

// ===== Add Controller & Swagger =====
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// ===== Middlewares =====
app.UseHttpsRedirection();
app.UseCors(MyAllowSpecificOrigins);
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<ExceptionMiddleware>();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.Run();
