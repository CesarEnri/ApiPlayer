namespace BeliVGames.ApiPlayer.Application.Features.JwtBearerToken.Query.GetJwtToken;

public class JwtTokenVm
{
    public Guid Id { get; set; }
    public string? UserName { get; set; }
    public string? RefreshToken { get; set; }
    public bool IsActive { get; set; }
}