using System.Text;
using CandidateJourney.API.GraphQlTypes;
using CandidateJourney.API.Queries;
using CandidateJourney.Application;
using CandidateJourney.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

builder.Services
    .AddGraphQLServer()
    .AddAPITypes();

builder.Services.AddCors(options => options.AddDefaultPolicy(policyBuilder => policyBuilder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
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

app.MapGraphQL();

app.UseHsts();
app.UseHttpsRedirection();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

app.MapFallback(context =>
{
    context.Response.Redirect("/graphql");
    return Task.CompletedTask;
});

app.Run();