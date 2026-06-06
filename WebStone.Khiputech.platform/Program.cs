using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using WebStone.Khiputech.Platform.Analytics.Application.QueryServices;
using WebStone.Khiputech.Platform.Analytics.Application.Internal.QueryServices;
using WebStone.Khiputech.Platform.Analytics.Domain.Repositories;
using WebStone.Khiputech.Platform.Analytics.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using WebStone.Khiputech.Platform.Iam.Application.CommandServices;
using WebStone.Khiputech.Platform.Iam.Application.Internal.CommandServices;
using WebStone.Khiputech.Platform.Iam.Application.Internal.OutboundServices;
using WebStone.Khiputech.Platform.Iam.Application.Internal.QueryServices;
using WebStone.Khiputech.Platform.Iam.Application.QueryServices;
using WebStone.Khiputech.Platform.Iam.Domain.Repositories;
using WebStone.Khiputech.Platform.Iam.Infrastructure.Hashing.BCrypt.Services;
using WebStone.Khiputech.Platform.Iam.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using WebStone.Khiputech.Platform.Iam.Infrastructure.Pipeline.Middleware.Extensions;
using WebStone.Khiputech.Platform.Iam.Infrastructure.Tokens.Jwt.Configuration;
using WebStone.Khiputech.Platform.Iam.Infrastructure.Tokens.Jwt.Services;
using WebStone.Khiputech.Platform.Iam.Resources;
using WebStone.Khiputech.Platform.Resources.Errors;
using WebStone.Khiputech.Platform.Shared.Domain.Repositories;
using WebStone.Khiputech.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using WebStone.Khiputech.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using WebStone.Khiputech.Platform.Shared.Infrastructure.Pipeline.Middleware.Extensions;
using WebStone.Khiputech.Platform.Shared.Interfaces.Rest.ProblemDetails;

var builder = WebApplication.CreateBuilder(args);

// 1. Configuración de servicios -------------------------------------------------

builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddControllers(options =>
    {
        // Opcional: transformar nombres de controladores a kebab-case (ej. "analytics" en lugar de "Analytics")
        options.Conventions.Add(new RouteTokenTransformerConvention(new SlugifyParameterTransformer()));
    })
    .AddDataAnnotationsLocalization();

// 2. DbContext MySQL
builder.Services.AddDbContext<AppDbContext>((serviceProvider, options) =>
{
    var connectionStringTemplate = builder.Configuration.GetConnectionString("DefaultConnection");
    if (string.IsNullOrWhiteSpace(connectionStringTemplate))
        throw new InvalidOperationException("Database connection string is missing.");
    
    var connectionString = Environment.ExpandEnvironmentVariables(connectionStringTemplate);
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
        .UseLoggerFactory(serviceProvider.GetRequiredService<ILoggerFactory>())
        .EnableDetailedErrors();
        
    if (builder.Environment.IsDevelopment())
        options.EnableSensitiveDataLogging();
});

// 3. CORS (permite peticiones desde el frontend)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllPolicy",
        policy => policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

// 4. Autenticación JWT
var tokenSettings = builder.Configuration.GetSection("TokenSettings").Get<TokenSettings>();
if (tokenSettings is null || string.IsNullOrWhiteSpace(tokenSettings.Secret))
    throw new InvalidOperationException("TokenSettings: Secret is not configured.");
var key = Encoding.ASCII.GetBytes(tokenSettings.Secret);

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false,
            ClockSkew = TimeSpan.Zero
        };
    });

// 5. Localización (recursos .resx)
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.AddSingleton<IStringLocalizer<ErrorMessages>, StringLocalizer<ErrorMessages>>();
builder.Services.AddSingleton<IStringLocalizer<IamMessages>, StringLocalizer<IamMessages>>();

// 6. Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "WebStone.Khiputech.Platform API",
        Version = "v1",
        Description = "API para la gestión de museos (bounded contexts)"
    });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Ingrese el token JWT con el prefijo 'Bearer '",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new List<string>()
        }
    });
    options.EnableAnnotations();
});

// 7. Inyección de dependencias (Shared, Iam, Analytics) -------------------------

// Shared
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ProblemDetailsFactory>();

// Iam
builder.Services.Configure<TokenSettings>(builder.Configuration.GetSection("TokenSettings"));
builder.Services.AddScoped<IHashingService, HashingService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserCommandService, UserCommandService>();
builder.Services.AddScoped<IUserQueryService, UserQueryService>();

// Analytics (con repositorios mock, los comentamos hasta que tengamos implementación real)
builder.Services.AddScoped<IAnalyticsQueryService, AnalyticsQueryService>();
// builder.Services.AddScoped<IArtworkStatRepository, ArtworkStatRepository>();
// builder.Services.AddScoped<IVisitorStatRepository, VisitorStatRepository>();
// builder.Services.AddScoped<ISponsorRepository, SponsorRepository>();

// -----------------------------------------------------------------------------

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseGlobalExceptionHandler();   // Asegúrate de tener implementado este middleware (ver más abajo)
app.UseRequestAuthorization();     // Middleware personalizado de Iam
app.UseCors("AllowAllPolicy");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate();
}

app.Run();

public class SlugifyParameterTransformer : IOutboundParameterTransformer
{
    public string? TransformOutbound(object? value)
    {
        if (value == null) return null;
        return value.ToString()!.Replace("Controller", "").ToLowerInvariant();
    }
}