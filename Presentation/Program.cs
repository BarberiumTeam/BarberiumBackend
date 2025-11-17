using Application.Abstraction;
using Application.Service;
using Infrastructure.ExternalServices;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repository;
using Infrastructure.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Polly;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);


#region Service Injections
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IClientRepository, ClientRepository>();

builder.Services.AddScoped<IBarberService, BarberService>();
builder.Services.AddScoped<IBarberRepository, BarberRepository>();

builder.Services.AddScoped<IBarberScheduleService, BarberScheduleService>();
builder.Services.AddScoped<IBarberScheduleRepository, BarberScheduleRepository>();

builder.Services.AddScoped<IScheduleExceptionService, ScheduleExceptionService>();
builder.Services.AddScoped<IScheduleExceptionRepository, ScheduleExceptionRepository>();

builder.Services.AddScoped<ITurnService,TurnService>();
builder.Services.AddScoped<ITurnRepository,TurnRepository>();

builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();

// SERVICE INJECTIONS OF AUTHENTICATION AND AUTHORIZATION
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<ITokenService, TokenService>();

//EXTERNAL SERVICES INJECTIONS
builder.Services.AddScoped<IInfoService, InfoService>();

#endregion

builder.Services.AddDbContext<BarberiumDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// chain AddJsonOptions to AddControllers()
builder.Services.AddControllers()
    .AddJsonOptions(o => o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(setupAction => 
{
    setupAction.AddSecurityDefinition("ApiBearerAuth", new OpenApiSecurityScheme()
    {
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        Description = "Aca pegar el Token generado al loguearse"
    });

    setupAction.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme()
            {
                Reference = new OpenApiReference()
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "ApiBearerAuth"
                }
            },
            new List<string>()
        }
    });
});


// LECTURA DE CLAVE SECRETA
var secret = builder.Configuration["JwtSettings:SecretKey"] ??
             throw new InvalidOperationException("JwtSettings:SecretKey no está configurado en appsettings.json.");
var key = Encoding.ASCII.GetBytes(secret);

//  CONFIGURACIÓN E INYECCIÓN DE JWT BEARER
builder.Services.AddAuthentication(options =>
{
    // Usamos el esquema JWT como predeterminado para autenticar y desafiar
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false; // Usar 'true' en producción
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,      // No validamos quién emite el token
        ValidateAudience = false,    // No validamos para quién es el token
        ClockSkew = TimeSpan.Zero    // No permitimos desfase de tiempo para la expiración
    };
});

// 1. LECTURA DE CONFIGURACIÓN DE POLLY (Simplificada)
var pollyConfig = builder.Configuration
    .GetSection("PollySettings")
    .Get<PollySettings>() ?? throw new InvalidOperationException("PollySettings no encontrado.");

var retryPolicy = ResiliencePolicies.GetRetryPolicy(pollyConfig);
var circuitBreakerPolicy = ResiliencePolicies.GetCircuitBreakerPolicy(pollyConfig);

// 2. REGISTRO Y ACOPLAMIENTO DE HTTPCLIENT Y POLLY
builder.Services.AddHttpClient<Application.Abstraction.IDollarClient, Infrastructure.ExternalServices.DolarClient>(client =>
{
    // Esto leerá "https://dolarapi.com/v1/dolares/blue" del appsettings.json
    client.BaseAddress = new Uri(builder.Configuration["ExternalServices:DollarApi:BaseUrl"]
        ?? throw new InvalidOperationException("ExternalServices:DollarApi:BaseUrl no configurado."));

    client.Timeout = TimeSpan.FromSeconds(30);
})
// Primero el Circuit breaker (para fallos graves), luego el wait and Retry (para fallos transitorios)
.AddPolicyHandler(circuitBreakerPolicy)
.AddPolicyHandler(retryPolicy);

builder.Services.AddAuthorization(); //  Asegura que el servicio de Autorización esté activado

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.UseAuthentication(); //  Identifica quien somos
app.UseAuthorization(); //  Verifica si tenemos permiso

app.MapControllers();

app.Run();
