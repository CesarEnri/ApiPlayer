using BeliVGames.ApiPlayer.Application.Contracts.Persistence;
using FluentValidation;

namespace BeliVGames.ApiPlayer.Application.Features.JwtBearerToken.Commands.CreateJwtBearerToken;

public class CreateJwtBearerTokenValidator: AbstractValidator<CreateJwtBearerTokenCommand>
{
    private readonly IJwtBearerTokenRepository _jwtBearerTokenRepository;

    public CreateJwtBearerTokenValidator(IJwtBearerTokenRepository jwtBearerTokenRepository)
    {
        _jwtBearerTokenRepository = jwtBearerTokenRepository;

        RuleFor(p => p.UserName).NotEmpty().WithMessage("{PropertyName} is required.").NotNull();
    }
}