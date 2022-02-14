using AutoMapper;
using BeliVGames.ApiPlayer.Application.Contracts.Persistence;
using MediatR;

namespace BeliVGames.ApiPlayer.Application.Features.Player.Query.UpdatePlayer;

public class UpdatePlayerCommandHandler: IRequestHandler<UpdatePlayerCommand>
{
    private readonly IAsyncRepository<Domain.Entities.Player> _playerRepository;
    private readonly IMapper _mapper;

    public UpdatePlayerCommandHandler(IAsyncRepository<Domain.Entities.Player> playerRepository, IMapper mapper)
    {
        _playerRepository = playerRepository;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(UpdatePlayerCommand request, CancellationToken cancellationToken)
    {
        var playerToUpdate = await _playerRepository.GetByIdAsync(request.PlayerId);

        _mapper.Map(request, playerToUpdate, typeof(UpdatePlayerCommand), typeof(Domain.Entities.Player));

        await _playerRepository.UpdateAsync(playerToUpdate);
        return Unit.Value;
    }
}