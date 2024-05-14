using System.Text;
using Application.Abstractions;
using Application.Services;
using CandidateJourney.Application;
using CandidateJourney.Application.Services;
using CandidateJourney.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

// Services GraphQl
builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<IInterestService, InterestService>();
builder.Services
    .AddGraphQLServer()
    .AddAPITypes()
    .AddFiltering()
    .AddSorting();

// Services Rest
builder.Services.AddControllers();
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
});

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

app.MapFallback(context =>
{
    context.Response.Redirect("/graphql");
    return Task.CompletedTask;
});

app.Run();