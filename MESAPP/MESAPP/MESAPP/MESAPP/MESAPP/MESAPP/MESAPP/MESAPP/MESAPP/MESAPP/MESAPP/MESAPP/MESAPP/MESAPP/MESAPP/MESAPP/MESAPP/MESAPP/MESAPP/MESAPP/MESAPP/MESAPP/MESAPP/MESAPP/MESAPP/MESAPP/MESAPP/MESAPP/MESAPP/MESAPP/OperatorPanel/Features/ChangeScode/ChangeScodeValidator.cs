using FluentValidation;
using OperatorPanel.Features.Commands.ChangeScode;

public class ChangeScodeCommandValidator : AbstractValidator<ChangeScodeCommand>
{
    private static readonly int[] ValidScodes = new[]
    {
        10,
        21, 22, 23, 24, 25,
        31, 32, 33, 34, 35,
        41, 42, 43, 44, 45,
        51, 52, 53, 54, 55
    };

    public ChangeScodeCommandValidator()
    {
        RuleFor(x => x.WorkstationId)
            .GreaterThan(0).WithMessage("Geçersiz Workstation ID.");

        RuleFor(x => x.OperatorId)
            .GreaterThan(0).WithMessage("Geçersiz Operatör ID.");

        RuleFor(x => x.NewScode)
            .Must(code => ValidScodes.Contains(code))
            .WithMessage("Geçersiz SCODE değeri. Sistem sadece ön tanımlı SCODE'ları kabul eder.");

    }
}