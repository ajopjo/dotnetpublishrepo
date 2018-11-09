using FluentValidation;
using Inventory.Models;
using JetBrains.Annotations;

namespace Inventory.Validators
{
    [UsedImplicitly]
    public class InventoryRequestValidator : AbstractValidator<InventoryRequest>
    {
        public InventoryRequestValidator()
        {
            RuleFor(request => request.MaterialNumbers)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .Must(BeNotNullOrEmpty)
                    .WithMessage(request =>  $"'{nameof(request.MaterialNumbers)}' cannot be empty.")
                .Matches(@"^[0-9a-zA-Z|]+$")
                    .WithMessage(request => $"'{nameof(request.MaterialNumbers)}' should contain alphanumeric characters only and special character |.")
                .Matches(@"^([a-zA-Z0-9]+)(\|[a-zA-Z0-9]+)*$")
                    .WithMessage(request => $"'{nameof(request.MaterialNumbers)}' has an invalid format.");

            RuleFor(request => request.CustomerNumber)
                .MaximumLength(10)
                .Matches(@"^[0-9a-zA-Z]+$")
                .WithMessage(request => $"'{nameof(request.CustomerNumber)}' should contain alphanumeric characters only.");

            RuleFor(request => request.PlantId)
                .MaximumLength(4)
                .Matches(@"^[0-9a-zA-Z]+$")
                .WithMessage(request => $"'{nameof(request.PlantId)}' should contain alphanumeric characters only.");
        }

        public bool BeNotNullOrEmpty(string value) => !string.IsNullOrWhiteSpace(value);
    }
}