using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using SkinCareHelper.DAL.DbContexts;
using SkinCareHelper.DAL;
using SkinCareHelper.DAL.Abstractions;
using SkinCareHelper.DAL.Repositories;
using SkinCareHelper.BLL;
using SkinCareHelper.BLL.DTOs;
using SkinCareHelper.BLL.Services;
using SkinCareHelper.BLL.Abstractions;
using SkinCareHelper.ViewModels.Products;
using Microsoft.AspNetCore.Identity;
using SkinCareHelper.DAL.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using FluentValidation;
using SkinCareHelper.BLL.Validators;
using BLL.Abstractions;
using SkincareHelper.BLL.Services;
using SkinCareHelper.Services.Abstractions;
using SkinCareHelper.Services;
using Microsoft.OpenApi.Models;
using System.Reflection;
using SkinCareHelper.DAL.Photos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
IConfiguration appConfig = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

builder.Services.AddDbContext<DataContextEF>(options =>
    options.UseSqlServer(
                    appConfig.GetConnectionString("DefaultConnection")));

builder.Services.AddAutoMapper(typeof(SkinCareHelper.DAL.Mapping.MappingProfiles).Assembly);

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IBanRepository, BanRepository>();
builder.Services.AddScoped<ISkincareRoutineRepository, SkincareRoutineRepository>();
builder.Services.AddScoped<ISensorDataRepository, SensorDataRepository>();

builder.Services.AddAutoMapper(typeof(SkinCareHelper.BLL.Mapping.MappingProfiles).Assembly);

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IBanService, BanService>();
builder.Services.AddScoped<ISkincareRoutineService, SkincareRoutineService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ISensorDataService, SensorDataService>();

builder.Services.AddScoped<IUserAccessor, UserAccessor>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IPhotoAccessor, PhotoAccessor>();

builder.Services.AddScoped<IValidator<UserDto>, UserValidator>();
builder.Services.AddScoped<IValidator<SkincareRoutineDto>, SkincareRoutineValidator>();
builder.Services.AddScoped<IValidator<ProductDto>, ProductValidator>();

builder.Services.AddIdentity<User, IdentityRole>()
    .AddDefaultTokenProviders()
    .AddEntityFrameworkStores<DataContextEF>();

builder.Services.AddAuthentication(auth =>
{
    auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidateAudience = true,
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            var accessToken = context.Request.Query["access_token"];
            var path = context.HttpContext.Request.Path;

            if (!string.IsNullOrEmpty(accessToken) && (path.StartsWithSegments("/chat")))
            {
                context.Token = accessToken;
            }

            return Task.CompletedTask;
        }
    };
});

builder.Services.Configure<CloudinarySettings>(appConfig.GetSection("Cloudinary"));

builder.Services.AddAutoMapper(typeof(SkinCareHelper.Mapping.MappingProfiles).Assembly);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Skincare API", Version = "v1" });

    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

builder.Services.AddCors((options) =>
{
    options.AddPolicy("DevCors", (corsBuilder) =>
    {
        corsBuilder.WithOrigins("http://localhost:4200", "http://localhost:3000", "http://localhost:8000")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
    });
    options.AddPolicy("ProdCors", (corsBuilder) =>
    {
        corsBuilder.WithOrigins("https://mySkinCareHelper.com")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseCors("DevCors");
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseCors("DevCors");
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseHttpsRedirection();
}

app.UseAuthorization();

app.MapControllers();

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;

try
{
    var context = services.GetRequiredService<DataContextEF>();
    var userManager = services.GetRequiredService<UserManager<User>>();
    await context.Database.MigrateAsync();
    await Seed.SeedData(context, userManager);
}
catch (Exception ex)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occured during migration.");
}

app.Run();
