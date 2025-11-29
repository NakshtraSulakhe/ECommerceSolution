using ECommerce.Domain.Interfaces;
using ECommerce.Infrastructure.Data;
using ECommerce.Infrastructure.Repositories;
using ECommerce.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer; // Add this using directive
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ECommerce.API.Helpers;


{
    var builder = WebApplication.CreateBuilder(args);

    // Add DB Connection
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

    // Add Services
    builder.Services.AddScoped<IProductRepository, ProductRepository>();
    builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
    builder.Services.AddScoped<IAuthService, AuthService>();
    builder.Services.AddScoped<IUserRepository, UserRepository>();
    builder.Services.AddScoped<IUserRoleRepository, UserRoleRepository>();
    builder.Services.AddScoped<IRoleRepository, RoleRepository>();
    builder.Services.AddScoped<ICartRepository, CartRepository>();
    builder.Services.AddScoped<IOrderRepository, OrderRepository>();
    builder.Services.AddScoped<ImageHelper>();
    builder.Services.AddScoped<ICMSPageRepository, CMSPageRepository>();
    builder.Services.AddScoped<IBannerRepository, BannerRepository>();





    // Add controllers
    builder.Services.AddControllers();
    
    // Swagger
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();


    // JWT Authentication

    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = builder.Configuration["Jwt:Issuer"],
                ValidAudience = builder.Configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
            };
        });

    var app = builder.Build();

    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseStaticFiles();

    app.UseAuthentication();
    app.UseAuthorization();

    app.UseHttpsRedirection();

    app.MapControllers();

    app.Run();
}
