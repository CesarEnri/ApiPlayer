using AutoMapper;
using BeliVGames.ApiPlayer.Application.Contracts.Persistence;
using MediatR;

namespace BeliVGames.ApiPlayer.Application.Features.Player.Query.DeletePlayer;

public class DeletePlayerCommandHandler: IRequestHandler<DeletePlayerCommand>
{
    private readonly IAsyncRepository<Domain.Entities.Player?> _playerRepository;
    private readonly IMapper _mapper;
    
    public DeletePlayerCommandHandler(IAsyncRepository<Domain.Entities.Player?> playerRepository, IMapper mapper)
    {
        _playerRepository = playerRepository;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(DeletePlayerCommand request, CancellationToken cancellationToken)
    {
        var playerToDelete = await _playerRepository.GetByIdAsync(request.PlayerId);
        await _playerRepository.DeleteAsync(playerToDelete);
        return Unit.Value;
    }
}