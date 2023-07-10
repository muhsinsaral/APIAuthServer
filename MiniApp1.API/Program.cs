using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using SharedLibrary.Configurations;
using SharedLibrary.Extensions;
using SharedLibrary.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<CustomTokenOption>(builder.Configuration.GetSection("TokenOption"));
var tokenOptions = builder.Configuration.GetSection("TokenOption").Get<CustomTokenOption>() ?? throw new Exception("TokenOption can not be null");

builder.Services.AddCustomTokenAuth(tokenOptions);

















//builder.Services.AddIdentity<UserApp, IdentityRole>(options =>
//{
//    options.User.RequireUniqueEmail = true;
//    options.Password.RequireNonAlphanumeric = false;
//}).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
////DI Register
//builder.Services.Configure<CustomTokenOption>(builder.Configuration.GetSection("TokenOption"));
//builder.Services.Configure<List<Client>>(builder.Configuration.GetSection("Clients"));

//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

//}).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
//{
//    var tokenOptions = builder.Configuration.GetSection("TokenOption").Get<CustomTokenOption>() ?? throw new Exception("TokenOption can not be null");
//    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
//    {
//        ValidIssuer = tokenOptions.Issuer,
//        ValidAudience = tokenOptions.Audience[0],
//        IssuerSigningKey = SignService.GetSymmetricSecurityKey(tokenOptions.SecurityKey),

//        ValidateIssuerSigningKey = true,
//        ValidateAudience = true,
//        ValidateIssuer = true,
//        ValidateLifetime = true,
//        ClockSkew = TimeSpan.Zero // => Hata payýný sýfýrladýk // Bunu kullanmasaydýk token ömür süresine 5 dk hata payý ekleyecekti.
//    };
//});

















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

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.Run();
