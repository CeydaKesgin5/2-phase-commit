var builder = WebApplication.CreateBuilder(args);


var app = builder.Build();


app.MapGet("/ready", () =>
{
    Console.WriteLine("Stock service is ready");
});

app.MapGet("/commit", () =>
{
    Console.WriteLine("Stock service is committed");
});
app.MapGet("/rollback", () =>
{
    Console.WriteLine("Stock service is rollbacked");
});
app.Run();
