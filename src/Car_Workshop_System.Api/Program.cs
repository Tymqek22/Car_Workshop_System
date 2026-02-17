using Car_Workshop_System.Application.Interfaces;
using Car_Workshop_System.Application.Services;
using Car_Workshop_System.Domain.Entities;
using Car_Workshop_System.Infrastructure.Auth.Identity.DbInitializer;
using Car_Workshop_System.Infrastructure.Auth.Identity.Models;
using Car_Workshop_System.Infrastructure.Auth.Services;
using Car_Workshop_System.Infrastructure.Persistence;
using Car_Workshop_System.Infrastructure.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<ApplicationDbContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("CarWorkshop")));

builder.Services.AddIdentity<ApplicationUser,IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

var jwt = builder.Configuration.GetSection("Jwt");

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwt["Issuer"],
        ValidAudience = jwt["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(jwt["Key"]))
    };
});

builder.Services.AddScoped<JwtService>();
builder.Services.AddScoped<IIdentityService,IdentityService>();
builder.Services.AddScoped<IRepository<WorkOrder>,Repository<WorkOrder>>();
builder.Services.AddScoped<IRepository<TechnicianAssignment>,Repository<TechnicianAssignment>>();
builder.Services.AddScoped<IRepository<Note>,Repository<Note>>();
builder.Services.AddScoped<IWorkOrderService,WorkOrderService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope()) 
{
    var services = scope.ServiceProvider;
    await DbInitializer.InitializeDb(services);
}

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment()) {
        app.MapOpenApi();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/openapi/v1.json","api");
        });
    }

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
