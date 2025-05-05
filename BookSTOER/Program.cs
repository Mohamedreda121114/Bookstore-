using System.Text;
using System.Text.Json.Serialization;
using BookSTOER.Data;
using BookSTOER.Model;
using BookSTOER.NewFolder.repos;
using BookSTOER.repos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Controllers with JSON config
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

// DbContext
builder.Services.AddDbContext<dbcontext>(options =>
    options.UseSqlServer("Server=.;Database=STORE;Trusted_Connection=True;Encrypt=True;TrustServerCertificate=True;"));
builder.Services.AddScoped<BookRepos>();
builder.Services.AddScoped<Ibookrepos, BookRepos>();
builder.Services.AddScoped<LoginRepose>();
builder.Services.AddScoped<ILoginRepos, LoginRepose>();
builder.Services.AddScoped<RigstrationRepos>();
builder.Services.AddScoped<IRigstrationRepos, RigstrationRepos>();
builder.Services.AddScoped<OrderRepos>();
builder.Services.AddScoped<IOrderRepos, OrderRepos>();
builder.Services.AddScoped<UserRepos>();
builder.Services.AddScoped<IuserRepos, UserRepos>();
// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// JWT Configuration
builder.Services.Configure<token>(builder.Configuration.GetSection("Jwt"));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var jwtSettings = builder.Configuration.GetSection("Jwt").Get<token>();

        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = jwtSettings.Issuer,

            ValidateAudience = true,
            ValidAudience = jwtSettings.Aidience,

            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.signkey)),

            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddAuthorization();

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy => policy
            .WithOrigins("http://localhost:5173")
            .AllowAnyMethod()
            .AllowAnyHeader());
});

var app = builder.Build();

// Use CORS
app.UseCors("AllowFrontend");

// Handle OPTIONS requests
app.Use(async (context, next) =>
{
    if (context.Request.Method == HttpMethods.Options)
    {
        context.Response.StatusCode = 200;
        await context.Response.CompleteAsync();
    }
    else
    {
        await next();
    }
});

// Swagger in development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();
