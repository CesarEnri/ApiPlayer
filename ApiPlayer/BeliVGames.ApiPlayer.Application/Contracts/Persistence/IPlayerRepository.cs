using System.ComponentModel.DataAnnotations;
using BeliVGames.ApiPlayer.Domain.Entities;

namespace BeliVGames.ApiPlayer.Application.Contracts.Persistence;

public interface IPlayerRepository:IAsyncRepository<Player>
{
    Task<bool> IsPlayerEmailUnique(EmailAddressAttribute email);
}