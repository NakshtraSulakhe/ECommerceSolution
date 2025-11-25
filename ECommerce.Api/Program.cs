using ECommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using ECommerce.Domain.Interfaces;
using ECommerce.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore.SqlServer; // Add this using directive

{
    var builder = WebApplication.CreateBuilder(args);

    // Add DB Connection
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

    // Add Services
    builder.Services.AddScoped<IProductRepository, ProductRepository>();

    // Add controllers
    builder.Services.AddControllers();
    
    // Swagger
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var app = builder.Build();

    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseHttpsRedirection();

    app.MapControllers();

    app.Run();
}
