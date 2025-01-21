using SupplierPortal.API.Services.Interfaces;
using SupplierPortal.API.Services;
using SupplierPortal.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens; // Para TokenValidationParameters
using Microsoft.OpenApi.Models; // Para OpenApiSecurityScheme
using System.Text; // Para Encoding.UTF8
using Microsoft.AspNetCore.Authentication.JwtBearer;



var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins", policy =>
    {
        policy.WithOrigins("http://localhost:4200") 
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Configuración de servicios
ConfigureServices(builder.Services, builder.Configuration);

var app = builder.Build();

// Configuración del pipeline de solicitudes HTTP
app.UseCors("AllowSpecificOrigins");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
ConfigureMiddleware(app);

app.Run();

// Métodos auxiliares
void ConfigureServices(IServiceCollection services, IConfiguration configuration)
{
    // Configurar Swagger/OpenAPI
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen(options =>
    {
        // Configuración de seguridad para JWT
        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "Ingrese el token JWT en el formato 'Bearer {token}'."
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
                Array.Empty<string>()
            }
        });
    });

    // Configurar servicios personalizados
    services.AddScoped<IProveedorService, ProveedorService>();
    services.AddScoped<IProductoService, ProductoService>();
    services.AddScoped<ISolicitudCompraService, SolicitudCompraService>();
    services.AddScoped<IUsuarioService, UsuarioService>();

    // Configurar DbContext para Entity Framework
    services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

    // Configurar autenticación JWT
    var jwtKey = configuration["Jwt:Key"];
    var jwtIssuer = configuration["Jwt:Issuer"];
    services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = configuration["Jwt:Issuer"],
            ValidAudience = configuration["Jwt:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
        };
    });

    services.AddAuthorization();

    // Registrar controladores
    services.AddControllers();
}

void ConfigureMiddleware(WebApplication app)
{
    // Habilitar Swagger solo en desarrollo
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    // Habilitar autenticación y autorización
    app.UseAuthentication();
    app.UseAuthorization();

    // Usar controladores en el pipelinea
    app.MapControllers();
}
