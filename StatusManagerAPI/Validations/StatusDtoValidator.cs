using FluentValidation;
using StatusManagerAPI.Models.Dtos;

namespace StatusManagerAPI.Validations
{
    public class StatusDtoValidator : AbstractValidator<StatusDto>
    {
        public StatusDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Status name cannot be empty.")
                .Length(2, 50).WithMessage("Status name must be between 2 and 50 characters long.");
        }
    }
}
