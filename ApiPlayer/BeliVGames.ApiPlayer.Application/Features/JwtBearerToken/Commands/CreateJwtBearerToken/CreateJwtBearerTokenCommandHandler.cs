using AutoMapper;
using BeliVGames.ApiPlayer.Application.Contracts.Persistence;
using BeliVGames.ApiPlayer.Application.Profiles;
using BeliVGames.ApiPlayer.Domain.Entities;
using FluentValidation;
using MediatR;

namespace BeliVGames.ApiPlayer.Application.Features.JwtBearerToken.Commands.CreateJwtBearerToken;

public class CreateJwtBearerTokenCommandHandler:  IRequestHandler<CreateJwtBearerTokenCommand, Guid>
{
    private readonly IMapper _mapper;
    private readonly IJwtBearerTokenRepository _jwtBearerTokenRepository;

    public CreateJwtBearerTokenCommandHandler(IMapper mapper, IJwtBearerTokenRepository jwtBearerTokenRepository)
    {
        _mapper = mapper;
        _jwtBearerTokenRepository = jwtBearerTokenRepository;
    }

    public async Task<Guid> Handle(CreateJwtBearerTokenCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateJwtBearerTokenValidator(_jwtBearerTokenRepository);
        var validationResult = await validator.ValidateAsync(request);
        
        if(validationResult.Errors.Count>0)
            throw new Exceptions.ValidationException(validationResult);
        
        var jwtToken = new UserRefreshTokens
        {
            UserName = request.UserName,
            RefreshToken = request.RefreshToken,
            IsActive = request.IsActive
        };

        jwtToken = await _jwtBearerTokenRepository.AddAsync(@jwtToken);

        return @jwtToken.Id;
    }
}