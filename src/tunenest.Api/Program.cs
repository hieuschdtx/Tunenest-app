using Serilog;
using tunenest.Api;
using tunenest.Api.Middlewares;
using tunenest.Infrastructure.Configurations;

var builder = WebApplication.CreateBuilder(args);

var appSettings = new AppSetting();
var connectionString = builder.Configuration.GetConnectionString("Tunnest_DbConnection");

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();

//Serilog
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Host.UseSerilog();

//Register services
builder.Services
    .AddInfrastructure()
    .AddApplication(builder.Configuration, connectionString)
    .AddAuthenticationAndAuthorization(appSettings);

//Add Cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        cors =>
        {
            cors
                .WithOrigins("http://localhost:5500/")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(_ => true)
                .AllowCredentials();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseCors("CorsPolicy");

app.UseStaticFiles();

app.UseAuthentication();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
