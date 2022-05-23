using AutoMapper;
using BeliVGames.ApiPlayer.Application.Contracts.Persistence;
using BeliVGames.ApiPlayer.Domain.Entities;
using MediatR;

namespace BeliVGames.ApiPlayer.Application.Features.JwtBearerToken.Commands.UpdateJwtBearerToken;

public class UpdateJwtBearerTokenCommandHandler: IRequestHandler<UpdateJwtBearerTokenCommand>
{
    private readonly IAsyncRepository<UserRefreshTokens> _repository;
    private readonly IMapper _mapper;

    public UpdateJwtBearerTokenCommandHandler(IAsyncRepository<UserRefreshTokens> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(UpdateJwtBearerTokenCommand request, CancellationToken cancellationToken)
    {
        var jwtTokenToUpdate = await _repository.GetByIdAsync(request.Id);

        _mapper.Map(request, jwtTokenToUpdate, typeof(UpdateJwtBearerTokenCommand), typeof(UserRefreshTokens));

        await _repository.UpdateAsync(jwtTokenToUpdate);
        return Unit.Value;
    }
}