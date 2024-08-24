using Api.Gateway.Users;
using Jubatus.WebApi.Extensions;

var builder = WebApplication.CreateBuilder( args );

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

var webApiMgr = new WebApiConfig( builder )
    .AddBearerJwtExtensions()
    .AddFixedRateLimiter();

if( builder.Environment.IsDevelopment() )
{
    var config = new ConfigurationBuilder().AddUserSecrets<Program>().Build();

    var log = Toolbox.GetLogger( LoggerMinLevel.Debug );
    log.Debug( "Params(DEV): {0}", config.GetValue<string>( "ReverseProxy:Clusters:UsersCluster:Destinations:Destination1:Address" ) );

    builder.Services.AddReverseProxy().LoadFromConfig( config.GetSection( ApiConsts.ReverseProxy ) );
}
else
{
    builder.Configuration.AddEnvironmentVariables();

    var log = Toolbox.GetLogger( LoggerMinLevel.Debug );
    log.Debug( "Params(PRO): {0}", builder.Configuration.GetValue<string>( "ReverseProxy:Clusters:UsersCluster:Destinations:Destination1:Address" ) );

    builder.Services.AddReverseProxy().LoadFromConfig( builder.Configuration.GetSection( ApiConsts.ReverseProxy ) );
}

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
