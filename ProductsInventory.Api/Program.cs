using Microsoft.EntityFrameworkCore;
using ProductsInventory.Api.Middlewares;
using ProductsInventory.Business;
using ProductsInventory.Business.Abstractions;
using ProductsInventory.Business.Kafka;
using ProductsInventory.Business.Kafka.MessageHandler;
using ProductsInventory.Repository;
using ProductsInventory.Repository.Abstractions;
using Utility.Kafka.Abstraction.MessageHandlers;
using Utility.Kafka.DependencyInjection;
using Utility.Kafka.Services;
using ProducerServiceWithSubscription = Utility.Kafka.Services.ProducerServiceWithSubscription;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<ProductsInventoryDbContext>(options => options.UseSqlServer("name=ConnectionStrings:ProductsInventoryDbContext", b => b.MigrationsAssembly("ProductsInventory.Api")));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IRepository, Repository>();
builder.Services.AddScoped<IBusiness, Business>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddKafkaConsumerAndProducer<KafkaTopicsInput,KafkaTopicsOutput, MessageHandlerFactory, ProducerServiceWithSubscription>(builder.Configuration);

var app = builder.Build();
app.UseMiddleware<ExceptionMiddleware>();


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


