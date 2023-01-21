using System.ComponentModel.DataAnnotations;
using BeliVGames.ApiPlayer.Application.Contracts.Persistence;
using BeliVGames.ApiPlayer.Domain.Entities;

namespace BeliVGames.ApiPlayer.Persistence.Repositories;

public class PlayerRepository:BaseRepository<Player>, IPlayerRepository
{
    public PlayerRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public Task<bool> IsPlayerEmailUnique(EmailAddressAttribute email)
    {
        throw new NotImplementedException();
    }
}