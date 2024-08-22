using Api.Gateway.Users;
using Jubatus.WebApi.Extensions;

var builder = WebApplication.CreateBuilder( args );

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

var webApiMgr = new WebApiConfig( builder )
    .AddBearerJwtExtensions()
    .AddFixedRateLimiter();

/// Adicionamos el Servicio de Reverse Proxy Server (API Gateway)
builder.Services.AddReverseProxy().LoadFromConfig( builder.Configuration.GetSection( ApiConsts.ReverseProxy ) );

var app = webApiMgr.BuildWebApp( ApiEndPoints.HealthCheckGatewayLive );

// Configure the HTTP request pipeline.
if( app.Environment.IsDevelopment() )
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseHttpsRedirection();
}

app.MapReverseProxy();

await app.RunAsync().ConfigureAwait( false );
