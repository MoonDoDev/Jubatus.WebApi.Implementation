using System.Text;
using System.Threading.RateLimiting;
using Api.Gateway.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition(ApiConsts.SecurityDefinitionName, new OpenApiSecurityScheme
    {
        Description = @"JMT Authorization header using the Bearer scheme. \r\n\r\n
            Enter 'Bearer' [space] and then your token in the text input below. \r\n\r\n
            Example: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = ApiConsts.SecurityDefinitionName
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = ApiConsts.SecurityDefinitionName
                },
                Scheme = "oauth2",
                Name = ApiConsts.SecurityDefinitionName,
                In = ParameterLocation.Header,
            },
            new List<string>()
        }
    });
});

// Getting JwtSettings from appsettigs.json
IConfigurationSection settings = builder.Configuration.GetSection(ApiConsts.JwtSettings);

// Adding JWT
builder.Services.AddAuthentication(auth =>
{
    auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    byte[] Key = Encoding.UTF8.GetBytes(settings.GetValue<string>(ApiConsts.JwtKey)!);

    o.SaveToken = true;
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = settings.GetValue<bool>(ApiConsts.ValidateIssuer),
        ValidateAudience = settings.GetValue<bool>(ApiConsts.ValidateAudience),
        ValidateLifetime = settings.GetValue<bool>(ApiConsts.ValidateLifetime),
        ValidateIssuerSigningKey = settings.GetValue<bool>(ApiConsts.ValidateIssuerSigningKey),
        ValidIssuer = settings.GetValue<string>(ApiConsts.JwtIssuer),
        ValidAudience = settings.GetValue<string>(ApiConsts.JwtAudience),
        IssuerSigningKey = new SymmetricSecurityKey(Key)
    };
});

/// Requests Rate Limiter by Stefan Djokic → Email URL: https://mail.google.com/mail/u/0/?ogbl#label/Newsletter%2FStefan+Djokic/FMfcgzGslkkkQrPwRgfrxvcJprjmXrQg
builder.Services.AddRateLimiter(rateLimiterOptions =>
    rateLimiterOptions.AddFixedWindowLimiter(policyName: "fixed", options =>
    {
        options.PermitLimit = 10;                                           // A maximum of 10 requests
        options.Window = TimeSpan.FromSeconds(5);                           // Per 5 seconds window.
        options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;    // Behaviour when not enough resources can be leased (Process oldest requests first).
        options.QueueLimit = 2;                                             // Maximum cumulative permit count of queued acquisition requests.
    })
);

/* Con la línea "options.SuppressAsyncSuffixInActionNames = false;" le estamos diciendo al compilador que No ...
   ... elimine el sufijo "Async" de los nombres de los métodos, ejemplo cuando usamos: nameof(MethodNameAsync) */
builder.Services.AddControllers(options => options.SuppressAsyncSuffixInActionNames = false);

/// Adicionamos el HealthCheck del Servicio
builder.Services.AddHealthChecks();

/// Adicionamos el Servicio de Reverse Proxy Server (API Gateway)
builder.Services.AddReverseProxy().LoadFromConfig(builder.Configuration.GetSection(ApiConsts.ReverseProxy));

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseHttpsRedirection();
}

/// Adicionamos el HealthCheck para validar el estado del Servicio
app.MapHealthChecks(ApiEndPoints.HealthCheckGatewayLive, new HealthCheckOptions
{
    Predicate = (_) => false
});

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

/* Requests Rate Limiter by Stefan Djokic */
app.UseRateLimiter();
app.MapDefaultControllerRoute().RequireRateLimiting(ApiConsts.RateLimiterPolicyName);
app.MapReverseProxy();

await app.RunAsync().ConfigureAwait(false);
