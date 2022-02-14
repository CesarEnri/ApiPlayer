using AutoMapper;
using BeliVGames.ApiPlayer.Application.Contracts.Persistence;
using BeliVGames.ApiPlayer.Application.Features.Player.Query.GetPlayerDetail;
using BeliVGames.ApiPlayer.Domain.Entities;
using MediatR;

namespace BeliVGames.ApiPlayer.Application.Features.Player.Query.GetPlayerList;

public class GetPlayerListQueryHandler: IRequestHandler<GetPlayerDetailQuery, PlayerDetailVm>
{
    private readonly IAsyncRepository<Domain.Entities.Player> _playerRepository;
    private readonly IAsyncRepository<PlayerDetail> _playerDetailRepository;
    private readonly IMapper _mapper;

    public GetPlayerListQueryHandler(IAsyncRepository<Domain.Entities.Player> playerRepository, IAsyncRepository<PlayerDetail> playerDetailRepository, IMapper mapper)
    {
        _playerRepository = playerRepository;
        _playerDetailRepository = playerDetailRepository;
        _mapper = mapper;
    }

    public async Task<PlayerDetailVm> Handle(GetPlayerDetailQuery request, CancellationToken cancellationToken)
    {
        var @player = await _playerRepository.GetByIdAsync(request.Id);
        var playerDetailDto = _mapper.Map<PlayerDetailVm>(@player);

        var detail = await _playerDetailRepository.GetByIdAsync(@player.PlayerId);

        playerDetailDto.PlayerDetail = _mapper.Map<PlayerDetailDto>(detail);
        
        return playerDetailDto;
    }
}