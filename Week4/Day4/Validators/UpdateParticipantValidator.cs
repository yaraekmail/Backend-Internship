using FluentValidation;

// Validator responsible for validating UpdateParticipantRequest.
public class UpdateParticipantValidator : AbstractValidator<UpdateParticipantRequest>
{
    public UpdateParticipantValidator()
    {
        // The participant name must not be empty.
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name is required.");

        // The participant name should not exceed 100 characters.
        RuleFor(x => x.Name)
            .MaximumLength(100)
            .WithMessage("Name must not exceed 100 characters.");

        // The participant email must have a valid email format.
        RuleFor(x => x.Email)
            .EmailAddress()
            .WithMessage("Email must be a valid email address.");
    }
}