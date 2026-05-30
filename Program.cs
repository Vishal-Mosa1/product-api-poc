var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

var products = new List<Product>
{
    new Product(1, "Laptop", 50000),
    new Product(2, "Monitor", 15000)
};

app.MapGet("/health", () =>
{
    return Results.Ok(new
    {
        Status = "Healthy",
        Time = DateTime.UtcNow
    });
});

app.MapGet("/products", () =>
{
    return Results.Ok(products);
});

app.MapPost("/products", (Product product) =>
{
    products.Add(product);
    return Results.Created($"/products/{product.Id}", product);
});

app.Run();

record Product(int Id, string Name, decimal Price);