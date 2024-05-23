using System.Text;
using Application.Abstractions;
using Application.Services;
using CandidateJourney.Application;
using CandidateJourney.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

builder.Services.AddHttpContextAccessor();

builder.Services.AddPooledDbContextFactory<CandidateJourneyDbContext>(options =>
{
    options.UseSqlServer(config.GetConnectionString("DefaultConnection"));
});

// Services GraphQl
builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<IInterestService, InterestService>();
builder.Services.AddScoped<ILocationService, LocationService>();
builder.Services
    .AddGraphQLServer()
    .ModifyOptions(o =>
    {
        o.EnableDefer = true;
    })
    .RegisterDbContext<CandidateJourneyDbContext>(DbContextKind.Pooled)
    .RegisterDbContext<CandidateJourneyDbContext>()
    .AddAPITypes()
    .AddFiltering()
    .AddSorting()
    .AddMaxExecutionDepthRule(15);

// Services Rest
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Cors and Auth
builder.Services.AddCors(options =>
    options.AddDefaultPolicy(policyBuilder =>
        policyBuilder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            RequireExpirationTime = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.GetValue<string>("Authentication:SecurityKey"))),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "CandidateJourney",
            ValidAudience = "CandidateJourney"
        };
        options.RequireHttpsMetadata = false;
    });

builder.Services.AddAuthorization();

builder.Services.AddCandidateJourneyApplication(builder.Configuration);

var app = builder.Build();
/*
app.Use(async (context, next) =>
{
    if (context.Request.Path == "/index.html")
    {
        context.Response.Redirect("/graphql");
    }
    else
    {
        await next();
    }
});*/

// Mapping
app.MapGraphQL();
app.MapControllers();

// Middleware
app.UseHsts();
app.UseHttpsRedirection();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

// Swagger UI config
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
    options.RoutePrefix = string.Empty;
});

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var seeder = services.GetRequiredService<DatabaseSeeder>();
    seeder.Seed();
}

app.MapFallback(context =>
{
    context.Response.Redirect("/graphql");
    return Task.CompletedTask;
});

app.Run();
