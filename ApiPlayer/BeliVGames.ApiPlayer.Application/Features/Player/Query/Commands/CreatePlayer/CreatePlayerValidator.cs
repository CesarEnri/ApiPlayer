using BeliVGames.ApiPlayer.Application.Contracts.Persistence;
using FluentValidation;

namespace BeliVGames.ApiPlayer.Application.Features.Player.Query.Commands.CreatePlayer;

public class CreatePlayerValidator:AbstractValidator<CreatePlayerCommand>
{
    private readonly IPlayerRepository _playerRepository;

    public CreatePlayerValidator(IPlayerRepository playerRepository)
    {
        _playerRepository = playerRepository;

        RuleFor(p => p.Email)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull();

        RuleFor(p => p.Name).
            NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull()
            .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters. ");
        
        RuleFor(p => p)
            .MustAsync(PlayerEmailUnique)
            .WithMessage("A Player with the same email and date already exists.");

    }

    private async Task<bool> PlayerEmailUnique(CreatePlayerCommand e, CancellationToken token)
    {
        return !(await _playerRepository.IsPlayerEmailUnique(e.Email));
    }
}