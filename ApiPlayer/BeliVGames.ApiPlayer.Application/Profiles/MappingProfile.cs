using AutoMapper;
using BeliVGames.ApiPlayer.Application.Features.JwtBearerToken.Commands.CreateJwtBearerToken;
using BeliVGames.ApiPlayer.Domain.Entities;

namespace BeliVGames.ApiPlayer.Application.Profiles;

public class MappingProfile: Profile
{
    public MappingProfile()
    {
        CreateMap<UserRefreshTokens, CreateJwtBearerTokenCommand>();
        
        CreateMap<UserRefreshTokens, CreateJwtBearerTokenCommand>().ReverseMap();
    }
}