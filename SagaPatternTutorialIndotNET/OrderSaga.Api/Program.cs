using OrderSaga.Api.Inventory;
using OrderSaga.Api.Payment;
using OrderSaga.Api.Shipping;
using OrderSaga.Api.Saga;
using OrderSaga.Api.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Register services with dependency injection
builder.Services.AddTransient<InventoryService>();
builder.Services.AddTransient<IPaymentService>();
builder.Services.AddTransient<IShippingService>();
builder.Services.AddTransient<OrderSagaService>();

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

app.MapPost("/api/orders", async (Order order, OrderSagaService orderSaga) =>
{
    bool success = await orderSaga.ProcessOrderAsync(order);
    if (success)
    {
        return Results.Ok("Order processed successfully");
    }
    else
    {
        return Results.BadRequest("Order processing failed and compensated");
    }
});

app.Run();

