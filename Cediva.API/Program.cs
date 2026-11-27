using System.Text;
using Cediva.API.Constantes;
using Cediva.API.Middleware;
using Cediva.Application;
using Cediva.Infrastructure;
using Cediva.Infrastructure.Persistencia.Inicializador;
using Cediva.Infrastructure.Seguridad;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// 1. Configurar Swagger con soporte JWT Bearer
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc(ConfiguracionApiConstantes.VersionSwagger, new OpenApiInfo
    {
        Title = ConfiguracionApiConstantes.TituloSwagger,
        Version = ConfiguracionApiConstantes.VersionSwagger,
        Description = "API REST de CEDIVA con autenticación basada en JWT y control de acceso por roles (RBAC)."
    });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header utilizando el esquema Bearer. \r\n\r\n" +
                      "Escribe 'Bearer' [espacio] y luego tu token.\r\n\r\n" +
                      "Ejemplo: \"Bearer eyJhbGciOiJIUzI1NiIsIn...\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            Array.Empty<string>()
        }
    });
});

// 2. Inyección de dependencias de las capas de Aplicación e Infraestructura
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

// 3. Configuración de Autenticación JWT Bearer
var jwtSection = builder.Configuration.GetSection(JwtSettings.SectionName);
var secretKey = jwtSection["SecretKey"] ?? throw new InvalidOperationException("No se encontró 'JwtSettings:SecretKey' en la configuración.");
var issuer = jwtSection["Issuer"] ?? "Cediva.API";
var audience = jwtSection["Audience"] ?? "Cediva.Frontend";

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
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
        ValidateIssuer = true,
        ValidIssuer = issuer,
        ValidateAudience = true,
        ValidAudience = audience,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddAuthorization();

// 4. Configurar manejo global de excepciones y ProblemDetails (RFC 7807)
builder.Services.AddExceptionHandler<MiddlewareManejoExcepciones>();
builder.Services.AddProblemDetails();

// 5. Agregar controladores con soporte para serialización de Enums (Texto y Numérico)
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
    });

// 6. Política CORS para Frontend (React / Vite en localhost, red local o túneles móviles)
builder.Services.AddCors(options =>
{
    options.AddPolicy(ConfiguracionApiConstantes.PoliticaCorsReact, policy =>
    {
        policy.SetIsOriginAllowed(_ => true)
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

var app = builder.Build();

// Ejecución del Data Seeder inicial (si la tabla Usuarios está vacía)
await DataSeeder.InicializarAsync(app.Services);

// Middlewares del pipeline
app.UseCors(ConfiguracionApiConstantes.PoliticaCorsReact);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseExceptionHandler();

// Autenticación antes de Autorización
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
