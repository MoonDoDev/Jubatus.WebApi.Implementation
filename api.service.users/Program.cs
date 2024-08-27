using Api.Service.Users;
using Api.Service.Users.Models;
using Jubatus.WebApi.Extensions;

var builder = WebApplication.CreateBuilder( args );

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

var webApiMgr = new WebApiConfig( builder )
    .AddMongoDbExtensions<UsersEntity>()
    .AddBearerJwtExtensions()
    .AddFixedRateLimiter()
    .AddUrlAndHeaderApiVersioning();

var app = webApiMgr.BuildWebApp( ApiEndPoints.HealthCheckUsersLive, ApiEndPoints.HealthCheckUsersReady );

// Configure the HTTP request pipeline.
if( app.Environment.IsDevelopment() )
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseHttpsRedirection();
}

await app.RunAsync().ConfigureAwait( false );
