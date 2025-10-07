using Game.api.Dtos;                  // твои DTO (с маленькой 'a')
using MinimalApis.Extensions;         // для .WithParameterValidation()
using Microsoft.AspNetCore.Routing;

namespace Game.api.Endpoints;

public static class GamesEndpoints
{
    private const string GetGameEndpointName = "GetGame";

    // демо-данные
    private static readonly List<GameDTO> games = new()
    {
        new(1, "NBA 2k24", "Sport",        123.321m, new DateOnly(2011, 4, 24)),
        new(2, "Tetris",   "Logical game", 333.187m, new DateOnly(1995, 2, 25)),
        new(3, "Roblox",   "Child Game",     1.420m, new DateOnly(2001, 7, 12)),
    };

    public static RouteGroupBuilder MapGamesEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/games")
                       .WithTags("Games")
                       .WithParameterValidation();

        // GET /games
        group.MapGet("/", () => Results.Ok(games))
             .WithName("ListGames");

        // GET /games/{id}
        group.MapGet("/{id:int}", (int id) =>
        {
            var game = games.Find(g => g.id == id); // в твоём DTO свойство 'id'
            return game is null ? Results.NotFound() : Results.Ok(game);
        })
        .WithName(GetGameEndpointName);

        // POST /games
        group.MapPost("/", (CreateGameDTO req) =>
        {
            var newId = games.Count == 0 ? 1 : games[^1].id + 1;

            var game = new GameDTO(
                newId,
                req.Name,
                req.Genre,
                req.Price,
                req.ReleaseDate   // CreateGameDTO.ReleaseDate у тебя не nullable
            );

            games.Add(game);
            return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.id }, game);
        });

        // PUT /games/{id}
        group.MapPut("/{id:int}", (int id, UpdateGameDTO req) =>
        {
            var index = games.FindIndex(g => g.id == id);
            if (index == -1) return Results.NotFound();

            var current = games[index];

            // так как UpdateGameDTO поля НЕ nullable — используем "если пусто, оставь старое".
            // для строк:
            var name  = string.IsNullOrWhiteSpace(req.Name) ? current.Name : req.Name;
            var genre = string.IsNullOrWhiteSpace(req.Genre) ? current.Genre : req.Genre;

            // для decimal — если 0, оставим прежнее (при желании можешь считать 0 валидным и тогда просто req.Price)
            var price = req.Price == default ? current.Price : req.Price;

            // для DateOnly нет '??', используем default-сравнение
            var release = req.ReleaseDate == default ? current.ReleaseDate : req.ReleaseDate;

            games[index] = new GameDTO(id, name, genre, price, release);
            return Results.NoContent();
        });

        // DELETE /games/{id}
        group.MapDelete("/{id:int}", (int id) =>
        {
            var removed = games.RemoveAll(g => g.id == id);
            return removed == 0 ? Results.NotFound() : Results.NoContent();
        });

        return group;
    }
}
