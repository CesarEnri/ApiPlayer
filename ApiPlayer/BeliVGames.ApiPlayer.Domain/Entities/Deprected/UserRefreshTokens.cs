using BeliVGames.ApiPlayer.Domain.Common;

namespace BeliVGames.ApiPlayer.Domain.Entities;

public class UserRefreshTokens: AuditableEntity
{
    public Guid Id { get; set; }
    public string? UserName { get; set; }
    public string? RefreshToken { get; set; }
    public bool IsActive { get; set; }
}