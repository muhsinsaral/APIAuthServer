using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using MiniApp1.API.Requirements;
using SharedLibrary.Configurations;
using SharedLibrary.Extensions;
using SharedLibrary.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<CustomTokenOption>(builder.Configuration.GetSection("TokenOption"));
var tokenOptions = builder.Configuration.GetSection("TokenOption").Get<CustomTokenOption>() ?? throw new Exception("TokenOption can not be null");

builder.Services.AddCustomTokenAuth(tokenOptions);

builder.Services.AddSingleton<IAuthorizationHandler,BirthDayRequirementHandler>();
builder.Services.AddAuthorization(opt =>
{
    opt.AddPolicy("BursaPolicy", policy =>
    {
        policy.RequireClaim("city", "bursa");
    });
    opt.AddPolicy("AgePolicy", policy =>
    {
        policy.Requirements.Add(new BirthDayRequirement(18));
    });

});


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
