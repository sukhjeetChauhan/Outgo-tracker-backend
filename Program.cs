using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using Outgo_tracker_Backend.Data;

using System.Text.Json.Serialization; // Import required namespace

var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsDevelopment())
{
  // Load environment variables from .env file
  DotNetEnv.Env.Load();
}


// Add environment variables to configuration
builder.Configuration["ConnectionStrings:DefaultConnection"] = Environment.GetEnvironmentVariable("DefaultConnection");
builder.Configuration["AzureAdB2C:Instance"] = Environment.GetEnvironmentVariable("AzureAdB2C__Instance");
builder.Configuration["AzureAdB2C:ClientId"] = Environment.GetEnvironmentVariable("AzureAdB2C__ClientId");
builder.Configuration["AzureAdB2C:Domain"] = Environment.GetEnvironmentVariable("AzureAdB2C__Domain");
builder.Configuration["AzureAdB2C:TenantId"] = Environment.GetEnvironmentVariable("AzureAdB2C__TenantId");
builder.Configuration["AzureAdB2C:SignUpSignInPolicyId"] = Environment.GetEnvironmentVariable("AzureAdB2C__SignUpSignInPolicyId");
builder.Configuration["AzureAdB2C:ResetPasswordPolicyId"] = Environment.GetEnvironmentVariable("AzureAdB2C__ResetPasswordPolicyId");
builder.Configuration["AzureAdB2C:Audience"] = Environment.GetEnvironmentVariable("AzureAdB2C__Audience");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
  options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Load Azure AD B2C config
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAdB2C"));

builder.Services.AddAuthorization();

// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(options =>
{
  // Convert all enums to string representation in JSON
  options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add CORS policy
builder.Services.AddCors(options =>
{
  options.AddPolicy("AllowReactFrontend", policy =>
  {
    policy.WithOrigins("https://outgotracker.app", "https://www.outgotracker.app", "http://localhost:5173") // Allow frontend origin
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
  });
});

builder.Services.Configure<CookiePolicyOptions>(options =>
{
  options.MinimumSameSitePolicy = SameSiteMode.None; // ✅ Required for cross-origin cookies
  options.Secure = CookieSecurePolicy.Always; // ✅ Required for HTTPS
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseCors("AllowReactFrontend"); // Apply CORS policy
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
