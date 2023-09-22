using CommentManagementService.Events;
using Empower.Infrastructure.EventBus.Interfaces;
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

app.MapControllers();

eventBus.Subscribe<DeletePostEvent, IIntegrationEventHandler<DeletePostEvent>>();

app.Run();
