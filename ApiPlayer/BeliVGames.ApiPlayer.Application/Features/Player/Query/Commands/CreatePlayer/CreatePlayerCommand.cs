using System.ComponentModel.DataAnnotations;
using MediatR;

namespace BeliVGames.ApiPlayer.Application.Features.Player.Query.CreatePlayer;

public class CreatePlayerCommand: IRequest<Guid>
{
    public EmailAddressAttribute Email { get; set; }
    public string Password { get; set; }
    public string Name { get; set; }
    public int CurrentLevel { get; set; }
    public int CurrentMoney { get; set; }
    public string? LastAccess { get; set; }
    public Guid PlayerDetailId { get; set; }
    
    public override string ToString()
    {
        return
            $"Player name: {Name}; CurrentLevel: {CurrentLevel}; By: {CurrentMoney}; On: {LastAccess}; Description: {LastAccess}";
    }
}