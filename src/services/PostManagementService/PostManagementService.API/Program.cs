
using Empower.Infrastructure.EventBus.Interfaces;
using Microsoft.EntityFrameworkCore.Infrastructure;
using PostManagementService.Events;
using PostManagementService.Repository;
using PostService.Data;
using Services.Common;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.AddServiceDefaults();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(Program)));

 static void ConfigureSqlOptions(SqlServerDbContextOptionsBuilder sqlOptions)
{
    sqlOptions.MigrationsAssembly(typeof(Program).Assembly.FullName);

    // Configuring Connection Resiliency: https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency 

    sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
};

builder.Services.AddDbContext<PostContext>(options => {
        options.UseSqlServer(builder.Configuration.GetRequiredConnectionString("PostDB"), ConfigureSqlOptions);
});


builder.Services.AddScoped<IPostRepository, PostRepository>();


var app = builder.Build();

app.UseServiceDefaults();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
