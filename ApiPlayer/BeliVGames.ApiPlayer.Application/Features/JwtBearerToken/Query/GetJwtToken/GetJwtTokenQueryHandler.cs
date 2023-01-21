using AutoMapper;
using BeliVGames.ApiPlayer.Application.Contracts.Persistence;
using BeliVGames.ApiPlayer.Domain.Entities;
using MediatR;

namespace BeliVGames.ApiPlayer.Application.Features.JwtBearerToken.Query.GetJwtToken;

public class GetJwtTokenQueryHandler:IRequestHandler<GetJwtTokenDetailQuery, JwtTokenListVm>
{
    private readonly IAsyncRepository<UserRefreshTokens> _repository;
    private readonly IMapper _mapper;

    public GetJwtTokenQueryHandler(IAsyncRepository<UserRefreshTokens> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<JwtTokenListVm> Handle(GetJwtTokenDetailQuery request, CancellationToken cancellationToken)
    {
        var jwtToken = (await _repository.ListAllAsync()).Where(x => x.UserName == request.UserName).MinBy(x => x.CreateAt);
        return _mapper.Map<JwtTokenListVm>(jwtToken);
    }
}