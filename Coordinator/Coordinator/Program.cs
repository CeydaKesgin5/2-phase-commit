using Coordinator.Models.Contexts;
using Coordinator.Services;
using Coordinator.Services.Abstractions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<TwoPhaseCommitContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("SQLServer")));


builder.Services.AddHttpClient("OrderAPI", client => client.BaseAddress = new("https://localhost:7040/"));
builder.Services.AddHttpClient("StockAPI", client => client.BaseAddress = new("https://localhost:7123/"));
builder.Services.AddHttpClient("PaymentAPI", client => client.BaseAddress = new("https://localhost:7101/"));


builder.Services.AddTransient<ITransactionService, TransactionService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.Map("/create-order-transaction", async (ITransactionService transactionService) =>

{
    //Phase 1 - Prepare
    var transactionId = await transactionService.CreateTransactionAsync();
    await transactionService.PrepareServicesAsync(transactionId);
    bool transactionState = await transactionService.CheckReadyServicesAsync(transactionId);
    if(transactionState)
    {
        //Phase 2- Commit
        await transactionService.CommitAsync(transactionId);
        transactionState = await transactionService.CheckTransactionStateServicesAsync(transactionId);
    }
    if (!transactionState)
        await transactionService.RoolbackAsync(transactionId);

});

app.Run();
