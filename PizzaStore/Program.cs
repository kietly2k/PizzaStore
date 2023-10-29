using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PizzaStore.Models;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddDbContext<PizzaDb>(options => options.UseInMemoryDatabase("items"));
var connectionString = builder.Configuration.GetConnectionString("Pizzas") ?? "DataSource=Pizzas.db";
builder.Services.AddSqlite<PizzaDb>(connectionString);

builder.Services.AddEndpointsApiExplorer();
// sets up header information on your API, like what it's called and the version
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "PizzaStore API", Description = "Making the Pizzas you love", Version = "v1" });
});

// 1) define a unique string
string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

// 2) define allowed domains, in this case "http://example.com" and "*" = all
//    domains, for testing purposes only.
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
      builder =>
      {
          builder.WithOrigins("http://example.com", "*");
      });
});

var app = builder.Build();
// These two code lines tell the API project to use Swagger and also where to find the specification file swagger.json.
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "PizzaStore API V1");
});

// 3) use the capability
app.UseCors(MyAllowSpecificOrigins);
app.MapGet("/", () => "Hello World!");
app.MapGet("/pizzas", async (PizzaDb db) => await db.Pizzas.ToListAsync());
app.MapPost("/pizzas", async (PizzaDb db, Pizza pizza) =>
{
    await db.Pizzas.AddAsync(pizza);
    await db.SaveChangesAsync();
    return Results.Created($"/pizza/{pizza.Id}", pizza);
});
app.MapGet("/pizzas/{id}", async (PizzaDb db, int id) => await db.Pizzas.FindAsync(id));
app.MapPut("/pizzas/{id}", async (PizzaDb db, Pizza pizzaUpdated, int id) =>
{
    Pizza? pizza = await db.Pizzas.FindAsync(id);
    if (pizza == null)
        return Results.NotFound();

    pizza.Name = pizzaUpdated.Name;
    pizza.Description = pizzaUpdated.Description;
    await db.SaveChangesAsync();

    return Results.NoContent();
});
app.MapDelete("/pizzas/{id}", async (PizzaDb db, int id) =>
{

    Pizza? pizza = await db.Pizzas.FindAsync(id);
    if (pizza == null)
        return Results.NotFound();

    db.Pizzas.Remove(pizza);
    await db.SaveChangesAsync();
    return Results.Ok();
});

app.Run();
