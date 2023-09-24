using System.Threading.RateLimiting;
using CommentManagementService.Events;
using CommentManagementService.Repository;
using Empower.Infrastructure.EventBus.Interfaces;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Services.Common;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.AddServiceDefaults();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IIntegrationEventHandler<DeletePostEvent>, DeletePostEventHandler>();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(Program)));
static void ConfigureSqlOptions(SqlServerDbContextOptionsBuilder sqlOptions)
{
    sqlOptions.MigrationsAssembly(typeof(Program).Assembly.FullName);

    // Configuring Connection Resiliency: https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency 

    sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
};

builder.Services.AddDbContext<CommentContext>(options => {
        options.UseSqlServer(builder.Configuration.GetRequiredConnectionString("CommentDB"), ConfigureSqlOptions);
});


builder.Services.AddScoped<ICommentRepository, CommentRepository>();

builder.Services.AddRateLimiter(rateLimiterOptions =>
{
    rateLimiterOptions.RejectionStatusCode = 429;
    rateLimiterOptions.AddSlidingWindowLimiter("sliding", options =>
    {
        options.PermitLimit = 10;
        options.Window = TimeSpan.FromSeconds(10);
        options.SegmentsPerWindow = 2;
        options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        options.QueueLimit = 5;
    });
});



var app = builder.Build();
app.UseServiceDefaults();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var eventBus = app.Services.GetRequiredService<IEventBus>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseRateLimiter();
app.MapControllers();

eventBus.Subscribe<DeletePostEvent, IIntegrationEventHandler<DeletePostEvent>>();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<CommentContext>();
    await context.Database.MigrateAsync();
}

app.Run();
