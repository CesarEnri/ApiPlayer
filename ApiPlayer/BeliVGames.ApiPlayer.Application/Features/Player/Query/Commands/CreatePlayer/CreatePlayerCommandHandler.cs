using AutoMapper;
using BeliVGames.ApiPlayer.Application.Contracts.Persistence;
using BeliVGames.ApiPlayer.Application.Features.Player.Query.Commands.CreatePlayer;
using MediatR;

namespace BeliVGames.ApiPlayer.Application.Features.Player.Query.CreatePlayer;

public class CreatePlayerCommandHandler:IRequestHandler<CreatePlayerCommand, Guid>
{
    private readonly IPlayerRepository _playerRepository;
    private readonly IMapper _mapper;

    public CreatePlayerCommandHandler(IMapper mapper, IPlayerRepository playerRepository)
    {
        _mapper = mapper;
        _playerRepository = playerRepository;
    }


    public async Task<Guid> Handle(CreatePlayerCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreatePlayerValidator(_playerRepository);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (validationResult.Errors.Count > 0)
            throw new Exceptions.ValidationException(validationResult);

        var @player = _mapper.Map<Domain.Entities.Player>(request);

        @player = await _playerRepository.AddAsync(@player);

        return @player.PlayerId;

    }
}
