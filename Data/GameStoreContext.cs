using System;
using Game.api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Game.api.Data;

public class GameStoreContext(DbContextOptions<GameStoreContext> options )
    :DbContext(options)
{
    public DbSet<Game.api.Entities.Game> Games => Set<Game.api.Entities.Game>();

    public DbSet<Genre> Genres => Set<Genre>();
}
