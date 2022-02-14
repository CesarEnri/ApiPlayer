using AutoMapper;
using BeliVGames.ApiPlayer.Application.Features.Player.Query.CreatePlayer;
using BeliVGames.ApiPlayer.Application.Features.Player.Query.DeletePlayer;
using BeliVGames.ApiPlayer.Application.Features.Player.Query.GetPlayerDetail;
using BeliVGames.ApiPlayer.Application.Features.Player.Query.GetPlayerList;
using BeliVGames.ApiPlayer.Application.Features.Player.Query.UpdatePlayer;
using BeliVGames.ApiPlayer.Domain.Entities;

namespace BeliVGames.ApiPlayer.Application.Profiles;

public class MappingProfile: Profile
{
    public MappingProfile()
    {
        CreateMap<Player, PlayerListVm>().ReverseMap();
        CreateMap<Player, CreatePlayerCommand>().ReverseMap();
        CreateMap<Player, UpdatePlayerCommand>().ReverseMap();
        CreateMap<Player, DeletePlayerCommand>().ReverseMap();
        CreateMap<Player, PlayerDetailVm>().ReverseMap();
        CreateMap<Player, PlayerDetailDto>().ReverseMap();

        // CreateMap<PlayerDetail, PlayerDetailDto>();
        // CreateMap<PlayerDetail, PlayerDetailVm>();
        
    }
}