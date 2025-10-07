namespace Game.api.Dtos;

public record class GameDTO(
    int id, 
    string Name, 
    string Genre,
    decimal Price,
    DateOnly ReleaseDate);

    
