using Game.api.Endpoints;
using Game.api.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<GameStoreContext>(opt =>
    opt.UseSqlite(builder.Configuration.GetConnectionString("Default") 
                  ?? "Data Source=Game.db"));


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.MapGamesEndpoints();   // именно так, не GamesEndpoints.Map(app)


using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<GameStoreContext>();
    db.Database.EnsureCreated();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapGet("/", () => Results.Redirect("/swagger"));
app.Run();

public partial class Program { }
